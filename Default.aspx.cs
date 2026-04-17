using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
 

public partial class _Default : System.Web.UI.Page
{
    string MyError = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("Pages/login.aspx");

     //   try
      //  {
       //     MyError = Request.QueryString["errorpath"].ToString();
        //    if (MyError != null)
         //   {
         //       this.Label1.Text = MyError;//
        //    }
       //     else
        //    {
       //         Response.Redirect("~/Pages/login.aspx");
       //     }
      //  }
      //  catch (Exception Ex)
     //   {
      //      string asdf = Ex.Message.ToString();
       //     Response.Redirect("~/Pages/login.aspx");
     //   }
    }
}