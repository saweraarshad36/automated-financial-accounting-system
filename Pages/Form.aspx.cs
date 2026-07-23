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


public partial class Pages_Form : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMessage.Visible = false;

    }
    protected void BtnDone_Click(object sender, EventArgs e)
    {
        string username = txtUserName.Text.Trim();
        string password = txtPassword.Text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            lblErrorMessage.Text = "Please enter Username and Password.";
            lblErrorMessage.Visible = true;
            return;
        }

        string connStr = ConfigurationManager.AppSettings["ConSqlWeb"];

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            // Ab hum sirf Username se row nikaal rahe hain, Password DB mein
            // match nahi kar rahe (kyunke DB mein ab hash stored hai, plain text nahi)
            string query = "SELECT Id, Username, Password FROM Users WHERE Username=@Username";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string storedHash = dr["Password"].ToString();
                int userId = Convert.ToInt32(dr["Id"]);
                string dbUsername = dr["Username"].ToString();
                dr.Close();

                if (PasswordHelper.VerifyPassword(password, storedHash))
                {
                    // Agar ye purana plain-text password tha, to ab isay hash karke
                    // DB mein update kar dete hain (auto-migration on login).
                    if (!storedHash.Contains(":"))
                    {
                        string newHash = PasswordHelper.HashPassword(password);

                        using (SqlConnection migrateConn = new SqlConnection(connStr))
                        {
                            migrateConn.Open();
                            string migrateQuery = "UPDATE Users SET Password=@Password WHERE Id=@Id";
                            SqlCommand migrateCmd = new SqlCommand(migrateQuery, migrateConn);
                            migrateCmd.Parameters.AddWithValue("@Password", newHash);
                            migrateCmd.Parameters.AddWithValue("@Id", userId);
                            migrateCmd.ExecuteNonQuery();
                        }
                    }

                    Session["UserId"] = userId;
                    Session["Username"] = dbUsername;
                    Session["LoginSuccess"] = "User Verified Successfully!";

                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    lblErrorMessage.Text = "Invalid Username or Password!";
                    lblErrorMessage.Visible = true;
                }
            }
            else
            {
                dr.Close();

                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                lblErrorMessage.Text = "Invalid Username or Password!";
                lblErrorMessage.Visible = true;
            }
        }
    }

    protected void BtnDone1_Click(object sender, EventArgs e)
    {
        txtUserName.Visible = true;
        txtUserName.Visible = true;

        Label5.Visible = true;
        Label6.Visible = true;

        txtPassword1.Visible = true;
        txtPassword2.Visible = true;

        BtnDone0.Visible = true;
    }

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

            // Naya password save karne se pehle hash kar rahe hain
            string hashedPassword = PasswordHelper.HashPassword(newPassword);

            string updateQuery = "UPDATE Users SET Password=@Password WHERE Username=@Username";
            SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
            updateCmd.Parameters.AddWithValue("@Password", hashedPassword);
            updateCmd.Parameters.AddWithValue("@Username", username);
            updateCmd.ExecuteNonQuery();

            lblErrorMessage.ForeColor = System.Drawing.Color.Green;
            lblErrorMessage.Text = "Password changed successfully!";
            lblErrorMessage.Visible = true;

            Label5.Visible = false;
            Label6.Visible = false;
            txtPassword1.Visible = false;
            txtPassword2.Visible = false;
            BtnDone0.Visible = false;
        }
    }
}