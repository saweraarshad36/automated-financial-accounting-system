using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Controls_ContactUc : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void SendContactEmail(object sender, EventArgs e)
    {
        string name = inp_name.Value.Trim();
        string email = inp_email.Value.Trim();
        string phone = inp_phone.Value.Trim();
        string message = inp_message.Value.Trim();

        // Simple validation
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
        {
            lblStatus.InnerText = "Please fill all required fields.";
            lblStatus.Style["color"] = "red";
            return;
        }

        if (string.IsNullOrEmpty(phone))
        {
            lblStatus.InnerText = "Please enter your phone number.";
            lblStatus.Style["color"] = "red";
            return;
        }

        string selectedValue = ddl_country.Value;
        string[] parts = selectedValue.Split('|');

        string countryName = parts[0];
        int minDigits = int.Parse(parts[1]);
        int maxDigits = int.Parse(parts[2]);

        // Sirf digits check
        if (!Regex.IsMatch(phone, @"^\d+$"))
        {
            lblStatus.InnerText = "Phone number must contain digits only.";
            lblStatus.Style["color"] = "red";
            return;
        }

        if (phone.Length < minDigits || phone.Length > maxDigits)
        {
            lblStatus.InnerText = "For the selected country (" + countryName + "), phone number must be between " + minDigits + " and " + maxDigits + " digits."; lblStatus.Style["color"] = "red";
            return;
        }

        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("saweraarshad9921@gmail.com");
            mail.To.Add("saweraarshad9921@gmail.com");
            mail.Subject = "New Contact Form Message from " + name;
            mail.Body =
                "Name: " + name + "\n" +
                "Email: " + email + "\n" +
                "Country: " + countryName + "\n" +
                "Phone: " + phone + "\n\n" +
                "Message:\n" + message;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("saweraarshad9921@gmail.com", " wfud nocp dlwi wipa");
            smtp.Send(mail);

            lblStatus.InnerText = "Message sent successfully!";
            lblStatus.Style["color"] = "lightgreen";

            // Clear fields after send
            inp_name.Value = "";
            inp_email.Value = "";
            inp_phone.Value = "";
            inp_message.Value = "";
        }
        catch (Exception ex)
        {
            lblStatus.InnerText = ex.Message;
            if (ex.InnerException != null)
            {
                lblStatus.InnerText += "<br/>" + ex.InnerException.Message;
            }
        }
    }
}