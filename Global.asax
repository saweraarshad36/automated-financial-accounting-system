<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        //MyMain oMy = new MyMain();
        //HttpContext.Current.Application["RfCFY"] = oMy.GetRefCfy();
        //VType
        //oMy = new MyMain();
        //HttpContext.Current.Application["RfVtype"] = oMy.FillComboVtype();
        //if (UType.IsSql())
        //{
        //    //clsEncrypt OclsEncrypt = new clsEncrypt();
        //    //string aaaa = OclsEncrypt.EncryptString("1");

        //    MyMain oMyMain = new MyMain();
        //    //HttpContext.Current.Application["RfOffice"] = oMyMain.Sp_FillComboOffice();

        //    //oMyMain = new MyMain();
        //    //HttpContext.Current.Application["RfProject"] = oMyMain.FillComboProject();

        //    oMyMain = new MyMain();
        //    HttpContext.Current.Application["RfVtype"] = oMyMain.FillComboVtype();

        //    oMyMain = new MyMain();
        //    HttpContext.Current.Application["RfActCode"] = oMyMain.FillComboChart();

        //    oMyMain = new MyMain();
        //    HttpContext.Current.Application["RfPayGroup"] = oMyMain.FillComboPayGroup();

        //    oMyMain = new MyMain();
        //    HttpContext.Current.Application["RfUOM"] = oMyMain.FillComboUOM();

        //    oMyMain = new MyMain();
        //    HttpContext.Current.Application["RfLeave"] = oMyMain.FillComboLeave();

        //    MyRef oMyRef = new MyRef();
        //    HttpContext.Current.Application["RfDepartment"] = oMyRef.FillComboDepartment();

        //    oMyMain = new MyMain();
        //    HttpContext.Current.Application["RfActLevel"] = oMyMain.FillComboActLevel();

        //    oMyRef = new MyRef();
        //    HttpContext.Current.Application["RfMenu"] = oMyRef.FillComboMenu();

        //    oMyMain = new MyMain();
        //    HttpContext.Current.Application["RfPaymentMode"] = oMyMain.FillComboPaymentMode();

        //    oMyMain = new MyMain();
        //    HttpContext.Current.Application["RfEmployeeStatus"] = oMyMain.FillComboEmpStatus();
        //}
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

   void Application_Error(object sender, EventArgs e)
    {
       // Code that runs when an unhandled error occurs

   }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
