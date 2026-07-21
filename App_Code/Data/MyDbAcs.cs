using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.OleDb;

/// <summary>
/// Summary description for MyDbAcs
/// </summary>
public class MyDbAcs
{
    #region Data Members

    private OleDbConnection _OleDbCon;
    private MyMainAcs _MyMain;
    string SqlStr = string.Empty;
    string Sqote = "'";
    string Comma = ",";

    #endregion


    #region Constructors
    public MyDbAcs(MyMainAcs oMyMain)
    {
        _MyMain = oMyMain;
    }

    public MyDbAcs(OleDbConnection OleCon, MyMainAcs oMyMain)
    {
        _OleDbCon = OleCon;
        _MyMain = oMyMain;
    }

    #endregion

    public bool DisposeOleDbConnection()
    {
        _OleDbCon.Close();
        _OleDbCon.Dispose();
        return true;
    }

    public DataSet GetDataset(string Str1)
    {
        DataSet ds = new DataSet();
        OleDbCommand oCmd = new OleDbCommand();
        OleDbDataAdapter oAdapter = new OleDbDataAdapter();
        try
        {
            oCmd.Connection = _OleDbCon;  //Connection.OleDbConnection;
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
            bool Res = DisposeOleDbConnection();
        }
        return ds;
    }
    public DataSet GetDataset1(string Str1)
    {
        DataSet ds = new DataSet();
        OleDbCommand oCmd = new OleDbCommand();
        OleDbDataAdapter oAdapter = new OleDbDataAdapter();
        try
        {
            oCmd.Connection = _OleDbCon;  //Connection.OleDbConnection;
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
            //bool Res = DisposeOleDbConnection();
        }
        return ds;
    }
    public string SetDataSet(string SqlStr)
    {
        string retVal = string.Empty;
        try
        {
            OleDbCommand oCmd = new OleDbCommand();
            oCmd.Connection = _OleDbCon; //Connection.OleDbConnection;
            oCmd.CommandText = SqlStr;
            if (oCmd.ExecuteNonQuery() > 0)
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

    public DataSet GetLogin(string pUserId, string pPassword)
    {
        SqlStr = "SELECT ui.*,ur.RoleId FROM UserInfo ui ";
        SqlStr += " INNER JOIN UserRole ur ON ui.UserId=ur.UserId  ";
        SqlStr += " WHERE ";
        SqlStr += " LoginId = " + Sqote + pUserId + Sqote;
        SqlStr += " AND	LoginPassword = " + Sqote + pPassword + Sqote;
        DataSet ds = GetDataset(SqlStr);
        return ds;

    }
    public string GetOffice()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfOffice ";
        //DataSet ds = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.Text, SqlStr);
        return SqlStr;
    }
    public DataSet GetCount()
    {
        //SqlStr = "SELECT count(SNo) as MaxSno from LoginHistory ";
        SqlStr = "SELECT * from LoginHistory where loginsts like '*cl*' ";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public string InsertLoginHistory()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO LoginHistory";
            SqlStr += "(";
            SqlStr += "UserId";
            SqlStr += ",LoginSts";
            SqlStr += ",LogDate ";
            SqlStr += ",LogTime ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += _MyMain.Fld1;
            SqlStr += Comma + Sqote + _MyMain.Fld2 + Sqote;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmmssff");
            SqlStr += ")";

            retVal = SetDataSet(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet Sp_GetMenu(string pUserId, string pMenuLevel)
    {
        DataSet ds1 = null;
        try
        {
            SqlStr = "SELECT rrm.MenuSno,rm.MenuText,rm.MenuPath,rrm.DepartId";
            SqlStr += " FROM ((UserRole ur";
            SqlStr += " INNER JOIN RfRoleMenu rrm ON ur.DepartId= rrm.DepartId and ur.ROLEID= rrm.RoleId )";
            SqlStr += " INNER JOIN RfMenu rm ON rrm.MenuId= rm.MenuId)";
            SqlStr += " WHERE ur.UserId= " + pUserId + " AND rm.MenuLevel= " + pMenuLevel;
            SqlStr += " ORDER BY rrm.MenuSno";
            ds1 = GetDataset(SqlStr);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetMenu_Dept(string pUserId, string pMenuLevel, string pDepart)
    {
        DataSet ds1 = null;
        try
        {
            SqlStr = " SELECT rrm.MenuSno,rm.MenuText,rm.MenuPath,rrm.DepartId";
            SqlStr += " FROM ((UserRole ur";
            SqlStr += " INNER JOIN RfRoleMenu rrm ON ur.roleid= rrm.RoleId)";
            SqlStr += " INNER JOIN RfMenu rm ON rrm.MenuId= rm.MenuId)";
            SqlStr += " WHERE ur.UserId= " + pUserId + " AND rm.MenuLevel= " + pMenuLevel;
            SqlStr += " AND rrm.DepartId = " + pDepart;
            SqlStr += " ORDER BY rrm.MenuSno";
            ds1 = GetDataset(SqlStr);

        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;

    }
    public DataSet GeRefTbl()
    {
        SqlStr = "SELECT ro.OfficeId,ro.OfficeDescription";
        SqlStr += ",rp.ProjectId,rp.ProjectDescription";
        SqlStr += " FROM ((RefTbl rt ";
        SqlStr += "INNER JOIN RfOffice ro ON rt.OfficeId=ro.OfficeId )";
        SqlStr += "INNER JOIN RfProject rp ON rt.ProjectId=rp.ProjectId )";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public DataSet GetFillComboActCode()
    {
        SqlStr = "SELECT Actcode,ActDesc ";
        SqlStr += " FROM ActChart order by actcode ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public DataSet GetFillComboOffice()
    {
        SqlStr = "SELECT * FROM RfOffice order by officeid";
        DataSet ds = GetDataset(SqlStr);
        return ds;

    }
    public DataSet GetFillComboProject()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfProject ";
        DataSet ds = GetDataset(SqlStr);
        return ds;

    }
    public DataSet GetRefData()
    {
        SqlStr = "SELECT ro.OfficeDescription,rp.ProjectDescription ";
        SqlStr += " ,rt.* FROM ((RefTbl rt ";
        SqlStr += " INNER JOIN RfOffice ro ON rt.OfficeId=ro.OfficeId)";
        SqlStr += " INNER JOIN RfProject rp ON rt.ProjectId=rp.ProjectId)";
        DataSet ds = GetDataset(SqlStr);
        return ds;

    }
    public DataSet SelectRef()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RefTbl ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public string InsertRef()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RefTbl";
            SqlStr += "(SDFyear";
            SqlStr += ",EDFyear ";
            SqlStr += ",LSIStat";
            SqlStr += ",RSIStat";
            SqlStr += ",LSBSheet";
            SqlStr += ",RSBSheet";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += _MyMain.StartFinancialYear;
            SqlStr += Comma + _MyMain.EndFinancialYear;
            SqlStr += Comma + _MyMain.LeftSideIncomeStatement;
            SqlStr += Comma + _MyMain.RightSideIncomeStatement;
            SqlStr += Comma + _MyMain.LeftSideBalanceSheet;
            SqlStr += Comma + _MyMain.RightSideBalanceSheet;
            SqlStr += ")";

            retVal = SetDataSet(SqlStr);

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
            SqlStr += " SDFyear = " + _MyMain.StartFinancialYear;
            SqlStr += ",EDFyear = " + _MyMain.EndFinancialYear;
            SqlStr += ",LSIStat = " + _MyMain.LeftSideIncomeStatement;
            SqlStr += ",RSIStat =" + _MyMain.RightSideIncomeStatement;
            SqlStr += ",LSBSheet =" + _MyMain.LeftSideBalanceSheet;
            SqlStr += ",RSBSheet = " + _MyMain.RightSideBalanceSheet;
            SqlStr += ",CFY = " + _MyMain.Fld1;
            SqlStr += ",DebtorActCode = " + _MyMain.Fld2;
            SqlStr += ",CreditorActCode = " + _MyMain.Fld3;
            SqlStr += ",PurActCode = " + _MyMain.Fld4;
            SqlStr += ",SalActCode = " + _MyMain.Fld5;
            retVal = SetDataSet(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet Sp_GetFillComboActLevel()
    {
        SqlStr = "SELECT * FROM ActRflevel ";
        SqlStr += " ORDER BY actleveldesc";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public DataSet Sp_SelectChart()
    {
        SqlStr = " SELECT ac.* FROM RefTbl rt ";
        SqlStr += " INNER JOIN ActChart ac ON rt.OfficeId=ac.OfficeId AND rt.ProjectId=ac.ProjectId";
        SqlStr += " WHERE ac.ActCode= " + _MyMain.ActCode;
        DataSet ds = GetDataset(SqlStr);
        return ds;

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
            SqlStr += ",Balance";
            SqlStr += ",BalanceSts";
            SqlStr += ",ActStatus";
            SqlStr += ",ActLevel ";
            SqlStr += ",AddUser ";
            SqlStr += ",AddDate ";
            SqlStr += ",AddTime ";
            SqlStr += ",ActCode1";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += _MyMain.OfficeId;
            SqlStr += Comma + _MyMain.ProjectId;
            SqlStr += Comma + _MyMain.ActCode;
            SqlStr += Comma + Sqote + _MyMain.ActDescription + Sqote;
            SqlStr += Comma + _MyMain.Balance;
            SqlStr += Comma + Sqote + _MyMain.BalanceSts + Sqote;
            SqlStr += Comma + Sqote + _MyMain.ActStatus + Sqote;
            SqlStr += Comma + _MyMain.ActLevel;
            SqlStr += Comma + Sqote + _MyMain.LoginId + Sqote;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmmssff");
            SqlStr += Comma + _MyMain.ActCode.PadRight(12, '0');
            SqlStr += ")";

            retVal = SetDataSet(SqlStr);
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
        try
        {
            SqlStr = "Update ActChart Set ";
            SqlStr += "OfficeId = " + _MyMain.OfficeId;
            SqlStr += ",ProjectId = " + _MyMain.ProjectId;
            SqlStr += ",ActCode = " + Sqote + _MyMain.ActCode + Sqote;
            SqlStr += ",ActCode1 = " + Sqote + _MyMain.ActCode.PadRight(12, '0') + Sqote;
            SqlStr += ",ActDesc = " + Sqote + _MyMain.ActDescription + Sqote;
            SqlStr += ",Balance = " + _MyMain.Balance;
            SqlStr += ",BalanceSts =" + Sqote + _MyMain.BalanceSts + Sqote;
            SqlStr += ",ActStatus =" + Sqote + _MyMain.ActStatus + Sqote;

            SqlStr += ",ActLevel =" + _MyMain.ActLevel;
            SqlStr += ",AddUser =" + Sqote + _MyMain.LoginId + Sqote;
            SqlStr += ",AddDate =" + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += ",AddTime =" + DateTime.Now.ToString("HHmmssff");
            SqlStr += " Where actcode = " + _MyMain.ActCode;

            retVal = SetDataSet(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet Sp_GetActCode()
    {
        SqlStr = " SELECT * FROM ActChart order by ActCode1 ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public DataSet Sp_PrintChartOfAccount()
    {
        DataSet ds = null;
        try
        {
            SqlStr = " SELECT ac.*,ro.OfficeDescription,rp.ProjectDescription ";
            SqlStr += " FROM (((RefTbl rt ";
            SqlStr += " INNER JOIN RfOffice ro ON rt.OfficeId=ro.OfficeId )";
            SqlStr += " INNER JOIN RfProject rp ON rt.ProjectId=rp.ProjectId )";
            SqlStr += " INNER JOIN ActChart ac ON rt.OfficeId=ac.OfficeId AND rt.ProjectId=ac.ProjectId )";
            SqlStr += " ORDER BY STR(ac.ActCode) ";
            ds = GetDataset(SqlStr);
        }
        catch (Exception ex)
        {

        }
        return ds;
    }
    public DataSet BalanceSheetSp(string pStartDate, string pEndDate)
    {
        DataSet ds = null;
        try
        {
            SqlStr = "SELECT ";
            SqlStr += " ro.[OfficeDescription],rp.[ProjectDescription] ";
            SqlStr += " ,atr.* ";
            SqlStr += " ,ac.[ActDesc] as actdescription,arv.[VType],arv.[VtypeDescription] ";
            SqlStr += " ,ac.[ActDesc] 	";
            SqlStr += " FROM ((((( [RefTbl] rt 	";
            SqlStr += " INNER JOIN [RfOffice] ro ON rt.[OfficeId]=ro.[OfficeId])";
            SqlStr += " INNER JOIN [RfProject] rp ON rt.[ProjectId]=rp.[ProjectId])";
            SqlStr += " INNER JOIN [ActTran] atr ON rt.[OfficeId]=atr.[OfficeId] AND rt.[ProjectId]=atr.[ProjectId] AND rt.[CFY]=atr.[CFY])";
            SqlStr += " INNER JOIN [ActChart] ac ON rt.[OfficeId]=ac.[OfficeId] AND rt.[ProjectId]=ac.[ProjectId] and atr.[ActCode]=ac.[ActCode])";
            SqlStr += " INNER JOIN [ActRfVtype] arv ON atr.[VTypeId]=arv.[VtypeId]) ";
            SqlStr += " WHERE	atr.[TranDate] >= " + pStartDate;
            SqlStr += " AND atr.[TranDate] <= " + pEndDate;
            SqlStr += " AND atr.[Amount] > 0";
            SqlStr += " ORDER BY atr.[TranDate]";
            ds = GetDataset1(SqlStr);

            SqlStr = " SELECT * FROM [RefTbl] ";
            DataSet ds2 = GetDataset1(SqlStr);
            string LSBS = string.Empty;
            string RSBS = string.Empty;
            int var1 = 0;
            int var2 = 0;
            if (ds2 != null)
            {
                LSBS = ds2.Tables[0].Rows[0]["LSBSheet"].ToString();
                RSBS = ds2.Tables[0].Rows[0]["RSBSheet"].ToString();
                var1 = LSBS.Length;
                var2 = RSBS.Length;
            }
            SqlStr = " SELECT * FROM [actchart]";
            SqlStr += " where mid(trim(str([ActCode])),1," + var1 + ") = " + LSBS + " OR mid(trim(str([ActCode])),1," + var2 + ")=" + RSBS;
            SqlStr += " ORDER BY ActCode1";
            DataSet ds1 = GetDataset(SqlStr);
            ds = UType.addTableinDataSet(ds, ds1.Tables[0], "Table1");
        }
        catch (Exception ex)
        {

        }
        return ds;


    }
    public DataSet IncomeStatementSp(string pStartDate, string pEndDate)
    {
        DataSet ds = null;
        try
        {
            SqlStr = "SELECT ";
            SqlStr += " ro.[OfficeDescription],rp.[ProjectDescription] ";
            SqlStr += " ,atr.* ";
            SqlStr += " ,ac.[ActDesc] as actdescription,arv.[VType],arv.[VtypeDescription] ";
            SqlStr += " ,ac.[ActDesc] 	";
            SqlStr += " FROM ((((( [RefTbl] rt 	";
            SqlStr += " INNER JOIN [RfOffice] ro ON rt.[OfficeId]=ro.[OfficeId])";
            SqlStr += " INNER JOIN [RfProject] rp ON rt.[ProjectId]=rp.[ProjectId])";
            SqlStr += " INNER JOIN [ActTran] atr ON rt.[OfficeId]=atr.[OfficeId] AND rt.[ProjectId]=atr.[ProjectId] AND rt.[CFY]=atr.[CFY])";
            SqlStr += " INNER JOIN [ActChart] ac ON rt.[OfficeId]=ac.[OfficeId] AND rt.[ProjectId]=ac.[ProjectId] and atr.[ActCode]=ac.[ActCode])";
            SqlStr += " INNER JOIN [ActRfVtype] arv ON atr.[VTypeId]=arv.[VtypeId]) ";
            SqlStr += " WHERE	atr.[TranDate] >= " + pStartDate;
            SqlStr += " AND atr.[TranDate] <= " + pEndDate;
            SqlStr += " AND atr.[Amount] > 0";
            SqlStr += " ORDER BY atr.[TranDate]";
            ds = GetDataset1(SqlStr);

            SqlStr = " SELECT * FROM [RefTbl] ";
            DataSet ds2 = GetDataset1(SqlStr);
            string LSIS = string.Empty;
            string RSIS = string.Empty;
            int var1 = 0;
            int var2 = 0;
            if (ds2 != null)
            {
                LSIS = ds2.Tables[0].Rows[0]["LSIStat"].ToString();
                RSIS = ds2.Tables[0].Rows[0]["RSIStat"].ToString();
                var1 = LSIS.Length;
                var2 = RSIS.Length;
            }
            SqlStr = " SELECT * FROM [actchart]";
            SqlStr += " where mid(trim(str([ActCode])),1," + var1 + ") = " + LSIS + " OR mid(trim(str([ActCode])),1," + var2 + ")=" + RSIS;
            DataSet ds1 = GetDataset(SqlStr);
            ds = UType.addTableinDataSet(ds, ds1.Tables[0], "Table1");
        }
        catch (Exception ex)
        {

        }
        return ds;
    }
    public DataSet GetFillComboVtype()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM ActRfVtype ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
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
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += _MyMain.OfficeId;
            SqlStr += Comma + _MyMain.ProjectId;
            SqlStr += Comma + _MyMain.ActCode;
            SqlStr += Comma + Sqote + _MyMain.Vtype + Sqote;
            SqlStr += Comma + _MyMain.VNo;
            SqlStr += Comma + _MyMain.TransectionDate;
            SqlStr += Comma + _MyMain.sNo;
            SqlStr += Comma + Sqote + _MyMain.Narration + Sqote;
            SqlStr += Comma + _MyMain.vAmount;
            SqlStr += Comma + Sqote + _MyMain.vStatus + Sqote;
            SqlStr += Comma + _MyMain.Fyear;
            SqlStr += Comma + DateTime.Now.ToString("yyyyMMdd");
            SqlStr += Comma + DateTime.Now.ToString("HHmm");
            SqlStr += ")";

            retVal = SetDataSet(SqlStr);
        }
        catch (Exception ex)
        {
            retVal = null;
        }
        return retVal;
    }
    public string DeleteTranTbl()
    {
        SqlStr = "DELETE  ";
        SqlStr += " FROM ActTran ";
        SqlStr += " WHERE ";
        SqlStr += " OfficeId = " + _MyMain.OfficeId;
        SqlStr += " and ProjectId = " + _MyMain.ProjectId;
        SqlStr += " and CFY = " + _MyMain.Fyear;
        SqlStr += " and Vtypeid = '" + _MyMain.Vtype + "'";
        SqlStr += " and VNo = " + _MyMain.VNo;

        string Res = SetDataSet(SqlStr);
        return Res;
    }
    public DataSet GetTransectionData()
    {
        SqlStr = "SELECT  ";
        SqlStr += " a.Actcode as ActCode ";
        SqlStr += ", b.ActDesc as ActName ";
        SqlStr += ", a.Amount as Amount ";
        SqlStr += ", a.Status as AmountSts ";
        SqlStr += ", a.Narration as Narration ";
        SqlStr += " FROM ActTran a,ActChart b ";
        SqlStr += " where ";
        SqlStr += " a.OfficeId = " + _MyMain.OfficeId;
        SqlStr += " and a.projectid = " + _MyMain.ProjectId;
        SqlStr += " and a.CFY = " + _MyMain.Fyear;
        SqlStr += " and a.Vtypeid = " + _MyMain.Vtype;
        SqlStr += " and a.Vno = " + _MyMain.VNo;
        SqlStr += " and a.projectid = b.projectid";
        SqlStr += " and a.officeid = b.officeid";
        SqlStr += " and a.actcode = b.actcode";

        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public DataSet GeEmailUser()
    {
        SqlStr = "SELECT * from RfEmailUser";

        DataSet Res = GetDataset(SqlStr);
        return Res;
    }
    public string GetRefCfy()
    {
        string retVal = string.Empty;
        SqlStr = "SELECT cfy ";
        SqlStr += " FROM RefTbl ";
        DataSet ds = GetDataset(SqlStr);
        if (ds.Tables[0].Rows.Count > 0)
        { retVal = ds.Tables[0].Rows[0]["cfy"].ToString(); }
        return retVal;
    }
    public DataSet LedgerPrintSp(string pActCode, string pStartDate, string pEndDate)
    {
        DataSet ds = null;
        try
        {
            SqlStr = "SELECT SUM(atr.[Amount]) as DrTotal ";
            SqlStr += " FROM (([RefTbl] rt";
            SqlStr += " INNER JOIN [ActTran] atr ON rt.[OfficeId]=atr.[OfficeId] AND rt.[ProjectId]=atr.[ProjectId] AND rt.[CFY]=atr.[CFY])";
            SqlStr += " INNER JOIN [ActRfVtype] arv ON atr.[VTypeId]=arv.[VtypeId])";
            SqlStr += " WHERE	atr.[ActCode]= " + pActCode;
            SqlStr += " AND atr.[TranDate] >= rt.[SDFyear] ";
            SqlStr += " AND atr.[TranDate] < " + pStartDate + " AND atr.[Status] ='D'";
            DataSet ds1 = GetDataset1(SqlStr);

            SqlStr = "SELECT SUM(atr.[Amount]) as CrTotal ";
            SqlStr += " FROM (([RefTbl] rt";
            SqlStr += " INNER JOIN [ActTran] atr ON rt.[OfficeId]=atr.[OfficeId] AND rt.[ProjectId]=atr.[ProjectId] AND rt.[CFY]=atr.[CFY])";
            SqlStr += " INNER JOIN [ActRfVtype] arv ON atr.[VTypeId]=arv.[VtypeId])";
            SqlStr += " WHERE	atr.[ActCode]= " + pActCode;
            SqlStr += " AND atr.[TranDate] >= rt.[SDFyear] ";
            SqlStr += " AND atr.[TranDate] < " + pStartDate + " AND atr.[Status] ='C'";
            DataSet ds2 = GetDataset1(SqlStr);

            SqlStr = "SELECT ro.OfficeDescription,rp.ProjectDescription,ac.ActDesc,arv.VType,arv.VtypeDescription ";
            SqlStr += " ,atr.*,ac.ActDesc FROM ((((( [RefTbl] rt";
            SqlStr += " INNER JOIN [RfOffice] ro ON rt.[OfficeId]=ro.[OfficeId])";
            SqlStr += " INNER JOIN [RfProject] rp ON rt.[ProjectId]=rp.[ProjectId])";
            SqlStr += " INNER JOIN [ActTran] atr ON rt.[OfficeId]=atr.[OfficeId] AND rt.[ProjectId]=atr.[ProjectId] AND rt.[CFY]=atr.[CFY])";
            SqlStr += " INNER JOIN [ActChart] ac ON rt.[OfficeId]=ac.[OfficeId] AND rt.[ProjectId]=ac.[ProjectId] and atr.[ActCode]=ac.ActCode)";
            SqlStr += " INNER JOIN [ActRfVtype] arv ON atr.[VTypeId]=arv.[VtypeId])";
            SqlStr += " WHERE	atr.[ActCode] = " + pActCode;
            SqlStr += " AND atr.[TranDate] >= " + pStartDate;
            SqlStr += " AND atr.[TranDate] <= " + pEndDate;
            SqlStr += " ORDER BY atr.[TranDate],atr.VNo";


            ds = GetDataset(SqlStr);

            ds = UType.addTableinDataSet(ds, ds1.Tables[0], "Table1");
            ds = UType.addTableinDataSet(ds, ds2.Tables[0], "Table2");

            return ds;

        }
        catch (Exception ex)
        {
            ds = null;
        }
        return ds;
    }

    public DataSet ProofListSp(string pStartDate, string pEndDate)
    {
        DataSet ds = null;
        try
        {
            string retVal = string.Empty;
            SqlStr = " SELECT ro.[OfficeDescription],rp.[ProjectDescription],ac.[ActDesc],arv.[VType],arv.[VtypeDescription] ";
            SqlStr += " ,atr.*,ac.[ActDesc] FROM ((((( [RefTbl] rt";
            SqlStr += " INNER JOIN [RfOffice] ro ON rt.[OfficeId]=ro.[OfficeId])";
            SqlStr += " INNER JOIN [RfProject] rp ON rt.[ProjectId]=rp.[ProjectId])";
            SqlStr += " INNER JOIN [ActTran] atr ON rt.[OfficeId]=atr.[OfficeId] AND rt.[ProjectId]=atr.[ProjectId] AND rt.[CFY]=atr.[CFY])";
            SqlStr += " INNER JOIN [ActChart] ac ON rt.[OfficeId]=ac.[OfficeId] AND rt.[ProjectId]=ac.[ProjectId] and atr.[ActCode]=ac.[ActCode])";
            SqlStr += " INNER JOIN [ActRfVtype] arv ON atr.[VTypeId]=arv.[VtypeId])";
            SqlStr += " WHERE atr.[TranDate] >= " + pStartDate;
            SqlStr += " AND atr.[TranDate] <= " + pEndDate;
            SqlStr += " ORDER BY atr.[TranDate],atr.[VNo],atr.[SNo]";
            ds = GetDataset(SqlStr);
        }
        catch (Exception ex)
        {
            ds = null;
        }
        return ds;
    }
}
