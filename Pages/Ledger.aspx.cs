using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Ledger : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.AppSettings["ConSqlWeb"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        // Session Check
        if (Session["UserId"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            lblUsername.Text = Session["Username"].ToString();

            LoadAccounts();
        }
    }

    void LoadAccounts()
    {
        try
        {
            int UserId = Convert.ToInt32(Session["UserId"]);

            SqlDataAdapter da = new SqlDataAdapter(@"
                SELECT AccountCode,
                       AccountDescription
                FROM FinanceStructure
                WHERE UserId = @UserId
                ORDER BY AccountDescription", con);

            da.SelectCommand.Parameters.AddWithValue("@UserId", UserId);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlAccountName.DataSource = dt;
            ddlAccountName.DataTextField = "AccountDescription";
            ddlAccountName.DataValueField = "AccountCode";
            ddlAccountName.DataBind();

            ddlAccountName.Items.Insert(0, "-- Select Account --");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            // Account select check
            if (ddlAccountName.SelectedIndex == 0)
            {
                Response.Write("<script>alert('Please select an account');</script>");
                return;
            }

            int UserId = Convert.ToInt32(Session["UserId"]);

            string query = @"
            SELECT VoucherNo,
                   VoucherType,
                   DC,
                   Amount,
                   Narration,
                   TransactionDate
            FROM JournalEntry
            WHERE AccountCode = @acc
              AND UserId = @UserId";

            // Optional date filter
            if (!string.IsNullOrEmpty(txtFromDate.Text)
                && !string.IsNullOrEmpty(txtToDate.Text))
            {
                query += " AND TransactionDate BETWEEN @from AND @to";
            }

            query += " ORDER BY TRY_CAST(VoucherNo AS INT), Id";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@acc", ddlAccountName.SelectedValue);
            cmd.Parameters.AddWithValue("@UserId", UserId);

            if (!string.IsNullOrEmpty(txtFromDate.Text)
                && !string.IsNullOrEmpty(txtToDate.Text))
            {
                cmd.Parameters.AddWithValue("@from",
                    Convert.ToDateTime(txtFromDate.Text));

                cmd.Parameters.AddWithValue("@to",
                    Convert.ToDateTime(txtToDate.Text));
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            // Running Balance Column
            dt.Columns.Add("Balance", typeof(decimal));

            decimal runningBalance = 0;

            foreach (DataRow row in dt.Rows)
            {
                decimal amount = Convert.ToDecimal(row["Amount"]);

                if (row["DC"].ToString() == "Debit")
                    runningBalance += amount;
                else
                    runningBalance -= amount;

                row["Balance"] = runningBalance;
            }

            gvLedger.DataSource = dt;
            gvLedger.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}