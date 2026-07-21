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

public partial class Pages_BalanceSheet : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["ConSqlWeb"];

    // PDF export ke liye latest data yahan store hoga.
    private DataTable dtIncomeData;
    private DataTable dtExpenseData;
    private decimal totalAssetsAmt;
    private decimal totalLiabilitiesAmt;

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

            // ================= ASSETS =================
            SqlCommand cmdIncome = new SqlCommand(@"

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
                ),0) + ISNULL(fs.OpeningBalance,0) AS Amount

            FROM FinanceStructure fs

            LEFT JOIN JournalEntry j 
                ON fs.AccountCode = j.AccountCode

            WHERE fs.ParentAccountCode = 1
            AND fs.UserId = @UserId

            AND (
                    @FromDate IS NULL 
                    OR j.TransactionDate BETWEEN @FromDate AND @ToDate
                )

            GROUP BY 
                fs.AccountCode,
                fs.AccountDescription,
                fs.OpeningBalance

            ORDER BY fs.AccountCode

            ", con);

            cmdIncome.Parameters.AddWithValue("@UserId", userId);

            // FIX: dono dates check karo, warna sirf toDate blank hone par
            // Convert.ToDateTime(toDate) crash kar deta tha.
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

            // ================= LIABILITIES =================
            SqlCommand cmdExpense = new SqlCommand(@"

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
                ),0) + ISNULL(fs.OpeningBalance,0) AS Amount

            FROM FinanceStructure fs

            LEFT JOIN JournalEntry j 
                ON fs.AccountCode = j.AccountCode

            WHERE fs.ParentAccountCode = 2
            AND fs.UserId = @UserId

            AND (
                    @FromDate IS NULL 
                    OR j.TransactionDate BETWEEN @FromDate AND @ToDate
                )

            GROUP BY 
                fs.AccountCode,
                fs.AccountDescription,
                fs.OpeningBalance

            ORDER BY fs.AccountCode

            ", con);

            cmdExpense.Parameters.AddWithValue("@UserId", userId);

            // FIX: same yahan bhi
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

            // ================= TOTALS =================
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

            // Export ke liye save kar lo
            dtIncomeData = dtIncome;
            dtExpenseData = dtExpense;
            totalAssetsAmt = totalIncome;
            totalLiabilitiesAmt = totalExpense;

            // ================= MESSAGE =================
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

        // FIX 1: Agar sirf ek date bhari hai, dusri khali hai -> error dikhao,
        // query mat chalao (crash yahin rukega).
        if (string.IsNullOrEmpty(fromDate) != string.IsNullOrEmpty(toDate))
        {
            lblMsg.Text = "Please select both From Date and To Date.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }

        // FIX 2: Agar dono dates di gayi hain, format aur From <= To validate karo.
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

        GenerateBalanceSheetPdf(fromDate, toDate);
    }

    private void GenerateBalanceSheetPdf(string fromDate, string toDate)
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

            PdfPCell logoCell;
            if (File.Exists(logoPath))
            {
                iTextSharp.text.Image logoImg = iTextSharp.text.Image.GetInstance(logoPath);
                logoImg.ScaleToFit(70f, 70f);
                logoCell = new PdfPCell(logoImg);
            }
            else
            {
                logoCell = new PdfPCell(new Phrase(""));
            }
            logoCell.Border = Rectangle.NO_BORDER;
            logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerTable.AddCell(logoCell);

            Paragraph titlePara = new Paragraph("Balance Sheet", titleFont);
            titlePara.Alignment = Element.ALIGN_CENTER;

            PdfPCell titleCell = new PdfPCell();
            titleCell.AddElement(titlePara);
            titleCell.Border = Rectangle.NO_BORDER;
            titleCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerTable.AddCell(titleCell);

            headerTable.SpacingAfter = 8f;   // FIX: title/logo row ke neeche gap
            doc.Add(headerTable);
            // ---------- END HEADER ----------

            string periodText = string.IsNullOrEmpty(fromDate)
                ? "Showing ALL records"
                : "Filtered from " + fromDate + " to " + toDate;

            Paragraph period = new Paragraph(periodText, cellFont);
            period.Alignment = Element.ALIGN_CENTER;
            period.SpacingBefore = 5f;    // FIX: title se gap
            period.SpacingAfter = 20f;    // FIX: "Assets Accounts" heading se gap
            doc.Add(period);

            // ---- ASSETS ----
            Paragraph assetsHeading = new Paragraph("Assets Accounts", sectionFont);
            assetsHeading.SpacingAfter = 4f;
            doc.Add(assetsHeading);
            doc.Add(BuildAccountTable(dtIncomeData, headerFont, cellFont, new BaseColor(46, 125, 50)));

            Paragraph assetsTotal = new Paragraph(
                "Total Assets: " + totalAssetsAmt.ToString("N2"), totalFont);
            assetsTotal.SpacingBefore = 5f;
            assetsTotal.SpacingAfter = 30f;   // FIX: pehle 20f tha, ab zyada gap Liability heading se pehle
            doc.Add(assetsTotal);

            // ---- LIABILITIES & EQUITY ----
            Paragraph liabHeading = new Paragraph("Liability & Equity", sectionFont);
            liabHeading.SpacingBefore = 10f;  // FIX: extra top gap
            liabHeading.SpacingAfter = 4f;
            doc.Add(liabHeading);
            doc.Add(BuildAccountTable(dtExpenseData, headerFont, cellFont, new BaseColor(198, 40, 40)));

            Paragraph liabTotal = new Paragraph(
                "Total Liability & Equity: " + totalLiabilitiesAmt.ToString("N2"), totalFont);
            liabTotal.SpacingBefore = 5f;
            liabTotal.SpacingAfter = 20f;
            doc.Add(liabTotal);

            // ---- BALANCE CHECK ----
            decimal diff = totalAssetsAmt - totalLiabilitiesAmt;
            string balanceText = diff == 0
                ? "Balance Check: Balanced"
                : "Balance Check: NOT Balanced (Difference: " + diff.ToString("N2") + ")";

            Paragraph balance = new Paragraph(balanceText, totalFont);
            balance.SpacingBefore = 10f;
            doc.Add(balance);

            doc.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition",
                "attachment; filename=BalanceSheet_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf");
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

        string[] headers = { "Code", "Account Name", "Amount" };
        foreach (string h in headers)
        {
            PdfPCell cell = new PdfPCell(new Phrase(h, headerFont));
            cell.BackgroundColor = headerColor;
            cell.Padding = 5;
            table.AddCell(cell);
        }

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
