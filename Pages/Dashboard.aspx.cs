using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class Pages_Dashboard : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.AppSettings["ConSqlWeb"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            if (Session["LoginSuccess"] != null)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "msg",
                    "alert('" + Session["LoginSuccess"].ToString() + "');",
                    true);

                Session.Remove("LoginSuccess");
            }

            lblUsername.Text = Session["Username"].ToString();

            LoadDashboard();
            LoadChart();
        }
    }

    void LoadDashboard()
    {
        int userId = Convert.ToInt32(Session["UserId"]);

        con.Open();

        int currentYear = DateTime.Now.Year;
        int prevYear = currentYear - 1;

        SqlCommand cmdIncome = new SqlCommand(@"
            SELECT ISNULL(SUM(j.Amount),0)
            FROM JournalEntry j
            INNER JOIN FinanceStructure f 
                ON j.AccountCode = f.AccountCode
            WHERE f.ParentAccountCode = 3
              AND j.DC = 'Credit'
              AND j.Status = 'Posted'
              AND j.UserId = @UserId
              AND YEAR(j.TransactionDate) = @yr
        ", con);

        cmdIncome.Parameters.AddWithValue("@UserId", userId);
        cmdIncome.Parameters.AddWithValue("@yr", currentYear);

        decimal revenue = Convert.ToDecimal(cmdIncome.ExecuteScalar());

        SqlCommand cmdExpense = new SqlCommand(@"
            SELECT ISNULL(SUM(j.Amount),0)
            FROM JournalEntry j
            INNER JOIN FinanceStructure f 
                ON j.AccountCode = f.AccountCode
            WHERE f.ParentAccountCode = 4
              AND j.DC = 'Debit'
              AND j.Status = 'Posted'
              AND j.UserId = @UserId
              AND YEAR(j.TransactionDate) = @yr
        ", con);

        cmdExpense.Parameters.AddWithValue("@UserId", userId);
        cmdExpense.Parameters.AddWithValue("@yr", currentYear);

        decimal expense = Convert.ToDecimal(cmdExpense.ExecuteScalar());

        decimal profit = revenue - expense;

        //cash flow
        SqlCommand cmdCash = new SqlCommand(@"
            SELECT
                ISNULL(SUM(
                    CASE 
                        WHEN DC='Debit' THEN Amount 
                        ELSE 0 
                    END
                ),0)
                -
                ISNULL(SUM(
                    CASE 
                        WHEN DC='Credit' THEN Amount 
                        ELSE 0 
                    END
                ),0)
            FROM JournalEntry
            WHERE UserId = @UserId
              AND Status = 'Posted'
        ", con);

        cmdCash.Parameters.AddWithValue("@UserId", userId);

        decimal cash = Convert.ToDecimal(cmdCash.ExecuteScalar());

        //Prev year reve
        SqlCommand cmdPrevIncome = new SqlCommand(@"
            SELECT ISNULL(SUM(j.Amount),0)
            FROM JournalEntry j
            INNER JOIN FinanceStructure f 
                ON j.AccountCode = f.AccountCode
            WHERE f.ParentAccountCode = 3
              AND j.DC = 'Credit'
              AND j.Status = 'Posted'
              AND j.UserId = @UserId
              AND YEAR(j.TransactionDate) = @yr
        ", con);

        cmdPrevIncome.Parameters.AddWithValue("@UserId", userId);
        cmdPrevIncome.Parameters.AddWithValue("@yr", prevYear);

        decimal prevRevenue = Convert.ToDecimal(cmdPrevIncome.ExecuteScalar());

        //  PREVIOUS YEAR EXPENSE
        SqlCommand cmdPrevExpense = new SqlCommand(@"
            SELECT ISNULL(SUM(j.Amount),0)
            FROM JournalEntry j
            INNER JOIN FinanceStructure f 
                ON j.AccountCode = f.AccountCode
            WHERE f.ParentAccountCode = 4
              AND j.DC = 'Debit'
              AND j.Status = 'Posted'
              AND j.UserId = @UserId
              AND YEAR(j.TransactionDate) = @yr
        ", con);

        cmdPrevExpense.Parameters.AddWithValue("@UserId", userId);
        cmdPrevExpense.Parameters.AddWithValue("@yr", prevYear);

        decimal prevExpense = Convert.ToDecimal(cmdPrevExpense.ExecuteScalar());

        con.Close();

        // LABELS
        lblRevenue.Text = revenue.ToString("N0");
        lblExpense.Text = expense.ToString("N0");
        lblProfit.Text = profit.ToString("N0");
        lblCash.Text = cash.ToString("N0");
        lblPrevRevenue.Text = prevRevenue.ToString("N0");
        lblPrevExpense.Text = prevExpense.ToString("N0");

        // PIE CHART
        string pieScript = string.Format(
            "loadPieChart({0},{1},{2},{3});",
            revenue,
            expense,
            prevRevenue,
            prevExpense
        );

        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "pieChart",
            pieScript,
            true
        );
    }

    void LoadChart()
    {
        int userId = Convert.ToInt32(Session["UserId"]);

        con.Open();

        SqlCommand cmd = new SqlCommand(@"
            SELECT
                DATENAME(MONTH, j.TransactionDate) AS Month,

                SUM(
                    CASE 
                        WHEN f.ParentAccountCode = 3 
                             AND j.DC='Credit'
                        THEN j.Amount 
                        ELSE 0 
                    END
                ) AS Revenue,

                SUM(
                    CASE 
                        WHEN f.ParentAccountCode = 4 
                             AND j.DC='Debit'
                        THEN j.Amount 
                        ELSE 0 
                    END
                ) AS Expense

            FROM JournalEntry j
            INNER JOIN FinanceStructure f 
                ON j.AccountCode = f.AccountCode

            WHERE YEAR(j.TransactionDate) = YEAR(GETDATE())
              AND j.TransactionDate <= GETDATE()
              AND j.Status = 'Posted'
              AND j.UserId = @UserId

            GROUP BY 
                MONTH(j.TransactionDate),
                DATENAME(MONTH, j.TransactionDate)

            ORDER BY MONTH(j.TransactionDate)
        ", con);

        cmd.Parameters.AddWithValue("@UserId", userId);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        da.Fill(dt);

        con.Close();

        System.Text.StringBuilder sbLabels =
            new System.Text.StringBuilder("[");

        System.Text.StringBuilder sbRevenue =
            new System.Text.StringBuilder("[");

        System.Text.StringBuilder sbExpense =
            new System.Text.StringBuilder("[");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i > 0)
            {
                sbLabels.Append(",");
                sbRevenue.Append(",");
                sbExpense.Append(",");
            }

            sbLabels.Append("\"" + dt.Rows[i]["Month"].ToString() + "\"");

            sbRevenue.Append(dt.Rows[i]["Revenue"].ToString());

            sbExpense.Append(dt.Rows[i]["Expense"].ToString());
        }

        sbLabels.Append("]");
        sbRevenue.Append("]");
        sbExpense.Append("]");

        string script =
            "loadChart(" +
            sbLabels + "," +
            sbRevenue + "," +
            sbExpense + ");";

        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "chart",
            script,
            true
        );
    }
}