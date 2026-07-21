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
using System.Net;
using System.Net.NetworkInformation;


public partial class LoginUc : System.Web.UI.UserControl
{

    //clsEncrypt OclsEncrypt = new clsEncrypt();

    protected void Page_Load(object sender, EventArgs e)
    {

       // MyFileCopy();
        //MyMsg("File download successfully");
        GetCounter();
    }
  
    private void MyFileCopyBk()
    {
        string fileName = "WeBOC.mdb";
        string fileNameNew = "New" +"WeBOC.mdb";
        string sourcePath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/") ;
        string targetPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Web");
       
        

        // Use Path class to manipulate file and directory paths.
        string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
        string destFile = System.IO.Path.Combine(targetPath, fileNameNew);

       
        System.IO.File.Copy(sourceFile, destFile, true);
        destFile = @"c:\" + fileNameNew;
        try
        {

            WebClient webClient = new WebClient();
            
            //webClient.DownloadFile(fpat, @"c:\myfile.txt");
            webClient.DownloadFile(sourceFile, destFile);
        }
        catch (Exception ex)
        {
            string asdf = ex.Message.ToString();
            // throw;
        }
    }
    private void Mymdb(string NewFile)
    {

        try
        {

            WebClient webClient = new WebClient();
            string fpat = System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/App_Data/WeBOC.zip";
            //webClient.DownloadFile(fpat, @"c:\myfile.txt");
            webClient.DownloadFile(fpat, @"c:\myfile.txt");
        }
        catch (Exception ex)
        {
            string asdf = ex.Message.ToString();
           // throw;
        }
        //
        //  string PdfPath = MyConfig.GetKey("PDFPath", ""); //@"C:\output.pdf";
        // FileInfo info = new FileInfo(PdfPathName);
        // if (info.Exists)
        // {
        // Response.Redirect("~/Pages/RptPdf.aspx");
        //  MyUrl("RptPdf.aspx");

        //string rdr = @"C:\Program Files\Adobe\Acrobat 5.0\Acrobat\Acrobat.exe";
        // System.Diagnostics.Process.Start(rdr, path);

        //Response.ContentType = "Application/pdf";
        //Response.TransmitFile(PdfPathName);
    //}
    //////
  //  MyMain oMy = new MyMain();
  //      oMy.Fld1 = "poiu";  // this.txtUserName.Text;
  //      oMy.Fld2 = "kjhg"; // this.txtPassword.Text;
   //     string dsResult = oMy.InsertUser();
    }

    protected void BtnSignIn_Click(object sender, EventArgs e)
    {
        //MyFileCopy();
        //Mymdb();
        if (DoLogin())
        {
            //InsertLogin();
            Response.Redirect("~/Pages/Main.aspx");
        }
    }

    private bool DoLogin()
    {
        bool retVal = false;
        MyMain oMy = new MyMain();
        //bool Res11 = oMy.pValidation();
        //if (Res11 == false)
        //{
        //    //return false;
        //}
        //oMy = new MyMain();
        string tPwd = string.Empty;
        DataSet ds = null;
        if (this.txtUserName.Text.Length < 1 || this.txtPassword.Text.Length < 1)
        {
            this.lblErrorMessage.Visible = true;
            this.lblErrorMessage.Text = "Must be enter Id and Password";
            return false;
        }
        tPwd = UType.Chk1(this.txtPassword.Text);
        if (UType.IsWeb() == false)
        {

            //tPwd = MyEncrption.Encrypt(tPwd);
        }
        MyMain oMyMain = new MyMain();
        oMyMain.Fld1 = UType.Chk1(this.txtUserName.Text);
        oMyMain.Fld2 = tPwd;
        oMyMain.Fld3 = DateTime.Now.ToString("yyyyMMdd");
        ds = oMyMain.GetLogin();
        if (ds.Tables[0].Rows.Count == 0)
        {
            MyMsg("Invalid User ID Or Password");
            return retVal;

        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["UserId"] = ds.Tables[0].Rows[0]["UserId"].ToString();
            Session["LoginId"] = this.txtUserName.Text;
            Session["LoginName"] = UType.Chk1(ds.Tables[0].Rows[0]["LoginName"].ToString());
            Session["MenuLevel"] = "1";
            Session["RoleId"] = ds.Tables[0].Rows[0]["UserRoleId"].ToString();
            Session["EmpId"] = ds.Tables[0].Rows[0]["EmpId"].ToString();
            Session["sOfficeId"] = ds.Tables[0].Rows[0]["OfficeID"].ToString();
            Session["sProjectId"] = ds.Tables[0].Rows[0]["ProjectID"].ToString();
            Session["sClientLogo"] = ds.Tables[0].Rows[0]["ClientLogo"].ToString();
            Session["sClientName"] = ds.Tables[0].Rows[0]["ClientName"].ToString();
            Session["sClientId"] = ds.Tables[0].Rows[0]["ClientId"].ToString();
            Session["sModuleId"] = ds.Tables[0].Rows[0]["ModuleId"].ToString();

            Session["sOfficeName"] = ds.Tables[0].Rows[0]["ClientName"].ToString();
            Session["sProjectName"] = ds.Tables[0].Rows[0]["CostCenter"].ToString();
            Session["IsImage"] = "0";
            Session["sDisplayGrid"] = "1";
            if (ds.Tables[0].Rows[0]["UserIP"].ToString().Length < 1)
            {
                if (ds.Tables[0].Rows[0]["UserIPSts"].ToString() != "1")
                {
                    if (ds.Tables[0].Rows[0]["FirstTime"].ToString() == "1")
                    {
                        string pMac = GetMacAddress();
                        string pIP = GetIp();
                        oMyMain = new MyMain();

                        oMyMain.Fld1 = Session["sOfficeId"].ToString();
                        oMyMain.Fld2 = Session["sProjectId"].ToString();
                        oMyMain.Fld3 = Session["UserId"].ToString();
                        oMyMain.Fld4 = pMac;
                        oMyMain.Fld5 = pIP;
                        oMyMain.Fld6 = "2";   
                        string Res = oMyMain.UpdateUserMac();
                        if (Res != "Ok")
                        {
                            MyMsg("Error in Login");
                            return retVal;
                        }
                    }
                }
            }
            //Get FY
            if (FYcheck() == false)
            {
                UpdateFY();
            }

            retVal = true;
        }

        SetCounter();
        return retVal;
    }

    private bool FYcheck()
    {
        bool retVal = false;
        //Temporary CFY
        decimal CurYear = UType.MyCtoD(DateTime.Now.ToString("yyyy"));
        decimal CurMM = UType.MyCtoD(DateTime.Now.ToString("MM"));
        if (CurMM < 7)
        {
            CurYear = CurYear - 1;
        }
        MyMain oMy = new MyMain();
        oMy.Fld1 = Session["sOfficeId"].ToString();
        oMy.Fld2 = Session["sProjectId"].ToString();
        string cFY = oMy.GetRefCfy();
        if (cFY == "")
        {
            InsertFY();
        }
        else
        {
            if (UType.MyCtoD(cFY) != CurYear)
            {
                UpdateFY();
            }
        }
        return retVal;
    }

    private bool InsertFY()
    {
        bool retVal = false;
        MyMain oMy = new MyMain();
        oMy.Fld1 = Session["sOfficeId"].ToString();
        oMy.Fld2 = Session["sProjectId"].ToString();
        //
        decimal CurYear = UType.MyCtoD(DateTime.Now.ToString("yyyy"));
        decimal CurMM = UType.MyCtoD(DateTime.Now.ToString("MM"));
        if (CurMM < 7)
        {
            CurYear = CurYear - 1;
        }
        //
        oMy.Fld3 = CurYear.ToString();
        oMy.Fld4 = CurYear.ToString() + "0701";
        oMy.Fld5 = Convert.ToString(CurYear + 1) + "0630";

        string Result = oMy.InsertRefNew();
        if (Result != "Ok")
        {
            MyMsg(Result);
        }
        return retVal;
    }
    private bool UpdateFY()
    {
        bool retVal = false;
        MyMain oMy = new MyMain();
        oMy.Fld1 = Session["sOfficeId"].ToString();
        oMy.Fld2 = Session["sProjectId"].ToString();
        //
        decimal CurYear = UType.MyCtoD(DateTime.Now.ToString("yyyy"));
        decimal CurMM = UType.MyCtoD(DateTime.Now.ToString("MM"));
        if (CurMM < 7)
        {
            CurYear = CurYear - 1;
        }
        //
        oMy.Fld3 = CurYear.ToString();
        oMy.Fld4 = CurYear.ToString() + "0701";
        oMy.Fld5 = Convert.ToString(CurYear + 1) + "0630";

        string Result = oMy.UpdateRef1();
        if (Result != "Ok")
        {
            MyMsg(Result);
        }
        return retVal;
    }


    private void GetCounter()
    {
        decimal hits = 0;
        DataSet ds = null;
       
            MyMain oMy = new MyMain();
            ds = oMy.GetCount();
       

        if (ds != null)
        {
            hits = UType.MyCtoD(ds.Tables[0].Rows[0]["MaxSno"].ToString());
        }
        hits += 1;
        lblCounter.Text = "Hit Count: " + hits.ToString();
    }

    private void SetCounter()
    {
        if (UType.IsSql())
        {
            MyMain oMy = new MyMain();
            try
            { oMy.Fld1 = Session["UserId"].ToString(); }
            catch (Exception ex)
            {
                oMy.Fld1 = "0";
            }
            oMy.Fld2 = "I";
            string Res = oMy.InsertLoginHistory();
        }
        if (UType.IsSql() == false)
        {
            MyMainAcs oMy = new MyMainAcs();
            try
            { oMy.Fld1 = Session["UserId"].ToString(); }
            catch (Exception ex)
            {
                oMy.Fld1 = "0";
            }
            oMy.Fld2 = "I";
            string Res = oMy.InsertLoginHistory();
        }

    }

    #region XML Count
    //private void DoCountXml()
    //{
    //    this.countMeXml();

    //    DataSet tmpDs = new DataSet();
    //    tmpDs.ReadXml(Server.MapPath("~/count/counter.xml"));

    //    lblCounter.Text = "Hit Count:" + tmpDs.Tables[0].Rows[0]["hits"].ToString();
    //}

    //private void countMeXml()
    //{

    //    DataSet tmpDs = new DataSet();
    //    tmpDs.ReadXml(Server.MapPath("~/count/counter.xml"), XmlReadMode.ReadSchema);

    //    int hits = Int32.Parse(tmpDs.Tables[0].Rows[0]["hits"].ToString());

    //    hits += 1;

    //    tmpDs.Tables[0].Rows[0]["hits"] = hits.ToString();

    //    //tmpDs.WriteXml(Server.MapPath("~/counter.xml"));

    //    //tmpDs.WriteXml(Context.Server.MapPath("~/counter.xml"), XmlWriteMode.WriteSchema);


    //    tmpDs.WriteXml(Context.Server.MapPath("~/count/counter.xml"), XmlWriteMode.WriteSchema);

    //} 
    #endregion

    protected void BtnChangePW_Click(object sender, EventArgs e)
    {

    }
    private void MyMsg(string Msg)
    {
        this.lblErrorMessage.Visible = false;
        if (Msg == "A")
        {
            try
            {
                if (Session["ErrorMessage"].ToString().Length > 0)
                {
                    this.lblErrorMessage.Text = Session["ErrorMessage"].ToString();
                    this.lblErrorMessage.Visible = true;
                    Session["ErrorMessage"] = "";
                }
            }
            catch (Exception ex)
            {


            }
        }
        else
        {
            Session["ErrorMessage"] = Msg;
            Response.Redirect("~/Pages/Login.aspx");
        }

    }
    private string GetMacAddress()
    {
        string macAddresses = string.Empty;

        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                macAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
            //if (macAddresses == string.Empty)
            //{
            //    macAddresses = nic.GetPhysicalAddress().ToString();  //nic.Id;
            //}

        }

        return macAddresses;
    }
    private string GetIp()
    {
        string retVal = "";
        try
        {
            retVal = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(retVal))
            {
                retVal = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            //string clientIPAddress;
            //string strHostName = Dns.GetHostName();
            //System.Net.IPHostEntry ipHostInfo = Dns.Resolve(System.Net.Dns.GetHostName());
            //System.Net.IPAddress ipAddress = ipHostInfo.AddressList[0];
            //retVal = Request.ServerVariables["REMOTE_ADDR"].ToString();
        }
        catch (Exception Ex)
        {
            String Res = Ex.Message.ToString();
        }
        return retVal;
    }

    protected void Btndown_Click(object sender, EventArgs e)
    {
       // MyFileCopy();
        //Response.Redirect("download.aspx");
    }
}
