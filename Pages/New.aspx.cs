using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Pages_New : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.AppSettings["ConSqlWeb"]);
    string CurrentVoucherNo
    {
        get { return ViewState["CurrentVoucherNo"] == null ? "" : ViewState["CurrentVoucherNo"].ToString(); }
        set { ViewState["CurrentVoucherNo"] = value; }
    }

    // ===== NEW: holds which voucher the main grid should be filtered to (set when "Open" is clicked) =====
    string FilterVoucherNo
    {
        get { return ViewState["FilterVoucherNo"] == null ? "" : ViewState["FilterVoucherNo"].ToString(); }
        set { ViewState["FilterVoucherNo"] = value; }
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

            txtVoucher.Text = GenerateVoucherNo();

            RecalculateAllVoucherStatuses();

            LoadFromDB();
            LoadDrafts();
        }
    }

    // ===== NEW: self-healing — recalculates Status for ALL vouchers of this user
    // so old / out-of-sync data never stays stuck showing wrong Draft/Posted status =====
    void RecalculateAllVoucherStatuses()
    {
        try
        {
            int UserId = Convert.ToInt32(Session["UserId"]);

            if (con.State != ConnectionState.Open)
                con.Open();

            SqlCommand cmd = new SqlCommand(@"
        UPDATE j
        SET j.Status = CASE
                WHEN (t.TotalDebit > 0 OR t.TotalCredit > 0) AND t.TotalDebit = t.TotalCredit THEN 'Posted'
                ELSE 'Draft'
            END
        FROM JournalEntry j
        INNER JOIN (
            SELECT
                VoucherNo,
                UserId,
                SUM(CASE WHEN DC='Debit' THEN Amount ELSE 0 END) AS TotalDebit,
                SUM(CASE WHEN DC='Credit' THEN Amount ELSE 0 END) AS TotalCredit
            FROM JournalEntry
            WHERE UserId=@UserId
            GROUP BY VoucherNo, UserId
        ) t ON j.VoucherNo = t.VoucherNo AND j.UserId = t.UserId
        WHERE j.UserId=@UserId", con);

            cmd.Parameters.AddWithValue("@UserId", UserId);

            cmd.ExecuteNonQuery();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    string GenerateVoucherNo()
    {
        string vno = "1";

        try
        {
            int UserId = Convert.ToInt32(Session["UserId"]);

            SqlCommand cmd = new SqlCommand(
                "SELECT ISNULL(MAX(CAST(VoucherNo AS INT)),0)+1 FROM JournalEntry WHERE UserId=@UserId",
                con);

            cmd.Parameters.AddWithValue("@UserId", UserId);

            con.Open();

            vno = cmd.ExecuteScalar().ToString();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        return vno;
    }

    void LoadFromDB()
    {
        try
        {
            int UserId = Convert.ToInt32(Session["UserId"]);

            string query = @"
SELECT
    j.Id,
    j.VoucherNo,
    j.VoucherType,
    j.TransactionDate,
    j.AccountCode,
    ISNULL(f.AccountDescription,'') AS AccountDescription,
    j.DC,
    j.Amount,
    j.Narration,
    j.Status
FROM JournalEntry j
LEFT JOIN FinanceStructure f
    ON j.AccountCode = f.AccountCode
    AND f.UserId = @UserId
WHERE j.UserId = @UserId";

            if (ddlFilterDC != null && ddlFilterDC.SelectedValue != "All")
            {
                query += " AND j.DC = @dc";
            }

            // ===== NEW: filter to a single voucher when one was opened from the Drafts panel =====
            if (!string.IsNullOrEmpty(FilterVoucherNo))
            {
                query += " AND j.VoucherNo = @fvno";
            }

            query += " ORDER BY TRY_CAST(j.VoucherNo AS INT), j.Id";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@UserId", UserId);

            if (ddlFilterDC != null && ddlFilterDC.SelectedValue != "All")
            {
                cmd.Parameters.AddWithValue("@dc", ddlFilterDC.SelectedValue);
            }

            if (!string.IsNullOrEmpty(FilterVoucherNo))
            {
                cmd.Parameters.AddWithValue("@fvno", FilterVoucherNo);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                gv.DataSource = null;
                gv.DataBind();
                return;
            }

            ViewState["dt"] = dt;

            gv.DataSource = dt;
            gv.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    // ===== NEW: Loads the list of Draft vouchers (Debit != Credit) into the side panel =====
    void LoadDrafts()
    {
        try
        {
            int UserId = Convert.ToInt32(Session["UserId"]);

            string query = @"
SELECT
    VoucherNo,
    MAX(VoucherType) AS VoucherType,
    MAX(TransactionDate) AS TransactionDate,
    SUM(CASE WHEN DC='Debit' THEN Amount ELSE 0 END) AS TotalDebit,
    SUM(CASE WHEN DC='Credit' THEN Amount ELSE 0 END) AS TotalCredit
FROM JournalEntry
WHERE UserId=@UserId
AND Status='Draft'
GROUP BY VoucherNo
ORDER BY TRY_CAST(VoucherNo AS INT)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", UserId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dt.Columns.Add("Difference", typeof(decimal));

            foreach (DataRow row in dt.Rows)
            {
                decimal d = Convert.ToDecimal(row["TotalDebit"]);
                decimal c = Convert.ToDecimal(row["TotalCredit"]);
                row["Difference"] = Math.Abs(d - c);
            }

            pnlDrafts.Visible = dt.Rows.Count > 0;
            lblDraftCount.Text = dt.Rows.Count.ToString();

            gvDrafts.DataSource = dt;
            gvDrafts.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    protected void ddlFilterDC_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadFromDB();
    }


    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtDate.Text))
        {
            Response.Write("<script>alert('Please select date first');</script>");
            return;
        }

        string vno = CurrentVoucherNo;

        if (string.IsNullOrEmpty(vno))
        {
            vno = txtVoucher.Text.Trim();
        }

        if (string.IsNullOrEmpty(vno))
        {
            vno = GenerateVoucherNo();
        }


        try
        {
            int UserId = Convert.ToInt32(Session["UserId"]);

            con.Open();

            SqlCommand cmd = new SqlCommand(@"
            INSERT INTO JournalEntry
            (
                VoucherNo,
                VoucherType,
                TransactionDate,
                AccountCode,
                DC,
                Amount,
                Narration,
                UserId,
                Status
            )
            VALUES
            (
                @v,
                @t,
                @d,
                '',
                'Debit',
                0,
                '',
                @UserId,
                'Draft'
            )", con);

            cmd.Parameters.AddWithValue("@v", vno);
            cmd.Parameters.AddWithValue("@t", ddlType.SelectedValue);
            cmd.Parameters.AddWithValue("@d", Convert.ToDateTime(txtDate.Text));
            cmd.Parameters.AddWithValue("@UserId", UserId);

            cmd.ExecuteNonQuery();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        ValidateVoucherBalance(vno);

        LoadFromDB();
        LoadDrafts();
    }

    protected void btnNewVoucher_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtDate.Text))
        {
            Response.Write("<script>alert('Please select date first');</script>");
            return;
        }

        string newVoucherNo = GenerateVoucherNo();

        try
        {
            int UserId = Convert.ToInt32(Session["UserId"]);

            con.Open();

            SqlCommand cmd = new SqlCommand(@"
        INSERT INTO JournalEntry
        (
            VoucherNo,
            VoucherType,
            TransactionDate,
            AccountCode,
            DC,
            Amount,
            Narration,
            UserId,
            Status
        )
        VALUES
        (
            @VoucherNo,
            @VoucherType,
            @Date,
            '',
            'Debit',
            0,
            '',
            @UserId,
            'Draft'
        )", con);

            cmd.Parameters.AddWithValue("@VoucherNo", newVoucherNo);
            cmd.Parameters.AddWithValue("@VoucherType", ddlType.SelectedValue);
            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text));
            cmd.Parameters.AddWithValue("@UserId", UserId);

            cmd.ExecuteNonQuery();

            txtVoucher.Text = newVoucherNo;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        ValidateVoucherBalance(newVoucherNo);

        LoadFromDB();
        LoadDrafts();
    }

    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CurrentVoucherNo =
    gv.DataKeys[e.NewEditIndex].Values["VoucherNo"].ToString();

        lblCurrentVoucher.Text = CurrentVoucherNo;
        gv.EditIndex = e.NewEditIndex;

        LoadFromDB();

        GridViewRow row = gv.Rows[e.NewEditIndex];

        DropDownList ddlAcc =
            (DropDownList)row.FindControl("ddlAccount");

        DropDownList ddlDC =
            (DropDownList)row.FindControl("ddlDC");

        if (ddlAcc != null)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT AccountCode, AccountDescription FROM FinanceStructure WHERE UserId=@UserId ORDER BY AccountDescription",
                con);

            da.SelectCommand.Parameters.AddWithValue("@UserId", UserId);

            DataTable dtAcc = new DataTable();
            da.Fill(dtAcc);

            ddlAcc.DataSource = dtAcc;
            ddlAcc.DataTextField = "AccountDescription";
            ddlAcc.DataValueField = "AccountCode";
            ddlAcc.DataBind();

            string accCode =
                gv.DataKeys[e.NewEditIndex].Values["AccountCode"].ToString();

            if (ddlAcc.Items.FindByValue(accCode) != null)
            {
                ddlAcc.SelectedValue = accCode;
            }
        }

        if (ddlDC != null)
        {
            string dc =
                gv.DataKeys[e.NewEditIndex].Values["DC"].ToString();

            if (ddlDC.Items.FindByValue(dc) != null)
            {
                ddlDC.SelectedValue = dc;
            }
        }
    }

    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        LoadFromDB();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        LoadFromDB();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gv.Rows[e.RowIndex];

        DropDownList ddlAcc =
            (DropDownList)row.FindControl("ddlAccount");

        DropDownList ddlDC =
            (DropDownList)row.FindControl("ddlDC");

        TextBox txtAmt =
            (TextBox)row.FindControl("txtAmount");

        TextBox txtNar =
            (TextBox)row.FindControl("txtNarration");

        int rowId =
            Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["Id"]);

        string voucherNo =
            gv.DataKeys[e.RowIndex].Values["VoucherNo"].ToString();

        int UserId =
            Convert.ToInt32(Session["UserId"]);

        // ===== NEW: basic field validation — Account, DC, and a valid Amount are mandatory =====
        if (ddlAcc == null || string.IsNullOrEmpty(ddlAcc.SelectedValue))
        {
            Response.Write("<script>alert('Please select an Account before saving.');</script>");
            return;
        }

        if (ddlDC == null || string.IsNullOrEmpty(ddlDC.SelectedValue))
        {
            Response.Write("<script>alert('Please select Debit or Credit before saving.');</script>");
            return;
        }

        decimal parsedAmount;
        if (txtAmt == null || !decimal.TryParse(txtAmt.Text, out parsedAmount) || parsedAmount <= 0)
        {
            Response.Write("<script>alert('Please enter a valid Amount greater than 0.');</script>");
            return;
        }

        try
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            // ===== ONLY ONE DEBIT ALLOWED =====
            if (ddlDC.SelectedValue == "Debit")
            {
                SqlCommand chkDebit = new SqlCommand(@"
            SELECT COUNT(*)
            FROM JournalEntry
            WHERE VoucherNo=@VoucherNo
            AND DC='Debit'
            AND Id<>@Id
            AND UserId=@UserId", con);

                chkDebit.Parameters.AddWithValue("@VoucherNo", voucherNo);
                chkDebit.Parameters.AddWithValue("@Id", rowId);
                chkDebit.Parameters.AddWithValue("@UserId", UserId);

                int debitCount =
                    Convert.ToInt32(chkDebit.ExecuteScalar());

                if (debitCount > 0)
                {
                    Response.Write("<script>alert('Only one Debit entry is allowed in this voucher');</script>");
                    return;
                }
            }

            // ===== NEW: CREDIT CANNOT BE THE FIRST ENTRY — a Debit must exist in this voucher first =====
            if (ddlDC.SelectedValue == "Credit")
            {
                SqlCommand chkAnyDebit = new SqlCommand(@"
            SELECT COUNT(*)
            FROM JournalEntry
            WHERE VoucherNo=@VoucherNo
            AND DC='Debit'
            AND Id<>@Id
            AND UserId=@UserId", con);

                chkAnyDebit.Parameters.AddWithValue("@VoucherNo", voucherNo);
                chkAnyDebit.Parameters.AddWithValue("@Id", rowId);
                chkAnyDebit.Parameters.AddWithValue("@UserId", UserId);

                int existingDebitCount =
                    Convert.ToInt32(chkAnyDebit.ExecuteScalar());

                if (existingDebitCount == 0)
                {
                    Response.Write("<script>alert('Pehle Debit entry karein — Credit pehli entry nahi ho sakti is voucher mein.');</script>");
                    return;
                }
            }

            SqlCommand cmd = new SqlCommand(@"
        UPDATE JournalEntry
        SET
            AccountCode=@a,
            DC=@dc,
            Amount=@amt,
            Narration=@n
        WHERE
            Id=@id
            AND UserId=@UserId", con);

            cmd.Parameters.AddWithValue("@id", rowId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@a", ddlAcc.SelectedValue);
            cmd.Parameters.AddWithValue("@dc", ddlDC.SelectedValue);
            cmd.Parameters.AddWithValue("@amt", parsedAmount);
            cmd.Parameters.AddWithValue("@n", txtNar.Text);

            cmd.ExecuteNonQuery();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        gv.EditIndex = -1;

        ValidateVoucherBalance(voucherNo);

        LoadFromDB();
        LoadDrafts();
    }
    private void ValidateVoucherBalance(string voucherNo)
    {
        decimal debit = 0;
        decimal credit = 0;
        int rowCount = 0;

        int UserId = Convert.ToInt32(Session["UserId"]);

        try
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            SqlCommand cmd = new SqlCommand(@"
        SELECT DC, Amount
        FROM JournalEntry
        WHERE VoucherNo=@VoucherNo
        AND UserId=@UserId", con);

            cmd.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmd.Parameters.AddWithValue("@UserId", UserId);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                rowCount++;

                decimal amt = Convert.ToDecimal(dr["Amount"]);

                if (dr["DC"].ToString() == "Debit")
                    debit += amt;
                else
                    credit += amt;
            }

            dr.Close();

            if (rowCount == 0)
            {
                lblStatus.Text = "Voucher No. " + voucherNo + " deleted — no entries remaining.";
                lblStatus.ForeColor = System.Drawing.Color.Gray;
                return;
            }

            
            if (debit == 0 && credit == 0)
            {
                SqlCommand cmdStatusEmpty = new SqlCommand(@"
            UPDATE JournalEntry
            SET Status='Draft'
            WHERE VoucherNo=@VoucherNo
            AND UserId=@UserId", con);

                cmdStatusEmpty.Parameters.AddWithValue("@VoucherNo", voucherNo);
                cmdStatusEmpty.Parameters.AddWithValue("@UserId", UserId);
                cmdStatusEmpty.ExecuteNonQuery();

                lblStatus.Text = "⚠ Voucher No. " + voucherNo + " created — please enter Debit/Credit amounts.";
                lblStatus.ForeColor = System.Drawing.Color.OrangeRed;
                return;
            }

           
            string newStatus = (debit == credit) ? "Posted" : "Draft";

            SqlCommand cmdStatus = new SqlCommand(@"
        UPDATE JournalEntry
        SET Status=@Status
        WHERE VoucherNo=@VoucherNo
        AND UserId=@UserId", con);

            cmdStatus.Parameters.AddWithValue("@Status", newStatus);
            cmdStatus.Parameters.AddWithValue("@VoucherNo", voucherNo);
            cmdStatus.Parameters.AddWithValue("@UserId", UserId);

            cmdStatus.ExecuteNonQuery();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        if (debit == credit)
        {
            lblStatus.Text = "✓ Balanced — Voucher No. " + voucherNo + " Posted";
            lblStatus.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblStatus.Text =
                "⚠ Voucher No. " + voucherNo + " saved in Draft (not posted) because Debit ≠ Credit. " +
                "Debit: " + debit +
                " | Credit: " + credit +
                " | Difference: " + Math.Abs(debit - credit);

            lblStatus.ForeColor = System.Drawing.Color.OrangeRed;
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int rowId =
            Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["Id"]);

        string voucherNo =
            gv.DataKeys[e.RowIndex].Values["VoucherNo"].ToString();

        try
        {
            int UserId = Convert.ToInt32(Session["UserId"]);

            con.Open();

            SqlCommand cmd = new SqlCommand(
                "DELETE FROM JournalEntry WHERE Id=@id AND UserId=@UserId",
                con);

            cmd.Parameters.AddWithValue("@id", rowId);
            cmd.Parameters.AddWithValue("@UserId", UserId);

            cmd.ExecuteNonQuery();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        ValidateVoucherBalance(voucherNo);

        LoadFromDB();
        LoadDrafts();
    }

    protected void gvDrafts_SelectedIndexChanged(object sender, EventArgs e)
    {
        string voucherNo = gvDrafts.SelectedDataKey.Value.ToString();

        CurrentVoucherNo = voucherNo;
        FilterVoucherNo = voucherNo;
        lblCurrentVoucher.Text = voucherNo;
        txtVoucher.Text = voucherNo;

        LoadFromDB();
        LoadDrafts();
    }

    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        FilterVoucherNo = "";
        LoadFromDB();
        LoadDrafts();
    }
}
