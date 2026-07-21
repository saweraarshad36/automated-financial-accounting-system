using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
public partial class Pages_TrialBalance : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.AppSettings["ConSqlWeb"]);

    private DataTable dtFinalTrialBalance;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            lblUsername.Text = Session["Username"].ToString();
            LoadTrialBalance();
        }
    }
    protected void gvTrialBalance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTrialBalance.PageIndex = e.NewPageIndex;

        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            DateTime fromDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime toDate = Convert.ToDateTime(txtToDate.Text);

            LoadTrialBalance(fromDate, toDate, ddlShowEntries.SelectedValue);
        }
        else
        {
            LoadTrialBalance(null, null, ddlShowEntries.SelectedValue);
        }
    }
    protected void ddlShowEntries_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            DateTime fromDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime toDate = Convert.ToDateTime(txtToDate.Text);

            LoadTrialBalance(fromDate, toDate, ddlShowEntries.SelectedValue);
        }
        else
        {
            LoadTrialBalance(null, null, ddlShowEntries.SelectedValue);
        }
    }
    private void LoadTrialBalance(DateTime? fromDate = null, DateTime? toDate = null, string statusFilter = null)
    {
        try
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            if (statusFilter == null)
                statusFilter = ddlShowEntries.SelectedValue;

            string query = @"
SELECT
    fs.AccountCode,
    fs.AccountDescription AS AccountName,
    (
        CASE
            WHEN fs.BalanceStatus='Debit' THEN fs.OpeningBalance
            ELSE -fs.OpeningBalance
        END
    )
    +
    ISNULL(
        SUM(
            CASE
                WHEN je.DC='Debit' THEN je.Amount
                WHEN je.DC='Credit' THEN -je.Amount
                ELSE 0
            END
        ),0
    ) AS NetBalance
FROM FinanceStructure fs
LEFT JOIN JournalEntry je
    ON fs.AccountCode = je.AccountCode
    AND fs.UserId = je.UserId
    AND (@StatusFilter = 'All' OR je.Status = @StatusFilter)
    AND (@FromDate IS NULL OR (je.TransactionDate >= @FromDate AND je.TransactionDate < DATEADD(DAY,1,@ToDate)))
WHERE fs.UserId = @UserId
GROUP BY
    fs.AccountCode,
    fs.AccountDescription,
    fs.OpeningBalance,
    fs.BalanceStatus
HAVING (@FromDate IS NULL) OR (COUNT(je.AccountCode) > 0)
ORDER BY fs.AccountCode";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@StatusFilter", statusFilter);

            if (fromDate.HasValue && toDate.HasValue)
            {
                cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = fromDate.Value.Date;
                cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = toDate.Value.Date;
            }
            else
            {
                cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = DBNull.Value;
                cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = DBNull.Value;
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            decimal totalDebit = 0;
            decimal totalCredit = 0;

            DataTable finalTable = new DataTable();
            finalTable.Columns.Add("AccountCode");
            finalTable.Columns.Add("AccountName");
            finalTable.Columns.Add("Debit", typeof(decimal));
            finalTable.Columns.Add("Credit", typeof(decimal));

            foreach (DataRow row in dt.Rows)
            {
                decimal bal = Convert.ToDecimal(row["NetBalance"]);
                decimal debit = bal > 0 ? bal : 0;
                decimal credit = bal < 0 ? Math.Abs(bal) : 0;

                totalDebit += debit;
                totalCredit += credit;

                finalTable.Rows.Add(row["AccountCode"], row["AccountName"], debit, credit);
            }

            gvTrialBalance.DataSource = finalTable;
            gvTrialBalance.DataBind();
            dtFinalTrialBalance = finalTable;

            lblTotalDebit.Text = totalDebit.ToString("N2");
            lblTotalCredit.Text = totalCredit.ToString("N2");

            if (dt.Rows.Count == 0)
            {
                lblMessage.ForeColor = Color.OrangeRed;
                lblMessage.Text = "No ledger records found.";
                return;
            }

            if (Math.Abs(totalDebit - totalCredit) > 0.01m)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Trial Balance is NOT balanced. Difference = "
                                + Math.Abs(totalDebit - totalCredit).ToString("N2");
            }
            else
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Trial Balance Loaded Successfully.";
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }
    protected void btnExportPdf_Click(object sender, EventArgs e)
    {   
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            DateTime fromDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime toDate = Convert.ToDateTime(txtToDate.Text);
            LoadTrialBalance(fromDate, toDate, ddlShowEntries.SelectedValue);
        }
        else
        {
            LoadTrialBalance(null, null, ddlShowEntries.SelectedValue);
        }

        Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

        using (MemoryStream ms = new MemoryStream())
        {
            PdfWriter.GetInstance(doc, ms);
            doc.Open();

            iTextSharp.text.Font titleFont =
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18,
                iTextSharp.text.Font.UNDERLINE);

            iTextSharp.text.Font boldFont =
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);

            iTextSharp.text.Font normalFont =
                FontFactory.GetFont(FontFactory.HELVETICA, 11);

            PdfPTable header = new PdfPTable(2);
            header.WidthPercentage = 100;
            header.SetWidths(new float[] { 1.5f, 8.5f });

            string logoPath = Server.MapPath("~/Images/whitelogo.png");

            if (File.Exists(logoPath))
            {
                iTextSharp.text.Image logo =
                    iTextSharp.text.Image.GetInstance(logoPath);

                logo.ScaleToFit(55f, 55f);

                PdfPCell logoCell = new PdfPCell(logo);
                logoCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                logoCell.HorizontalAlignment = Element.ALIGN_LEFT;
                logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                header.AddCell(logoCell);
            }
            else
            {
                PdfPCell blank = new PdfPCell(new Phrase(""));
                blank.Border = iTextSharp.text.Rectangle.NO_BORDER;
                header.AddCell(blank);
            }

            Paragraph heading = new Paragraph();
            heading.Alignment = Element.ALIGN_CENTER;
            heading.Add(new Chunk("Trial Balance Report", titleFont));
            heading.Add(Chunk.NEWLINE);
            heading.Add(new Chunk("Showing : " + ddlShowEntries.SelectedItem.Text, normalFont));

            PdfPCell titleCell = new PdfPCell();
            titleCell.AddElement(heading);
            titleCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
            titleCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            header.AddCell(titleCell);

            doc.Add(header);
            doc.Add(new Paragraph(" "));

            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 2.5f, 5f, 2f, 2f });
            table.HeaderRows = 1;
            table.SplitLate = false;
            table.SplitRows = true;

            PdfPCell cell;
            cell = new PdfPCell(new Phrase(new Chunk("Account Code", boldFont)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(new Chunk("Account Name", boldFont)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(new Chunk("Debit", boldFont)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(new Chunk("Credit", boldFont)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            if (dtFinalTrialBalance != null)
            {
                foreach (DataRow row in dtFinalTrialBalance.Rows)
                {
                    decimal debit = Convert.ToDecimal(row["Debit"]);
                    decimal credit = Convert.ToDecimal(row["Credit"]);

                    cell = new PdfPCell(new Phrase(row["AccountCode"].ToString(), normalFont));
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(row["AccountName"].ToString(), normalFont));
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(debit.ToString("N2"), normalFont));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(credit.ToString("N2"), normalFont));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cell);
                }
            }

            doc.Add(table);

            doc.Add(new Paragraph(" "));

            Paragraph p1 = new Paragraph();
            p1.Add(new Chunk("Total Debit : ", boldFont));
            p1.Add(new Chunk(lblTotalDebit.Text, normalFont));
            doc.Add(p1);

            Paragraph p2 = new Paragraph();
            p2.Add(new Chunk("Total Credit : ", boldFont));
            p2.Add(new Chunk(lblTotalCredit.Text, normalFont));
            doc.Add(p2);

            doc.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=TrialBalance.pdf");
            Response.Buffer = true;
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text == "" || txtToDate.Text == "")
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "Select dates first.";
            return;
        }

        DateTime fromDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime toDate = Convert.ToDateTime(txtToDate.Text);

        if (toDate < fromDate)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "Invalid Date Range.";
            return;
        }

        LoadTrialBalance(fromDate, toDate, ddlShowEntries.SelectedValue);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlShowEntries.SelectedValue = "Posted";

        LoadTrialBalance(null, null, "Posted");
    }
    protected void btnToday_Click(object sender, EventArgs e)
    {
        DateTime today = DateTime.Today;

        txtFromDate.Text = today.ToString("yyyy-MM-dd");
        txtToDate.Text = today.ToString("yyyy-MM-dd");

        LoadTrialBalance(today, today, ddlShowEntries.SelectedValue);
    }
    protected void btnMonth_Click(object sender, EventArgs e)
    {
        DateTime start = new DateTime(
            DateTime.Today.Year,
            DateTime.Today.Month,
            1);

        DateTime end = start.AddMonths(1).AddDays(-1);

        txtFromDate.Text = start.ToString("yyyy-MM-dd");
        txtToDate.Text = end.ToString("yyyy-MM-dd");

        LoadTrialBalance(start, end, ddlShowEntries.SelectedValue);
    }
    protected void btnFY_Click(object sender, EventArgs e)
    {
        DateTime today = DateTime.Today;

        DateTime start = new DateTime(today.Year, 1, 1);
        DateTime end = new DateTime(today.Year, 12, 31);

        txtFromDate.Text = start.ToString("yyyy-MM-dd");
        txtToDate.Text = end.ToString("yyyy-MM-dd");

        LoadTrialBalance(start, end, ddlShowEntries.SelectedValue);
    }
}
