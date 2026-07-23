using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Pages_Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;

        string connStr = ConfigurationManager.AppSettings["ConSqlWeb"];
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            // 🔍 1. Username check
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username=@Username";
            SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            int count = (int)checkCmd.ExecuteScalar();
            if (count > 0)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Username already exists!";
                txtUsername.Focus();
                return;
            }

            // 🔐 Password ko save karne se pehle hash kar rahe hain
            string hashedPassword = PasswordHelper.HashPassword(txtPassword.Text);

            // 💾 2. Insert data
            string insertQuery = "INSERT INTO Users (FirstName, LastName, Username, Password) VALUES (@FirstName, @LastName, @Username, @Password)";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            cmd.Parameters.AddWithValue("@Password", hashedPassword);
            cmd.ExecuteNonQuery();
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Successfully Registered! Redirecting to Login Page in 1 seconds...";
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "redirect",
                "setTimeout(function(){ window.location='Form.aspx'; }, 1000);",
                true);
        }
    }
}