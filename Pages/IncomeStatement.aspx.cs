using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
public partial class Pages_IncomeStatement : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["ConSqlWeb"];

    private DataTable dtIncomeData;
    private DataTable dtExpenseData;
    private decimal totalIncomeAmt;
    private decimal totalExpenseAmt;
    private string currentFromDate;
    private string currentToDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            lblUsername.Text = Session["Username"].ToString();
            LoadIncomeExpense(null, null);
        }
    }
    private void LoadIncomeExpense(string fromDate, string toDate)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            con.Open();

            int userId = Convert.ToInt32(Session["UserId"]);

            SqlCommand cmdIncome = new SqlCommand(@"

        SELECT 
            fs.AccountCode,
            fs.AccountDescription,

            ISNULL(SUM(
                CASE  
                    WHEN UPPER(LTRIM(RTRIM(j.DC))) = 'CREDIT'
                        THEN j.Amount 

                    WHEN UPPER(LTRIM(RTRIM(j.DC))) = 'DEBIT'
                        THEN -j.Amount 
                END
            ),0) AS Amount

        FROM FinanceStructure fs

        LEFT JOIN JournalEntry j
            ON fs.AccountCode = j.AccountCode

        WHERE fs.ParentAccountCode = 3
        AND fs.UserId = @UserId

        AND (
                @FromDate IS NULL
                OR j.TransactionDate BETWEEN @FromDate AND @ToDate
            )

        GROUP BY 
            fs.AccountCode,
            fs.AccountDescription

        ORDER BY fs.AccountCode

        ", con);

            cmdIncome.Parameters.AddWithValue("@UserId", userId);

            
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                cmdIncome.Parameters.AddWithValue("@FromDate",
                    Convert.ToDateTime(fromDate));

                cmdIncome.Parameters.AddWithValue("@ToDate",
                    Convert.ToDateTime(toDate));
            }
            else
            {
                cmdIncome.Parameters.AddWithValue("@FromDate", DBNull.Value);
                cmdIncome.Parameters.AddWithValue("@ToDate", DBNull.Value);
            }

            DataTable dtIncome = new DataTable();
            new SqlDataAdapter(cmdIncome).Fill(dtIncome);

            gvIncome.DataSource = dtIncome;
            gvIncome.DataBind();

            SqlCommand cmdExpense = new SqlCommand(@"

        SELECT 
            fs.AccountCode,
            fs.AccountDescription,

            ISNULL(SUM(
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(j.DC))) = 'DEBIT'
                        THEN j.Amount 

                    WHEN UPPER(LTRIM(RTRIM(j.DC))) = 'CREDIT'
                        THEN -j.Amount 
                END
            ),0) AS Amount

        FROM FinanceStructure fs

        LEFT JOIN JournalEntry j
            ON fs.AccountCode = j.AccountCode

        WHERE fs.ParentAccountCode = 4
        AND fs.UserId = @UserId

        AND (
                @FromDate IS NULL
                OR j.TransactionDate BETWEEN @FromDate AND @ToDate
            )

        GROUP BY 
            fs.AccountCode,
            fs.AccountDescription

        ORDER BY fs.AccountCode

        ", con);

            cmdExpense.Parameters.AddWithValue("@UserId", userId);

            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                cmdExpense.Parameters.AddWithValue("@FromDate",
                    Convert.ToDateTime(fromDate));

                cmdExpense.Parameters.AddWithValue("@ToDate",
                    Convert.ToDateTime(toDate));
            }
            else
            {
                cmdExpense.Parameters.AddWithValue("@FromDate", DBNull.Value);
                cmdExpense.Parameters.AddWithValue("@ToDate", DBNull.Value);
            }

            DataTable dtExpense = new DataTable();
            new SqlDataAdapter(cmdExpense).Fill(dtExpense);

            gvExpense.DataSource = dtExpense;
            gvExpense.DataBind();

            decimal totalIncome = 0;
            decimal totalExpense = 0;

            if (dtIncome.Rows.Count > 0)
            {
                totalIncome = dtIncome.AsEnumerable()
                    .Sum(r => r.Field<decimal>("Amount"));
            }

            if (dtExpense.Rows.Count > 0)
            {
                totalExpense = dtExpense.AsEnumerable()
                    .Sum(r => r.Field<decimal>("Amount"));
            }

            lblIncomeTotal.Text = totalIncome.ToString("N2");
            lblExpenseTotal.Text = totalExpense.ToString("N2");

            decimal netProfit = totalIncome - totalExpense;

            dtIncomeData = dtIncome;
            dtExpenseData = dtExpense;
            totalIncomeAmt = totalIncome;
            totalExpenseAmt = totalExpense;
            currentFromDate = fromDate;
            currentToDate = toDate;

           
            if (string.IsNullOrEmpty(fromDate))
            {
                lblMsg.Text = "Showing ALL records";
            }
            else
            {
                lblMsg.Text = "Filtered from "
                    + fromDate + " to " + toDate;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string fromDate = txtFromDate.Text.Trim();
        string toDate = txtToDate.Text.Trim();

        
        if (string.IsNullOrEmpty(fromDate) != string.IsNullOrEmpty(toDate))
        {
            lblMsg.Text = "Please select both From Date and To Date.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }

        
        if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
        {
            DateTime dFrom, dTo;
            bool validFrom = DateTime.TryParse(fromDate, out dFrom);
            bool validTo = DateTime.TryParse(toDate, out dTo);

            if (!validFrom || !validTo)
            {
                lblMsg.Text = "Invalid date format.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (dFrom > dTo)
            {
                lblMsg.Text = "From Date cannot be greater than To Date.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        lblMsg.ForeColor = System.Drawing.Color.Black;
        LoadIncomeExpense(fromDate, toDate);
    }

    protected void btnExportPdf_Click(object sender, EventArgs e)
    {
        string fromDate = txtFromDate.Text.Trim();
        string toDate = txtToDate.Text.Trim();

        
        if (string.IsNullOrEmpty(fromDate) != string.IsNullOrEmpty(toDate))
        {
            lblMsg.Text = "Please select both From Date and To Date.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }

        if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
        {
            DateTime dFrom, dTo;
            bool validFrom = DateTime.TryParse(fromDate, out dFrom);
            bool validTo = DateTime.TryParse(toDate, out dTo);

            if (!validFrom || !validTo)
            {
                lblMsg.Text = "Invalid date format.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (dFrom > dTo)
            {
                lblMsg.Text = "From Date cannot be greater than To Date.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        LoadIncomeExpense(fromDate, toDate);

        GenerateIncomeExpensePdf(fromDate, toDate);
    }

    private void GenerateIncomeExpensePdf(string fromDate, string toDate)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            Document doc = new Document(PageSize.A4, 30, 30, 30, 30);
            PdfWriter.GetInstance(doc, ms);
            doc.Open();

            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Font sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);
            Font cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            Font totalFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);

            // ---------- HEADER WITH LOGO ----------
            string logoPath = Server.MapPath("~/Images/whitelogo.png");

            PdfPTable headerTable = new PdfPTable(2);
            headerTable.WidthPercentage = 100;
            headerTable.SetWidths(new float[] { 20f, 80f });

            // Logo cell
            PdfPCell logoCell;
            if (File.Exists(logoPath))
            {
                iTextSharp.text.Image logoImg = iTextSharp.text.Image.GetInstance(logoPath);
                logoImg.ScaleToFit(70f, 70f);
                logoCell = new PdfPCell(logoImg);
            }
            else
            {
                logoCell = new PdfPCell(new Phrase("")); // agar path na mile to blank
            }
            logoCell.Border = Rectangle.NO_BORDER;
            logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerTable.AddCell(logoCell);

            // Title cell (beside logo)
            Paragraph titlePara = new Paragraph("Income Statement", titleFont);
            titlePara.Alignment = Element.ALIGN_CENTER;

            PdfPCell titleCell = new PdfPCell();
            titleCell.AddElement(titlePara);
            titleCell.Border = Rectangle.NO_BORDER;
            titleCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerTable.AddCell(titleCell);

            doc.Add(headerTable);
            // ---------- END HEADER ----------

            string periodText = string.IsNullOrEmpty(fromDate)
                ? "Showing ALL records"
                : "Filtered from " + fromDate + " to " + toDate;

            Paragraph period = new Paragraph(periodText, cellFont);
            period.Alignment = Element.ALIGN_CENTER;
            period.SpacingBefore = 10f;
            period.SpacingAfter = 15f;
            doc.Add(period);

            Paragraph incomeHeading = new Paragraph("Income", sectionFont);
            incomeHeading.SpacingBefore = 0f;
            incomeHeading.SpacingAfter = 2f;
            doc.Add(incomeHeading);
            doc.Add(BuildAccountTable(dtIncomeData, headerFont, cellFont, new BaseColor(46, 125, 50)));

            Paragraph incomeTotal = new Paragraph(
                "Total Income: " + totalIncomeAmt.ToString("N2"), totalFont);
            incomeTotal.SpacingBefore = 5f;
            incomeTotal.SpacingAfter = 20f;
            doc.Add(incomeTotal);

            Paragraph expenseHeading = new Paragraph("Expense", sectionFont);
            expenseHeading.SpacingBefore = 10f;
            expenseHeading.SpacingAfter = 2f;
            doc.Add(expenseHeading);
            doc.Add(BuildAccountTable(dtExpenseData, headerFont, cellFont, new BaseColor(198, 40, 40)));

            Paragraph expenseTotal = new Paragraph(
                "Total Expense: " + totalExpenseAmt.ToString("N2"), totalFont);
            expenseTotal.SpacingBefore = 5f;
            expenseTotal.SpacingAfter = 20f;
            doc.Add(expenseTotal);

            decimal netProfit = totalIncomeAmt - totalExpenseAmt;
            Paragraph net = new Paragraph(
                "Net Profit / (Loss): " + netProfit.ToString("N2"), totalFont);
            net.SpacingBefore = 10f;
            doc.Add(net);

            doc.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition",
                "attachment; filename=IncomeStatement_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf");
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.End();
        }
    }

    private PdfPTable BuildAccountTable(DataTable dt, Font headerFont, Font cellFont, BaseColor headerColor)
    {
        PdfPTable table = new PdfPTable(3);
        table.WidthPercentage = 100;
        table.SetWidths(new float[] { 20f, 50f, 30f });
        table.SpacingAfter = 5f;

        // Header row
        string[] headers = { "Code", "Account Name", "Amount" };
        foreach (string h in headers)
        {
            PdfPCell cell = new PdfPCell(new Phrase(h, headerFont));
            cell.BackgroundColor = headerColor;
            cell.Padding = 5;
            table.AddCell(cell);
        }

        // Data rows
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                table.AddCell(new PdfPCell(new Phrase(row["AccountCode"].ToString(), cellFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(row["AccountDescription"].ToString(), cellFont)) { Padding = 5 });

                decimal amt = Convert.ToDecimal(row["Amount"]);
                PdfPCell amtCell = new PdfPCell(new Phrase(amt.ToString("N2"), cellFont));
                amtCell.Padding = 5;
                amtCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(amtCell);
            }
        }
        else
        {
            PdfPCell noData = new PdfPCell(new Phrase("No records found", cellFont));
            noData.Colspan = 3;
            noData.Padding = 5;
            noData.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(noData);
        }

        return table;
    }

}
