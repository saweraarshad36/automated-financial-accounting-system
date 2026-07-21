using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.IO;

/// <summary>
/// Summary description for MyMainAcs
/// </summary>
public class MyMainAcs
{
  
    #region Initialization

    string FolderReport = string.Empty;

    #endregion

    #region Constructor
    public MyMainAcs()
    {
    }
  
    #endregion


    #region Columns

    private string _ActCode = string.Empty;
    private string _ActDescription = string.Empty;
    private string _ActStatus = string.Empty;
    private string _Balance = string.Empty;
    private string _BalanceSts = string.Empty;
    private string _OfficeId = string.Empty;
    private string _ProjectId = string.Empty;
    private string _ActLevel = string.Empty;
    private string _LoginId = string.Empty;

    private string _Fyear = string.Empty;
    private string _TransectionDate = string.Empty;
    private string _Vtype = string.Empty;
    private string _VNo = string.Empty;
    private string _vAmount = string.Empty;
    private string _vStatus = string.Empty;
    private string _Narration = string.Empty;
    private string _sNo = string.Empty;
    private string _TranId = string.Empty;

    private string _StartFinancialYear = string.Empty;
    private string _EndFinancialYear = string.Empty;
    private string _LeftSideIncomeStatement = string.Empty;
    private string _RightSideIncomeStatement = string.Empty;
    private string _LeftSideBalanceSheet = string.Empty;
    private string _RightSideBalanceSheet = string.Empty;

    private string _EmpId = string.Empty;
    private string _EmpName = string.Empty;
    private string _EmpAddress = string.Empty;
    private string _EmpPhoneNo = string.Empty;
    private string _RecommendedId = string.Empty;
    private string _ApprovalId = string.Empty;
    private string _Advance = string.Empty;
    private string _UpdateDate = string.Empty;
    private string _UpdateTime = string.Empty;

    private string _Fld1 = string.Empty;
    private string _Fld2 = string.Empty;
    private string _Fld3 = string.Empty;
    private string _Fld4 = string.Empty;
    private string _Fld5 = string.Empty;
    private string _Fld6 = string.Empty;
    private string _Fld7 = string.Empty;
    private string _Fld8 = string.Empty;
    private string _Fld9 = string.Empty;
    private string _Fld10 = string.Empty;
    private string _Fld11 = string.Empty;
    private string _Fld12 = string.Empty;
    private string _Fld13 = string.Empty;
    private string _Fld14 = string.Empty;
    private string _Fld15 = string.Empty;
    private string _Fld16 = string.Empty;
    private string _Fld17 = string.Empty;
    private string _Fld18 = string.Empty;
    private string _Fld19 = string.Empty;
    private string _Fld20 = string.Empty;
    private string _Fld21 = string.Empty;
    private string _Fld22 = string.Empty;
    private string _Fld23 = string.Empty;
    private string _Fld24 = string.Empty;
    private string _Fld25 = string.Empty;
    private string _Fld26 = string.Empty;
    private string _Fld27 = string.Empty;
    private string _Fld28 = string.Empty;
    private string _Fld29 = string.Empty;
    private string _Fld30 = string.Empty;


    #endregion

    #region Property

    #region Ref
    public string StartFinancialYear
    {
        get { return _StartFinancialYear; }
        set { _StartFinancialYear = value; }
    }
    public string EndFinancialYear
    {
        get { return _EndFinancialYear; }
        set { _EndFinancialYear = value; }
    }
    public string LeftSideIncomeStatement
    {
        get { return _LeftSideIncomeStatement; }
        set { _LeftSideIncomeStatement = value; }
    }
    public string RightSideIncomeStatement
    {
        get { return _RightSideIncomeStatement; }
        set { _RightSideIncomeStatement = value; }
    }
    public string LeftSideBalanceSheet
    {
        get { return _LeftSideBalanceSheet; }
        set { _LeftSideBalanceSheet = value; }
    }
    public string RightSideBalanceSheet
    {
        get { return _RightSideBalanceSheet; }
        set { _RightSideBalanceSheet = value; }
    }

    #endregion

    #region ChartofAccount

    public string ActCode
    {
        get { return _ActCode; }
        set { _ActCode = value; }
    }

    public string ActDescription
    {
        get { return _ActDescription; }
        set { _ActDescription = value; }
    }

    public string ActStatus
    {
        get { return _ActStatus; }
        set { _ActStatus = value; }
    }

    public string Balance
    {
        get { return _Balance; }
        set
        {
            if (value == "")
            {
                _Balance = "0";
            }
            else
            {
                _Balance = value;
            }
        }
    }

    public string BalanceSts
    {
        get { return _BalanceSts; }
        set { _BalanceSts = value; }
    }

    public string OfficeId
    {
        get { return _OfficeId; }
        set { _OfficeId = value; }
    }
    public string ProjectId
    {
        get { return _ProjectId; }
        set { _ProjectId = value; }
    }

    public string LoginId
    {
        get { return _LoginId; }
        set { _LoginId = value; }
    }

    public string ActLevel
    {
        get { return _ActLevel; }
        set { _ActLevel = value; }
    }
    # endregion

    #region Transection Tbl
    public string Fyear
    {
        get { return _Fyear; }
        set { _Fyear = value; }
    }
    public string TransectionDate
    {
        get
        {
            if (_TransectionDate == "")
            {
                _TransectionDate = "0";
            }

            return _TransectionDate;
        }
        set { _TransectionDate = value; }
    }


    public string vAmount
    {
        get { return _vAmount; }
        set { _vAmount = value; }
    }
    public string vStatus
    {
        get { return _vStatus; }
        set { _vStatus = value; }
    }
    public string Vtype
    {
        get { return _Vtype; }
        set { _Vtype = value; }
    }
    public string VNo
    {
        get { return _VNo; }
        set { _VNo = value; }
    }
    public string sNo
    {
        get { return _sNo; }
        set { _sNo = value; }
    }
    public string Narration
    {
        get { return _Narration; }
        set { _Narration = value; }
    }
    public string TranId
    {
        get { return _TranId; }
        set { _TranId = value; }
    }


    #endregion

    #region RfEmployee
    public string EmpId
    {
        get { return _EmpId; }
        set { _EmpId = value; }
    }
    public string EmpName
    {
        get
        {
            return _EmpName;
        }
        set { _EmpName = value; }
    }


    public string EmpAddress
    {
        get { return _EmpAddress; }
        set { _EmpAddress = value; }
    }
    public string EmpPhoneNo
    {
        get { return _EmpPhoneNo; }
        set { _EmpPhoneNo = value; }
    }
    public string RecommendedId
    {
        get
        {
            if (_RecommendedId == "")
            {
                _RecommendedId = "0";
            }
            return _RecommendedId;
        }
        set { _RecommendedId = value; }
    }
    public string ApprovalId
    {
        get
        {
            if (_ApprovalId == "")
            {
                _ApprovalId = "0";
            }
            return _ApprovalId;
        }
        set { _ApprovalId = value; }
    }

    public string Advance
    {
        get
        {
            if (_Advance == "")
            {
                _Advance = "0";
            }
            return _Advance;
        }
        set { _Advance = value; }
    }

    public string UpdateDate
    {
        get { return _UpdateDate; }
        set { _UpdateDate = value; }
    }
    public string UpdateTime
    {
        get { return _UpdateTime; }
        set { _UpdateTime = value; }
    }

    #endregion

  

    public string Fld1
    {
        get { return _Fld1; }
        set { _Fld1 = value; }
    }
    public string Fld2
    {
        get { return _Fld2; }
        set { _Fld2 = value; }
    }
    public string Fld3
    {
        get { return _Fld3; }
        set { _Fld3 = value; }
    }
    public string Fld4
    {
        get { return _Fld4; }
        set { _Fld4 = value; }
    }
    public string Fld5
    {
        get { return _Fld5; }
        set { _Fld5 = value; }
    }
    public string Fld6
    {
        get { return _Fld6; }
        set { _Fld6 = value; }
    }
    public string Fld7
    {
        get { return _Fld7; }
        set { _Fld7 = value; }
    }
    public string Fld8
    {
        get { return _Fld8; }
        set { _Fld8 = value; }
    }
    public string Fld9
    {
        get { return _Fld9; }
        set { _Fld9 = value; }
    }
    public string Fld10
    {
        get { return _Fld10; }
        set { _Fld10 = value; }
    }

    public string Fld11
    {
        get { return _Fld11; }
        set { _Fld11 = value; }
    }
    public string Fld12
    {
        get { return _Fld12; }
        set { _Fld12 = value; }
    }
    public string Fld13
    {
        get { return _Fld13; }
        set { _Fld13 = value; }
    }
    public string Fld14
    {
        get { return _Fld14; }
        set { _Fld14 = value; }
    }
    public string Fld15
    {
        get { return _Fld15; }
        set { _Fld15 = value; }
    }
    public string Fld16
    {
        get { return _Fld16; }
        set { _Fld16 = value; }
    }
    public string Fld17
    {
        get { return _Fld17; }
        set { _Fld17 = value; }
    }
    public string Fld18
    {
        get { return _Fld18; }
        set { _Fld18 = value; }
    }
    public string Fld19
    {
        get { return _Fld19; }
        set { _Fld19 = value; }
    }
    public string Fld20
    {
        get { return _Fld20; }
        set { _Fld20 = value; }
    }

    public string Fld21
    {
        get { return _Fld21; }
        set { _Fld21 = value; }
    }
    public string Fld22
    {
        get { return _Fld22; }
        set { _Fld22 = value; }
    }
    public string Fld23
    {
        get { return _Fld23; }
        set { _Fld23 = value; }
    }
    public string Fld24
    {
        get { return _Fld24; }
        set { _Fld24 = value; }
    }
    public string Fld25
    {
        get { return _Fld25; }
        set { _Fld25 = value; }
    }
    public string Fld26
    {
        get { return _Fld26; }
        set { _Fld26 = value; }
    }
    public string Fld27
    {
        get { return _Fld27; }
        set { _Fld27 = value; }
    }
    public string Fld28
    {
        get { return _Fld28; }
        set { _Fld28 = value; }
    }
    public string Fld29
    {
        get { return _Fld29; }
        set { _Fld29 = value; }
    }
    public string Fld30
    {
        get { return _Fld30; }
        set { _Fld30 = value; }
    } 



    #endregion

    public DataSet Sp_GetLogin(string pUserId, string pPassword)
    {      
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.GetLogin(pUserId, pPassword);
        return result;
    }
    public DataSet GetCount()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection,this);
        DataSet result = oMyDb.GetCount();
        return result;
    }
    public string InsertLoginHistory()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        string result = oMyDb.InsertLoginHistory();
        return result;
    }
    public DataSet Sp_GetMenu(string pUserId, string MenuLevel)
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.Sp_GetMenu(pUserId, MenuLevel);
        return result;
    }
    public DataSet Sp_GetMenu_Dept(string pUserId, string MenuLevel, string pDepart)
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.Sp_GetMenu_Dept(pUserId, MenuLevel, pDepart);
        return result;
    }
    public DataSet GetRefTbl()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.GeRefTbl();

        return result;
    }
    public DataSet FillComboChart()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.GetFillComboActCode();
        return result;
    }

    public DataSet FillComboOffice()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.GetFillComboOffice();
        return result;
    }

    public DataSet FillComboProject()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.GetFillComboProject();

        return result;
    }
    public DataSet GetRefData()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.GetRefData();

        return result;
    }
    public DataSet SelectRef()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.SelectRef();       
        return result;
    }
    public string InsertRef()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        string result = oMyDb.InsertRef();
        return result;
    }
    public string UpdateRef()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        string result = oMyDb.UpdateRef();
        return result;

    }
    public DataSet FillComboActLevel()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.Sp_GetFillComboActLevel();

        return result;
    }
    public DataSet Sp_SelectChart()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.Sp_SelectChart();
        return result;
    }
    public string InsertChart()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        string result = oMyDb.InsertChart();
        
        return result;

    }

    public string UpdateChart()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        string result = oMyDb.UpdateChart();
        
        return result;

    }
    public DataSet Sp_GetActCode()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.Sp_GetActCode();

        return result;
    }
    public DataSet Sp_PrintChartOfAccount()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.Sp_PrintChartOfAccount();
        return result;

    }
    public DataSet MoveInRptDsChartOfAccount(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = row1["OfficeDescription"];
                row["C2"] = row1["ProjectDescription"];
                row["C3"] = row1["ActCode"];
                row["C4"] = row1["ActDesc"];
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }
    public byte[] GetImage()
    {
        byte[] imgbyte = null;
        string FilePath = AppDomain.CurrentDomain.BaseDirectory + "pral.Jpg";
        FileStream fs = null;
        if (File.Exists(FilePath))
        {
            fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            BinaryReader br;
            br = new BinaryReader(fs);
            imgbyte = new byte[fs.Length + 1];
            imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
        }
        return imgbyte;
    }
    public DataSet BalanceSheetSp(string pStartDate, string pEndDate)
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.BalanceSheetSp(pStartDate, pEndDate);
        return result;

    }
    public DataSet MoveInRptDsBalanceSheet(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal TotalBalD = 0;
        decimal TotalBalC = 0;
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[1].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = "ABC";
                row["C2"] = "Balance Sheet";
                //row["C3"] = row1["ActDesc"].ToString();
                row["C4"] = UType.GetDate1(sDate);
                row["C5"] = UType.GetDate1(eDate);
                row["C11"] = row1["ActCode"].ToString();
                row["C12"] = row1["ActDesc"].ToString();

                decimal BalD = 0;
                decimal BalC = 0;

                foreach (DataRow row2 in InputDataSet.Tables[0].Rows)
                {
                    if (row1["ActCode"].ToString() == row2["ActCode"].ToString())
                    {
                        if (row2["Status"].ToString() == "D")
                        {
                            BalD = BalD + UType.MyCtoD(row2["Amount"].ToString());
                        }
                        if (row2["Status"].ToString() == "C")
                        {
                            BalC = BalC + UType.MyCtoD(row2["Amount"].ToString());
                        }
                    }
                }
                if (BalD > BalC)
                {
                    BalD = BalD - BalC;
                    row["C13"] = BalD.ToString();
                    row["C14"] = "0";
                    TotalBalD += BalD;
                }
                else
                {
                    BalC = BalC - BalD;
                    row["C13"] = "0";
                    row["C14"] = BalC.ToString();
                    TotalBalC += BalC;
                }
                row["C15"] = TotalBalD.ToString();
                row["C16"] = TotalBalC.ToString();
                row["CLogo"] = GetImage();
                row["C18"] = BalanceSts;
                if (row1["ActCode"].ToString().Length == 2 || BalD > 0 || BalC > 0)
                {
                    rptDataSet.Tables[0].Rows.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }
    public DataSet IncomeStatementSp(string pStartDate, string pEndDate)
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.IncomeStatementSp(pStartDate, pEndDate);
        return result;

    }
    public DataSet MoveInRptDsIncomeStatement(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal TotalBalD = 0;
        decimal TotalBalC = 0;
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[1].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = "ABC";
                row["C2"] = "Income Statement";
                //row["C3"] = row1["ActDesc"].ToString();
                row["C4"] = UType.GetDate1(sDate);
                row["C5"] = UType.GetDate1(eDate);
                row["C11"] = row1["ActCode"].ToString();
                row["C12"] = row1["ActDesc"].ToString();

                decimal BalD = 0;
                decimal BalC = 0;

                foreach (DataRow row2 in InputDataSet.Tables[0].Rows)
                {
                    if (row1["ActCode"].ToString() == row2["ActCode"].ToString())
                    {
                        if (row2["Status"].ToString() == "D")
                        {
                            BalD = BalD + UType.MyCtoD(row2["Amount"].ToString());
                        }
                        if (row2["Status"].ToString() == "C")
                        {
                            BalC = BalC + UType.MyCtoD(row2["Amount"].ToString());
                        }
                    }
                }
                if (BalD > BalC)
                {
                    BalD = BalD - BalC;
                    row["C13"] = BalD.ToString();
                    row["C14"] = "0";
                    TotalBalD += BalD;
                }
                else
                {
                    BalC = BalC - BalD;
                    row["C13"] = "0";
                    row["C14"] = BalC.ToString();
                    TotalBalC += BalC;
                }
                row["C15"] = TotalBalD.ToString();
                row["C16"] = TotalBalC.ToString();
                row["CLogo"] = GetImage();
                row["C18"] = BalanceSts;
                if (row1["ActCode"].ToString().Length == 2 || BalD > 0 || BalC > 0)
                {
                    rptDataSet.Tables[0].Rows.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }
    public DataSet FillComboVtype()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.GetFillComboVtype();

        return result;
    }
   
    public string InsertTranTbl()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        string result = oMyDb.InsertTranTbl();
       
        return result;

    }
    public string DeleteTranTbl()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        string result = oMyDb.DeleteTranTbl();
        
        return result;
    }
    public DataSet GetTransectionData()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.GetTransectionData();
        return result;
    }
    public DataSet GetEmailUser()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.GeEmailUser();

        return result;
    }
    public string GetRefCfy()
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        string result = oMyDb.GetRefCfy();
        
        return result;

    }
    public DataSet LedgerPrintSp(string pActCode, string pStartDate, string pEndDate)
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.LedgerPrintSp(pActCode, pStartDate, pEndDate);
        return result;

    }
    public DataSet MoveInRptDsLedger(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal BalanceAmt = UType.MyCtoD(InputDataSet.Tables[0].Rows[0]["Amount"].ToString());  //0;
        string BalanceSts = InputDataSet.Tables[0].Rows[0]["Status"].ToString(); //"D";
        decimal TotalDr = 0;
        decimal TotalCr = 0;
        try
        {
            //DataRow row = rptDataSet.Tables[0].NewRow();
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                decimal OpeningBalance = 0;
                decimal DrTotal = 0;
                decimal CrTotal = 0;
                if (UType.IsSql())
                {
                    DrTotal = UType.MyCtoD(row1["DrTotal"].ToString());
                    CrTotal = UType.MyCtoD(row1["CrTotal"].ToString());
                    OpeningBalance = DrTotal - CrTotal;
                }
                if (UType.IsSql() == false)
                {
                    try
                    {
                        DrTotal = UType.MyCtoD(InputDataSet.Tables[1].Rows[0]["DrTotal"].ToString());
                        CrTotal = UType.MyCtoD(InputDataSet.Tables[2].Rows[0]["CrTotal"].ToString());
                        OpeningBalance = DrTotal - CrTotal;
                    }
                    catch (Exception ex)
                    {
                        string var1 = ex.Message.ToString();
                    }
                }
                //decimal OpeningBalance = UType.MyCtoD(row1["DrTotal"].ToString()) - UType.MyCtoD(row1["CrTotal"].ToString());
                BalanceAmt = OpeningBalance;
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = "ABC";
                row["C2"] = row1["ActDesc"].ToString() + " " + "Ledger Report";
                //row["C3"] = row1["ActDesc"].ToString();
                row["C4"] = UType.GetDate1(sDate);
                row["C5"] = UType.GetDate1(eDate);
                if (CrTotal > DrTotal)
                {
                    BalanceSts = "C";
                }
                row["C6"] = OpeningBalance.ToString();
                row["C7"] = BalanceSts;
                row["C11"] = row1["VType"].ToString();
                row["C12"] = row1["VNo"].ToString();
                row["C13"] = UType.GetDate1(row1["TranDate"].ToString());
                row["C14"] = row1["Narration"].ToString();
                row["C15"] = row1["Amount"].ToString();

                #region BalanceSts D
                if (BalanceSts == "D")
                {
                    if (row1["Status"].ToString() == "D")
                    {
                        BalanceAmt += UType.MyCtoD(row1["Amount"].ToString());
                    }
                    if (row1["Status"].ToString() == "C")
                    {
                        if (UType.MyCtoD(row1["Amount"].ToString()) > BalanceAmt)
                        {
                            BalanceSts = "C";
                        }
                        BalanceAmt = BalanceAmt - UType.MyCtoD(row1["Amount"].ToString());
                    }
                }

                #endregion

                #region BalanceSts C

                if (BalanceSts == "C")
                {
                    if (row1["Status"].ToString() == "C")
                    {
                        BalanceAmt += UType.MyCtoD(row1["Amount"].ToString());
                    }
                    if (row1["Status"].ToString() == "D")
                    {
                        if (UType.MyCtoD(row1["Amount"].ToString()) > BalanceAmt)
                        {
                            BalanceSts = "D";
                        }
                        BalanceAmt = BalanceAmt - UType.MyCtoD(row1["Amount"].ToString());
                    }
                }
                #endregion

                if (row1["Status"].ToString() == "C")
                {
                    row["C15"] = "0";
                    row["C16"] = row1["Amount"].ToString();
                }
                row["CLogo"] = GetImage();
                row["C17"] = BalanceAmt;
                row["C18"] = BalanceSts;

                //row["C19"] = Convert.ToString(TotalDr + UType.MyCtoD(row["C15"].ToString()));
                //row["C20"] = Convert.ToString(TotalDr + UType.MyCtoD(row["C15"].ToString()));

                rptDataSet.Tables[0].Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet ProofListSp(string pStartDate, string pEndDate)
    {
        MyDbAcs oMyDb = new MyDbAcs(Connection.OleDbConnection, this);
        DataSet result = oMyDb.ProofListSp(pStartDate, pEndDate);
        return result;

    }
    public DataSet MoveInRptDsProofList(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = "ABC";
                row["C2"] = "Proof List Report";
                row["C3"] = row1["ActDesc"].ToString();
                row["C4"] = UType.GetDate1(sDate);
                row["C5"] = UType.GetDate1(eDate);
                row["C11"] = row1["VType"].ToString();
                row["C12"] = row1["VNo"].ToString();
                row["C13"] = UType.GetDate1(row1["TranDate"].ToString());
                row["C14"] = row1["ActDesc"].ToString();
                row["C15"] = row1["Amount"].ToString();
                if (row1["Status"].ToString() == "C")
                {
                    row["C15"] = "";
                    row["C16"] = row1["Amount"].ToString();
                }
                row["CLogo"] = GetImage();
                row["C17"] = row1["Narration"].ToString();
                row["C18"] = BalanceSts;
                rptDataSet.Tables[0].Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }
    public DataSet MoveInRptDsClrRpt(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                
                row["C2"] = "Proof List Report";
                row["C3"] = row1["ActDesc"].ToString();
                row["C4"] = UType.GetDate1(sDate);
                row["C5"] = UType.GetDate1(eDate);
                row["C11"] = row1["VType"].ToString();
                row["C12"] = row1["VNo"].ToString();
                row["C13"] = UType.GetDate1(row1["TranDate"].ToString());
                row["C14"] = row1["ActDesc"].ToString();
                row["C15"] = row1["Amount"].ToString();
                if (row1["Status"].ToString() == "C")
                {
                    row["C15"] = "";
                    row["C16"] = row1["Amount"].ToString();
                }
                row["CLogo"] = GetImage();
                row["C17"] = row1["Narration"].ToString();
                row["C18"] = BalanceSts;
                rptDataSet.Tables[0].Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

}
