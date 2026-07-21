using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_JournalEntry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.AppSettings["ConSqlWeb"]);

    private DataTable CachedTable
    {
        get { return ViewState["JournalData"] as DataTable; }
        set { ViewState["JournalData"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            lblUsername.Text = Session["Username"].ToString();

            LoadAllJournalData();
        }
    }
    void LoadAllJournalData()
    {
        try
        {
            int UserId = Convert.ToInt32(Session["UserId"]);

            SqlDataAdapter da = new SqlDataAdapter(@"
        SELECT
               Id,
               VoucherNo,
               VoucherType,
               TransactionDate,
               DC,
               Amount,
               Narration,
               AccountCode
        FROM JournalEntry
        WHERE UserId = @UserId
        ORDER BY TRY_CAST(VoucherNo AS INT), Id", con);

            da.SelectCommand.Parameters.AddWithValue("@UserId", UserId);

            DataTable dt = new DataTable();
            da.Fill(dt);

            CachedTable = dt;

            BindGrid(dt);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    void BindGrid(DataTable dt)
    {
        gvJournal.DataSource = dt;
        gvJournal.DataBind();

        // Record count badge
        int total = dt.Rows.Count;
        lblRecordCount.Text = total + (total == 1 ? " record" : " records");

        // Page X of Y
        UpdatePageInfo();
    }

    void BindGrid(DataView dv)
    {
        gvJournal.DataSource = dv;
        gvJournal.DataBind();

        int total = dv.Count;
        lblRecordCount.Text = total + (total == 1 ? " record" : " records");

        UpdatePageInfo();
    }

    void UpdatePageInfo()
    {
        int current = gvJournal.PageIndex + 1;
        int total = gvJournal.PageCount;
        lblPageInfo.Text = total > 1
            ? "Page " + current + " of " + total
            : "";
    }

    // ── PAGINATION EVENT ───────────────────────────────────────────────────────
    protected void gvJournal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvJournal.PageIndex = e.NewPageIndex;

        DataTable dt = CachedTable;
        if (dt == null)
        {
            LoadAllJournalData();
            return;
        }

        // Check if a search filter is active
        string voucher = txtSearchVoucher.Text.Trim();
        if (!string.IsNullOrEmpty(voucher))
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "VoucherNo = '" + voucher + "'";
            BindGrid(dv);
        }
        else
        {
            BindGrid(dt);
        }
    }

    // ── SEARCH ─────────────────────────────────────────────────────────────────
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = CachedTable;
        if (dt == null) return;

        string voucherNo = txtSearchVoucher.Text.Trim();

        // Reset to page 1 on new search
        gvJournal.PageIndex = 0;

        if (voucherNo == "")
        {
            BindGrid(dt);
            return;
        }

        DataView dv = dt.DefaultView;
        dv.RowFilter = "VoucherNo = '" + voucherNo + "'";
        BindGrid(dv);
    }
}
