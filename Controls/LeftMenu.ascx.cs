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

public partial class Controls_LeftMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["sClientLogo"] = "logo.png";
               // this.ClientLogo.ImageUrl = "../Images/" + Session["sClientLogo"].ToString();
               // this.Image2.ImageUrl = "../Images/line.png" ;
                //this.Image3.ImageUrl = "../Images/search.png";
                MyMain oMyMain = new MyMain();
                oMyMain.Fld1 = Session["UserID"].ToString();
               // oMyMain.Fld2 = "1";
                DataSet mBG = oMyMain.GetRfColor();
                string DG1 = "Gray"; string DG2 = "#52ab98";  //string DG1 = "#6495FF"; string DG2 = "#99CCFF";
                if(mBG.Tables[0].Rows.Count>0)
                {
                    //DG1 = mBG.Tables[0].Rows[0]["leftmenu1"].ToString();
                   // DG2 = mBG.Tables[0].Rows[0]["leftmenu2"].ToString();
                }

                this.dgMenu.BackColor = System.Drawing.Color.FromName(DG1);
                this.dgMenu.AlternatingItemStyle.BackColor = System.Drawing.Color.FromName(DG2);
                //GetMenuInfo(Session["UserId"].ToString(), Session["MenuLevel"].ToString());

                DoGetMenuInfo();
            }
            catch (Exception ex)
            {
                Response.Redirect("Login.aspx");
            }

            #region Set Mouse Event
           // this.dgMenu.Attributes.Add("onmouseover", "change1(event);");
          //  this.dgMenu.Attributes.Add("onmouseout", "change2();");

            #endregion
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("chartofAccount.aspx");
    }

    //    private void DoGetMenuInfo(string pUserId, string MenuLevel)
    private void DoGetMenuInfo()
    {
        MyMain oMyMain = new MyMain();

        oMyMain.Fld1 = Session["sOfficeId"].ToString();
        oMyMain.Fld2 = Session["sProjectId"].ToString();

        // oMyMain.Fld3 = "1";
        try
        {
            oMyMain.Fld3 = Session["LevelId"].ToString();
        }
        catch (Exception ex)
        {

            oMyMain.Fld3 = "1";
        }
        try
        {
            //oMyMain.Fld4 = Session["MenuId"].ToString();
            oMyMain.Fld4 = Session["MenuId"].ToString().Substring(0,1);
            if(UType.MyCtoD(oMyMain.Fld3)<2)
            { //oMyMain.Fld4 = "0"; 
            }
        }
        catch (Exception ex)
        {

            oMyMain.Fld4 = "0";
        }
        oMyMain.Fld5 = Session["UserId"].ToString();

      
        DataSet ds = oMyMain.Sp_GetMenu();
        if (ds != null)
        {
            InitGrid(ds.Tables[0]);
        }
    }

    



    private void InitGrid(DataTable Tbl1)
    {
        this.dgMenu.DataSource = Tbl1;
        this.dgMenu.DataBind();
    }
    protected void dgMenu_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string fMenuPath = e.Item.Cells[0].Text;
        string fMenuID = e.Item.Cells[1].Text;
        Session["MenuId"] = e.Item.Cells[1].Text;
        //if (UType.MyCtoD(fMenuID) == 6)
        //{
        //    Response.Redirect(fMenuPath);
        //}
            if (UType.MyCtoD(fMenuID) < 99)
        {
            Session["LevelId"] = "2";
            fMenuPath = ("~/Pages/Main.aspx");
            Session["MenuId"] = e.Item.Cells[1].Text;
            Session["IsImage"] = "1";
        }
        if (UType.MyCtoD(fMenuID) > 99)
        {
            //Session["MenuId"] = "0";
            Session["LevelId"] = "2";
            Session["IsImage"] = "0";
        }
    
        if (UType.MyCtoD(fMenuID) == 99)
        {
         Session["LevelId"] = "1";
         Session["MenuId"] = "1";
         Session["IsImage"] = "0";
        }
        if (UType.MyCtoD(fMenuID) == 98)
        {
            Session["LevelId"] = "2";
            fMenuPath = ("~/Pages/login.aspx");
           
        }
        Response.Redirect(fMenuPath);
    }
    private string GetDepart()
    {
        string retVal = string.Empty;
        try
        {
            retVal = Session["pDepart"].ToString();
        }
        catch (Exception Ex)
        {
            retVal = "1";
        }
        return retVal;
    }
    private string GetMenuId()
    {
        string retVal = string.Empty;
        try
        {
            retVal = Session["fMenuId"].ToString();
        }
        catch (Exception Ex)
        {
            retVal = "10";
        }
        return retVal;
    }


    public void SetTbl()
    {
        this.tblLeft.Height = "600px";
    }


}

