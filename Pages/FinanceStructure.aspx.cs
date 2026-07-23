using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Pages_Finance_Structure : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConSqlWeb"]);
    private DataTable CachedTable
    {
        get { return ViewState["CachedTable"] as DataTable; }
        set { ViewState["CachedTable"] = value; }
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
            LoadGrid();
        }
    }

    void LoadGrid()
    {
        SqlDataAdapter da = new SqlDataAdapter(
            "SELECT * FROM FinanceStructure WHERE UserId=@UserId ORDER BY ParentAccountCode ASC", con);

        da.SelectCommand.Parameters.AddWithValue("@UserId",
            Session["UserId"].ToString());

        DataTable dt = new DataTable();
        da.Fill(dt);

        CachedTable = dt;
        BindGrid(dt);
    }

    void BindGrid(DataTable dt)
    {
        GridView1.DataSource = dt;
        GridView1.DataBind();

        int total = dt.Rows.Count;
        lblRecordCount.Text = total + (total == 1 ? " record" : " records");

        UpdatePageInfo();
    }

    void UpdatePageInfo()
    {
        int current = GridView1.PageIndex + 1;
        int total = GridView1.PageCount;
        lblPageInfo.Text = total > 1
            ? "Page " + current + " of " + total
            : "";
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        DataTable dt = CachedTable;
        if (dt == null)
        {
            LoadGrid();
            return;
        }
        BindGrid(dt);
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        DropDownList1.SelectedIndex = 0;

        LoadGrid();
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        string parentCode = TextBox2.Text.Trim();
        string accountCode = TextBox3.Text.Trim();

       
        if (parentCode != "1" &&
            parentCode != "2" &&
            parentCode != "3" &&
            parentCode != "4")
        {
            Response.Write(
            "<script>alert('Invalid Parent Account Code');</script>");
            return;
        }

        
        if (!accountCode.StartsWith(parentCode))
        {
            Response.Write(
            "<script>alert('Account Code does not belong to selected Parent Code Series');</script>");
            return;
        }

        con.Open();

        SqlCommand checkCmd = new SqlCommand(
            "SELECT COUNT(*) FROM FinanceStructure WHERE AccountCode=@a AND UserId=@UserId", con);

        checkCmd.Parameters.AddWithValue("@a", accountCode);

        checkCmd.Parameters.AddWithValue("@UserId",
            Session["UserId"].ToString());

        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

        if (count > 0)
        {
            con.Close();

            Response.Write(
            "<script>alert('Account Code already exists');</script>");
            return;
        }

        SqlCommand cmd = new SqlCommand(
        @"INSERT INTO FinanceStructure
    (UserId, ParentAccountCode, AccountCode,
     AccountDescription, OpeningBalance, BalanceStatus)
    VALUES
    (@UserId,@p,@a,@d,@o,@s)", con);

        cmd.Parameters.AddWithValue("@UserId",
            Session["UserId"].ToString());

        cmd.Parameters.AddWithValue("@p", parentCode);

        cmd.Parameters.AddWithValue("@a", accountCode);

        cmd.Parameters.AddWithValue("@d",
            TextBox4.Text);

        cmd.Parameters.AddWithValue("@o",
            Convert.ToDecimal(TextBox5.Text));

        cmd.Parameters.AddWithValue("@s",
            DropDownList1.SelectedValue);

        cmd.ExecuteNonQuery();

        con.Close();

        Response.Write(
        "<script>alert('Saved Successfully');</script>");

        LoadGrid();
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        string parentCode = TextBox2.Text.Trim();
        string accountCode = TextBox3.Text.Trim();

        if (TextBox3.Text == "")
        {
            Response.Write("Select record first!");
            return;
        }

        if (!accountCode.StartsWith(parentCode))
        {
            Response.Write(
            "<script>alert('Wrong Account Code for Selected Parent');</script>");
            return;
        }

        if (parentCode != "1" &&
            parentCode != "2" &&
            parentCode != "3" &&
            parentCode != "4")
        {
            Response.Write(
            "<script>alert('Invalid Parent Code');</script>");
            return;
        }

        con.Open();

        SqlCommand cmd = new SqlCommand(
            "UPDATE FinanceStructure SET ParentAccountCode=@p, AccountDescription=@d, " +
            "OpeningBalance=@o, BalanceStatus=@s WHERE AccountCode=@a", con);

        cmd.Parameters.AddWithValue("@p", parentCode);

        cmd.Parameters.AddWithValue("@a", accountCode);

        cmd.Parameters.AddWithValue("@d", TextBox4.Text.Trim());

        cmd.Parameters.AddWithValue("@o",
            string.IsNullOrEmpty(TextBox5.Text)
            ? 0
            : Convert.ToDecimal(TextBox5.Text));

        cmd.Parameters.AddWithValue("@s",
            DropDownList1.SelectedValue);

        int rows = cmd.ExecuteNonQuery();

        con.Close();

        if (rows > 0)
            Response.Write("<script>alert('Update Successfully');</script>");
        else
            Response.Write("<script>alert('Update Failed');</script>");

        LoadGrid();
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        if (TextBox3.Text == "")
        {
            Response.Write("Select record first!");
            return;
        }

        con.Open();

        SqlCommand cmd = new SqlCommand(
            "DELETE FROM FinanceStructure WHERE AccountCode=@a AND UserId=@UserId", con);
        cmd.Parameters.AddWithValue("@a", TextBox3.Text.Trim());
        cmd.Parameters.AddWithValue("@UserId",
     Session["UserId"].ToString());
        int rows = cmd.ExecuteNonQuery();
        con.Close();

        if (rows > 0)
            Response.Write("Deleted Successfully");
        else
            Response.Write("Delete Failed");

        LoadGrid();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GridView1.SelectedRow;

        TextBox2.Text = row.Cells[0].Text;
        TextBox3.Text = row.Cells[1].Text;
        TextBox4.Text = row.Cells[2].Text;
        TextBox5.Text = row.Cells[3].Text;
        DropDownList1.SelectedValue = row.Cells[4].Text;
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter da = new SqlDataAdapter(
           "SELECT * FROM FinanceStructure WHERE AccountCode LIKE @search AND UserId=@UserId", con);
        da.SelectCommand.Parameters.AddWithValue("@search", "%" + TextBox1.Text + "%");
        da.SelectCommand.Parameters.AddWithValue("@UserId",
    Session["UserId"].ToString());
        DataTable dt = new DataTable();
        da.Fill(dt);

        GridView1.PageIndex = 0;
        CachedTable = dt;
        BindGrid(dt);
    }
}