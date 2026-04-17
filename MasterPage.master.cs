using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Session["sClientLogo"] = "logo.png";
       // this.ClientLogo.ImageUrl = "Images/" + Session["sClientLogo"].ToString();
       // this.lblDate.ForeColor=System.Drawing.Color.White;
       
       //lblDate.Text = "Login Date is : " + DateTime.Now.ToString("dd-MM-yyyy");
        MyMain oMyMain = new MyMain();
        oMyMain.Fld1 = Session["UserID"].ToString();
        oMyMain.Fld2 = "1";
        string mBG = oMyMain.MyColor();
        this.MPage.Attributes.Add("bgcolor", "white"); //this.MPage.Attributes.Add("bgcolor", mBG);
        this.lblUserID.Text = "Login Date is : " + DateTime.Now.ToString("dd-MM-yyyy") + "      |          " +"User ID : " + Session["LoginId"].ToString();
    }

    protected void btnHide_Click(object sender, EventArgs e)
    {
       
    }
    public void SetTable()
    {
        //this.LeftMenu1.SetTbl();
    }
}
