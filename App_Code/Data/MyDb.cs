using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data.OleDb;
using System.Runtime.InteropServices;
/// <summary>
/// Summary description for DBEDBHeader
/// </summary>
public class MyDb
{

    #region Data Members

    private SqlConnection _SqlConnection;
    private MyMain _MyMain;
    string SqlStr = string.Empty;
    string Sqote = "'";
    string Comma = ",";

    #endregion


    #region Constructors
    public MyDb(MyMain objMyMain)
    {
        _MyMain = objMyMain;
    }

    public MyDb(SqlConnection Sqlcon, MyMain objMyMain)
    {
        _SqlConnection = Sqlcon;
        _MyMain = objMyMain;
    }

    #endregion

    #region Properties

    private SqlConnection SqlConnection
    {
        get { return _SqlConnection; }
    }

    private MyMain MyMain
    {
        get { return _MyMain; }
    }

    #endregion


    #region SQL functions

    public DataSet GetLogin()
    {

        SqlStr = "SELECT ui.*,ur.UserRoleId, ro.OfficeDescription as ClientName ,rp.projectdescription as CostCenter,rp.oAddress,rp.oTel,rp.oEmail,rp.oNTN ";
        SqlStr += ",rp.oLogoPath as clientLogo,ui.loginid as clientid, rp.moduleid, rp.countryid,rp.ocityid as CityID  ";
        SqlStr += " FROM UserInfo ui ";
        SqlStr += " INNER JOIN UserRole ur ON ui.UserId = ur.UserId";
        SqlStr += " INNER JOIN rfoffice  ro ON ui.officeid = ro.officeid ";
        SqlStr += " INNER JOIN rfproject rp ON ui.officeid = rp.officeid and ui.projectid=rp.projectid";
        SqlStr += " WHERE ";
        SqlStr += " Loginid = '" + MyMain.Fld1 + "'";
        SqlStr += " AND Loginpassword = '" + MyMain.Fld2 + "'";
        SqlStr += " AND rp.effdate <=" + MyMain.Fld3;
        SqlStr += " AND rp.enddate >=" + MyMain.Fld3;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
        //AND rp.effdate <=20210606 and  rp.enddate >=20210606
    }
    public string UpdateLogin()
    {

        SqlStr = "Update UserInfo  ";
        SqlStr += " set Loginpassword = '" + MyMain.Fld3 + "'";
        SqlStr += " , Updatedate = " + MyMain.Fld4;
        SqlStr += " , FirstTime = 2 ";

        SqlStr += " WHERE ";
        SqlStr += " Loginid = '" + MyMain.Fld1 + "'";
        SqlStr += " AND Loginpassword = '" + MyMain.Fld2 + "'";


        string ds = NonQryCmdSp(SqlStr);
        return ds;
        //AND rp.effdate <=20210606 and  rp.enddate >=20210606
    }

    public DataSet Sp_GetLogin(string pUserId, string pPassword)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pLoginId", pUserId, ParameterDirection.Input, SqlDbType.VarChar);
            parameters[1] = Connection.GetParam("@pPassword", pPassword, ParameterDirection.Input, SqlDbType.VarChar);
            //parameters[1] = Connection.GetParam("@pPassword", Convert.ToDecimal(ActCode), ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetLogin", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet GetMenu()
    {
        DataSet ds1 = null;
        try
        {
            SqlStr = "SELECT ui.loginid,ui.UserId ";
            SqlStr += ",rrm.*  ,rm.menutext,rm.MenuPath,rm.ImagePath,rm.MenuLevel ";
            SqlStr += "FROM userinfo as ui ";
            SqlStr += "INNER JOIN RfProject AS rp ON ui.OfficeId = rp.OfficeId AND ui.ProjectId=rp.ProjectId ";
            //SqlStr += "inner join RfRoleMenu as rrm ON ui.OfficeId = rrm.OfficeId AND ui.ProjectId=rrm.ProjectId and  ui.roleid=rrm.roleid ";
            SqlStr += "inner join RfUserMenu as rrm ON ui.OfficeId = rrm.OfficeId AND ui.ProjectId=rrm.ProjectId and  ui.userid=rrm.USerID ";
            SqlStr += "INNER JOIN RfMenu AS rm ON rrm.menuid = rm.MenuId  ";

            SqlStr += " WHERE ";
            SqlStr += "  ui.officeid = " + MyMain.Fld1;
            SqlStr += " and ui.projectid = " + MyMain.Fld2;
            SqlStr += " and rm.menulevel = " + MyMain.Fld3;
            //SqlStr += " and rrm.level = " + MyMain.Fld4;

            SqlStr += " and ui.userid = " + MyMain.Fld5;

            if (UType.MyCtoD(MyMain.Fld4) > 0)
            {
                if (UType.MyCtoD(MyMain.Fld3) == 2)
                {
                    //SqlStr += " and substring(ltrim(CONVERT(varchar, rrm.level)), 1, 1) = " + MyMain.Fld4;
                    SqlStr += " and (substring(ltrim(CONVERT(varchar, rrm.menuid)), 1, 1)) = " + MyMain.Fld4;
                }
            }
            // if (UType.MyCtoD(MyMain.Fld3) == 1)
            //  {
            //      SqlStr += "  or rrm.menuid = 98 ";
            //  }
            //  if (UType.MyCtoD(MyMain.Fld3) == 2)
            //  {
            //     SqlStr += "  or rrm.menuid = 99 ";
            //  }
            // SqlStr += "  or rrm.menuid = 99 ";
            SqlStr += "  and rm.IsActive = 1";
            SqlStr += "  and rrm.Isview = 1";
            //SqlStr += " ) "; // or rm.menuid = 99";
            SqlStr += " ORDER BY rrm.MenuSno";
            ds1 = GetDataset(SqlStr);

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet GetMenuBk()
    {
        DataSet ds1 = null;
        try
        {
            SqlStr = "SELECT ui.loginid,ui.UserId ";
            SqlStr += ",rrm.*  ,rm.menutext,rm.MenuPath,rm.ImagePath,rm.MenuLevel ";
            SqlStr += "FROM userinfo as ui ";
            SqlStr += "INNER JOIN RfProject AS rp ON ui.OfficeId = rp.OfficeId AND ui.ProjectId=rp.ProjectId ";
            SqlStr += "inner join RfRoleMenu as rrm ON ui.OfficeId = rrm.OfficeId AND ui.ProjectId=rrm.ProjectId and  ui.roleid=rrm.roleid ";
            SqlStr += "INNER JOIN RfMenu AS rm ON rrm.menuid = rm.MenuId  ";

            SqlStr += " WHERE ";
            SqlStr += " ui.officeid = " + MyMain.Fld1;
            SqlStr += " and ui.projectid = " + MyMain.Fld2;
            SqlStr += " and rm.menulevel = " + MyMain.Fld3;
            SqlStr += " and ui.RoleId = " + MyMain.Fld4;
            SqlStr += " and ui.userid = " + MyMain.Fld5;

            if (UType.MyCtoD(MyMain.Fld6) > 0)
            {
                //SqlStr += " and substring(ltrim(CONVERT(varchar, rrm.MenuSno)), 1, 1) =9"; // + MyMain.Fld6;
            }
            SqlStr += " ORDER BY rrm.MenuSno";
            ds1 = GetDataset(SqlStr);

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetActCode()
    {
        SqlStr = "SELECT * FROM ActChart order by ActCode1 ";

        DataSet Res = GetDatasetSpNew(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;

    }

    public DataSet GetList(string pActLevel)
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM Chart";
        SqlStr += " WHERE ";
        SqlStr += " ActLevel <= " + pActLevel;
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }

    public DataSet GetListByActCode(string pActCode)
    {
        int ActCodeLen = pActCode.Length;
        SqlStr = "SELECT * ";
        SqlStr += " FROM Chart";
        SqlStr += " WHERE ";

        SqlStr += " CONVERT(INT,substring(LTRIM(STR(actcode)),1," + ActCodeLen + "))" + "=" + pActCode;
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;


    }

    public DataSet GetDescriptionByActCode(string pActCode)
    {
        int ActCodeLen = pActCode.Length;
        SqlStr = "SELECT ActDesc ";
        SqlStr += " FROM actChart";
        SqlStr += " WHERE ";
        SqlStr += " actcode =" + pActCode;
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;


    }
    public DataSet GetFillComboLevel()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfActLevel ";
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }




    public DataSet GetFillComboProject()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfProject ";
        DataSet ds = GetDataset(SqlStr);
        return ds;

    }

    public DataSet GetFillComboVtype()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM ActRfVtype ";
        if (MyMain.Fld1.Length > 0)
        {
            SqlStr += " where vtypeid >=" + MyMain.Fld1;
            SqlStr += " and vtypeid <=" + MyMain.Fld2;
        }
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetActChart()
    {
        SqlStr = "SELECT ac.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  actchart ac  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        SqlStr += " where ac.officeid = " + MyMain.Fld1;
        SqlStr += " and ac.ProjectId = " + MyMain.Fld2;
        if (MyMain.Fld3.Length > 0)
        {
            SqlStr += " and ac.actdesc like '%" + MyMain.Fld3 + "%'";
        }
        if (MyMain.Fld4.Length > 0)
        {
            SqlStr += " and SUBSTRING(LTRIM(STR(ac.actcode)), 1, " + MyMain.Fld4.Length + ") = " + MyMain.Fld4;
        }
        if (MyMain.Fld10.Length > 0)
        {
            if (MyMain.Fld10 == "5")
            {
                //SqlStr += " and len(LTRIM(STR(ac.actcode))) > " + MyMain.Fld10;
                SqlStr += " and len(LTRIM(STR(ac.actcode))) < 8";
            }
            if (MyMain.Fld10 == "7")
            {
                SqlStr += " and len(LTRIM(STR(ac.actcode))) > " + MyMain.Fld10;
            }
        }
        SqlStr += " order by ac.actcode1 ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetActChartEntry()
    {
        SqlStr = "SELECT a.* ";
        //SqlStr += " FROM ActChart  ";
        SqlStr += " FROM    ";
        SqlStr += "  actchart a  ";
        //SqlStr += "  a.officechartid = b.chartid ";
        SqlStr += " where a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (MyMain.Fld3.Length > 0)
        {
            SqlStr += " and a.actdesc like '%" + MyMain.Fld3 + "%'";
        }
        SqlStr += " and len(LTRIM(STR(a.actcode))) > 7 ";

        SqlStr += " order by a.actcode ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetActChartReference()
    {
        SqlStr = "SELECT ac.* ";
        SqlStr += " FROM    ";
        SqlStr += " actchart ac  ";
        SqlStr += " where ac.officeid = " + MyMain.Fld1;
        SqlStr += " and ac.ProjectId = " + MyMain.Fld2;
        SqlStr += " and len(LTRIM(STR(ac.actcode))) < 3 ";

        SqlStr += " order by ac.actcode1 ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetActChartCode()
    {
        SqlStr = "SELECT ac.* ";
        SqlStr += " FROM  ";
        SqlStr += " actchart ac  ";
        SqlStr += " where ac.officeid = " + MyMain.Fld1;
        SqlStr += " and ac.ProjectId = " + MyMain.Fld2;
        if (MyMain.Fld3.Length > 0)
        {
            SqlStr += " and ac.actcode = " + MyMain.Fld3;
        }

        SqlStr += " order by ac.actcode1 ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxActCodeNew()
    {


        // SqlStr = " SELECT ac.actcode, SUBSTRING(LTRIM(STR(ac.actcode)), 1, " + MyMain.Fld3.Length + ") as MaxCode ";
        SqlStr = " SELECT max(ac.actcode) as MaxCode ";
        SqlStr += " FROM    ";
        SqlStr += " actchart ac  ";
        SqlStr += " where ac.officeid = " + MyMain.Fld1;
        SqlStr += " and ac.ProjectId = " + MyMain.Fld2;
        SqlStr += " and SUBSTRING(LTRIM(STR(ac.actcode)), 1, " + MyMain.Fld3.Length + ") =" + MyMain.Fld3;

        //SqlStr += " order by ac.actcode desc";

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxActCode()
    {


        SqlStr = " SELECT ac.actcode, SUBSTRING(LTRIM(STR(ac.actcode)), 1, " + MyMain.Fld3.Length + ") as MaxCode ";
        SqlStr += " FROM    ";
        SqlStr += " actchart ac  ";
        SqlStr += " where ac.officeid = " + MyMain.Fld1;
        SqlStr += " and ac.ProjectId = " + MyMain.Fld2;

        if (MyMain.Fld3.Length > 0)
        {
            if (MyMain.Fld3.Length == 2 || MyMain.Fld3.Length == 4)
            {
                int FldLen = MyMain.Fld3.Length + 2;
                SqlStr += " and SUBSTRING(LTRIM(STR(ac.actcode)), 1, " + MyMain.Fld3.Length + ") = " + MyMain.Fld3;
                SqlStr += " and len(LTRIM(STR(ac.actcode))) = " + FldLen;
            }
            if (MyMain.Fld3.Length == 6)
                SqlStr += " and SUBSTRING(LTRIM(STR(ac.actcode)), 1, " + MyMain.Fld3.Length + " ) = " + MyMain.Fld3;
        }
        SqlStr += " order by ac.actcode desc";

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetFillComboPayGroup()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfPayGroup order by PayGroupId ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }

    public DataSet GetFillComboEmpStatus()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfEmployeeStatus order by EmployeeStatusId ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }

    public DataSet GetFillComboUOM()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfUOM order by UOMId ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }


    #region Chart
    public DataSet Sp_SelectChart()
    {
        DataSet ds1 = new DataSet();
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@P1", Convert.ToDecimal(MyMain.ActCode), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_SelectChart", parameters);

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet SelectChartAll()
    {
        //SqlStr = "SELECT a.* ";
        //SqlStr += " FROM Actchart a, ActRef b ";
        //SqlStr += " where a.officeid = b.officeid";
        //SqlStr += " and   a.projectid = b.projectid";
        //SqlStr += " order by a.actcode";
        DataSet ds = null;// SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }

    public DataSet SelectChartAllSp()
    {
        DataSet ds1 = null;
        try
        {
            //return SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_VoucherPrint", parameters);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_ChartPrint");
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public string InsertChartOld()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "INSERT INTO ActChart";
            SqlStr += "(";
            SqlStr += "OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",ActCode";
            SqlStr += ",ActDesc ";
            SqlStr += ",Balance";
            SqlStr += ",BalanceSts";
            SqlStr += ",ActStatus";
            SqlStr += ",ActLevel ";
            SqlStr += ",AddUser ";
            SqlStr += ",AddDate ";
            SqlStr += ",AddTime ";
            SqlStr += ",ActCode1";
            SqlStr += ",unitrate";
            SqlStr += ",arlimit";
            SqlStr += ",arlimitdays";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.OfficeId;
            SqlStr += Comma + MyMain.ProjectId;
            SqlStr += Comma + MyMain.ActCode;
            SqlStr += Comma + Sqote + MyMain.ActDescription + Sqote;
            SqlStr += Comma + MyMain.Balance;
            SqlStr += Comma + Sqote + MyMain.BalanceSts + Sqote;
            SqlStr += Comma + Sqote + MyMain.ActStatus + Sqote;
            SqlStr += Comma + MyMain.ActLevel;
            SqlStr += Comma + Sqote + MyMain.LoginId + Sqote;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmmssff");
            SqlStr += Comma + MyMain.ActCode.PadRight(12, '0');
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ")";

            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertChart()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "INSERT INTO ActChart";
            SqlStr += "(";
            SqlStr += "OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",ActCode";
            SqlStr += ",ActDesc ";
            //SqlStr += ",Balance";
            //SqlStr += ",BalanceSts";
            //SqlStr += ",ActStatus";
            //SqlStr += ",ActLevel ";
            //SqlStr += ",AddUser ";
            //SqlStr += ",AddDate ";
            //SqlStr += ",AddTime ";
            //SqlStr += ",ActCode1";
            //SqlStr += ",unitrate";
            //SqlStr += ",arlimit";
            //SqlStr += ",arlimitdays";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + Sqote + Sqote;
            //SqlStr += Comma + MyMain.Balance;
            //SqlStr += Comma + Sqote + MyMain.BalanceSts + Sqote;
            //SqlStr += Comma + Sqote + MyMain.ActStatus + Sqote;
            //SqlStr += Comma + MyMain.ActLevel;
            //SqlStr += Comma + Sqote + MyMain.LoginId + Sqote;
            //SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            //SqlStr += Comma + DateTime.Now.ToString("HHmmssff");
            //SqlStr += Comma + MyMain.ActCode.PadRight(12, '0');
            //SqlStr += Comma + UType.MyCtoDs(MyMain.Fld1);
            //SqlStr += Comma + UType.MyCtoDs(MyMain.Fld11);
            //SqlStr += Comma + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ")";

            retVal = NonQryCmdSp(SqlStr);
            //_SqlConnection.Open();

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateChart()
    {
        string retVal = string.Empty;
        DataSet Ds = null;
        string tOffChartID = "0";
        string MaxOffChartID = "0";
        bool InsRec = false;
        //try
        //{
        //    SqlStr = "SELECT oc.chartid  ";
        //    SqlStr += " FROM actchart oc ";
        //    SqlStr += " WHERE ";
        //    SqlStr += " oc.officeid = " + MyMain.OfficeId;
        //    SqlStr += " and oc.projectid = " + MyMain.ProjectId;
        //    SqlStr += " and oc.actcode = " + MyMain.ActCode;
        //    Ds = GetDatasetSpNew(SqlStr);
        //    if (Ds.Tables[0].Rows.Count > 0)
        //    {
        //        tOffChartID = Ds.Tables[0].Rows[0]["chartid"].ToString();
        //        InsRec = true;
        //    }

        //}
        //catch (Exception ex)
        //{
        //    retVal = ex.Message;
        //}

        try
        {


            SqlStr = "Update ActChart Set ";
            //SqlStr += "OfficeId = " + MyMain.OfficeId;
            //SqlStr += ",ProjectId = " + MyMain.ProjectId;
            //SqlStr += ",ActCode = " + Sqote + MyMain.ActCode + Sqote;
            //SqlStr += "ActCode1 = " + Sqote + MyMain.ActCode.PadRight(16, '0') + Sqote;
            SqlStr += "ActCode1 = " + Sqote + MyMain.Fld22 + Sqote;
            SqlStr += ",ActDesc = " + Sqote + MyMain.ActDescription + Sqote;
            SqlStr += ",Balance = " + MyMain.Balance;
            SqlStr += ",BalanceSts =" + Sqote + MyMain.BalanceSts + Sqote;
            SqlStr += ",ActStatus =" + Sqote + MyMain.ActStatus + Sqote;

            SqlStr += ",ActLevel =" + MyMain.ActLevel;
            SqlStr += ",AddUser =" + Sqote + MyMain.LoginId + Sqote;
            SqlStr += ",AddDate =" + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += ",AddTime =" + DateTime.Now.ToString("HHmmssff");
            SqlStr += ",unitrate =" + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += ",arlimit =" + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",arlimitdays =" + UType.MyCtoDs(MyMain.Fld12);
            //SqlStr += ",actaddress =" + Sqote + MyMain.Fld13 + Sqote;
            //SqlStr += ",ReportId =" + MyMain.Fld14;
            //SqlStr += ",FldLevel =" + MyMain.Fld15;
            SqlStr += ",FldSNo =" + UType.MyCtoDs(MyMain.Fld16);

            SqlStr += ",ActNTN =" + Sqote + MyMain.Fld17 + Sqote;
            SqlStr += ",ActSTR =" + Sqote + MyMain.Fld18 + Sqote;
            SqlStr += ",ActGST =" + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",ActADGST =" + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",ActPhone =" + Sqote + MyMain.Fld21 + Sqote;
            SqlStr += ",CountryID =" + UType.MyCtoDs(MyMain.Fld23);
            SqlStr += ",cityid =" + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ",ActAddress =" + Sqote + MyMain.Fld25 + Sqote;
            SqlStr += ",ClientStatus =" + UType.MyCtoDs(MyMain.Fld26);
            SqlStr += ",ExpenseRate =" + UType.MyCtoDs(MyMain.Fld27);


            SqlStr += " Where officeid = " + MyMain.OfficeId;
            SqlStr += " and Projectid = " + MyMain.ProjectId;
            SqlStr += " and ActCode = " + MyMain.ActCode;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet GetActCodeDesc(string pActCode)
    {
        //string sQote = "'";
        //SqlStr = "SELECT ActDesc,arlimit,arlimitdays ";
        //SqlStr += " FROM ActChart ";
        //SqlStr += " Where ActCode = " + Sqote + pActCode + Sqote;
        DataSet ds = null; GetDataset(SqlStr);
        return ds;
    }

    #endregion


    #region Ref
    public DataSet SelectRef()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RefTbl ";
        SqlStr += " where officeid = " + MyMain.Fld1;
        SqlStr += " and Projectid = " + MyMain.Fld2;

        DataSet ds = GetDatasetSpNew(SqlStr); // SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }


    public string GetRefCfy()
    {
        string retVal = string.Empty;
        try
        {
            if (UType.MyCtoD(MyMain.Fld1) < 1)
            {
                return "Login Please";
            }

            DataSet ds = SelectRef(); //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            { retVal = ds.Tables[0].Rows[0]["cfy"].ToString(); }

        }
        catch (Exception ex)
        {
            retVal = "Database not accessable";
        }
        return retVal;
    }
    public string GetRefCfyStart()
    {
        string retVal = string.Empty;
        try
        {
            retVal = GetRefCfy() + "0701";
        }
        catch (Exception ex)
        {
            retVal = "Database not accessable";
        }

        return retVal;
    }
    public string InsertRef()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "INSERT INTO RefTbl";
            SqlStr += "( OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",SDFyear";
            SqlStr += ",EDFyear ";
            SqlStr += ",cfy ";
            SqlStr += ",LSIStat";
            SqlStr += ",RSIStat";
            SqlStr += ",LSBSheet";
            SqlStr += ",RSBSheet";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld20;
            SqlStr += Comma + MyMain.Fld21;
            SqlStr += Comma + MyMain.StartFinancialYear;
            SqlStr += Comma + MyMain.EndFinancialYear;
            SqlStr += Comma + MyMain.Fld1;
            SqlStr += Comma + MyMain.LeftSideIncomeStatement;
            SqlStr += Comma + MyMain.RightSideIncomeStatement;
            SqlStr += Comma + MyMain.LeftSideBalanceSheet;
            SqlStr += Comma + MyMain.RightSideBalanceSheet;
            SqlStr += ")";


            retVal = NonQryCmdSp(SqlStr);
            //_SqlConnection.Open();

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateRef()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "Update RefTbl Set ";

            SqlStr += "SDFyear = " + MyMain.StartFinancialYear;
            SqlStr += ",EDFyear = " + MyMain.EndFinancialYear;
            SqlStr += ",LSIStat = " + MyMain.LeftSideIncomeStatement;
            SqlStr += ",RSIStat =" + MyMain.RightSideIncomeStatement;
            SqlStr += ",LSBSheet =" + MyMain.LeftSideBalanceSheet;
            SqlStr += ",RSBSheet = " + MyMain.RightSideBalanceSheet;
            SqlStr += ",CFY = " + MyMain.Fld1;
            SqlStr += ",DebtorActCode = " + MyMain.Fld2;
            SqlStr += ",CreditorActCode = " + MyMain.Fld3;
            SqlStr += ",PurActCode = " + MyMain.Fld4;
            SqlStr += ",SalActCode = " + MyMain.Fld5;
            SqlStr += ",ExpenseActCode = " + MyMain.Fld6;
            SqlStr += " where";
            SqlStr += " OfficeId = " + MyMain.Fld20;
            SqlStr += " and ProjectId = " + MyMain.Fld21;
            //DebtorActCode
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    public string InsertRefNew()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "INSERT INTO RefTbl";
            SqlStr += "( OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",SDFyear";
            SqlStr += ",EDFyear ";
            SqlStr += ",cfy ";
            //SqlStr += ",LSIStat";
            //SqlStr += ",RSIStat";
            //SqlStr += ",LSBSheet";
            //SqlStr += ",RSBSheet";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld3;
            //SqlStr += Comma + MyMain.LeftSideIncomeStatement;
            //SqlStr += Comma + MyMain.RightSideIncomeStatement;
            //SqlStr += Comma + MyMain.LeftSideBalanceSheet;
            //SqlStr += Comma + MyMain.RightSideBalanceSheet;
            SqlStr += ")";


            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet SelectTranTbl()
    {
        SqlStr = "SELECT *  ";
        SqlStr += " FROM ActTran ";
        SqlStr += " WHERE ";
        SqlStr += " OfficeId = " + MyMain.OfficeId;
        SqlStr += " and ProjectId = " + MyMain.ProjectId;
        SqlStr += " and CFY = " + MyMain.Fyear;
        //SqlStr += " and Vtypeid = '" + MyMain.Vtype + "'";
        SqlStr += " and VNo = " + MyMain.VNo;

        DataSet Res = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public string DeleteTranTbl()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "DELETE  ";
            SqlStr += " FROM ActTran ";
            SqlStr += " WHERE ";

            SqlStr += " tranid =" + MyMain.TranId;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;

        //old
        //SqlStr = "DELETE  ";
        //SqlStr += " FROM ActTran ";
        //SqlStr += " WHERE ";
        //SqlStr += " OfficeId = " + MyMain.OfficeId;
        //SqlStr += " and ProjectId = " + MyMain.ProjectId;
        //SqlStr += " and CFY = " + MyMain.Fyear;
        ////SqlStr += " and Vtypeid = '" + MyMain.Vtype + "'";
        //SqlStr += " and VNo = " + MyMain.VNo;

        //string Res = NonQryCmdSp(SqlStr);
        //return Res;
    }

    public string DeleteVoucherActtran()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "DELETE  ";
            SqlStr += " FROM ActTran ";
            SqlStr += " WHERE ";
            SqlStr += " Officeid = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            SqlStr += " and CFY = " + MyMain.Fld3;
            SqlStr += " and Jobno = " + MyMain.Fld4;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;


    }

    public string DeleteExpVoucherActtran()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "DELETE  ";
            SqlStr += " FROM ActTran ";
            SqlStr += " WHERE ";
            SqlStr += " Officeid = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            SqlStr += " and CFY = " + MyMain.Fld3;
            SqlStr += " and Jobno = " + MyMain.Fld4;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;


    }

    public string DeleteTranTblDtl()
    {
        SqlStr = "DELETE  ";
        SqlStr += " FROM ActTranDtl ";
        SqlStr += " WHERE ";
        SqlStr += " tranid = " + MyMain.Fld1;
        decimal Res = SqlHelper.ExecuteNonQuery(_SqlConnection, CommandType.Text, SqlStr);
        return Res.ToString();
    }

    public DataSet GetTranIdFromPurCosting()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM PurchaseCosting ";
        SqlStr += " WHERE ";
        SqlStr += " voucherno = " + MyMain.Fld1;
        SqlStr += " and cfy = " + MyMain.Fld2;
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }
    public DataSet Sp_ChkVnoInActTran()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pC1", MyMain.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", MyMain.Fld2, ParameterDirection.Input, SqlDbType.Decimal);
            //_SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_ChkVnoInActTran", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet Sp_GetSaleDataByAwbNo()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pC1", MyMain.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            //_SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetSaleDataByAWBNo", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public string DeleteFromPurCosting()
    {
        //Detail
        SqlStr = "DELETE  ";
        SqlStr += " FROM PurchaseCostingDtl ";
        SqlStr += " WHERE ";
        SqlStr += " PurchaseCostingId = " + MyMain.Fld1;
        decimal Res = SqlHelper.ExecuteNonQuery(_SqlConnection, CommandType.Text, SqlStr);

        SqlStr = "DELETE FROM ActTran WHERE LTRIM(CONVERT( VARCHAR(20),vno ))+LTRIM(CONVERT( VARCHAR(20),cfy )) = ";
        //SqlStr += " (SELECT LTRIM(CONVERT( VARCHAR(20),vtranid ))+LTRIM(CONVERT( VARCHAR(20),cfy )) ";
        SqlStr += " (SELECT LTRIM(CONVERT( VARCHAR(20),VoucherNo ))+LTRIM(CONVERT( VARCHAR(20),cfy )) ";
        SqlStr += "  from purchasecosting where  PurchaseCostingId = " + MyMain.Fld1 + ")";
        Res = SqlHelper.ExecuteNonQuery(_SqlConnection, CommandType.Text, SqlStr);

        //Header

        SqlStr = "DELETE  ";
        SqlStr += " FROM PurchaseCosting ";
        SqlStr += " WHERE ";
        SqlStr += " PurchaseCostingId = " + MyMain.Fld1;
        Res = SqlHelper.ExecuteNonQuery(_SqlConnection, CommandType.Text, SqlStr);



        return Res.ToString();
    }

    public string InsertTranTbl()
    {
        string retVal = string.Empty;
        try
        {


            SqlStr = "INSERT INTO ActTran";
            SqlStr += "(";
            SqlStr += " OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",CFY";
            SqlStr += ",VNo";
            SqlStr += ",ActCode";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.OfficeId;
            SqlStr += Comma + MyMain.ProjectId;
            SqlStr += Comma + MyMain.Fyear;
            SqlStr += Comma + UType.MyCtoDs(MyMain.VNo);
            SqlStr += Comma + UType.MyCtoDs(MyMain.ActCode);
            SqlStr += ")";

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertTranTblAuto()
    {
        string retVal = string.Empty;
        try
        {


            SqlStr = "INSERT INTO ActTran";
            SqlStr += "(";
            SqlStr += " OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",CFY";
            SqlStr += ",Job";
            SqlStr += ",JobSnoType";
            SqlStr += ",JobSno";
            SqlStr += ",Vno";
            SqlStr += ",Vauto";
            SqlStr += ",Discount";    //SqlStr += ",ActCode";
            SqlStr += ",Sno";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.OfficeId;
            //SqlStr += Comma + MyMain.ProjectId;
            // SqlStr += Comma + MyMain.Fyear;
            SqlStr += UType.MyCtoDs(MyMain.Fld1);
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld8);
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ")";

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertTranTblDtl()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "INSERT INTO [ActTranDetail]";
            SqlStr += "(";
            SqlStr += " vno";
            SqlStr += ",IsAttach";
            SqlStr += ",[VNoAttach] ";

            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld3);

            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";

            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();

            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertTranTblLog()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "INSERT INTO ActTranLog";
            SqlStr += "(";
            SqlStr += " OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",ActCode";
            SqlStr += ",VTypeid ";
            SqlStr += ",VNo";
            SqlStr += ",TranDate";
            SqlStr += ",SNo";
            SqlStr += ",Narration";
            SqlStr += ",Amount";
            SqlStr += ",Status";

            SqlStr += ",CFY";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ",ActivityId";
            SqlStr += ",ActivityReason";
            SqlStr += ",AddUser";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.OfficeId;
            SqlStr += Comma + MyMain.ProjectId;
            SqlStr += Comma + MyMain.ActCode;
            SqlStr += Comma + Sqote + MyMain.Vtype + Sqote;
            SqlStr += Comma + MyMain.VNo;
            SqlStr += Comma + MyMain.TransectionDate;
            SqlStr += Comma + MyMain.sNo;
            SqlStr += Comma + Sqote + MyMain.Narration + Sqote;
            SqlStr += Comma + MyMain.vAmount;
            SqlStr += Comma + Sqote + MyMain.vStatus + Sqote;
            SqlStr += Comma + MyMain.Fyear;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += Comma + MyMain.Fld10;
            SqlStr += Comma + Sqote + MyMain.Fld11 + Sqote;
            SqlStr += Comma + Sqote + MyMain.Fld12 + Sqote;
            SqlStr += ")";

            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateTranTbl()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "Update actTran Set ";
            SqlStr += "OfficeId=" + MyMain.OfficeId;
            SqlStr += " ,ProjectId=" + MyMain.ProjectId;
            SqlStr += " ,CFY=" + MyMain.Fyear;
            SqlStr += " ,VTypeid=" + UType.MyCtoDs(MyMain.Vtype);
            SqlStr += " ,TranDate=" + MyMain.TransectionDate;
            SqlStr += " ,JobNo =" + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += " ,InvoiceDate=" + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += " ,GST =" + Sqote + MyMain.Fld4 + Sqote;
            SqlStr += " ,DueDate =" + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += " ,Currency =" + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += " ,ExchangeRate =" + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += " ,ChqNo=" + Sqote + MyMain.Fld8 + Sqote;
            SqlStr += " ,ChqDate=" + UType.MyCtoDs(MyMain.Fld9);


            SqlStr += " ,SNo=" + UType.MyCtoDs(MyMain.sNo);
            SqlStr += " ,ActCode =" + UType.MyCtoDs(MyMain.ActCode);
            SqlStr += " ,Amount=" + UType.MyCtoDs(MyMain.vAmount);
            SqlStr += " ,Status=" + Sqote + MyMain.vStatus + Sqote;
            SqlStr += " ,Narration=" + Sqote + MyMain.Narration + Sqote;

            SqlStr += " ,UpdateDate=" + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += " ,UpdateTime=" + DateTime.Now.ToString("HHmmss");
            SqlStr += " ,UpdateUser=" + Sqote + MyMain.LoginId + Sqote;
            SqlStr += " ,transtatus=" + UType.MyCtoDs(MyMain.Fld50);
            SqlStr += " ,vauto=" + UType.MyCtoDs(MyMain.Fld10);
            // SqlStr += " ,InvoiceNo=" + Sqote + MyMain.Fld26 + Sqote;
            // SqlStr += " ,Purchaseorder=" + Sqote + MyMain.Fld27 + Sqote;
            SqlStr += " Where ";
            SqlStr += " tranid =" + MyMain.TranId;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateTranTblAuto()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "Update actTran Set ";
            //SqlStr += "OfficeId=" + MyMain.OfficeId;
            // SqlStr += " ,ProjectId=" + MyMain.ProjectId;
            // SqlStr += " ,CFY=" + MyMain.Fyear;
            SqlStr += " VTypeid=" + UType.MyCtoDs(MyMain.Vtype);
            SqlStr += " ,TranDate=" + MyMain.TransectionDate;
            SqlStr += " ,Job =" + MyMain.Fld2;
            //SqlStr += " ,Jobsno =" + MyMain.Fld3;
            SqlStr += " ,JobsnoType =" + MyMain.Fld4;
            SqlStr += " ,ActCode =" + UType.MyCtoDs(MyMain.ActCode);
            SqlStr += " ,Amount=" + UType.MyCtoDs(MyMain.vAmount);
            SqlStr += " ,Status=" + Sqote + MyMain.vStatus + Sqote;
            SqlStr += " ,Narration=" + Sqote + MyMain.Narration + Sqote;
            // SqlStr += " ,SNo=" + MyMain.Fld5;
            SqlStr += " ,Currency=" + MyMain.Fld6;





            SqlStr += " ,UpdateDate=" + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += " ,UpdateTime=" + DateTime.Now.ToString("HHmmss");
            SqlStr += " ,UpdateUser=" + Sqote + MyMain.LoginId + Sqote;
            SqlStr += " ,jobno=" + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += " ,ExpenseDisplay=" + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += " ,ExpenseReceive=" + UType.MyCtoDs(MyMain.Fld17);
            // SqlStr += " ,Purchaseorder=" + Sqote + MyMain.Fld27 + Sqote;
            SqlStr += " Where ";
            // SqlStr += " tranid =" + MyMain.TranId;
            SqlStr += " officeid =" + MyMain.Fld10;
            SqlStr += " and projectid =" + MyMain.Fld11;
            SqlStr += " and Cfy =" + MyMain.Fld12;
            SqlStr += " and vno =" + MyMain.Fld13;
            SqlStr += " and jobsno  =" + MyMain.Fld14;
            SqlStr += " and sno  =" + MyMain.Fld5;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet GetMaxVno()
    {
        SqlStr = "SELECT max(vno) ";
        SqlStr += " FROM TranTbl ";
        SqlStr += " where ";
        SqlStr += " OfficeId = " + MyMain.OfficeId;
        SqlStr += " and Fyear = " + MyMain.Fyear;
        //SqlStr += " and Vtype = '" + MyMain.Vtype + "'";

        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }
    public string UpdateActTranId()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "Update PurchaseCosting Set ";
            SqlStr += "vTranId =" + MyMain.Fld1;

            SqlStr += "Where ";
            SqlStr += " PurchaseCostingId =" + MyMain.Fld2;

            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateActTranIdSale()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "Update SaleCosting Set ";
            SqlStr += "vTranId =" + MyMain.Fld1;

            SqlStr += "Where ";
            SqlStr += " SaleCostingId =" + MyMain.Fld2;

            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetProofListAllAccounts()
    {
        SqlStr = "SELECT ac.actcode,ac.actdesc,ac.actcode1 FROM actchart ac   ";
        SqlStr += " INNER  JOIN RefTbl AS rt ON ac.OfficeId = rt.officeid AND ac.ProjectId = rt.projectid ";
        SqlStr += " inner join ActTran at on oc.officeid=at.officeid and oc.projectid=at.projectid ";
        //SqlStr += " INNER  JOIN actchart ac ON oc.actcode = ac.actcode ";
        SqlStr += " WHERE ac.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND ac.projectid = " + MyMain.ProjectId;
        //SqlStr += " and at.CFY = " + MyMain.Fyear;
        // SqlStr += " and at.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and at.trandate >= rt.sdfyear ";
        SqlStr += " and at.trandate < " + MyMain.Fld4;
        SqlStr += " GROUP BY ac.ActCode,ac.actdesc,ac.actcode1 ";
        SqlStr += " ORDER BY  ac.ActCode1,ac.actdesc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetProofListDataOpen()
    {
        SqlStr = "SELECT at.actcode,ac.actdesc, SUM(at.amount) AS 'AmountSum', at.[Status] FROM ActTran AS at  ";
        SqlStr += " INNER  JOIN RefTbl AS rt ON at.OfficeId = rt.officeid AND at.ProjectId = rt.projectid ";
        SqlStr += " INNER  JOIN actchart AS ac ON at.OfficeId = ac.officeid AND at.ProjectId = ac.projectid and at.actcode = ac.actcode";
        SqlStr += " WHERE at.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND at.projectid = " + MyMain.ProjectId;
        SqlStr += " and at.CFY = " + MyMain.Fyear;
        // SqlStr += " and at.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and at.trandate >= rt.sdfyear ";
        SqlStr += " and at.trandate < " + MyMain.Fld1;
        SqlStr += " GROUP BY at.ActCode,ac.actdesc, at.[Status] ";
        SqlStr += " ORDER BY  at.ActCode,ac.actdesc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetProofListData()
    {
        SqlStr = "SELECT  ";
        SqlStr += " TranId ";
        SqlStr += ", 10 as ActCode ";
        SqlStr += ", a.Actcode as ActCode1 ";
        SqlStr += ", b.ActDesc as ActName ";
        SqlStr += ", a.unitrate as unitrate ";
        SqlStr += ", a.quantity as quantity ";
        SqlStr += ", a.Amount as Amount ";
        SqlStr += ", a.Status as AmountSts ";
        SqlStr += ", a.Narration as Narration ";
        SqlStr += ", a.trandate ";
        SqlStr += ", a.chqno ";
        SqlStr += ", a.chqdate ";
        SqlStr += ", a.vtypeid ";
        SqlStr += ", a.officeid ";
        SqlStr += ", a.goodsfrom ";
        SqlStr += ", a.commision";
        SqlStr += ", a.discount ";
        SqlStr += ", a.DeliveryOrder ";
        SqlStr += ", a.InvoiceNo ";
        SqlStr += ", a.PurchaseOrder ";
        SqlStr += ", a.vno ";
        SqlStr += ", c.VtypeDescription ";
        SqlStr += ", c.Vtype ";

        //
        SqlStr += " FROM ActTran a";
        SqlStr += " inner join actchart b on ";
        SqlStr += " a.Officeid = b.Officeid and a.projectid = b.projectid and a.actcode = b.actcode ";
        SqlStr += " inner join ActRfVtype c on ";
        SqlStr += " a.VtypeId = c.VtypeId ";
        SqlStr += " where ";
        SqlStr += " a.OfficeId = " + MyMain.OfficeId;
        SqlStr += " and a.projectid = " + MyMain.ProjectId;
        SqlStr += " and a.CFY = " + MyMain.Fyear;
        // SqlStr += " and at.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and a.trandate >= " + MyMain.Fld1;
        SqlStr += " and a.trandate <= " + MyMain.Fld2;
        SqlStr += " order by a.trandate,a.vno,a.tranid ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }


    public DataSet GetTransectionData()
    {
        SqlStr = "SELECT  ";
        SqlStr += " TranId ";
        SqlStr += ", 1 as ActCode ";
        SqlStr += ", a.Actcode as ActCode1 ";
        SqlStr += ", z.ActDesc as ActName ";
        SqlStr += ", a.unitrate as unitrate ";
        SqlStr += ", a.quantity as quantity ";
        SqlStr += ", a.Amount as Amount ";
        SqlStr += ", a.Status as AmountSts ";
        SqlStr += ", a.Narration as Narration ";
        SqlStr += ", a.trandate ";
        SqlStr += ", a.chqno ";
        SqlStr += ", a.chqdate ";
        SqlStr += ", a.vtypeid ";
        SqlStr += ", a.officeid ";
        SqlStr += ", a.goodsfrom ";
        SqlStr += ", a.commision";
        SqlStr += ", a.discount ";
        SqlStr += ", a.DeliveryOrder ";
        SqlStr += ", a.InvoiceNo ";
        SqlStr += ", a.PurchaseOrder ";
        SqlStr += ", a.vno ";
        SqlStr += ", c.VtypeDescription,c.VType ";
        SqlStr += ", a.sno,a.transtatus ";
        SqlStr += ", a.jobno,currency,duedate,gst,invoicedate,vauto,transtatus ";
        //
        SqlStr += " FROM ActTran a";
        //SqlStr += " inner join officechart b on "; 
        //SqlStr += " a.Officeid = b.Officeid and a.projectid = b.projectid and a.actcode = b.actcode ";
        SqlStr += " inner join ActRfVtype c on ";
        //SqlStr += " a.Officeid = c.Officeid and a.projectid = c.projectid and a.VtypeId = c.VtypeId ";
        SqlStr += "  a.VtypeId = c.VtypeId ";
        //SqlStr += " a.VtypeId = c.VtypeId ";
        SqlStr += " inner join actchart z on a.Officeid = z.Officeid and a.projectid = z.projectid and a.actcode = z.actcode ";
        SqlStr += " where ";
        SqlStr += " a.OfficeId = " + MyMain.OfficeId;
        SqlStr += " and a.projectid = " + MyMain.ProjectId;
        SqlStr += " and a.CFY = " + MyMain.Fyear;
        //SqlStr += " and a.Vtypeid = " + MyMain.Vtype;
        SqlStr += " and a.Vno = " + MyMain.VNo;
        SqlStr += " order by tranid ";

        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetLedgerDataOpen()
    {
        SqlStr = "SELECT actcode, at.amount, at.[Status] FROM ActTran AS at  ";
        SqlStr += " INNER  JOIN RefTbl AS rt ON at.OfficeId = rt.officeid AND at.ProjectId = rt.projectid ";
        SqlStr += " WHERE at.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND at.projectid = " + MyMain.ProjectId;
        SqlStr += " and at.CFY = " + MyMain.Fyear;
        SqlStr += " and at.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and at.trandate >= rt.sdfyear ";
        SqlStr += " and at.trandate < " + MyMain.Fld3;
        SqlStr += " GROUP BY at.ActCode,at.amount, at.[Status] ";
        SqlStr += " ORDER BY  at.ActCode ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetLedgerData()
    {
        SqlStr = "SELECT  ";
        SqlStr += " TranId ";
        SqlStr += ", 10 as ActCode ";
        SqlStr += ", a.Actcode as ActCode1 ";
        SqlStr += ", b.ActDesc as ActName ";
        SqlStr += ", a.unitrate as unitrate ";
        SqlStr += ", a.quantity as quantity ";
        SqlStr += ", a.Amount as Amount ";
        SqlStr += ", a.Status as AmountSts ";
        SqlStr += ", a.Narration as Narration ";
        SqlStr += ", a.trandate ";
        SqlStr += ", a.chqno ";
        SqlStr += ", a.chqdate ";
        SqlStr += ", a.vtypeid ";
        SqlStr += ", a.officeid ";
        SqlStr += ", a.goodsfrom ";
        SqlStr += ", a.commision";
        SqlStr += ", a.discount ";
        SqlStr += ", a.DeliveryOrder ";
        SqlStr += ", a.InvoiceNo ";
        SqlStr += ", a.PurchaseOrder ";
        SqlStr += ", a.vno ";
        SqlStr += ", c.VtypeDescription ";
        SqlStr += ", a.ExpenseDisplay ";
        SqlStr += ", a.ExpenseReceive ";
        SqlStr += ", c.Vtype ";
        SqlStr += ", d.City_short ";
        //
        SqlStr += " FROM ActTran a";
        SqlStr += " inner join actchart b on ";
        SqlStr += " a.Officeid = b.Officeid and a.projectid = b.projectid and a.actcode = b.actcode ";
        SqlStr += " inner join ActRfVtype c on ";
        SqlStr += "  a.VtypeId = c.VtypeId ";

        SqlStr += " left join rfcities d on ";
        SqlStr += " b.cityid = d.city_id ";
        SqlStr += " where ";
        SqlStr += " a.OfficeId = " + MyMain.OfficeId;
        SqlStr += " and a.projectid = " + MyMain.ProjectId;
        SqlStr += " and a.CFY = " + MyMain.Fyear;
        SqlStr += " and a.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and a.trandate >= " + MyMain.Fld3;
        SqlStr += " and a.trandate <= " + MyMain.Fld4;
        SqlStr += " order by a.trandate ";
        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetLedgerDataSOA()
    {

        SqlStr = "select a.principalcode,b.customername as ActDesc, c.actdesc as Expense,a.ExpenseDisplay, a.ExpenseReceive, a.ExpenseDisplay - a.ExpenseReceive as ProfitLoss  ";
        SqlStr += " ,a.ChequeNo,a.ChequeDate ,a.JobNo,a.JobCy,a.Vno,a.VnoCFY,a.adddate from charges a ";
        SqlStr += " inner join RfCustomer b on a.Officeid = b.Officeid and a.projectid = b.projectid and a.principalcode = b.customerid ";
        SqlStr += " inner join actchart c on a.Officeid = c.Officeid and a.projectid = c.projectid and a.particular = c.actcode ";


        SqlStr += " where ";
        SqlStr += " a.OfficeId = " + MyMain.OfficeId;
        SqlStr += " and a.projectid = " + MyMain.ProjectId;
        SqlStr += " and a.PrincipalCode = " + MyMain.Fld1;
        //SqlStr += " and a.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and a.adddate >= " + MyMain.Fld3;
        SqlStr += " and a.adddate <= " + MyMain.Fld4;
        // SqlStr += " order by a.trandate ";
        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetTrialAllAccounts()
    {
        SqlStr = " select b.officeid,b.projectid    ";
        SqlStr += " ,SUBSTRING(LTRIM(str(b.ActCode)),1,1) as ParentCode,c.ActDesc as ParentDesc ";
        SqlStr += " ,SUBSTRING(LTRIM(str(b.ActCode)),1,3) as ParentCode1,d.ActDesc as ParentDesc1 ";
        SqlStr += " ,SUBSTRING(LTRIM(str(b.ActCode)),1,6) as ParentCode2,e.ActDesc as ParentDesc2";
        SqlStr += " ,b.ActCode,b.actdesc,b.actcode1";
        SqlStr += " ,a.lsbsheet,a.Rsbsheet,a.lsistat,a.Rsistat    from RefTbl a ";
        SqlStr += " inner join actchart b on a.OfficeId= b.OfficeId and a.ProjectId = b.ProjectId  ";
        SqlStr += " inner join actchart c on b.OfficeId = c.OfficeId and b.ProjectId = c.ProjectId";
        SqlStr += " and SUBSTRING(LTRIM(str(b.ActCode)),1,1) = c.ActCode   ";
        SqlStr += " inner join actchart d on b.OfficeId = d.OfficeId and b.ProjectId = d.ProjectId ";
        SqlStr += " and SUBSTRING(LTRIM(str(b.ActCode)),1,3) = d.ActCode ";
        SqlStr += " inner join actchart e on b.OfficeId = e.OfficeId and b.ProjectId = e.ProjectId";
        SqlStr += " and SUBSTRING(LTRIM(str(b.ActCode)),1,6) = e.ActCode";

        SqlStr += " WHERE a.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND a.projectid = " + MyMain.ProjectId;
        SqlStr += " and a.CFY = " + MyMain.Fyear;
        SqlStr += "  GROUP BY   b.officeid,b.projectid ";
        SqlStr += "  ,SUBSTRING(LTRIM(str(b.ActCode)),1,1)";
        SqlStr += "  ,SUBSTRING(LTRIM(str(b.ActCode)),1,3) ";
        SqlStr += "  ,SUBSTRING(LTRIM(str(b.ActCode)),1,6)";
        SqlStr += "  ,c.ActDesc ,d.ActDesc ,e.ActDesc ,b.ActCode,b.actdesc,b.actcode1 ";
        SqlStr += "  ,a.lsbsheet,a.Rsbsheet,a.lsistat,a.Rsistat  ";
        SqlStr += "  ORDER BY  b.ActCode1";
        //DataSet ds = GetDatasetSpTrial(SqlStr); //GetDatasetSpNew(SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetTrialDataOpen()
    {
        SqlStr = "SELECT distinct at.actcode,ac.actdesc ";
        SqlStr += " ,at.VNo,at.amount, at.[Status] FROM   ";
        SqlStr += " ActTran AS at   INNER  JOIN RefTbl AS rt ON at.OfficeId = rt.officeid  ";
        SqlStr += " AND at.ProjectId = rt.projectid  ";
        SqlStr += " INNER  JOIN actchart AS ac ON at.OfficeId = ac.officeid ";
        SqlStr += " AND at.ProjectId = ac.projectid and at.actcode=ac.actcode ";

        SqlStr += " WHERE at.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND at.projectid = " + MyMain.ProjectId;
        SqlStr += " and at.CFY = " + MyMain.Fyear;
        // SqlStr += " and at.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and at.trandate >= rt.sdfyear ";
        SqlStr += " and at.trandate < " + MyMain.Fld3;
        SqlStr += " ORDER BY  at.ActCode,ac.actdesc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetTrialDataBk10192021()
    {
        SqlStr = "SELECT at.actcode,ac.actdesc, SUM(at.amount) AS 'Amount', at.[Status] FROM ActTran AS at  ";
        SqlStr += " INNER  JOIN RefTbl AS rt ON at.OfficeId = rt.officeid AND at.ProjectId = rt.projectid ";
        SqlStr += " INNER  JOIN actchart AS ac ON at.OfficeId = ac.officeid AND at.ProjectId = ac.projectid and at.actcode=ac.actcode ";
        SqlStr += " WHERE at.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND at.projectid = " + MyMain.ProjectId;
        SqlStr += " and at.CFY = " + MyMain.Fyear;
        // SqlStr += " and at.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and at.trandate >= " + MyMain.Fld3;
        SqlStr += " and at.trandate <= " + MyMain.Fld4;
        SqlStr += " GROUP BY at.ActCode,ac.actdesc, at.[Status] ";
        SqlStr += " ORDER BY  at.ActCode,ac.actdesc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetTrialData()
    {
        SqlStr = "SELECT distinct at.actcode,ac.actdesc ";
        SqlStr += " ,at.VNo,at.amount, at.[Status] FROM   ";
        SqlStr += " ActTran AS at   INNER  JOIN RefTbl AS rt ON at.OfficeId = rt.officeid  ";
        SqlStr += " AND at.ProjectId = rt.projectid  ";
        SqlStr += " INNER  JOIN actchart AS ac ON at.OfficeId = ac.officeid ";
        SqlStr += " AND at.ProjectId = ac.projectid and at.actcode=ac.actcode ";
        SqlStr += " WHERE at.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND at.projectid = " + MyMain.ProjectId;
        SqlStr += " and at.CFY = " + MyMain.Fyear;
        SqlStr += " and at.trandate >= " + MyMain.Fld3;
        SqlStr += " and at.trandate <= " + MyMain.Fld4;

        SqlStr += " ORDER BY  at.ActCode,at.status";

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    //Income
    public DataSet GetIncomeAllAccounts()
    {
        SqlStr = " select b.officeid,b.projectid    ";
        SqlStr += " ,SUBSTRING(LTRIM(str(b.ActCode)),1,1) as ParentCode,c.ActDesc as ParentDesc ";
        SqlStr += " ,SUBSTRING(LTRIM(str(b.ActCode)),1,3) as ParentCode1,d.ActDesc as ParentDesc1 ";
        SqlStr += " ,SUBSTRING(LTRIM(str(b.ActCode)),1,6) as ParentCode2,e.ActDesc as ParentDesc2";
        SqlStr += " ,b.ActCode,b.actdesc,b.actcode1";
        SqlStr += " ,a.lsbsheet,a.Rsbsheet,a.lsistat,a.Rsistat    from RefTbl a ";
        SqlStr += " inner join actchart b on a.OfficeId= b.OfficeId and a.ProjectId = b.ProjectId  ";
        SqlStr += " inner join actchart c on b.OfficeId = c.OfficeId and b.ProjectId = c.ProjectId";
        SqlStr += " and SUBSTRING(LTRIM(str(b.ActCode)),1,1) = c.ActCode   ";
        SqlStr += " inner join actchart d on b.OfficeId = d.OfficeId and b.ProjectId = d.ProjectId ";
        SqlStr += " and SUBSTRING(LTRIM(str(b.ActCode)),1,3) = d.ActCode ";
        SqlStr += " inner join actchart e on b.OfficeId = e.OfficeId and b.ProjectId = e.ProjectId";
        SqlStr += " and SUBSTRING(LTRIM(str(b.ActCode)),1,6) = e.ActCode";

        SqlStr += " WHERE a.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND a.projectid = " + MyMain.ProjectId;
        SqlStr += " and a.CFY = " + MyMain.Fyear;
        SqlStr += "  GROUP BY   b.officeid,b.projectid ";
        SqlStr += "  ,SUBSTRING(LTRIM(str(b.ActCode)),1,1)";
        SqlStr += "  ,SUBSTRING(LTRIM(str(b.ActCode)),1,3) ";
        SqlStr += "  ,SUBSTRING(LTRIM(str(b.ActCode)),1,6)";
        SqlStr += "  ,c.ActDesc ,d.ActDesc ,e.ActDesc ,b.ActCode,b.actdesc,b.actcode1 ";
        SqlStr += "  ,a.lsbsheet,a.Rsbsheet,a.lsistat,a.Rsistat  ";
        SqlStr += "  ORDER BY  b.ActCode1";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetIncomeDataOpen()
    {
        SqlStr = "SELECT distinct at.actcode,ac.actdesc ";
        SqlStr += " ,at.VNo,at.amount, at.[Status] FROM   ";
        SqlStr += " ActTran AS at   INNER  JOIN RefTbl AS rt ON at.OfficeId = rt.officeid  ";
        SqlStr += " AND at.ProjectId = rt.projectid  ";
        SqlStr += " INNER  JOIN actchart AS ac ON at.OfficeId = ac.officeid ";
        SqlStr += " AND at.ProjectId = ac.projectid and at.actcode=ac.actcode ";

        SqlStr += " WHERE at.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND at.projectid = " + MyMain.ProjectId;
        SqlStr += " and at.CFY = " + MyMain.Fyear;
        // SqlStr += " and at.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and at.trandate >= rt.sdfyear ";
        SqlStr += " and at.trandate < " + MyMain.Fld3;
        SqlStr += " ORDER BY  at.ActCode,ac.actdesc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetIncomeData()
    {
        SqlStr = "SELECT at.actcode,ac.actdesc, SUM(at.amount) AS 'Amount', at.[Status] FROM ActTran AS at  ";
        SqlStr += " INNER  JOIN RefTbl AS rt ON at.OfficeId = rt.officeid AND at.ProjectId = rt.projectid ";
        SqlStr += " INNER  JOIN actchart AS ac ON at.OfficeId = ac.officeid AND at.ProjectId = ac.projectid and at.actcode=ac.actcode ";
        SqlStr += " WHERE at.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND at.projectid = " + MyMain.ProjectId;
        SqlStr += " and at.CFY = " + MyMain.Fyear;
        // SqlStr += " and at.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and at.trandate >= " + MyMain.Fld3;
        SqlStr += " and at.trandate <= " + MyMain.Fld4;
        SqlStr += " GROUP BY at.ActCode,ac.actdesc, at.[Status] ";
        SqlStr += " ORDER BY  at.ActCode,ac.actdesc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    //en d

    //Balance
    public DataSet GetBalanceAllAccounts()
    {
        SqlStr = "SELECT rt.lsbsheet,rt.rsbsheet,ac.actcode,ac.actdesc,ac.actcode1 FROM ActTran AS at  ";
        SqlStr += " INNER  JOIN RefTbl AS rt ON at.OfficeId = rt.officeid AND at.ProjectId = rt.projectid ";
        SqlStr += " INNER  JOIN actchart AS ac ON at.OfficeId = ac.officeid AND at.ProjectId = ac.projectid ";
        SqlStr += " WHERE at.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND at.projectid = " + MyMain.ProjectId;
        SqlStr += " and at.CFY = " + MyMain.Fyear;
        // SqlStr += " and at.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and at.trandate >= rt.sdfyear ";
        SqlStr += " and at.trandate < " + MyMain.Fld4;
        SqlStr += " GROUP BY rt.lsbsheet,rt.rsbsheet,ac.ActCode,ac.actdesc,ac.actcode1 ";
        SqlStr += " ORDER BY  ac.ActCode1,ac.actdesc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetBalanceDataOpen()
    {
        SqlStr = "SELECT at.actcode,ac.actdesc, SUM(at.amount) AS 'AmountSum', at.[Status] FROM ActTran AS at  ";
        SqlStr += " INNER  JOIN RefTbl AS rt ON at.OfficeId = rt.officeid AND at.ProjectId = rt.projectid ";
        SqlStr += " INNER  JOIN actchart AS ac ON at.OfficeId = ac.officeid AND at.ProjectId = ac.projectid and at.actcode = ac.actcode";
        SqlStr += " WHERE at.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND at.projectid = " + MyMain.ProjectId;
        SqlStr += " and at.CFY = " + MyMain.Fyear;
        // SqlStr += " and at.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and at.trandate >= rt.sdfyear ";
        SqlStr += " and at.trandate < " + MyMain.Fld3;
        SqlStr += " GROUP BY at.ActCode,ac.actdesc, at.[Status] ";
        SqlStr += " ORDER BY  at.ActCode,ac.actdesc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetBalanceData()
    {
        SqlStr = "SELECT at.actcode,ac.actdesc, SUM(at.amount) AS 'Amount', at.[Status] FROM ActTran AS at  ";
        SqlStr += " INNER  JOIN RefTbl AS rt ON at.OfficeId = rt.officeid AND at.ProjectId = rt.projectid ";
        SqlStr += " INNER  JOIN actchart AS ac ON at.OfficeId = ac.officeid AND at.ProjectId = ac.projectid and at.actcode=ac.actcode ";
        SqlStr += " WHERE at.OfficeId = " + MyMain.OfficeId;
        SqlStr += " AND at.projectid = " + MyMain.ProjectId;
        SqlStr += " and at.CFY = " + MyMain.Fyear;
        // SqlStr += " and at.actcode  between " + MyMain.Fld1 + " and " + MyMain.Fld2;
        SqlStr += " and at.trandate >= " + MyMain.Fld3;
        SqlStr += " and at.trandate <= " + MyMain.Fld4;
        SqlStr += " GROUP BY at.ActCode,ac.actdesc, at.[Status] ";
        SqlStr += " ORDER BY  at.ActCode,ac.actdesc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    //en d
    public DataSet GetGridvNumber()
    {
        SqlStr = "SELECT  ";
        //SqlStr += " TranId ";
        SqlStr += " a.vno as Voucher_No,c.jobno,c.customer ";
        SqlStr += ", a.vtypeid as vTypeID ";
        SqlStr += ", a.Narration as Narration ";
        SqlStr += ", a.trandate ";

        SqlStr += " FROM ActTran a";
        SqlStr += " inner join actchart b on ";
        //SqlStr += " inner join officechart b on ";
        SqlStr += " a.Officeid = b.Officeid and a.projectid = b.projectid and a.actcode = b.actcode ";
        SqlStr += " left join Charges c on ";
        SqlStr += " a.Officeid = c.Officeid and a.projectid = c.projectid and a.Vno = c.Vno ";
        SqlStr += " where ";
        SqlStr += " a.OfficeId = " + MyMain.OfficeId;
        SqlStr += " and a.projectid = " + MyMain.ProjectId;
        SqlStr += " and a.CFY = " + MyMain.Fyear;
        // SqlStr += " and a.vno <> " + MyMain.VNo;
        SqlStr += " group by a.vno, a.vtypeid,a.narration,a.trandate ";
        SqlStr += " order by a.vno desc ";


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetItemDataOpenBk()
    {
        SqlStr = "select a.VTypeId,b.itemid,c.productdescription,a.TranDate, a.narration  ";
        SqlStr += " ,a.actcode, e.actdesc ";
        SqlStr += " ,b.*,f.* from acttran a ";
        SqlStr += " inner join acttrandtl b on a.tranid = b.tranid  ";
        SqlStr += " inner join RfProduct c on a.officeid= c.officeid and a.projectid= c.projectid and b.itemid= c.productid ";
        SqlStr += " inner join reftbl d on a.officeid= d.officeid and a.projectid= d.projectid and a.cfy = d.cfy  ";
        SqlStr += " inner join actchart e on a.officeid=e.officeid and a.projectid=e.projectid and a.actcode=e.actcode ";
        SqlStr += " inner join rfuom f on c.UomId = f.uomid";
        SqlStr += " WHERE ";
        SqlStr += " a.TranDate >= d.SDFyear  ";
        SqlStr += " and a.trandate < " + MyMain.Fld3;
        SqlStr += " and c.officeid = " + MyMain.OfficeId;
        SqlStr += " and c.projectid = " + MyMain.ProjectId;

        SqlStr += " ORDER BY a.TranDate,c.ProductDescription ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetItemDataOpen()
    {
        SqlStr = "select a.* from ";
        SqlStr += " reftbl a ";
        SqlStr += " WHERE ";
        SqlStr += " a.officeid = " + MyMain.OfficeId;
        SqlStr += " and a.projectid = " + MyMain.ProjectId;
        SqlStr += " and a.cfy = " + MyMain.Fyear;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetItemData()
    {
        SqlStr = "select a.VTypeId,b.itemid,c.productdescription,a.TranDate , a.narration ";
        SqlStr += " ,a.actcode,e.actdesc ";
        SqlStr += " ,b.*,f.*,c.* from acttran a ";
        SqlStr += " inner join acttrandtl b on a.tranid = b.tranid  ";
        SqlStr += " inner join RfProduct c on a.officeid= c.officeid and a.projectid= c.projectid and b.itemid= c.productid ";
        //SqlStr += " inner join reftbl d on a.officeid= d.officeid and a.projectid= d.projectid and a.cfy = d.cfy  ";
        SqlStr += " inner join actchart e on a.officeid=e.officeid and a.projectid=e.projectid  and a.actcode=e.actcode ";
        SqlStr += " inner join rfuom f on c.UomId = f.uomid";
        SqlStr += " WHERE ";
        SqlStr += "  a.officeid = " + MyMain.OfficeId;
        SqlStr += " and a.projectid = " + MyMain.ProjectId;
        //SqlStr += " and a.cfy = " + MyMain.Fyear;
        //SqlStr += " and a.TranDate >= " + MyMain.Fld3;
        //SqlStr += " and a.trandate <= " + MyMain.Fld4;


        //SqlStr += " ORDER BY a.TranDate,c.ProductDescription ";
        SqlStr += " ORDER BY c.ProductDescription,a.TranDate ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetTransectionDataDtlAttached()
    {
        SqlStr = "select a.Jobno,a.chargestype,a.sno,b.actdesc as Expense, a.vno,a.quantity,a.rate,a.localamount    ";
        SqlStr += " ,1 as checkvno from charges a  ";
        SqlStr += " inner join actchart b on a.officeid=b.officeid and a.projectid = b.projectid and a.particular =b.actcode  ";
        SqlStr += " where a.vno = " + MyMain.Fld1;
        SqlStr += " and len(a.particular) > 0 ";
        // SqlStr = " and ";

        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetTransectionDataDtlAttached1()
    {
        SqlStr = "select a.Jobno,a.chargestype,a.sno,b.actdesc as Expense, a.vno,a.quantity,a.rate,a.localamount    ";
        SqlStr += " ,1 as checkvno from charges a  ";
        SqlStr += " inner join actchart b on a.officeid=b.officeid and a.projectid = b.projectid and a.particular =b.actcode  ";
        SqlStr += " where a.vnobank = " + MyMain.Fld1;
        SqlStr += " and len(a.particular) > 0 ";
        // SqlStr = " and ";

        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetTransectionDataDtlAttachedExp()
    {
        SqlStr = "select a.Jobno,a.chargestype,a.sno,b.actdesc as Expense, a.vno,a.quantity,a.rate,a.netamount    ";
        SqlStr += " ,1 as checkvno from Expcharges a  ";
        SqlStr += " inner join actchart b on a.officeid=b.officeid and a.projectid = b.projectid and a.particular =b.actcode  ";
        SqlStr += " where a.vno = " + MyMain.Fld1;
        SqlStr += " and len(a.particular) > 0 ";
        // SqlStr = " and ";

        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetNewAttached()
    {
        SqlStr = "select a.Jobno,a.chargestype,a.sno,b.actdesc as Expense, a.vno,a.quantity,a.rate,a.localamount  ";
        SqlStr += " ,c.mblno,hblno,vessel from charges a  ";
        SqlStr += " inner join actchart b on a.officeid=b.officeid and a.projectid = b.projectid and a.particular=b.actcode  ";
        //SqlStr += " inner join actchart b on a.officeid=b.officeid and a.projectid = b.projectid and a.customer=b.actcode  ";
        SqlStr += " inner join JobInfo c on a.officeid=c.officeid and a.projectid = c.projectid and a.JobNo=c.jobno  ";
        SqlStr += " where a.chargestype = " + MyMain.Fld1;
        SqlStr += " and a.customer = " + MyMain.Fld2;
        SqlStr += " and a.vno = 0 ";//  is null "; //= 0";
        SqlStr += " order by jobno ";

        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetNewAttachedDet()
    {
        SqlStr = "select a.Jobno,a.sno,a.ContainerNo,a.SecurityAmountReceive,a.SecurityAmountPaid,a.TotalDetention * a.exrate,a.WashingCharges,a.Damagecharges  ";
        SqlStr += " ,a.Demurrage ,c.mblno,hblno,vessel from equipment a   ";
        //SqlStr += " inner join actchart b on a.officeid=b.officeid and a.projectid = b.projectid and a.particular=b.actcode  ";

        SqlStr += " inner join JobInfo c on a.officeid=c.officeid and a.projectid = c.projectid and a.JobNo=c.jobno ";
        SqlStr += " where a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.projectid = " + MyMain.Fld2;
        SqlStr += " and c.Consignee = " + MyMain.Fld3;
        SqlStr += " and a.detvno = 0 ";//  is null "; //= 0";
        SqlStr += " order by a.jobno ";

        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetNewAttachedBank()
    {
        SqlStr = "select a.Jobno,a.chargestype,a.sno,b.actdesc as Expense, a.vno,a.quantity,a.rate,a.localamount  ";
        SqlStr += " ,c.mblno,hblno,vessel from charges a  ";
        SqlStr += " inner join actchart b on a.officeid=b.officeid and a.projectid = b.projectid and a.particular=b.actcode  ";
        //SqlStr += " inner join actchart b on a.officeid=b.officeid and a.projectid = b.projectid and a.customer=b.actcode  ";
        SqlStr += " inner join JobInfo c on a.officeid=c.officeid and a.projectid = c.projectid and a.JobNo=c.jobno  ";
        SqlStr += " where a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.Projectid = " + MyMain.Fld2;
        //SqlStr += " and a.chargestype = " + MyMain.Fld1;
        SqlStr += " and a.particular = " + MyMain.Fld3;
        SqlStr += " and a.vno > 0 ";//  is null "; //= 0";
        SqlStr += " and a.vnobank = 0 ";
        SqlStr += " order by jobno ";

        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetNewAttachedExp()
    {
        SqlStr = "select a.Jobno,a.chargestype,a.sno,b.actdesc as Expense, a.vno,a.quantity,a.rate,a.netamount  ";
        SqlStr += " ,c.mblno,hblno,vessel from Expcharges a  ";
        SqlStr += " inner join actchart b on a.officeid=b.officeid and a.projectid = b.projectid and a.particular=b.actcode  ";
        //SqlStr += " inner join actchart b on a.officeid=b.officeid and a.projectid = b.projectid and a.customer=b.actcode  ";
        SqlStr += " inner join bookInfo c on a.officeid=c.officeid and a.projectid = c.projectid and a.JobNo=c.jobno  ";
        SqlStr += " where a.chargestype = " + MyMain.Fld1;
        SqlStr += " and a.customer = " + MyMain.Fld2;
        SqlStr += " and a.vno = 0 ";//  is null "; //= 0";
        SqlStr += " order by jobno ";

        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetTransectionDataDtlAttachedbk()
    {
        SqlStr = "select distinct  a.VNo,a.Narration,b.IsAttach,b.VNoAttach  from acttran a  ";
        SqlStr += " inner join acttrandetail b on a.VNo = b.VNo  ";
        SqlStr += " where ";
        SqlStr += " a.VNo = " + MyMain.Fld1;

        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetTransectionDataDtl()
    {
        SqlStr = " select distinct  a.VNo,a.Narration,a.TranDate  from acttran a  ";
        SqlStr += " right  join acttrandetail b on a.officeid= b.officeid and a.projectid=b.projectid and a.vno = b.vno ";
        if (UType.MyCtoD(MyMain.Fld1) > 0 && UType.MyCtoD(MyMain.Fld2) > 0)
        {
            SqlStr += " where ";
            SqlStr += " a.jobno = " + MyMain.Fld1;
            SqlStr += " and a.vno <> " + MyMain.Fld2;
        }
        if (UType.MyCtoD(MyMain.Fld1) < 1 && UType.MyCtoD(MyMain.Fld2) > 0)
        {
            SqlStr += " where ";

            SqlStr += "  a.vno <> " + MyMain.Fld2;
        }
        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetOfficeNamebyOfficeId(string pOfficeId)
    {
        SqlStr = "SELECT OfficeDescription ";
        SqlStr += " FROM RfOffice ";
        SqlStr += " where Officeid =" + pOfficeId;
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }

    public DataSet VoucherPrintSp(string vType, string vNo)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pC1", vType, ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(vNo), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_PrintVoucher", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet VoucherPSp()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(MyMain.Fld1), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(MyMain.Fld2), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_PrintVoucherP", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet VoucherSSp()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(MyMain.Fld1), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(MyMain.Fld2), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_PrintVoucherS", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet LedgerPrintSp(string pActCode, string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(pActCode), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(pStartDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pC3", Convert.ToDecimal(pEndDate), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_PrintLedgerNew", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet LedgerPrintSpAll()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(MyMain.Fld1), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(MyMain.Fld2), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pC3", Convert.ToDecimal(MyMain.Fld3), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[3] = Connection.GetParam("@pC4", Convert.ToDecimal(MyMain.Fld4), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[4] = Connection.GetParam("@pC5", Convert.ToDecimal(MyMain.Fld5), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(Connection.SqlConnection, CommandType.StoredProcedure, "Sp_PrintLedgerAll", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet LedgerPrintSp(string pOfficeId, string pActCode, string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(pActCode), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(pStartDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pC3", Convert.ToDecimal(pEndDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[3] = Connection.GetParam("@pC4", Convert.ToDecimal(pOfficeId), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_PrintLedgerNewOffice", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }



    public DataSet ProofListSp(string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(pStartDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(pEndDate), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_ProofList", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet TrialBalanceSp(string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(pStartDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(pEndDate), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_TrialBalance", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet TrialBalanceSp(string pOfficeId, string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(pStartDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(pEndDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pC3", Convert.ToDecimal(pOfficeId), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_TrialBalanceOffice", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet IncomeStatementSp(string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(pStartDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(pEndDate), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_IncomeStatement", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet IncomeStatementSp(string pOfficeId, string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(pStartDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(pEndDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pC3", Convert.ToDecimal(pOfficeId), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_IncomeStatementOffice", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet IncStatementSp(string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlStr = " SELECT a.ActCode,a.ActCode1,a.ActDesc, SUM(b.Amount) as AmountTotal, a.FldLevel FROM ActChart a ";
            SqlStr += " left join ActTran b on a.ActCode =  SUBSTRING(LTRIM(STR(b.actcode)),1,LEN(a.actcode)) and LEN(b.ActCode)>7";
            SqlStr += " where b.TranDate >= " + pStartDate;
            SqlStr += " and b.TranDate <= " + pEndDate;
            SqlStr += " and a.reportid = 2 ";
            SqlStr += " group by a.ActCode,a.ActCode1, a.ActDesc,a.fldlevel";
            SqlStr += " order by a.ActCode1 ";
            ds1 = GetDatasetSpNew(SqlStr);
            return ds1;
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet BalStatementSp(string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlStr = " SELECT a.ActCode,a.ActCode1,a.ActDesc, SUM(b.Amount) as AmountTotal, a.FldLevel FROM ActChart a ";
            SqlStr += " left join ActTran b on a.ActCode =  SUBSTRING(LTRIM(STR(b.actcode)),1,LEN(a.actcode)) and LEN(b.ActCode)>7";
            SqlStr += " where b.TranDate >= " + pStartDate;
            SqlStr += " and b.TranDate <= " + pEndDate;
            SqlStr += " and a.reportid = 1 ";
            SqlStr += " group by a.ActCode,a.ActCode1, a.ActDesc,a.fldlevel";
            SqlStr += " order by a.ActCode1 ";
            ds1 = GetDatasetSpNew(SqlStr);
            return ds1;
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet BalStatementSp(string pOfficeId, string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlStr = " SELECT a.ActCode,a.ActCode1,a.ActDesc, SUM(b.Amount) as AmountTotal, a.FldLevel FROM ActChart a ";
            SqlStr += " left join ActTran b on a.ActCode =  SUBSTRING(LTRIM(STR(b.actcode)),1,LEN(a.actcode)) and LEN(b.ActCode)>7";
            SqlStr += " where b.TranDate >= " + pStartDate;
            SqlStr += " and b.TranDate <= " + pEndDate;
            SqlStr += " and a.reportid = 1 ";
            SqlStr += " group by a.ActCode,a.ActCode1, a.ActDesc,a.fldlevel";
            SqlStr += " order by a.ActCode1 ";
            ds1 = GetDatasetSpNew(SqlStr);
            return ds1;
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet BalanceSheetSp(string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(pStartDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(pEndDate), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_BalanceSheet", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet BalanceSheetSp(string pOfficeId, string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(pStartDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(pEndDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pC3", Convert.ToDecimal(pOfficeId), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_BalanceSheetOffice", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    #endregion

    public DataSet GeRefTbl()
    {
        SqlStr = "SELECT ro.OfficeId,ro.OfficeDescription";
        SqlStr += ",rp.ProjectId,rp.ProjectDescription";
        SqlStr += " FROM RefTbl rt ";
        SqlStr += "INNER JOIN RfOffice ro ON rt.OfficeId=ro.OfficeId ";
        SqlStr += "INNER JOIN RfProject rp ON rt.ProjectId=rp.ProjectId ";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    #region Employee
    public DataSet GetEmpInfo()
    {
        SqlStr = "SELECT b.*  ";
        SqlStr += " FROM RefTbl a,RfEmployee b ";
        SqlStr += " WHERE ";
        SqlStr += " a.OfficeId = b.officeid";
        SqlStr += " and a.ProjectId = b.projectid";

        DataSet Res = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public DataSet GetEmpInfo1()
    {
        SqlStr = "SELECT b.*  ";
        SqlStr += " FROM RefTbl a,RfEmployee b ";
        SqlStr += " WHERE ";
        SqlStr += " a.OfficeId = b.OfficeId ";
        SqlStr += " and a.ProjectId = b.ProjectId ";
        SqlStr += " and b.empid = " + MyMain.EmpId;

        DataSet Res = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public string InsertRfEmployee()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "INSERT INTO RfEmployee";
            SqlStr += "(";
            SqlStr += " OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",EmpId";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.OfficeId;
            SqlStr += Comma + MyMain.ProjectId;
            SqlStr += Comma + MyMain.EmpId;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";

            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateRfEmployee()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "Update RfEmployee Set ";
            SqlStr += "OfficeId = " + MyMain.OfficeId;
            SqlStr += ",ProjectId = " + MyMain.ProjectId;
            SqlStr += ",EmpId = " + MyMain.EmpId;
            SqlStr += ",EmpName = " + Sqote + MyMain.EmpName + Sqote;
            SqlStr += ",EmpAddress = " + Sqote + MyMain.EmpAddress + Sqote;
            SqlStr += ",EmpTel =" + Sqote + MyMain.EmpPhoneNo + Sqote;
            SqlStr += ",RecommendedEmpId  =" + MyMain.RecommendedId;
            SqlStr += ",ApprovedEmpId =" + MyMain.ApprovalId;
            SqlStr += ",DesignationId =" + MyMain.Fld1;
            SqlStr += ",DepartId =" + MyMain.Fld2;

            SqlStr += ",UpdateDate =" + MyMain.UpdateDate;
            SqlStr += ",UpdateTime =" + MyMain.UpdateTime;
            SqlStr += ",PayGroupId =" + MyMain.Fld3;
            SqlStr += ",EmpStatus =" + MyMain.Fld4;
            SqlStr += ",EmpDateOfJoining =" + UType.MyCtoD(MyMain.Fld5);
            SqlStr += ",EmpDateOfLeaving =" + UType.MyCtoD(MyMain.Fld6);
            SqlStr += ",JobTypeId =" + UType.MyCtoD(MyMain.Fld7);
            SqlStr += ",EmpAccountNo =" + Sqote + MyMain.Fld8 + Sqote;
            SqlStr += " Where OfficeId = " + MyMain.OfficeId;
            SqlStr += " and ProjectId = " + MyMain.ProjectId;
            SqlStr += " and empid = " + MyMain.EmpId;

            sqlCommand.CommandText = SqlStr;

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    #endregion

    #region Customer
    public DataSet GetCustomerInfo()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " from RfCustomer a ";
        if (UType.MyCtoD(MyMain.EmpId) > 0)
        {
            SqlStr += " WHERE ";
            SqlStr += " a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectID = " + MyMain.Fld3;
            SqlStr += " and a.customerid = " + MyMain.EmpId;
        }
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " WHERE ";
            SqlStr += " a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectID = " + MyMain.Fld3;
            // SqlStr += " and a.customerid = " + MyMain.EmpId;
            SqlStr += " and a.Statusid = " + MyMain.Fld1;
        }
        if (UType.MyCtoD(MyMain.Fld20) > 0)
        {
            SqlStr += " WHERE ";

            SqlStr += " a.customerid = " + MyMain.Fld20;
        }
        SqlStr += " order by a.sortno ";
        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    public DataSet GetCustomerInfoMax()
    {
        SqlStr = "SELECT max(a.customerid) as MaxCustomerID  ";
        SqlStr += " from RfCustomer a ";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    public DataSet GetCustomerInfoAll()
    {
        SqlStr = "SELECT *  ";
        SqlStr += " from RfCustomer ";
        SqlStr += " order by CustomerName ";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    public string InsertRfCustomer()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfCustomer";
            SqlStr += "(";
            SqlStr += "Officeid";
            SqlStr += ",Projectid";
            SqlStr += ",CustomerId";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.EmpId;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateRfCustomer()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update RfCustomer Set ";
            // SqlStr += " CustomerId = " + MyMain.EmpId;
            SqlStr += "CustomerName = " + Sqote + MyMain.EmpName + Sqote;
            SqlStr += ",StatusID = " + MyMain.Fld1;


            SqlStr += ",CustomerTel =" + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += ",CustomerTel1 =" + Sqote + MyMain.Fld3 + Sqote;
            SqlStr += ",CustomerAddress = " + Sqote + UType.Chk10(MyMain.Fld4) + Sqote;
            //SqlStr += ",Status  =" + Sqote + MyMain.RecommendedId + Sqote;

            SqlStr += ",Sortno = " + MyMain.Fld7;
            SqlStr += ",ISOCode = " + Sqote + MyMain.Fld8 + Sqote;
            SqlStr += " Where officeID = " + MyMain.Fld5;
            SqlStr += " and ProjectID = " + MyMain.Fld6;
            SqlStr += " and Customerid = " + MyMain.EmpId;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    #endregion

    #region Product
    public DataSet GetAllDed()
    {
        SqlStr = "SELECT a.*";
        SqlStr += " ,0 as ExpenseIdAmount ";
        SqlStr += " FROM RfAllDed a  ";
        SqlStr += " INNER JOIN RfAllDedSts b ON a.AllDedSts=b.AllDedStsId ";
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            // if (UType.MyCtoD(MyMain.Fld1) == 2)
            // {
            //     MyMain.Fld1 = "1";
            // }
            SqlStr += " where  a.AllDedfor = " + MyMain.Fld1;
        }

        SqlStr += " ORDER BY a.AllDedId ";

        DataSet Res = GetDataset(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;

    }

    public DataSet GetAllDedPrint()
    {
        SqlStr = "SELECT b.*";
        SqlStr += " ,0 as ExpenseIdAmount ";
        SqlStr += " FROM clearancehdr a  ";
        SqlStr += " INNER JOIN RfAllDed b ON a.billfor=b.AllDedFor ";
        SqlStr += " INNER JOIN RfAllDedSts c ON b.AllDedSts=c.AllDedStsId ";
        SqlStr += " where  a.billno = " + MyMain.Fld1;
        SqlStr += " and  a.billyear = " + MyMain.Fld2;

        SqlStr += " ORDER BY b.AllDedId ";

        DataSet Res = GetDataset(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;

    }


    public DataSet GetAllRefTbl(string TblName, string OrderFld)
    {
        SqlStr = "SELECT *  ";
        SqlStr += " from " + TblName;
        SqlStr += " ORDER BY ";
        SqlStr += OrderFld;

        DataSet Res = GetDataset(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;

    }

    public DataSet GetRefTblInfo(string TblName, string IdName)
    {
        SqlStr = "SELECT *  ";
        SqlStr += " from " + TblName;
        SqlStr += " WHERE ";
        SqlStr += IdName + " = " + MyMain.Fld1;
        DataSet Res = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }
    public DataSet GetRfProduct()
    {
        DataSet Res = null;
        SqlStr = "SELECT officeID,ProjectId,ProductID,Productdescription,productunitprice,ProductBalance,MRP,Tax,uomid  ";
        SqlStr += " from RfProduct ";
        SqlStr += " WHERE ";
        SqlStr += " OfficeId = " + MyMain.Fld1;
        SqlStr += " and ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {

            SqlStr += " and ProductId = " + MyMain.Fld3;
        }
        Res = GetDatasetSpNew(SqlStr);//SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public DataSet GetProductInfoAll()
    {
        SqlStr = "SELECT *  ";
        SqlStr += " from RfProduct ";
        SqlStr += " order by ProductDescription ";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    public string InsertRfProduct()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "INSERT INTO RfProduct";
            SqlStr += "(";
            SqlStr += " OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",ProductId";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            string Res11 = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateRfProduct()
    {
        string retVal = string.Empty;
        try
        {
            MyMain oMyMain = new MyMain();

            SqlStr = "Update RfProduct Set ";
            SqlStr += "ProductDescription = " + Sqote + MyMain.Fld4 + Sqote;
            SqlStr += ",ProductUnitPrice = " + UType.MyCtoD(MyMain.Fld5);
            SqlStr += ",ProductBalance =" + UType.MyCtoD(MyMain.Fld6);
            SqlStr += ",UomId =" + UType.MyCtoD(MyMain.Fld9);
            SqlStr += ",ProductTypeId =" + UType.MyCtoD(MyMain.Fld10);
            SqlStr += ",mrp =" + UType.MyCtoD(MyMain.Fld11);
            SqlStr += ",Tax =" + UType.MyCtoD(MyMain.Fld12);
            SqlStr += " Where ";
            SqlStr += " Officeid = " + MyMain.Fld1;
            SqlStr += " and Projectid = " + MyMain.Fld2;
            SqlStr += " and Productid = " + MyMain.Fld3;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    #endregion

    #region CustomerHdr
    public DataSet GetCustomerHdrInfo()
    {
        SqlStr = "SELECT a.*  ";
        SqlStr += " from CustomerHdr a ";
        SqlStr += " WHERE ";
        SqlStr += " a.OfficeId = " + MyMain.OfficeId;
        SqlStr += " and a.ProjectId = " + MyMain.ProjectId;
        SqlStr += " and a.Customerid = " + MyMain.EmpId;


        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    public DataSet GetCustomerHdrInfo(string ProductId)
    {
        SqlStr = "SELECT a.*  ";
        SqlStr += " from CustomerHdr a ";
        SqlStr += " WHERE ";
        SqlStr += " a.OfficeId = " + MyMain.OfficeId;
        SqlStr += " and a.ProjectId = " + MyMain.ProjectId;
        SqlStr += " and a.Customerid = " + MyMain.EmpId;
        SqlStr += " and a.ProductId = " + ProductId;


        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    public DataSet GetCustomerHdrInfoAll()
    {
        SqlStr = "SELECT b.*,c.*,d.customername  ";
        SqlStr += " FROM RefTbl a,CustomerHdr b";
        SqlStr += " ,RfProduct c,RfCustomer d ";
        SqlStr += " WHERE ";
        SqlStr += " a.OfficeId = b.officeid";
        SqlStr += " and a.ProjectId = b.projectid";
        SqlStr += " and b.Productid = c.productid";
        SqlStr += " and b.Customerid = d.Customerid";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    public string InsertCustomerHdr(string ProductId)
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO CustomerHdr";
            SqlStr += "(";
            SqlStr += " OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",CustomerId";
            SqlStr += ",ProductId";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.OfficeId;
            SqlStr += Comma + MyMain.ProjectId;
            SqlStr += Comma + MyMain.EmpId;
            SqlStr += Comma + ProductId;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateCustomerHdr()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update CustomerHdr Set ";
            SqlStr += "OfficeId = " + MyMain.OfficeId;
            SqlStr += ",ProjectId = " + MyMain.ProjectId;
            SqlStr += ",ProductId = " + MyMain.EmpPhoneNo;
            SqlStr += ",ProductNo = " + MyMain.EmpName;
            //SqlStr += ",UomId = " + MyMain.EmpAddress;
            SqlStr += " Where OfficeId = " + MyMain.OfficeId;
            SqlStr += " and ProjectId = " + MyMain.ProjectId;
            SqlStr += " and Customerid = " + MyMain.EmpId;
            SqlStr += " and ProductId = " + MyMain.EmpPhoneNo;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    #endregion


    public string DeleteCustomerTran()
    {
        SqlStr = "DELETE  ";
        SqlStr += " FROM CustomerTran ";
        SqlStr += " WHERE ";
        SqlStr += " OfficeId = " + MyMain.OfficeId;
        SqlStr += " and ProjectId = " + MyMain.ProjectId;
        SqlStr += " and Trandate = " + MyMain.TransectionDate;


        string Res = NonQryCmdSp(SqlStr);
        return Res;
    }
    public string InsertCustomerTran()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO CustomerTran";
            SqlStr += "(";
            SqlStr += " OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",CustomerId";
            SqlStr += ",ProductId";
            SqlStr += ",Trandate";
            SqlStr += ",ProductNo";
            //SqlStr += ",UOMId";
            SqlStr += ",ProductUnitPrice";
            SqlStr += ",Status";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.OfficeId;
            SqlStr += Comma + MyMain.ProjectId;
            SqlStr += Comma + MyMain.EmpId;
            SqlStr += Comma + MyMain.sNo;
            SqlStr += Comma + MyMain.TransectionDate;
            SqlStr += Comma + MyMain.EmpName;
            //SqlStr += Comma + MyMain.EmpAddress;
            SqlStr += Comma + MyMain.EmpPhoneNo;
            SqlStr += Comma + Sqote + MyMain.BalanceSts + Sqote;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";

            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet Sp_GetLeaveTranData()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pUserId", Convert.ToDecimal(MyMain.EmpId), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetLeaveTranData", parameters);

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet GetFillComboLeave()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfLeave  ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }


    public DataSet Sp_GetFillComboActLevel()
    {
        SqlStr = "Sp_GetRfActLevel";
        DataSet ds = GetDatasetSp(SqlStr);
        return ds;
    }

    public DataSet Sp_GetFillComboDesignation(string DepartmentId)
    {
        DataSet ds1 = null;
        try
        {
            _SqlConnection = Connection.SqlConnection;
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pDepartmentId", Convert.ToDecimal(DepartmentId), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetDesignation", parameters);
            return ds1;
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetFillComboPaymentMode()
    {
        SqlStr = "Sp_GetRfPaymentMode";
        DataSet ds = GetDatasetSp(SqlStr);
        return ds;
    }

    public DataSet GetEmpAllLeaveData()
    {
        SqlStr = "SELECT b.EmpId,b.LeavePurpose,b.StartDate  ";
        SqlStr += " ,b.EndDate,b.LeaveId,'A' as LeaveType, b.RecommendedDate,'S' as StartDateS ";
        SqlStr += " ,'E' as EndDateS, b.TranDate ";
        SqlStr += " FROM RefTbl a,LeaveTran b ";
        SqlStr += " WHERE ";
        SqlStr += " a.OfficeId = b.OfficeId ";
        SqlStr += " and a.ProjectId = b.ProjectId ";
        SqlStr += " and b.empid = " + MyMain.EmpId;
        //SqlStr += " and b.trandate = " + MyMain.TransectionDate;

        DataSet Res = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public DataSet GetEmpLeaveData()
    {
        SqlStr = "SELECT b.EmpId,b.LeaveId,b.LeavePurpose,b.StartDate  ";
        SqlStr += " ,b.EndDate,b.RecommendedDate ";
        SqlStr += " FROM RefTbl a,LeaveTran b ";
        SqlStr += " WHERE ";
        SqlStr += " a.OfficeId = b.OfficeId ";
        SqlStr += " and a.ProjectId = b.ProjectId ";
        SqlStr += " and b.empid = " + MyMain.EmpId;
        SqlStr += " and b.trandate = " + MyMain.TransectionDate;

        DataSet Res = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public string InsertLeaveTran()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "INSERT INTO LeaveTran";
            SqlStr += "(";
            SqlStr += " OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",EmpId";
            SqlStr += ",LeaveId";
            SqlStr += ",LeavePurpose";
            SqlStr += ",StartDate";
            SqlStr += ",EndDate";
            SqlStr += ",TranDate";
            SqlStr += ",RecommendedEmpId";
            SqlStr += ",ApprovedEmpId";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.OfficeId;
            SqlStr += Comma + MyMain.ProjectId;
            SqlStr += Comma + MyMain.EmpId;
            SqlStr += Comma + MyMain.Fld7;
            SqlStr += Comma + Sqote + MyMain.Fld1 + Sqote; //Leave Purpose
            SqlStr += Comma + MyMain.Fld2; //start Date
            SqlStr += Comma + MyMain.Fld3; // End Date
            SqlStr += Comma + MyMain.Fld4; //TranDate
            SqlStr += Comma + MyMain.Fld5; //RecommendedEmpId
            SqlStr += Comma + MyMain.Fld6;

            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";

            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateLeaveTran()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "Update LeaveTran Set ";
            SqlStr += "LeavePurpose = " + Sqote + MyMain.Fld1 + Sqote;
            SqlStr += ",StartDate = " + MyMain.Fld2;
            SqlStr += ",EndDate = " + MyMain.Fld3;
            SqlStr += ",LeaveId = " + MyMain.Fld7;

            SqlStr += ",UpdateDate =" + MyMain.UpdateDate;
            SqlStr += ",UpdateTime =" + MyMain.UpdateTime;
            SqlStr += " Where OfficeId = " + MyMain.OfficeId;
            SqlStr += " and ProjectId = " + MyMain.ProjectId;
            SqlStr += " and empid = " + MyMain.EmpId;
            SqlStr += " and TranDate = " + MyMain.Fld4;

            sqlCommand.CommandText = SqlStr;

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet GetEmpIdByUserId(string pUserId)
    {
        SqlStr = "SELECT b.EmpId ";
        SqlStr += " FROM RefTbl a,UserInfo b ";
        SqlStr += " WHERE ";
        SqlStr += " a.OfficeId = b.OfficeId ";
        SqlStr += " and a.ProjectId = b.ProjectId ";
        SqlStr += " and b.userid = " + pUserId;

        DataSet Res = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public DataSet GetEmpLeaveDataForPrint()
    {
        SqlStr = "SELECT b.EmpId,b.LeaveId,b.LeavePurpose,b.StartDate  ";
        SqlStr += " ,b.EndDate,b.TranDate,b.RecommendedDate,c.LeaveDescription ";
        SqlStr += " ,b.RecommendedEmpId,ApprovedEmpId ";
        SqlStr += " FROM RefTbl a,LeaveTran b,RfLeave c ";
        SqlStr += " WHERE ";
        SqlStr += " a.OfficeId = b.OfficeId ";
        SqlStr += " and a.ProjectId = b.ProjectId ";
        SqlStr += " and b.LeaveId = c.LeaveId ";
        SqlStr += " and b.empid = " + MyMain.EmpId;
        SqlStr += " and b.trandate = " + MyMain.TransectionDate;

        DataSet Res = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public DataSet Sp_GetAllLeaveTranData(string sDate, string eDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pStartDate", Convert.ToDecimal(sDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pEndDate", Convert.ToDecimal(eDate), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetAllLeaveTranData", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetLeaveTranDataNew(string pUserId, string pTranDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pUserId", Convert.ToDecimal(pUserId), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pTranDate", Convert.ToDecimal(pTranDate), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetLeaveTranDataNew", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet GetAllEmployee()
    {
        SqlStr = "SELECT * from ";
        SqlStr += " RfEmployee ";
        SqlStr += " order by empname ";

        DataSet Res = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public string GetAllCustomerTran()
    {
        try
        {
            SqlStr = "SELECT ct.CustomerId,rc.CustomerName,ct.ProductId,rpr.ProductDescription";
            SqlStr += ",ct.uomid, ru.UOMDescription,ct.ProductNo,ct.ProductUnitPrice";
            SqlStr += ",ct.ProductUnitPrice as TotalAmount,ct.trandate";
            SqlStr += "FROM RefTbl rt";
            SqlStr += "INNER JOIN CustomerTran ct ON rt.OfficeId=ct.OfficeId AND rt.ProjectId=ct.ProjectId";
            SqlStr += "INNER JOIN RfOffice ro ON rt.OfficeId=ro.OfficeId";
            SqlStr += " INNER JOIN RfProject rp ON rt.ProjectId=rp.ProjectId";
            SqlStr += " INNER JOIN RfProduct rPr ON ct.ProductId=rpr.ProductId";
            SqlStr += " INNER JOIN RfCustomer rc ON ct.CustomerId=rc.CustomerId";
            SqlStr += " INNER JOIN RfUOM ru ON ct.UOMId=ru.UOMId  ";
            SqlStr += " WHERE ct.TranDate>= " + MyMain.Fld1 + " and ct.TranDate<=" + MyMain.Fld2;

            return SqlStr;
        }
        catch (Exception ex)
        {
            return SqlStr;
        }
        return SqlStr;
    }

    public DataSet Sp_GetAllCustomerTran()
    {
        DataSet ds1 = null;
        try
        {
            ds1 = GetDatasetSp("Sp_GetAllCustomerTran");

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetAllCustomerTranSingle()
    {
        DataSet ds1 = null;
        try
        {
            ds1 = GetDatasetSp("Sp_GetAllCustomerTran");

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetCustomerTran(string StartDate, string EndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pStartDate", Convert.ToDecimal(StartDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pEndDate", Convert.ToDecimal(EndDate), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetCustomerTran", parameters);

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetCustomerTranSingle(string pCustomerId, string StartDate, string EndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = Connection.GetParam("@pCustomerId", Convert.ToDecimal(pCustomerId), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pSDate", Convert.ToDecimal(StartDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pEDate", Convert.ToDecimal(EndDate), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetCustomerTranSingle", parameters);

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_PrintChartOfAccount()
    {
        DataSet ds1 = null;
        try
        {
            //return SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_VoucherPrint", parameters);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_PrintChartOfAccount");
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet PrintChartOfAccount1()
    {
        DataSet ds1 = null;
        try
        {
            SqlStr = "Select * from ActChart order by actcode1";
            //ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_PrintChartOfAccount1");
            ds1 = GetDatasetSpNew(SqlStr);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }


    public DataSet GetDataset(string Str1)
    {
        DataSet ds = new DataSet();
        if (UType.IsSql())
        {
            try
            {
                _SqlConnection = Connection.SqlConnection;
                ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, Str1);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _SqlConnection.Close();
                //_SqlConnection.Dispose();
            }
        }
        else
        {
            OleDbCommand oCmd = new OleDbCommand();
            OleDbDataAdapter oAdapter = new OleDbDataAdapter();
            try
            {
                oCmd.Connection = Connection.OleDbConnection;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = Str1;
                oAdapter.SelectCommand = oCmd;
                oAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                oCmd.Dispose();
                oAdapter.Dispose();
            }
        }
        return ds;
    }

    public DataSet GetDatasetSp(string SpName)
    {
        DataSet ds = new DataSet();
        string Str2 = string.Empty;
        if (UType.IsSql())
        {
            try
            {
                _SqlConnection = Connection.SqlConnection;
                ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, SpName);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _SqlConnection.Close();
                _SqlConnection.Dispose();
            }
        }
        else
        {
            OleDbCommand oCmd = new OleDbCommand();
            OleDbDataAdapter oAdapter = new OleDbDataAdapter();
            try
            {
                oCmd.Connection = Connection.OleDbConnection;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = Str2;
                oAdapter.SelectCommand = oCmd;
                oAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                oCmd.Dispose();
                oAdapter.Dispose();
            }
        }
        return ds;
    }
    public DataSet GetDatasetSpNew(string SqlString)
    {
        DataSet ds1 = new DataSet();
        try
        {
            _SqlConnection = Connection.SqlConnection;
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pC1", SqlString, ParameterDirection.Input, SqlDbType.VarChar);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "GetTable_Sp", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;

    }

    public DataSet GetDatasetSpTrial()
    {
        DataSet ds1 = new DataSet();
        try
        {
            _SqlConnection = Connection.SqlConnection;
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = Connection.GetParam("@pOfficeId", UType.MyCtoD(MyMain.OfficeId), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pProjectId", UType.MyCtoD(MyMain.ProjectId), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pCFY", UType.MyCtoD(MyMain.Fyear), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[3] = Connection.GetParam("@pOpeningEndDate", UType.MyCtoD(MyMain.Fld3), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[4] = Connection.GetParam("@pActivityEndDate", UType.MyCtoD(MyMain.Fld4), ParameterDirection.Input, SqlDbType.Decimal);
            //parameters[5] = Connection.GetParam("@pCFY", UType.MyCtoD(MyMain.Fyear), ParameterDirection.Input, SqlDbType.Decimal);
            // parameters[1] = Connection.GetParam("@pC2", SqlString1, ParameterDirection.Input, SqlDbType.VarChar);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "GetTrialBalance_Sp", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;

    }
    public DataSet GetDatasetSpIncome()
    {
        DataSet ds1 = new DataSet();
        try
        {
            _SqlConnection = Connection.SqlConnection;
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = Connection.GetParam("@pOfficeId", UType.MyCtoD(MyMain.OfficeId), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pProjectId", UType.MyCtoD(MyMain.ProjectId), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pCFY", UType.MyCtoD(MyMain.Fyear), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[3] = Connection.GetParam("@pOpeningEndDate", UType.MyCtoD(MyMain.Fld3), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[4] = Connection.GetParam("@pActivityEndDate", UType.MyCtoD(MyMain.Fld4), ParameterDirection.Input, SqlDbType.Decimal);
            //parameters[5] = Connection.GetParam("@pCFY", UType.MyCtoD(MyMain.Fyear), ParameterDirection.Input, SqlDbType.Decimal);
            // parameters[1] = Connection.GetParam("@pC2", SqlString1, ParameterDirection.Input, SqlDbType.VarChar);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "GetIncome_Sp", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;

    }
    public DataSet GetDatasetSpBalance()
    {
        DataSet ds1 = new DataSet();
        try
        {
            _SqlConnection = Connection.SqlConnection;
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = Connection.GetParam("@pOfficeId", UType.MyCtoD(MyMain.OfficeId), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pProjectId", UType.MyCtoD(MyMain.ProjectId), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pCFY", UType.MyCtoD(MyMain.Fyear), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[3] = Connection.GetParam("@pOpeningEndDate", UType.MyCtoD(MyMain.Fld3), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[4] = Connection.GetParam("@pActivityEndDate", UType.MyCtoD(MyMain.Fld4), ParameterDirection.Input, SqlDbType.Decimal);
            //parameters[5] = Connection.GetParam("@pCFY", UType.MyCtoD(MyMain.Fyear), ParameterDirection.Input, SqlDbType.Decimal);
            // parameters[1] = Connection.GetParam("@pC2", SqlString1, ParameterDirection.Input, SqlDbType.VarChar);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "GetBalance_Sp", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;

    }
    public string NonQryCmdSp(string SqlString)
    {
        string retVal = string.Empty;
        try
        {
            _SqlConnection = Connection.SqlConnection;
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pC1", SqlString, ParameterDirection.Input, SqlDbType.VarChar);
            int ResQry = SqlHelper.ExecuteNonQuery(_SqlConnection, CommandType.StoredProcedure, "GetTable_Sp", parameters);
            if (ResQry > 0)
            {
                retVal = "Ok";
            }
            else
            {
                retVal = "Error";
            }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;

    }

    #region OldNonQry
    //public string NonQryCmd(string Str1)
    //{
    //    bool result = false;
    //    string retVal = string.Empty;
    //    if (UType.IsSql())
    //    {
    //        try
    //        {
    //            SqlCommand sqlCommand = new SqlCommand();
    //            sqlCommand.Connection = Connection.SqlConnection;
    //            sqlCommand.CommandText = Str1;

    //            if (sqlCommand.ExecuteNonQuery() > 0)
    //            {
    //                retVal = "Ok";
    //            }
    //            else
    //            { retVal = "Error"; }
    //        }
    //        catch (Exception ex)
    //        {
    //            retVal = ex.Message;
    //        }
    //    }
    //    else
    //    {
    //        OleDbCommand oCmd = null;
    //        OleDbDataAdapter oAdapter = null;
    //        OleDbConnection oCon = Connection.OleDbConnection;

    //        try
    //        {
    //            oCmd = new OleDbCommand();
    //            oAdapter = new OleDbDataAdapter();
    //            oCmd.Connection = Connection.OleDbConnection;

    //            oCmd.CommandType = CommandType.Text;
    //            oCmd.CommandText = Str1;
    //            if (Str1.Trim().Substring(0, 1) == "I")
    //            {
    //                oAdapter.InsertCommand = oCmd;
    //            }
    //            {
    //                oAdapter.UpdateCommand = oCmd;
    //                DataTable dt1 = new DataTable();
    //                oAdapter.Update(dt1);
    //            }

    //            if (oCmd.ExecuteNonQuery() > 0)
    //            {
    //                result = true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            result = false;
    //            throw;
    //        }
    //        finally
    //        {
    //            oCmd.Dispose();
    //            oAdapter.Dispose();
    //        }
    //    }
    //    return retVal;
    //}

    #endregion

    //public string InsertRfEmployee()
    //{
    //    string retVal = string.Empty;
    //    try
    //    {
    //        SqlCommand sqlCommand = new SqlCommand();
    //        sqlCommand.Connection = _SqlConnection;

    //        SqlStr = "INSERT INTO RfEmployee";
    //        SqlStr += "(";
    //        SqlStr += " OfficeId";
    //        SqlStr += ",ProjectId";
    //        SqlStr += ",EmpId";
    //        SqlStr += ",AddDate";
    //        SqlStr += ",AddTime";
    //        SqlStr += ")";
    //        SqlStr += "VALUES( ";
    //        SqlStr += MyMain.OfficeId;
    //        SqlStr += Comma + MyMain.ProjectId;
    //        SqlStr += Comma + MyMain.EmpId;
    //        SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
    //        SqlStr += Comma + DateTime.Now.ToString("HHmm");
    //        SqlStr += ")";

    //        sqlCommand.CommandText = SqlStr;
    //        //_SqlConnection.Open();

    //        if (sqlCommand.ExecuteNonQuery() > 0)
    //        {
    //            retVal = "Ok";
    //        }
    //        else
    //        { retVal = "Error"; }
    //    }
    //    catch (Exception ex)
    //    {
    //        retVal = ex.Message;
    //    }
    //    return retVal;
    //}
    public string InsertRefTbl(string TblName)
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO " + TblName;
            SqlStr += "(";
            SqlStr += MyMain.Fld1;
            if (MyMain.Fld3.Length > 0)
            {
                SqlStr += "," + MyMain.Fld3;
            }
            if (MyMain.Fld5.Length > 0)
            {
                SqlStr += "," + MyMain.Fld5;
            }
            if (MyMain.Fld7.Length > 0)
            {
                SqlStr += "," + MyMain.Fld7;
            }
            if (MyMain.Fld9.Length > 0)
            {
                SqlStr += Comma + MyMain.Fld9;
            }
            if (MyMain.Fld11.Length > 0)
            {
                SqlStr += Comma + MyMain.Fld11;
            }
            if (MyMain.Fld13.Length > 0)
            {
                SqlStr += Comma + MyMain.Fld13;
            }
            if (MyMain.Fld15.Length > 0)
            {
                SqlStr += Comma + MyMain.Fld15;
            }

            SqlStr += "," + "AddDate";
            SqlStr += "," + "AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld2;
            if (MyMain.Fld3.Length > 0)
            {
                SqlStr += Comma + MyMain.Fld4;
            }
            if (MyMain.Fld5.Length > 0)
            {
                SqlStr += Comma + MyMain.Fld6;
            }
            if (MyMain.Fld7.Length > 0)
            {
                SqlStr += Comma + MyMain.Fld8;
            }
            if (MyMain.Fld9.Length > 0)
            {
                SqlStr += Comma + MyMain.Fld10;
            }
            if (MyMain.Fld11.Length > 0)
            {
                SqlStr += Comma + MyMain.Fld12;
            }
            if (MyMain.Fld13.Length > 0)
            {
                SqlStr += Comma + MyMain.Fld14;
            }
            if (MyMain.Fld15.Length > 0)
            {
                SqlStr += Comma + MyMain.Fld16;
            }

            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmmssff");
            SqlStr += ")";

            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }


    public string UpdateRefTbl(string TblName)
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update " + TblName;
            SqlStr += " Set ";
            SqlStr += MyMain.Fld1 + "=" + MyMain.Fld2;
            if (MyMain.Fld3.Length > 0)
            {
                SqlStr += "," + MyMain.Fld3 + "=" + MyMain.Fld4;
            }
            if (MyMain.Fld5.Length > 0)
            {
                SqlStr += "," + MyMain.Fld5 + "=" + "'" + MyMain.Fld6 + "'";
            }
            if (MyMain.Fld7.Length > 0)
            {
                SqlStr += "," + MyMain.Fld7 + "=" + "'" + MyMain.Fld8 + "'";
            }
            if (MyMain.Fld9.Length > 0)
            {
                SqlStr += "," + MyMain.Fld9 + "=" + "'" + MyMain.Fld10 + "'";
            }
            if (MyMain.Fld11.Length > 0)
            {
                SqlStr += "," + MyMain.Fld11 + "=" + "'" + MyMain.Fld12 + "'";
            }
            if (MyMain.Fld13.Length > 0)
            {
                SqlStr += "," + MyMain.Fld13 + "=" + "'" + MyMain.Fld14 + "'";
            }
            if (MyMain.Fld15.Length > 0)
            {
                SqlStr += "," + MyMain.Fld15 + "=" + "'" + MyMain.Fld16 + "'";
            }


            SqlStr += " Where ";
            SqlStr += MyMain.Fld1 + " = " + MyMain.Fld2;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet Sp_GetEmpInfo()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pEmpId", Convert.ToDecimal(MyMain.EmpId), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetEmpInfo", parameters);

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetCustomerDefault(string pCustomerId)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pCustomerId", pCustomerId, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetCustomerDefault", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetCustomerSingle(string pCustomerId, string pSdate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = Connection.GetParam("@pCustomerId", pCustomerId, ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pDate", pSdate, ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pDate", pSdate, ParameterDirection.Input, SqlDbType.Decimal);
            //_SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetCustomerTranSingle", parameters);

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }


    public DataSet Sp_GetUom(string pUomId)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@p1", pUomId, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetUom", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetCustomerDefaultProduct(string pCustomerId)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pCustomerId", pCustomerId, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetCustomerDefaultProduct", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet GeEmailUser()
    {
        SqlStr = "SELECT * from RfEmailUser";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public DataSet GetAllUser()
    {
        SqlStr = "SELECT  ";
        SqlStr += "[LoginId] ";
        SqlStr += ",[UserId] ";
        SqlStr += ",[LoginPassword] ";
        SqlStr += ",[LoginName] ";
        SqlStr += "  from UserInfo";
        if (MyMain.Fld1.Length > 0)
        {
            SqlStr += " Where officeId =   " + MyMain.Fld2;
            SqlStr += " and projectId =   " + MyMain.Fld3;
            SqlStr += " and  loginid =   '" + MyMain.Fld1 + "'";
        }

        //if (MyMain.Fld2.Length > 0 && MyMain.Fld3.Length > 0)
        //{
        //    SqlStr += " Where officeId =   " + MyMain.Fld2;
        //    SqlStr += " and projectId =   " + MyMain.Fld3;
        //}
        //if (MyMain.Fld2.Length > 0 && MyMain.Fld3.Length == 0)
        //{
        //    SqlStr += " Where officeId =   " + MyMain.Fld2;
        //}
        //if (MyMain.Fld1.Length > 0 && MyMain.Fld2.Length == 0 && MyMain.Fld3.Length == 0)
        //{
        //    SqlStr += " Where LoginId =   '" + MyMain.Fld1 + "'";
        //}

        //if (MyMain.Fld1.Length > 0 && MyMain.Fld2.Length > 0 && MyMain.Fld3.Length > 0)
        //{
        //    SqlStr += " Where officeId =   " + MyMain.Fld2;
        //    SqlStr += " and projectId =   " + MyMain.Fld3;
        //    SqlStr += " and loginid =   '" + MyMain.Fld1 + "'";
        //}


        DataSet Res = GetDatasetSpNew(SqlStr);
        return Res;
    }
    public DataSet Sp_GetUserInfo()
    {
        SqlStr = "SELECT *  ";
        SqlStr += " from UserInfo    ";
        if (MyMain.Fld1.Length > 0)
        {
            SqlStr += " Where LoginId =   '" + MyMain.Fld1 + "'";
        }


        DataSet Res = GetDatasetSpNew(SqlStr);
        return Res;
    }
    public DataSet Sp_GetUserInfonk()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pVar1", MyMain.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetUserInfo", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public string InsertUserInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO UserInfo ";
            SqlStr += "(";
            SqlStr += "OfficeId ";
            SqlStr += ",ProjectId ";
            SqlStr += ",UserId ";
            SqlStr += ",LoginID ";
            SqlStr += ",AddDate ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + Sqote + MyMain.Fld4 + Sqote;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateUserInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update UserInfo ";
            SqlStr += " Set ";
            SqlStr += "LoginId = " + Sqote + MyMain.Fld4 + Sqote;
            SqlStr += ",LoginPassword = " + Sqote + MyMain.Fld5 + Sqote;
            SqlStr += ",[LoginName] = " + Sqote + MyMain.Fld6 + Sqote;
            SqlStr += ",EmailId = " + Sqote + MyMain.Fld7 + Sqote;
            SqlStr += ",LoginSts = " + Sqote + MyMain.Fld8 + Sqote;
            //SqlStr += ",UserIP = " + Sqote + MyMain.Fld9 + Sqote;
            SqlStr += ",UserIPSts = " + Sqote + MyMain.Fld10 + Sqote;
            SqlStr += ",UpdateDate = " + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += ",roleid = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",ProjectId = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += " Where ";
            SqlStr += " OfficeId = " + MyMain.Fld1;
            //SqlStr += " and ProjectId = " + MyMain.Fld2;
            SqlStr += " and UserId = " + MyMain.Fld3;
            retVal = NonQryCmdSp(SqlStr);
        }

        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }



    public DataSet Sp_GetRoleInfo()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pVar1", MyMain.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetRoleInfo", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetRoleInfo1()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pVar1", MyMain.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pVar2", MyMain.Fld2, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetRoleInfo1", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public string InsertRfRole()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfRole ";
            SqlStr += "(";
            //SqlStr += "DepartId ";
            SqlStr += "RoleId ";
            SqlStr += ",RoleName ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            // SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + Sqote + MyMain.Fld3 + Sqote;
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateRoleInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update RfRole ";
            SqlStr += " Set ";
            SqlStr += "DepartId = " + MyMain.Fld2;
            SqlStr += ",RoleName = " + Sqote + MyMain.Fld3 + Sqote;
            SqlStr += " Where ";
            SqlStr += " RoleId = " + MyMain.Fld1;
            SqlStr += " and DepartId = " + MyMain.Fld2;

            retVal = NonQryCmdSp(SqlStr);
        }

        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet Sp_GetMenuInfo()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pVar1", MyMain.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetMenuInfo", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet GetMenuInfo()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pVar1", MyMain.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetMenuInfo", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetMenuAll()
    {
        DataSet ds1 = null;
        try
        {
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetMenuAll");
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public string InsertMenuInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfMenu ";
            SqlStr += "(";
            SqlStr += "MenuId ";
            SqlStr += ",MenuText ";
            SqlStr += ",MenuPath ";
            SqlStr += ",MenuLevel ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += Comma + Sqote + MyMain.Fld3 + Sqote;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateMenuInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update RfMenu ";
            SqlStr += " Set ";
            SqlStr += "MenuText = " + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += ",MenuPath = " + Sqote + MyMain.Fld3 + Sqote;
            SqlStr += ",MenuLevel = " + MyMain.Fld4;
            SqlStr += " Where ";
            SqlStr += " MenuId = " + MyMain.Fld1;
            retVal = NonQryCmdSp(SqlStr);
        }

        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet Sp_GetEmailId()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pVar1", MyMain.Fld1, ParameterDirection.Input, SqlDbType.VarChar);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetEmailId", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }



    public string SendLog()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfEmailLog ";
            SqlStr += "(";
            SqlStr += "EmailSNo ";
            SqlStr += ",TranSts ";
            SqlStr += ",AddDate ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + " getdate()";
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet GeAutoEmailId()
    {
        SqlStr = "SELECT * from RfEmail";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public DataSet GeAutoEmailId(string Sts1)
    {
        SqlStr = "SELECT * from RfEmail where Sts = " + Sts1;

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public DataSet GeAutoEmailId(string Sts1, string EmailSno)
    {
        SqlStr = "SELECT * from RfEmail where Sts = " + Sts1;
        SqlStr += " and emailsno > " + EmailSno;


        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public DataSet Sp_GetPayHeader()
    {
        SqlStr = "SELECT re.*,ro.OfficeDescription,rp.ProjectDescription ";
        SqlStr += " ,rdp.DepartDescription ";
        SqlStr += " ,rd.DesignationDescription";
        SqlStr += " ,rpg.PayGroupDescription";
        SqlStr += " ,ph.AllDedAmount,ph.YearMonth";
        SqlStr += " ,rad.AllDedId,rad.AllDedDescription,rad.AllDedSts,rad.AllDedShort";
        SqlStr += " FROM RefTbl rt";
        SqlStr += " INNER JOIN RfEmployee re ON rt.OfficeId=re.OfficeId AND rt.ProjectId=rt.ProjectId";
        SqlStr += " INNER JOIN RfOffice ro ON rt.OfficeId=ro.OfficeId";
        SqlStr += " INNER JOIN RfProject rp ON rt.ProjectId=rp.ProjectId";
        SqlStr += " INNER JOIN RfDepart rdp ON re.DepartId = rdp.DepartId";
        SqlStr += " INNER JOIN RfDesignation rd ON rdp.DepartId=rd.DepartId and re.DesignationId= rd.DesignationId";

        SqlStr += " INNER JOIN RfPayGroup rpg ON re.PayGroupId=rpg.PayGroupId";
        SqlStr += " INNER JOIN payheader ph ON re.TranId = ph.tranid";
        SqlStr += " INNER JOIN RfAllDed rad ON ph.AllDedId=rad.AllDedId";
        SqlStr += " WHERE re.EmpId= " + MyMain.Fld1;
        SqlStr += " AND ph.yearmonth =" + MyMain.Fld2;
        SqlStr += " ORDER BY rad.AllDedDescription";

        DataSet Res = GetDatasetSpNew(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;

    }

    //public DataSet Sp_GetPayHeader()
    //{
    //    DataSet ds1 = null;
    //    try
    //    {
    //        SqlParameter[] parameters = new SqlParameter[2];
    //        parameters[0] = Connection.GetParam("@pEmpId", Convert.ToDecimal(MyMain.Fld1), ParameterDirection.Input, SqlDbType.Decimal);
    //        parameters[1] = Connection.GetParam("@pYearMonth", Convert.ToDecimal(MyMain.Fld2), ParameterDirection.Input, SqlDbType.Decimal);
    //        ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetPayHeader", parameters);
    //    }
    //    catch (Exception ex)
    //    {
    //        ds1 = null;
    //    }
    //    return ds1;
    //}

    public DataSet Sp_GetPayHeaderMonth()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pYearMonth", Convert.ToDecimal(MyMain.Fld1), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetPayHeaderMonth", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet Sp_GetPayHeaderNew()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pEmpId", Convert.ToDecimal(MyMain.Fld1), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetPayHeaderNew", parameters);

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public string InsertEmailId()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfEmail ";
            SqlStr += "(";
            SqlStr += "EmailId ";
            SqlStr += ",AddDate ";
            SqlStr += ",AddTime ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += Sqote + MyMain.Fld1 + Sqote;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmmssff");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet Sp_GetPayHeaderNewAll()
    {
        DataSet ds1 = null;
        try
        {
            //SqlParameter[] parameters = new SqlParameter[1];
            //parameters[0] = Connection.GetParam("@p1", Convert.ToDecimal(MyMain.Fld1), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetPayHeaderNewAll");

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public string InsertPayHeader()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO PayHeader ";
            SqlStr += "(";
            SqlStr += "TranId ";
            SqlStr += ",YearMonth ";
            SqlStr += ",PayMode ";
            SqlStr += ",ChequeNo ";
            SqlStr += ",Remarks ";
            SqlStr += ",AllDedId ";
            SqlStr += ",AllDedAmount ";
            SqlStr += ",AllDedAmountFix ";
            SqlStr += ",AddDate ";
            SqlStr += ",AddTime ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + Sqote + MyMain.Fld4 + Sqote;
            SqlStr += Comma + Sqote + MyMain.Fld5 + Sqote;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + MyMain.Fld7;
            SqlStr += Comma + MyMain.Fld8;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmmssff");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdatePayHeader()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "UPDATE PayHeader ";
            SqlStr += " SET ";
            SqlStr += " PayMode =" + MyMain.Fld3;
            SqlStr += " ,ChequeNo = " + Sqote + MyMain.Fld4 + Sqote;
            SqlStr += " ,Remarks =" + Sqote + MyMain.Fld5 + Sqote;
            SqlStr += " ,AllDedAmount =" + MyMain.Fld7;
            SqlStr += " WHERE ";
            SqlStr += " TranId =" + MyMain.Fld1;
            SqlStr += " AND YearMonth =" + MyMain.Fld2;
            SqlStr += " AND AllDedId =" + MyMain.Fld6;

            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet Sp_GetDepartment()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pVar1", MyMain.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetDepartment", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_SalesReport(string pStartDate, string pEndDate)
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(pStartDate), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(pEndDate), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(this.SqlConnection, CommandType.StoredProcedure, "Sp_SalesReport", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet GetJobType()
    {
        SqlStr = "SELECT *  ";
        SqlStr += " FROM RfJobType ";
        SqlStr += " ORDER By JobTypeDescription ";

        DataSet Res = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }


    #region Accounts
    public DataSet GetRefData()
    {
        SqlStr = "SELECT  ";
        SqlStr += " rt.* FROM RefTbl rt ";
        SqlStr += " where rt.OfficeId = " + MyMain.Fld1;
        SqlStr += " and rt.ProjectId = " + MyMain.Fld2;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetVoucherData()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM Reftbl ";
        DataSet ds = GetDataset(SqlStr);
        return ds;

    }
    #endregion

    public DataSet ShipmentType()
    {
        SqlStr = "SELECT *  ";
        SqlStr += " FROM RfShipmentType ";
        SqlStr += " Order by ShipmentTypeId ";

        DataSet Res = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }
    public DataSet ProductType()
    {
        SqlStr = "SELECT *  ";
        SqlStr += " FROM RfProductType ";
        SqlStr += " Order by ProductTypeId ";

        DataSet Res = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public string InsertPurCosting()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "INSERT INTO PurchaseCosting";
            SqlStr += "(";
            SqlStr += " OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",VoucherNo";
            SqlStr += ",cfy ";
            SqlStr += ",VoucherDate ";
            SqlStr += ",InvNo";
            SqlStr += ",InvDate";
            SqlStr += ",VehNo";
            SqlStr += ",VehType";
            SqlStr += ",Purchase";
            SqlStr += ",UOMId";
            SqlStr += ",Consignee";
            SqlStr += ",Area";
            SqlStr += ",NoOfPackage";
            SqlStr += ",TotAmount";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ",AddUser";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.OfficeId;
            SqlStr += Comma + MyMain.ProjectId;
            SqlStr += Comma + Sqote + MyMain.Fld1 + Sqote;
            SqlStr += Comma + MyMain.Fld15;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + Sqote + MyMain.Fld3 + Sqote;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + Sqote + MyMain.Fld5 + Sqote;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + MyMain.Fld7;
            SqlStr += Comma + MyMain.Fld8;
            SqlStr += Comma + MyMain.Fld9;
            SqlStr += Comma + Sqote + MyMain.Fld10 + Sqote;
            SqlStr += Comma + MyMain.Fld11;
            SqlStr += Comma + MyMain.Fld12;

            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += Comma + Sqote + MyMain.Fld13 + Sqote;
            SqlStr += ")";

            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertPurCostingDtl()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "INSERT INTO PurchaseCostingDtl";
            SqlStr += "(";
            SqlStr += " PurchaseCostingId";
            SqlStr += ",VoucherNo";
            SqlStr += ",cfy ";
            SqlStr += ",ItemId";
            SqlStr += ",Qty";
            SqlStr += ",AvgWt";
            SqlStr += ",NetWt";
            SqlStr += ",UnitRate";
            SqlStr += ",Amount";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ",AddUser";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + MyMain.Fld7;
            SqlStr += Comma + MyMain.Fld8;
            SqlStr += Comma + MyMain.Fld9;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += Comma + Sqote + MyMain.Fld10 + Sqote;
            SqlStr += ")";

            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet GetTranIdFromSaleCosting()
    {
        SqlStr = "SELECT *  ";
        SqlStr += " FROM SaleCosting ";
        SqlStr += " WHERE ";
        SqlStr += " voucherno = " + MyMain.Fld1;
        SqlStr += " and cfy = " + MyMain.Fld2;
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }

    public string DeleteFromSaleCosting()
    {
        //Detail
        SqlStr = "DELETE  ";
        SqlStr += " FROM SaleCostingDtl ";
        SqlStr += " WHERE ";
        SqlStr += " SaleCostingid = " + MyMain.Fld1;
        decimal Res = SqlHelper.ExecuteNonQuery(_SqlConnection, CommandType.Text, SqlStr);

        SqlStr = "DELETE FROM ActTran WHERE LTRIM(CONVERT( VARCHAR(20),vno ))+LTRIM(CONVERT( VARCHAR(20),cfy )) = ";
        SqlStr += " (SELECT LTRIM(CONVERT( VARCHAR(20),vtranid ))+LTRIM(CONVERT( VARCHAR(20),cfy )) ";
        SqlStr += "  from salecosting where  saleCostingId = " + MyMain.Fld1 + ")";
        Res = SqlHelper.ExecuteNonQuery(_SqlConnection, CommandType.Text, SqlStr);

        //Header

        SqlStr = "DELETE  ";
        SqlStr += " FROM SaleCosting ";
        SqlStr += " WHERE ";
        SqlStr += " SaleCostingid = " + MyMain.Fld1;
        Res = SqlHelper.ExecuteNonQuery(_SqlConnection, CommandType.Text, SqlStr);
        return Res.ToString();
    }

    public string InsertSaleCosting()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "INSERT INTO SaleCosting";
            SqlStr += "(";
            SqlStr += " OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",VoucherNo";
            SqlStr += ",cfy ";
            SqlStr += ",VoucherDate";
            SqlStr += ",InvNo";
            SqlStr += ",InvDate";
            SqlStr += ",BlNo";
            SqlStr += ",BlDate";
            SqlStr += ",Shipped";
            SqlStr += ",Through";
            SqlStr += ",sale";
            SqlStr += ",UOMId";
            SqlStr += ",Consignee";
            SqlStr += ",Area";
            SqlStr += ",NoOfPackage";
            SqlStr += ",TotAmount";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ",AddUser";
            SqlStr += ",TotSaleAmount";
            //SqlStr += ",Ddl1";
            //SqlStr += ",Ddl1Amt";
            //SqlStr += ",Ddl2";
            //SqlStr += ",Ddl2Amt";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.OfficeId;
            SqlStr += Comma + MyMain.ProjectId;
            SqlStr += Comma + Sqote + MyMain.Fld1 + Sqote;
            SqlStr += Comma + MyMain.Fld16;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + Sqote + MyMain.Fld3 + Sqote;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + Sqote + MyMain.Fld7 + Sqote;
            SqlStr += Comma + Sqote + MyMain.Fld8 + Sqote;
            SqlStr += Comma + MyMain.Fld9;
            SqlStr += Comma + MyMain.Fld10;
            SqlStr += Comma + MyMain.Fld11;
            SqlStr += Comma + Sqote + MyMain.Fld12 + Sqote;
            SqlStr += Comma + MyMain.Fld13;
            SqlStr += Comma + MyMain.Fld14;

            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += Comma + Sqote + MyMain.Fld15 + Sqote;
            SqlStr += Comma + MyMain.Fld17;
            //SqlStr += Comma + MyMain.Fld18;
            //SqlStr += Comma + MyMain.Fld19;
            //SqlStr += Comma + MyMain.Fld20;
            //SqlStr += Comma + MyMain.Fld21;

            SqlStr += ")";


            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertSaleCostingDtl()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "INSERT INTO SaleCostingDtl";
            SqlStr += "(";
            SqlStr += " SaleCostingId";
            SqlStr += ",VoucherNo";
            SqlStr += ",cfy ";
            SqlStr += ",ItemId";
            SqlStr += ",Qty";
            SqlStr += ",AvgWt";
            SqlStr += ",NetWt";
            SqlStr += ",UnitRate";
            SqlStr += ",Amount";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ",AddUser";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + MyMain.Fld7;
            SqlStr += Comma + MyMain.Fld8;
            SqlStr += Comma + MyMain.Fld9;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += Comma + Sqote + MyMain.Fld10 + Sqote;
            SqlStr += ")";

            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetPurchaseData()
    {
        SqlStr = "SELECT b.VoucherNo,b.VoucherDate,b.itemid,c.ProductDescription,b.Qty,d.uomdescription ";
        SqlStr += ",b.qtyamount,((QtyAmount - WastageAmount)/(Qty-WastageWt)) as AvgAmt ";
        SqlStr += " FROM purchasecosting a INNER JOIN purchasecostingdtl b ";
        SqlStr += " on a.purchasecostingid = b.purchasecostingid ";
        SqlStr += " INNER JOIN RfProduct c ";
        SqlStr += " on b.itemid = c.productid ";
        SqlStr += " INNER JOIN RfUom d ";
        SqlStr += " on c.uomid = d.uomid ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public DataSet GetSaleR2Data()
    {
        string sQote = "'";
        SqlStr = "SELECT sc.*,scd.ItemSaleQty,pc.*,pcd.* FROM SaleCosting sc ";
        SqlStr += " INNER JOIN SaleCostingDtl scd ON sc.SaleCostingId=scd.SaleCostingId ";
        SqlStr += " INNER JOIN PurchaseCosting pc ON scd.PurchaseVoucherNo=pc.VoucherNo AND scd.PurchaseVoucherDate=pc.VoucherDate ";
        SqlStr += " INNER JOIN PurchaseCostingDtl pcd ON scd.PurchaseVoucherNo=pcd.VoucherNo AND scd.PurchaseVoucherDate=pcd.VoucherDate AND scd.ItemId=pcd.ItemId ";
        SqlStr += " WHERE sc.VoucherNo = " + MyMain.Fld1;
        SqlStr += " and   sc.VoucherDate = " + MyMain.Fld2;
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }

    public DataSet GetSaleR3Data()
    {
        string sQote = "'";
        SqlStr = "SELECT sc.*,scd.ItemSaleQty,pc.*,pcd.* FROM SaleCosting sc ";
        SqlStr += " INNER JOIN SaleCostingDtl scd ON sc.SaleCostingId=scd.SaleCostingId ";
        SqlStr += " INNER JOIN PurchaseCosting pc ON scd.PurchaseVoucherNo=pc.VoucherNo AND scd.PurchaseVoucherDate=pc.VoucherDate ";
        SqlStr += " INNER JOIN PurchaseCostingDtl pcd ON scd.PurchaseVoucherNo=pcd.VoucherNo AND scd.PurchaseVoucherDate=pcd.VoucherDate AND scd.ItemId=pcd.ItemId ";
        SqlStr += " WHERE sc.VoucherDate >= " + MyMain.Fld1;
        SqlStr += " and   sc.VoucherDate <= " + MyMain.Fld2;
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }

    public DataSet GetSaleR3Data_Sp()
    {
        DataSet ds1 = new DataSet();
        try
        {
            _SqlConnection = Connection.SqlConnection;
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(MyMain.Fld1), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(MyMain.Fld2), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_SalesR3Data", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet GetSaleR3Data_Sp1()
    {
        DataSet ds1 = new DataSet();
        try
        {
            _SqlConnection = Connection.SqlConnection;
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(MyMain.Fld1), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pC2", Convert.ToDecimal(MyMain.Fld2), ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pC3", Convert.ToDecimal(MyMain.Fld3), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_SalesR3Data1", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet GetSaleR4Data_Sp()
    {
        DataSet ds1 = new DataSet();
        try
        {
            _SqlConnection = Connection.SqlConnection;
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pC1", Convert.ToDecimal(MyMain.Fld1), ParameterDirection.Input, SqlDbType.Decimal);
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_SalesR4Data", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }


    public DataSet GetPurchaseCostingData()
    {
        SqlStr = "SELECT *  ";
        SqlStr += " FROM PurchaseCosting ";
        SqlStr += " WHERE ";
        SqlStr += " voucherno = " + MyMain.Fld1;
        SqlStr += " and voucherdate = " + MyMain.Fld2;
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }
    public DataSet GetItem()
    {
        SqlStr = "SELECT Productid as ActCode, ProductDescription as ActDesc, *  ";
        SqlStr += " FROM Rfproduct ";
        SqlStr += " WHERE officeID = " + MyMain.Fld1;
        SqlStr += " AND ProjectID = " + MyMain.Fld2;
        SqlStr += " Order by ProductID ";

        DataSet Res = GetDatasetSpNew(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public DataSet FillComboDebtor()
    {
        SqlStr = "SELECT ac.ActCode, ac.ActDesc  FROM RefTbl rt ";
        SqlStr += "INNER JOIN  ActChart ac ON rt.OfficeId=ac.OfficeId AND rt.ProjectId=ac.OfficeId ";
        SqlStr += " AND rt.debtorActCode = SUBSTRING(CONVERT(varchar(20), ac.ActCode),1,LEN(rt.debtorActCode)) ";
        SqlStr += " AND  LEN(ac.ActCode) > LEN(rt.debtorActCode) ";
        SqlStr += " order by actcode1 ";
        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    public DataSet FillComboCreditor()
    {
        SqlStr = "SELECT ac.ActCode, ac.ActDesc  FROM RefTbl rt ";
        SqlStr += "INNER JOIN  ActChart ac ON rt.OfficeId=ac.OfficeId AND rt.ProjectId=ac.OfficeId ";
        SqlStr += " AND rt.CreditorActCode = SUBSTRING(CONVERT(varchar(20), ac.ActCode),1,LEN(rt.CreditorActCode)) ";
        SqlStr += " AND  LEN(ac.ActCode) > LEN(rt.CreditorActCode) ";
        SqlStr += " order by actcode1 ";
        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public DataSet FillComboPurchase()
    {
        SqlStr = "SELECT ac.ActCode, ac.ActDesc  FROM RefTbl rt ";
        SqlStr += "INNER JOIN  ActChart ac ON rt.OfficeId=ac.OfficeId AND rt.ProjectId=ac.OfficeId ";
        SqlStr += " AND rt.PurActCode = SUBSTRING(CONVERT(varchar(20), ac.ActCode),1,LEN(rt.PurActCode)) ";
        SqlStr += " AND  LEN(ac.ActCode) > LEN(rt.PurActCode) ";
        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public DataSet FillComboSale()
    {
        SqlStr = "SELECT ac.ActCode, ac.ActDesc  FROM RefTbl rt ";
        SqlStr += "INNER JOIN  ActChart ac ON rt.OfficeId=ac.OfficeId AND rt.ProjectId=ac.OfficeId ";
        SqlStr += " AND rt.salActCode = SUBSTRING(CONVERT(varchar(20), ac.ActCode),1,LEN(rt.salActCode)) ";
        SqlStr += " AND  LEN(ac.ActCode) > LEN(rt.salActCode) ";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    public DataSet FillComboExpense()
    {
        SqlStr = "SELECT ac.ActCode, ac.ActDesc  FROM RefTbl rt ";
        SqlStr += "INNER JOIN  ActChart ac ON rt.OfficeId=ac.OfficeId AND rt.ProjectId=ac.OfficeId ";
        SqlStr += " AND rt.ExpenseActCode = SUBSTRING(CONVERT(varchar(20), ac.ActCode),1,LEN(rt.salActCode)) ";
        SqlStr += " AND  LEN(ac.ActCode) > LEN(rt.ExpenseActCode) ";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    public DataSet GetVoucherNo()
    {
        SqlStr = "SELECT MAX(vno) as maxvno FROM ActTran ";
        SqlStr += " WHERE ";
        SqlStr += " OfficeId = " + MyMain.Fld1;
        SqlStr += " and ProjectId = " + MyMain.Fld2;
        SqlStr += " and CFY = " + MyMain.Fld3;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetVoucherNoSel()
    {
        SqlStr = "SELECT MAX(vno) as maxvno FROM ActTran ";
        SqlStr += " WHERE ";
        SqlStr += " OfficeId = " + MyMain.Fld1;
        SqlStr += " and ProjectId = " + MyMain.Fld2;
        //SqlStr += " and CFY = " + MyMain.Fld3;
        SqlStr += " and jobno = " + MyMain.Fld4;
        SqlStr += " and jobsnotype = " + MyMain.Fld5;
        SqlStr += " and actcode = " + MyMain.Fld6;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetVoucherNoAuto()
    {
        SqlStr = "SELECT a.* FROM ActTran a ";
        SqlStr += " WHERE ";
        SqlStr += " a.OfficeId = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.CFY = " + MyMain.Fld3;
        SqlStr += " and a.job = " + MyMain.Fld4;
        //SqlStr += " and a.ActCode = " + MyMain.Fld5;
        SqlStr += " and a.JobSno = " + MyMain.Fld5;
        if (MyMain.Fld6.Length > 0)
        {
            SqlStr += " and a.Sno = " + MyMain.Fld6; //  SqlStr += " and a.Discount = " + MyMain.Fld6; //
        }
        //if (MyMain.Fld7.Length > 0)
        //{
        //    SqlStr += " and a.status = '" + MyMain.Fld7 + "'";
        //}
        SqlStr += " order by JobSno,tranid desc ";

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public DataSet GetVoucherNoAutoBk()
    {
        SqlStr = "SELECT a.* FROM ActTran a ";
        SqlStr += " WHERE ";
        SqlStr += " a.OfficeId = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.CFY = " + MyMain.Fld3;
        SqlStr += " and a.job = " + MyMain.Fld4;
        SqlStr += " and a.JobSnoType = " + MyMain.Fld5;
        //SqlStr += " and a.JobSno = " + MyMain.Fld6;
        if (MyMain.Fld6.Length > 0)
        {
            SqlStr += " and a.JobSno = '" + MyMain.Fld6 + "'";
        }
        if (MyMain.Fld7.Length > 0)
        {
            SqlStr += " and a.status = '" + MyMain.Fld7 + "'";
        }
        SqlStr += " order by JobSno,tranid desc ";

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetActTranId()
    {
        SqlStr = "SELECT * FROM ActTran ";
        SqlStr += " WHERE ";
        SqlStr += " officeid = " + MyMain.Fld1;
        SqlStr += " and ProjectID = " + MyMain.Fld2;
        SqlStr += " and cfy = " + MyMain.Fld3;
        SqlStr += " and VNo = " + MyMain.Fld4;
        if (UType.MyCtoD(MyMain.Fld5) > 0)
        {
            SqlStr += " and actcode = " + MyMain.Fld5;
        }
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public DataSet GetTranIdFromPurCostingDtl()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM PurchaseCostingDtl ";
        SqlStr += " WHERE ";
        SqlStr += " purchasecostingid = " + MyMain.Fld1;
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }
    public string UpdatePurchaseId()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "Update RfProduct";
            SqlStr += " set ";
            SqlStr += " Productbalance = productbalance+" + MyMain.Fld1;
            SqlStr += " where ";
            SqlStr += ",productid=  " + MyMain.Fld2;

            sqlCommand.CommandText = SqlStr;


            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet GetTranIdFromSaleCostingDtl()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM SaleCostingDtl ";
        SqlStr += " WHERE ";
        SqlStr += " SaleCostingid = " + MyMain.Fld1;
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }

    public DataSet FillComboProduct()
    {
        SqlStr = "SELECT * from RfProduct ";
        SqlStr += "where costingsts = 'Y' and CostingHdr='Y' ";


        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public DataSet FillComboVehType()
    {
        SqlStr = "SELECT * from RfVehType ";
        SqlStr += " order by Vehtypeid ";


        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    public DataSet GetUnitRate()
    {
        SqlStr = "SELECT SUM(amount),SUM(netwt), SUM(amount) / SUM(netwt) AS PurUnitRate FROM PurchaseCostingDtl pcd ";
        SqlStr += " where itemid = " + MyMain.Fld1;

        DataSet Res = GetDataset(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;

    }

    public DataSet GetProductType()
    {
        SqlStr = "SELECT rpt.* FROM RfProduct rp ";
        SqlStr += " INNER JOIN RfProductType rpt ON rp.ProductTypeId=rpt.ProductTypeId";
        SqlStr += " WHERE rp.ProductId= " + MyMain.Fld1;

        DataSet Res = GetDataset(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;

    }
    public DataSet GetCount()
    {
        SqlStr = "SELECT count(SNo) as MaxSno from LoginHistory ";

        DataSet Res = GetDatasetSpNew(SqlStr);
        return Res;
    }

    public string InsertLoginHistoryOld19032021()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            _SqlConnection = Connection.SqlConnection;
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "INSERT INTO LoginHistory";
            SqlStr += "(";
            SqlStr += "UserId";
            SqlStr += ",LoginSts";
            SqlStr += ",LogDate ";
            SqlStr += ",LogTime ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmmssff");
            SqlStr += ")";

            sqlCommand.CommandText = SqlStr;

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertLoginHistory()
    {
        string retVal = string.Empty;
        try
        {
            //SqlCommand sqlCommand = new SqlCommand();
            //_SqlConnection = Connection.SqlConnection;
            //sqlCommand.Connection = _SqlConnection;

            SqlStr = "INSERT INTO LoginHistory";
            SqlStr += "(";
            SqlStr += "UserId";
            SqlStr += ",LoginSts";
            SqlStr += ",LogDate ";
            SqlStr += ",LogTime ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmmssff");
            SqlStr += ")";

            //sqlCommand.CommandText = SqlStr;
            string ResQry = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet pValidationStatus()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfFld";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet pValidation()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfOffice";
        SqlStr += " where  officeid= " + MyMain.Fld1;
        SqlStr += " and  projectid= " + MyMain.Fld2;


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string UpdateRfFld()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            _SqlConnection = Connection.SqlConnection;
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "UPDATE RfFld ";
            SqlStr += " SET ";
            SqlStr += MyMain.Fld1 + " = '" + MyMain.Fld2 + "'";
            sqlCommand.CommandText = SqlStr;

            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                retVal = "Ok";
            }
            else
            { retVal = "Error"; }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet GetClearanceData()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM clearancehdr ";
        SqlStr += " where ";
        SqlStr += " billno = " + MyMain.Fld1;
        if (UType.MyCtoD(MyMain.Fld2) > 0)
        {
            SqlStr += " and billyear = " + MyMain.Fld2;
        }
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }
    public DataSet GetByExporter()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM clearancehdr a ";
        SqlStr += " inner join rfcustomer b on a.exporterid=  b.customerid ";
        SqlStr += " where ";
        SqlStr += " exporterid = " + MyMain.Fld1;

        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }
    public DataSet GetDupCntr()
    {
        SqlStr = "SELECT a.tranid,a.billno,a.billyear,b.* ";
        SqlStr += " FROM clearancehdr a ";
        SqlStr += " INNER JOIN clearancedtl b ON a.tranid=b.tranid";
        SqlStr += " where ";
        SqlStr += " b.cntrnrno like '%" + MyMain.Fld1 + "%'";
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }
    public DataSet GetDupFileNo()
    {
        SqlStr = "SELECT a.tranid,a.billno,a.billyear ";
        SqlStr += " FROM clearancehdr a ";
        //SqlStr += " INNER JOIN clearancedtl b ON a.tranid=b.tranid";
        SqlStr += " where ";
        SqlStr += " a.fileno =" + MyMain.Fld1;
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }

    public DataSet GetDupBOE()
    {
        SqlStr = "SELECT a.tranid,a.billno,a.billyear ";
        SqlStr += " FROM clearancehdr a ";
        //SqlStr += " INNER JOIN clearancedtl b ON a.tranid=b.tranid";
        SqlStr += " where ";
        SqlStr += " a.boeno like '%" + MyMain.Fld1 + "%'";
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }
    public DataSet GetDupFE()
    {
        SqlStr = "SELECT a.tranid,a.billno,a.billyear ";
        SqlStr += " FROM clearancehdr a ";
        //SqlStr += " INNER JOIN clearancedtl b ON a.tranid=b.tranid";
        SqlStr += " where ";
        SqlStr += " a.formeno like '%" + MyMain.Fld1 + "%'";
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }
    public DataSet GetDupVehicle()
    {
        SqlStr = "SELECT a.tranid,a.billno,a.billyear,b.* ";
        SqlStr += " FROM clearancehdr a ";
        SqlStr += " INNER JOIN clearancedtl b ON a.tranid=b.tranid";
        SqlStr += " where ";
        SqlStr += " b.VehicleNo like '%" + MyMain.Fld1 + "%'";
        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }

    public string InsertClearanceHdr()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO clearancehdr";
            SqlStr += "(";
            SqlStr += "billno";
            SqlStr += ",billyear";
            SqlStr += ",AddDate";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + "'" + DateTime.Now + "'";
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateClearanceHdr()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "UPDATE clearancehdr";
            SqlStr += " SET ";
            SqlStr += " BillFor = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += " ,Billdate = " + UType.MyCtoDs(MyMain.Fld4);
            //SqlStr += ",[CustomerId] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[ImporterId] = " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ",[ExporterId] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ",[Country] = '" + MyMain.Fld8 + "'";
            SqlStr += ",[Commodity] = '" + MyMain.Fld9 + "'";
            SqlStr += ",[RefNo] = '" + MyMain.Fld10 + "'";
            SqlStr += ",[ShipCompany] = '" + MyMain.Fld11 + "'";
            SqlStr += ",[Vessel] = '" + MyMain.Fld12 + "'";
            SqlStr += ",[Voyage] = '" + MyMain.Fld13 + "'";
            SqlStr += ",[Port] = '" + MyMain.Fld14 + "'";
            SqlStr += ",[FileNo] = '" + MyMain.Fld15 + "'";
            SqlStr += ",[IgmNo] = " + UType.MyCtoDs(MyMain.Fld16);
            //SqlStr += ",[IgmCy] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[IndxNo] = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",[BoeNo] = '" + MyMain.Fld19 + "'";
            SqlStr += ",[BoeDate] = " + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",[CntrnrType] = '" + MyMain.Fld21 + "'";
            SqlStr += ",[NoOfPkg] = " + UType.MyCtoDs(MyMain.Fld22);
            SqlStr += ",[SbNo] = '" + MyMain.Fld23 + "'";
            SqlStr += ",[SbDate] = " + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ",[FormEno] = '" + MyMain.Fld25 + "'";

            SqlStr += ",[AwbNo] = '" + MyMain.Fld26 + "'";
            SqlStr += ",[PkgWt] = '" + UType.MyCtoDs(MyMain.Fld27) + "'";
            SqlStr += ",[CfrValue] = '" + UType.MyCtoDs(MyMain.Fld28) + "'";
            SqlStr += ",[ExchRate] = '" + UType.MyCtoDs(MyMain.Fld29) + "'";
            //SqlStr += ",[PettycashAct] = " + MyMain.Fld26 ;
            SqlStr += ",AddDate = '" + DateTime.Now + "'";
            SqlStr += " Where ";
            SqlStr += " BillNo = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and billyear = " + UType.MyCtoDs(MyMain.Fld2);


            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet GetCustomer()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfCustomer ";
        if (MyMain.Fld1.Length > 0)
        {
            SqlStr += " where ";
            SqlStr += " Status = " + MyMain.Fld1;
        }
        if (MyMain.Fld2.Length > 0)
        {
            SqlStr += " where ";
            SqlStr += " CustomerID = " + MyMain.Fld2;
        }
        DataSet ds = GetDataset(SqlStr); //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }
    public DataSet GetCntr()
    {
        SqlStr = "SELECT b.* from ClearanceHdr a ";
        SqlStr += " LEFT JOIN ClearanceDtl b ON a.TranId=b.TranId ";
        SqlStr += " Where a.billno = " + MyMain.Fld1;
        SqlStr += " and a.billyear = " + MyMain.Fld2;
        SqlStr += " ORDER BY b.TranIdDtl ";

        DataSet Res = GetDataset(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }
    public DataSet GetExpense()
    {
        SqlStr = "SELECT b.*,c.alldeddescription as ExpenseDescription from ClearanceHdr a ";
        SqlStr += " INNER JOIN Clearancebill b ON a.TranId=b.TranId ";
        SqlStr += " INNER JOIN RfAllDed c ON b.ExpenseId=c.alldedid ";
        SqlStr += " Where a.billno = " + MyMain.Fld1;
        SqlStr += " and a.billyear = " + MyMain.Fld2;
        SqlStr += " ORDER BY b.expenseid";

        DataSet Res = GetDataset(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public DataSet GetExpenseClrRpt2()
    {
        SqlStr = "SELECT b.*,c.alldeddescription as ExpenseDescription from ClearanceHdr a ";
        SqlStr += " INNER JOIN Clearancebill b ON a.TranId=b.TranId ";
        SqlStr += " INNER JOIN RfAllDed c ON b.ExpenseId=c.alldedid ";
        SqlStr += " Where a.exporterid = " + MyMain.Fld1;
        SqlStr += " and a.billfor = 3 ";
        SqlStr += " and a.billdate >= " + MyMain.Fld2;
        SqlStr += " and a.billdate <= " + MyMain.Fld3;
        SqlStr += " ORDER BY b.expenseid";

        DataSet Res = GetDataset(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;
    }

    public DataSet GetClrRpt()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " ,c.CustomerName as CustomerName ";
        SqlStr += " ,d.CustomerName as ImporterName ";
        SqlStr += " ,e.CustomerName as ExporterName ";
        SqlStr += " ,f.CustomerName as AirlineName ";
        SqlStr += " from ClearanceHdr a ";
        SqlStr += " LEFT JOIN ClearanceDtl b ON a.TranId=b.TranId ";
        SqlStr += " LEFT JOIN RfCustomer c ON a.CustomerId=c.CustomerId ";
        SqlStr += " LEFT JOIN RfCustomer d ON a.ImporterId=d.CustomerId ";
        SqlStr += " LEFT JOIN RfCustomer e ON a.ExporterId=e.CustomerId ";
        SqlStr += " LEFT JOIN RfCustomer f ON a.shipcompany=f.CustomerId";
        SqlStr += " Where a.billno = " + MyMain.Fld1;
        SqlStr += " and a.billyear = " + MyMain.Fld2;
        SqlStr += " ORDER BY b.TranIdDtl ";

        DataSet Res = GetDataset(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;

    }

    public string DeleteClearanceCntr()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "DELETE FROM clearancedtl";
            SqlStr += " where tranid = " + MyMain.Fld1;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetClrRpt2()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " ,e.CustomerName as ExporterName ";
        SqlStr += " from ClearanceHdr a ";
        //SqlStr += " INNER JOIN ClearanceDtl b ON a.TranId=b.TranId ";
        SqlStr += " INNER JOIN RfCustomer e ON a.ExporterId=e.CustomerId ";
        SqlStr += " Where a.ExporterId = " + MyMain.Fld1;
        SqlStr += " and a.billfor = 3 ";
        SqlStr += " and a.billdate >= " + MyMain.Fld2;
        SqlStr += " and a.billdate <= " + MyMain.Fld3;
        //SqlStr += " ORDER BY b.TranIdDtl ";

        DataSet Res = GetDataset(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;

    }

    public string InsertClearanceCntr()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO clearancedtl";
            SqlStr += "(";
            SqlStr += " Tranid ";
            SqlStr += ",CntrnrNo";
            SqlStr += ",VehicleNo";
            SqlStr += ",AddDate";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + "'" + MyMain.Fld2 + "'";
            SqlStr += Comma + "'" + MyMain.Fld3 + "'";
            SqlStr += Comma + "'" + DateTime.Now + "'";
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string DeleteClearanceBill()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "DELETE FROM clearancebill";
            SqlStr += " where tranid = " + MyMain.Fld1;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string InsertClearanceBill()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO clearanceBill";
            SqlStr += "(";
            SqlStr += " Tranid ";
            SqlStr += ",ExpenseId";
            SqlStr += ",ExpenseIdAmount";
            SqlStr += ",AddDate";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + "'" + DateTime.Now + "'";
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetRfCustomer()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfCustomer ";
        SqlStr += " where ";
        SqlStr += " CustomerId = " + MyMain.Fld1;

        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }
    public DataSet GetFileNo()
    {
        SqlStr = "SELECT count(*) as totfileno ";
        SqlStr += " FROM clearancehdr ";
        SqlStr += " where ";
        SqlStr += " ExporterId = " + MyMain.Fld1;

        DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return ds;
    }

    public DataSet GetAllItem()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfProduct ";
        SqlStr += " where officeid = " + MyMain.Fld1;
        SqlStr += " and Projectid = " + MyMain.Fld2;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetInvRpt1()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " ,b.*,b.quantity as itemquantity ";
        SqlStr += " ,c.*,d.* ";
        SqlStr += " from acttran a ";
        SqlStr += " INNER JOIN acttrandtl b ON a.tranId=b.tranId ";
        SqlStr += " INNER JOIN actchart c ON a.actcode=c.actcode ";
        SqlStr += " INNER JOIN rfproduct d ON b.itemId=d.productId ";
        SqlStr += " Where a.vno = " + MyMain.Fld1;
        SqlStr += " and a.cfy = " + MyMain.Fld2;

        DataSet Res = GetDatasetSpNew(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;

    }

    public DataSet MaxUserID()
    {
        SqlStr = "SELECT max(userId) as MaxUserID from UserInfo ";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public string InsertUserIPInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "delete from UserIPInfo where ipid= " + MyMain.Fld1;
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {

        }

        try
        {
            SqlStr = "INSERT INTO UserIPInfo ";
            SqlStr += "(";
            SqlStr += "IpId ";
            SqlStr += ",IpSts ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet MaxEmpID()
    {
        SqlStr = "SELECT max(empId) as MaxEmpID from UserInfo ";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public DataSet GetIPId()
    {
        SqlStr = "SELECT *   from Usermac ";
        SqlStr += " Where userid =   '" + MyMain.Fld1 + "'";
        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public string InsertUserMac()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Delete from UserMac where UserId = " + MyMain.Fld1;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
        }

        try
        {
            SqlStr = "INSERT INTO UserMac ";
            SqlStr += "(";
            SqlStr += "UserId ";
            SqlStr += ",UserIP ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet MaxIPID()
    {
        SqlStr = "SELECT max(IPId) as MaxIPID from UserIPInfo ";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }

    public DataSet InventoryReport()
    {
        SqlStr = " select a.productid,a.productdescription,a.productBalance,d.*,b.*,c.* from rfproduct a ";
        SqlStr += " inner join acttrandtl b on a.productid = b.itemid ";
        SqlStr += " inner join acttran c on b.tranid = c.tranid";
        SqlStr += " inner join actrfvtype d on c.vtypeid= d.vtypeid";
        SqlStr += " where c.trandate >=" + MyMain.Fld1;
        SqlStr += " and   c.trandate <=" + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " and   a.productid =" + MyMain.Fld3;
        }
        DataSet Res = GetDatasetSpNew(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;

    }
    public DataSet GetImage()
    {
        SqlStr = "SELECT * ";
        SqlStr += " from RfFld ";

        DataSet Res = GetDataset(SqlStr);  //SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return Res;

    }
    public DataSet GetQuestion()
    {
        DataSet ds1 = null;
        try
        {
            SqlStr = "SELECT a.*,b.imagepath,c.* from RefQuestionAnswer a ";
            SqlStr += " inner join refquestion b on a.questionid= b.questionid";
            SqlStr += " inner join refAnswer c on a.Answerid= c.Answerid";
            SqlStr += " ORDER BY a.questionid";
            ds1 = GetDatasetSpNew(SqlStr);

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public string UpdateProject()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "UPDATE RfProject";
            SqlStr += " SET ";
            SqlStr += " [ProjectDescription] = '" + MyMain.Fld3 + "'";
            SqlStr += " ,[oAddress] = '" + MyMain.Fld4 + "'";
            SqlStr += ",[oAreaId] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[oCityId] = " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ",[oTel] = '" + MyMain.Fld7 + "'";
            SqlStr += ",[oCell] = '" + MyMain.Fld8 + "'";
            //SqlStr += ",[oLogoPath] = '" + MyMain.Fld9 + "'";
            SqlStr += ",[oTypeId] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[oEmail] = '" + MyMain.Fld11 + "'";
            //SqlStr += ",[oAltEmail] = '" + MyMain.Fld12 + "'";
            SqlStr += ",[EffDate] = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",[EndDate] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[ModuleId] = " + UType.MyCtoDs(MyMain.Fld15);

            SqlStr += " Where ";
            SqlStr += " [OfficeId] = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and [ProjectId] = " + UType.MyCtoDs(MyMain.Fld2);


            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertProject()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "INSERT INTO RfProject";
            SqlStr += "(";
            SqlStr += "OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",ProjectDescription  ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + Sqote + MyMain.Fld3 + Sqote;
            SqlStr += ")";

            retVal = NonQryCmdSp(SqlStr);
            //_SqlConnection.Open();

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetMaxUserID()
    {
        SqlStr = "SELECT MAX(userid) as maxUserID FROM userinfo ";


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetModuleFromUserID()
    {
        //SqlStr = "select c.moduledescription from rfproject a";
        //SqlStr += " inner join userinfo b on a.officeid=b.officeid and a.projectid= b.projectid ";
        //SqlStr += " inner join rfmodule c on a.moduleid = c.moduleid ";
        //SqlStr += " Where b.officeid = " +  MyMain.Fld1 ;
        //SqlStr += " and   b.projectid = " + MyMain.Fld2 ;
        //SqlStr += " and   b.UserID = " +   MyMain.Fld3  ;

        SqlStr = "select a.moduledescription,a.moduleID from rfmodule a";
        SqlStr += " inner join rfproject b on a.moduleid=b.moduleid ";
        SqlStr += " Where b.officeid = " + MyMain.Fld1;
        SqlStr += " and   b.projectid = " + MyMain.Fld2;
        DataSet Res = GetDatasetSpNew(SqlStr);
        return Res;
    }
    public string InsertUserrole()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO userrole ";
            SqlStr += "(";
            SqlStr += "userid ";
            // SqlStr += ",DepartId ";
            SqlStr += ",userRoleId ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            // SqlStr += Comma + MyMain.Fld3;
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;

    }
    public string UpdateUserMac()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "UPDATE userinfo";
            SqlStr += " SET ";
            SqlStr += " [UserMac] = '" + MyMain.Fld4 + "'";
            SqlStr += ", [UserIP] = '" + MyMain.Fld5 + "'";
            SqlStr += ", [FirstTime] = " + MyMain.Fld6;
            SqlStr += " Where ";
            SqlStr += " [OfficeId] = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and [ProjectId] = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += " and [UserId] = " + UType.MyCtoDs(MyMain.Fld3);


            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    //Urdu
    public DataSet GetUrduMenu()
    {
        DataSet ds1 = null;
        try
        {
            SqlStr = "SELECT rm.[MenuID] ";
            SqlStr += ", rm.[MenuText] ";
            SqlStr += ", rm.[MenuPath] ";
            SqlStr += ", rm.[ImagePath] ";
            SqlStr += ", rm.[MenuPath1] ";
            SqlStr += ", rm.[ImagePath1] ";
            SqlStr += " FROM uRfMenu as rm ";

            //SqlStr += " WHERE ";
            //SqlStr += " ui.officeid = " + MyMain.Fld1;

            SqlStr += " ORDER BY rm.MenuId";
            ds1 = GetDataset(SqlStr);

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public string InsertOfficeChart()
    {
        string retVal = string.Empty;

        try
        {
            SqlStr = "Delete from ";
            SqlStr += " [Actchart] ";
            SqlStr += " where  OfficeID = " + MyMain.Fld1;
            SqlStr += " and ProjectID = " + MyMain.Fld2;
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        try
        {
            SqlStr = "INSERT INTO Actchart (OfficeID, ProjectID,ActCode,actcode1,ActDesc,ActStatus,ActLevel) ";
            //SqlStr += " SELECT ChartId + (select max(chartid) from refmodulechart) ";
            SqlStr += " SELECT  ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += ",[ActCode],actcode1,ActDesc,ActStatus,ActLevel     FROM [RefModuleChart] ";
            //SqlStr += " where  moduleid= " + MyMain.Fld3;


            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;

    }

    public string InsertOffChart()
    {
        string retVal = string.Empty;
        DataSet Ds = null;
        string tOffChartID = "0";

        bool InsRec = false;
        try
        {
            SqlStr = "SELECT oc.officechartid  ";
            SqlStr += " FROM officechart oc ";
            SqlStr += " WHERE ";
            SqlStr += " oc.officeid = " + MyMain.Fld1;
            SqlStr += " and oc.projectid = " + MyMain.Fld2;
            SqlStr += " and oc.actcode = " + MyMain.Fld3;
            Ds = GetDatasetSpNew(SqlStr);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                tOffChartID = Ds.Tables[0].Rows[0]["officechartid"].ToString();
                InsRec = true;
            }

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        try
        {
            if (UType.MyCtoD(tOffChartID) < 1)
            {
                SqlStr = "SELECT max(officechartid)  ";
                SqlStr += " FROM officechart  ";
                Ds = GetDatasetSpNew(SqlStr);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    tOffChartID = Ds.Tables[0].Rows[0][0].ToString();
                }

                tOffChartID = Convert.ToString(UType.MyCtoD(tOffChartID) + 1);
                SqlStr = "INSERT INTO officechart (OfficeChartID, OfficeID, ProjectID,ActCode) ";
                SqlStr += " values(";
                SqlStr += tOffChartID;
                SqlStr += Comma + MyMain.Fld1;
                SqlStr += Comma + MyMain.Fld2;
                SqlStr += Comma + MyMain.Fld3;
                SqlStr += ")";
                retVal = NonQryCmdSp(SqlStr);

                InsRec = true;
            }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        try
        {
            if (InsRec)
            {
                SqlStr = "insert into actchart (chartid,projectid,actcode,actdesc) ";
                SqlStr += " values (";
                SqlStr += tOffChartID;
                SqlStr += Comma + "1";
                SqlStr += Comma + MyMain.Fld3;
                SqlStr += Comma + "' '";
                SqlStr += ")";
                retVal = NonQryCmdSp(SqlStr);
                InsRec = true;
            }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }

        return retVal;

    }
    public string UpdateRef1()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "Update RefTbl Set ";
            SqlStr += " CFY = " + MyMain.Fld3;
            SqlStr += ",SDFyear = " + MyMain.Fld4;
            SqlStr += ",EDFyear = " + MyMain.Fld5;

            SqlStr += " where";
            SqlStr += " OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;


            //DebtorActCode
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string InsertDemo()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "INSERT INTO Demo";
            SqlStr += "(";
            SqlStr += "UserName";
            SqlStr += ",EmailID";
            SqlStr += ",Cellno";
            SqlStr += ",AddDate ";
            SqlStr += ",Addtime";
            SqlStr += ",DemoSts";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += Sqote + MyMain.Fld1 + Sqote;
            SqlStr += Comma + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmmssff");
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += ")";

            retVal = NonQryCmdSp(SqlStr);
            //_SqlConnection.Open();

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string UpdateDemo()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "Update Demo Set ";
            SqlStr += " DemoSts = " + MyMain.Fld2;

            SqlStr += " where";
            SqlStr += " CellNo = " + MyMain.Fld1;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetMainGridData()
    {
        SqlStr = "SELECT  ";
        SqlStr += "  a.* from manifest a ";
        SqlStr += " where a.OfficeId = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " and a.docno = " + MyMain.Fld3;
        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.docno = " + MyMain.Fld4;
        }
        if (MyMain.Fld5.Length > 0)
        {
            SqlStr += " and a.vessel like " + "'%" + MyMain.Fld5 + "%'";
        }
        SqlStr += " order by docno desc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public DataSet GetQueryData()
    {
        SqlStr = "SELECT a.DocNo,a.DocYear,a.Vessel,b.MBLNo,b.HBLNo,b.JobNo,c.actdesc as Consignee,d.actdesc as Shipper     ";

        SqlStr += "  from manifest a ";

        SqlStr += "   left join jobinfo b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.DocNo = b.DocNo ";
        SqlStr += "  left join ActChart c  on b.OfficeId = c.OfficeId and b.projectid = c.projectid and b.consignee = c.actcode ";
        SqlStr += "  left join ActChart d  on b.OfficeId = c.OfficeId and b.projectid = c.projectid and b.shipper = d.actcode ";

        SqlStr += " where a.OfficeId = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " and b.jobno = " + MyMain.Fld3;
        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and b.Mblno = " + MyMain.Fld4;
        }
        if (UType.MyCtoD(MyMain.Fld5) > 0)
        {
            SqlStr += " and b.docno = " + MyMain.Fld5;
        }
        if (MyMain.Fld6.Length > 0)
        {
            SqlStr += " and a.vessel like " + "'%" + MyMain.Fld6 + "%'";
        }
        if (MyMain.Fld7.Length > 0)
        {
            SqlStr += " and c.actdesc like " + "'%" + MyMain.Fld7 + "%'";
        }
        if (MyMain.Fld8.Length > 0)
        {
            SqlStr += " and d.actdesc like " + "'%" + MyMain.Fld8 + "%'";
        }
        if (UType.MyCtoD(MyMain.Fld9) > 0)
        {
            SqlStr += " and b.hblno = " + MyMain.Fld9;
        }
        SqlStr += " order by a.docno desc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public DataSet GetQueryAccount()
    {
        SqlStr = "select distinct a.vno,a.ActCode ,c.VtypeDescription ,b.ActDesc, a.Narration,a.TranDate,d.jobno,d.customer,chargestype,c.VtypeId from ActTran a ";
        SqlStr += " inner join ActChart b on a.actcode = b.ActCode ";
        SqlStr += " inner join ActRfVtype c on a.VTypeId = c.VtypeId ";
        SqlStr += " left join Charges d on ";
        SqlStr += " a.Officeid = d.Officeid and a.projectid = d.projectid and a.Vno = d.Vno ";
        SqlStr += " where a.OfficeId = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " and a.vno = " + MyMain.Fld3;
        }
        if (MyMain.Fld4.Length > 0)
        {
            SqlStr += " and b.actdesc like " + "'%" + MyMain.Fld4 + "%'";
        }

        if (UType.MyCtoD(MyMain.Fld5) > 0)
        {
            SqlStr += " and a.vauto is NULL";
        }
        if (MyMain.Fld6.Length > 0)
        {
            SqlStr += " and a.vessel like " + "'%" + MyMain.Fld6 + "%'";
        }



        SqlStr += " order by a.vno desc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetVesselMBLData()
    {
        SqlStr = "SELECT  ";
        SqlStr += "  a.* ";
        SqlStr += "  ,b.mblno,b.MblDate,b.SNo  ";
        SqlStr += "  ,c.hblno,c.hblDate,c.SNo ";
        SqlStr += "  from manifest a ";
        SqlStr += "  left join MBLInfo b on a.DocNo=b.DocNo  ";
        SqlStr += "  left join HBLInfo c on a.DocNo=c.DocNo  ";
        SqlStr += " where a.OfficeId = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " and a.docno = " + MyMain.Fld3;
        }
        SqlStr += " and b.mblno = " + Sqote + MyMain.Fld7 + Sqote;
        SqlStr += " and c.hblno = " + Sqote + MyMain.Fld8 + Sqote;


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public DataSet GetVesselMBLJobData()
    {
        SqlStr = "SELECT b.Vessel,b.Voyage,a.*,c.MblNo,c.MblDate,d.hblNo,d.hblDate  FROM jobinfo a  ";
        SqlStr += " inner join manifest b on b.officeid = a.officeid and b.ProjectId = a.ProjectId and a.docno = b.DocNo ";
        SqlStr += " inner join mblinfo c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.docno = c.DocNo  and a.MBLno=c.MblNo";
        SqlStr += "  inner join hblinfo d on a.officeid = d.officeid and a.ProjectId = d.ProjectId and a.docno = d.DocNo  and a.HBLNo=d.hblNo";

        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;


        //SqlStr = "SELECT  ";
        //SqlStr += "  a.* ";
        //SqlStr += "  ,b.mblno,b.MblDate,b.SNo  ";
        //SqlStr += "  ,c.hblno,c.hblDate,c.SNo ";
        //SqlStr += "  ,d.consignee,d.shipper ";
        //SqlStr += "  from manifest a ";
        //SqlStr += "  left join MBLInfo b on a.OfficeId = b.OfficeId and a.ProjectId= b.ProjectId and a.DocNo=b.DocNo  ";
        //SqlStr += "  left join HBLInfo c on a.OfficeId = c.OfficeId and a.ProjectId= c.ProjectId and  a.DocNo=c.DocNo  ";
        //SqlStr += "  left join jobinfo d on a.OfficeId = b.OfficeId and a.ProjectId= b.ProjectId and c.mblno = d.mblno and c.hblno = d.hblno ";
        //SqlStr += " where a.OfficeId = " + MyMain.Fld1;
        //SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        //if (UType.MyCtoD(MyMain.Fld3) > 0)
        //{
        //    SqlStr += " and a.docno = " + MyMain.Fld3;
        //}
        //SqlStr += " and c.mblno = " + Sqote + MyMain.Fld7 + Sqote;
        //SqlStr += " and c.hblno = " + Sqote + MyMain.Fld8 + Sqote;


        //DataSet ds = GetDatasetSpNew(SqlStr);
        //return ds;

    }

    public DataSet GetCustomerData()
    {
        SqlStr = "SELECT a.* ";

        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCustomer a  ";
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.StatusID = " + MyMain.Fld4;
        //SqlStr += " or a.StatusID = 0 ";

        SqlStr += " order by a.sortno , a.CustomerID,a.CustomerName ";
        if (MyMain.Fld25 == "1")
        {
            SqlStr = "SELECT a.CustomerID as actcode, a.CustomerName as actdesc ";

            SqlStr += " FROM "; // officechart oc   ";
            SqlStr += "  RfCustomer a  ";
            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.StatusID = " + MyMain.Fld4;
            SqlStr += " order by a.sortno , a.CustomerID,a.CustomerName ";
        }
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetArrivalInfo()
    {

        SqlStr = " select b.arrivaldate,c.dodate from jobinfo a ";

        SqlStr += " inner join manifest b on a.DocNo = b.DocNo and a.docyear = b.docyear"; // officechart oc   ";
        SqlStr += "  left join DeliveryOrder c on a.JobNo = c.JobNo   ";
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and a.jobyear = " + MyMain.Fld4;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetCustomerDescription()
    {
        SqlStr = "SELECT a.*,b.country_short, city_short ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += " RfCustomer a  ";
        SqlStr += " left join RfCities b on a.CustomerTel = b.Country_ID and a.CustomerTel1 = b.city_id  ";
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.CustomerID = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetAccountExpense()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ActChart a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld3) == 1)
        {
            SqlStr += " and SUBSTRING(CONVERT(varchar(20), a.ActCode) ,1,1)='4'  and len(a.ActCode) >6";
        }
        if (UType.MyCtoD(MyMain.Fld3) == 2)
        {
            SqlStr += " and SUBSTRING(CONVERT(varchar(20), a.ActCode) ,1,6)='102003' and len(a.ActCode) >6";
        }
        if (UType.MyCtoD(MyMain.Fld3) == 3)
        {
            SqlStr += " and SUBSTRING(CONVERT(varchar(20), a.ActCode) ,1,6)='202001' and len(a.ActCode) >6";
        }
        if (UType.MyCtoD(MyMain.Fld3) == 6)
        {
            SqlStr += " and SUBSTRING(CONVERT(varchar(20), a.ActCode) ,1,6)='102003' and len(a.ActCode) >6";
            SqlStr += " and clientstatus =" + MyMain.Fld3;
        }
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetAccountExpense1()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ActChart a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += "and a.ActCode=" + MyMain.Fld3;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetAccountConsignee()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ActChart a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld3) < 100)
        {
            SqlStr += " and a.ClientStatus = " + MyMain.Fld3;
        }
        if (UType.MyCtoD(MyMain.Fld3) > 100)
        {
            SqlStr += " and actcode > " + MyMain.Fld3 + " and actcode <= " + MyMain.Fld3 + "9999";
            //;  102003 and actcode <= 1020039999 " + MyMain.Fld3;
        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetAccountOneConsignee()
    {
        SqlStr = " SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ActChart a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.ActCode = " + MyMain.Fld3;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetAccountnCity()
    {
        SqlStr = " select b.country_name,b.city_name,b.country_short,b.city_short,a.* from actchart a ";
        SqlStr += " left join RfCities b on  a.cityid= b.city_id "; // officechart oc   ";
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.ActCode = " + MyMain.Fld3;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetAccountOneConsigneeOld()
    {
        SqlStr = " SELECT b.Country_code as Country_Name,c.City_short as City_Name,c.Country_Name as Country_Namedtl,c.City_Name as City_Namedtl,a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ActChart a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 

        SqlStr += " left join rfcountries b on a.countryid = b.country_Id  ";
        SqlStr += " left join rfcities c on  a.countryid = c.country_Id and a.cityid = c.city_id  ";
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.ActCode = " + MyMain.Fld3;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetRfPort()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfPort a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 

        SqlStr += " order by a.PortID,a.PortDescription ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetLocationData()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfLocation a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.CountryID = " + MyMain.Fld1;
            if (UType.MyCtoD(MyMain.Fld2) > 0)
            {
                SqlStr += "  and  ";
                SqlStr += " a.CityID = " + MyMain.Fld2;
            }
            if (UType.MyCtoD(MyMain.Fld3) > 0)
            {
                SqlStr += "  and  ";
                SqlStr += " a.LocationStatusID = " + MyMain.Fld3;
            }
            if (UType.MyCtoD(MyMain.Fld4) > 0)
            {
                SqlStr += "  and  ";
                SqlStr += " a.LocationID = " + MyMain.Fld4;
            }
            SqlStr += " or a.CountryID = 0 ";
        }

        SqlStr += " order by a.CountryID,a.LocationDescription ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetCountryDataNew()
    {
        SqlStr = "select count(a.Country_Id), a.Country_Id, Country_Name from rfcities a  ";

        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.Country_Id = " + MyMain.Fld1;
        }
        if (MyMain.Fld10.Length > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.Country_name like '" + MyMain.Fld10 + "'";
        }
        SqlStr += " group by a.Country_Id,a.Country_Name having count(a.Country_Name) > 1 order by a.Country_Name ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetCountryManifest()
    {
        SqlStr = "select count(a.Country_Id), a.Country_Id, Country_Name from RfCities a  ";

        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.CountryID = " + MyMain.Fld1;
        }

        SqlStr += " group by a.Country_Id,a.Country_Name having count(a.Country_Name) > 1 order by a.Country_Name ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetCountryData()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCountry a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.CountryID = " + MyMain.Fld1;
        }

        SqlStr += " order by a.CountryDescription ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetCountriesData()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCountries a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.Country_ID = " + MyMain.Fld1;
        }

        SqlStr += " order by a.Country_Name ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetCountriesddl()
    {
        SqlStr = "SELECT distinct   country_Name   ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCities a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.Country_ID = " + MyMain.Fld1;
        }

        SqlStr += " order by a.Country_Name ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetCityddl()
    {
        SqlStr = "SELECT distinct   city_Name   ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCities a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.city_ID = " + MyMain.Fld1;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetCityData()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCities a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.City_ID = " + MyMain.Fld1;
        }
        if (UType.MyCtoD(MyMain.Fld2) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.Country_ID = " + MyMain.Fld2;
        }
        if (UType.MyCtoD(MyMain.Fld10) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.Country_ID = " + MyMain.Fld2;
        }
        SqlStr += " order by a.City_Name ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public DataSet GetCityData1()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCities a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.City_ID = " + MyMain.Fld1;
        }
        if (UType.MyCtoD(MyMain.Fld2) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.Country_ID = " + MyMain.Fld2;
        }
        if (UType.MyCtoD(MyMain.Fld10) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.Country_ID = " + MyMain.Fld2;
        }
        SqlStr += " order by a.City_Name ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetCityDataNew()
    {
        SqlStr = "SELECT distinct a.Country_id,a.country_name,a.City_id,a.city_name ";

        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCities a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
                                    // SqlStr += " inner join rfCountries b on a.country_id = b.country_id  ";
        SqlStr += " where  ";
        if (MyMain.Fld1 == "")
        {
            if (MyMain.Fld11 == "11")
            {
                SqlStr += " a.Country_Name like '%" + MyMain.Fld12 + "%'";
            }
            else
            {
                SqlStr += " 0 <> 0";
            }

        }
        if (MyMain.Fld1 != "")
        {
            SqlStr += " a.City_Name like '%" + MyMain.Fld1 + "%'";
        }

        SqlStr += " order by a.City_Name ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetCountryDataNew1()
    {
        SqlStr = "SELECT distinct a.Country_id,a.country_name ";

        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCities a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
                                    // SqlStr += " inner join rfCountries b on a.country_id = b.country_id  ";
        SqlStr += " where  ";
        if (MyMain.Fld1 == "")
        {
            SqlStr += " 0 <> 0";

        }
        if (MyMain.Fld1 != "")
        {
            SqlStr += " a.country_name like '%" + MyMain.Fld1 + "%'";
        }

        SqlStr += " order by a.country_name ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }


    public DataSet GetCityDataAct()
    {
        SqlStr = " SELECT b.city_ID as CustomerID,b.City_name as CustomerName, b.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ActChart a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 

        SqlStr += " left join rfcities b on a.countryid = b.country_Id  ";
        // SqlStr += " left join rfcities c on  a.countryid = c.country_Id and a.cityid = c.city_id  ";
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.ActCode = " + MyMain.Fld3;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetRfPCT()
    {
        SqlStr = " SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfPCT a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " where  ";
            //SqlStr += " a.officeid = " + MyMain.Fld1;
            //SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " a.Code = " + MyMain.Fld3;
        }
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetCityDataBk()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCity a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.CityID = " + MyMain.Fld1;
        }
        if (UType.MyCtoD(MyMain.Fld2) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.CountryID = " + MyMain.Fld2;
        }
        SqlStr += " order by a.CityDescription ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetIncoTermData()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfIncoTerm a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " where  ";
            //SqlStr += " a.officeid = " + MyMain.Fld1;
            //SqlStr += " and ac.ProjectId = " + MyMain.Fld2;

            SqlStr += " a.IncoTermID = " + MyMain.Fld3;
        }
        SqlStr += " order by a.IncoTermDescription ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    #region Customer
    public DataSet GetCustomerStatus()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCustomerStatus a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " where  ";
            //SqlStr += " a.officeid = " + MyMain.Fld1;
            //SqlStr += " and ac.ProjectId = " + MyMain.Fld2;

            SqlStr += " a.Statusid = " + MyMain.Fld3;
        }

        SqlStr += " order by a.StatusName ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }



    public string InsertRfCustomerStatus()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfCustomerStatus";
            SqlStr += "(";
            SqlStr += "StatusId";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.EmpId;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateRfCustomerStatus()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update RfCustomerStatus Set ";
            // SqlStr += " CustomerId = " + MyMain.EmpId;
            SqlStr += "StatusName = " + Sqote + MyMain.EmpName + Sqote;

            SqlStr += " Where Statusid = " + MyMain.EmpId;
            //string str2 = "select * from rfCustomer where 0<>0";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetCustomerStatusMax()
    {
        SqlStr = "SELECT max(a.Statusid) as MaxCustomerStatusID  ";
        SqlStr += " from RfCustomerStatus a ";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    #endregion

    public DataSet GetTranDetail()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Acttrandetail a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            //    SqlStr += " where  ";
            //    //SqlStr += " a.officeid = " + MyMain.Fld1;
            //    //SqlStr += " and ac.ProjectId = " + MyMain.Fld2;

            //    SqlStr += " a.vno = " + MyMain.Fld3;
        }

        //        SqlStr += " order by a.StatusName ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetTran()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Acttran a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (MyMain.Fld3 == "0")
        {
            SqlStr += " where 0 <> 0 ";
            //SqlStr += " a.officeid = " + MyMain.Fld1;
            //    //SqlStr += " and ac.ProjectId = " + MyMain.Fld2;

            //    SqlStr += " a.vno = " + MyMain.Fld3;
        }

        //        SqlStr += " order by a.StatusName ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetCurrencyData()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCurrency a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.CurrencyID = " + MyMain.Fld1;
        }

        SqlStr += " order by a.CurrencyDescription ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    #region Region Manifest
    public DataSet GetManifest()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Manifest a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            //SqlStr += " a.officeid = " + MyMain.Fld1;
            //SqlStr += " and ac.ProjectId = " + MyMain.Fld2;

            SqlStr += " a.docno = " + MyMain.Fld1;
        }

        SqlStr += " order by a.docno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetManifestGrid()
    {
        SqlStr = "select a.docno,a.voyage,a.Vessel,a.VIRNo from expmanifest a ";

        SqlStr += " order by a.adddate desc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxManifest()
    {
        SqlStr = "SELECT max(a.docno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Manifest a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertManifest()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO Manifest";
            SqlStr += "(";
            SqlStr += "OfficeID";
            SqlStr += ",ProjectID";
            SqlStr += ",docno";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += UType.MyCtoDs(MyMain.Fld1);
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateManifest()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Manifest Set ";
            //SqlStr += "officeID = " + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += "officeID = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += ",projectid = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ", [DocYear] = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += ", [ManifestSts] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[OperationID] = " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ",[ShippingLineID] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ",[BookNo] = " + Sqote + UType.Chk10(MyMain.Fld8) + Sqote;
            SqlStr += ",[CustomID] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",[GuaranteeNo] =+ " + Sqote + UType.Chk10(MyMain.Fld10) + Sqote;
            SqlStr += ",[Vessel] = " + Sqote + UType.Chk10(MyMain.Fld11) + Sqote;
            SqlStr += ",[Voyage] = " + Sqote + UType.Chk10(MyMain.Fld12) + Sqote;
            SqlStr += ",[CountryID] = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",[ArrivalDate] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[LastPortCallID] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[IGMDate] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[BerthName] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[GroundDate] = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",[CFS] = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",[GroundTime]  = " + Sqote + UType.Chk10(MyMain.Fld20) + Sqote;
            SqlStr += ",[ShippingLicenseID] = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ",[IGMNo] = " + UType.MyCtoDs(MyMain.Fld22);
            SqlStr += ",[LocationID] = " + UType.MyCtoDs(MyMain.Fld23); //local port
            SqlStr += ",[VIRNo] = " + Sqote + UType.Chk10(MyMain.Fld24) + Sqote;
            SqlStr += ",[ShippingCompanyID] = " + UType.MyCtoDs(MyMain.Fld25);
            SqlStr += ",[StartingIndex] = " + UType.MyCtoDs(MyMain.Fld26);
            SqlStr += ",[CaptainName]  = " + Sqote + UType.Chk10(MyMain.Fld27) + Sqote;
            SqlStr += ",[PreAlertDate] = " + UType.MyCtoDs(MyMain.Fld28);
            //SqlStr += ",[BerthName] = " + Sqote + UType.Chk10(MyMain.Fld29) + Sqote;
            SqlStr += ",[AmendmentDate] = " + UType.MyCtoDs(MyMain.Fld30);
            SqlStr += ", [Remarks]= " + Sqote + UType.Chk10(MyMain.Fld31) + Sqote;
            SqlStr += ",[OperatorCode] = " + UType.MyCtoDs(MyMain.Fld32);
            SqlStr += ",[ManifestRefNo] = " + Sqote + UType.Chk10(MyMain.Fld33) + Sqote;
            SqlStr += ", [SameBottomCargo] = " + Sqote + UType.Chk10(MyMain.Fld34) + Sqote;
            SqlStr += ",[DocRecShipingLine] = " + UType.MyCtoDs(MyMain.Fld38);
            SqlStr += ",[ManifestToCustom] = " + UType.MyCtoDs(MyMain.Fld35);
            SqlStr += ",[ShedId] = " + UType.MyCtoDs(MyMain.Fld36);
            SqlStr += ",[IsManifest] = " + UType.MyCtoDs(MyMain.Fld37);
            SqlStr += ",[TerminalID] = " + UType.MyCtoDs(MyMain.Fld40);
            SqlStr += ",[TranID1] = " + Sqote + MyMain.Fld41 + Sqote;
            SqlStr += ",[UserId] = " + UType.MyCtoDs(MyMain.Fld42);


            //SqlStr += "StatusName = " + Sqote + MyMain.EmpName + Sqote;

            SqlStr += " Where docno = " + MyMain.Fld1;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    #endregion

    #region MyRegion MBL
    public DataSet GetMBLInfo()
    {
        SqlStr = "SELECT a.*, 0 as  [JobNature1], 0 as [JobType1]";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  MBLInfo a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.docno = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.sno = " + MyMain.Fld4;
        }
        if (UType.MyCtoD(MyMain.Fld5) > 0)
        {
            SqlStr += " and a.mblno = " + MyMain.Fld5;
        }
        SqlStr += " order by a.docno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetMBLInfoGrid()
    {
        //SqlStr = "SELECT a.*, 0 as  [JobNature1], 0 as [JobType1],b.vessel,b.docyear,igmno,b.voyage,b.arrivaldate ";
        SqlStr = "SELECT b.docno,b.docyear,b.vessel,a.mblno,a.mbldate,b.voyage,b.arrivaldate ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  MBLInfo a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        SqlStr += "  inner join manifest b on a.officeid = b.officeid and a.projectid = b.projectid and a.docno = b.docno "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";

            SqlStr += "  a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.docno = " + MyMain.Fld3;
        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.sno = " + MyMain.Fld4;
        }
        if (UType.MyCtoD(MyMain.Fld5) > 0)
        {
            SqlStr += " and a.mblno = " + MyMain.Fld5;
        }
        SqlStr += " order by a.docno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetMaxMBLInfo()
    {
        SqlStr = "SELECT max(a.sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  MBLInfo a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += "  a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.docno = " + MyMain.Fld3;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertMBLInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO MBLInfo";
            SqlStr += "(";
            SqlStr += "docno";
            SqlStr += ",OfficeID";
            SqlStr += ",ProjectID";
            SqlStr += ",sno";
            SqlStr += ",jobnature";
            SqlStr += ",jobtype";

            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;

            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateMBLInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update MBLInfo Set ";

            SqlStr += "officeID = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ",projectid = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += ",[JobNature] = " + UType.MyCtoDs(MyMain.Fld5);

            SqlStr += ",[JobType] = " + UType.MyCtoDs(MyMain.Fld6);


            SqlStr += ", [JobNo] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ", [JobDate] = " + UType.MyCtoDs(MyMain.Fld8);


            SqlStr += ",[MblNo] = " + Sqote + UType.Chk10(MyMain.Fld9) + Sqote;
            SqlStr += ",[MblDate] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[DeStuffingDate] = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",[Totalhbl] = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ",[Volume] = " + Sqote + UType.Chk10(MyMain.Fld13) + Sqote;
            SqlStr += ",[TotalContainers] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[FT20] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[FT40] = " + UType.MyCtoDs(MyMain.Fld16);




            SqlStr += " Where docno = " + MyMain.Fld1;
            SqlStr += " and officeid = " + MyMain.Fld3;
            SqlStr += " and projectid = " + MyMain.Fld4;
            SqlStr += " and sno = " + MyMain.Fld2;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region MyRegion HBL
    public DataSet GetHBLInfo()
    {
        SqlStr = "SELECT a.*, '0' as  [IndexType1], '0' as  [JobNature1]";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  HBLInfo a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += "  a.docno = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.sno = " + MyMain.Fld4;
        }
        if (UType.MyCtoD(MyMain.Fld5) > 0)
        {
            SqlStr += " and a.mblno = " + MyMain.Fld5;
        }
        if (UType.MyCtoD(MyMain.Fld6) > 0)
        {
            SqlStr += " and a.mblsno = " + MyMain.Fld6;
        }

        SqlStr += " order by a.docno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxHBLInfo()
    {
        SqlStr = "SELECT max(a.sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  HBLInfo a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.OfficeId = " + MyMain.Fld1;
            SqlStr += " and a.Projectid = " + MyMain.Fld2;
            SqlStr += " and a.docno = " + MyMain.Fld3;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertHBLInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO HBLInfo";
            SqlStr += "(";
            SqlStr += "docno";
            SqlStr += ",OfficeID";
            SqlStr += ",ProjectID";
            SqlStr += ",sno";
            SqlStr += ",jobnature";
            SqlStr += ",Indextype";
            SqlStr += ",MBLSNO";
            //SqlStr += ",MBLNO";
            //SqlStr += ",MBLdate";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + MyMain.Fld7;
            //SqlStr += Comma + MyMain.Fld8;
            //SqlStr += Comma + Sqote+ MyMain.Fld9 +Sqote;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateHBLInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update HBLInfo Set ";

            SqlStr += "officeID = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ",projectid = " + UType.MyCtoDs(MyMain.Fld4);

            SqlStr += ",IndexNo = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[IndexType] = " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ", [JobNo] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ", [JobDate] = " + UType.MyCtoDs(MyMain.Fld8);

            SqlStr += ",[JobNature] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",[HblNo] = " + Sqote + UType.Chk10(MyMain.Fld10) + Sqote;
            SqlStr += ",[HblDate] = " + UType.MyCtoDs(MyMain.Fld11);

            SqlStr += ",[MblNo] = " + Sqote + UType.Chk10(MyMain.Fld12) + Sqote;
            SqlStr += ",[MblDate] = " + UType.MyCtoDs(MyMain.Fld13);


            SqlStr += ",[ClientName] = " + Sqote + UType.Chk10(MyMain.Fld14) + Sqote;
            SqlStr += ",[Volume] = " + Sqote + UType.Chk10(MyMain.Fld15) + Sqote;



            SqlStr += ",[TotalContainers] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[FT20] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[FT40] = " + UType.MyCtoDs(MyMain.Fld18);


            SqlStr += ",[Package] = " + Sqote + UType.Chk10(MyMain.Fld19) + Sqote;
            SqlStr += ",[PortofLoading] = " + Sqote + UType.Chk10(MyMain.Fld20) + Sqote;

            SqlStr += " Where docno = " + MyMain.Fld1;
            SqlStr += " and officeID = " + MyMain.Fld3;
            SqlStr += " and ProjectID = " + MyMain.Fld4;
            SqlStr += " and sno = " + MyMain.Fld2;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string DeleteHBLInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "delete HBLInfo ";
            SqlStr += " Where docno = " + MyMain.Fld1;
            SqlStr += " and officeID = " + MyMain.Fld2;
            SqlStr += " and ProjectID = " + MyMain.Fld3;
            SqlStr += " and sno = " + MyMain.Fld4;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    #endregion


    #region ArrivalNote
    public DataSet GetArrivalNote()
    {
        SqlStr = "SELECT  ";
        SqlStr += "  a.* from ArrivalNote a ";
        //SqlStr += " where a.OfficeId = " + MyMain.Fld1;
        //SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public string InsertArrivalNote()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO ArrivalNote";
            SqlStr += "(";
            SqlStr += "docno";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string UpdateArrivalNote()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update MBLInfo Set ";

            SqlStr += "officeID = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ",projectid = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += ",[JobNature] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[JobType] = " + UType.MyCtoDs(MyMain.Fld6);

            SqlStr += ", [JobNo] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ", [JobDate] = " + UType.MyCtoDs(MyMain.Fld8);


            SqlStr += ",[MblNo] = " + Sqote + UType.Chk10(MyMain.Fld9) + Sqote;
            SqlStr += ",[MblDate] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[DeStuffingDate] = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",[Totalhbl] = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ",[Volume] = " + Sqote + UType.Chk10(MyMain.Fld13) + Sqote;
            SqlStr += ",[TotalContainers] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[FT20] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[FT40] = " + UType.MyCtoDs(MyMain.Fld16);




            SqlStr += " Where docno = " + MyMain.Fld1;
            SqlStr += " and sno = " + MyMain.Fld2;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region JobInfo
    public DataSet GetJobInfo()
    {
        SqlStr = "SELECT  ";
        SqlStr += "  a.* from JobInfo a ";

        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            if (UType.MyCtoD(MyMain.Fld3) > 0)
            {
                SqlStr += " where  ";
                SqlStr += " a.officeid = " + MyMain.Fld1;
                SqlStr += " and a.ProjectId = " + MyMain.Fld2;

                SqlStr += " and a.jobno = " + MyMain.Fld3;
            }
            if (UType.MyCtoD(MyMain.Fld4) > 0)
            {
                SqlStr += " where  ";
                SqlStr += " a.officeid = " + MyMain.Fld1;
                SqlStr += " and a.ProjectId = " + MyMain.Fld2;

                SqlStr += " and a.docno = " + MyMain.Fld4;
                if (MyMain.Fld7.Length > 1)
                {
                    SqlStr += " and a.MBLNO =" + Sqote + MyMain.Fld7 + Sqote;
                }
                if (MyMain.Fld8.Length > 1)
                {
                    SqlStr += " and a.HBLNO =" + Sqote + MyMain.Fld8 + Sqote;
                }
            }
        }
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public DataSet GetJobnEquipmentInfo()
    {
        SqlStr = "select sum(a.netwt) as TotalNetWt , sum(a.grosswt) as TotalGrossWt,sum(a.tarewt) as TotalTareWt,wtunit, sum(a.package) as package,unit  from Equipment a  ";

        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " group by wtunit,unit ";

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetMaxJobInfo()
    {
        SqlStr = "SELECT max(a.JobNo) as MaxJobNo ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  JobInfo a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.OfficeId = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.jobyear = " + MyMain.Fld3;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetMaxJobInfoSNo()
    {
        SqlStr = "SELECT max(a.Sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  JobInfo a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.OfficeId = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            if (UType.MyCtoD(MyMain.Fld3) > 0)
            {
                SqlStr += " and a.JobNo = " + MyMain.Fld3;
            }
        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string InsertJobInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO JobInfo";
            SqlStr += "(";
            SqlStr += "OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",docno";
            SqlStr += ",jobno";
            SqlStr += ",natureid";
            SqlStr += ",jobkind";
            SqlStr += ",MBLNO";
            SqlStr += ",HBLNo";


            SqlStr += ",jobDate";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ",jobyear";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + Sqote + MyMain.Fld7 + Sqote;
            SqlStr += Comma + Sqote + MyMain.Fld8 + Sqote;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += Comma + DateTime.Now.ToString("yyyy");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }



    public string UpdateJobInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Jobinfo Set ";
            // SqlStr += " txtJob = " + MyMain.EmpId;
            SqlStr += "JobNo = " + Sqote + MyMain.Fld1 + Sqote;
            SqlStr += ",JobDate = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += ",NatureID = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ",IncoTerm = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += ",Type = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",SubType = " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ",CostCenter = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ",Console = " + UType.MyCtoDs(MyMain.Fld8);
            SqlStr += ",JobKind = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",Customer = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",Security = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",JobStatus = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ",ShipStatus = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",FreightType = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",Nomination = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",txtFile = " + Sqote + UType.Chk10(MyMain.Fld16) + Sqote;
            SqlStr += ",HBLNo = " + Sqote + UType.Chk10(MyMain.Fld17) + Sqote;
            SqlStr += ",HBLDate = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",MBLNo = " + Sqote + UType.Chk10(MyMain.Fld19) + Sqote;
            SqlStr += ",MBLDate = " + UType.MyCtoDs(MyMain.Fld20);

            SqlStr += ",ParentJobNo = " + Sqote + UType.Chk10(MyMain.Fld21) + Sqote;
            SqlStr += ",TotalContainers = " + UType.MyCtoDs(MyMain.Fld22);
            SqlStr += ",Client = " + Sqote + UType.Chk10(MyMain.Fld23) + Sqote;
            SqlStr += ",SalesRep = " + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ",Consignee = " + UType.MyCtoDs(MyMain.Fld25);
            SqlStr += ",Sline = " + UType.MyCtoDs(MyMain.Fld26);
            SqlStr += ",Shipper = " + UType.MyCtoDs(MyMain.Fld27);
            SqlStr += ",LocalVendor = " + UType.MyCtoDs(MyMain.Fld28);
            SqlStr += ",Commodity = " + UType.MyCtoDs(MyMain.Fld29);
            SqlStr += ",Overseas = " + UType.MyCtoDs(MyMain.Fld30);
            SqlStr += ",PortLoading = " + UType.MyCtoDs(MyMain.Fld31);
            SqlStr += ",Principal = " + UType.MyCtoDs(MyMain.Fld32);
            SqlStr += ",PortDischarge = " + UType.MyCtoDs(MyMain.Fld33);
            SqlStr += ",Vessel = " + Sqote + UType.Chk10(MyMain.Fld34) + Sqote;
            SqlStr += ",CustomClearance = " + UType.MyCtoDs(MyMain.Fld35);
            SqlStr += ",Transportation = " + UType.MyCtoDs(MyMain.Fld36);
            SqlStr += ",Voyage = " + Sqote + UType.Chk10(MyMain.Fld37) + Sqote;
            SqlStr += ",ED = " + Sqote + UType.Chk10(MyMain.Fld38) + Sqote;
            SqlStr += ",Coloader = " + UType.MyCtoDs(MyMain.Fld39);
            SqlStr += ",TracingNotes = " + Sqote + UType.Chk10(MyMain.Fld40) + Sqote;
            SqlStr += ",CutOffDate = " + UType.MyCtoDs(MyMain.Fld41);
            SqlStr += ",PlannedETD = " + UType.MyCtoDs(MyMain.Fld42);
            SqlStr += ",PlannedETA = " + UType.MyCtoDs(MyMain.Fld43);
            SqlStr += ",ActualETD = " + UType.MyCtoDs(MyMain.Fld44);
            SqlStr += ",ActualETA = " + UType.MyCtoDs(MyMain.Fld45);
            SqlStr += ",Weight = " + Sqote + UType.Chk10(MyMain.Fld46) + Sqote;
            SqlStr += ",Volume = " + Sqote + UType.Chk10(MyMain.Fld47) + Sqote;
            SqlStr += ",Container = " + Sqote + UType.Chk10(MyMain.Fld48) + Sqote;
            SqlStr += ",TEU = " + Sqote + UType.Chk10(MyMain.Fld49) + Sqote;
            SqlStr += ",PCS = " + Sqote + UType.Chk10(MyMain.Fld50) + Sqote;
            SqlStr += ",Quotation = " + Sqote + UType.Chk10(MyMain.Fld51) + Sqote;

            SqlStr += ",cbPartFCL = " + UType.MyCtoDs(MyMain.Fld52);
            SqlStr += ",cbMTYMove = " + UType.MyCtoDs(MyMain.Fld53);
            SqlStr += ",cbCustomClearance = " + UType.MyCtoDs(MyMain.Fld54);
            SqlStr += ",cbTransportation = " + UType.MyCtoDs(MyMain.Fld55);
            SqlStr += ",DGId = " + UType.MyCtoDs(MyMain.Fld56);
            SqlStr += ",DocNo = " + UType.MyCtoDs(MyMain.Fld58);
            SqlStr += ",FinalDestination = " + UType.MyCtoDs(MyMain.Fld59);
            SqlStr += ",UserId = " + UType.MyCtoDs(MyMain.Fld60);
            SqlStr += ",jobcy = " + UType.MyCtoDs(MyMain.Fld62);
            SqlStr += ",docyear = " + UType.MyCtoDs(MyMain.Fld63);
            //SqlStr += ",portofloadingcountry = " + UType.MyCtoDs(MyMain.Fld61);
            SqlStr += " Where jobNo = " + MyMain.Fld1;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    #endregion
    #region MyRegion Equipment
    public DataSet GetEquipment()
    {
        SqlStr = "SELECT a.*,'1' as Principalcode1 ,'1' as Curren1,'1' as TotalDeten,'1' as ContainerMove1,'0' as NetAmount,'0' as OtherCharges";
        SqlStr += " ,'0' as DetentionDays,'0' as DetentionPKR FROM "; // officechart oc   ";
        SqlStr += "  Equipment a  ";
        //SqlStr += "  left join manifest b on   ";
        //inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.jobno = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.sno = " + MyMain.Fld4;
        }
        SqlStr += " order by a.jobno,sno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetEquipmentWeb1()
    {
        SqlStr = "SELECT b.*,c.indexno, d.delivery";
        SqlStr += " FROM jobinfo a  ";
        SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";

        SqlStr += " left join BLDetail c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " inner join Routing d on a.officeid = d.officeid and a.ProjectId = d.ProjectId and a.jobno = d.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetEquipmentWeb()
    {
        SqlStr = "SELECT b.*,c.indexno, d.delivery";
        SqlStr += " FROM jobinfo a  ";
        SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";

        SqlStr += " left join BLDetail c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " left join Routing d on a.officeid = d.officeid and a.ProjectId = d.ProjectId and a.jobno = d.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.docno = " + MyMain.Fld3;

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetDetInvoiceWeb()
    {
        SqlStr = "SELECT c.* ";
        SqlStr += " FROM jobinfo a  ";
        //SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";

        //SqlStr += " left join BLDetail c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";

        SqlStr += " left join charges c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and c.netamount > 0 ";
        SqlStr += " and c.chargestype = 1 ";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetDetInvoiceWebPay()
    {
        SqlStr = "SELECT a.* ";
        // SqlStr += " FROM jobinfo a  ";
        //SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";

        //SqlStr += " left join BLDetail c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";

        SqlStr += " FROM charges a "; // on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and a.customer = " + MyMain.Fld4;
        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 2";
        SqlStr += " and a.vno > 0";
        SqlStr += " order by a.sno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetActTran()
    {
        SqlStr = "SELECT a.* ";
        //SqlStr += " FROM jobinfo a  ";
        //SqlStr += " left join acttran b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " from acttran a where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and a.actcode =" + MyMain.Fld4;
        //SqlStr += " and c.chargestype = 2";
        //SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetActTranPay()
    {
        SqlStr = "SELECT a.* ";
        //SqlStr += " FROM jobinfo a  ";
        //SqlStr += " left join acttran b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " from acttran a where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        // SqlStr += " and a.jobno = " + MyMain.Fld3;

        SqlStr += " and a.actcode =" + MyMain.Fld4;
        SqlStr += " and a.vno = " + MyMain.Fld5;
        //SqlStr += " and c.chargestype = 2";
        //SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetActTranPayInvoice()
    {
        SqlStr = "SELECT a.* ";
        //SqlStr += " FROM jobinfo a  ";
        //SqlStr += " left join acttran b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " from acttran a where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        //SqlStr += " and a.jobno = " + MyMain.Fld3;

        SqlStr += " and a.actcode =" + MyMain.Fld4;
        SqlStr += " and a.vno = " + MyMain.Fld5;
        //SqlStr += " and c.chargestype = 2";
        //SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetDetReportCh()
    {
        SqlStr = "SELECT c.* ";
        SqlStr += " FROM jobinfo a  ";

        SqlStr += " left join charges c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and c.netamount > 0 ";
        SqlStr += " and c.chargestype = 1";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetDetReportChCosting()
    {
        SqlStr = "SELECT c.* ";
        SqlStr += " FROM jobinfo a  ";

        SqlStr += " left join charges c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and c.netamount > 0 ";

        SqlStr += " order by a.jobno,chargestype ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetDetInvoiceWebPayNew()
    {
        SqlStr = "SELECT c.* ";
        SqlStr += " FROM jobinfo a  ";
        //SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";

        //SqlStr += " left join BLDetail c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";

        SqlStr += " left join charges c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and c.netamount > 0 ";
        SqlStr += " and c.chargestype = 2";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetExpDetInvoiceWebPayNew()
    {
        SqlStr = "SELECT c.* ";
        SqlStr += " FROM bookinfo a  ";
        //SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";

        //SqlStr += " left join BLDetail c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";

        SqlStr += " left join expcharges c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and c.netamount > 0 ";
        SqlStr += " and c.chargestype = 2";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetDetInvoiceWebPayNew1712()
    {
        SqlStr = "SELECT a.*,b.HBLNo,b.HBLDate,b.MBLNo,b.MBLNo ";
        SqlStr += " FROM charges a  ";
        SqlStr += " inner join jobinfo b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " where  ";
        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        // if (UType.MyCtoD(MyMain.Fld5) > 0)
        //  {
        //     SqlStr += " and a.Vno = " + MyMain.Fld5;
        //  }
        SqlStr += " and a.customer = " + MyMain.Fld4; ;
        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 2 ";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetExpDetInvoiceWebPayNew1712()
    {
        SqlStr = "SELECT a.*,b.HBLNo,b.HBLDate,b.MBLNo,b.MBLNo ";
        SqlStr += " FROM charges a  ";
        SqlStr += " inner join jobinfo b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " where  ";
        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        // if (UType.MyCtoD(MyMain.Fld5) > 0)
        //  {
        //     SqlStr += " and a.Vno = " + MyMain.Fld5;
        //  }
        SqlStr += " and a.customer = " + MyMain.Fld4; ;
        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 2 ";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetDetInvoiceWebRec()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM charges a  ";
        //SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";

        //SqlStr += " left join BLDetail c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";

        //SqlStr += " left join charges c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        //SqlStr += " and a.jobno = " + MyMain.Fld3;
        if (UType.MyCtoD(MyMain.Fld5) > 0)
        {
            SqlStr += " and a.Vno = " + MyMain.Fld5;
        }

        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 1";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetDetInvoiceWebRecRemarks()
    {
        SqlStr = "select sum(a.netamount) as TotalNetAmount,b.hblno,b.hbldate,MBLNo,MBLDate,c.Vessel,b.Consignee,d.ActDesc as ConsigneeName,e.ChqNo,b.jobno from charges a";
        SqlStr += " inner join jobinfo b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " inner join Manifest c on b.officeid = c.officeid and b.ProjectId = c.ProjectId and b.DocNo = c.DocNo";
        SqlStr += " inner join ActChart d on b.officeid = c.officeid and b.ProjectId = c.ProjectId and b.Consignee= d.actcode";
        SqlStr += " left join ActTran e on a.officeid = e.officeid and a.ProjectId = e.ProjectId and a.vno= e.VNo and a.SNo=1";
        SqlStr += " where  ";
        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and a.chargestype = " + MyMain.Fld4;
        SqlStr += " group by b.hblno,b.hbldate,MBLNo,MBLDate,c.Vessel,b.Consignee,d.ActDesc,e.ChqNo,b.jobno  ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetDetInvoiceWebRecRemarks1()
    {
        SqlStr = "select distinct e.ChqNo,MBLNo,b.hblno,c.Vessel,b.jobno from charges a";
        //SqlStr = "select sum(a.netamount) as TotalNetAmount,b.hblno,b.hbldate,MBLNo,MBLDate,c.Vessel,b.Consignee,d.ActDesc as ConsigneeName,e.ChqNo,b.jobno from charges a";
        SqlStr += " inner join jobinfo b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " inner join Manifest c on b.officeid = c.officeid and b.ProjectId = c.ProjectId and b.DocNo = c.DocNo";
        SqlStr += " inner join ActChart d on b.officeid = c.officeid and b.ProjectId = c.ProjectId and b.Consignee= d.actcode";
        SqlStr += " left join ActTran e on a.officeid = e.officeid and a.ProjectId = e.ProjectId and a.vno= e.VNo and a.SNo=1";
        SqlStr += " where  ";
        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and a.chargestype = " + MyMain.Fld4;
        SqlStr += " group by e.ChqNo,MBLNo,b.hblno,c.Vessel,b.jobno  ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetDetInvoiceWebRecReport()
    {
        SqlStr = "SELECT a.*,b.HBLNo,b.HBLDate,b.MBLNo,b.MBLNo ,c.ActCode,d.actdesc,c.ChqNo,c.ChqDate,c.Narration,c.vtypeid,c.trandate";
        SqlStr += " ,e.actdesc as ConsigneeName,f.actdesc as ParticularName  ";
        SqlStr += " FROM charges a  ";
        SqlStr += " inner join jobinfo b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " inner join acttran c on a.officeid = c.officeid and a.projectid = c.projectid and a.vno = c.vno ";
        SqlStr += " left join ActChart d on c.officeid = d.officeid and c.projectid=  d.projectid and c.actcode= d.actcode";
        SqlStr += " left join ActChart e on a.officeid = e.officeid and a.projectid=  e.projectid and a.Customer= e.actcode";
        SqlStr += " left join ActChart f on a.officeid = f.officeid and a.projectid=  f.projectid and CONVERT(DECIMAL(10, 0), ltrim(a.particular))= f.actcode ";
        SqlStr += "";
        SqlStr += " where  ";
        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld6) > 0)
        {
            SqlStr += " and a.particular = " + MyMain.Fld6;
        }
        //SqlStr += " and a.customer = " + MyMain.Fld4; ;
        SqlStr += " and a.vno = " + MyMain.Fld5; ;
        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 1";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetDetInvoiceWebRecReportbk2810()
    {
        SqlStr = "SELECT a.*,b.HBLNo,b.HBLDate,b.MBLNo,b.MBLNo ";
        SqlStr += " FROM charges a  ";
        SqlStr += " inner join jobinfo b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " where  ";
        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld6) > 0)
        {
            SqlStr += " and a.particular = " + MyMain.Fld6;
        }
        SqlStr += " and a.customer = " + MyMain.Fld4; ;
        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 1";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetExpDetInvoiceWebRecReport()
    {
        SqlStr = "SELECT a.*,b.HBLNo,b.HBLDate,b.MBLNo,b.MBLNo ";
        SqlStr += " FROM Expcharges a  ";
        SqlStr += " inner join jobinfo b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " where  ";
        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld6) > 0)
        {
            SqlStr += " and a.particular = " + MyMain.Fld6;
        }
        SqlStr += " and a.customer = " + MyMain.Fld4; ;
        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 1";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetDetRemarksPay()
    {
        SqlStr = "SELECT a.*,b.HBLNo,b.HBLDate,b.MBLNo,b.MBLNo ";
        SqlStr += " FROM charges a  ";
        SqlStr += " inner join jobinfo b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " where  ";
        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld6) > 0)
        {
            SqlStr += " and a.particular = " + MyMain.Fld6;
        }
        SqlStr += " and a.customer = " + MyMain.Fld4; ;
        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 2";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetDetInvoiceWebPayReport()
    {
        SqlStr = "SELECT a.*,b.HBLNo,b.HBLDate,b.MBLNo,b.MBLNo ";
        SqlStr += " FROM charges a  ";
        SqlStr += " inner join jobinfo b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " where  ";
        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld5) > 0)
        {
             SqlStr += " and a.Vno = " + MyMain.Fld5;
        }
        SqlStr += " and a.customer = " + MyMain.Fld4; ;
        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 2";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetDetInvoiceWebRecPrint()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM charges a  ";
        //SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";

        //SqlStr += " left join BLDetail c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";

        //SqlStr += " left join charges c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        //if (UType.MyCtoD(MyMain.Fld5) > 0)
        //{
        //   SqlStr += " and a.Vno = " + MyMain.Fld5;
        //}

        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 1";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetExpDetInvoiceWebRecPrint()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM Expcharges a  ";
        //SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";

        //SqlStr += " left join BLDetail c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";

        //SqlStr += " left join charges c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        //if (UType.MyCtoD(MyMain.Fld5) > 0)
        //{
        //   SqlStr += " and a.Vno = " + MyMain.Fld5;
        //}

        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 1";
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetDetInvoiceWebPayMBL()
    {
        SqlStr = "SELECT  d.vessel,d.voyage, c.hblno,c.hbldate,b.mblno,b.mbldate ";
        SqlStr += " FROM jobinfo a  ";
        SqlStr += " inner join manifest d on a.officeid = d.officeid and a.ProjectId = d.ProjectId and a.docno = d.docno";

        SqlStr += "  inner join mblinfo b on b.officeid = a.officeid and b.ProjectId = a.ProjectId and a.docno = b.DocNo";
        SqlStr += "  inner join hblinfo c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.DocNo = c.docNo ";

        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetExpDetInvoiceWebPayMBL()
    {
        SqlStr = "SELECT  d.vessel,d.voyage, c.hbl,c.hbldate,a.mblno,a.mbldate  ";
        SqlStr += " FROM bookinfo a  ";
        SqlStr += " inner join Expmanifest d on a.officeid = d.officeid and a.ProjectId = d.ProjectId and a.docno = d.docno";

        //SqlStr += "  inner join mblinfo b on b.officeid = a.officeid and b.ProjectId = a.ProjectId and a.docno = b.DocNo";
        SqlStr += "  inner join ExpBLDetail c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.DocNo = c.docNo ";

        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }


    public DataSet GetDetInvoiceReport()
    {
        SqlStr = "select a.*,b.*,e.chequeno,e.chequedate,invoice,e.RemarksPay,e.RemarksRec";
        // SqlStr += " a.Consignee,a.Shipper,a.PortLoading,a.PortDischarge,a.FinalDestination,a.mblno,a.hblno,d.IndexNo from JobInfo a   ";
        SqlStr += " ,d.IndexNo from JobInfo a   ";
        SqlStr += " inner join manifest b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.docno = b.docno";
        //SqlStr += " inner join mblinfo c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.docno = c.DocNo";
        SqlStr += " inner join hblinfo d on a.officeid = d.officeid and a.ProjectId = d.ProjectId and a.DocNo = d.docNo ";
        SqlStr += " inner join chargeshdr e on a.officeid = e.officeid and a.ProjectId = e.ProjectId and a.jobNo = e.jobNo ";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " order by e.chequeno desc";

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetExpDetInvoiceReport()
    {
        SqlStr = "select a.*,b.*,invoice,e.RemarksPay,e.RemarksRec";
        // SqlStr += " a.Consignee,a.Shipper,a.PortLoading,a.PortDischarge,a.FinalDestination,a.mblno,a.hblno,d.IndexNo from JobInfo a   ";
        SqlStr += " ,d.IndexNo from bookInfo a   ";
        SqlStr += " inner join expmanifest b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.docno = b.docno";
        //SqlStr += " inner join mblinfo c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.docno = c.DocNo";
        SqlStr += " inner join Expbldetail d on a.officeid = d.officeid and a.ProjectId = d.ProjectId and a.DocNo = d.docNo ";
        SqlStr += " inner join Expchargeshdr e on a.officeid = e.officeid and a.ProjectId = e.ProjectId and a.jobNo = e.jobNo ";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetDetInvoiceWebSmry()
    {
        SqlStr = "select distinct Customer ";
        SqlStr += " FROM charges a   ";

        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 1 ";
        //SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetExpDetInvoiceWebSmry()
    {
        SqlStr = "select distinct Customer ";
        SqlStr += " FROM Expcharges a   ";

        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 1 ";
        //SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetDetInvoiceWebSmryPay()
    {
        SqlStr = "select distinct Customer ";
        SqlStr += " FROM charges a   ";

        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        SqlStr += " and a.netamount > 0 ";
        SqlStr += " and a.chargestype = 2";
        //SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetDetInvoiceWeb1()
    {
        SqlStr = "SELECT b.* ";
        SqlStr += " FROM jobinfo a  ";
        SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";

        //SqlStr += " left join BLDetail c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";

        // SqlStr += " left join charges c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        // SqlStr += " and c.amount > 0 ";

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetExpDetInvoiceWeb1()
    {
        SqlStr = "SELECT b.* ";
        SqlStr += " FROM bookinfo a  ";
        SqlStr += " inner join ExpEquipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";

        //SqlStr += " left join BLDetail c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";

        // SqlStr += " left join charges c on a.officeid = c.officeid and a.ProjectId = c.ProjectId and a.jobno = c.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        // SqlStr += " and c.amount > 0 ";

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetDetReportEqDetention()
    {
        SqlStr = "SELECT distinct c.* ";
        SqlStr += " FROM jobinfo a  ";
        SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " inner join rfdetention c on b.officeid = c.officeid and b.ProjectId = c.ProjectId and b.PrincipalCode =c.PrincipalID";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        // SqlStr += " and c.amount > 0 ";

        //SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetDetReportEq()
    {
        SqlStr = "SELECT b.* ";
        SqlStr += " FROM jobinfo a  ";
        SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        // SqlStr += " and c.amount > 0 ";

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetCostingAccount()
    {
        SqlStr = "SELECT a.*,b.actdesc   FROM acttran a  ";
        SqlStr += " inner join actchart b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.actcode = b.actcode   ";
       
        SqlStr += " where  ";
         
        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        // SqlStr += " and c.amount > 0 ";

        SqlStr += " order by a.VNo,a.SNo  ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetExpDetReportEq()
    {
        SqlStr = "SELECT b.* ";
        SqlStr += " FROM bookinfo a  ";
        SqlStr += " inner join ExpEquipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;
        // SqlStr += " and c.amount > 0 ";

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetRecReportAct()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM acttran a  ";

        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.vno = " + MyMain.Fld5;
        SqlStr += " and a.amount > 0 ";

        //SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }


    public DataSet GetRecReportActRemarks()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM acttran a  ";

        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.vno = " + MyMain.Fld5;
        // SqlStr += " and c.amount > 0 ";

        //SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetEquipmentGatePass()
    {
        SqlStr = "SELECT b.* ";
        SqlStr += " FROM jobinfo a  ";
        SqlStr += " inner join Equipment b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        //    SqlStr += " left join blinfo b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";

        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobno = " + MyMain.Fld3;

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetRoutingWeb()
    {
        SqlStr = "SELECT b.* ";
        SqlStr += " FROM jobinfo a  ";
        SqlStr += " inner join Routing b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.jobno = b.jobno";
        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.docno = " + MyMain.Fld3;

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }


    public DataSet GetMaxEquipment()
    {
        SqlStr = "SELECT max(a.sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Equipment a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.jobno = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.projectid = " + MyMain.Fld3;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertEquipment()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO Equipment";
            SqlStr += "(";
            SqlStr += "[OfficeId]";
            SqlStr += ",[ProjectID]";
            SqlStr += ",jobno";
            SqlStr += ",sno";
            SqlStr += ",SizenType1";
            SqlStr += ",WTUnit1";
            SqlStr += ",Unit1";
            SqlStr += ",LoadType1";
            SqlStr += ",CargoType1";
            SqlStr += ",Jobcy";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";

            SqlStr += ")";
            SqlStr += "VALUES( ";

            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + MyMain.Fld7;
            SqlStr += Comma + MyMain.Fld8;
            SqlStr += Comma + MyMain.Fld9;
            SqlStr += Comma + MyMain.Fld10;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");

            SqlStr += ")";

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateEquipment()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Equipment Set ";

            SqlStr += "ContainerNo = " + Sqote + UType.Chk10(MyMain.Fld3) + Sqote;
            SqlStr += ",Seal = " + Sqote + UType.Chk10(MyMain.Fld4) + Sqote;
            SqlStr += ",[SizenType] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[RateGroup] = " + UType.MyCtoDs(MyMain.Fld6);

            SqlStr += ", [NetWt] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ", [GrossWt] = " + UType.MyCtoDs(MyMain.Fld8);

            SqlStr += ",[TareWt] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",[WTUnit] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[CBM] = " + UType.MyCtoDs(MyMain.Fld12);
            //SqlStr += ",[Package] = " + Sqote + UType.Chk10(MyMain.Fld13) + Sqote;
            SqlStr += ",[Package] = " + UType.MyCtoDs(MyMain.Fld13);

            SqlStr += ",[Unit] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[Temp] = " + Sqote + UType.Chk10(MyMain.Fld15) + Sqote;
            SqlStr += ",[LoadType] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[Remarks] = " + Sqote + UType.Chk10(MyMain.Fld17) + Sqote;

            SqlStr += ",[PrincipalName] = " + Sqote + UType.Chk10(MyMain.Fld19) + Sqote;
            SqlStr += ",[PrincipalEquipInv] = " + Sqote + UType.Chk10(MyMain.Fld20) + Sqote;
            SqlStr += ",[IsManual] = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ",[Detention] = " + UType.MyCtoDs(MyMain.Fld22);
            //SqlStr += ",[PrincipalCode] = " + UType.MyCtoDs(MyMain.Fld23);
            SqlStr += ",[Demurrage] = " + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ",[Plugin] = " + Sqote + UType.Chk10(MyMain.Fld25) + Sqote;
            SqlStr += ",[CargoType] = " + UType.MyCtoDs(MyMain.Fld24);  //+ Sqote + UType.Chk10(MyMain.Fld24) + Sqote;
            SqlStr += ",[PartFCL] = " + UType.MyCtoDs(MyMain.Fld27);
            SqlStr += ",[PartFCL1] = " + Sqote + Sqote;
            SqlStr += ",[SOC] = " + UType.MyCtoDs(MyMain.Fld28);
            SqlStr += ",[SOC1] = " + Sqote + Sqote;
            SqlStr += ",[OOG] = " + UType.MyCtoDs(MyMain.Fld29);
            SqlStr += ",[OOG1] = " + Sqote + Sqote;
            SqlStr += ",[Top] = " + Sqote + UType.Chk10(MyMain.Fld30) + Sqote;
            SqlStr += ",[Left] = " + Sqote + UType.Chk10(MyMain.Fld31) + Sqote;
            SqlStr += ",[Right] = " + Sqote + UType.Chk10(MyMain.Fld32) + Sqote;
            SqlStr += ",[Front] = " + Sqote + UType.Chk10(MyMain.Fld33) + Sqote;

            SqlStr += ",[ContainerEntryDate] = " + UType.MyCtoDs(MyMain.Fld34);
            SqlStr += ",[SecurityAmountReceive] = " + UType.MyCtoDs(MyMain.Fld35);
            SqlStr += ",[ContainerReturnDate] = " + UType.MyCtoDs(MyMain.Fld36);
            SqlStr += ",[SecurityAmountPaid] = " + UType.MyCtoDs(MyMain.Fld37);
            SqlStr += ",[WashingCharges] = " + UType.MyCtoDs(MyMain.Fld38);
            SqlStr += ",[Settle] = " + UType.MyCtoDs(MyMain.Fld39);


            SqlStr += ",[Curren] = " + UType.MyCtoDs(MyMain.Fld41);
            SqlStr += ",[Discount] = " + UType.MyCtoDs(MyMain.Fld42);
            SqlStr += ",[Documentcharges] = " + UType.MyCtoDs(MyMain.Fld43);
            SqlStr += ",[exrate] = " + UType.MyCtoDs(MyMain.Fld40);
            SqlStr += ",[ContainerMove] = " + UType.MyCtoDs(MyMain.Fld45);
            SqlStr += " Where JobNo = " + MyMain.Fld1;
            SqlStr += " and  Jobcy = " + MyMain.Fld46;
            if (UType.MyCtoD(MyMain.Fld2) > 0)
            {
                SqlStr += " and sno = " + MyMain.Fld2;
            }
            SqlStr += " and  Officeid = " + MyMain.Fld47;
            SqlStr += " and  ProjectID = " + MyMain.Fld48;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateEquipmentNew()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Equipment Set ";

            SqlStr += "[ContainerArrvalDate] = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ",[ContainerEntryDate] = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += ",[ContainerReturnDate] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[SecurityAmountReceive] = " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ",[SecurityAmountPaid] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ",[LogDays] = " + UType.MyCtoDs(MyMain.Fld8); ;
            SqlStr += ",[Detention] = " + UType.MyCtoDs(MyMain.Fld9);
            // SqlStr += ",[Detention] = " + UType.MyCtoDs(MyMain.Fld9);
            // SqlStr += ",[DetentionRate] = " + UType.MyCtoDs(MyMain.Fld10);
            // SqlStr += ",[Demurrage] = " + UType.MyCtoDs(MyMain.Fld11);
            // SqlStr += ",[Plugin] = " + UType.MyCtoDs(MyMain.Fld12);
            //  SqlStr += ",[Damagecharges] = " + UType.MyCtoDs(MyMain.Fld13);
            //  SqlStr += ",[WashingCharges] = " + UType.MyCtoDs(MyMain.Fld14);

            //   SqlStr += ",[Discount] = " + UType.MyCtoDs(MyMain.Fld15);
            //  SqlStr += ",[LateEIRCharges] = " + UType.MyCtoDs(MyMain.Fld16);
            //   SqlStr += ",[DocumentCharges] = " + UType.MyCtoDs(MyMain.Fld17);

            //  SqlStr += ",[AdvanceDetanin] = " + UType.MyCtoDs(MyMain.Fld18);
            //   SqlStr += ",[ExRate] = " + UType.MyCtoDs(MyMain.Fld19);
            //   SqlStr += ",[Curren] = " + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",[PrincipalCode] = " + UType.MyCtoDs(MyMain.Fld21);
            // SqlStr += ",[TotalDetention] = " + UType.MyCtoDs(MyMain.Fld22);

            SqlStr += ",[DetHdrFreeDays] = " + UType.MyCtoDs(MyMain.Fld51);
            SqlStr += ",[DemHdrFreeDays] = " + UType.MyCtoDs(MyMain.Fld52);
            SqlStr += ",[PluginHdrFreeDays] = " + UType.MyCtoDs(MyMain.Fld53);
            SqlStr += ",[HdrManual] = " + UType.MyCtoDs(MyMain.Fld54);
            SqlStr += ",[Calculateitem] = " + UType.MyCtoDs(MyMain.Fld55);



            SqlStr += " Where JobNo = " + MyMain.Fld1;
            if (UType.MyCtoD(MyMain.Fld2) > 0)
            {
                SqlStr += " and sno = " + MyMain.Fld2;
            }
            SqlStr += " and  Officeid = " + MyMain.Fld47;
            SqlStr += " and  ProjectID = " + MyMain.Fld48;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateEquipmentNew2()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Equipment Set ";

            // SqlStr += "[ContainerArrvalDate] = " + UType.MyCtoDs(MyMain.Fld3);
            // SqlStr += ",[ContainerEntryDate] = " + UType.MyCtoDs(MyMain.Fld4);
            // SqlStr += ",[ContainerReturnDate] = " + UType.MyCtoDs(MyMain.Fld5);
            // SqlStr += ",[SecurityAmountReceive] = " + UType.MyCtoDs(MyMain.Fld6);
            //  SqlStr += ",[SecurityAmountPaid] = " + UType.MyCtoDs(MyMain.Fld7);
            //  SqlStr += ",[LogDays] = " + UType.MyCtoDs(MyMain.Fld8); ;// + Sqote + UType.Chk10(MyMain.Fld19) + Sqote;
            //SqlStr += " [Detention] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += " [DetentionRate] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[Demurrage] = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",[Plugin] = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ",[Damagecharges] = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",[WashingCharges] = " + UType.MyCtoDs(MyMain.Fld14);

            SqlStr += ",[Discount] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[LateEIRCharges] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[DocumentCharges] = " + UType.MyCtoDs(MyMain.Fld17);

            SqlStr += ",[AdvanceDetanin] = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",[ExRate] = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",[Curren] = " + UType.MyCtoDs(MyMain.Fld20);
            // SqlStr += ",[PrincipalCode] = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ",[TotalDetention] = " + UType.MyCtoDs(MyMain.Fld22);

            //  SqlStr += ",[DetHdrFreeDays] = " + UType.MyCtoDs(MyMain.Fld51);
            //  SqlStr += ",[DemHdrFreeDays] = " + UType.MyCtoDs(MyMain.Fld52);
            // SqlStr += ",[PluginHdrFreeDays] = " + UType.MyCtoDs(MyMain.Fld53);
            // SqlStr += ",[HdrManual] = " + UType.MyCtoDs(MyMain.Fld54);



            SqlStr += " Where JobNo = " + MyMain.Fld1;
            if (UType.MyCtoD(MyMain.Fld2) > 0)
            {
                SqlStr += " and sno = " + MyMain.Fld2;
            }
            SqlStr += " and  Officeid = " + MyMain.Fld47;
            SqlStr += " and  ProjectID = " + MyMain.Fld48;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateEquipment1()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Equipment Set ";


            SqlStr += " [IsManual] = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ",[Detention] = " + UType.MyCtoDs(MyMain.Fld22);
            //SqlStr += ",[PrincipalCode] = " + UType.MyCtoDs(MyMain.Fld23);
            SqlStr += ",[Demurrage] = " + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ",[Plugin] = " + Sqote + UType.Chk10(MyMain.Fld25) + Sqote;


            SqlStr += " Where JobNo = " + MyMain.Fld1;


            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string DeleteEquipment()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Delete from Equipment ";
            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            SqlStr += " and JobNo = " + MyMain.Fld3;
            SqlStr += " and sno = " + MyMain.Fld4;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region MyRegion EquipmentSummary
    public DataSet GetEquipmentSummary()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  EquipmentSummary a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.jobno = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;


        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.sno = " + MyMain.Fld4;
        }
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxEquipmentSummary()
    {
        SqlStr = "SELECT max(a.sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  EquipmentSummary a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.jobno = " + MyMain.Fld1;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertEquipmentSummary()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO EquipmentSummary";
            SqlStr += "(";
            SqlStr += "[OfficeId]";
            SqlStr += ",[ProjectID]";
            SqlStr += ",jobno";
            SqlStr += ",sno";
            SqlStr += ",SizenType1";

            SqlStr += ",CargoType1";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateEquipmentSummary()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update EquipmentSummary Set ";


            SqlStr += "[SizenType] = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ",[SizenType1] = 1";
            SqlStr += ",[RateGroup] = " + UType.MyCtoDs(MyMain.Fld4);

            SqlStr += ", [Qty] = " + UType.MyCtoDs(MyMain.Fld5);

            SqlStr += ",[PrincipalCode] = " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ",[PrincipalName] = " + Sqote + UType.Chk10(MyMain.Fld7) + Sqote;

            SqlStr += ",[CargoType] = " + UType.MyCtoDs(MyMain.Fld8);
            SqlStr += ",[CargoType1] =1 ";

            SqlStr += " Where JobNo = " + MyMain.Fld1;
            SqlStr += " and sno = " + MyMain.Fld2;
            SqlStr += " and  Officeid = " + MyMain.Fld47;
            SqlStr += " and  ProjectID = " + MyMain.Fld48;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region MyRegion Product
    public DataSet GetProduct()
    {
        SqlStr = "SELECT a.*, '1' as CountryCode1, '1' as SerialCategory1, '1' as SerialType1, '1' ProductUnit1 ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Product a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            //SqlStr += " a.officeid = " + MyMain.Fld1;
            // SqlStr += " and ac.ProjectId = " + MyMain.Fld2;

            SqlStr += " a.docno = " + MyMain.Fld1;
        }
        if (UType.MyCtoD(MyMain.Fld2) > 0)
        {
            SqlStr += " and a.Productid = " + MyMain.Fld2;
        }
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxProduct()
    {
        SqlStr = "SELECT max(a.sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Product a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.jobno = " + MyMain.Fld1;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertProduct()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO Product";
            SqlStr += "(";
            SqlStr += "jobno";
            SqlStr += ",sno";
            SqlStr += ",ProductUnit";
            SqlStr += ",SerialType";
            SqlStr += ",SerialCategory";
            SqlStr += ",CountryCode";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateProduct()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Product Set ";

            SqlStr += "officeID = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ",projectid = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += ",[ProductQty] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[ContainerNo] = " + Sqote + MyMain.Fld6 + Sqote;

            SqlStr += ", [ProductUnit] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ", [UnitPerValue] = " + UType.MyCtoDs(MyMain.Fld8);


            SqlStr += ",[SerialType] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",[SerialCategory] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[SerialException] = " + Sqote + MyMain.Fld11 + Sqote;
            SqlStr += ",[CountryCode] = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ",[RateGroup] = " + Sqote + UType.Chk10(MyMain.Fld13) + Sqote;
            SqlStr += ",[Make] = " + Sqote + UType.Chk10(MyMain.Fld14) + Sqote;
            SqlStr += ",[Model] = " + Sqote + UType.Chk10(MyMain.Fld15) + Sqote;
            SqlStr += ",[ChasisNo] = " + Sqote + UType.Chk10(MyMain.Fld16) + Sqote;
            SqlStr += ",[Engine] = " + Sqote + UType.Chk10(MyMain.Fld17) + Sqote;
            SqlStr += ",[DENumber] = " + Sqote + UType.Chk10(MyMain.Fld18) + Sqote;



            SqlStr += " Where JobNo = " + MyMain.Fld1;
            SqlStr += " and sno = " + MyMain.Fld2;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region MyRegion ChargesHdr
    public DataSet GetChargesHdr()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += "  ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Chargeshdr a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.JobNo = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
        }

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetChargesHdrReport()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += "  ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Chargeshdr a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.JobNo = " + MyMain.Fld3;
        }

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }


    public string InsertChargesHdr()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO ChargesHdr";
            SqlStr += "(";
            SqlStr += "officeid";
            SqlStr += ",ProjectId";
            SqlStr += ",jobno";


            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertExpChargesHdr()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO ExpChargesHdr";
            SqlStr += "(";
            SqlStr += "officeid";
            SqlStr += ",ProjectId";
            SqlStr += ",jobno";


            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateChargesHdr()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update ChargesHdr Set ";


            SqlStr += "[Invoice] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[Approved] = " + UType.MyCtoDs(MyMain.Fld6);

            SqlStr += ", [ExRateBuying] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ", [ExRateSelling] = " + UType.MyCtoDs(MyMain.Fld8);


            SqlStr += ",[Quotation] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",[Security] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[ReceivablePP] = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",[ReceivableCC] = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ",[PayableCC] = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",[PayablePP] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[ReceivableTotal] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[ReceivableTaxTotal] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[PayableTotal] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[PayableTaxTotal]  = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",[SD]  = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",[Net]  = " + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",[LSD]  = " + UType.MyCtoDs(MyMain.Fld21);

            SqlStr += ",[ManifestRemarks]  = " + Sqote + MyMain.Fld22 + Sqote;
            SqlStr += ",[PerShipment]  = " + Sqote + MyMain.Fld23 + Sqote;
            SqlStr += ",[EquipInvoice]  = " + Sqote + MyMain.Fld24 + Sqote;

            SqlStr += ",[ChequeNo]  = " + Sqote + MyMain.Fld29 + Sqote;
            SqlStr += ",[ChequeDate]  = " + MyMain.Fld30;

            SqlStr += ",[RemarksRec]  = " + Sqote + MyMain.Fld31 + Sqote;
            SqlStr += ",[RemarksPay]  = " + Sqote + MyMain.Fld32 + Sqote;

            SqlStr += ",[UserId]  = " + MyMain.Fld33;

            SqlStr += " Where JobNo = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and officeID = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += " and projectid = " + UType.MyCtoDs(MyMain.Fld3);
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region MyRegion Charges
    public DataSet GetCharges()
    {
        SqlStr = "SELECT a.*,1 as particular1 ";
        SqlStr += "  ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Charges a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.JobNo = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
            if (UType.MyCtoD(MyMain.Fld5) > 0)
            {
                SqlStr += " and a.ChargesType = " + MyMain.Fld5;
            }
        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.Chargesid = " + MyMain.Fld4;
        }
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetChargesEq()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Charges a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.JobNo = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
            SqlStr += " and a.ChargesType = " + MyMain.Fld4;
            SqlStr += " and a.Principalcode = " + MyMain.Fld5;
            SqlStr += " and a.Particular = " + MyMain.Fld6;



        }

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxCharges()
    {
        SqlStr = "SELECT max(a.sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Charges a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.JobNo = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxExpCharges()
    {
        SqlStr = "SELECT max(a.sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpCharges a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.JobNo = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxChargesPaymentID()
    {
        DataSet ds = null;
        try
        {
            SqlStr = "SELECT max(a.paymentid) as MaxPaymentID ";
            SqlStr += " FROM "; // officechart oc   ";
            SqlStr += "  Charges a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
            if (UType.MyCtoD(MyMain.Fld1) > 0)
            {
                SqlStr += " where  ";
                SqlStr += " a.paymentidyear = " + MyMain.Fld1;
                SqlStr += " and a.officeid = " + MyMain.Fld2;
                SqlStr += " and a.ProjectId = " + MyMain.Fld3;
            }


            ds = GetDatasetSpNew(SqlStr);
        }
        catch (Exception ex)
        {

            string chk1 = ex.ToString();
        }
        return ds;
    }

    public DataSet GetMaxExpChargesPaymentID()
    {
        DataSet ds = null;
        try
        {
            SqlStr = "SELECT max(a.paymentid) as MaxPaymentID ";
            SqlStr += " FROM "; // officechart oc   ";
            SqlStr += "  Charges a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
            if (UType.MyCtoD(MyMain.Fld1) > 0)
            {
                SqlStr += " where  ";
                SqlStr += " a.paymentidyear = " + MyMain.Fld1;
                SqlStr += " and a.officeid = " + MyMain.Fld2;
                SqlStr += " and a.ProjectId = " + MyMain.Fld3;
            }


            ds = GetDatasetSpNew(SqlStr);
        }
        catch (Exception ex)
        {

            string chk1 = ex.ToString();
        }
        return ds;
    }
    public string InsertCharges()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO Charges";
            SqlStr += "(";
            SqlStr += "jobno";
            SqlStr += ",officeID";
            SqlStr += ",ProjectID";
            SqlStr += ",sno";
            SqlStr += ",Type1";
            SqlStr += ",Basis1";
            SqlStr += ",PPCC1";
            SqlStr += ",CollectBy1";
            SqlStr += ",SizeType1";
            SqlStr += ",DGNonDG1";
            SqlStr += ",PrincipalCode1";
            SqlStr += ",Currency1";
            SqlStr += ",Customer1";
            SqlStr += ",Tariff1";
            SqlStr += ",ChargesType";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ",jobcy";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + MyMain.Fld7;
            SqlStr += Comma + MyMain.Fld8;
            SqlStr += Comma + MyMain.Fld9;
            SqlStr += Comma + MyMain.Fld10;
            SqlStr += Comma + MyMain.Fld11;
            SqlStr += Comma + MyMain.Fld12;
            SqlStr += Comma + MyMain.Fld13;
            SqlStr += Comma + MyMain.Fld14;
            SqlStr += Comma + MyMain.Fld15;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += Comma + DateTime.Now.ToString("yyyy");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateCharges()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Charges Set ";


            SqlStr += "[BillInvoice] = " + Sqote + UType.Chk10(MyMain.Fld85) + Sqote;
            SqlStr += ",[Charges] = " + UType.MyCtoDs(MyMain.Fld6);

            SqlStr += ", [Particular] = " + Sqote + UType.Chk10(MyMain.Fld7) + Sqote;
            SqlStr += ", [Description] = " + Sqote + UType.Chk10(MyMain.Fld8) + Sqote;


            SqlStr += ",[Type] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",[Type1] = 1";
            SqlStr += ",[Basis] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[Basis1] = 1";
            SqlStr += ",[PPCC] = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",[PPCC1] = 1";
            SqlStr += ",[CollectBy] = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ",[CollectBy1] =1 ";
            SqlStr += ",[SizeType] = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",[SizeType1] = 1";
            SqlStr += ",[RateGroup] = " + Sqote + UType.Chk10(MyMain.Fld14) + Sqote;
            SqlStr += ",[DGNonDG] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[DGNonDG1] = 1";
            SqlStr += ",[Shared] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[Shared1] = " + Sqote + Sqote;
            SqlStr += ",[PrincipalCode] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[PrincipalCode1] = 1";
            SqlStr += ",[Manual] = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",[Manual1]  = " + Sqote + Sqote;
            SqlStr += ",[Currency] = " + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",[Currency1] = 1";
            SqlStr += ",[Quantity] = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ",[Rate] = " + UType.MyCtoDs(MyMain.Fld22);
            SqlStr += ",[Amount] = " + UType.MyCtoDs(MyMain.Fld23);
            SqlStr += ",[Discount] = " + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ",[TaxApply] = " + UType.MyCtoDs(MyMain.Fld25);
            SqlStr += ",[TaxApply1] = " + Sqote + Sqote;

            SqlStr += ",[TaxAmount] = " + UType.MyCtoDs(MyMain.Fld26);
            SqlStr += ",[NetAmount] = " + UType.MyCtoDs(MyMain.Fld27);
            SqlStr += ",[ExRate] = " + UType.MyCtoDs(MyMain.Fld28);
            SqlStr += ",[LocalAmount] = " + UType.MyCtoDs(MyMain.Fld29);

            SqlStr += ",[Customer] = " + UType.MyCtoDs(MyMain.Fld30);
            SqlStr += ",[Customer1] = 1";
            SqlStr += ",[ManifestRemarks] = " + Sqote + UType.Chk10(MyMain.Fld31) + Sqote;
            SqlStr += ",[Tariff] = " + UType.MyCtoDs(MyMain.Fld32);
            SqlStr += ",[Tariff1] = 1";
            SqlStr += ",[Approved] =  " + Sqote + UType.Chk10(MyMain.Fld33) + Sqote;
            SqlStr += ",[ApprovedBy] =  " + Sqote + UType.Chk10(MyMain.Fld34) + Sqote;
            SqlStr += ",[ApprovedDate] =  " + Sqote + UType.Chk10(MyMain.Fld35) + Sqote;
            SqlStr += ",[ApprovedLog] =  " + Sqote + UType.Chk10(MyMain.Fld36) + Sqote;
            SqlStr += ",[PaymentID] = " + UType.MyCtoDs(MyMain.Fld40);
            SqlStr += ",[PaymentIDyear] = " + UType.MyCtoDs(MyMain.Fld41);
            SqlStr += ",[ExpenseDisplay] = " + UType.MyCtoDs(MyMain.Fld42);
            SqlStr += ",[ExpenseReceive] = " + UType.MyCtoDs(MyMain.Fld43);
            SqlStr += ",[Vno] = " + UType.MyCtoDs(MyMain.Fld44);
            SqlStr += ",[UserId]  = " + MyMain.Fld45;
            // SqlStr += ",[jobcy]  = " + UType.MyCtoDs(MyMain.Fld48);

            SqlStr += " Where JobNo = " + MyMain.Fld1;
            SqlStr += " and officeID = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += " and projectid = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += " and sno = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += " and jobcy = " + UType.MyCtoDs(MyMain.Fld48);
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region MyRegion ExpChargesHdr
    public DataSet GetExpChargesHdr()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += "  ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpChargeshdr a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.JobNo = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
        }

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetExpChargesHdrReport()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += "  ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpChargeshdr a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.JobNo = " + MyMain.Fld3;
        }

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }


    public string UpdateExpChargesHdr()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update ExpChargesHdr Set ";


            SqlStr += "[Invoice] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[Approved] = " + UType.MyCtoDs(MyMain.Fld6);

            SqlStr += ", [ExRateBuying] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ", [ExRateSelling] = " + UType.MyCtoDs(MyMain.Fld8);


            SqlStr += ",[Quotation] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",[Security] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[ReceivablePP] = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",[ReceivableCC] = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ",[PayableCC] = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",[PayablePP] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[ReceivableTotal] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[ReceivableTaxTotal] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[PayableTotal] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[PayableTaxTotal]  = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",[SD]  = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",[Net]  = " + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",[LSD]  = " + UType.MyCtoDs(MyMain.Fld21);

            SqlStr += ",[ManifestRemarks]  = " + Sqote + MyMain.Fld22 + Sqote;
            SqlStr += ",[PerShipment]  = " + Sqote + MyMain.Fld23 + Sqote;
            SqlStr += ",[EquipInvoice]  = " + Sqote + MyMain.Fld24 + Sqote;

            SqlStr += ",[ChequeNo]  = " + Sqote + MyMain.Fld29 + Sqote;
            SqlStr += ",[ChequeDate]  = " + MyMain.Fld30;

            SqlStr += ",[RemarksRec]  = " + Sqote + MyMain.Fld31 + Sqote;
            SqlStr += ",[RemarksPay]  = " + Sqote + MyMain.Fld32 + Sqote;

            SqlStr += ",[UserId]  = " + MyMain.Fld33;

            SqlStr += " Where JobNo = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and officeID = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += " and projectid = " + UType.MyCtoDs(MyMain.Fld3);
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region MyRegion ExpCharges
    public DataSet GetExpCharges()
    {
        SqlStr = "SELECT a.*,1 as particular1 ";
        SqlStr += "  ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpCharges a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.JobNo = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
            if (UType.MyCtoD(MyMain.Fld5) > 0)
            {
                SqlStr += " and a.ChargesType = " + MyMain.Fld5;
            }
        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.Chargesid = " + MyMain.Fld4;
        }
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetExpMaxCharges()
    {
        SqlStr = "SELECT max(a.sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpCharges a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.JobNo = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetExpMaxChargesPaymentID()
    {
        DataSet ds = null;
        try
        {
            SqlStr = "SELECT max(a.paymentid) as MaxPaymentID ";
            SqlStr += " FROM "; // officechart oc   ";
            SqlStr += "  ExpCharges a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
            if (UType.MyCtoD(MyMain.Fld1) > 0)
            {
                SqlStr += " where  ";
                SqlStr += " a.paymentidyear = " + MyMain.Fld1;
                SqlStr += " and a.officeid = " + MyMain.Fld2;
                SqlStr += " and a.ProjectId = " + MyMain.Fld3;
            }


            ds = GetDatasetSpNew(SqlStr);
        }
        catch (Exception ex)
        {

            string chk1 = ex.ToString();
        }
        return ds;
    }
    public string InsertExpCharges()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO ExpCharges";
            SqlStr += "(";
            SqlStr += "jobno";
            SqlStr += ",officeID";
            SqlStr += ",ProjectID";
            SqlStr += ",sno";
            SqlStr += ",Type1";
            SqlStr += ",Basis1";
            SqlStr += ",PPCC1";
            SqlStr += ",CollectBy1";
            SqlStr += ",SizeType1";
            SqlStr += ",DGNonDG1";
            SqlStr += ",PrincipalCode1";
            SqlStr += ",Currency1";
            SqlStr += ",Customer1";
            SqlStr += ",Tariff1";
            SqlStr += ",ChargesType";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + MyMain.Fld7;
            SqlStr += Comma + MyMain.Fld8;
            SqlStr += Comma + MyMain.Fld9;
            SqlStr += Comma + MyMain.Fld10;
            SqlStr += Comma + MyMain.Fld11;
            SqlStr += Comma + MyMain.Fld12;
            SqlStr += Comma + MyMain.Fld13;
            SqlStr += Comma + MyMain.Fld14;
            SqlStr += Comma + MyMain.Fld15;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateExpCharges()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update ExpCharges Set ";


            SqlStr += "[BillInvoice] = " + Sqote + UType.Chk10(MyMain.Fld5) + Sqote;
            SqlStr += ",[Charges] = " + UType.MyCtoDs(MyMain.Fld6);

            SqlStr += ", [Particular] = " + Sqote + UType.Chk10(MyMain.Fld7) + Sqote;
            SqlStr += ", [Description] = " + Sqote + UType.Chk10(MyMain.Fld8) + Sqote;


            SqlStr += ",[Type] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",[Type1] = 1";
            SqlStr += ",[Basis] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[Basis1] = 1";
            SqlStr += ",[PPCC] = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",[PPCC1] = 1";
            SqlStr += ",[CollectBy] = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ",[CollectBy1] =1 ";
            SqlStr += ",[SizeType] = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",[SizeType1] = 1";
            SqlStr += ",[RateGroup] = " + Sqote + UType.Chk10(MyMain.Fld14) + Sqote;
            SqlStr += ",[DGNonDG] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[DGNonDG1] = 1";
            SqlStr += ",[Shared] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[Shared1] = " + Sqote + Sqote;
            SqlStr += ",[PrincipalCode] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[PrincipalCode1] = 1";
            SqlStr += ",[Manual] = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",[Manual1]  = " + Sqote + Sqote;
            SqlStr += ",[Currency] = " + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",[Currency1] = 1";
            SqlStr += ",[Quantity] = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ",[Rate] = " + UType.MyCtoDs(MyMain.Fld22);
            SqlStr += ",[Amount] = " + UType.MyCtoDs(MyMain.Fld23);
            SqlStr += ",[Discount] = " + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ",[TaxApply] = " + UType.MyCtoDs(MyMain.Fld25);
            SqlStr += ",[TaxApply1] = " + Sqote + Sqote;

            SqlStr += ",[TaxAmount] = " + UType.MyCtoDs(MyMain.Fld26);
            SqlStr += ",[NetAmount] = " + UType.MyCtoDs(MyMain.Fld27);
            SqlStr += ",[ExRate] = " + UType.MyCtoDs(MyMain.Fld28);
            SqlStr += ",[LocalAmount] = " + UType.MyCtoDs(MyMain.Fld29);

            SqlStr += ",[Customer] = " + UType.MyCtoDs(MyMain.Fld30);
            SqlStr += ",[Customer1] = 1";
            SqlStr += ",[ManifestRemarks] = " + Sqote + UType.Chk10(MyMain.Fld31) + Sqote;
            SqlStr += ",[Tariff] = " + UType.MyCtoDs(MyMain.Fld32);
            SqlStr += ",[Tariff1] = 1";
            SqlStr += ",[Approved] =  " + Sqote + UType.Chk10(MyMain.Fld33) + Sqote;
            SqlStr += ",[ApprovedBy] =  " + Sqote + UType.Chk10(MyMain.Fld34) + Sqote;
            SqlStr += ",[ApprovedDate] =  " + Sqote + UType.Chk10(MyMain.Fld35) + Sqote;
            SqlStr += ",[ApprovedLog] =  " + Sqote + UType.Chk10(MyMain.Fld36) + Sqote;
            SqlStr += ",[PaymentID] = " + UType.MyCtoDs(MyMain.Fld40);
            SqlStr += ",[PaymentIDyear] = " + UType.MyCtoDs(MyMain.Fld41);
            SqlStr += ",[ExpenseDisplay] = " + UType.MyCtoDs(MyMain.Fld42);
            SqlStr += ",[ExpenseReceive] = " + UType.MyCtoDs(MyMain.Fld43);
            SqlStr += ",[Vno] = " + UType.MyCtoDs(MyMain.Fld44);
            SqlStr += ",[UserId]  = " + MyMain.Fld45;

            SqlStr += " Where JobNo = " + MyMain.Fld1;
            SqlStr += " and officeID = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += " and projectid = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += " and sno = " + UType.MyCtoDs(MyMain.Fld4);
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region MyRegion GetBLDetail
    public DataSet GetBLDetail()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += "  ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  BLDetail a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;

            SqlStr += " and a.jobno = " + MyMain.Fld3;
        }

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetBLDetailSelected()
    {
        SqlStr = "SELECT  [JobNo]      ,[IndexNo]         ,[Shipper]      ,[Consignee]      ,[NotifyParty1] ";

        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  BLDetail   "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " officeid = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            if (UType.MyCtoD(MyMain.Fld3) > 0)
            {
                SqlStr += " and jobno = " + MyMain.Fld3;
            }
        }

        SqlStr += " order by jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetMaxBLDetail()
    {
        SqlStr = "SELECT max(a.IndexNo) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  BLDetail a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.docno = " + MyMain.Fld1;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string InsertBLDetail()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO BLDetail";
            SqlStr += "(";
            SqlStr += "jobno";
            SqlStr += ",IndexNo";
            SqlStr += ",officeID";
            SqlStr += ",projectid";
            SqlStr += ",DocNo";

            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateBLDetail()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update BLDetail Set ";

            SqlStr += "officeID = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ",projectid = " + UType.MyCtoDs(MyMain.Fld4);
            // SqlStr += ",[IndexNo] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[Shipper] = " + Sqote + UType.Chk10(MyMain.Fld6) + Sqote;

            SqlStr += ", [Consignee] = " + Sqote + UType.Chk10(MyMain.Fld7) + Sqote;
            SqlStr += ", [NotifyParty1] = " + Sqote + UType.Chk10(MyMain.Fld8) + Sqote;


            SqlStr += ",[NotifyParty2] = " + Sqote + UType.Chk10(MyMain.Fld9) + Sqote;
            SqlStr += ",[PortofLoading] = " + Sqote + UType.Chk10(MyMain.Fld10) + Sqote;
            SqlStr += ",[DeliveryAgent] = " + Sqote + UType.Chk10(MyMain.Fld11) + Sqote;
            SqlStr += ",[PortofDischarge] = " + Sqote + UType.Chk10(MyMain.Fld12) + Sqote;
            SqlStr += ",[PortofDelivery] = " + Sqote + UType.Chk10(MyMain.Fld13) + Sqote;
            SqlStr += ",[CBM] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[Packages] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[ManualNetWt] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[GrossWt] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[TareWt]  = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",[ProductQty]  = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",[UnitPerValue]  = " + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",[MarkNoContainerNo]  = " + Sqote + UType.Chk10(MyMain.Fld21) + Sqote;
            SqlStr += ",[NoofPackagesShippingUnits]  = " + Sqote + UType.Chk10(MyMain.Fld22) + Sqote;
            //SqlStr += ",[MarkNoContainerNo]  = " + Sqote + UType.Chk10(MyMain.Fld22) + Sqote;
            SqlStr += ",[MarksGrossWeight]  = " + UType.MyCtoDs(MyMain.Fld23);
            SqlStr += ",[Measurement]  = " + Sqote + UType.Chk10(MyMain.Fld24) + Sqote;
            SqlStr += ",[OnBoardDate]  = " + UType.MyCtoDs(MyMain.Fld25);
            SqlStr += ",[DateofIssue]  = " + UType.MyCtoDs(MyMain.Fld26);
            SqlStr += ",[CargoType]  = " + UType.MyCtoDs(MyMain.Fld27);
            SqlStr += ",[IndexType]  = " + UType.MyCtoDs(MyMain.Fld28);

            SqlStr += ",[FreightType]  = " + UType.MyCtoDs(MyMain.Fld29);
            SqlStr += ",[Unit]  = " + UType.MyCtoDs(MyMain.Fld30);
            SqlStr += ",[WtUnit]  = " + UType.MyCtoDs(MyMain.Fld31);
            SqlStr += ",[ProductUnit]  = " + UType.MyCtoDs(MyMain.Fld32);
            SqlStr += ",[SerialType]  = " + UType.MyCtoDs(MyMain.Fld33);
            SqlStr += ",[SerialCategory]  = " + UType.MyCtoDs(MyMain.Fld34);
            SqlStr += ",[CountryofOrigin]  = " + UType.MyCtoDs(MyMain.Fld35);
            SqlStr += ",[SerialException]  = " + UType.MyCtoDs(MyMain.Fld36);
            SqlStr += ",[HSCode]  = " + Sqote + UType.Chk10(MyMain.Fld37) + Sqote;
            SqlStr += ",[Freight]  = " + UType.MyCtoDs(MyMain.Fld38);
            SqlStr += ",[OriginalBL]  = " + UType.MyCtoDs(MyMain.Fld39);
            SqlStr += ",[AgentStamp]  = " + Sqote + UType.Chk10(MyMain.Fld40) + Sqote;
            SqlStr += ",[HazmatCode]  = " + Sqote + UType.Chk10(MyMain.Fld41) + Sqote;
            SqlStr += ",[DescriptionofGoodsPackages]  = " + Sqote + UType.Chk10(MyMain.Fld42) + Sqote;
            SqlStr += ",[PlaceofIssue]  = " + Sqote + UType.Chk10(MyMain.Fld43) + Sqote;
            SqlStr += ",[cbManualNetWt]  = " + UType.MyCtoDs(MyMain.Fld44);

            //  SqlStr += ",[ShipperCity]  = " + UType.MyCtoDs(MyMain.Fld47);
            //  SqlStr += ",[ConsgCity]  = " + UType.MyCtoDs(MyMain.Fld48);
            SqlStr += ",ShipDate = " + UType.MyCtoDs(MyMain.Fld60);
            SqlStr += ",TypeOfBL = " + UType.MyCtoDs(MyMain.Fld61);
            //ShipperCity
            SqlStr += " Where JobNo = " + MyMain.Fld1;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region OtherInfo
    public DataSet GetOtherInfo()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  OtherInfo a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " officeid = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            if (UType.MyCtoD(MyMain.Fld3) > 0)
            {
                SqlStr += " and jobno = " + MyMain.Fld3;
            }
        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertOtherInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO OtherInfo";
            SqlStr += "(";

            //SqlStr += "IndexNo";
            SqlStr += "officeID";
            SqlStr += ",projectid";
            SqlStr += ",jobno";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;

            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string UpdateOtherInfo()
    {
        string retVal = string.Empty;
        string SqlStr = string.Empty;
        try
        {
            SqlStr = "Update OtherInfo Set ";
            SqlStr += " OfficeId = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += ", ProjectId = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += ", AnyOtherInformation = " + Sqote + UType.Chk10(MyMain.Fld4) + Sqote;
            SqlStr += " ,Remarks = " + Sqote + UType.Chk10(MyMain.Fld5) + Sqote;
            SqlStr += ", CargoManifestRemarks = " + Sqote + UType.Chk10(MyMain.Fld6) + Sqote;
            SqlStr += ", DocumentType = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ", ITNumber = " + Sqote + UType.Chk10(MyMain.Fld8) + Sqote;
            SqlStr += ", Remarks1  = " + Sqote + UType.Chk10(MyMain.Fld9) + Sqote;
            SqlStr += ", LocationDate = " + Sqote + UType.Chk10(MyMain.Fld10) + Sqote;
            SqlStr += ", CPRS = " + Sqote + UType.Chk10(MyMain.Fld11) + Sqote;
            SqlStr += ", LocationDate1 = " + Sqote + UType.Chk10(MyMain.Fld12) + Sqote;
            SqlStr += ", LCNo = " + Sqote + UType.Chk10(MyMain.Fld13) + Sqote;
            SqlStr += ", LCDate = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ", SubBLNo = " + Sqote + UType.Chk10(MyMain.Fld15) + Sqote;
            SqlStr += ", DeStuffingDate = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ", DeliveryDate = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ", Buyer = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ", BuyerHouse = " + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ", CargoPickup = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ", CargoPickupDate = " + UType.MyCtoDs(MyMain.Fld22);
            SqlStr += ", CargoPickupTime = " + UType.MyCtoDs(MyMain.Fld23);
            SqlStr += ", ContainerPickup = " + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ", ContainerPickupDate = " + UType.MyCtoDs(MyMain.Fld26);
            SqlStr += ", ContainerPickupTime = " + UType.MyCtoDs(MyMain.Fld27);


            SqlStr += ", CargoDropOff = " + UType.MyCtoDs(MyMain.Fld31);
            SqlStr += ", CargoDropOffDate = " + UType.MyCtoDs(MyMain.Fld32);
            SqlStr += ", CargoDropOffTime = " + UType.MyCtoDs(MyMain.Fld33);
            SqlStr += ", ContainerDrop = " + UType.MyCtoDs(MyMain.Fld34);
            SqlStr += ", ContainerDropDate = " + UType.MyCtoDs(MyMain.Fld35);

            SqlStr += ", ContainerDropTime = " + UType.MyCtoDs(MyMain.Fld36);

            SqlStr += " Where JobNo = " + MyMain.Fld1;


            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion
    #region Routing
    public DataSet GetRouting()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Routing a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " officeid = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            if (UType.MyCtoD(MyMain.Fld3) > 0)
            {
                SqlStr += " and jobno = " + MyMain.Fld3;
            }
        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertRouting()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO Routing";
            SqlStr += "(";


            SqlStr += "officeID";
            SqlStr += ",projectid";
            SqlStr += ",jobno";
            SqlStr += ",IndexNo";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string UpdateRouting()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Routing Set ";

            SqlStr += "officeID = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += ",projectid = " + UType.MyCtoDs(MyMain.Fld2);

            SqlStr += ",[ServiceType] = " + Sqote + UType.Chk10(MyMain.Fld4) + Sqote;
            SqlStr += ",[CargoType] = " + UType.MyCtoDs(MyMain.Fld5);

            SqlStr += ", [PlaceofReceipt] = " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ", [PlaceofReceiptdate] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ", [PortofLoading] = " + UType.MyCtoDs(MyMain.Fld8);
            SqlStr += ", [PortofLoadingdate] = " + UType.MyCtoDs(MyMain.Fld9);

            SqlStr += ", [PortofTransShipment] = " + UType.MyCtoDs(MyMain.Fld10);

            SqlStr += ", [PortofTransShipmentdate] = " + UType.MyCtoDs(MyMain.Fld11);

            SqlStr += ", [PortofDischarge] = " + UType.MyCtoDs(MyMain.Fld12);

            SqlStr += ", [PortofDischargedate] = " + UType.MyCtoDs(MyMain.Fld13);


            SqlStr += ", [FinalDestination] = " + UType.MyCtoDs(MyMain.Fld14);

            SqlStr += ", [FreightPayableAt] = " + UType.MyCtoDs(MyMain.Fld15);

            SqlStr += ", [DepotFacility] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ", [Terminal] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ", [ViaPort] = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ", [Delivery] = " + UType.MyCtoDs(MyMain.Fld19);

            //SqlStr += ", [TransShipment] = " + UType.MyCtoDs(MyMain.Fld20);

            SqlStr += ", [FeederVessel] = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ", [voyage] = " + Sqote + UType.Chk10(MyMain.Fld22) + Sqote;
            SqlStr += ", [TransShipment] = " + UType.MyCtoDs(MyMain.Fld23);


            SqlStr += " Where JobNo = " + MyMain.Fld3;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region Invoice
    public DataSet GetInvoice()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Invoice a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " officeid = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            if (UType.MyCtoD(MyMain.Fld3) > 0)
            {
                SqlStr += " and jobno = " + MyMain.Fld3;
            }
        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertInvoice()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO Invoice";
            SqlStr += "(";


            SqlStr += "officeID";
            SqlStr += ",projectid";
            SqlStr += ",jobno";
            //SqlStr += ",IndexNo";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            // SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateInvoice()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Invoice Set ";
            SqlStr += "officeID = " + UType.MyCtoDs(MyMain.Fld30);
            SqlStr += ",projectid = " + UType.MyCtoDs(MyMain.Fld31);

            SqlStr += ",[tran] = " + UType.MyCtoDs(MyMain.Fld1); //Sqote + UType.Chk10(MyMain.Fld1) + Sqote;
            SqlStr += ",[InvDate] = " + UType.MyCtoDs(MyMain.Fld2);

            SqlStr += ", [ReferenceNo] = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ", [InvStatus] = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += ", [Category] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ", [Client] = " + UType.MyCtoDs(MyMain.Fld6);

            SqlStr += ", [InvSequence] = " + Sqote + UType.Chk10(MyMain.Fld7) + Sqote;
            SqlStr += ", [InvoiceType] = " + UType.MyCtoDs(MyMain.Fld8);
            SqlStr += ", [RefTranNo] = " + Sqote + UType.Chk10(MyMain.Fld9) + Sqote;
            SqlStr += ", [Operation] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ", [Currency] = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ", [CostCenter] = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ", [BillTo] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ", [CheckRoundOff] = " + Sqote + UType.Chk10(MyMain.Fld15) + Sqote;
            SqlStr += ", [CheckPP] = " + Sqote + UType.Chk10(MyMain.Fld16) + Sqote;
            SqlStr += ", [CheckCC] = " + Sqote + UType.Chk10(MyMain.Fld17) + Sqote;
            SqlStr += ", [CheckManualRemarks] = " + Sqote + UType.Chk10(MyMain.Fld18) + Sqote;
            SqlStr += ", [VoucherNo] = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ", [Remarks] = " + Sqote + UType.Chk10(MyMain.Fld20) + Sqote;
            SqlStr += ", [TotalAmount] = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ", [NetAmountIncTax] = " + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ", [Discount] = " + UType.MyCtoDs(MyMain.Fld25);
            SqlStr += ", [LocalAmount] = " + UType.MyCtoDs(MyMain.Fld26);

            SqlStr += " Where JobNo = " + MyMain.Fld11;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region InvoiceDtl
    public DataSet GetInvoiceDtl()
    {
        SqlStr = "SELECT a.jobNo,a.sno,a.charges,a.ChargesDescription,a.Size ,'1' as charges1,'1' as size1,'1' as  DGNonDG1,'1' as  Principal1,'1' as  Currency1 ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  InvoiceDtl a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " officeid = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            if (UType.MyCtoD(MyMain.Fld3) > 0)
            {
                SqlStr += " and jobno = " + MyMain.Fld3;
            }
        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetInvoiceDtlgrid()
    {
        SqlStr = "SELECT a.*,'1' as charges1,'1' as size1,'1' as  DGNonDG1,'1' as  Principal1,'1' as  Currency1 ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  InvoiceDtl a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " officeid = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            if (UType.MyCtoD(MyMain.Fld3) > 0)
            {
                SqlStr += " and jobno = " + MyMain.Fld3;
            }
        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string InsertInvoiceDtl()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO InvoiceDtl";
            SqlStr += "(";


            SqlStr += "officeID";
            SqlStr += ",projectid";
            SqlStr += ",jobno";
            SqlStr += ",SNo";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string UpdateInvoiceDtl()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update InvoiceDtl Set ";

            // oMyMain.Fld1 = this.txtJobNo.Text; //((TextBox)item.FindControl("TxtJobNo")).Text;
            //oMyMain.Fld2 = ((TextBox)item.FindControl("TxtSNo")).Text;
            SqlStr += "SNo = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += ",officeID = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ",projectid = " + UType.MyCtoDs(MyMain.Fld4);

            SqlStr += ",Charges = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",ChargesDescription = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ",Size = " + UType.MyCtoDs(MyMain.Fld8);
            SqlStr += ",RateGroup = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",DGNonDG = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",Principal = " + UType.MyCtoDs(MyMain.Fld13);

            SqlStr += ",Qty = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",Currency = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",Amount = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",Discount = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",NetAmount = " + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",Margins = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ",NetAmountIncTax = " + UType.MyCtoDs(MyMain.Fld22);
            SqlStr += ",ExRate = " + UType.MyCtoDs(MyMain.Fld23);
            SqlStr += ",LocalAmount = " + UType.MyCtoDs(MyMain.Fld24);



            SqlStr += " Where JobNo = " + MyMain.Fld1;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet GetMaxInvoiceDtl()
    {
        SqlStr = "SELECT max(a.sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  InvoiceDtl a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.jobno = " + MyMain.Fld1;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    #endregion

    public DataSet GetOffice()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfOffice  "; // inner join actchart ac on oc.actcode = ac.actcode "; 


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetProject()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfProject  "; // inner join actchart ac on oc.actcode = ac.actcode "; 


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetAllRole()
    {

        SqlStr = "select * from rfrole";
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " Where roleid = " + MyMain.Fld1;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetUserRole()
    {
        if (UType.MyCtoD(MyMain.Fld1) < 1)
        {
            MyMain.Fld1 = "2";
        }

        SqlStr = "select e.rolename,d.MenuText from UserInfo a ";

        SqlStr += "inner join UserRole b on a.UserId = b.UserId ";
        SqlStr += "inner join RfRoleMenu c on b.UserRoleId = c.RoleId ";
        SqlStr += "inner join RfMenu d on c.MenuId = d.MenuId ";
        //SqlStr += " inner join RfRole e on a.RoleId = e.RoleId ";
        SqlStr += " inner join RfRole e on a.RoleId = e.RoleId ";
        SqlStr += "where a.userid = " + MyMain.Fld1;



        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetUserRole1()
    {


        SqlStr = "select a.* from UserRole a ";


        SqlStr += "where a.userid = " + MyMain.Fld1;



        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetUserRoleByUser()
    {
        if (UType.MyCtoD(MyMain.Fld1) < 1)
        {
            MyMain.Fld1 = "2";
        }

        SqlStr = "select e.rolename,d.MenuText from UserInfo a ";

        SqlStr += "inner join UserRole b on a.UserId = b.UserId ";
        SqlStr += "inner join RfRoleMenu c on b.UserRoleId = c.RoleId ";
        SqlStr += "inner join RfMenu d on c.MenuId = d.MenuId ";
        SqlStr += " inner join RfRole e on a.RoleId = e.RoleId ";
        SqlStr += "where a.userid = " + MyMain.Fld1;



        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string UpdateUserRole()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update UserRole Set ";

            SqlStr += "userroleid = " + UType.MyCtoDs(MyMain.Fld2);

            SqlStr += " Where userid = " + UType.MyCtoDs(MyMain.Fld1);

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        try
        {
            SqlStr = "Update Userinfo Set ";

            SqlStr += "roleid = " + UType.MyCtoDs(MyMain.Fld2);

            SqlStr += " Where userid = " + UType.MyCtoDs(MyMain.Fld1);

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetAllMenu()
    {

        SqlStr = "select * from rfMenu";
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " Where Menuid = " + MyMain.Fld1;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetRfRoleMenu()
    {

        SqlStr = "select * from RfRoleMenu";
        if (UType.MyCtoD(MyMain.Fld1) > 0 && UType.MyCtoD(MyMain.Fld2) > 0)
        {
            SqlStr += " Where userid = " + MyMain.Fld1;
            SqlStr += " and Menuid = " + MyMain.Fld2;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetMenuByRole()
    {

        SqlStr = "select * from RfRoleMenu";
        if (UType.MyCtoD(MyMain.Fld1) > 0 && UType.MyCtoD(MyMain.Fld2) > 0)
        {
            SqlStr += " Where roleid = " + MyMain.Fld1;
            SqlStr += " and Menuid = " + MyMain.Fld2;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string InsertRfRoleMenu()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfRoleMenu ";
            SqlStr += "(";
            SqlStr += "OfficeID ";
            SqlStr += ",ProjectId ";
            SqlStr += ",DepartId ";
            SqlStr += ",RoleId ";
            SqlStr += ",MenuId ";
            SqlStr += ",MenuSno";
            SqlStr += ")";
            SqlStr += "VALUES( ";

            SqlStr += MyMain.Fld10;
            SqlStr += Comma + MyMain.Fld11;
            SqlStr += Comma + MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetRoleMenu()
    {
        if (UType.MyCtoD(MyMain.Fld1) < 1)
        {
            MyMain.Fld1 = "2";
        }

        SqlStr = "select e.rolename,d.MenuText from UserRole b ";

        SqlStr += " inner join RfRoleMenu c on b.UserRoleId = c.RoleId ";
        SqlStr += " inner join RfMenu d on c.MenuId = d.MenuId ";
        SqlStr += " inner join RfRole e on b.UserRoleId = e.RoleId ";
        SqlStr += " where b.UserRoleId =  " + MyMain.Fld1;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetRoleMenuByRole()
    {
        if (UType.MyCtoD(MyMain.Fld1) < 1)
        {
            MyMain.Fld1 = "2";
        }

        SqlStr = "select a.roleid,a.RoleName,c.menuid, c.MenuText from RfRole a ";

        SqlStr += " inner join RfRoleMenu b on a.RoleId = b.RoleId ";
        SqlStr += " inner join RfMenu c on b.MenuId = c.MenuId ";

        SqlStr += " where a.RoleId =  " + MyMain.Fld1;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    #region MyRegion Receipt
    public DataSet GetReceipt()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += "  ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Receipt a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.JobNo = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
        }

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string InsertReceipt()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO Receipt";
            SqlStr += "(";
            SqlStr += "officeid";
            SqlStr += ",ProjectId";
            SqlStr += ",jobno";


            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateReceipt()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Receipt Set ";


            SqlStr += "[Invoice] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[Approved] = " + UType.MyCtoDs(MyMain.Fld6);

            SqlStr += ", [ExRateBuying] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ", [ExRateSelling] = " + UType.MyCtoDs(MyMain.Fld8);


            SqlStr += ",[Quotation] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",[Security] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[ReceivablePP] = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",[ReceivableCC] = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ",[PayableCC] = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",[PayablePP] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[ReceivableTotal] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[ReceivableTaxTotal] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[PayableTotal] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[PayableTaxTotal]  = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",[SD]  = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",[Net]  = " + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",[LSD]  = " + UType.MyCtoDs(MyMain.Fld21);

            SqlStr += ",[ManifestRemarks]  = " + Sqote + MyMain.Fld22 + Sqote;
            SqlStr += ",[PerShipment]  = " + Sqote + MyMain.Fld23 + Sqote;
            SqlStr += ",[EquipInvoice]  = " + Sqote + MyMain.Fld24 + Sqote;

            SqlStr += " Where JobNo = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and officeID = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += " and projectid = " + UType.MyCtoDs(MyMain.Fld3);
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region MyRegion ReceiptDtl
    public DataSet GetReceiptDtl()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += "  ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ReceiptDtl a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.JobNo = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
        }

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string InsertReceiptDtl()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO ReceiptDtl";
            SqlStr += "(";
            SqlStr += "officeid";
            SqlStr += ",ProjectId";
            SqlStr += ",jobno";


            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateReceiptDtl()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update ReceiptDtl Set ";


            SqlStr += "[Invoice] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[Approved] = " + UType.MyCtoDs(MyMain.Fld6);

            SqlStr += ", [ExRateBuying] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ", [ExRateSelling] = " + UType.MyCtoDs(MyMain.Fld8);


            SqlStr += ",[Quotation] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",[Security] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[ReceivablePP] = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",[ReceivableCC] = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ",[PayableCC] = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",[PayablePP] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[ReceivableTotal] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[ReceivableTaxTotal] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[PayableTotal] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[PayableTaxTotal]  = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",[SD]  = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",[Net]  = " + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",[LSD]  = " + UType.MyCtoDs(MyMain.Fld21);

            SqlStr += ",[ManifestRemarks]  = " + Sqote + MyMain.Fld22 + Sqote;
            SqlStr += ",[PerShipment]  = " + Sqote + MyMain.Fld23 + Sqote;
            SqlStr += ",[EquipInvoice]  = " + Sqote + MyMain.Fld24 + Sqote;

            SqlStr += " Where JobNo = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and officeID = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += " and projectid = " + UType.MyCtoDs(MyMain.Fld3);
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet GetMaxReceiptDtl()
    {
        SqlStr = "SELECT max(a.sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ReceiptDtl a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.JobNo = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    #endregion
    public DataSet GetUserMenu()
    {

        SqlStr = "select a.MenuText,b.IsEdit,b.MenuSno,'' as IsEdit1,b.IsView, '' as IsView1,b.level,a.MenuID from rfmenu a  ";
        SqlStr += "left join RfUserMenu b on a.MenuId = b.MenuId ";

        if (MyMain.Fld3.Length > 0)
        {
            SqlStr += " Where officeId =   " + MyMain.Fld1;
            SqlStr += " and projectId =   " + MyMain.Fld2;
            SqlStr += " and  Userid =  " + MyMain.Fld3;
        }

        DataSet Res = GetDatasetSpNew(SqlStr);
        return Res;
    }
    #region UserMenu
    public DataSet GetUserMenu1()
    {
        SqlStr = "SELECT a.* FROM ";
        SqlStr += "  RfUserMenu a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.UserId = " + MyMain.Fld3;
            SqlStr += " and a.MenuId = " + MyMain.Fld4;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);

        return ds;
    }
    public string AddUserMenu()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfUserMenu";
            SqlStr += "(";
            SqlStr += "officeid";
            SqlStr += ",ProjectId";
            SqlStr += ",UserId";
            SqlStr += ",MenuId";



            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateUserMenu()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update [RfUserMenu] Set ";

            //SqlStr += "[MenuId] = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += "[Level] =" + UType.MyCtoDs(MyMain.Fld5); ;
            SqlStr += ",[IsView] = " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ",[IsEdit] = " + UType.MyCtoDs(MyMain.Fld7);

            SqlStr += " Where  ";
            SqlStr += "Officeid = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and projectid = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += " and [USerID] = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += " and MenuId = " + UType.MyCtoDs(MyMain.Fld4);
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion
    public DataSet DoCheckEdit()
    {
        SqlStr = "SELECT  ";
        SqlStr += "  a.* from RfUserMenu a ";
        SqlStr += " where a.OfficeId = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.UserID = " + MyMain.Fld3;
        SqlStr += " and a.MenuID = " + MyMain.Fld4;
        SqlStr += " and a.IsEdit =  " + MyMain.Fld5;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }


    public DataSet GetHBLInfoByJobNo()
    {
        SqlStr = "SELECT b.*, '0' as  [IndexType1], '0' as  [JobNature1]";
        SqlStr += " FROM jobinfo a ";
        SqlStr += " inner join HBLInfo b on a.OfficeId = B.OfficeId and a.ProjectId = b.ProjectId and a.HBLNo = b.HBLNo   ";

        SqlStr += " where  ";

        SqlStr += "  a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.jobNo = " + MyMain.Fld3;



        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetDdlDescrition()
    {
        SqlStr = "SELECT * FROM ";
        SqlStr += "   rfcustomer a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.customerid = " + MyMain.Fld1;

        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetMenuAll()
    {
        SqlStr = "SELECT a.*,'' as level,'' as IsView1,'' as IsView,'' as IsEdit1,'' as IsEdit FROM ";
        SqlStr += "   rfmenu a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetCountryIDFromCustomer()
    {
        SqlStr = "SELECT b.City_ID,City_Name ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCustomer a  ";
        SqlStr += "inner join RFCities b on a.CustomerTel = b.Country_ID ";
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.CustomerID = " + MyMain.Fld1;
        }

        SqlStr += " order by b.City_Name  ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetCurrency()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfCountries a  ";
        //SqlStr += "inner join RFCities b on a.CustomerTel = b.Country_ID ";
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.Country_id = " + MyMain.Fld1;
        }
        if (UType.MyCtoD(MyMain.Fld1) < 1)
        {
            SqlStr += " where  ";
            SqlStr += " a.Currency_Abb != '' ";
        }
        if (UType.MyCtoD(MyMain.Fld2) == 1)
        {
            SqlStr += " and currencysts = 1 ";
        }
        SqlStr += " order by a.sortno, a.Country_Name  ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string DeleteWeBOCIndex()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "DELETE * FROM Indexes;";


            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        try
        {
            SqlStr = "DELETE * FROM Items;";

            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        try
        {
            SqlStr = "DELETE * FROM ContainerItems;";

            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        try
        {
            SqlStr = "DELETE * FROM Containers;";

            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        try
        {
            SqlStr = "DELETE * FROM emptyContainers;";

            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertWeBOCIndex()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO Indexes "; // SqlStr = "INSERT INTO Indexes IN 'WEBOC12.mdb' ";
            SqlStr += "(";
            SqlStr += "IndexNumber ";
            SqlStr += ",Documentno ";
            SqlStr += ") ";
            SqlStr += "VALUES ( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + Sqote + MyMain.Fld2 + Sqote;


            SqlStr += " );";
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string UpdateWeBOCIndex()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Indexes Set ";


            SqlStr += "	VIRNumber	= " + Sqote + UType.Chk10(MyMain.Fld3, 50) + Sqote;
            SqlStr += ",	BLNumber	= " + Sqote + UType.Chk10(MyMain.Fld4, 30) + Sqote;
            SqlStr += ",	TypeOfBL	= " + Sqote + UType.Chk10(MyMain.Fld5, 50) + Sqote;
            SqlStr += ",	CargoType	= " + Sqote + UType.Chk10(MyMain.Fld6, 20) + Sqote;
            SqlStr += ",	StuffingStatus	= " + Sqote + UType.Chk10(MyMain.Fld7, 3) + Sqote;
            SqlStr += ",	DeliveryMode	= " + Sqote + UType.Chk10(MyMain.Fld8, 3) + Sqote;

            SqlStr += ",	ConsigneeName	= " + Sqote + UType.Chk10(MyMain.Fld9, 50) + Sqote;
            SqlStr += ",	ConsigneeCountry = " + Sqote + UType.Chk10(MyMain.Fld10, 2) + Sqote;
            SqlStr += ",	ConsigneeCity	= " + Sqote + UType.Chk10(MyMain.Fld11, 5) + Sqote;
            SqlStr += ",	ConsigneeAddress	= " + Sqote + UType.Chk10(MyMain.Fld12, 250) + Sqote;
            SqlStr += ",	ShipperName	= " + Sqote + UType.Chk10(MyMain.Fld13, 50) + Sqote;
            SqlStr += ",	ShipperCountry	= " + Sqote + UType.Chk10(MyMain.Fld14, 2) + Sqote;
            SqlStr += ",	ShipperCity	= " + Sqote + UType.Chk10(MyMain.Fld15, 5) + Sqote;
            SqlStr += ",	ShipperAddress	= " + Sqote + UType.Chk10(MyMain.Fld16, 250) + Sqote;
            SqlStr += ",	PortofShipment	= " + Sqote + UType.Chk10(MyMain.Fld17, 5) + Sqote;
            SqlStr += ",	FinalDestination	= " + Sqote + UType.Chk10(MyMain.Fld18, 5) + Sqote;
            SqlStr += ",	PortOfDischarge	= " + Sqote + UType.Chk10(MyMain.Fld19, 5) + Sqote;
            //SqlStr += ",	ViaPort	= " + Sqote + UType.Chk10(MyMain.Fld20) + Sqote;
            //SqlStr += ",	UCRN	= " + Sqote + UType.Chk10(MyMain.Fld21) + Sqote;
            SqlStr += ",	MarksNumbers	= " + Sqote + UType.Chk10(MyMain.Fld22, 250) + Sqote;
            SqlStr += ",	GrossWeight	= " + Sqote + UType.Chk10(MyMain.Fld23, 50) + Sqote;
            SqlStr += ",	NetWeight	= " + Sqote + UType.Chk10(MyMain.Fld24, 50) + Sqote;
            SqlStr += ",	PortofLoading	= " + Sqote + UType.Chk10(MyMain.Fld25, 255) + Sqote;
            SqlStr += ",	IndexType	= " + Sqote + UType.Chk10(MyMain.Fld26, 255) + Sqote;


            SqlStr += " Where  ";
            SqlStr += " IndexNumber = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and DocumentNo = " + Sqote + UType.Chk10(MyMain.Fld2) + Sqote;


            retVal = NonQryCmd(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertWeBOCItems()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO Items "; // SqlStr = "INSERT INTO Indexes IN 'WEBOC12.mdb' ";
            SqlStr += "(";
            SqlStr += "IndexNumber ";
            SqlStr += ",ItemSerialNumber ";


            SqlStr += ") ";
            SqlStr += "VALUES ( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + Sqote + MyMain.Fld2 + Sqote;


            SqlStr += " );";
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string UpdateWeBOCItems()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Items Set ";


            SqlStr += "	ModeOfPacking	= " + Sqote + UType.Chk10(MyMain.Fld3, 50) + Sqote;
            SqlStr += ",	HSCode	= " + Sqote + UType.Chk10(MyMain.Fld4, 50) + Sqote;
            SqlStr += ",	Description	= " + Sqote + UType.Chk10(MyMain.Fld5, 250) + Sqote;
            //SqlStr += ",	UNDGCode	= " + Sqote + UType.Chk10(MyMain.Fld6) + Sqote;
            SqlStr += ",	Quantity	= " + UType.MyCtoDs(MyMain.Fld7);



            SqlStr += " Where  ";
            SqlStr += " IndexNumber = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and ItemSerialNumber = " + Sqote + UType.Chk10(MyMain.Fld2) + Sqote;


            retVal = NonQryCmd(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string MyReplace(string InpStr)
    {
        string retVal = "";
        foreach (char c in InpStr)
        {
            if (c != '&')
            {
                retVal = retVal + c.ToString();
            }
        }
        return retVal;

    }
    public string InsertContainerItems()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO ContainerItems "; // SqlStr = "INSERT INTO Indexes IN 'WEBOC12.mdb' ";
            SqlStr += "(";
            SqlStr += "ContainerNumber ";
            SqlStr += ",ItemSerialNumber ";
            SqlStr += ",IndexNumber ";
            SqlStr += ",PackingQuantity ";

            SqlStr += ") ";
            SqlStr += "VALUES ( ";
            SqlStr += Sqote + MyMain.Fld1 + Sqote;
            SqlStr += Comma + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;


            SqlStr += " );";
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertContainer()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO Containers "; // SqlStr = "INSERT INTO Indexes IN 'WEBOC12.mdb' ";
            SqlStr += "(";
            SqlStr += "IndexNumber ";
            SqlStr += ",ContainerNumber ";
            SqlStr += ",SealNumber ";
            SqlStr += ",ISOCode ";
            SqlStr += ",GrossWeight ";
            SqlStr += ",NetWeight ";
            SqlStr += ",SOC ";
            SqlStr += ",ContainerOwnershipNTN ";
            SqlStr += ") ";
            SqlStr += "VALUES ( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += Comma + Sqote + UType.Chk10(MyMain.Fld3, 50) + Sqote;
            SqlStr += Comma + Sqote + UType.Chk10(MyMain.Fld4, 50) + Sqote;
            SqlStr += Comma + Sqote + UType.Chk10(MyMain.Fld5, 50) + Sqote;
            SqlStr += Comma + Sqote + UType.Chk10(MyMain.Fld6, 50) + Sqote;
            SqlStr += Comma + Sqote + UType.Chk10(MyMain.Fld7, 50) + Sqote;
            SqlStr += Comma + Sqote + UType.Chk10(MyMain.Fld8, 255) + Sqote;

            SqlStr += " );";
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertEmptyContainer()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO EmptyContainers "; // SqlStr = "INSERT INTO Indexes IN 'WEBOC12.mdb' ";
            SqlStr += "(";
            SqlStr += "ContainerNumber ";
            SqlStr += ",ISOCode ";
            SqlStr += ",TareWeight ";
            SqlStr += ",VIRNumber ";
            SqlStr += ",PortOfDischarge ";
            SqlStr += ",ContainerOwnershipNTN ";
            SqlStr += ") ";
            SqlStr += "VALUES ( ";
            SqlStr += Sqote + MyMain.Fld1 + Sqote;
            SqlStr += Comma + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += Comma + Sqote + MyMain.Fld3 + Sqote;
            SqlStr += Comma + Sqote + MyMain.Fld4 + Sqote;
            SqlStr += Comma + Sqote + MyMain.Fld5 + Sqote;
            SqlStr += Comma + Sqote + MyMain.Fld6 + Sqote;

            SqlStr += " );";
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }


    public string NonQryCmd(string Str1)
    {
        bool result = false;
        string retVal = string.Empty;

        OleDbCommand oCmd = null;
        OleDbDataAdapter oAdapter = null;
        OleDbConnection oCon = Connection.OleDbConnection;

        try
        {
            oCmd = new OleDbCommand();
            oAdapter = new OleDbDataAdapter();
            oCmd.Connection = Connection.OleDbConnection;

            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = Str1;
            if (Str1.Trim().Substring(0, 1) == "I")
            {
                oAdapter.InsertCommand = oCmd;
            }
            if (Str1.Trim().Substring(0, 1) == "U")
            {
                oAdapter.UpdateCommand = oCmd;
                DataTable dt1 = new DataTable();
                oAdapter.Update(dt1);

            }
            if (Str1.Trim().Substring(0, 1) == "D")
            {
                oAdapter.DeleteCommand = oCmd;

            }

            if (oCmd.ExecuteNonQuery() > 0)
            {
                result = true;
            }
        }
        catch (Exception ex)
        {
            result = false;
            throw;
        }
        finally
        {
            oCmd.Dispose();
            oAdapter.Dispose();

        }

        return retVal;
    }
    public DataSet GetDOinfo()
    {
        SqlStr = " select a.DocNo, a.*,b.*,c.* from jobinfo a  ";
        SqlStr += "   inner join manifest b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.DocNo = b.DocNo ";
        SqlStr += "   left join BLDetail c on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.JobNo = c.JobNo ";
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {

            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;

            SqlStr += " and a.jobno = " + MyMain.Fld3;

            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;

            SqlStr += " and a.jobno = " + MyMain.Fld3;

        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    #region Region DeliveryOrder

    public DataSet GetDoDetailInfo()
    {
        SqlStr = " select a.DocNo, a.*,b.vessel,b.voyage,b.berthname,b.LocationID,b.VIRNo,b.IGMNo,b.ArrivalDate,b.shippinglineid,b.ManifestRefNo  ";
        SqlStr += " ,c.*,d.mblno,d.mbldate,e.hblno,e.hbldate,f.*  ";
        SqlStr += "  from jobinfo a  ";
        SqlStr += "   inner join manifest b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.DocNo = b.DocNo ";
        SqlStr += "   left join BLDetail c on a.OfficeId = c.OfficeId and a.projectid = c.projectid and a.JobNo = c.JobNo ";
        SqlStr += "   left join MBLinfo d on a.OfficeId = d.OfficeId and a.projectid = d.projectid and a.MBLNo = d.MblNo ";
        SqlStr += "   left join HBLinfo e on a.OfficeId = e.OfficeId and a.projectid = e.projectid and a.HBLNo = e.hblNo";
        SqlStr += "   inner join DeliveryOrder f on a.OfficeId = f.OfficeId and a.projectid = f.projectid and a.JobNo = f.JobNo ";
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {

            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;

            SqlStr += " and a.jobno = " + MyMain.Fld3;



        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetDeliveryOrder()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  DeliveryOrder a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;

            SqlStr += " and a.jobno = " + MyMain.Fld3;
        }

        //SqlStr += " order by a.docno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxDeliveryOrder()
    {
        string CurYear = DateTime.Now.ToString("yyyy");
        SqlStr = "SELECT max(a.dono) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  DeliveryOrder a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.doyear = " + CurYear;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertDeliveryOrder()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO DeliveryOrder";
            SqlStr += "(";
            SqlStr += "OfficeID";
            SqlStr += ",ProjectID";
            SqlStr += ",Jobno";
            SqlStr += ",DoNo";
            SqlStr += ",Importer";
            SqlStr += ",Exporter";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateDeliveryOrder()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update DeliveryOrder Set ";
            SqlStr += "  JobDate = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ", Dodate= " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ", Exporter = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ", MBLNo = " + Sqote + UType.Chk10(MyMain.Fld8) + Sqote;
            SqlStr += ", Importer = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ", NetAmount = " + UType.MyCtoDs(MyMain.Fld10);
            //SqlStr += ", Vessel = " + Sqote + UType.Chk10(MyMain.Fld11) + Sqote;
            SqlStr += ", Received = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ", Voyage = " + Sqote + UType.Chk10(MyMain.Fld13) + Sqote;
            SqlStr += ", Balance = " + UType.MyCtoDs(MyMain.Fld14);
            //SqlStr += ", LastPortCallID = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ", HBLNo = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ", HBLDate= " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ", ClearingAgent = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ", LocalCustom = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ", DeliverdTo = " + Sqote + UType.Chk10(MyMain.Fld20) + Sqote;
            SqlStr += ", DeliverdReqTo = " + Sqote + UType.Chk10(MyMain.Fld21) + Sqote;
            SqlStr += ", OnAccountOf = " + Sqote + UType.Chk10(MyMain.Fld22) + Sqote;
            SqlStr += ", ExpDate = " + UType.MyCtoDs(MyMain.Fld23); //local port
            SqlStr += ", DOName= " + Sqote + UType.Chk10(MyMain.Fld24) + Sqote;
            SqlStr += ", CNICNo = " + Sqote + UType.Chk10(MyMain.Fld25) + Sqote;
            SqlStr += ", PrintBy = " + Sqote + UType.Chk10(MyMain.Fld26) + Sqote;
            SqlStr += ", Remarks = " + Sqote + UType.Chk10(MyMain.Fld27) + Sqote;
            SqlStr += ", EndoresementInstruction = " + UType.MyCtoDs(MyMain.Fld28);
            SqlStr += ", ReturnAt = " + Sqote + UType.Chk10(MyMain.Fld29) + Sqote;
            SqlStr += ", GatePass = " + Sqote + UType.Chk10(MyMain.Fld30) + Sqote;
            SqlStr += ",  NetReceive =" + UType.MyCtoDs(MyMain.Fld31);
            SqlStr += ",  DoNo =" + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += ",  ReferenceNo =" + Sqote + MyMain.Fld32 + Sqote;
            SqlStr += ",  DoYear =" + UType.MyCtoDs(MyMain.Fld33);

            SqlStr += " Where   ";
            SqlStr += "officeID = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and projectid = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += " and JobNo = " + UType.MyCtoDs(MyMain.Fld3);
            //SqlStr += " and DoNo = " + UType.MyCtoDs(MyMain.Fld4);
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion
    public DataSet GetEpassData()
    {
        SqlStr = " select a.DocNo, a.*,b.*,c.*,d.*,e.delivery,d.package as containerpackageqty    ";
        // SqlStr += " ,f.CustomerName as [ShipperName],f.CustomerAddress as [ShipperAddress]   ";
        // SqlStr += " ,i.CustomerName as [consigneeName],i.CustomerAddress as [consigneeAddress]   ";

        SqlStr += "  from manifest a  ";
        SqlStr += "   inner join jobinfo b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.DocNo = b.DocNo ";
        SqlStr += "   left join BLDetail c on b.OfficeId = c.OfficeId and b.projectid = c.projectid and b.JobNo = c.JobNo ";
        SqlStr += "   left join Equipment d on b.OfficeId = d.OfficeId and b.projectid = d.projectid and b.JobNo = d.JobNo ";
        SqlStr += "   left join routing e on b.OfficeId = e.OfficeId and b.projectid = e.projectid and b.JobNo = e.JobNo ";



        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {

            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.DocNo = " + MyMain.Fld3;

        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }


    public DataSet GetEpassDataBLDetail()
    {
        SqlStr = " select c.*    ";

        SqlStr += "  from manifest a  ";
        //SqlStr += "   inner join jobinfo b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.DocNo = b.DocNo ";
        SqlStr += "   left join BLDetail c on a.OfficeId = c.OfficeId and a.projectid = c.projectid and a.docNo = c.docNo ";
        // SqlStr += "   left join Equipment d on b.OfficeId = d.OfficeId and b.projectid = d.projectid and b.JobNo = d.JobNo ";
        // SqlStr += "   left join routing e on b.OfficeId = e.OfficeId and b.projectid = e.projectid and b.JobNo = e.JobNo ";

        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.DocNo = " + MyMain.Fld3;
        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public DataSet GetEpassDataPartial()
    {
        SqlStr = " select a.DocNo, a.*,b.*,c.*,d.*,e.delivery,d.package as containerpackageqty    ";
        // SqlStr += " ,f.CustomerName as [ShipperName],f.CustomerAddress as [ShipperAddress]   ";
        // SqlStr += " ,i.CustomerName as [consigneeName],i.CustomerAddress as [consigneeAddress]   ";

        SqlStr += "  from manifest a  ";
        SqlStr += "   inner join jobinfo b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.DocNo = b.DocNo ";
        SqlStr += "   inner join BLDetail c on b.OfficeId = c.OfficeId and b.projectid = c.projectid and b.JobNo = c.JobNo ";
        SqlStr += "   left join Equipment d on b.OfficeId = d.OfficeId and b.projectid = d.projectid and b.JobNo = d.JobNo ";
        SqlStr += "   left join routing e on b.OfficeId = e.OfficeId and b.projectid = e.projectid and b.JobNo = e.JobNo ";



        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {

            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.DocNo = " + MyMain.Fld3;
            SqlStr += " and c.IndexNo >= " + MyMain.Fld4;
            SqlStr += " and c.IndexNo <= " + MyMain.Fld5;
        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }


    public DataSet GetEpassDataOld()
    {
        SqlStr = " select a.DocNo, a.*,b.*,c.*   ";
        // SqlStr += " ,f.CustomerName as [ShipperName],f.CustomerAddress as [ShipperAddress]   ";
        // SqlStr += " ,i.CustomerName as [consigneeName],i.CustomerAddress as [consigneeAddress]   ";

        SqlStr += "  from manifest a  ";
        SqlStr += "   inner join jobinfo b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.DocNo = b.DocNo ";
        SqlStr += "   left join BLDetail c on a.OfficeId = b.OfficeId and a.projectid = b.projectid and b.JobNo = c.JobNo ";

        //  SqlStr += "   left join rfcustomer f on b.OfficeId = f.OfficeId and b.projectid = f.projectid and b.Shipper = f.CustomerId  ";

        //   SqlStr += "   left join rfcustomer i  on b.OfficeId = i.OfficeId and b.projectid = i.projectid and b.consignee = i.CustomerId  ";

        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {

            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.DocNo = " + MyMain.Fld3;

        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetEpassDataNOC()
    {
        SqlStr = "  select a.DocNo, a.*,b.*,c.*,  c.jobdate as ValidDoDate,c.dodate as DOCurDate,d.vessel,d.voyage,d.ShippingLineID,d.IGMNo,d.VIRNo  from jobinfo a    ";

        SqlStr += "    inner join BLDetail b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.JobNo = b.JobNo   ";

        SqlStr += "   inner join deliveryorder c on a.OfficeId = c.OfficeId and a.projectid = c.projectid and a.jobno = c.JobNo  ";

        SqlStr += "   inner join Manifest d on a.OfficeId = d.OfficeId and a.projectid = d.projectid and a.docNo = d.DocNo ";



        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {

            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.jobNo = " + MyMain.Fld3;

        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public DataSet GetEpassDataArrival()
    {
        SqlStr = " select d.*, a.*, b.*,c.*,'1' as consignee1,'1' as Shipper1 ,j.jobdate as ValidDoDate,e.*  ";
        //SqlStr = " select d.MarkNoContainerNo,d.Packages,Unit,DescriptionofGoodsPackages,IndexNo,portofloading,ManualNetWt,GrossWt,wtunit, a.*, b.*,c.*,'1' as consignee1,'1' as Shipper1   ";
        //SqlStr += " ,f.CustomerName as [ShipperName],f.CustomerAddress as [ShipperAddress]   ";
        //SqlStr += " ,i.CustomerName as [consigneeName],i.CustomerAddress as [consigneeAddress]   ";
        SqlStr += " , j.*";
        SqlStr += "  from jobinfo b  ";
        SqlStr += "   inner join Manifest a on b.OfficeId = a.OfficeId and b.projectid = a.projectid and b.DocNo = a.DocNo   ";
        SqlStr += "   left join jobinfo c on b.OfficeId = c.OfficeId and b.projectid = c.projectid and b.JobNo = c.JobNo   ";
        SqlStr += "    left join bldetail d on b.OfficeId = d.OfficeId and b.projectid = d.projectid and b.JobNo = d.JobNo  ";
        //SqlStr += "   left join rfcustomer f on b.OfficeId = f.OfficeId and b.projectid = f.projectid and b.Shipper = f.CustomerId  ";
        //SqlStr += "   left join rfcustomer i  on b.OfficeId = i.OfficeId and b.projectid = i.projectid and b.consignee = i.CustomerId  ";
        SqlStr += "  left join Equipment e on b.OfficeId = e.OfficeId and b.projectid = e.projectid and b.JobNo = e.JobNo ";
        SqlStr += "    left join DeliveryOrder j  on b.OfficeId = j.OfficeId and b.projectid = j.projectid and b.jobno = j.jobno    ";

        SqlStr += " where  ";
        SqlStr += " b.officeid = " + MyMain.Fld1;
        SqlStr += " and b.ProjectId = " + MyMain.Fld2;
        SqlStr += " and b.JobNo = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public DataSet GetEpassDataJob()
    {
        SqlStr = " select d.*, a.*, b.*,c.*,'1' as consignee1,'1' as Shipper1 ,j.jobdate as ValidDoDate  ";
        //SqlStr = " select d.MarkNoContainerNo,d.Packages,Unit,DescriptionofGoodsPackages,IndexNo,portofloading,ManualNetWt,GrossWt,wtunit, a.*, b.*,c.*,'1' as consignee1,'1' as Shipper1   ";
        //SqlStr += " ,f.CustomerName as [ShipperName],f.CustomerAddress as [ShipperAddress]   ";
        //SqlStr += " ,i.CustomerName as [consigneeName],i.CustomerAddress as [consigneeAddress]   ";
        SqlStr += " , j.*";
        SqlStr += "  from jobinfo b  ";
        SqlStr += "   inner join Manifest a on b.OfficeId = a.OfficeId and b.projectid = a.projectid and b.DocNo = a.DocNo ";
        SqlStr += "   left join jobinfo c on b.OfficeId = c.OfficeId and b.projectid = c.projectid and b.JobNo = c.JobNo ";
        SqlStr += "   inner join bldetail d on b.OfficeId = d.OfficeId and b.projectid = d.projectid and b.DocNo = d.docNo ";
        //SqlStr += "   left join rfcustomer f on b.OfficeId = f.OfficeId and b.projectid = f.projectid and b.Shipper = f.CustomerId  ";
        //SqlStr += "   left join rfcustomer i  on b.OfficeId = i.OfficeId and b.projectid = i.projectid and b.consignee = i.CustomerId  ";

        SqlStr += "   left join DeliveryOrder j  on b.OfficeId = j.OfficeId and b.projectid = j.projectid and b.jobno = j.jobno  ";

        SqlStr += " where  ";
        SqlStr += " b.officeid = " + MyMain.Fld1;
        SqlStr += " and b.ProjectId = " + MyMain.Fld2;
        SqlStr += " and b.JobNo = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }


    public DataSet GetEpassDataJobRec()
    {
        SqlStr = " select a.*, b.*,c.*   ";
        // SqlStr += " ,f.CustomerName as [ShipperName],f.CustomerAddress as [ShipperAddress]   ";
        // SqlStr += " ,i.CustomerName as [consigneeName],i.CustomerAddress as [consigneeAddress]   ";
        // SqlStr += " , j.*,k.*";
        SqlStr += "  from jobinfo b  ";
        SqlStr += "   inner join Manifest a on b.OfficeId = a.OfficeId and b.projectid = a.projectid and b.DocNo = a.DocNo ";
        SqlStr += "   left join bldetail c on b.OfficeId = c.OfficeId and b.projectid = c.projectid and b.JobNo = c.JobNo ";

        // SqlStr += "   left join rfcustomer f on b.OfficeId = f.OfficeId and b.projectid = f.projectid and b.Shipper = f.CustomerId  ";
        // SqlStr += "   left join rfcustomer i  on b.OfficeId = i.OfficeId and b.projectid = i.projectid and b.consignee = i.CustomerId  ";

        //SqlStr += "   left join charges j  on b.OfficeId = j.OfficeId and b.projectid = j.projectid and b.jobno = j.jobno  ";
        // SqlStr += "   left join DeliveryOrder k on b.OfficeId = k.OfficeId and b.projectid = k.projectid and b.jobno = k.jobno  ";
        SqlStr += " where  ";
        SqlStr += " b.officeid = " + MyMain.Fld1;
        SqlStr += " and b.ProjectId = " + MyMain.Fld2;
        SqlStr += " and b.JobNo = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetJobInfoAccounts()
    {
        SqlStr = " select a.*, b.*,c.*   ";

        SqlStr += "  from jobinfo a  ";
        SqlStr += "   left join bldetail b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.JobNo = b.JobNo ";
        SqlStr += "   inner join Manifest c on a.OfficeId = c.OfficeId and a.projectid = c.projectid and a.DocNo = c.DocNo ";

        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.JobNo = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetExpJobInfoAccounts()
    {
        SqlStr = " select a.*, b.*,c.*   ";

        SqlStr += "  from bookinfo a  ";
        SqlStr += "   left join Expbldetail b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.JobNo = b.JobNo ";
        SqlStr += "   inner join ExpManifest c on a.OfficeId = c.OfficeId and a.projectid = c.projectid and a.DocNo = c.DocNo ";

        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.JobNo = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetEpassDataTest1()
    {
        SqlStr = " select a.DocNo, a.* from manifest a  ";
        SqlStr += "   inner join jobinfo b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.DocNo = b.DocNo ";
        SqlStr += "   left join BLDetail c on a.OfficeId = b.OfficeId and a.projectid = b.projectid and b.JobNo = c.JobNo ";
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {

            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.DocNo = " + MyMain.Fld3;

        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetEpassDataTest2()
    {
        SqlStr = " select a.DocNo, a.* from manifest a  ";
        SqlStr += "   inner join jobinfo b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.DocNo = b.DocNo ";
        SqlStr += "   left join BLDetail c on a.OfficeId = b.OfficeId and a.projectid = b.projectid and b.JobNo = c.JobNo ";
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {

            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.DocNo = " + MyMain.Fld3;

        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet MyColor()
    {
        SqlStr = " select  b.* from Colors a  ";
        SqlStr += " inner join RfColors b on a.colorid = b.colorid  ";
        SqlStr += " where  ";
        SqlStr += " a.userid = " + MyMain.Fld1;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public string InsertColor()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfColors";
            SqlStr += "(";
            SqlStr += "UserID";
            SqlStr += ",ColorId";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string UpdateColor()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update RfColors ";
            SqlStr += " Set ";
            SqlStr += " ColorId = " + MyMain.Fld2;
            SqlStr += " ,BackColor = '" + MyMain.Fld3 + "'";
            SqlStr += " ,LeftMenu1 = '" + MyMain.Fld4 + "'";
            SqlStr += " ,LeftMenu2 = '" + MyMain.Fld5 + "'";
            SqlStr += " ,Font1 = '" + MyMain.Fld6 + "'";
            SqlStr += " where userid = " + MyMain.Fld1;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetRfColor()
    {
        SqlStr = "SELECT  ";
        SqlStr += "  a.* from RfColors a ";
        SqlStr += " where a.UserId = " + MyMain.Fld1;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public string InsertLog()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO ErrLog "; // SqlStr = "INSERT INTO Indexes IN 'WEBOC12.mdb' ";
            SqlStr += "(";
            SqlStr += "Log ";
            SqlStr += ",logDate ";

            SqlStr += ") ";
            SqlStr += "VALUES ( ";
            SqlStr += Sqote + MyMain.Fld1 + Sqote;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += " );";
            retVal = NonQryCmdSp(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet Sp_GetGraphInfo()
    {
        SqlStr = "select b.TradeDescription,sum(a.tradetotal) as Tradesum  from mishdr a inner join  rftrade b on a.tradeid = b.tradeid ";
        SqlStr += " group by b.TradeDescription   ";

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet Sp_GetGraphInfo1()
    {
        SqlStr = "select b.ProcessDescription,a.tradetotal from mishdr a inner join  rfProcess b on a.Processid = b.Processid ";
        SqlStr += " where a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.Projectid = " + MyMain.Fld2;
        SqlStr += " and a.tradeid = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetMaxPaymentID()
    {
        SqlStr = "SELECT  ";
        SqlStr += " max(a.paymentid) FROM charges a ";
        SqlStr += " where a.OfficeId = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.paymentidyear = " + MyMain.Fld3;
        // SqlStr += " and a.paymentid = 0 " ;

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public string UpdateChargesPaymentID()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Charges Set ";
            SqlStr += "[PaymentID] = " + UType.MyCtoDs(MyMain.Fld10);

            SqlStr += " Where ";
            SqlStr += " officeID = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and projectid = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += " and jobno = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += " and chargestype = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += " and customer = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += " and paymentid = 0 ";

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateExpChargesPaymentID()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update ExpCharges Set ";
            SqlStr += "[PaymentID] = " + UType.MyCtoDs(MyMain.Fld10);

            SqlStr += " Where ";
            SqlStr += " officeID = " + UType.MyCtoDs(MyMain.Fld1);
            SqlStr += " and projectid = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += " and jobno = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += " and chargestype = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += " and customer = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += " and paymentid = 0 ";

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    #region Export

    #region BookInfo
    public DataSet GetBookInfoGrid()
    {
        SqlStr = "select distinct a.jobno,a.jobdate ";

        SqlStr += " ,f.Vessel,e.hbl as BLNo,e.hbldate as BLDate,b.ActDesc as ShipperName, c.ActDesc as ClientName  ";
        SqlStr += " , d.Country_Name,d.City_Name  from bookInfo a  ";
        SqlStr += " left join actchart b on a.officeid= b.OfficeID and a.ProjectId=b.ProjectId and a.Shipper = b.ActCode  ";
        SqlStr += " left join actchart c on a.officeid= c.OfficeID and a.ProjectId=c.ProjectId and a.Client = c.ActCode  ";
        SqlStr += " left join RfCities d on a.PortDischarge= d.City_ID  ";
        SqlStr += " left join expbldetail e on a.officeid= e.OfficeID and a.ProjectId=e.ProjectId and a.jobno = e.jobno   ";
        SqlStr += " left join ExpManifest f on a.officeid= f.OfficeID and a.ProjectId=f.ProjectId and a.DocNo = f.DocNo  ";
        // SqlStr += " where a.jobno >0 ";
        //  if(MyMain.Fld3.ToString().Length>0)
        //  {
        //      SqlStr += " and e.hbl like '%" + MyMain.Fld3 + "%'";
        //  }
        //
        SqlStr += " where a.OfficeId = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " and a.jobno = " + MyMain.Fld3;
        }
        // if (UType.MyCtoD(MyMain.Fld4) > 0)
        //{
        //    SqlStr += " and b.Mblno = " + MyMain.Fld4;
        // }
        if (UType.MyCtoD(MyMain.Fld5) > 0)
        {
            SqlStr += " and f.docno = " + MyMain.Fld5;
        }
        if (MyMain.Fld6.Length > 0)
        {
            SqlStr += " and f.vessel like " + "'%" + MyMain.Fld6 + "%'";
        }
        if (MyMain.Fld7.Length > 0)
        {
            SqlStr += " and b.actdesc like " + "'%" + MyMain.Fld7 + "%'";
        }
        if (MyMain.Fld8.Length > 0)
        {
            SqlStr += " and c.actdesc like " + "'%" + MyMain.Fld8 + "%'";
        }
        if (UType.MyCtoD(MyMain.Fld9) > 0)
        {
            SqlStr += " and e.hblno = " + MyMain.Fld9;
        }

        if (UType.MyCtoD(MyMain.Fld20) > 0)
        {
            SqlStr += " and d.Country_ID = " + MyMain.Fld20;
            SqlStr += " and d.City_ID = " + MyMain.Fld21;
        }
        SqlStr += " order by a.jobno desc ";
        //
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetBookInfoGridBk()
    {
        SqlStr = "SELECT a.*, 0 as  [JobNature1], 0 as [JobType1] ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  bookinfo a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
                                    // SqlStr += "  inner join manifest b on a.officeid = b.officeid and a.projectid = b.projectid and a.docno = b.docno "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";

            SqlStr += "  a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        }
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " and a.Jobno = " + MyMain.Fld3;
        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.sno = " + MyMain.Fld4;
        }

        SqlStr += " order by a.docno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetBookInfo()
    {
        SqlStr = " select a.*, b.* ";
        SqlStr += "  from Bookinfo a  ";
        SqlStr += "  left join Expbldetail b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.JobNo = b.JobNo ";
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.JobNo = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxBookInfo()
    {
        SqlStr = "SELECT max(a.JobNo) as MaxJobNo ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  BookInfo a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.OfficeId = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.jobcy = " + MyMain.Fld3;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string UpdateBookInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Bookinfo Set ";
            // SqlStr += " txtJob = " + MyMain.EmpId;
            SqlStr += "JobNo = " + Sqote + MyMain.Fld1 + Sqote;
            SqlStr += ",JobDate = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += ",NatureID = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ",IncoTerm = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += ",Type = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",SubType = " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ",CostCenter = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ",Console = " + UType.MyCtoDs(MyMain.Fld8);
            SqlStr += ",JobKind = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",Customer = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",Security = " + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",JobStatus = " + UType.MyCtoDs(MyMain.Fld12);
            SqlStr += ",ShipStatus = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",FreightType = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",Nomination = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",txtFile = " + Sqote + UType.Chk10(MyMain.Fld16) + Sqote;
            SqlStr += ",HBLNo = " + Sqote + UType.Chk10(MyMain.Fld17) + Sqote;
            SqlStr += ",HBLDate = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",MBLNo = " + Sqote + UType.Chk10(MyMain.Fld19) + Sqote;
            SqlStr += ",MBLDate = " + UType.MyCtoDs(MyMain.Fld20);

            SqlStr += ",ParentJobNo = " + Sqote + UType.Chk10(MyMain.Fld21) + Sqote;
            SqlStr += ",TotalContainers = " + UType.MyCtoDs(MyMain.Fld22);
            SqlStr += ",Client = " + UType.Chk10(MyMain.Fld23);
            SqlStr += ",SalesRep = " + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ",Consignee = " + UType.MyCtoDs(MyMain.Fld25);
            SqlStr += ",Sline = " + UType.MyCtoDs(MyMain.Fld26);
            SqlStr += ",Shipper = " + UType.MyCtoDs(MyMain.Fld27);
            SqlStr += ",LocalVendor = " + UType.MyCtoDs(MyMain.Fld28);
            SqlStr += ",Commodity = " + UType.MyCtoDs(MyMain.Fld29);
            SqlStr += ",Overseas = " + UType.MyCtoDs(MyMain.Fld30);
            SqlStr += ",PortLoading = " + UType.MyCtoDs(MyMain.Fld31);
            SqlStr += ",Principal = " + UType.MyCtoDs(MyMain.Fld32);
            SqlStr += ",PortDischarge = " + UType.MyCtoDs(MyMain.Fld33);
            SqlStr += ",Vessel = " + Sqote + UType.Chk10(MyMain.Fld34) + Sqote;
            SqlStr += ",CustomClearance = " + UType.MyCtoDs(MyMain.Fld35);
            SqlStr += ",Transportation = " + UType.MyCtoDs(MyMain.Fld36);
            SqlStr += ",Voyage = " + Sqote + UType.Chk10(MyMain.Fld37) + Sqote;
            SqlStr += ",ED = " + Sqote + UType.Chk10(MyMain.Fld38) + Sqote;
            SqlStr += ",Coloader = " + UType.MyCtoDs(MyMain.Fld39);
            SqlStr += ",TracingNotes = " + Sqote + UType.Chk10(MyMain.Fld40) + Sqote;
            SqlStr += ",CutOffDate = " + UType.MyCtoDs(MyMain.Fld41);
            SqlStr += ",PlannedETD = " + UType.MyCtoDs(MyMain.Fld42);
            SqlStr += ",PlannedETA = " + UType.MyCtoDs(MyMain.Fld43);
            SqlStr += ",ActualETD = " + UType.MyCtoDs(MyMain.Fld44);
            SqlStr += ",ActualETA = " + UType.MyCtoDs(MyMain.Fld45);
            SqlStr += ",Weight = " + Sqote + UType.Chk10(MyMain.Fld46) + Sqote;
            SqlStr += ",Volume = " + Sqote + UType.Chk10(MyMain.Fld47) + Sqote;
            SqlStr += ",Container = " + Sqote + UType.Chk10(MyMain.Fld48) + Sqote;
            SqlStr += ",TEU = " + Sqote + UType.Chk10(MyMain.Fld49) + Sqote;
            SqlStr += ",PCS = " + Sqote + UType.Chk10(MyMain.Fld50) + Sqote;
            SqlStr += ",Quotation = " + UType.MyCtoDs(MyMain.Fld51);  // Sqote + UType.Chk10(MyMain.Fld51) + Sqote;

            SqlStr += ",cbPartFCL = " + UType.MyCtoDs(MyMain.Fld52);
            SqlStr += ",cbMTYMove = " + UType.MyCtoDs(MyMain.Fld53);
            SqlStr += ",cbCustomClearance = " + UType.MyCtoDs(MyMain.Fld54);
            SqlStr += ",cbTransportation = " + UType.MyCtoDs(MyMain.Fld55);
            SqlStr += ",DGId = " + UType.MyCtoDs(MyMain.Fld56);
            SqlStr += ",DocNo = " + UType.MyCtoDs(MyMain.Fld58);
            SqlStr += ",FinalDestination = " + UType.MyCtoDs(MyMain.Fld59);
            SqlStr += ",viaport = " + UType.MyCtoDs(MyMain.Fld60);
            SqlStr += ",Wharf = " + UType.MyCtoDs(MyMain.Fld61);
            SqlStr += ",[UserId]  = " + MyMain.Fld62;
            SqlStr += " Where jobNo = " + MyMain.Fld1;
            SqlStr += " and  jobcy = " + MyMain.Fld63;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertBookInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO BookInfo";
            SqlStr += "(";
            SqlStr += "OfficeId";
            SqlStr += ",ProjectId";
            //SqlStr += ",docno";
            SqlStr += ",JobNo";
            SqlStr += ",natureid";
            SqlStr += ",jobkind";
            SqlStr += ",coloader";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ",Jobcy";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld5;
            //SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += Comma + DateTime.Now.ToString("yyyy");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    #endregion

    #region MyRegion Get Export BLDetail
    public DataSet GetExpBLDetail()
    {
        SqlStr = "SELECT a.*, b.vessel ";
        SqlStr += "  ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpBLDetail a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        SqlStr += "  inner join expmanifest b on a.officeid = b.officeid and a.projectid= b.projectid and a.docno=b.docno  ";
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;

            SqlStr += " and a.jobno = " + MyMain.Fld3;
        }

        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetExpBLDetailSelected()
    {
        SqlStr = "SELECT  [JobNo]      ,[IndexNo]         ,[Shipper]      ,[Consignee]      ,[NotifyParty1] ";

        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpBLDetail   "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " officeid = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            if (UType.MyCtoD(MyMain.Fld3) > 0)
            {
                SqlStr += " and jobno = " + MyMain.Fld3;
            }
        }

        SqlStr += " order by jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetMaxExpBLDetail()
    {
        SqlStr = "SELECT max(a.IndexNo) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpBLDetail a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.docno = " + MyMain.Fld1;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetDupExpBLDetail()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpBLDetail a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {

            SqlStr += " where  ";
            SqlStr += " officeid = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            SqlStr += " and a.jobno = " + MyMain.Fld3;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string InsertExpBLDetail()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO ExpBLDetail";
            SqlStr += "(";
            SqlStr += "jobno";
            SqlStr += ",IndexNo";
            SqlStr += ",officeID";
            SqlStr += ",projectid";
            SqlStr += ",DocNo";

            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateExpBLDetail()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update ExpBLDetail Set ";

            SqlStr += "officeID = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ",projectid = " + UType.MyCtoDs(MyMain.Fld4);
            // SqlStr += ",[IndexNo] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[Shipper] = " + Sqote + UType.Chk10(MyMain.Fld6) + Sqote;
            SqlStr += ", [Consignee] = " + Sqote + UType.Chk10(MyMain.Fld7) + Sqote;
            SqlStr += ", [NotifyParty1] = " + Sqote + UType.Chk10(MyMain.Fld8) + Sqote;
            SqlStr += ",[NotifyParty2] = " + Sqote + UType.Chk10(MyMain.Fld9) + Sqote;
            SqlStr += ",[PortofLoading] = " + Sqote + UType.Chk10(MyMain.Fld10) + Sqote;
            SqlStr += ",[DeliveryAgent] = " + Sqote + UType.Chk10(MyMain.Fld11) + Sqote;
            SqlStr += ",[PortofDischarge] = " + Sqote + UType.Chk10(MyMain.Fld12) + Sqote;
            SqlStr += ",[PortofDelivery] = " + Sqote + UType.Chk10(MyMain.Fld13) + Sqote;
            SqlStr += ",[CBM] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[Packages] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[ManualNetWt] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[GrossWt] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[TareWt]  = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",[ProductQty]  = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",[UnitPerValue]  = " + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",[MarkNoContainerNo]  = " + Sqote + UType.Chk10(MyMain.Fld21) + Sqote;
            SqlStr += ",[NoofPackagesShippingUnits]  = " + Sqote + UType.Chk10(MyMain.Fld22) + Sqote;
            //SqlStr += ",[MarkNoContainerNo]  = " + Sqote + UType.Chk10(MyMain.Fld22) + Sqote;
            SqlStr += ",[MarksGrossWeight]  = " + UType.MyCtoDs(MyMain.Fld23);
            SqlStr += ",[Measurement]  = " + Sqote + UType.Chk10(MyMain.Fld24) + Sqote;
            SqlStr += ",[OnBoardDate]  = " + UType.MyCtoDs(MyMain.Fld25);
            //SqlStr += ",[DateofIssue]  = " + UType.MyCtoDs(MyMain.Fld26);
            SqlStr += ",[CargoType]  = " + UType.MyCtoDs(MyMain.Fld27);
            SqlStr += ",[IndexType]  = " + UType.MyCtoDs(MyMain.Fld28);

            SqlStr += ",[FreightType]  = " + UType.MyCtoDs(MyMain.Fld29);
            SqlStr += ",[Unit]  = " + UType.MyCtoDs(MyMain.Fld30);
            SqlStr += ",[Principal]  = " + UType.MyCtoDs(MyMain.Fld31);
            SqlStr += ",[ProductUnit]  = " + UType.MyCtoDs(MyMain.Fld32);
            SqlStr += ",[SerialType]  = " + UType.MyCtoDs(MyMain.Fld33);
            SqlStr += ",[SerialCategory]  = " + UType.MyCtoDs(MyMain.Fld34);
            SqlStr += ",[CountryofOrigin]  = " + UType.MyCtoDs(MyMain.Fld35);
            SqlStr += ",[SerialException]  = " + UType.MyCtoDs(MyMain.Fld36);
            SqlStr += ",[HSCode]  = " + Sqote + UType.Chk10(MyMain.Fld37) + Sqote;
            SqlStr += ",[Freight]  = " + UType.MyCtoDs(MyMain.Fld38);
            // SqlStr += ",[OriginalBL]  = " + UType.MyCtoDs(MyMain.Fld39);
            SqlStr += ",[AgentStamp]  = " + Sqote + UType.Chk10(MyMain.Fld40) + Sqote;
            SqlStr += ",[HazmatCode]  = " + Sqote + UType.Chk10(MyMain.Fld41) + Sqote;
            SqlStr += ",[DescriptionofGoodsPackages]  = " + Sqote + UType.Chk10(MyMain.Fld42) + Sqote;
            SqlStr += ",[PlaceofIssue]  = " + UType.MyCtoDs(MyMain.Fld43);  //+ Sqote + UType.Chk10(MyMain.Fld43) + Sqote;
            SqlStr += ",[cbManualNetWt]  = " + UType.MyCtoDs(MyMain.Fld44);

            //SqlStr += ",[originalbl]  = " + UType.MyCtoDs(MyMain.Fld47); // + Sqote+ MyMain.Fld47 +Sqote;
            SqlStr += ",[Vessel]  = " + Sqote + MyMain.Fld48 + Sqote;
            ////  SqlStr += ",[ConsgCity]  = " + UType.MyCtoDs(MyMain.Fld48);
            SqlStr += ",HBLDate = " + UType.MyCtoDs(MyMain.Fld60);
            SqlStr += ",TypeOfBL = " + UType.MyCtoDs(MyMain.Fld61);
            SqlStr += ",pre_carriage = " + Sqote + MyMain.Fld66 + Sqote;
            SqlStr += ",PlaceofReceipt = " + Sqote + MyMain.Fld65 + Sqote;
            SqlStr += ",OceanVessel = " + Sqote + MyMain.Fld64 + Sqote;
            SqlStr += ",ForwarderReference = " + Sqote + MyMain.Fld67 + Sqote;
            SqlStr += ",NumberandKindofPackages = " + Sqote + MyMain.Fld62 + Sqote;
            SqlStr += ",HBL = " + Sqote + MyMain.Fld47 + Sqote;
            //SqlStr += ",HBLIssue = " + UType.MyCtoDs(MyMain.Fld69);
            SqlStr += ",ShipDate = " + MyMain.Fld68;

            SqlStr += ",overseas = " + Sqote + MyMain.Fld71 + Sqote;
            SqlStr += ",DateOFIssue = " + UType.MyCtoDs(MyMain.Fld73);
            SqlStr += ",[shiponBoardPlace] = " + UType.MyCtoDs(MyMain.Fld74);
            SqlStr += ",NoOfBL = " + Sqote + MyMain.Fld75 + Sqote;
            SqlStr += " Where JobNo = " + MyMain.Fld1;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion

    #region CROreport
    public DataSet GetCROReportBookInfo()
    {
        SqlStr = "  select a.officeid,a.projectid,a.jobno,a.shipper,a.Consignee,d.NotifyParty1,d.pre_carriage,b.Vessel,b.voyage ";
        SqlStr += " ,a.PortDischarge,a.PortLoading,a.FinalDestination,a.MBLNo,a.HBLNo,d.Shipper as ShipperBL ";//   b.Vessel,b.voyage,b.egmno,a.PortLoading,d.PortofDischarge,d.PlaceofIssue, d.MarkNoContainerNo,d.Packages,d.WtUnit  ";
        SqlStr += " ,a.ForwarderReference,d.MarkNoContainerNo,d.NumberandKindofPackages,d.DescriptionofGoodsPackages "; // a.sline,b.PreAlertDate,b.DocRecShipingLine,b.PreAlertDate,b.GroundDate,a.pcs,a.commodity,a.FinalDestination,a.viaport ";// a.portdischarge,a.consignee,a.CustomClearance,a.pcs,b.locationid,a.commodity  ";
        SqlStr += "  ,b.PreAlertDate,a.jobdate,a.docno,d.IndexType,a.principal,a.overseas,a.ViaPort ,a.sline,b.ShippingCompanyID,b.PreAlertDate,d.noofBL,d.overseas,d.DateofIssue";
        SqlStr += "  ,a.wharf,b.DocRecShipingLine as eta, a.sline,a.CustomClearance,b.grounddate,d.hbl,d.hbldate,d.portofloading,d.shipper as shipper11,d.consignee as consignee11,d.FreightType,d.placeofissue,d.PlaceofReceipt ";
        SqlStr += "  ,a.TracingNotes,b.CutofTime,a.Commodity,a.TracingNotes,a.ActualETD,a.ActualETA from Bookinfo a  ";
        SqlStr += "  inner join expmanifest b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.docNo = b.docNo ";
        //SqlStr += "  left join ExpRouting c on a.OfficeId = c.OfficeId and a.projectid = c.projectid and a.JobNo = c.JobNo ";
        SqlStr += "  left join expBldetail d on a.OfficeId = d.OfficeId and a.projectid = d.projectid and a.JobNo = d.JobNo   ";
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.JobNo = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetCROReportBookInfoBL()
    {
        SqlStr = "  select a.Consignee,a.shipper,a.officeid,a.projectid,a.jobno,d.Consignee,d.Shipper,d.originalbl,d.NotifyParty1,d.NotifyParty2 ";
        SqlStr += " ,b.Vessel,b.voyage,b.egmno,a.PortLoading,d.PortofDischarge,d.PlaceofIssue, d.MarkNoContainerNo,d.Packages,d.WtUnit  ";
        SqlStr += "  ,a.JobDate,a.sline,b.PreAlertDate,b.DocRecShipingLine,b.PreAlertDate,b.GroundDate,a.pcs,a.commodity,a.FinalDestination,a.viaport ";// a.portdischarge,a.consignee,a.CustomClearance,a.pcs,b.locationid,a.commodity  ";
        SqlStr += "  ,a.TracingNotes,a.wharf,a.customclearance,a.Coloader ";
        SqlStr += "   from Bookinfo a  ";
        SqlStr += "  inner join expmanifest b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.docNo = b.docNo ";
        SqlStr += "  left join ExpRouting c on a.OfficeId = c.OfficeId and a.projectid = c.projectid and a.JobNo = c.JobNo ";
        SqlStr += "  inner join expBldetail d on a.OfficeId = d.OfficeId and a.projectid = d.projectid and a.JobNo = d.JobNo   ";
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.JobNo = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetJobBalanceReport()
    {
        SqlStr = "  select a.Consignee,a.shipper,a.officeid,a.projectid,a.jobno,d.Consignee,d.Shipper,d.originalbl,d.NotifyParty1,d.NotifyParty2 ";
        SqlStr += " ,b.Vessel,b.voyage,b.egmno,a.PortLoading,d.PortofDischarge,d.PlaceofIssue, d.MarkNoContainerNo,d.Packages,d.WtUnit  ";
        SqlStr += "  ,a.JobDate,a.sline,b.PreAlertDate,b.DocRecShipingLine,b.PreAlertDate,b.GroundDate,a.pcs,a.commodity,a.FinalDestination,a.viaport ";// a.portdischarge,a.consignee,a.CustomClearance,a.pcs,b.locationid,a.commodity  ";
        SqlStr += "  ,a.TracingNotes,a.wharf,a.customclearance,a.Coloader ";
        SqlStr += "   from Bookinfo a  ";
        SqlStr += "  inner join expmanifest b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.docNo = b.docNo ";
        SqlStr += "  left join ExpRouting c on a.OfficeId = c.OfficeId and a.projectid = c.projectid and a.JobNo = c.JobNo ";
        SqlStr += "  inner join expBldetail d on a.OfficeId = d.OfficeId and a.projectid = d.projectid and a.JobNo = d.JobNo   ";
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.JobNo = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetCROReportEquipment()
    {
        SqlStr = " select distinct a.*, b.* ";
        SqlStr += "  from Bookinfo a  ";
        SqlStr += "  inner join ExpEquipment b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.JobNo = b.JobNo ";
        SqlStr += " where  ";
        SqlStr += " b.officeid = " + MyMain.Fld1;
        SqlStr += " and b.ProjectId = " + MyMain.Fld2;
        SqlStr += " and b.JobNo = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetCROReportCharges()
    {
        SqlStr = " select a.*, b.* ";
        SqlStr += "  from Bookinfo a  ";
        SqlStr += "  inner join ExpCharges b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.JobNo = b.JobNo ";
        SqlStr += " where  ";
        SqlStr += " b.officeid = " + MyMain.Fld1;
        SqlStr += " and b.ProjectId = " + MyMain.Fld2;
        SqlStr += " and b.JobNo = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    #endregion



    #endregion

    #region ExpRouting
    public DataSet GetExpRouting()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpRouting a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " officeid = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            if (UType.MyCtoD(MyMain.Fld3) > 0)
            {
                SqlStr += " and jobno = " + MyMain.Fld3;
            }
        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertExpRouting()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO ExpRouting";
            SqlStr += "(";


            SqlStr += "officeID";
            SqlStr += ",projectid";
            SqlStr += ",jobno";
            SqlStr += ",IndexNo";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string UpdateExpRouting()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update ExpRouting Set ";


            SqlStr += "[RoutingId]=" + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[LocalCustom]=" + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ",[Wharf]=" + UType.MyCtoDs(MyMain.Fld8);
            SqlStr += ",[CargoStatus]=" + UType.MyCtoDs(MyMain.Fld9);
            //SqlStr += ",[PortofDischarge]=" + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += ",[LoadingFlag]=" + UType.MyCtoDs(MyMain.Fld11);
            SqlStr += ",[LoadingTerminal]=" + UType.MyCtoDs(MyMain.Fld12);



            SqlStr += ",[Status]=" + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",[DischargeTerminal]=" + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[CostCenter]=" + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[LoadingDate]=" + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[LoadingTime]=" + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[BookNo]=" + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",[AllocAvailable]=" + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",[GatePass]=" + UType.MyCtoDs(MyMain.Fld20);
            SqlStr += ",[ContainerAvailable]=" + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ",[ArrivalDate]=" + UType.MyCtoDs(MyMain.Fld22);
            SqlStr += ",[GatePassDate]=" + UType.MyCtoDs(MyMain.Fld23);
            SqlStr += ",[SOBDate]=" + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ",[CROIssueDate]=" + UType.MyCtoDs(MyMain.Fld25);
            SqlStr += ",[Letter]=" + Sqote + UType.Chk10(MyMain.Fld26) + Sqote;
            SqlStr += ",[ContainerSplit]=" + UType.MyCtoDs(MyMain.Fld27);
            SqlStr += ",[CRO]=" + UType.MyCtoDs(MyMain.Fld28);
            SqlStr += ",[BLRequired]=" + Sqote + UType.Chk10(MyMain.Fld29) + Sqote;
            SqlStr += ",[EGM]=" + UType.MyCtoDs(MyMain.Fld30);
            SqlStr += ",[ValidityDate]=" + UType.MyCtoDs(MyMain.Fld31);
            SqlStr += ",[ETD]=" + UType.MyCtoDs(MyMain.Fld32);
            SqlStr += ",[ContainerWt]=" + UType.MyCtoDs(MyMain.Fld33);
            SqlStr += ",[CutOffDate]=" + UType.MyCtoDs(MyMain.Fld34);
            SqlStr += ",[CutOffTime]=" + UType.MyCtoDs(MyMain.Fld35);
            SqlStr += ",[CustomClearance]=" + UType.MyCtoDs(MyMain.Fld36);
            SqlStr += ",[Berth]=" + UType.MyCtoDs(MyMain.Fld37);
            SqlStr += ",[ContainerPickup]=" + UType.MyCtoDs(MyMain.Fld38);
            SqlStr += ",[ViaPort]=" + UType.MyCtoDs(MyMain.Fld39);
            SqlStr += ",[ContainerTempretureCent]=" + UType.MyCtoDs(MyMain.Fld40);
            SqlStr += ",[Vent]=" + Sqote + UType.Chk10(MyMain.Fld41) + Sqote;
            SqlStr += ",[IsText1]=" + UType.MyCtoDs(MyMain.Fld42);
            SqlStr += ",[IsLoadingTerms]=" + UType.MyCtoDs(MyMain.Fld43);
            SqlStr += ",[Text1]=" + Sqote + UType.Chk10(MyMain.Fld44) + Sqote;
            SqlStr += ",[LoadingTerms]=" + Sqote + UType.Chk10(MyMain.Fld45) + Sqote;
            SqlStr += ",[ContainerInfo]=" + Sqote + UType.Chk10(MyMain.Fld46) + Sqote;
            SqlStr += ",[SpecialInstruction]=" + UType.MyCtoDs(MyMain.Fld47);
            SqlStr += ",[PortofReceipt]=" + UType.MyCtoDs(MyMain.Fld48);

            SqlStr += " Where ";
            SqlStr += " officeid = " + MyMain.Fld1;
            SqlStr += " and projectid = " + MyMain.Fld2;
            SqlStr += " and jobno = " + MyMain.Fld3;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    #region ExpManifest
    public DataSet GetExpManifest()
    {
        SqlStr = "SELECT  ";
        SqlStr += "  a.* from expmanifest a ";
        SqlStr += " where a.OfficeId = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        if (UType.MyCtoD(MyMain.Fld3) > 0)
        {
            SqlStr += " and a.docno = " + MyMain.Fld3;
        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.docno = " + MyMain.Fld4;
        }
        if (MyMain.Fld5.Length > 0)
        {
            SqlStr += " and a.vessel like " + "'%" + MyMain.Fld5 + "%'";
        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public string InsertExpManifest()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO ExpManifest";
            SqlStr += "(";
            SqlStr += "OfficeID";
            SqlStr += ",ProjectID";
            SqlStr += ",docno";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += UType.MyCtoDs(MyMain.Fld1);
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateExpManifest()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update ExpManifest Set ";
            //SqlStr += "officeID = " + Sqote + MyMain.Fld2 + Sqote;
            SqlStr += "officeID = " + UType.MyCtoDs(MyMain.Fld2);
            SqlStr += ",projectid = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ", [DocYear] = " + UType.MyCtoDs(MyMain.Fld4);
            SqlStr += ", [ManifestSts] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[OperationID] = " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ",[ShippingLineID] = " + UType.MyCtoDs(MyMain.Fld7);
            SqlStr += ",[BookNo] = " + Sqote + UType.Chk10(MyMain.Fld8) + Sqote;
            SqlStr += ",[CustomID] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",[GuaranteeNo] =+ " + Sqote + UType.Chk10(MyMain.Fld10) + Sqote;
            SqlStr += ",[Vessel] = " + Sqote + UType.Chk10(MyMain.Fld11) + Sqote;
            SqlStr += ",[Voyage] = " + Sqote + UType.Chk10(MyMain.Fld12) + Sqote;
            SqlStr += ",[CountryID] = " + UType.MyCtoDs(MyMain.Fld13);
            SqlStr += ",[ArrivalDate] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[LastPortCallID] = " + UType.MyCtoDs(MyMain.Fld15);
            SqlStr += ",[IGMDate] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[BerthName] = " + UType.MyCtoDs(MyMain.Fld17);
            SqlStr += ",[GroundDate] = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",[CFS] = " + UType.MyCtoDs(MyMain.Fld19);
            SqlStr += ",[GroundTime]  = " + Sqote + UType.Chk10(MyMain.Fld20) + Sqote;
            SqlStr += ",[ShippingLicenseID] = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ",[IGMNo] = " + UType.MyCtoDs(MyMain.Fld22);
            SqlStr += ",[LocationID] = " + UType.MyCtoDs(MyMain.Fld23); //local port
            SqlStr += ",[VIRNo] = " + Sqote + UType.Chk10(MyMain.Fld24) + Sqote;
            SqlStr += ",[ShippingCompanyID] = " + UType.MyCtoDs(MyMain.Fld25);
            SqlStr += ",[StartingIndex] = " + UType.MyCtoDs(MyMain.Fld26);
            SqlStr += ",[CaptainName]  = " + Sqote + UType.Chk10(MyMain.Fld27) + Sqote;
            SqlStr += ",[PreAlertDate] = " + UType.MyCtoDs(MyMain.Fld28);
            //SqlStr += ",[BerthName] = " + Sqote + UType.Chk10(MyMain.Fld29) + Sqote;
            SqlStr += ",[AmendmentDate] = " + UType.MyCtoDs(MyMain.Fld30);
            SqlStr += ", [Remarks]= " + Sqote + UType.Chk10(MyMain.Fld31) + Sqote;
            SqlStr += ",[OperatorCode] = " + UType.MyCtoDs(MyMain.Fld32);
            SqlStr += ",[ManifestRefNo] = " + Sqote + UType.Chk10(MyMain.Fld33) + Sqote;
            SqlStr += ", [SameBottomCargo] = " + Sqote + UType.Chk10(MyMain.Fld34) + Sqote;
            SqlStr += ",[DocRecShipingLine] = " + UType.MyCtoDs(MyMain.Fld38);
            SqlStr += ",[ManifestToCustom] = " + UType.MyCtoDs(MyMain.Fld35);
            SqlStr += ",[ShedId] = " + UType.MyCtoDs(MyMain.Fld36);
            SqlStr += ",[IsManifest] = " + UType.MyCtoDs(MyMain.Fld37);
            SqlStr += ",[TerminalID] = " + UType.MyCtoDs(MyMain.Fld40);
            SqlStr += ",[TranID1] = " + Sqote + MyMain.Fld41 + Sqote;

            SqlStr += ",[LoadingCountry] = " + UType.MyCtoDs(MyMain.Fld42);
            SqlStr += ",[UserId]  = " + MyMain.Fld45;

            //SqlStr += "StatusName = " + Sqote + MyMain.EmpName + Sqote;

            SqlStr += " Where docno = " + MyMain.Fld1;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet GetMaxExpManifest()
    {
        SqlStr = "SELECT max(a.docno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpManifest a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    #endregion

    #region Expoert Equipment
    public DataSet GetExpEquipment()
    {
        SqlStr = "SELECT a.*,'1' as netwtunit1 ,'1' as Grosswtunit1,'1' as PackageUnit1,'1' as Principalcode1 ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpEquipment a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.jobno = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;
        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.sno = " + MyMain.Fld4;
        }
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxExpEquipment()
    {
        SqlStr = "SELECT max(a.sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpEquipment a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.jobno = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.projectid = " + MyMain.Fld3;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertExpEquipment()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO ExpEquipment";
            SqlStr += "(";
            SqlStr += "[OfficeId]";
            SqlStr += ",[ProjectID]";
            SqlStr += ",jobno";
            SqlStr += ",sno";
            SqlStr += ",SizenType1";
            SqlStr += ",WTUnit1";
            SqlStr += ",Unit1";
            SqlStr += ",LoadType1";
            SqlStr += ",CargoType1";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";

            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + MyMain.Fld7;
            SqlStr += Comma + MyMain.Fld8;
            SqlStr += Comma + MyMain.Fld9;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateExpEquipment()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update ExpEquipment Set ";

            SqlStr += "ContainerNo = " + Sqote + UType.Chk10(MyMain.Fld3) + Sqote;
            SqlStr += ",Seal = " + Sqote + UType.Chk10(MyMain.Fld4) + Sqote;
            SqlStr += ",[SizenType] = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[RateGroup] = " + UType.MyCtoDs(MyMain.Fld6);

            // SqlStr += ", [NetWt] = " + UType.MyCtoDs(MyMain.Fld7);
            //  SqlStr += ", [GrossWt] = " + UType.MyCtoDs(MyMain.Fld8);

            SqlStr += ",[TareWt] = " + UType.MyCtoDs(MyMain.Fld9);
            SqlStr += ",[WTUnit] = " + UType.MyCtoDs(MyMain.Fld10);
            // SqlStr += ",[CBM] = " + UType.MyCtoDs(MyMain.Fld12);
            //SqlStr += ",[Package] = " + UType.MyCtoDs(MyMain.Fld13); // + Sqote + UType.Chk10(MyMain.Fld13) + Sqote;


            SqlStr += ",[Unit] = " + UType.MyCtoDs(MyMain.Fld14);
            SqlStr += ",[Temp] = " + Sqote + UType.Chk10(MyMain.Fld15) + Sqote;
            SqlStr += ",[LoadType] = " + UType.MyCtoDs(MyMain.Fld16);
            SqlStr += ",[Remarks] = " + Sqote + UType.Chk10(MyMain.Fld17) + Sqote;
            SqlStr += ",[PrincipalCode] = " + UType.MyCtoDs(MyMain.Fld18);
            SqlStr += ",[PrincipalName] = " + Sqote + UType.Chk10(MyMain.Fld19) + Sqote;
            SqlStr += ",[PrincipalEquipInv] = " + Sqote + UType.Chk10(MyMain.Fld20) + Sqote;
            SqlStr += ",[IsManual] = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ",[Detention] = " + UType.MyCtoDs(MyMain.Fld22);
            //SqlStr += ",[PrincipalCode] = " + UType.MyCtoDs(MyMain.Fld23);
            SqlStr += ",[Demurrage] = " + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ",[Plugin] = " + Sqote + UType.Chk10(MyMain.Fld25) + Sqote;
            SqlStr += ",[CargoType] = " + UType.MyCtoDs(MyMain.Fld24);  //+ Sqote + UType.Chk10(MyMain.Fld24) + Sqote;
            SqlStr += ",[PartFCL] = " + UType.MyCtoDs(MyMain.Fld27);
            SqlStr += ",[PartFCL1] = " + Sqote + Sqote;
            SqlStr += ",[SOC] = " + UType.MyCtoDs(MyMain.Fld28);
            SqlStr += ",[SOC1] = " + Sqote + Sqote;
            SqlStr += ",[OOG] = " + UType.MyCtoDs(MyMain.Fld29);
            SqlStr += ",[OOG1] = " + Sqote + Sqote;
            SqlStr += ",[Top] = " + Sqote + UType.Chk10(MyMain.Fld30) + Sqote;
            SqlStr += ",[Left] = " + Sqote + UType.Chk10(MyMain.Fld31) + Sqote;
            SqlStr += ",[Right] = " + Sqote + UType.Chk10(MyMain.Fld32) + Sqote;
            SqlStr += ",[Front] = " + Sqote + UType.Chk10(MyMain.Fld33) + Sqote;

            SqlStr += ",[NetWt] = " + UType.MyCtoDs(MyMain.Fld34);
            SqlStr += ",[NetWtUnit] = " + UType.MyCtoDs(MyMain.Fld35);
            SqlStr += ",[GrossWt] = " + UType.MyCtoDs(MyMain.Fld36);
            SqlStr += ",[Package] = " + UType.MyCtoDs(MyMain.Fld37);
            SqlStr += ",[PackageUnit] = " + UType.MyCtoDs(MyMain.Fld38);
            SqlStr += ",[CBM] = " + UType.MyCtoDs(MyMain.Fld39);





            SqlStr += " Where JobNo = " + MyMain.Fld1;
            if (UType.MyCtoD(MyMain.Fld2) > 0)
            {
                SqlStr += " and sno = " + MyMain.Fld2;
            }

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateExpEquipment1()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update ExpEquipment Set ";


            SqlStr += " [IsManual] = " + UType.MyCtoDs(MyMain.Fld21);
            SqlStr += ",[Detention] = " + UType.MyCtoDs(MyMain.Fld22);
            //SqlStr += ",[PrincipalCode] = " + UType.MyCtoDs(MyMain.Fld23);
            SqlStr += ",[Demurrage] = " + UType.MyCtoDs(MyMain.Fld24);
            SqlStr += ",[Plugin] = " + Sqote + UType.Chk10(MyMain.Fld25) + Sqote;


            SqlStr += " Where JobNo = " + MyMain.Fld1;


            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    #endregion
    #region MyRegion EquipmentSummary
    public DataSet GetExpEquipmentSummary()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpEquipmentSummary a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.jobno = " + MyMain.Fld1;
            SqlStr += " and a.officeid = " + MyMain.Fld2;
            SqlStr += " and a.ProjectId = " + MyMain.Fld3;


        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.sno = " + MyMain.Fld4;
        }
        SqlStr += " order by a.jobno ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public DataSet GetMaxExpEquipmentSummary()
    {
        SqlStr = "SELECT max(a.sno) as MaxSno ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ExpEquipmentSummary a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            SqlStr += " a.jobno = " + MyMain.Fld1;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }

    public string InsertExpEquipmentSummary()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO ExpEquipmentSummary";
            SqlStr += "(";
            SqlStr += "[OfficeId]";
            SqlStr += ",[ProjectID]";
            SqlStr += ",jobno";
            SqlStr += ",sno";
            SqlStr += ",SizenType1";

            SqlStr += ",CargoType1";
            SqlStr += ",AddDate";
            SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateExpEquipmentSummary()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update ExpEquipmentSummary Set ";


            SqlStr += "[SizenType] = " + UType.MyCtoDs(MyMain.Fld3);
            SqlStr += ",[SizenType1] = 1";
            SqlStr += ",[RateGroup] = " + UType.MyCtoDs(MyMain.Fld4);

            SqlStr += ", [Qty] = " + UType.MyCtoDs(MyMain.Fld5);

            SqlStr += ",[PrincipalCode] = " + UType.MyCtoDs(MyMain.Fld6);
            SqlStr += ",[PrincipalName] = " + Sqote + UType.Chk10(MyMain.Fld7) + Sqote;

            SqlStr += ",[CargoType] = " + UType.MyCtoDs(MyMain.Fld8);
            SqlStr += ",[CargoType1] =1 ";

            SqlStr += " Where JobNo = " + MyMain.Fld1;
            SqlStr += " and sno = " + MyMain.Fld2;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }


    #endregion

    public DataSet GetDetention()
    {
        SqlStr = "SELECT a.* ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  RfDetention a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.PrincipalID = " + MyMain.Fld3;
        SqlStr += " and a.sizentype = " + MyMain.Fld5;
        //SqlStr += " and a.endday > " + MyMain.Fld4;
        SqlStr += " order by startday ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    //and a.startday <= 12 and a.endday > 12

    public string FillMisHdr()
    {
        string retVal = string.Empty;

        #region Truncate MisHdr
        try
        {
            SqlStr = "truncate table mishdr ";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion
        try
        {
            SqlStr = "INSERT INTO MISHdr ([OfficeId], [ProjectId], [TradeId],[ProcessId],UserId,AddDate) ";
            SqlStr += "SELECT [OfficeId], [ProjectId], [TradeId],[ProcessId] ";
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += " FROM [RfProcess] ";
            retVal = NonQryCmdSp(SqlStr);
            retVal = "ok";
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string UpdateMisHdr()
    {
        string retVal = string.Empty;

        #region Get Manifest Total
        try
        {
            SqlStr = "Select count(docno) as ManifestTotal from Manifest ";

            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            MyMain.Fld10 = "0";
            DataSet ds = GetDatasetSpNew(SqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MyMain.Fld10 = ds.Tables[0].Rows[0]["ManifestTotal"].ToString();
            }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion

        #region Update MISHDR
        try
        {
            MyMain.Fld3 = "1"; // Import
            MyMain.Fld4 = "1"; // Manifest
            SqlStr = "Update MisHdr Set ";
            SqlStr += " [TradeTotal] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            SqlStr += " and TradeId = " + MyMain.Fld3;
            SqlStr += " and ProcessId = " + MyMain.Fld4;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion

        #region Get Job Creation Total
        try
        {
            SqlStr = "Select count(jobno) as JobTotal from jobinfo ";

            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            MyMain.Fld10 = "0";
            DataSet ds = GetDatasetSpNew(SqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MyMain.Fld10 = ds.Tables[0].Rows[0]["JobTotal"].ToString();
            }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion

        #region Update MISHDR
        try
        {
            MyMain.Fld3 = "1"; // Import
            MyMain.Fld4 = "2"; // Job Info
            SqlStr = "Update MisHdr Set ";
            SqlStr += " [TradeTotal] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            SqlStr += " and TradeId = " + MyMain.Fld3;
            SqlStr += " and ProcessId = " + MyMain.Fld4;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion

        #region Get Equipment Total
        try
        {
            SqlStr = "Select count(jobno) as JobTotal from Equipment ";

            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            MyMain.Fld10 = "0";
            DataSet ds = GetDatasetSpNew(SqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MyMain.Fld10 = ds.Tables[0].Rows[0]["JobTotal"].ToString();
            }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion

        #region Update MISHDR
        try
        {
            MyMain.Fld3 = "1"; // Import
            MyMain.Fld4 = "3"; // Equipment
            SqlStr = "Update MisHdr Set ";
            SqlStr += " [TradeTotal] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            SqlStr += " and TradeId = " + MyMain.Fld3;
            SqlStr += " and ProcessId = " + MyMain.Fld4;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion

        #region Get charges Total
        try
        {
            SqlStr = "Select count(jobno) as JobTotal from charges ";

            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            MyMain.Fld10 = "0";
            DataSet ds = GetDatasetSpNew(SqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MyMain.Fld10 = ds.Tables[0].Rows[0]["JobTotal"].ToString();
            }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion

        #region Update MISHDR
        try
        {
            MyMain.Fld3 = "1"; // Import
            MyMain.Fld4 = "4"; // charges
            SqlStr = "Update MisHdr Set ";
            SqlStr += " [TradeTotal] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            SqlStr += " and TradeId = " + MyMain.Fld3;
            SqlStr += " and ProcessId = " + MyMain.Fld4;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion

        #region Get Export Total
        try
        {
            SqlStr = "Select count(jobno) as JobTotal from bookinfo ";

            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            MyMain.Fld10 = "0";
            DataSet ds = GetDatasetSpNew(SqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MyMain.Fld10 = ds.Tables[0].Rows[0]["JobTotal"].ToString();
            }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion

        #region Update MISHDR
        try
        {
            MyMain.Fld3 = "2"; // Export
            MyMain.Fld4 = "1"; // charges
            SqlStr = "Update MisHdr Set ";
            SqlStr += " [TradeTotal] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            SqlStr += " and TradeId = " + MyMain.Fld3;
            SqlStr += " and ProcessId = " + MyMain.Fld4;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion

        #region Get Export BL Detail Total
        try
        {
            SqlStr = "Select count(jobno) as JobTotal from expbldetail ";

            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            MyMain.Fld10 = "0";
            DataSet ds = GetDatasetSpNew(SqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MyMain.Fld10 = ds.Tables[0].Rows[0]["JobTotal"].ToString();
            }
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion

        #region Update MISHDR
        try
        {
            MyMain.Fld3 = "2"; // Export
            MyMain.Fld4 = "2"; // charges
            SqlStr = "Update MisHdr Set ";
            SqlStr += " [TradeTotal] = " + UType.MyCtoDs(MyMain.Fld10);
            SqlStr += " Where OfficeId = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            SqlStr += " and TradeId = " + MyMain.Fld3;
            SqlStr += " and ProcessId = " + MyMain.Fld4;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        #endregion

        return retVal;
    }
    #endregion
    public string UpdateAttachBank()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "Update charges Set ";
            SqlStr += " vnoBank = " + MyMain.Fld4;
            SqlStr += ", ChequeNo = '" + MyMain.Fld5 + "'";
            SqlStr += ", ChequeDate = " + MyMain.Fld6;

            SqlStr += " Where ";
            SqlStr += " jobno =" + MyMain.Fld1;
            SqlStr += " and chargestype =" + MyMain.Fld2;
            SqlStr += " and sno = " + MyMain.Fld3;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateAttach()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "Update charges Set ";
            SqlStr += " vno = " + MyMain.Fld4;
            SqlStr += ", ChequeNo = '" + MyMain.Fld5 + "'";
            SqlStr += ", ChequeDate = " + MyMain.Fld6;

            SqlStr += " Where ";
            SqlStr += " jobno =" + MyMain.Fld1;
            SqlStr += " and chargestype =" + MyMain.Fld2;
            SqlStr += " and sno = " + MyMain.Fld3;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateAttachExp()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "Update Expcharges Set ";
            SqlStr += " vno = " + MyMain.Fld4;

            SqlStr += " Where ";
            SqlStr += " jobno =" + MyMain.Fld1;
            SqlStr += " and chargestype =" + MyMain.Fld2;
            SqlStr += " and sno = " + MyMain.Fld3;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string DeleteAttach()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "Update charges Set ";
            SqlStr += " vno = 0 ";

            SqlStr += " Where ";
            SqlStr += " jobno =" + MyMain.Fld1;
            SqlStr += " and chargestype =" + MyMain.Fld2;
            SqlStr += " and sno = " + MyMain.Fld3;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string DeleteAttachBank()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "Update charges Set ";
            SqlStr += " vnobank = 0 ";

            SqlStr += " Where ";
            SqlStr += " jobno =" + MyMain.Fld1;
            SqlStr += " and chargestype =" + MyMain.Fld2;
            SqlStr += " and sno = " + MyMain.Fld3;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string DeleteAttachExp()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "Update Expcharges Set ";
            SqlStr += " vno = 0 ";

            SqlStr += " Where ";
            SqlStr += " jobno =" + MyMain.Fld1;
            SqlStr += " and chargestype =" + MyMain.Fld2;
            SqlStr += " and sno = " + MyMain.Fld3;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string DeleteRfDetention()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "Delete RfDetention";
            SqlStr += " Where ";
            SqlStr += " OfficeId =" + MyMain.Fld1;
            SqlStr += " and ProjectId =" + MyMain.Fld2;
            SqlStr += " and PrincipalID =" + MyMain.Fld3;


            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string InsertRfDetention()
    {
        string retVal = string.Empty;
        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _SqlConnection;

            SqlStr = "INSERT INTO RfDetention";
            SqlStr += "(";
            SqlStr += " OfficeId";
            SqlStr += ",ProjectId";
            SqlStr += ",PrincipalID";
            SqlStr += ",StartDay";
            SqlStr += ",EndDay ";
            SqlStr += ",Rate";
            SqlStr += ",SizenType";


            //SqlStr += ",AddDate";
            // SqlStr += ",AddTime";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += Comma + MyMain.Fld2;
            SqlStr += Comma + MyMain.Fld3;
            SqlStr += Comma + MyMain.Fld4;
            SqlStr += Comma + MyMain.Fld5;
            SqlStr += Comma + MyMain.Fld6;
            SqlStr += Comma + MyMain.Fld7;

            //SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            //SqlStr += Comma + DateTime.Now.ToString("HHmm");

            SqlStr += ")";

            sqlCommand.CommandText = SqlStr;
            //_SqlConnection.Open();
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetRfDetention()
    {

        SqlStr = "SELECT a.*,1 as sizentype from rfdetention a ";
        if (UType.MyCtoD(MyMain.Fld1) < 1)
        {
            SqlStr += " Where 0<>0 ";
        }
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " Where a.officeid =" + MyMain.Fld1;
            SqlStr += " and a.projectid = " + MyMain.Fld2;
            SqlStr += " and a.principalid = " + MyMain.Fld3;
            if (UType.MyCtoD(MyMain.Fld4) > 0)
            {
                SqlStr += " and a.sizentype = " + MyMain.Fld4;
            }
            SqlStr += " order by  " ;
            if (UType.MyCtoD(MyMain.Fld4) > 0)
            {
                SqlStr += "  a.sizentype , ";
            }
            SqlStr += "  a.startday  ";

        }

        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public DataSet GetExpenseRate()
    {
        SqlStr = "Select a.* from actchart a ";
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.actcode = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string DeleteDatainTable()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "DELETE  ";
            SqlStr += " FROM ActTran ";
            SqlStr += " WHERE ";
            SqlStr += " OfficeId = " + MyMain.OfficeId;
            SqlStr += " and ProjectId = " + MyMain.ProjectId;
            SqlStr += " and CFY = " + MyMain.Fld1;
            SqlStr += " and vno =" + MyMain.Fld2;
            SqlStr += " and actcode =" + MyMain.Fld3;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;

        //old
        //SqlStr = "DELETE  ";
        //SqlStr += " FROM ActTran ";
        //SqlStr += " WHERE ";
        //SqlStr += " OfficeId = " + MyMain.OfficeId;
        //SqlStr += " and ProjectId = " + MyMain.ProjectId;
        //SqlStr += " and CFY = " + MyMain.Fyear;
        ////SqlStr += " and Vtypeid = '" + MyMain.Vtype + "'";
        //SqlStr += " and VNo = " + MyMain.VNo;

        //string Res = NonQryCmdSp(SqlStr);
        //return Res;
    }

    public DataSet GetOpeningBalance()
    {
        SqlStr = " SELECT a.actcode, SUM(a.amount) AS 'OpeningBal', a.[Status] as OpeningSts FROM ActTran AS a  ";
        SqlStr += " INNER  JOIN RefTbl AS b ON a.OfficeId = b.officeid AND a.ProjectId = b.projectid ";
        SqlStr += " WHERE ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.ActCode = " + MyMain.Fld3;
        SqlStr += " and a.trandate >= b.SDFyear and a.trandate <=" + MyMain.Fld4;
        SqlStr += "  GROUP BY a.ActCode, a.[Status] order by SUM(a.amount)  desc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }

    public DataSet GetActivity()
    {
        SqlStr = " SELECT a.actcode, SUM(a.amount) AS 'ActivityBal', a.[Status] as ActivitySts FROM ActTran AS a  ";
        SqlStr += " INNER  JOIN RefTbl AS b ON a.OfficeId = b.officeid AND a.ProjectId = b.projectid ";
        SqlStr += " WHERE ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.ActCode = " + MyMain.Fld3;
        SqlStr += " and a.trandate >= " + MyMain.Fld4;
        SqlStr += " and a.trandate <= " + MyMain.Fld5;

        SqlStr += "  GROUP BY a.ActCode, a.[Status] order by SUM(a.amount)  desc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetJobContainer()
    {
        SqlStr = " SELECT a.*,b.*,c.* from manifest a  ";
        SqlStr += "  inner join JobInfo b on a.officeid = b.officeid and a.ProjectId = b.ProjectId and a.docno = b.docno  ";
        SqlStr += " inner join Equipment c on b.officeid = c.officeid and b.ProjectId = c.ProjectId and b.jobno = c.jobno  ";
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and a.docno = " + MyMain.Fld3;
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public DataSet GetReceiptList()
    {
        SqlStr = " select a.DocNo, a.*,b.*,c.*,d.*,e.delivery,f.*   ";
        // SqlStr += " ,f.CustomerName as [ShipperName],f.CustomerAddress as [ShipperAddress]   ";
        // SqlStr += " ,i.CustomerName as [consigneeName],i.CustomerAddress as [consigneeAddress]   ";

        SqlStr += "  from manifest a  ";
        SqlStr += "   inner join jobinfo b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.DocNo = b.DocNo ";
        SqlStr += "   left join BLDetail c on b.OfficeId = c.OfficeId and b.projectid = c.projectid and b.JobNo = c.JobNo ";
        SqlStr += "   left join Equipment d on b.OfficeId = d.OfficeId and b.projectid = d.projectid and b.JobNo = d.JobNo ";
        SqlStr += "   left join routing e on b.OfficeId = e.OfficeId and b.projectid = e.projectid and b.JobNo = e.JobNo ";
        SqlStr += " left join charges f on b.officeid = f.officeid and b.projectid= f.projectid and b.jobno= f.jobno ";


        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {

            SqlStr += " where  ";
            SqlStr += " b.officeid = " + MyMain.Fld1;
            SqlStr += " and b.ProjectId = " + MyMain.Fld2;
            SqlStr += " and b.JobDate >= " + MyMain.Fld3;
            SqlStr += " and b.JobDate <= " + MyMain.Fld4;
        }

        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public string UpdateIndexNo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Bldetail ";
            SqlStr += " Set ";
            SqlStr += " indexno = " + MyMain.Fld5;
            SqlStr += " where officeid = " + MyMain.Fld1;
            SqlStr += " and ProjectId = " + MyMain.Fld2;
            SqlStr += " and docno = " + MyMain.Fld3;
            SqlStr += " and indexno = " + MyMain.Fld4;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetAccountSearch()
    {
        SqlStr = "SELECT a.actcode,a.actdesc as Description";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  ActChart a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        SqlStr += " where  ";
        SqlStr += " a.officeid = " + MyMain.Fld1;
        SqlStr += " and a.ProjectId = " + MyMain.Fld2;
        SqlStr += " and len(a.ActCode) >6   ";
        if (MyMain.Fld3.Length > 0)
        {
            SqlStr += " and a.actdesc like '%" + MyMain.Fld3 + "%'";
        }
        if (UType.MyCtoD(MyMain.Fld4) > 0)
        {
            SqlStr += " and a.actcode =" + MyMain.Fld4;
        }
        SqlStr += " and a.actcode >= 1010010000";


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public DataSet GetInventorydData()
    {
        SqlStr = "select a.DocNo,a.DocYear,b.jobno,b.JobDate,c.ContainerNo,b.PortLoading";
        SqlStr += ",b.PortDischarge,a.ArrivalDate,c.SizeNtype as Size,d.NoOfDays as 'No_of_Days',d.freedays,d.Remarks ";
        SqlStr += "  from manifest a   ";
        SqlStr += " inner join jobinfo b on a.OfficeId = b.OfficeId and a.projectid = b.projectid and a.DocNo = b.DocNo     ";
        SqlStr += " left join Equipment c on b.OfficeId = c.OfficeId and b.projectid = c.projectid and b.JobNo = c.JobNo ";
        SqlStr += " left join inventory d on c.ContainerNo=d.containerno ";

        // SqlStr += " order by b.docno desc ";
        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;

    }
    public string InserInventory()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO Inventory";
            SqlStr += "(";
            SqlStr += "docno";
            SqlStr += ",Docyear";
            SqlStr += ",ContainerNo";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyMain.Fld1;
            SqlStr += MyMain.Fld2;
            SqlStr += Comma + "'" + MyMain.Fld3 + "'";
            SqlStr += ")";
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string UpdateInventory()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update Inventory Set ";


            SqlStr += "[OfficeId]  = " + Sqote + UType.Chk10(MyMain.Fld5) + Sqote;
            SqlStr += ",[Charges] = " + UType.MyCtoDs(MyMain.Fld6);


            SqlStr += ",[UserId]  = " + MyMain.Fld45;

            SqlStr += " Where docno = " + MyMain.Fld1;
            SqlStr += " and Docyear = " + MyMain.Fld2;
            SqlStr += " and ContainerNo = " + Sqote + UType.Chk10(MyMain.Fld3) + Sqote;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetMaxBillInvoice()
    {
        DataSet ds = null;
        try
        {
            SqlStr = "SELECT max(BillInvoice)  as MaxCustomer ,jobcy ";
            SqlStr += " FROM "; // officechart oc   ";
            SqlStr += "  Charges a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
            if (UType.MyCtoD(MyMain.Fld1) > 0)
            {
                SqlStr += " where  ";
                SqlStr += " a.officeid = " + MyMain.Fld1;
                SqlStr += " and a.ProjectId = " + MyMain.Fld2;
                SqlStr += " and a.Jobcy = " + MyMain.Fld3;
                SqlStr += " and a.Customer = " + MyMain.Fld4;
                SqlStr += " and a.chargestype = " + MyMain.Fld5;
            }
            SqlStr += "  group by jobcy  ";
            ds = GetDatasetSpNew(SqlStr);
        }
        catch (Exception ex)
        {

            string chk1 = ex.ToString();
        }
        return ds;
    }

    public string DeleteUserMenu()
    {
        string retVal = string.Empty;
        try
        {

            SqlStr = "delete from  RfUserMenu ";


            SqlStr += " Where ";
            SqlStr += " officeid =" + MyMain.Fld1;
            SqlStr += " and projectid = " + MyMain.Fld2;
            SqlStr += " and userid = " + MyMain.Fld3;
            SqlStr += " and menuid = " + MyMain.Fld4;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetMaxInvoiceCon()
    {
        SqlStr = "SELECT max(a.invoiceno) as MaxInvoiceNo ";
        SqlStr += " FROM "; // officechart oc   ";
        SqlStr += "  Equipment a  "; // inner join actchart ac on oc.actcode = ac.actcode "; 
        if (UType.MyCtoD(MyMain.Fld1) > 0)
        {
            SqlStr += " where  ";
            //SqlStr += " a.jobno = " + MyMain.Fld1;
            SqlStr += " a.officeid = " + MyMain.Fld1;
            SqlStr += " and a.projectid = " + MyMain.Fld2;
            SqlStr += " and a.jobcy = " + MyMain.Fld3;
            SqlStr += " and a.principalcode = " + MyMain.Fld4;
        }


        DataSet ds = GetDatasetSpNew(SqlStr);
        return ds;
    }
    public string UpdateMaxInvoice()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "Update equipment Set ";


            SqlStr += "[invoiceno]  = " + UType.MyCtoDs(MyMain.Fld5);
            SqlStr += ",[invoicedate] = " + DateTime.Now.ToString("yyyyMMdd");

            SqlStr += " where  ";
            //SqlStr += " a.jobno = " + MyMain.Fld1;
            SqlStr += " officeid = " + MyMain.Fld1;
            SqlStr += " and projectid = " + MyMain.Fld2;
            SqlStr += " and jobNo = " + MyMain.Fld3;
            SqlStr += " and principalcode = " + MyMain.Fld4;
            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string DeleteRfDeten()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "DELETE  ";
            SqlStr += " FROM RfDetention ";
            SqlStr += " WHERE ";

            SqlStr += " officeid =" + MyMain.Fld1;
            SqlStr += " and projectid =" + MyMain.Fld2;
            SqlStr += " and principal =" + MyMain.Fld3;
            SqlStr += " and startday =" + MyMain.Fld4;
            SqlStr += " and sizentype =" + MyMain.Fld5;

            retVal = NonQryCmdSp(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;

        //old
        //SqlStr = "DELETE  ";
        //SqlStr += " FROM ActTran ";
        //SqlStr += " WHERE ";
        //SqlStr += " OfficeId = " + MyMain.OfficeId;
        //SqlStr += " and ProjectId = " + MyMain.ProjectId;
        //SqlStr += " and CFY = " + MyMain.Fyear;
        ////SqlStr += " and Vtypeid = '" + MyMain.Vtype + "'";
        //SqlStr += " and VNo = " + MyMain.VNo;

        //string Res = NonQryCmdSp(SqlStr);
        //return Res;
    }
}