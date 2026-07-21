using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
public partial class Pages_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMessage.Visible = false;
    }
   

    // 🔹 LOGIN BUTTON
    protected void BtnDone_Click(object sender, EventArgs e)
    {
        string username = txtUserName.Text.Trim();
        string password = txtPassword.Text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            lblErrorMessage.Text = "Please enter Username and Password.";
            lblErrorMessage.Visible = true;
            return;
        }

        string connStr = ConfigurationManager.AppSettings["ConSqlWeb"];

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string query = "SELECT COUNT(*) FROM Users WHERE Username=@Username AND Password=@Password";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            int count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                // Login successful → redirect dashboard
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                lblErrorMessage.Text = "Invalid Username or Password.";
                lblErrorMessage.Visible = true;
            }
        }
    }

    // 🔹 SHOW CHANGE PASSWORD CONTROLS
    protected void BtnDone1_Click(object sender, EventArgs e)
    {
        Label5.Visible = true;
        Label6.Visible = true;
        txtPassword1.Visible = true;
        txtPassword2.Visible = true;
        BtnDone0.Visible = true;
    }

    // 🔹 CHANGE PASSWORD BUTTON
    protected void BtnDone0_Click(object sender, EventArgs e)
    {
        string username = txtUserName.Text.Trim();
        string newPassword = txtPassword1.Text.Trim();
        string confirmPassword = txtPassword2.Text.Trim();

        if (string.IsNullOrEmpty(username))
        {
            lblErrorMessage.Text = "Enter Username first.";
            lblErrorMessage.Visible = true;
            return;
        }

        if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
        {
            lblErrorMessage.Text = "Enter new password and confirm password.";
            lblErrorMessage.Visible = true;
            return;
        }

        if (newPassword != confirmPassword)
        {
            lblErrorMessage.Text = "Passwords do not match.";
            lblErrorMessage.Visible = true;
            return;
        }

        string connStr = ConfigurationManager.AppSettings["ConSqlWeb"];

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            // Check if username exists
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username=@Username";
            SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@Username", username);

            int count = (int)checkCmd.ExecuteScalar();

            if (count == 0)
            {
                lblErrorMessage.Text = "Username not found!";
                lblErrorMessage.Visible = true;
                return;
            }

            // Update password
            string updateQuery = "UPDATE Users SET Password=@Password WHERE Username=@Username";
            SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
            updateCmd.Parameters.AddWithValue("@Password", newPassword);
            updateCmd.Parameters.AddWithValue("@Username", username);

            updateCmd.ExecuteNonQuery();

            lblErrorMessage.ForeColor = System.Drawing.Color.Green;
            lblErrorMessage.Text = "Password changed successfully!";
            lblErrorMessage.Visible = true;

            // Hide change password controls
            Label5.Visible = false;
            Label6.Visible = false;
            txtPassword1.Visible = false;
            txtPassword2.Visible = false;
            BtnDone0.Visible = false;
        }
    }
}