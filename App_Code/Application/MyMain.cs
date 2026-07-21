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
using System.Data.OleDb;
using System.IO;
using System.ServiceModel.Channels;
using System.Windows.Forms;
using System.Runtime.InteropServices.WindowsRuntime;

/// <summary>
/// Summary description for MyMain
/// </summary>
public class MyMain
{

    #region Defaults

    private SqlConnection _SqlConnection;
    private OleDbConnection _OleDbConnection;


    #endregion

    public bool DisposeSQLConnection()
    {
        //_SqlConnection.Close();
        // _SqlConnection.Dispose();
        return true;
    }

    public bool DisposeOleDbConnection()
    {
        _OleDbConnection.Close();
        _OleDbConnection.Dispose();
        return true;
    }


    #region Initialization

    public DataTable DtChart = null;
    public DataTable DtRef = null;
    public DataTable DtVoucher = null;
    public DataTable DtTran = null;
    public DataTable DtLedger = null;

    public DataTable DtChartRep = null;
    string FolderReport = string.Empty;
    string SqlStr = string.Empty;
    #endregion

    #region Constructors

    public MyMain()
    {
    }
    public MyMain(SqlConnection con)
    {
        _SqlConnection = con;
    }

    public MyMain(OleDbConnection con)
    {
        _OleDbConnection = con;
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
    private string _Fld31 = string.Empty;
    private string _Fld32 = string.Empty;
    private string _Fld33 = string.Empty;
    private string _Fld34 = string.Empty;
    private string _Fld35 = string.Empty;
    private string _Fld36 = string.Empty;
    private string _Fld37 = string.Empty;
    private string _Fld38 = string.Empty;
    private string _Fld39 = string.Empty;
    private string _Fld40 = string.Empty;
    private string _Fld41 = string.Empty;
    private string _Fld42 = string.Empty;
    private string _Fld43 = string.Empty;
    private string _Fld44 = string.Empty;
    private string _Fld45 = string.Empty;
    private string _Fld46 = string.Empty;
    private string _Fld47 = string.Empty;
    private string _Fld48 = string.Empty;
    private string _Fld49 = string.Empty;
    private string _Fld50 = string.Empty;
    private string _Fld51 = string.Empty;
    private string _Fld52 = string.Empty;
    private string _Fld53 = string.Empty;
    private string _Fld54 = string.Empty;
    private string _Fld55 = string.Empty;
    private string _Fld56 = string.Empty;
    private string _Fld57 = string.Empty;
    private string _Fld58 = string.Empty;
    private string _Fld59 = string.Empty;
    private string _Fld60 = string.Empty;
    private string _Fld61 = string.Empty;
    private string _Fld62 = string.Empty;
    private string _Fld63 = string.Empty;
    private string _Fld64 = string.Empty;
    private string _Fld65 = string.Empty;
    private string _Fld66 = string.Empty;
    private string _Fld67 = string.Empty;
    private string _Fld68 = string.Empty;
    private string _Fld69 = string.Empty;
    private string _Fld70 = string.Empty;
    private string _Fld71 = string.Empty;
    private string _Fld72 = string.Empty;
    private string _Fld73 = string.Empty;
    private string _Fld74 = string.Empty;
    private string _Fld75 = string.Empty;
    private string _Fld76 = string.Empty;
    private string _Fld77 = string.Empty;
    private string _Fld78 = string.Empty;
    private string _Fld79 = string.Empty;
    private string _Fld80 = string.Empty;
    private string _Fld81 = string.Empty;
    private string _Fld82 = string.Empty;
    private string _Fld83 = string.Empty;
    private string _Fld84 = string.Empty;
    private string _Fld85 = string.Empty;
    private string _Fld86 = string.Empty;
    private string _Fld87 = string.Empty;
    private string _Fld88 = string.Empty;
    private string _Fld89 = string.Empty;
    private string _Fld90 = string.Empty;
    private string _Fld91 = string.Empty;
    private string _Fld92 = string.Empty;
    private string _Fld93 = string.Empty;
    private string _Fld94 = string.Empty;
    private string _Fld95 = string.Empty;
    private string _Fld96 = string.Empty;
    private string _Fld97 = string.Empty;
    private string _Fld98 = string.Empty;
    private string _Fld99 = string.Empty;
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

    #region RfTbl
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

    public string Fld31
    {
        get { return _Fld31; }
        set { _Fld31 = value; }
    }
    public string Fld32
    {
        get { return _Fld32; }
        set { _Fld32 = value; }
    }
    public string Fld33
    {
        get { return _Fld33; }
        set { _Fld33 = value; }
    }
    public string Fld34
    {
        get { return _Fld34; }
        set { _Fld34 = value; }
    }
    public string Fld35
    {
        get { return _Fld35; }
        set { _Fld35 = value; }
    }
    public string Fld36
    {
        get { return _Fld36; }
        set { _Fld36 = value; }
    }
    public string Fld37
    {
        get { return _Fld37; }
        set { _Fld37 = value; }
    }
    public string Fld38
    {
        get { return _Fld38; }
        set { _Fld38 = value; }
    }
    public string Fld39
    {
        get { return _Fld39; }
        set { _Fld39 = value; }
    }
    public string Fld40
    {
        get { return _Fld40; }
        set { _Fld40 = value; }
    }
    public string Fld41
    {
        get { return _Fld41; }
        set { _Fld41 = value; }
    }
    public string Fld42
    {
        get { return _Fld42; }
        set { _Fld42 = value; }
    }
    public string Fld43
    {
        get { return _Fld43; }
        set { _Fld43 = value; }
    }
    public string Fld44
    {
        get { return _Fld44; }
        set { _Fld44 = value; }
    }
    public string Fld45
    {
        get { return _Fld45; }
        set { _Fld45 = value; }
    }
    public string Fld46
    {
        get { return _Fld46; }
        set { _Fld46 = value; }
    }
    public string Fld47
    {
        get { return _Fld47; }
        set { _Fld47 = value; }
    }
    public string Fld48
    {
        get { return _Fld48; }
        set { _Fld48 = value; }
    }
    public string Fld49
    {
        get { return _Fld49; }
        set { _Fld49 = value; }
    }
    public string Fld50
    {
        get { return _Fld50; }
        set { _Fld50 = value; }
    }
    public string Fld51
    {
        get { return _Fld51; }
        set { _Fld51 = value; }
    }
    public string Fld52
    {
        get { return _Fld52; }
        set { _Fld52 = value; }
    }
    public string Fld53
    {
        get { return _Fld53; }
        set { _Fld53 = value; }
    }
    public string Fld54
    {
        get { return _Fld54; }
        set { _Fld54 = value; }
    }
    public string Fld55
    {
        get { return _Fld55; }
        set { _Fld55 = value; }
    }
    public string Fld56
    {
        get { return _Fld56; }
        set { _Fld56 = value; }
    }
    public string Fld57
    {
        get { return _Fld57; }
        set { _Fld57 = value; }
    }
    public string Fld58
    {
        get { return _Fld58; }
        set { _Fld58 = value; }
    }
    public string Fld59
    {
        get { return _Fld59; }
        set { _Fld59 = value; }
    }
    public string Fld60
    {
        get { return _Fld60; }
        set { _Fld60 = value; }
    }
    public string Fld61
    {
        get { return _Fld61; }
        set { _Fld61 = value; }
    }
    public string Fld62
    {
        get { return _Fld62; }
        set { _Fld62 = value; }
    }
    public string Fld63
    {
        get { return _Fld63; }
        set { _Fld63 = value; }
    }
    public string Fld64
    {
        get { return _Fld64; }
        set { _Fld64 = value; }
    }
    public string Fld65
    {
        get { return _Fld65; }
        set { _Fld65 = value; }
    }
    public string Fld66
    {
        get { return _Fld66; }
        set { _Fld66 = value; }
    }
    public string Fld67
    {
        get { return _Fld67; }
        set { _Fld67 = value; }
    }
    public string Fld68
    {
        get { return _Fld68; }
        set { _Fld68 = value; }
    }
    public string Fld69
    {
        get { return _Fld69; }
        set { _Fld69 = value; }
    }
    public string Fld70
    {
        get { return _Fld70; }
        set { _Fld70 = value; }
    }
    public string Fld71
    {
        get { return _Fld71; }
        set { _Fld71 = value; }
    }
    public string Fld72
    {
        get { return _Fld72; }
        set { _Fld72 = value; }
    }
    public string Fld73
    {
        get { return _Fld73; }
        set { _Fld73 = value; }
    }
    public string Fld74
    {
        get { return _Fld74; }
        set { _Fld74 = value; }
    }
    public string Fld75
    {
        get { return _Fld75; }
        set { _Fld75 = value; }
    }
    public string Fld76
    {
        get { return _Fld76; }
        set { _Fld76 = value; }
    }
    public string Fld77
    {
        get { return _Fld77; }
        set { _Fld77 = value; }
    }
    public string Fld78
    {
        get { return _Fld78; }
        set { _Fld78 = value; }
    }
    public string Fld79
    {
        get { return _Fld79; }
        set { _Fld79 = value; }
    }
    public string Fld80
    {
        get { return _Fld80; }
        set { _Fld80 = value; }
    }
    public string Fld81
    {
        get { return _Fld81; }
        set { _Fld81 = value; }
    }
    public string Fld82
    {
        get { return _Fld82; }
        set { _Fld82 = value; }
    }
    public string Fld83
    {
        get { return _Fld83; }
        set { _Fld83 = value; }
    }
    public string Fld84
    {
        get { return _Fld84; }
        set { _Fld84 = value; }
    }
    public string Fld85
    {
        get { return _Fld85; }
        set { _Fld85 = value; }
    }
    public string Fld86
    {
        get { return _Fld86; }
        set { _Fld86 = value; }
    }
    public string Fld87
    {
        get { return _Fld87; }
        set { _Fld87 = value; }
    }
    public string Fld88
    {
        get { return _Fld88; }
        set { _Fld88 = value; }
    }
    public string Fld89
    {
        get { return _Fld89; }
        set { _Fld89 = value; }
    }
    public string Fld90
    {
        get { return _Fld90; }
        set { _Fld90 = value; }
    }
    public string Fld91
    {
        get { return _Fld92; }
        set { _Fld92 = value; }
    }
    public string Fld93
    {
        get { return _Fld93; }
        set { _Fld93 = value; }
    }
    public string Fld94
    {
        get { return _Fld94; }
        set { _Fld94 = value; }
    }
    public string Fld95
    {
        get { return _Fld95; }
        set { _Fld95 = value; }
    }
    public string Fld96
    {
        get { return _Fld96; }
        set { _Fld96 = value; }
    }
    public string Fld97
    {
        get { return _Fld97; }
        set { _Fld97 = value; }
    }
    public string Fld98
    {
        get { return _Fld98; }
        set { _Fld98 = value; }
    }
    public string Fld99
    {
        get { return _Fld99; }
        set { _Fld99 = value; }
    }


    #endregion

    #endregion

    #region CreateDataTable

    public DataTable CreateDataTableRef()
    {
        DtRef = new DataTable();
        AddCol(DtRef, "CompanyName");
        AddCol(DtRef, "StartDate");
        AddCol(DtRef, "EndDate");
        return DtRef;
    }

    public DataTable CreateDataTableChart()
    {
        DtChart = new DataTable();

        AddCol(DtChart, "ActCode");
        AddCol(DtChart, "ActDescription");
        AddCol(DtChart, "Balance");
        AddCol(DtChart, "BalStatus");
        AddCol(DtChart, "ActStatus");
        AddCol(DtChart, "OfficeId");
        AddCol(DtChart, "OfficeName");
        AddCol(DtChart, "ProjectId");
        AddCol(DtChart, "ProjectName");
        return DtChart;
    }

    public DataTable CreateDataTableVoucher()
    {
        DtVoucher = new DataTable();

        AddCol(DtVoucher, "ActCode");
        AddCol(DtVoucher, "ActDescription");
        AddCol(DtVoucher, "Amount");
        AddCol(DtVoucher, "Status");
        AddCol(DtVoucher, "OfficeName");
        AddCol(DtVoucher, "VNo");
        AddCol(DtVoucher, "VDate");
        AddCol(DtVoucher, "Narration");
        return DtVoucher;
    }


    #endregion

    #region Add Data to Table
    public void AddDataToTableRef()
    {
        try
        {
            DataRow row = DtRef.NewRow();
            row["CompanyName"] = "ABC & Company";
            DtRef.Rows.Add(row);
            row = null;
        }
        catch (Exception ex)
        {

        }

    }

    public void AddDataToTable(DataSet RptDataset, DataSet inputDataSet)
    {
        try
        {
            DataRow row;

            foreach (DataRow row1 in inputDataSet.Tables[0].Rows)
            {
                row = DtChart.NewRow();
                row["ActCode"] = row1["ActCode"];
                row["ActDescription"] = UType.GetActName(inputDataSet.Tables["RfActCode"], row1["ActCode"].ToString());
                row["ActDescription"] = row1["ActDesc"];
                row["Balance"] = row1["Balance"];
                row["BalStatus"] = row1["BalanceSts"];
                row["ActStatus"] = row1["ActStatus"];
                row["OfficeId"] = row1["OfficeId"];
                row["OfficeName"] = UType.GetOfficeName(inputDataSet.Tables["RfOffice"], row1["OfficeId"].ToString());
                row["ProjectId"] = row1["ProjectId"];
                row["ProjectName"] = UType.GetProjectName(inputDataSet.Tables["RfProject"], row1["ProjectId"].ToString());
                DtChart.Rows.Add(row);
            }
            row = null;


        }
        catch (Exception ex)
        {
            throw (ex);
        }

    }




    #endregion

    public DataSet MoveInSalesR2(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal AvgCost = 0;
        DataRow row2 = null;
        try
        {
            //Sales
            if (rptDataSet.Tables[0].Rows.Count > 0)
            {
                row2 = rptDataSet.Tables[0].NewRow();
                row2["c1"] = row2["ShipmentNo"];
                row2["c2"] = row2["ShipmentDate"];
                row2["c3"] = row2["AwbNo"];
                row2["c4"] = row2["Consignee"];
                // row2["c5"] = row1[""];

                row2["c11"] = "Sales";
                row2["c12"] = "No Of Package Sold";
                row2["c13"] = "Unit";
                row2["c14"] = "Per Unit Rate";
                row2["c15"] = "Amount Sold";
                row2["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row2);
            }
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C22"] = row1["NoofPackage"];
                row["C23"] = GetUomDescription(row1["uomid"].ToString());
                row["C24"] = row1["PerUnitRate"];
                row["C25"] = Convert.ToString(UType.MyCtoD(row1["NoofPackage"].ToString()) * UType.MyCtoD(row1["PerUnitRate"].ToString()));
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
            }
            row2 = rptDataSet.Tables[0].NewRow();
            row2["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row2);

            //Purchases
            row2 = rptDataSet.Tables[0].NewRow();
            row2["c11"] = "Purchases";
            row2["c12"] = "Per Unit Qty";
            row2["c13"] = "Unit";
            row2["c14"] = "Unit Rate";
            row2["c15"] = "Type";
            row2["c16"] = "Quantity";
            row2["c17"] = "Qty Unit";
            row2["c18"] = "Amount";
            row2["c19"] = "Wastage Wt";
            row2["c20"] = "Wastage Amt.";
            row2["c21"] = "Avg. Cost";

            row2["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row2);

            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C22"] = row1["PerUnitQty"];
                row["C23"] = GetUomDescription(row1["PerUnitQtyUomId"].ToString());
                row["C24"] = row1["QtyUnitRate"]; //ProductType
                row["C25"] = GetTypeDescription(row1["ProductType"].ToString());
                row["C26"] = row1["Qty"];
                row["C27"] = GetUomDescription(row1["QtyUomId"].ToString());
                row["C28"] = row1["QtyAmount"];
                row["C29"] = row1["WastageWt"];
                row["C30"] = row1["WastageAmount"];

                if (UType.MyCtoD(row1["QtyAmount"].ToString()) > 0 || UType.MyCtoD(row1["WastageAmount"].ToString()) > 0)
                {
                    AvgCost = (UType.MyCtoD(row1["QtyUnitRate"].ToString()) * (UType.MyCtoD(row1["Qty"].ToString()) - UType.MyCtoD(row1["WastageWt"].ToString()))) / (UType.MyCtoD(row1["QtyAmount"].ToString()) + UType.MyCtoD(row1["WastageAmount"].ToString()));
                }
                row["C31"] = UType.MyFormat(AvgCost.ToString());


                //row["C25"] = Convert.ToString(UType.MyCtoD(row1["NoofPackage"].ToString()) * UType.MyCtoD(row1["PerUnitRate"].ToString()));
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

    public DataSet MoveInSalesR3Old(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal AvgCost = 0;
        DataRow row = null;
        try
        {
            #region Opeining Balance

            row = rptDataSet.Tables[0].NewRow();
            row["c11"] = "OPENING STOCK";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);

            row = rptDataSet.Tables[0].NewRow();
            row["c11"] = "Invoice No";
            row["c12"] = "Variety";
            row["c13"] = "No Of Packages";
            row["c14"] = "Avg. Wt";
            row["c15"] = "Net Wt";
            row["c16"] = "Rate";
            row["c17"] = "Amount";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);

            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                row = rptDataSet.Tables[0].NewRow();
                row["C21"] = "1";
                row["C22"] = row1["ProductDescription"];
                row["C23"] = row1["Qty"];
                //row["C24"] = UType.MyFormat(row1["AvgWt"].ToString());
                try
                {
                    row["C24"] = Convert.ToString(UType.MyFormat(UType.MyCtoD(row["NetWt"].ToString()) / UType.MyCtoD(row["Qty"].ToString())));
                }
                catch (Exception)
                {
                }
                row["C25"] = row1["NetWt"];
                //row["C26"] = row1["Rate"];
                try
                {
                    row["C26"] = Convert.ToString(UType.MyFormat(UType.MyCtoD(row["UnitRate"].ToString()) / UType.MyCtoD(row["CountUnitRate"].ToString())));
                }
                catch (Exception)
                {
                }
                row["C27"] = row1["Amount"];
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
            }
            row = rptDataSet.Tables[0].NewRow();
            row["c21"] = "----------";
            row["c22"] = "----------";
            row["c23"] = "----------";
            row["c24"] = "----------";
            row["c25"] = "----------";
            row["c26"] = "----------";
            row["c27"] = "----------";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);
            #endregion

            #region Purchases

            row = rptDataSet.Tables[0].NewRow();
            row["c11"] = "PURCHASES";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);

            row = rptDataSet.Tables[0].NewRow();
            row["c11"] = "Invoice No";
            row["c12"] = "Variety";
            row["c13"] = "No Of Packages";
            row["c14"] = "Avg. Wt";
            row["c15"] = "Net Wt";
            row["c16"] = "Rate";
            row["c17"] = "Amount";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);

            foreach (DataRow row2 in InputDataSet.Tables[1].Rows)
            {
                row = rptDataSet.Tables[0].NewRow();
                row["C21"] = row2["invno"]; ;
                row["C22"] = row2["ProductDescription"];
                row["C23"] = row2["Qty"];
                row["C24"] = UType.MyFormat(row2["AvgWt"].ToString());
                row["C25"] = row2["NetWt"];
                row["C26"] = row2["Unitrate"];
                row["C27"] = row2["amount"];
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
            }
            row = rptDataSet.Tables[0].NewRow();
            row["c21"] = "----------";
            row["c22"] = "----------";
            row["c23"] = "----------";
            row["c24"] = "----------";
            row["c25"] = "----------";
            row["c26"] = "----------";
            row["c27"] = "----------";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);
            #endregion

            #region Available for Sale

            row = rptDataSet.Tables[0].NewRow();
            row["c11"] = "AVAILABLE FOR SALE";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);

            row = rptDataSet.Tables[0].NewRow();
            row["c11"] = "Invoice No";
            row["c12"] = "Variety";
            row["c13"] = "No Of Packages";
            row["c14"] = "Avg. Wt";
            row["c15"] = "Net Wt";
            row["c16"] = "Rate";
            row["c17"] = "Amount";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);

            foreach (DataRow row3 in InputDataSet.Tables[3].Rows)
            {
                row = rptDataSet.Tables[0].NewRow();
                //row["c21"] = "Total";
                row["c22"] = row3["productDescription"].ToString();
                row["c23"] = row3["Qty"].ToString();
                //row["c24"] = row3["AvgWt"].ToString();
                try
                {
                    row["C24"] = Convert.ToString(UType.MyFormat(UType.MyCtoD(row3["NetWt"].ToString()) / UType.MyCtoD(row3["Qty"].ToString())));
                }
                catch (Exception)
                {
                }
                row["C25"] = row3["NetWt"].ToString();
                //row["C26"] = row3["Rate"].ToString();
                try
                {
                    row["C26"] = Convert.ToString(UType.MyFormat(UType.MyCtoD(row3["UnitRate"].ToString()) / UType.MyCtoD(row3["CountUnitRate"].ToString())));
                }
                catch (Exception)
                {
                }

                row["C27"] = row3["Amount"].ToString();
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
            }
            row = rptDataSet.Tables[0].NewRow();
            row["c21"] = "----------";
            row["c22"] = "----------";
            row["c23"] = "----------";
            row["c24"] = "----------";
            row["c25"] = "----------";
            row["c26"] = "----------";
            row["c27"] = "----------";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);
            #endregion

            #region Sold

            row = rptDataSet.Tables[0].NewRow();
            row["c11"] = "SALES";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);

            row = rptDataSet.Tables[0].NewRow();
            row["c11"] = "Invoice No";
            row["c12"] = "Variety";
            row["c13"] = "No Of Packages";
            row["c14"] = "Avg. Wt";
            row["c15"] = "Net Wt";
            row["c16"] = "Rate";
            row["c17"] = "Amount";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);

            foreach (DataRow row5 in InputDataSet.Tables[2].Rows)
            {
                row = rptDataSet.Tables[0].NewRow();
                row["C21"] = row5["InvNo"].ToString();
                row["C22"] = row5["ProductDescription"];
                row["C23"] = row5["Qty"];
                try
                {
                    row["C24"] = Convert.ToString(UType.MyFormat(UType.MyFormat(UType.MyCtoD(row5["NetWt"].ToString()) / UType.MyCtoD(row5["Qty"].ToString()))));
                }
                catch (Exception)
                {
                }
                row["C25"] = row5["NetWt"];
                row["C26"] = row5["UnitRate"];
                row["C27"] = row5["Amount"];
                //try
                //{
                //    row["C26"] = Convert.ToString(UType.MyFormat(PurAmt / PurWt));
                //}
                //catch (Exception)
                //{
                //}
                //try
                //{
                //    row["C27"] = Convert.ToString(UType.MyFormat(UType.MyCtoD(row5["Qty"].ToString()) * PurAmt / PurWt));
                //}
                //catch (Exception)
                //{
                //}
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
            }
            row = rptDataSet.Tables[0].NewRow();
            row["c21"] = "----------";
            row["c22"] = "----------";
            row["c23"] = "----------";
            row["c24"] = "----------";
            row["c25"] = "----------";
            row["c26"] = "----------";
            row["c27"] = "----------";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);
            #endregion

            #region Closing Stock

            row = rptDataSet.Tables[0].NewRow();
            row["c11"] = "CLOSING STOCK";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);

            row = rptDataSet.Tables[0].NewRow();
            row["c11"] = "Invoice No";
            row["c12"] = "Variety";
            row["c13"] = "No Of Packages";
            row["c14"] = "Avg. Wt";
            row["c15"] = "Net Wt";
            row["c16"] = "Rate";
            row["c17"] = "Amount";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);


            foreach (DataRow row7 in InputDataSet.Tables[3].Rows)
            {
                row = rptDataSet.Tables[0].NewRow();
                //row["c21"] = "Total";
                string tProductId = row7["productId"].ToString();
                decimal ClosingQty = UType.MyCtoD(row7["Qty"].ToString());
                decimal ClosingNetWt = UType.MyCtoD(row7["NetWt"].ToString());
                decimal ClosingAmount = UType.MyCtoD(row7["Amount"].ToString());
                foreach (DataRow row71 in InputDataSet.Tables[4].Rows)
                {
                    if (tProductId == row71["ProductId"].ToString())
                    {
                        ClosingQty = ClosingQty - UType.MyCtoD(row71["Qty"].ToString());
                        ClosingNetWt = ClosingNetWt - UType.MyCtoD(row71["NetWt"].ToString());
                        ClosingAmount = ClosingAmount - UType.MyCtoD(row71["Amount"].ToString());
                    }
                }
                row["c22"] = row7["productDescription"].ToString();
                row["c23"] = ClosingQty.ToString(); // row3["TotalQty"].ToString();
                //row["c24"] = row3["AvgWt"].ToString();
                row["C25"] = ClosingNetWt.ToString(); //row3["NetWt"].ToString();
                //row["C26"] = row3["Rate"].ToString();
                row["C27"] = ClosingAmount.ToString(); //row3["Amount"].ToString();
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
            }
            row = rptDataSet.Tables[0].NewRow();
            row["c21"] = "----------";
            row["c22"] = "----------";
            row["c23"] = "----------";
            row["c24"] = "----------";
            row["c25"] = "----------";
            row["c26"] = "----------";
            row["c27"] = "----------";
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);
            #endregion



        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet MoveInInventoryReport(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal AvgCost = 0;
        DataRow row = null;
        decimal dBalance = UType.MyCtoD(InputDataSet.Tables[0].Rows[0]["ProductBalance"].ToString());
        try
        {
            foreach (DataRow row2 in InputDataSet.Tables[0].Rows)
            {
                row = rptDataSet.Tables[0].NewRow();
                row["c20"] = row2["ProductBalance"];
                row["c11"] = row2["productdescription"];
                row["c12"] = row2["vtype"];
                row["c15"] = row2["Quantity"];
                if (row["c12"].ToString() == "PV")
                {
                    dBalance = dBalance + UType.MyCtoD(row["c15"].ToString());
                }
                if (row["c12"].ToString() == "SV")
                {
                    dBalance = dBalance - UType.MyCtoD(row["c15"].ToString());
                }
                row["c13"] = row2["trandate"];
                row["c14"] = row2["narration"];

                row["c16"] = row2["unitrate"];
                row["c17"] = row2["itemamount"];
                row["c18"] = dBalance;
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
    private string GetTotalNetWt(DataSet ds)
    {
        decimal retVal = 0;
        foreach (DataRow row1 in ds.Tables[0].Rows)
        {
            retVal += UType.MyCtoD(row1["NetWt"].ToString());
        }
        return retVal.ToString();
    }

    public DataSet MoveInSalesR4(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        DataRow row = null;
        decimal TotalAmount = 0;
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                row = rptDataSet.Tables[0].NewRow();
                row["C1"] = row1["VoucherNo"].ToString();
                row["C2"] = row1["BlNo"];
                row["C3"] = row1["ConsigneeActdesc"].ToString();
                row["C4"] = GetTotalNetWt(InputDataSet) + "  KGS";
                row["C5"] = row1["ProductTypeDescription"].ToString();
                row["C11"] = row1["NetWt"].ToString();
                row["C12"] = row1["uomdescription"].ToString();
                row["C13"] = row1["productdescription"].ToString();
                row["C14"] = row1["Qty"].ToString();
                row["C15"] = row1["AvgWt"].ToString();
                row["C16"] = row1["UnitRate"].ToString();
                row["C17"] = row1["Amount"].ToString();
                row["C26"] = row1["TotSaleAmount"].ToString();
                row["CLogo"] = GetImage();
                TotalAmount += UType.MyCtoD(row1["Amount"].ToString());
                rptDataSet.Tables[0].Rows.Add(row);
            }
            foreach (DataRow row21 in rptDataSet.Tables[0].Rows)
            {
                row21["C25"] = TotalAmount.ToString();
                row21["C27"] = Convert.ToString(UType.MyCtoD(row21["C26"].ToString()) - TotalAmount);

            }
        }
        catch (Exception ex)
        {
            // throw (ex);
        }
        return rptDataSet;
    }

    private string GetTotalQty(DataTable dt1, string pProduct, string pConsignee)
    {
        decimal retVal = 0;
        foreach (DataRow row1 in dt1.Rows)
        {
            if (row1["ProductId"].ToString() == pProduct && row1["Consignee"].ToString() == pConsignee)
            {
                retVal += UType.MyCtoD(row1["Qty"].ToString());
            }
        }
        return retVal.ToString();

    }

    private string GetUomDescription(string pUom)
    {
        MyDb oMyDb = new MyDb(this);
        DataSet dsUom = oMyDb.GetFillComboUOM();

        string retVal = UType.GetUomDescription(dsUom.Tables[0], pUom);
        return retVal;
    }

    private string GetTypeDescription(string pType)
    {


        string retVal = string.Empty;

        MyMain oMy = new MyMain(Connection.SqlConnection);
        DataSet ds1 = oMy.GetProductType();


        foreach (DataRow row1 in ds1.Tables[0].Rows)
        {
            if (row1["producttypeid"].ToString() == pType)
            {
                retVal = row1["producttypedescription"].ToString();
            }
        }
        return retVal;
    }

    public DataSet MoveDataInReportDataSet(DataSet rptDataSet, DataSet InputDataSet)
    {
        try
        {
            DataRow row = null;
            DataRow row1 = InputDataSet.Tables[0].Rows[0];
            //DataRow row2 = InputDataSet.Tables[1].Rows[0];
            //foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            //{
            row = rptDataSet.Tables[0].NewRow();
            row["C1"] = row1["TranDate"];
            row["C2"] = row1["EmpName"];
            row["C3"] = row1["DesignationDescription"];
            row["C4"] = UType.GetDate(row1["StartDate"].ToString());
            row["C5"] = UType.GetDate(row1["EndDate"].ToString());
            row["C6"] = UType.GetDiffDays1(row1["StartDate"].ToString(), row1["EndDate"].ToString());
            row["C7"] = row1["LeavePurpose"];
            row["C8"] = "";

            //string FilePath=  "C:/Web/ERP/Images/Pral.jpg";
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "pral.Jpg";
            FileStream fs = null;
            if (File.Exists(FilePath))
            {
                fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                //fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "10157.Jpg", FileMode.Open); 
                BinaryReader br;
                br = new BinaryReader(fs);
                // define the byte array of filelength 
                byte[] imgbyte = new byte[fs.Length + 1];
                // read the bytes from the binary reader 
                imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                row["CLogo"] = imgbyte;
            }
            rptDataSet.Tables[0].Rows.Add(row);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet Old_MoveDataInReportDataSetInv1(DataSet rptDataSet, DataSet InputDataSet)
    {
        try
        {
            DataRow row = null;
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            //DataRow row2 = InputDataSet.Tables[1].Rows[0];
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                row = rptDataSet.Tables[0].NewRow();
                row["C1"] = UType.GetDate(row1["TranDate"].ToString());
                row["C2"] = row1["CustomerName"];
                row["C3"] = row1["ProductDescription"];
                row["C4"] = row1["ProductNo"];
                row["C5"] = row1["ProductUnitPrice"];
                row["C6"] = row1["TotalAmount"];
                string FilePath = AppDomain.CurrentDomain.BaseDirectory + "pral.Jpg";
                FileStream fs = null;
                if (File.Exists(FilePath))
                {
                    fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                    //fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "10157.Jpg", FileMode.Open); 
                    BinaryReader br;
                    br = new BinaryReader(fs);
                    // define the byte array of filelength 
                    byte[] imgbyte = new byte[fs.Length + 1];
                    // read the bytes from the binary reader 
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                    row["CLogo"] = imgbyte;
                }
                rptDataSet.Tables[0].Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet MoveDataInReportDataSetInv1(DataSet rptDataSet, DataSet InputDataSet)
    {
        decimal dAmount = 0;
        decimal dGSTRate = 0;
        decimal dAGSTRate = 0;

        decimal dGST = 0;
        decimal dAGST = 0;
        decimal dTotal = 0;
        try
        {
            DataRow row = null;
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            //DataRow row2 = InputDataSet.Tables[1].Rows[0];
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                row = rptDataSet.Tables[0].NewRow();
                row["C1"] = row1["DeliveryOrder"];  //UType.GetDate(row1["TranDate"].ToString());
                row["C2"] = row1["InvoiceNo"];
                row["C3"] = row1["ActDesc"];
                row["C4"] = row1["ActAddress"];
                row["C30"] = row1["ActPhone"];
                row["C31"] = row1["ActNTN"] + "--" + row1["ActSTR"];

                row["C5"] = "1700-24018-3412"; // row1["ProductUnitPrice"];
                row["C6"] = "2401834-1"; //row1["TotalAmount"];
                row["C7"] = row1["PurchaseOrder"];
                row["C8"] = UType.GetDate(row1["TranDate"].ToString());
                row["C11"] = row1["itemQuantity"];
                row["C12"] = row1["fQuantity"];
                row["C13"] = row1["ProductDescription"];
                row["C14"] = row1["size"];
                row["C15"] = row1["batch"];
                row["C16"] = UType.GetDate(row1["expiry"].ToString());
                row["C17"] = row1["MRP"];
                row["C18"] = row1["TP"];
                dAmount = dAmount + UType.MyCtoD(row1["ItemAmount"].ToString());
                dGSTRate = UType.MyCtoD(row1["ACTGST"].ToString());
                dAGSTRate = UType.MyCtoD(row1["ACTAdGST"].ToString());

                row["C19"] = row1["ItemAmount"];
                dGST = (dAmount * dGSTRate) / 100; //     (dAmount * 17) / 100;
                dAGST = (dAmount * dAGSTRate) / 100;
                dTotal = dTotal + dAmount;

                row["C21"] = Convert.ToString(dAmount);
                row["C22"] = Convert.ToString(dGST);
                row["C23"] = Convert.ToString(dTotal);
                row["C24"] = Convert.ToString(dAGST);

                string FilePath = AppDomain.CurrentDomain.BaseDirectory + "abc.Jpg";
                FileStream fs = null;
                if (File.Exists(FilePath))
                {
                    fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                    //fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "10157.Jpg", FileMode.Open); 
                    BinaryReader br;
                    br = new BinaryReader(fs);
                    // define the byte array of filelength 
                    byte[] imgbyte = new byte[fs.Length + 1];
                    // read the bytes from the binary reader 
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                    row["CLogo"] = imgbyte;
                }
                rptDataSet.Tables[0].Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet MoveDataInReportDataSetInv2(DataSet rptDataSet, DataSet InputDataSet)
    {
        try
        {
            DataRow row = null;
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            //DataRow row2 = InputDataSet.Tables[1].Rows[0];
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                row = rptDataSet.Tables[0].NewRow();
                row["C1"] = UType.GetDate(row1["TranDate"].ToString());
                row["C2"] = row1["CustomerName"];
                row["C3"] = row1["ProductDescription"];
                row["C4"] = row1["ProductNo"];
                row["C5"] = row1["ProductUnitPrice"];
                row["C6"] = row1["TotalAmount"];
                string FilePath = AppDomain.CurrentDomain.BaseDirectory + "pral.Jpg";
                FileStream fs = null;
                if (File.Exists(FilePath))
                {
                    fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                    //fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "10157.Jpg", FileMode.Open); 
                    BinaryReader br;
                    br = new BinaryReader(fs);
                    // define the byte array of filelength 
                    byte[] imgbyte = new byte[fs.Length + 1];
                    // read the bytes from the binary reader 
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                    row["CLogo"] = imgbyte;
                }
                rptDataSet.Tables[0].Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet MoveDataInReportSales(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = new DataSet();
        rptDataSet.ReadXmlSchema(UType.FileReportXsd);
        try
        {
            DataRow row = null;
            decimal TotalAmount = 0;
            decimal TotalQuantity = 0;
            string PrvName = string.Empty;
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            //DataRow row2 = InputDataSet.Tables[1].Rows[0];

            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                row = rptDataSet.Tables[0].NewRow();
                row["C1"] = "From " + UType.GetDate(sDate) + " To " + eDate;
                row["C2"] = "Product Name : " + row1["ProductDescription"].ToString();
                row["C3"] = "Unit Price " + row1["ProductUnitPrice"].ToString();
                row["C4"] = row1["CustomerName"];
                row["C8"] = UType.MyCtoD(row1["ProductUnitPrice"].ToString()) * UType.MyCtoD(row1["ProductNo"].ToString());
                TotalAmount += UType.MyCtoD(row1["TotalAmount"].ToString());
                TotalQuantity += UType.MyCtoD(row1["ProductNo"].ToString());
                foreach (DataRow row2 in InputDataSet.Tables[0].Rows)
                {
                    if (row1["CustomerName"].ToString().Trim() == row2["CustomerName"].ToString().Trim())
                    {

                        int pDay = UType.GetDay(row2["TranDate"].ToString());
                        switch (pDay)
                        {
                            case 1:
                                row["C11"] = row2["ProductNo"];
                                break;
                            case 2:
                                row["C12"] = row2["ProductNo"];
                                break;
                            case 3:
                                row["C13"] = row2["ProductNo"];
                                break;
                            case 4:
                                row["C14"] = row2["ProductNo"];
                                break;
                            case 5:
                                row["C15"] = row2["ProductNo"];
                                break;
                            case 6:
                                row["C16"] = row2["ProductNo"];
                                break;
                            case 7:
                                row["C17"] = row2["ProductNo"];
                                break;
                            case 8:
                                row["C18"] = row1["ProductNo"];
                                break;
                            case 9:
                                row["C19"] = row2["ProductNo"];
                                break;
                            case 10:
                                row["C20"] = row2["ProductNo"];
                                break;
                            case 11:
                                row["C21"] = row2["ProductNo"];
                                break;
                            case 12:
                                row["C22"] = row2["ProductNo"];
                                break;
                            case 13:
                                row["C23"] = row2["ProductNo"];
                                break;
                            case 14:
                                row["C24"] = row2["ProductNo"];
                                break;
                            case 15:
                                row["C25"] = row2["ProductNo"];
                                break;
                            case 16:
                                row["C26"] = row2["ProductNo"];
                                break;
                            case 17:
                                row["C27"] = row2["ProductNo"];
                                break;
                            case 18:
                                row["C28"] = row2["ProductNo"];
                                break;
                            case 19:
                                row["C29"] = row2["ProductNo"];
                                break;
                            case 20:
                                row["C30"] = row2["ProductNo"];
                                break;
                            case 21:
                                row["C31"] = row2["ProductNo"];
                                break;
                            case 22:
                                row["C32"] = row2["ProductNo"];
                                break;
                            case 23:
                                row["C33"] = row2["ProductNo"];
                                break;
                            case 24:
                                row["C34"] = row2["ProductNo"];
                                break;
                            case 25:
                                row["C35"] = row2["ProductNo"];
                                break;
                            case 26:
                                row["C36"] = row2["ProductNo"];
                                break;
                            case 27:
                                row["C37"] = row2["ProductNo"];
                                break;
                            case 28:
                                row["C38"] = row2["ProductNo"];
                                break;
                            case 29:
                                row["C39"] = row2["ProductNo"];
                                break;
                            case 30:
                                row["C40"] = row2["ProductNo"];
                                break;
                            case 31:
                                row["C51"] = row2["ProductNo"];
                                break;
                        }
                    }
                }
                #region Logo
                string FilePath = AppDomain.CurrentDomain.BaseDirectory + "pral.Jpg";
                FileStream fs = null;
                if (File.Exists(FilePath))
                {
                    fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                    //fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "10157.Jpg", FileMode.Open); 
                    BinaryReader br;
                    br = new BinaryReader(fs);
                    // define the byte array of filelength 
                    byte[] imgbyte = new byte[fs.Length + 1];
                    // read the bytes from the binary reader 
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                    row["CLogo"] = imgbyte;
                }
                #endregion

                if (PrvName != row1["CustomerName"].ToString())
                {
                    PrvName = row1["CustomerName"].ToString();
                    rptDataSet.Tables[0].Rows.Add(row);
                }
            }
            foreach (DataRow row3 in rptDataSet.Tables[0].Rows)
            {
                row3["C52"] = TotalQuantity.ToString();
                row3["C53"] = TotalAmount.ToString();
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
        if (UType.IsWeb() == false)
        {

            MyDb oMyDb = new MyDb(this._SqlConnection, this);
            DataSet result = oMyDb.GetImage();
            if (result.Tables[0].Rows.Count > 0)
            {
                imgbyte = (byte[])result.Tables[0].Rows[0]["Fld4"];
            }
        }
        if (UType.IsWebImage())
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "logo.Jpg";
            FileStream fs = null;
            if (File.Exists(FilePath))
            {
                fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                BinaryReader br;
                br = new BinaryReader(fs);
                imgbyte = new byte[fs.Length + 1];
                imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
            }
        }
        return imgbyte;
    }

    public byte[] GetImageBLRpt(string ImgID)
    {
        byte[] imgbyte = null;
        if (ImgID == "0")
        {
            ImgID = "307";
        }
        //string FilePath = AppDomain.CurrentDomain.BaseDirectory + "/Images/AxlBLRpt.png";
        string FilePath = AppDomain.CurrentDomain.BaseDirectory + "/Images/Line/" + ImgID + ".png";
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

    public byte[] GetImageOld()
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

    public DataSet MoveInRptDsChartOfAccount(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                //row["C1"] = row1["OfficeDescription"];
                // row["C2"] = row1["ProjectDescription"];
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

    public DataSet MoveInRptDsVoucher(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            decimal TotalAmt = 0;
            decimal TotalDr = 0;
            decimal TotalCr = 0;
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                //row["C1"] = "ABC";
                row["C2"] = "Voucher";
                row["C11"] = row1["vtype"].ToString() + "-" + row1["VNo"] + " / " + UType.GetDateYear(row1["TranDate"].ToString());
                row["C12"] = row1["vtypedescription"].ToString() + " Voucher";
                row["C13"] = UType.GetDateTxt(row1["TranDate"].ToString());
                row["C14"] = row1["ChqNo"];

                row["C15"] = UType.GetDateTxt(row1["ChqDate"].ToString());
                row["C16"] = row1["ActCode1"];
                row["C17"] = row1["ActName"];
                if (row1["AmountSts"].ToString() == "D")
                {
                    row["C18"] = UType.MyFormat(row1["Amount"].ToString());
                    TotalDr += UType.MyCtoD(row1["Amount"].ToString());
                }
                if (row1["AmountSts"].ToString() == "C")
                {
                    row["C19"] = UType.MyFormat(row1["Amount"].ToString());
                    TotalCr += UType.MyCtoD(row1["Amount"].ToString());
                }
                row["C20"] = row1["Narration"];
                //TotalAmt += UType.MyCtoD(row1["Amount"].ToString());
                row["C22"] = TotalDr.ToString();
                row["C23"] = TotalDr.ToString();
                row["C25"] = UType.NumberToWords(TotalDr);

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

    public DataSet MoveInRptDsChallan(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            decimal TotalAmt = 0;
            decimal TotalDr = 0;
            decimal TotalCr = 0;
            int Ctr = 1;
            string PartyName = "";
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                //row["C1"] = "ABC";
                //row["C2"] = "Voucher";
                row["C3"] = row1["VNo"];
                row["C4"] = UType.GetDate1(row1["TranDate"].ToString());
                row["C11"] = row1["ChqNo"];
                if (row1["ActCode"].ToString().Substring(0, 6) == "100104")
                {
                    //row["C5"] = row1["ActDesc"].ToString();
                    PartyName = row1["ActDesc"].ToString();
                }
                row["C11"] = row1["Narration"];  //UType.GetDate1(row1["ChqDate"].ToString());
                row["C7"] = "";//row1["ActCode"];
                row["C8"] = row1["VNo"];  //row1["ActDesc"];

                row["C9"] = Ctr;
                row["C10"] = row1["Narration"];
                //row["C21"] = row1["ActDesc"].ToString();
                if (row1["ActCode"].ToString().Substring(0, 2) == "30")
                {
                    row["C21"] = row1["ActDesc"].ToString();
                }
                row["C22"] = UType.MyFormat(row1["Quantity"].ToString());
                row["C23"] = UType.MyFormat(row1["UnitRate"].ToString());
                row["C24"] = UType.MyFormat(row1["height"].ToString());
                row["C25"] = UType.MyFormat(row1["Width"].ToString());

                row["CLogo"] = GetImage();
                //100104
                switch (row1["vtypeid"].ToString())
                {
                    case "6":
                    //if (row1["ActCode"].ToString().Substring(0, 2) == "30")
                    //{
                    //    //if (row1["Status"].ToString() == "D")
                    //    //{
                    //    //    row["C26"] = UType.MyFormat(row1["Amount"].ToString());
                    //    //    TotalDr += UType.MyCtoD(row1["Amount"].ToString());
                    //    //}
                    //    //if (row1["Status"].ToString() == "C")
                    //    //{
                    //        row["C27"] = UType.MyFormat(row1["Amount"].ToString());
                    //        TotalCr += UType.MyCtoD(row1["Amount"].ToString());
                    //    //}
                    //    row["C21"] = row1["ActDesc"].ToString();
                    //    row["C5"] = PartyName;

                    //    rptDataSet.Tables[0].Rows.Add(row);
                    //}
                    //break;
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "8":
                    case "7":
                        // if (row1["ActCode"].ToString().Substring(0, 6) == "100101")
                        // {
                        if (row1["Status"].ToString() == "D")
                        {
                            row["C26"] = UType.MyFormat(row1["Amount"].ToString());
                            TotalDr += UType.MyCtoD(row1["Amount"].ToString());
                        }
                        if (row1["Status"].ToString() == "C")
                        {
                            row["C27"] = UType.MyFormat(row1["Amount"].ToString());
                            TotalCr += UType.MyCtoD(row1["Amount"].ToString());
                        }
                        row["C21"] = row1["ActDesc"].ToString();
                        row["C5"] = PartyName;
                        rptDataSet.Tables[0].Rows.Add(row);
                        //}
                        break;


                }

                Ctr++;
            }
            foreach (DataRow row11 in rptDataSet.Tables[0].Rows)
            {
                row11["C5"] = PartyName;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet MoveInRptDsChallanAdd(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            decimal TotalAmt = 0;
            decimal TotalDr = 0;
            decimal TotalCr = 0;
            int Ctr = 1;
            string PartyName = "";
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                //row["C1"] = "ABC";
                //row["C2"] = "Voucher";
                row["C3"] = "Voucher No : " + row1["VNo"];
                row["C4"] = "Voucher Date : " + UType.GetDate1(row1["TranDate"].ToString());
                row["C4"] = "Voucher Type : " + row1["vtypeid"].ToString();
                row["C11"] = row1["ChqNo"];
                if (row1["ActCode"].ToString().Substring(0, 6) == "100104")
                {
                    //row["C5"] = row1["ActDesc"].ToString();
                    PartyName = row1["ActDesc"].ToString();
                }
                row["C11"] = row1["Narration"];  //UType.GetDate1(row1["ChqDate"].ToString());
                row["C7"] = "";//row1["ActCode"];
                row["C8"] = row1["VNo"];  //row1["ActDesc"];

                row["C9"] = Ctr;
                row["C10"] = row1["Narration"];
                //row["C21"] = row1["ActDesc"].ToString();
                if (row1["ActCode"].ToString().Substring(0, 2) == "30")
                {
                    row["C21"] = row1["ActDesc"].ToString();
                }
                row["C22"] = UType.MyFormat(row1["Quantity"].ToString());
                row["C23"] = UType.MyFormat(row1["UnitRate"].ToString());
                row["C24"] = UType.MyFormat(row1["height"].ToString());
                row["C25"] = UType.MyFormat(row1["Width"].ToString());

                row["CLogo"] = GetImage();
                //100104
                switch (row1["vtypeid"].ToString())
                {
                    case "6":
                    //if (row1["ActCode"].ToString().Substring(0, 2) == "30")
                    //{
                    //    //if (row1["Status"].ToString() == "D")
                    //    //{
                    //    //    row["C26"] = UType.MyFormat(row1["Amount"].ToString());
                    //    //    TotalDr += UType.MyCtoD(row1["Amount"].ToString());
                    //    //}
                    //    //if (row1["Status"].ToString() == "C")
                    //    //{
                    //        row["C27"] = UType.MyFormat(row1["Amount"].ToString());
                    //        TotalCr += UType.MyCtoD(row1["Amount"].ToString());
                    //    //}
                    //    row["C21"] = row1["ActDesc"].ToString();
                    //    row["C5"] = PartyName;

                    //    rptDataSet.Tables[0].Rows.Add(row);
                    //}
                    //break;
                    case "1":
                    case "2":
                    case "4":
                        // if (row1["ActCode"].ToString().Substring(0, 6) == "100101")
                        // {
                        if (row1["Status"].ToString() == "D")
                        {
                            row["C26"] = UType.MyFormat(row1["Amount"].ToString());
                            TotalDr += UType.MyCtoD(row1["Amount"].ToString());
                        }
                        if (row1["Status"].ToString() == "C")
                        {
                            row["C27"] = UType.MyFormat(row1["Amount"].ToString());
                            TotalCr += UType.MyCtoD(row1["Amount"].ToString());
                        }
                        row["C21"] = row1["ActDesc"].ToString();
                        row["C5"] = PartyName;
                        rptDataSet.Tables[0].Rows.Add(row);
                        //}
                        break;


                }

                Ctr++;
            }
            foreach (DataRow row11 in rptDataSet.Tables[0].Rows)
            {
                row11["C5"] = PartyName;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }
    private string GetColorName(Int16 ColorID)
    {
        string retVal = "";

        switch (ColorID)
        {
            case 1:
                retVal = "Film";
                break;
            case 2:
                retVal = "Plates";
                break;
        }
        return retVal;
    }
    public DataSet MoveInRptDsVoucherP(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset1();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            decimal TotalAmt = 0;
            decimal TotalDr = 0;
            decimal TotalCr = 0;
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                //row["C1"] = "ABC";
                row["C2"] = "Voucher";
                row["C3"] = row1["VNo"];
                row["C4"] = UType.GetDate1(row1["TranDate"].ToString());
                row["C5"] = row1["ChqNo"];
                row["C6"] = UType.GetDate1(row1["ChqDate"].ToString());
                row["C7"] = row1["ActCode"];
                row["C8"] = row1["ActDesc"];
                if (row1["Status"].ToString() == "D")
                {
                    row["C9"] = UType.MyFormat(row1["Amount"].ToString());
                    TotalDr += UType.MyCtoD(row1["Amount"].ToString());
                }
                if (row1["Status"].ToString() == "C")
                {
                    row["C11"] = UType.MyFormat(row1["Amount"].ToString());
                    TotalCr += UType.MyCtoD(row1["Amount"].ToString());
                }
                row["C10"] = row1["Narration"];
                //TotalAmt += UType.MyCtoD(row1["Amount"].ToString());
                row["C12"] = TotalDr.ToString();
                row["C15"] = TotalDr.ToString();
                row["C16"] = TotalCr.ToString();
                row["C17"] = row1["vtypedescription"].ToString();
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
            }
            int Ctr = rptDataSet.Tables[0].Rows.Count;
            int Ctr1 = 1;
            //Inventory Data
            foreach (DataRow row11 in InputDataSet.Tables[1].Rows)
            {
                DataRow row = rptDataSet.Tables[1].NewRow();
                row["C1"] = row11["productdescription"];
                row["C2"] = row11["Qty"];
                row["C3"] = row11["AvgWt"];
                row["C4"] = row11["NetWt"];
                row["C5"] = row11["UnitRate"];
                row["C6"] = row11["Amount"];
                rptDataSet.Tables[1].Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }
    private string GetCurrencyReport(DataSet ds1)
    {
        string retVal = "";
        Fld1 = ds1.Tables[0].Rows[0]["Currency"].ToString();
        DataSet dsCur = GetCurrency();

        if (dsCur.Tables[0].Rows.Count > 0)
        {
            retVal = dsCur.Tables[0].Rows[0]["Currency_abb"].ToString();
        }
        return retVal;

    }
    private string GetActChartCodeReport(DataSet ds1)
    {
        string retVal = "";
        Fld1 = ds1.Tables[0].Rows[0]["officeid"].ToString();
        Fld2 = ds1.Tables[0].Rows[0]["Projectid"].ToString();
        Fld3 = ds1.Tables[0].Rows[0]["client"].ToString();
        DataSet dsCur = GetActChartCode();

        if (dsCur.Tables[0].Rows.Count > 0)
        {
            retVal = dsCur.Tables[0].Rows[0]["ActDesc"].ToString();
        }
        return retVal;

    }
    private string GetCurrencyString(string Str1)
    {
        string retVal = "";
        Fld1 = Str1;
        DataSet dsCur = GetCurrency();

        if (dsCur.Tables[0].Rows.Count > 0)
        {
            retVal = dsCur.Tables[0].Rows[0]["Currency_abb"].ToString();
        }
        return retVal;

    }


    public DataSet MoveInRptDsPayVoucher(DataSet InputDataSet, string Customer)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            DataRow row1 = InputDataSet.Tables[0].Rows[0];
            DataRow rowMBL = InputDataSet.Tables["TblMBL"].Rows[0];
            DataRow rowCh = InputDataSet.Tables["TblCh"].Rows[0];
            decimal TotalLocalAmount = 0; decimal TotalTaxAmount = 0; decimal TotalNetAmount = 0;
            foreach (DataRow row22 in InputDataSet.Tables["TblCh"].Rows)
            {
                TotalLocalAmount = TotalLocalAmount + UType.MyCtoD(row22["LocalAmount"].ToString());
                TotalTaxAmount = TotalTaxAmount + UType.MyCtoD(row22["TaxAmount"].ToString());
                TotalNetAmount = TotalNetAmount + UType.MyCtoD(row22["NetAmount"].ToString());
            }
            foreach (DataRow row11 in InputDataSet.Tables["TblEq"].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C31"] = row11["SNo"];
                row["C5"] = rowMBL["MBLNo"].ToString() + " / " + rowMBL["HBLNo"].ToString();
                row["C6"] = row1["Jobno"];
                row["C7"] = row11["ContainerNo"] + " / " + GetDescriptionDDL(row11["sizentype"].ToString());
                row["C8"] = row1["vessel"].ToString();
                row["C10"] = Convert.ToString(TotalLocalAmount);
                row["C15"] = UType.NumberToWords(TotalLocalAmount);

                #region Consignee
                MyMain oMy = new MyMain();
                try
                {
                    DataSet dsConsignee = null;
                    MyMain oMyConsignee = new MyMain();
                    oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                    oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                    oMyConsignee.Fld3 = row1["Consignee"].ToString();

                    dsConsignee = oMyConsignee.GetAccountOneConsignee();
                    if (dsConsignee != null)
                    {
                        row["C18"] = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                        oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                        oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                        oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    }

                }
                catch (Exception ex)
                {


                }
                #endregion

                #region shipper
                oMy = new MyMain();
                try
                {
                    DataSet dsConsignee = null;
                    MyMain oMyConsignee = new MyMain();
                    oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                    oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                    oMyConsignee.Fld3 = rowCh["customer"].ToString();

                    dsConsignee = oMyConsignee.GetAccountOneConsignee();
                    if (dsConsignee != null)
                    {
                        row["C19"] = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                        oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                        oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                        oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    }

                }
                catch (Exception ex)
                {


                }
                #endregion

                row["C20"] = rowCh["Currency"].ToString();
                row["C21"] = rowCh["ExRate"];
                row["C29"] = Convert.ToString(TotalTaxAmount);
                row["C33"] = Convert.ToString(TotalNetAmount);

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
    private string GetActCity(string ActCode)
    {
        string retVal = "";
        try
        {
            DataSet dsResult = null;
            MyMain oMy = new MyMain();


            oMy.Fld1 = ActCode;

            dsResult = oMy.GetCityData();

            if (dsResult != null)
            {

                retVal = dsResult.Tables[0].Rows[0]["City_Short"].ToString();
            }

        }
        catch (Exception)
        {

            retVal = "";
        }
        return retVal;
    }

    private string GetCity_Name(string ActCode)
    {
        string retVal = "";
        try
        {
            DataSet dsResult = null;
            MyMain oMy = new MyMain();


            oMy.Fld1 = ActCode;

            dsResult = oMy.GetCityData();

            if (dsResult != null)
            {

                retVal = dsResult.Tables[0].Rows[0]["City_Name"].ToString();
            }

        }
        catch (Exception)
        {

            retVal = "";
        }
        return retVal;
    }
    public string GetActCity1(string ActCode)
    {
        string retVal = "";
        try
        {
            DataSet dsResult = null;
            MyMain oMy = new MyMain();


            oMy.Fld1 = ActCode;

            dsResult = oMy.GetCityData();

            if (dsResult != null)
            {

                retVal = dsResult.Tables[0].Rows[0]["City_Name"].ToString();
            }

        }
        catch (Exception)
        {

            retVal = "";
        }
        return retVal;
    }
    public string GetActCity2(string ActCode)
    {
        string retVal = "";
        try
        {
            DataSet dsResult = null;
            MyMain oMy = new MyMain();


            oMy.Fld1 = ActCode;

            dsResult = oMy.GetCityData();

            if (dsResult != null)
            {

                retVal = dsResult.Tables[0].Rows[0]["City_Short"].ToString();
            }

        }
        catch (Exception)
        {

            retVal = "";
        }
        return retVal;
    }
    public DataSet MoveInJobBalanceReport(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal SnoCtr = 1;
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[1].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();

                row["C4"] = row1["ActDesc"].ToString();
                rptDataSet.Tables[0].Rows.Add(row);
                SnoCtr = SnoCtr + 1;

            }
        }
        catch (Exception ex)
        {

        }
        return rptDataSet;
    }

    public DataSet MoveInRptDsJobPaymentVoucherBk(DataSet InputDataSet, string Customer)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row1 = null;
            DataRow rowMBL = null;
            row1 = InputDataSet.Tables[0].Rows[0];
            string ArrivalDate = UType.GetDateTxt(row1["ArrivalDate"].ToString());  //GetActCity1(row1["PortLoading"].ToString());
            string Portofloading = GetActCity1(row1["Portloading"].ToString());
            string PortDischarge = GetActCity1(row1["PortDischarge"].ToString());
            string FinalDestination = GetActCity1(row1["FinalDestination"].ToString());
            string ShippingCompanyID = GetDescriptionDDL(row1["ShippingCompanyID"].ToString());
            string Consignee = "";
            string ShipperName = "";
            string ConsigneeName = "";
            #region shipper
            MyMain oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["customer"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ShipperName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion
            #region Consignee
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["Consignee"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ConsigneeName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            { }
            #endregion



            foreach (DataRow rowCh in InputDataSet.Tables[1].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                #region Accountexpense
                oMy = new MyMain();
                try
                {

                    row["C19"] = ArrivalDate;
                    row["C20"] = row1["Currency"].ToString();
                    row["C21"] = row1["ExRate"].ToString();
                    row["C22"] = row1["sno"].ToString();
                    row["C23"] = rowCh["billinvoice"].ToString(); //Portofloading; //  GetActCity1(row1["Portofloading"].ToString());
                    row["C24"] = PortDischarge; //ShipperName; //   PortDischarge; // GetActCity1(row1["PortDischarge"].ToString());
                    row["C51"] = FinalDestination; // GetActCity1(row1["FinalDestination"].ToString());
                    row["C52"] = ShippingCompanyID; // GetDescriptionDDL(row1["ShippingCompanyID"].ToString());


                    row["C25"] = row1["volume"].ToString();

                    DataSet dsConsignee = null;
                    MyMain oMyAccountexpense = new MyMain();
                    oMyAccountexpense.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                    oMyAccountexpense.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                    oMyAccountexpense.Fld3 = rowCh["particular"].ToString();

                    dsConsignee = oMyAccountexpense.GetAccountExpense1();
                    if (dsConsignee != null)
                    {
                        row["C4"] = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                        //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                        //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                        // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    }

                }
                catch (Exception ex)
                { }
                #endregion




                //qq
                row["C17"] = InputDataSet.Tables["TblSmry"].Rows[0]["ChequeNo"].ToString(); // dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                row["C18"] = UType.GetDateTxt(InputDataSet.Tables["TblSmry"].Rows[0]["Chequedate"].ToString()); // dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                row["C2"] = rowCh["SNo"];
                row["C3"] = rowCh["sno"].ToString();
                // row["C4"] = rowCh["charges"].ToString();  //Consignee; // dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                // row["C4"] = ""; //dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //rowCh["charges"].ToString();

                row["C5"] = rowCh["Quantity"];
                row["C6"] = rowCh["rate"];
                row["C7"] = GetCurrencyString(rowCh["currency"].ToString());
                row["C8"] = rowCh["exRate"];
                row["C9"] = rowCh["NetAmount"]; // GetDescriptionDDL(row1["ShippingLineID"].ToString());
                row["C10"] = rowCh["LocalAmount"];
                // row["C11"] = row2["LocalAmount"];
                //qq
                //header
                row["C11"] = "JP" + rowCh["paymentid"].ToString() + rowCh["paymentidyear"].ToString();   //GetActCity(row1["PortDischarge"].ToString());
                //row["C12"] = rowCh["billinvoice"].ToString();
                row["C13"] = UType.GetDateTxt(row1["jobDate"].ToString());

                row["C14"] = ShipperName; //GetCurrencyString(rowCh["Currency"].ToString());
                row["C15"] = rowCh["ExRate"].ToString();

                row["C16"] = "";




                try
                {
                    //rowMBL = InputDataSet.Tables["TblMBL"].Rows[0];
                    row["C25"] = row1["MBLNo"].ToString(); //rowMBL["MBLNo"].ToString();
                    row["C25"] = row[25] + row1["HBLNo"].ToString(); //rowMBL["HBLNo"].ToString();
                }
                catch (Exception ex)
                { }



                row["C26"] = row1["jobno"].ToString();

                row["C27"] = "";
                row["C28"] = "";
                row["C29"] = rowCh["localAmount"].ToString();
                //row["C29"] = row1["Measurement"];
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);

            }


            decimal TotalTaxAmount = 0; decimal TotalDiscountAmount = 0; decimal TotalNetAmount = 0; decimal TotalLocalAmount = 0;

            foreach (DataRow row11 in InputDataSet.Tables["TblCH"].Rows)
            {
                TotalTaxAmount += UType.MyCtoD(row11["TaxAmount"].ToString());
                TotalNetAmount += UType.MyCtoD(row11["NetAmount"].ToString());
                TotalLocalAmount += UType.MyCtoD(row11["LocalAmount"].ToString());
                TotalNetAmount += UType.MyCtoD(row11["NetAmount"].ToString());
                TotalDiscountAmount += UType.MyCtoD(row11["discount"].ToString());

            }

            decimal TotalWt = 0; decimal TotalPCs = 0;

            foreach (DataRow row11 in InputDataSet.Tables["TblEq"].Rows)
            {
                TotalWt += UType.MyCtoD(row11["GrossWt"].ToString());
                TotalWt += UType.MyCtoD(row11["package"].ToString());

            }
            foreach (DataRow row11 in rptDataSet.Tables[0].Rows)
            {
                row11["C30"] = UType.MyFormat(TotalTaxAmount.ToString());

                row11["C26"] = UType.MyFormat(TotalWt.ToString());
                row11["C27"] = UType.MyFormat(TotalWt.ToString());
                row11["C29"] = UType.MyFormat(TotalDiscountAmount.ToString());

                row11["C31"] = UType.MyFormat(TotalNetAmount.ToString());
                row11["C15"] = UType.MyFormat(UType.MyCtoD(row11["C29"].ToString()));
                row11["C33"] = UType.NumberToWords(UType.MyCtoD(row11["C29"].ToString()));
            }


        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet MoveInRptDsJobPaymentVoucher(DataSet InputDataSet, string Customer)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row11 = InputDataSet.Tables[0].Rows[0]; DataRow rowCh = InputDataSet.Tables["TblCh"].Rows[0];
            DataRow row2 = InputDataSet.Tables["TblSmry"].Rows[0]; DataRow row3 = InputDataSet.Tables["TblMBL"].Rows[0];
            DataRow rowAct = InputDataSet.Tables["TblActTran"].Rows[0];
            string ArrivalDate = UType.GetDateTxt(row11["ArrivalDate"].ToString());  //GetActCity1(row1["PortLoading"].ToString());
            string Portofloading = GetActCity1(row11["Portloading"].ToString());
            string PortDischarge = GetActCity1(row11["PortDischarge"].ToString());
            string FinalDestination = GetActCity(row11["FinalDestination"].ToString());
            string ShippingCompanyID = GetDescriptionDDL(row11["ShippingCompanyID"].ToString());
            string Consignee = "";
            string ShipperName = "";
            string ConsigneeName = "";
            decimal LocalAmount = 0; decimal NetAmount = 0; decimal TotalLocalAmount = 0;
            #region ConsigneeName
            MyMain oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row11["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row11["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = InputDataSet.Tables[1].Rows[0]["customer"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ConsigneeName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion
            //
            foreach (DataRow rowCh1 in InputDataSet.Tables["TblCh"].Rows)
            {
                if (rowAct["actcode"].ToString() == Customer)
                {
                    LocalAmount = LocalAmount + UType.MyCtoD(rowCh1["localamount"].ToString());
                    NetAmount = NetAmount + UType.MyCtoD(rowCh1["NetAmount"].ToString());
                }
            }
            int SnoCtr = 1;

            //foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            try
            {
                DataRow row1 = InputDataSet.Tables[0].Rows[0];

                //{
                DataRow row = rptDataSet.Tables[0].NewRow();

                oMy = new MyMain();
                try
                {
                    row["C11"] = "JP " + rowCh["paymentid"].ToString() + rowCh["paymentidyear"].ToString();
                    row["C12"] = rowCh["BillInvoice"].ToString();
                    row["C13"] = UType.GetDateTxt(row1["jobDate"].ToString());
                    row["C14"] = ConsigneeName;
                    row["C15"] = UType.NumberToWords(LocalAmount);

                    try
                    {
                        row["C17"] = rowAct["ChqNo"].ToString();
                        row["C18"] = UType.GetDateTxt(rowAct["ChqDate"].ToString());
                    }
                    catch (Exception ex)
                    {

                    }
                    if (row["C17"].ToString().Length > 0)
                    {
                        row["C16"] = "Cheque";
                    }
                    row["C19"] = InputDataSet.Tables[1].Rows[0]["customer"].ToString();
                    row["C20"] = GetCurrencyString(rowCh["currency"].ToString());
                    row["C21"] = rowCh["ExRate"].ToString();
                    row["C22"] = Convert.ToString(SnoCtr); //rowCh["sno"].ToString();
                    row["C23"] = rowCh["BillInvoice"].ToString();
                    row["C24"] = UType.GetDateTxt(row1["jobDate"].ToString());
                    row["C25"] = row3["MBLNo"].ToString(); //rowMBL["MBLNo"].ToString();
                    row["C25"] = row["C25"].ToString() + " " + row3["HBLno"].ToString(); //rowMBL["HBLNo"].ToString();
                    row["C26"] = rowCh["jobno"].ToString();
                    row["C27"] = rowCh["Quantity"].ToString();
                    row["C28"] = row3["Vessel"].ToString() + " ";
                    if (row3["Vessel"].ToString().Length > 0)
                    {
                        row["C28"] = row["C28"].ToString() + " / " + row3["Voyage"].ToString();
                    }
                    row["C29"] = UType.MyFormat(LocalAmount); //rowCh["localamount"].ToString();
                    TotalLocalAmount = TotalLocalAmount + LocalAmount;
                    row["C34"] = UType.MyFormat(TotalLocalAmount); //rowCh["localamount"].ToString();
                    row["C35"] = row2["RemarksPay"].ToString(); ;
                    row["CLogo"] = GetImage();
                    if (rowAct["actcode"].ToString() == Customer)
                    {

                        //TotalLocalAmount = TotalLocalAmount + LocalAmount;
                        row["C34"] = UType.MyFormat(TotalLocalAmount);
                        rptDataSet.Tables[0].Rows.Add(row);
                    }
                    SnoCtr = SnoCtr + 1;
                }

                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            catch (Exception)
            {

                //throw;
            }
            //}
            //

        }
        catch (Exception ex)
        {
            // throw (ex);
        }
        return rptDataSet;
    }



    public DataSet MoveInRptDsJobRecinfo(DataSet InputDataSet, string Customer)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        DataRow row1 = null; DataRow rowMBL = null;
        string Continers = "";
        try
        {
            // DataRow row1 = InputDataSet.Tables[0].Rows[0];
            foreach (DataRow rowEq1 in InputDataSet.Tables["TblEq"].Rows)
            {
                Continers = Continers + " " + rowEq1["ContainerNo"];
            }

            try
            {
                row1 = InputDataSet.Tables[0].Rows[0];
            }
            catch (Exception ex)
            { }
            try
            {
                rowMBL = InputDataSet.Tables["TblMBL"].Rows[0];
            }
            catch (Exception ex)
            { }
            int SnoCtr = 1;
            foreach (DataRow rowCh in InputDataSet.Tables[1].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                //qq
                row["C3"] = Convert.ToString(SnoCtr); //;rowCh["SNo"];
                row["C4"] = rowCh["adddate"];
                row["C5"] = rowCh["BillInvoice"];
                #region Accountexpense
                MyMain oMy = new MyMain();
                try
                {
                    DataSet dsConsignee = null;
                    MyMain oMyAccountexpense = new MyMain();
                    oMyAccountexpense.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                    oMyAccountexpense.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                    oMyAccountexpense.Fld3 = rowCh["particular"].ToString();

                    dsConsignee = oMyAccountexpense.GetAccountExpense1();
                    if (dsConsignee != null)
                    {
                        row["C6"] = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                        oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                        oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                        oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    }

                }
                catch (Exception ex)
                {


                }
                #endregion
                //row["C6"] = rowCh["Charges"];
                //row["C5"] = rowCh["Charges"];
                row["C7"] = rowCh["Quantity"];
                row["C85"] = rowCh["rate"];
                row["C8"] = GetCurrencyString(rowCh["currency"].ToString());
                row["C9"] = rowCh["exRate"];
                row["C10"] = rowCh["NetAmount"]; // GetDescriptionDDL(row1["ShippingLineID"].ToString());
                row["C33"] = rowCh["discount"];
                row["C34"] = rowCh["LocalAmount"];
                //qq
                //header
                #region Consignee
                oMy = new MyMain();
                try
                {
                    DataSet dsConsignee = null;
                    MyMain oMyConsignee = new MyMain();
                    oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                    oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                    oMyConsignee.Fld3 = Customer;//row1["Consignee"].ToString();

                    dsConsignee = oMyConsignee.GetAccountOneConsignee();
                    if (dsConsignee != null)
                    {
                        row["C11"] = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                        oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                        oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                        oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    }

                }
                catch (Exception ex)
                {


                }
                #endregion
                #region shipper
                oMy = new MyMain();
                try
                {
                    DataSet dsConsignee = null;
                    MyMain oMyConsignee = new MyMain();
                    oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                    oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                    oMyConsignee.Fld3 = row1["shipper"].ToString();

                    dsConsignee = oMyConsignee.GetAccountOneConsignee();
                    if (dsConsignee != null)
                    {
                        row["C12"] = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                        oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                        oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                        oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    }

                }
                catch (Exception ex)
                {


                }
                #endregion

                row["C13"] = " "; // UType.GetDateTxt(row1["jobDate"].ToString());

                row["C14"] = " "; //GetCurrencyString(rowCh["Currency"].ToString());
                try
                {
                    row["C15"] = row1["MBLNo"].ToString();// +"  "+ rowMBL["HBLNo"].ToString();
                }
                catch (Exception ex)
                { }

                try
                {
                    row["C16"] = row1["jobno"];
                    row["C17"] = row1["Vessel"].ToString();
                    if (row1["voyage1"].ToString().Length > 0)
                    {
                        row["C17"] = row["C17"] + "/ " + row1["voyage1"].ToString();
                    }
                }
                catch (Exception ex)
                { }
                row["C18"] = UType.GetDateTxt(row1["JobDate"].ToString());
                row["C19"] = GetActCity1(row1["Portloading"].ToString());
                row["C82"] = GetActCity1(row1["PortDischarge"].ToString());
                row["C20"] = GetActCity1(row1["PortofDischarge"].ToString()) + "  " + GetDescriptionDDL(row1["ShippingCompanyID"].ToString());

                try
                {
                    row["C39"] = row1["HBLNo"].ToString();
                }
                catch (Exception ex)
                {
                }

                // row["C19"] = UType.GetDateTxt(row1["ArrivalDate"].ToString());  //GetActCity1(row1["PortLoading"].ToString());
                //row["C20"] = row1["jobno"];
                row["C21"] = UType.GetDateTxt(row1["JobDate"].ToString());
                //  row["C22"] = GetActCity1(row1["Vessel"].ToString());



                row["C52"] = GetDescriptionDDL(row1["ShippingCompanyID"].ToString());


                try
                {
                    row["C21"] = row1["volume"].ToString();
                }
                catch (Exception ex)
                { }
                row["C26"] = "";

                row["C27"] = "";
                row["C28"] = "";
                //row["C29"] = row1["Measurement"];
                row["CLogo"] = GetImage();
                row["c78"] = UType.GetDateTxt(row1["ArrivalDate"].ToString());
                row["c79"] = Continers;
                if (rowCh["Customer"].ToString() == Customer)
                {
                    rptDataSet.Tables[0].Rows.Add(row);

                    SnoCtr = SnoCtr + 1;
                }

            }
            decimal TotalWt = 0; decimal TotalPCs = 0;

            foreach (DataRow row11 in InputDataSet.Tables["TblEq"].Rows)
            {
                TotalWt += UType.MyCtoD(row11["GrossWt"].ToString());
                TotalPCs += UType.MyCtoD(row11["package"].ToString());

            }

            decimal TotalTaxAmount = 0; decimal TotalInvAmount = 0; decimal TotalDiscountAmount = 0; decimal TotalNetAmount = 0; decimal TotalLocalAmount = 0;
            TotalTaxAmount = 0;
            foreach (DataRow row11 in InputDataSet.Tables["TblCH"].Rows)
            {
                TotalTaxAmount += UType.MyCtoD(row11["TaxAmount"].ToString());

                if (row11["Customer"].ToString() == Customer)
                {
                    TotalNetAmount += UType.MyCtoD(row11["NetAmount"].ToString());
                    TotalLocalAmount += UType.MyCtoD(row11["LocalAmount"].ToString());
                }
                TotalNetAmount += UType.MyCtoD(row11["NetAmount"].ToString());
                TotalInvAmount += UType.MyCtoD(row11["LocalAmount"].ToString()) + UType.MyCtoD(row11["TaxAmount"].ToString());
            }


            foreach (DataRow row11 in rptDataSet.Tables[0].Rows)
            {
                row11["C29"] = UType.MyFormat(TotalDiscountAmount.ToString());
                row11["C30"] = UType.MyFormat(TotalTaxAmount.ToString());
                row11["C31"] = UType.MyFormat(TotalNetAmount.ToString()); // UType.MyFormat(TotalLocalAmount.ToString()); 
                row11["C83"] = UType.MyFormat(TotalLocalAmount.ToString());
                row11["C84"] = UType.NumberToWords(UType.MyCtoD(row11["C83"].ToString()));
                row11["C35"] = UType.MyFormat(TotalWt.ToString());
                row11["C36"] = UType.MyFormat(TotalPCs.ToString());
            }


        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet MoveInRptDsNOCinfo(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];

            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();


                row["C10"] = "THIS NOC IS VALID UPTO " + UType.GetDateTxt(row1["ValidDoDate"].ToString());

                row["C11"] = row1["gatepass"].ToString();
                row["C12"] = UType.GetDateTxt(row1["DoCurDate"].ToString()); // GetDescriptionDDL(row1["ShippingLineID"].ToString());

                Fld1 = row1["PortDischarge"].ToString();

                DataSet tem1 = GetCityData();
                row["C13"] = "";
                if (tem1.Tables[0].Rows.Count > 0)
                {
                    row["C13"] = tem1.Tables[0].Rows[0]["City_Name"].ToString();
                }

                //row["C13"] = GetDescriptionDDL(row1["PortDischarge"].ToString()) ; //GetDescriptionDDL(row1["ManifestRefNo"].ToString());
                //row["C11"] = GetDescriptionDDL(row1["LocationID"].ToString());
                row["C14"] = "The Deputy Traffic Manager"; //row1["Vessel"];
                //row["C15"] = "N.O.C"; // row1["mblno"];
                row["C16"] = "N.O.C"; // GetDescriptionDDL(row1["SubType"].ToString()); //row1["jobno"];
                row["C17"] = row1["Vessel"].ToString() + " / " + row1["voyage"].ToString();
                row["C18"] = row1["HBLNo"].ToString();

                row["C32"] = GetDescriptionDDL(row1["ShippingLineID"].ToString());
                //
                Fld1 = row1["officeid"].ToString();
                Fld2 = row1["projectid"].ToString();
                Fld3 = row1["PortDischarge"].ToString();
                #region Consignee
                try
                {
                    DataSet dsConsignee = null;
                    MyMain oMyConsignee = new MyMain();
                    oMyConsignee.Fld1 = row1["officeid"].ToString();
                    oMyConsignee.Fld2 = row1["projectid"].ToString();
                    oMyConsignee.Fld3 = row1["Consignee"].ToString();

                    dsConsignee = oMyConsignee.GetAccountOneConsignee();
                    if (dsConsignee != null)
                    {
                        row["C19"] = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();

                    }

                }
                catch (Exception ex)
                {


                }
                #endregion

                // row["C20"] = "";
                row["C22"] = row1["Voyage2"].ToString(); ;
                row["C23"] = row1["IndexNo"].ToString();
                row["C24"] = row1["IGMNo"].ToString();
                row["C25"] = row1["VIRNo"].ToString();
                row["C26"] = row1["VIRNo"];
                row["C28"] = row1["hbldate"];
                string ConsigneeName = "";
                try
                {
                    #region dsConsignee
                    DataSet dsConsignee = null;
                    MyMain oMyConsignee = new MyMain();
                    oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                    oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                    oMyConsignee.Fld3 = row1["CustomClearance"].ToString();

                    dsConsignee = oMyConsignee.GetAccountOneConsignee();
                    if (dsConsignee != null)
                    {
                        ConsigneeName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                                                                                             // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                }
                row["C20"] = ConsigneeName;// GetDescriptionDDL(row1["CustomClearance"].ToString());
                //row["C14"] = GetDescriptionDDL(row1["CustomClearance"].ToString());
                foreach (DataRow row12 in InputDataSet.Tables[1].Rows)
                {
                    row["C21"] = row["C21"] + row12["containerno"].ToString() + " / " + GetDescriptionDDL(row12["sizentype"].ToString()) + "   ";

                }
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


    public DataSet MoveInRptDsDOinfo(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal TotNet = 0; decimal TotGross = 0;
        try
        {
            DataRow rowT1 = InputDataSet.Tables[0].Rows[0];
            string TotalPkg = ""; string PkgUnit = "";

            foreach (DataRow row11 in InputDataSet.Tables[1].Rows)
            {
                TotalPkg = Convert.ToString(UType.MyCtoD(TotalPkg) + UType.MyCtoD(row11["package"].ToString()));
                PkgUnit = GetDescriptionDDL(row11["Unit"].ToString());
            }
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                // row["C1"] = row1["OfficeDescription"];
                row["C3"] = row1["MarkNoContainerNo"].ToString();
                row["C4"] = TotalPkg; // row1["Packages"].ToString();
                row["C5"] = PkgUnit; // GetDescriptionDDL(row1["Unit"].ToString());
                row["C6"] = row1["DescriptionofGoodsPackages"].ToString();
                row["C11"] = GetDescriptionDDL(row1["LocationID"].ToString());
                row["C12"] = "The Deputy Traffic Manager";
                row["C13"] = GetDescriptionDDL(row1["BerthName"].ToString());
                string ConsigneeName = "";
                try
                {
                    #region dsConsignee
                    DataSet dsConsignee = null;
                    MyMain oMyConsignee = new MyMain();
                    oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                    oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                    oMyConsignee.Fld3 = row1["CustomClearance"].ToString();

                    dsConsignee = oMyConsignee.GetAccountOneConsignee();
                    if (dsConsignee != null)
                    {
                        ConsigneeName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                                                                                             // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                }

                row["C14"] = ConsigneeName; //GetDescriptionDDL(row1["CustomClearance"].ToString());
                row["C51"] = GetDescriptionDDL(row1["sline"].ToString());
                row["C40"] = "DO-" + UType.MyFormat4(row1["dono"].ToString()) + "-" + UType.GetYYYY();
                #region Consignee
                try
                {
                    DataSet dsConsignee = null;
                    MyMain oMyConsignee = new MyMain();
                    oMyConsignee.Fld1 = row1["OfficeID"].ToString();
                    oMyConsignee.Fld2 = row1["ProjectId"].ToString();
                    oMyConsignee.Fld3 = row1["Consignee2"].ToString();

                    dsConsignee = oMyConsignee.GetAccountOneConsignee();
                    if (dsConsignee != null)
                    {
                        row["C15"] = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();

                    }

                }
                catch (Exception ex)
                {


                }
                #endregion
                row["C10"] = UType.GetDateTxt(row1["dodate"].ToString()); // GetDescriptionDDL(row1["ManifestRefNo"].ToString());
                row["C16"] = UType.GetDateTxt(row1["jobdate2"].ToString());
                row["C17"] = row1["Vessel"];
                row["C18"] = row1["IndexNo"];
                row["C19"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["portloading1"].ToString());//GetDescriptionDDL(row1["portofloading"].ToString());
                row["C20"] = row1["ManualNetWt"];
                TotNet = TotNet + UType.MyCtoD(row1["ManualNetWt"].ToString());
                row["C21"] = row1["VIRNo"];
                row["C22"] = row1["Voyage"];
                row["C23"] = row1["HBLNo"];
                row["C24"] = row1["IGMNo"];
                row["C25"] = row1["GrossWt"];
                TotGross = TotGross + UType.MyCtoD(row1["GrossWt"].ToString());
                row["C26"] = UType.GetDateTxt(row1["ArrivalDate"].ToString());
                row["C27"] = UType.GetDateTxt(row1["dateofissue"].ToString());
                row["C28"] = UType.GetDateTxt(row1["igmdate"].ToString());
                row["C29"] = GetDescriptionDDL(row1["WTUNIT"].ToString());
                //
                //row["C29"] = SessionParameter GetDescriptionDDL(row1["WTUNIT"].ToString());
                //

                row["CLogo"] = GetImage();

                rptDataSet.Tables[0].Rows.Add(row);
            }

            int rcRpt = rptDataSet.Tables[0].Rows.Count;
            int Ctr = 0;
            int RecCtr = 1;
            TotNet = 0; TotGross = 0;
            foreach (DataRow row1 in InputDataSet.Tables[1].Rows)
            {
                DataRow rowRpt = null;

                bool IsNew = false;
                try
                {
                    rowRpt = rptDataSet.Tables[0].Rows[Ctr];
                }
                catch (Exception ex)
                {
                    rowRpt = rptDataSet.Tables[0].NewRow();
                    IsNew = true;
                }
                TotNet = TotNet + UType.MyCtoD(row1["NetWt"].ToString());
                TotGross = TotGross + UType.MyCtoD(row1["GrossWt"].ToString());

                rowRpt["C25"] = Convert.ToString(TotGross);
                if (RecCtr == 1)
                {

                    //rowRpt["C30"] = GetDescriptionDDL(row1["Sizentype"].ToString());
                    //rowRpt["C31"] = row1["ContainerNo"].ToString();
                    //rowRpt["C32"] = row1["Seal"].ToString();
                    //rowRpt["C33"] = GetDescriptionDDL(row1["delivery"].ToString());
                    rowRpt["C30"] = GetDescriptionDDL(row1["Sizentype"].ToString()) + "  " + row1["ContainerNo"].ToString() + " " + row1["Seal"].ToString() + "  " + GetDescriptionDDL(row1["delivery"].ToString()) + "  " + GetDescriptionDDL(rowT1["incoterm"].ToString());

                }
                if (RecCtr == 2)
                {

                    rowRpt["C35"] = GetDescriptionDDL(row1["Sizentype"].ToString()) + "  " + row1["ContainerNo"].ToString() + " " + row1["Seal"].ToString() + "  " + GetDescriptionDDL(row1["delivery"].ToString());
                }
                if (RecCtr == 3)
                {

                    rowRpt["C31"] = GetDescriptionDDL(row1["Sizentype"].ToString()) + "  " + row1["ContainerNo"].ToString() + " " + row1["Seal"].ToString() + "  " + GetDescriptionDDL(row1["delivery"].ToString());
                }
                if (RecCtr == 4)
                {

                    rowRpt["C36"] = GetDescriptionDDL(row1["Sizentype"].ToString()) + "  " + row1["ContainerNo"].ToString() + row1["Seal"].ToString() + "  " + GetDescriptionDDL(row1["delivery"].ToString());
                }
                if (RecCtr == 5)
                {
                    rowRpt["C32"] = GetDescriptionDDL(row1["Sizentype"].ToString()) + "  " + row1["ContainerNo"].ToString() + row1["Seal"].ToString() + "  " + GetDescriptionDDL(row1["delivery"].ToString());
                }
                if (RecCtr == 6)
                {
                    rowRpt["C37"] = GetDescriptionDDL(row1["Sizentype"].ToString()) + "  " + row1["ContainerNo"].ToString() + row1["Seal"].ToString() + "  " + GetDescriptionDDL(row1["delivery"].ToString());
                }
                if (RecCtr == 7)
                {
                    rowRpt["C33"] = GetDescriptionDDL(row1["Sizentype"].ToString()) + "  " + row1["ContainerNo"].ToString() + row1["Seal"].ToString() + "  " + GetDescriptionDDL(row1["delivery"].ToString());
                }
                if (RecCtr == 8)
                {
                    rowRpt["C38"] = GetDescriptionDDL(row1["Sizentype"].ToString()) + "  " + row1["ContainerNo"].ToString() + row1["Seal"].ToString() + "  " + GetDescriptionDDL(row1["delivery"].ToString());
                }
                if (RecCtr == 9)
                {
                    rowRpt["C34"] = GetDescriptionDDL(row1["Sizentype"].ToString()) + "  " + row1["ContainerNo"].ToString() + row1["Seal"].ToString() + "  " + GetDescriptionDDL(row1["delivery"].ToString());
                }
                if (RecCtr == 10)
                {
                    rowRpt["C39"] = GetDescriptionDDL(row1["Sizentype"].ToString()) + "  " + row1["ContainerNo"].ToString() + row1["Seal"].ToString() + "  " + GetDescriptionDDL(row1["delivery"].ToString());
                }
                if (RecCtr == 10)
                {
                    RecCtr = 1;
                    Ctr = Ctr + 1;
                }
                RecCtr = RecCtr + 1;
                if (IsNew)
                {
                    rowRpt["CLogo"] = GetImage();
                    rptDataSet.Tables[0].Rows.Add(rowRpt);
                }

            }
            foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
            {


                row1["C20"] = Convert.ToString(TotNet);
                row1["C25"] = Convert.ToString(TotGross);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet MoveInRptDsGPinfo(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C7"] = "AXL CONTAINER LINE PVT LTD";

                row["C1"] = "Second Foil";
                row["C2"] = "First Foil";
                row["C51"] = "Counter Foil";
                row["C52"] = "Office Foil";

                row["C3"] = ""; // row1["HBLNo"];
                row["C4"] = row1["Gatepass"];
                row["C5"] = row1["HBLNo"];
                row["C6"] = row1["IndexNo"];
                // row["C7"] = GetDescriptionDDL(row1["ShippingLineID"].ToString());  // GetDescriptionDDL(row1["LocationID"].ToString()) + " The Deputy Traffic Manager";
                row["C8"] = GetDescriptionDDL(row1["ShippingLicenseID"].ToString());
                row["C9"] = UType.GetDateTxt(row1["ArrivalDate"].ToString()); // GetDescriptionDDL(row1["ShippingLineID"].ToString());
                string ConsigneeName = "";
                try
                {
                    #region dsConsignee
                    DataSet dsConsignee = null;
                    MyMain oMyConsignee = new MyMain();
                    oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                    oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                    oMyConsignee.Fld3 = row1["CustomClearance"].ToString();

                    dsConsignee = oMyConsignee.GetAccountOneConsignee();
                    if (dsConsignee != null)
                    {
                        ConsigneeName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                                                                                             // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                }
                row["C10"] = ConsigneeName; // GetDescriptionDDL(row1["CustomClearance"].ToString());  // row1["ManifestRefNo"].ToString();
                //row["C11"] = GetDescriptionDDL(row1["Consignee"].ToString()); //GetDescriptionDDL(row1["ManifestRefNo"].ToString());
                #region Consignee
                try
                {
                    DataSet dsConsignee = null;
                    MyMain oMyConsignee = new MyMain();
                    oMyConsignee.Fld1 = row1["OfficeID"].ToString();
                    oMyConsignee.Fld2 = row1["ProjectId"].ToString();
                    oMyConsignee.Fld3 = row1["Consignee2"].ToString();

                    dsConsignee = oMyConsignee.GetAccountOneConsignee();
                    if (dsConsignee != null)
                    {
                        row["C11"] = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();

                    }

                }
                catch (Exception ex)
                {


                }
                #endregion
                row["C12"] = GetDescriptionDDL(row1["LocationID"].ToString()); //row1["Vessel"];

                row["C13"] = GetDescriptionDDL(row1["LocationID"].ToString());
                row["C14"] = row1["Vessel"];
                row["C15"] = ""; //row1["ManualNetWt"];
                row["C16"] = row1["Voyage"];
                row["C17"] = UType.GetDateTxt(row1["ArrivalDate"].ToString());
                row["C18"] = GetDescriptionDDL(row1["importer"].ToString());  //row1["HBLNo"];
                row["C19"] = "";
                row["C20"] = "";
                row["C21"] = UType.GetDateTxt(row1["ArrivalDate"].ToString());
                row["C23"] = UType.GetDateTxt(row1["ArrivalDate"].ToString());
                row["C24"] = row1["igmno"].ToString();
                string CntrNo1 = ""; string Siz1 = ""; int Ctr = 1; string CntrNo2 = ""; string Siz2 = "";
                foreach (DataRow row2 in InputDataSet.Tables[1].Rows)
                {
                    if (Ctr == 1)
                    {
                        CntrNo1 = CntrNo1 + row2["ContainerNo"].ToString();
                        Siz1 = Siz1 + GetDescriptionDDL(row2["sizentype"].ToString());
                        // if (CntrNo1.Length > 0)
                        //  {
                        //    CntrNo1 = CntrNo1 + ", ";
                        //   Siz1 = Siz1 + ", ";
                        //   }
                    }
                    if (Ctr == 2)
                    {
                        if (CntrNo2.Length > 0)
                        {
                            CntrNo2 = ", " + CntrNo2;
                            Siz2 = ", " + Siz2;
                        }
                        CntrNo2 = CntrNo2 + row2["ContainerNo"].ToString();
                        Siz2 = Siz2 + GetDescriptionDDL(row2["sizentype"].ToString());

                    }
                    Ctr = Ctr + 1;
                    if (Ctr > 2)
                    { Ctr = 1; }
                }
                row["C25"] = CntrNo1.ToString();
                row["C26"] = Siz1.ToString();
                row["C27"] = CntrNo2.ToString();
                row["C28"] = Siz2.ToString();
                row["C29"] = row1["Measurement"];
                row["C30"] = UType.GetDateTxt(row1["ValidDoDate"].ToString());

                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
            }
            DataRow row123 = rptDataSet.Tables[0].NewRow();
            row123["C7"] = "AXL CONTAINER LINE PVT LTD";

            row123["C1"] = "Third Foil";
            row123["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row123);
            row123 = rptDataSet.Tables[0].NewRow();
            row123["C7"] = "AXL CONTAINER LINE PVT LTD";

            row123["C1"] = "Four Foil";
            row123["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row123);
            //        Cntr1 = Cntr1 + row1["ContainerNo"].ToString();
            //        Size1 = Size1 + GetDescriptionCust(row1["officeid"].ToString(), row1["ProjectId"].ToString(), row1["SizeNtype"].ToString());
            //        ctr = "0";
            //    }
            //}
            //rptDataSet.Tables[0].Rows[0]["C25"] = Cntr;
            //rptDataSet.Tables[0].Rows[0]["C26"] = Size;
            //rptDataSet.Tables[0].Rows[0]["C27"] = Cntr1;
            //rptDataSet.Tables[0].Rows[0]["C28"] = Size1;



        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }
    public DataSet MoveInRptDsBL(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        string wtUnit = "";
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            DataRow row1 = InputDataSet.Tables[0].Rows[0];

            #region Consignee
            //string Consignee = "";

            //MyMain oMy = new MyMain();
            //try
            //{
            //    DataSet dsConsignee = null;
            //    MyMain oMyAccountexpense = new MyMain();
            //    oMyAccountexpense.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
            //    oMyAccountexpense.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
            //    oMyAccountexpense.Fld3 = row1["consignee"].ToString();

            //    dsConsignee = oMyAccountexpense.GetAccountnCity();
            //    if (dsConsignee != null)
            //    {
            //        Consignee = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
            //        oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
            //        oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
            //        oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
            //        Consignee = Consignee + "\r\n" + dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
            //    }

            //}
            //catch (Exception ex)
            //{
            //}

            #endregion



            #region shipper
            //string Shipper = "";
            //oMy = new MyMain();
            //try
            //{
            //    DataSet dsConsignee = null;
            //    MyMain oMyConsignee = new MyMain();
            //    oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
            //    oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
            //    oMyConsignee.Fld3 = row1["shipper"].ToString();

            //    dsConsignee = oMyConsignee.GetAccountnCity();
            //    if (dsConsignee != null)
            //    {
            //        Shipper = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
            //        oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
            //        oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
            //        oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
            //        Shipper = Shipper + "\r\n" + dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
            //    }

            //}
            //catch (Exception ex)
            //{


            //}
            #endregion



            #region shipperBL
            string shipperBL = "";
            MyMain oMy = new MyMain();
            try
            {
                DataSet dsshipperBL = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["shipper"].ToString();

                dsshipperBL = oMyConsignee.GetAccountnCity();
                if (dsshipperBL != null)
                {
                    shipperBL = dsshipperBL.Tables[0].Rows[0]["ActDesc"].ToString();
                    oMy.Fld10 = dsshipperBL.Tables[0].Rows[0]["country_name"].ToString();
                    oMy.Fld11 = oMy.Fld10 + dsshipperBL.Tables[0].Rows[0]["city_name"].ToString();
                    oMy.Fld12 = dsshipperBL.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion
            decimal TotalGrossWt = 0; decimal TotalMeasure = 0; decimal TotalNetWt = 0;
            int Ctr = 1;
            int RowCount = InputDataSet.Tables[1].Rows.Count;
            DataRow row = rptDataSet.Tables[0].NewRow();
            decimal AddRow = 0;
            foreach (DataRow rowCh in InputDataSet.Tables[1].Rows)
            {


                TotalGrossWt = TotalGrossWt + UType.MyCtoD(rowCh["GrossWt"].ToString());
                TotalNetWt = TotalNetWt + UType.MyCtoD(rowCh["NetWt"].ToString());
                TotalMeasure = TotalMeasure + UType.MyCtoD(rowCh["Measurement"].ToString());
                wtUnit = GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                row["C2"] = rowCh["SNo"];
                row["C13"] = rowCh["NotifyParty1"];
                row["C84"] = GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                row["C86"] = GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                row["C98"] = "N";
                if (Ctr == 1)
                {
                    row["C30"] = rowCh["ContainerNo"].ToString();
                    row["C31"] = GetDescriptionDDL(rowCh["SizeNtype"].ToString());  // rowCh["SizeNtype"].ToString();
                    row["C32"] = rowCh["Seal"].ToString();
                    row["C33"] = UType.MyFormat1N(rowCh["Package"].ToString()) + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                    row["C34"] = UType.MyFormat1(rowCh["GrossWt"].ToString()) + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C35"] = UType.MyFormat1(rowCh["NetWt"].ToString()) + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                }

                if (Ctr == 2)
                {

                    row["C30"] = row["C30"] + "\r\n" + rowCh["ContainerNo"].ToString();

                    row["C31"] = row["C31"] + "\r\n" + GetDescriptionDDL(rowCh["SizeNtype"].ToString());

                    row["C32"] = row["C32"] + "\r\n" + rowCh["Seal"].ToString();
                    row["C33"] = row["C33"] + "\r\n" + UType.MyFormat1N(rowCh["Package"].ToString()) + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                    //row["C40"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    row["C34"] = row["C34"] + "\r\n" + UType.MyFormat1(rowCh["GrossWt"].ToString()) + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    //row["C89"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C35"] = row["C35"] + "\r\n" + UType.MyFormat1(rowCh["NetWt"].ToString()) + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());

                }
                if (Ctr == 3)
                {

                    row["C30"] = row["C30"] + "\r\n" + rowCh["ContainerNo"].ToString();
                    //row["C88"] = GetDescriptionDDL(rowCh["SizeNtype"].ToString()); //rowCh["SizeNtype"].ToString();
                    row["C31"] = row["C31"] + "\r\n" + GetDescriptionDDL(rowCh["SizeNtype"].ToString());
                    // row["C74"] = rowCh["Seal"].ToString();
                    row["C32"] = row["C32"] + "\r\n" + rowCh["Seal"].ToString();
                    row["C33"] = row["C33"] + "\r\n" + UType.MyFormat1N(rowCh["Package"].ToString()) + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                    row["C75"] = "";// rowCh["GrossWt"].ToString();
                    //row["C76"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C34"] = row["C34"] + "\r\n" + UType.MyFormat1(rowCh["GrossWt"].ToString()) + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    //row["C77"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C35"] = row["C35"] + "\r\n" + UType.MyFormat1(rowCh["NetWt"].ToString()) + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());

                }
                if (Ctr == 4)
                {
                    // row["C78"] = rowCh["ContainerNo"].ToString();
                    row["C30"] = row["C30"] + "\r\n" + rowCh["ContainerNo"].ToString();
                    //row["C79"] = GetDescriptionDDL(rowCh["SizeNtype"].ToString()); //rowCh["SizeNtype"].ToString();
                    row["C31"] = row["C31"] + "\r\n" + GetDescriptionDDL(rowCh["SizeNtype"].ToString());
                    // row["C80"] = rowCh["Seal"].ToString();
                    row["C32"] = row["C32"] + "\r\n" + rowCh["Seal"].ToString();
                    row["C33"] = row["C33"] + "\r\n" + UType.MyFormat1N(rowCh["Package"].ToString()) + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                    row["C81"] = "";// rowCh["GrossWt"].ToString();
                    //row["C82"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C34"] = row["C34"] + "\r\n" + UType.MyFormat1N(rowCh["GrossWt"].ToString()) + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    //row["C83"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C35"] = row["C35"] + "\r\n" + UType.MyFormat1N(rowCh["NetWt"].ToString()) + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());

                }

                row["C36"] = rowCh["Measurement"].ToString();
                Ctr = Ctr + 1;
                //AddRow = 0;
            }

            row["CLogo"] = GetImageBLRpt(row1["IndexType"].ToString());
            rptDataSet.Tables[0].Rows.Add(row);

            foreach (DataRow rowR in rptDataSet.Tables[0].Rows)
            {
                rowR["C1"] = "MULTIMODAL TRANSPORT"; //"MULTIMODAL TRANSPORT BILL OF LADING";

                rowR["C11"] = row1["Shipper11"].ToString();    // +"\r\n"+ BLShipper;

                rowR["C12"] = row1["Consignee11"].ToString();   // + "\r\n" + BLConsignee;
                rowR["C13"] = row1["NotifyParty1"].ToString();  //Consignee;
                rowR["C71"] = row1["HBL"].ToString(); //+ "  " + row1["hblno"].ToString();

                rowR["C73"] = GetActCity1(row1["ForwarderReference"].ToString());
                rowR["C14"] = row1["pre_carriage"].ToString();
                row["C15"] = row1["PlaceofReceipt"].ToString(); // GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["PlaceofReceipt"].ToString());
                rowR["C16"] = row1["Vessel"].ToString() + "  " + row1["voyage"].ToString();
                rowR["C17"] = GetActCity1(row1["PortLoading"].ToString());

                // row["C18"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["PortofDischarge"].ToString());  //rowCh["PortofDischarge"];
                rowR["C18"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["PortDischarge"].ToString());  //rowCh["PortofDischarge"];
                rowR["C19"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["FinalDestination"].ToString()); //GetDescriptionDDL(row1["PortofDelivery"].ToString()); //rowCh["FinalDestination"];

                rowR["C72"] = shipperBL;
                rowR["C73"] = row1["ForwarderReference"].ToString(); ;

                rowR["C20"] = row1["MarkNoContainerNo"].ToString();
                rowR["C21"] = row1["NumberandKindofPackages"].ToString();

                string GoodsMsg = "";
                try
                {
                    string Goods = "";
                    int lenGoods = row1["DescriptionofGoodsPackages"].ToString().Length;
                    if (lenGoods < 400)
                    {
                        Goods = row1["DescriptionofGoodsPackages"].ToString();
                        Goods = Goods.PadRight(400 - lenGoods, '.');
                        Goods = Goods + ".";
                    }
                    if (lenGoods > 400)
                    {
                        Goods = row1["DescriptionofGoodsPackages"].ToString().Substring(0, 400);
                        GoodsMsg = "CONTINUED ON FOLLOWING PAGE";
                    }
                }
                catch (Exception)
                {
                }

                rowR["C22"] = row1["DescriptionofGoodsPackages"].ToString();
                rowR["C23"] = UType.MyFormat1N(Convert.ToString(TotalGrossWt)) + " " + wtUnit + " " + "\r\n" + "Net Weight:" + "\r\n" + UType.MyFormat1N(Convert.ToString(TotalNetWt));
                rowR["C55"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["portofloading"].ToString());
                rowR["C56"] = GoodsMsg;
                rowR["C57"] = row1["noofBL"].ToString();
                rowR["C58"] = UType.GetDateTxt(row1["dateofissue"].ToString());
                rowR["C59"] = row1["overseas1"].ToString();
                rowR["C60"] = GetDescriptionDDL(row1["indextype"].ToString());

                rowR["C76"] = GetDescriptionDDL(row1["FreightType"].ToString());
                rowR["C24"] = Convert.ToString(TotalMeasure); ; // rowCh["Measurement"].ToString();

            }
        }
        catch (Exception ex)
        {
        }

        return rptDataSet;
    }

    public DataSet MoveInRptDsReleaseMessage(DataSet InputDataSet, string LoginName)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            DataRow row1 = InputDataSet.Tables[0].Rows[0];

            #region Consignee
            //string Consignee = "";

            //MyMain oMy = new MyMain();
            //try
            //{
            //    DataSet dsConsignee = null;
            //    MyMain oMyAccountexpense = new MyMain();
            //    oMyAccountexpense.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
            //    oMyAccountexpense.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
            //    oMyAccountexpense.Fld3 = row1["consignee"].ToString();

            //    dsConsignee = oMyAccountexpense.GetAccountnCity();
            //    if (dsConsignee != null)
            //    {
            //        Consignee = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
            //        oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
            //        oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
            //        oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
            //        Consignee = Consignee + "\r\n" + dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
            //    }

            //}
            //catch (Exception ex)
            //{
            //}

            #endregion



            //#region overseas
            //string overseas = "";
            //MyMain oMy = new MyMain();
            //try
            //{
            //    DataSet dsConsignee = null;
            //    MyMain oMyConsignee = new MyMain();
            //    oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
            //    oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
            //    oMyConsignee.Fld3 = row1["overseas"].ToString();

            //    dsConsignee = oMyConsignee.GetAccountnCity();
            //    if (dsConsignee != null)
            //    {
            //        overseas = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
            //        oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
            //        oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
            //        oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
            //        overseas = overseas + "\r\n" + dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
            //    }

            //}
            //catch (Exception ex)
            //{


            //}
            //#endregion



            #region shipperBL
            string shipperBL = "";
            MyMain oMy = new MyMain();
            try
            {
                DataSet dsshipperBL = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["shipper"].ToString();

                dsshipperBL = oMyConsignee.GetAccountnCity();
                if (dsshipperBL != null)
                {
                    shipperBL = dsshipperBL.Tables[0].Rows[0]["ActDesc"].ToString();
                    oMy.Fld10 = dsshipperBL.Tables[0].Rows[0]["country_name"].ToString();
                    oMy.Fld11 = oMy.Fld10 + dsshipperBL.Tables[0].Rows[0]["city_name"].ToString();
                    oMy.Fld12 = dsshipperBL.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion
            decimal TotalGrossWt = 0; decimal TotalMeasure = 0; decimal TotalNetWt = 0;
            int Ctr = 1;
            int RowCount = InputDataSet.Tables[1].Rows.Count;
            DataRow row = rptDataSet.Tables[0].NewRow();
            decimal AddRow = 0;
            foreach (DataRow rowCh in InputDataSet.Tables[1].Rows)
            {


                TotalGrossWt = TotalGrossWt + UType.MyCtoD(rowCh["GrossWt"].ToString());
                TotalNetWt = TotalNetWt + UType.MyCtoD(rowCh["NetWt"].ToString());
                TotalMeasure = TotalMeasure + UType.MyCtoD(rowCh["Measurement"].ToString());
                row["C2"] = rowCh["SNo"];
                row["C13"] = rowCh["NotifyParty1"];
                row["C84"] = GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                row["C86"] = GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                row["C98"] = "N";
                if (Ctr == 1)
                {
                    row["C30"] = rowCh["ContainerNo"].ToString();
                    row["C31"] = GetDescriptionDDL(rowCh["SizeNtype"].ToString());  // rowCh["SizeNtype"].ToString();
                    row["C32"] = rowCh["Seal"].ToString();
                    row["C33"] = rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                    row["C34"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C35"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                }

                if (Ctr == 2)
                {

                    row["C30"] = row["C30"] + "\r\n" + rowCh["ContainerNo"].ToString();

                    row["C31"] = row["C31"] + "\r\n" + GetDescriptionDDL(rowCh["SizeNtype"].ToString());
                    //row["C38"] = rowCh["Seal"].ToString();
                    row["C32"] = row["C32"] + "\r\n" + rowCh["Seal"].ToString();
                    row["C33"] = row["C33"] + "\r\n" + rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                    //row["C40"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    row["C34"] = row["C34"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    //row["C89"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C35"] = row["C35"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());

                }
                if (Ctr == 3)
                {
                    //row["C87"] = rowCh["ContainerNo"].ToString();
                    row["C30"] = row["C30"] + "\r\n" + rowCh["ContainerNo"].ToString();
                    //row["C88"] = GetDescriptionDDL(rowCh["SizeNtype"].ToString()); //rowCh["SizeNtype"].ToString();
                    row["C31"] = row["C31"] + "\r\n" + GetDescriptionDDL(rowCh["SizeNtype"].ToString());
                    // row["C74"] = rowCh["Seal"].ToString();
                    row["C32"] = row["C32"] + "\r\n" + rowCh["Seal"].ToString();
                    row["C33"] = row["C33"] + "\r\n" + rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                    row["C75"] = "";// rowCh["GrossWt"].ToString();
                    //row["C76"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C34"] = row["C34"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    //row["C77"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C35"] = row["C35"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());

                }
                if (Ctr == 4)
                {
                    // row["C78"] = rowCh["ContainerNo"].ToString();
                    row["C30"] = row["C30"] + "\r\n" + rowCh["ContainerNo"].ToString();
                    //row["C79"] = GetDescriptionDDL(rowCh["SizeNtype"].ToString()); //rowCh["SizeNtype"].ToString();
                    row["C31"] = row["C31"] + "\r\n" + GetDescriptionDDL(rowCh["SizeNtype"].ToString());
                    // row["C80"] = rowCh["Seal"].ToString();
                    row["C32"] = row["C32"] + "\r\n" + rowCh["Seal"].ToString();
                    row["C33"] = row["C33"] + "\r\n" + rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                    row["C81"] = "";// rowCh["GrossWt"].ToString();
                    //row["C82"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C34"] = row["C34"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    //row["C83"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C35"] = row["C35"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());

                }


                Ctr = Ctr + 1;
                //AddRow = 0;
            }

            row["C83"] = LoginName;
            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);

            foreach (DataRow rowR in rptDataSet.Tables[0].Rows)
            {
                rowR["C1"] = "RELEASE MESSAGE"; //"MULTIMODAL TRANSPORT BILL OF LADING";

                rowR["C11"] = row1["Shipper11"].ToString();    // +"\r\n"+ BLShipper;

                rowR["C12"] = GetDescriptionDDL(row1["overseas"].ToString());   // + "\r\n" + BLConsignee;
                rowR["C13"] = row1["NotifyParty1"].ToString();  //Consignee;
                rowR["C71"] = row1["HBL"].ToString(); //+ "  " + row1["hblno"].ToString();

                rowR["C73"] = GetActCity1(row1["ForwarderReference"].ToString());
                rowR["C14"] = row1["pre_carriage"].ToString();
                //row["C15"] = GetActCity1(row1["PlaceofReceipt"].ToString());
                rowR["C16"] = row1["Vessel"].ToString() + "  " + row1["voyage"].ToString();
                rowR["C17"] = GetActCity1(row1["PortLoading"].ToString());

                // row["C18"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["PortofDischarge"].ToString());  //rowCh["PortofDischarge"];
                rowR["C18"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["PortDischarge"].ToString());  //rowCh["PortofDischarge"];
                rowR["C19"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["FinalDestination"].ToString()); //GetDescriptionDDL(row1["PortofDelivery"].ToString()); //rowCh["FinalDestination"];

                rowR["C72"] = shipperBL;
                rowR["C73"] = row1["ForwarderReference"].ToString(); ;

                rowR["C20"] = GetDescriptionDDL(row1["FreightType"].ToString());
                rowR["C21"] = row1["NumberandKindofPackages"].ToString();


                try
                {
                    string Goods = "";
                    int lenGoods = row1["DescriptionofGoodsPackages"].ToString().Length;
                    if (lenGoods < 400)
                    {
                        Goods = row1["DescriptionofGoodsPackages"].ToString();
                        Goods = Goods.PadRight(400 - lenGoods, '.');
                        Goods = Goods + ".";
                    }
                    if (lenGoods > 400)
                    {
                        Goods = row1["DescriptionofGoodsPackages"].ToString().Substring(0, 400);
                    }
                }
                catch (Exception)
                {
                }

                rowR["C22"] = row1["DescriptionofGoodsPackages"].ToString();
                rowR["C23"] = "Gross Weight: " + "\r\n" + Convert.ToString(TotalGrossWt) + "\r\n" + "Net Weight:" + "\r\n" + Convert.ToString(TotalNetWt);
                rowR["C55"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["portofloading"].ToString());
                rowR["C56"] = "CONTINUED ON FOLLOWING PAGE";
                rowR["C59"] = GetDescriptionDDL1(row1["overseas"].ToString());
                //rowR["C83"] = "GW: "+Convert.ToString(TotalGrossWt);
                if (TotalGrossWt > 0)
                {
                    // rowR["C84"] = rowR["C84"] + " " + GetDescriptionDDL(row1["wtunit"].ToString());
                }
                // rowR["C85"] = "NW: " + Convert.ToString(TotalNetWt);
                // rowR["C86"] = Convert.ToString(TotalNetWt);
                if (TotalNetWt > 0)
                {
                    //  rowR["C86"] = rowR["C86"] + " " + GetDescriptionDDL(row1["wtunit"].ToString());
                }

                rowR["C24"] = Convert.ToString(TotalMeasure); ; // rowCh["Measurement"].ToString();

            }
        }
        catch (Exception ex)
        {
        }

        return rptDataSet;
    }

    public DataSet MoveInRptDsCargo(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            DataRow row1 = InputDataSet.Tables[0].Rows[0];

            #region Consignee
            //string Consignee = "";

            //MyMain oMy = new MyMain();
            //try
            //{
            //    DataSet dsConsignee = null;
            //    MyMain oMyAccountexpense = new MyMain();
            //    oMyAccountexpense.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
            //    oMyAccountexpense.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
            //    oMyAccountexpense.Fld3 = row1["consignee"].ToString();

            //    dsConsignee = oMyAccountexpense.GetAccountnCity();
            //    if (dsConsignee != null)
            //    {
            //        Consignee = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
            //        oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
            //        oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
            //        oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
            //        Consignee = Consignee + "\r\n" + dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
            //    }

            //}
            //catch (Exception ex)
            //{
            //}

            #endregion



            #region shipper
            //string Shipper = "";
            //oMy = new MyMain();
            //try
            //{
            //    DataSet dsConsignee = null;
            //    MyMain oMyConsignee = new MyMain();
            //    oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
            //    oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
            //    oMyConsignee.Fld3 = row1["shipper"].ToString();

            //    dsConsignee = oMyConsignee.GetAccountnCity();
            //    if (dsConsignee != null)
            //    {
            //        Shipper = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
            //        oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
            //        oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
            //        oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
            //        Shipper = Shipper + "\r\n" + dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
            //    }

            //}
            //catch (Exception ex)
            //{


            //}
            #endregion
            decimal NoOfBL = 0;
            foreach (DataRow rowCh in InputDataSet.Tables[0].Rows)
            {
                NoOfBL = NoOfBL + 1;
            }


            #region shipperBL
            string shipperBL = "";
            MyMain oMy = new MyMain();
            try
            {
                DataSet dsshipperBL = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["shipper"].ToString();

                dsshipperBL = oMyConsignee.GetAccountnCity();
                if (dsshipperBL != null)
                {
                    shipperBL = dsshipperBL.Tables[0].Rows[0]["ActDesc"].ToString();
                    oMy.Fld10 = dsshipperBL.Tables[0].Rows[0]["country_name"].ToString();
                    oMy.Fld11 = oMy.Fld10 + dsshipperBL.Tables[0].Rows[0]["city_name"].ToString();
                    oMy.Fld12 = dsshipperBL.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion
            decimal TotalGrossWt = 0; decimal TotalMeasure = 0; decimal TotalNetWt = 0;
            int Ctr = 1;
            int RowCount = InputDataSet.Tables[1].Rows.Count;
            DataRow row = rptDataSet.Tables[0].NewRow();
            decimal AddRow = 0;
            foreach (DataRow rowCh in InputDataSet.Tables[1].Rows)
            {


                TotalGrossWt = TotalGrossWt + UType.MyCtoD(rowCh["GrossWt"].ToString());
                TotalNetWt = TotalNetWt + UType.MyCtoD(rowCh["NetWt"].ToString());
                TotalMeasure = TotalMeasure + UType.MyCtoD(rowCh["Measurement"].ToString());
                row["C2"] = rowCh["SNo"];
                row["C13"] = rowCh["NotifyParty1"];
                row["C84"] = GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                row["C86"] = GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                row["C98"] = "N";
                if (Ctr == 1)
                {
                    row["C30"] = rowCh["ContainerNo"].ToString();
                    row["C31"] = GetDescriptionDDL(rowCh["SizeNtype"].ToString());  // rowCh["SizeNtype"].ToString();
                    row["C32"] = rowCh["Seal"].ToString();
                    row["C33"] = rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                    row["C34"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C35"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                }

                if (Ctr == 2)
                {

                    row["C30"] = row["C30"] + "\r\n" + rowCh["ContainerNo"].ToString();

                    row["C31"] = row["C31"] + "\r\n" + GetDescriptionDDL(rowCh["SizeNtype"].ToString());
                    //row["C38"] = rowCh["Seal"].ToString();
                    row["C32"] = row["C32"] + "\r\n" + rowCh["Seal"].ToString();
                    row["C33"] = row["C33"] + "\r\n" + rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                    //row["C40"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    row["C34"] = row["C34"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    //row["C89"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C35"] = row["C35"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());

                }
                if (Ctr == 3)
                {
                    //row["C87"] = rowCh["ContainerNo"].ToString();
                    row["C30"] = row["C30"] + "\r\n" + rowCh["ContainerNo"].ToString();
                    //row["C88"] = GetDescriptionDDL(rowCh["SizeNtype"].ToString()); //rowCh["SizeNtype"].ToString();
                    row["C31"] = row["C31"] + "\r\n" + GetDescriptionDDL(rowCh["SizeNtype"].ToString());
                    // row["C74"] = rowCh["Seal"].ToString();
                    row["C32"] = row["C32"] + "\r\n" + rowCh["Seal"].ToString();
                    row["C33"] = row["C33"] + "\r\n" + rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                    row["C75"] = "";// rowCh["GrossWt"].ToString();
                    //row["C76"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C34"] = row["C34"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    //row["C77"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C35"] = row["C35"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());

                }
                if (Ctr == 4)
                {
                    // row["C78"] = rowCh["ContainerNo"].ToString();
                    row["C30"] = row["C30"] + "\r\n" + rowCh["ContainerNo"].ToString();
                    //row["C79"] = GetDescriptionDDL(rowCh["SizeNtype"].ToString()); //rowCh["SizeNtype"].ToString();
                    row["C31"] = row["C31"] + "\r\n" + GetDescriptionDDL(rowCh["SizeNtype"].ToString());
                    // row["C80"] = rowCh["Seal"].ToString();
                    row["C32"] = row["C32"] + "\r\n" + rowCh["Seal"].ToString();
                    row["C33"] = row["C33"] + "\r\n" + rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                    row["C81"] = "";// rowCh["GrossWt"].ToString();
                    //row["C82"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C34"] = row["C34"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    //row["C83"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    row["C35"] = row["C35"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());

                }

                row["C28"] = NoOfBL.ToString();
                Ctr = Ctr + 1;
                //AddRow = 0;
            }

            row["CLogo"] = GetImage();
            rptDataSet.Tables[0].Rows.Add(row);

            foreach (DataRow rowR in rptDataSet.Tables[0].Rows)
            {
                rowR["C1"] = "CARGO MANIFEST"; //"MULTIMODAL TRANSPORT BILL OF LADING";

                rowR["C12"] = "(S) " + row1["Shipper11"].ToString() + "\r\n";

                rowR["C12"] = rowR["C12"].ToString() + "(C)" + row1["Consignee11"].ToString() + "\r\n";// + BLConsignee;
                rowR["C12"] = rowR["C12"].ToString() + "(N)" + row1["NotifyParty1"].ToString();  //Consignee;
                rowR["C71"] = row1["HBL"].ToString() + "\r\n" + rowR["C30"].ToString(); //+ "  " + row1["hblno"].ToString();

                rowR["C73"] = GetActCity1(row1["ForwarderReference"].ToString());
                rowR["C14"] = UType.GetDateTxt(row1["PreAlertDate"].ToString());
                //row["C15"] = GetActCity1(row1["PlaceofReceipt"].ToString());
                rowR["C16"] = row1["Vessel"].ToString() + "  " + row1["voyage"].ToString();
                rowR["C17"] = GetActCity1(row1["PortLoading"].ToString());

                // row["C18"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["PortofDischarge"].ToString());  //rowCh["PortofDischarge"];
                rowR["C18"] = GetDescriptionDDL1(row1["ShippingCompanyID"].ToString());
                rowR["C19"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["FinalDestination"].ToString()); //GetDescriptionDDL(row1["PortofDelivery"].ToString()); //rowCh["FinalDestination"];

                rowR["C72"] = shipperBL;
                rowR["C73"] = row1["ForwarderReference"].ToString(); ;

                rowR["C20"] = row1["MarkNoContainerNo"].ToString();
                rowR["C21"] = row1["NumberandKindofPackages"].ToString();


                try
                {
                    string Goods = "";
                    int lenGoods = row1["DescriptionofGoodsPackages"].ToString().Length;
                    if (lenGoods < 400)
                    {
                        Goods = row1["DescriptionofGoodsPackages"].ToString();
                        Goods = Goods.PadRight(400 - lenGoods, '.');
                        Goods = Goods + ".";
                    }
                    if (lenGoods > 400)
                    {
                        Goods = row1["DescriptionofGoodsPackages"].ToString().Substring(0, 400);
                    }
                }
                catch (Exception)
                {
                }

                rowR["C22"] = row1["DescriptionofGoodsPackages"].ToString();
                rowR["C23"] = Convert.ToString(TotalGrossWt) + "\r\n" + Convert.ToString(TotalNetWt); // "Net Weight:" + "\r\n" + Convert.ToString(TotalNetWt);
                rowR["C55"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["portofloading"].ToString());
                rowR["C56"] = "CONTINUED ON FOLLOWING PAGE";
                rowR["C59"] = GetDescriptionDDL1(row1["overseas"].ToString());
                //rowR["C83"] = "GW: "+Convert.ToString(TotalGrossWt);
                if (TotalGrossWt > 0)
                {
                    // rowR["C84"] = rowR["C84"] + " " + GetDescriptionDDL(row1["wtunit"].ToString());
                }
                // rowR["C85"] = "NW: " + Convert.ToString(TotalNetWt);
                // rowR["C86"] = Convert.ToString(TotalNetWt);
                if (TotalNetWt > 0)
                {
                    //  rowR["C86"] = rowR["C86"] + " " + GetDescriptionDDL(row1["wtunit"].ToString());
                }

                rowR["C24"] = Convert.ToString(TotalMeasure); ; // rowCh["Measurement"].ToString();

            }
        }
        catch (Exception ex)
        {
        }

        return rptDataSet;
    }

    public DataSet MoveInRptDsContainer(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            decimal TotalGrossWt = 0; decimal TotalMeasure = 0; decimal TotalNetWt = 0;
            int Ctr = 1; decimal NoOfBL = 0;

            #region Consignee
            string client = "";
            DataRow row11 = InputDataSet.Tables[0].Rows[0];
            MyMain oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyAccountexpense = new MyMain();
                oMyAccountexpense.Fld1 = row11["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyAccountexpense.Fld2 = row11["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyAccountexpense.Fld3 = row11["client"].ToString();

                dsConsignee = oMyAccountexpense.GetAccountnCity();
                if (dsConsignee != null)
                {
                    client = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    ///oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    //oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    //Consignee = client + "\r\n" + dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {
            }

            #endregion

            #region shipper
            string Principalcode = "";
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row11["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row11["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row11["Principalcode"].ToString();

                dsConsignee = oMyConsignee.GetAccountnCity();
                if (dsConsignee != null)
                {
                    Principalcode = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    // oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    //oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    // Principalcode = Principalcode + "\r\n" + dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion
            string ConID = ""; decimal Tot20 = 0; decimal Tot40 = 0; decimal TotGrsWt = 0;
            decimal TotNetWt = 0; decimal TotCBM = 0; decimal TotPkgs = 0; decimal Tot45 = 0;

            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                TotalGrossWt = TotalGrossWt + UType.MyCtoD(row1["GrossWt"].ToString());
                TotalNetWt = TotalNetWt + UType.MyCtoD(row1["NetWt"].ToString());
                //TotalMeasure = TotalMeasure + UType.MyCtoD(row1["Measurement"].ToString());
                row["C2"] = row1["SNo"];
                row["C4"] = "SIJ-" + UType.MyFormat4(row1["JobNo"].ToString()) + "-" + UType.GetYYYY(); //row1["JobNo"];
                row["C5"] = UType.GetDate(row1["ArrivalDate"].ToString());
                row["C6"] = row1["hBLNO"];
                row["C7"] = row1["Vessel"].ToString();
                row["C8"] = row1["Voyage"].ToString();
                row["C9"] = client; // row1["client"].ToString();
                row["C10"] = GetDescriptionDDL(row1["Principal"].ToString());//    Principalcode; // row1["Principalcode"].ToString();
                row["C11"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["PortLoading"].ToString());
                row["C12"] = GetDescriptionDDL(row1["TerminalID"].ToString());
                row["C13"] = row1["ContainerNo"].ToString();
                row["C14"] = row1["seal"].ToString();
                row["C15"] = GetDescriptionDDL(row1["Sizentype"].ToString()); // row1["sizentype"].ToString()  ;
                row["C16"] = row1["grosswt"].ToString();
                TotGrsWt = TotGrsWt + UType.MyCtoD(row1["grosswt"].ToString());
                row["C17"] = row1["NetWt"].ToString();
                TotNetWt = TotNetWt + UType.MyCtoD(row1["NetWt"].ToString());
                row["C18"] = row1["CBM"].ToString();
                TotCBM = TotCBM + UType.MyCtoD(row1["CBM"].ToString());
                row["C19"] = row1["Package"].ToString();
                TotPkgs = TotPkgs + UType.MyCtoD(row1["Package"].ToString());
                row["C20"] = GetDescriptionDDL(row1["Unit"].ToString());
                row["C28"] = NoOfBL.ToString();

                try
                {
                    ConID = row["C15"].ToString().Substring(0, 2);
                }
                catch (Exception ex)
                {
                }

                if (ConID == "20")
                {
                    Tot20 = Tot20 + 1;
                }
                if (ConID == "40")
                {
                    Tot40 = Tot40 + 1;
                }
                if (ConID == "45")
                {
                    Tot45 = Tot45 + 1;
                }
                Ctr = Ctr + 1;
                //AddRow = 0;
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
            }

            decimal SNoCtr = 1;
            foreach (DataRow row in rptDataSet.Tables[0].Rows)
            {
                row["C3"] = SNoCtr.ToString();
                row["C21"] = Convert.ToString(Tot20);
                row["C22"] = Convert.ToString(Tot40);
                row["C23"] = Convert.ToString(Tot45);
                row["C24"] = "";
                if (Tot20 > 0)
                {
                    row["C24"] = Convert.ToString(Tot20 * 2);
                }
                row["C25"] = Convert.ToString(TotGrsWt);
                row["C26"] = Convert.ToString(TotNetWt);
                row["C27"] = Convert.ToString(TotCBM);
                row["C28"] = Convert.ToString(TotPkgs);
                SNoCtr = SNoCtr + 1;
            }
            #region shipperBL
            string shipperBL = "";
            oMy = new MyMain();
            try
            {
                DataSet dsshipperBL = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row11["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row11["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row11["shipper"].ToString();

                dsshipperBL = oMyConsignee.GetAccountnCity();
                if (dsshipperBL != null)
                {
                    shipperBL = dsshipperBL.Tables[0].Rows[0]["ActDesc"].ToString();
                    oMy.Fld10 = dsshipperBL.Tables[0].Rows[0]["country_name"].ToString();
                    oMy.Fld11 = oMy.Fld10 + dsshipperBL.Tables[0].Rows[0]["city_name"].ToString();
                    oMy.Fld12 = dsshipperBL.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion




        }
        catch (Exception ex)
        {
        }

        return rptDataSet;
    }

    public DataSet MoveInRptDsArrival(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            decimal TotalGrossWt = 0; decimal TotalMeasure = 0; decimal TotalNetWt = 0;
            int Ctr = 1; decimal NoOfBL = 0;

            #region Consignee
            string client = "";
            DataRow row11 = InputDataSet.Tables[0].Rows[0];
            MyMain oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyAccountexpense = new MyMain();
                oMyAccountexpense.Fld1 = row11["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyAccountexpense.Fld2 = row11["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyAccountexpense.Fld3 = row11["client"].ToString();

                dsConsignee = oMyAccountexpense.GetAccountnCity();
                if (dsConsignee != null)
                {
                    client = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    ///oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    //oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    client = client + "\r\n" + dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {
            }

            #endregion

            #region shipper
            string Principalcode = "";
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row11["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row11["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row11["Principalcode"].ToString();

                dsConsignee = oMyConsignee.GetAccountnCity();
                if (dsConsignee != null)
                {
                    Principalcode = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    // oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    //oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    // Principalcode = Principalcode + "\r\n" + dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion
            string ConID = ""; decimal Tot20 = 0; decimal Tot40 = 0; decimal TotGrsWt = 0;
            decimal TotNetWt = 0; decimal TotCBM = 0; decimal TotPkgs = 0; decimal Tot45 = 0;

            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                TotalGrossWt = TotalGrossWt + UType.MyCtoD(row1["GrossWt"].ToString());
                TotalNetWt = TotalNetWt + UType.MyCtoD(row1["ManualNetWt"].ToString()); //NetWt
                //TotalMeasure = TotalMeasure + UType.MyCtoD(row1["Measurement"].ToString());
                row["C11"] = client;
                row["C12"] = "SIJ-" + UType.MyFormat4(row1["JobNo"].ToString()) + "-" + UType.GetYYYY();
                row["C13"] = UType.GetCDate();  //UType.SetDate(row1["ArrivalDate"].ToString());
                row["C14"] = row1["Vessel"].ToString();
                row["C15"] = row1["Voyage"].ToString();
                row["C16"] = UType.GetDate(row1["ArrivalDate"].ToString());
                row["C17"] = row1["hBLNO"];
                row["C18"] = row1["indexno"];
                row["C19"] = row1["igmno"];
                row["C20"] = GetCity(row1["OfficeId"].ToString(), row1["ProjectId"].ToString(), row1["PortLoading"].ToString());


                row["C3"] = row1["MarkNoContainerNo"].ToString();
                row["C4"] = row1["Package"].ToString();
                string GoodsMsg = ""; string Goods = "";
                try
                {

                    int lenGoods = row1["DescriptionofGoodsPackages"].ToString().Length;
                    if (lenGoods < 400)
                    {
                        Goods = row1["DescriptionofGoodsPackages"].ToString();
                        Goods = Goods.PadRight(400 - lenGoods, '.');
                        Goods = Goods + ".";
                    }
                    if (lenGoods > 400)
                    {
                        Goods = row1["DescriptionofGoodsPackages"].ToString().Substring(0, 400);
                        //GoodsMsg = "CONTINUED ON FOLLOWING PAGE";
                    }
                }
                catch (Exception)
                {
                }
                row["C6"] = Goods;

                //
                row["C21"] = row1["ContainerNo"].ToString();
                // row["C14"] = row1["seal"].ToString();
                //row["C15"] = GetDescriptionDDL(row1["Sizentype"].ToString()); // row1["sizentype"].ToString()  ;

                //AddRow = 0;
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
            }


            //foreach (DataRow row in rptDataSet.Tables[0].Rows)
            // {

            //row["C23"] = Convert.ToString(Tot45);
            // row["C24"] = "";

            // row["C28"] = Convert.ToString(TotPkgs);

            //  }





        }
        catch (Exception ex)
        {
        }

        return rptDataSet;
    }
    public DataSet MoveInRptDsBLAttach(DataSet InputDataSet)
    {
        string Goods = "";
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {




            foreach (DataRow rowR in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = "BL Attach Sheet"; //"MULTIMODAL TRANSPORT BILL OF LADING";

                row["C11"] = rowR["HBL"].ToString();


                row["C12"] = rowR["MarkNoContainerNo"].ToString();



                //row["C13"] = rowR["DescriptionofGoodsPackages"].ToString();
                try
                {
                    Goods = "";
                    int lenGoods = rowR["DescriptionofGoodsPackages"].ToString().Length;
                    //if (lenGoods < 400)
                    //{
                    //    Goods = rowR["DescriptionofGoodsPackages"].ToString();
                    //    Goods = Goods.PadRight(400 - lenGoods, '.');
                    //    Goods = Goods + ".";
                    //}
                    if (lenGoods > 400)
                    {
                        Goods = rowR["DescriptionofGoodsPackages"].ToString().Substring(400, lenGoods - 400);
                    }
                }
                catch (Exception)
                {
                }
                row["C13"] = Goods;
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);

            }
            foreach (DataRow row in rptDataSet.Tables[0].Rows)
            {
                decimal TotalGrossWt = 0; decimal TotalNetWt = 0; decimal TotalMeasure = 0;
                decimal Ctr = 1;
                foreach (DataRow rowCh in InputDataSet.Tables[1].Rows)
                {
                    TotalGrossWt = TotalGrossWt + UType.MyCtoD(rowCh["GrossWt"].ToString());
                    TotalNetWt = TotalNetWt + UType.MyCtoD(rowCh["NetWt"].ToString());
                    TotalMeasure = TotalMeasure + UType.MyCtoD(rowCh["Measurement"].ToString());
                    row["C2"] = rowCh["SNo"];
                    row["C13"] = rowCh["NotifyParty1"];
                    row["C84"] = GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    row["C86"] = GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                    row["C98"] = "N";
                    if (Ctr == 1)
                    {
                        row["C30"] = rowCh["ContainerNo"].ToString();
                        row["C31"] = GetDescriptionDDL(rowCh["SizeNtype"].ToString());  // rowCh["SizeNtype"].ToString();
                        row["C32"] = rowCh["Seal"].ToString();
                        row["C33"] = rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                        row["C34"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                        row["C35"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                    }

                    if (Ctr == 2)
                    {

                        row["C30"] = row["C30"] + "\r\n" + rowCh["ContainerNo"].ToString();

                        row["C31"] = row["C31"] + "\r\n" + GetDescriptionDDL(rowCh["SizeNtype"].ToString());
                        //row["C38"] = rowCh["Seal"].ToString();
                        row["C32"] = row["C32"] + "\r\n" + rowCh["Seal"].ToString();
                        row["C33"] = row["C33"] + "\r\n" + rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                        //row["C40"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                        row["C34"] = row["C34"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                        //row["C89"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                        row["C35"] = row["C35"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());

                    }
                    if (Ctr == 3)
                    {
                        //row["C87"] = rowCh["ContainerNo"].ToString();
                        row["C30"] = row["C30"] + "\r\n" + rowCh["ContainerNo"].ToString();
                        //row["C88"] = GetDescriptionDDL(rowCh["SizeNtype"].ToString()); //rowCh["SizeNtype"].ToString();
                        row["C31"] = row["C31"] + "\r\n" + GetDescriptionDDL(rowCh["SizeNtype"].ToString());
                        // row["C74"] = rowCh["Seal"].ToString();
                        row["C32"] = row["C32"] + "\r\n" + rowCh["Seal"].ToString();
                        row["C33"] = row["C33"] + "\r\n" + rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                        row["C75"] = "";// rowCh["GrossWt"].ToString();
                                        //row["C76"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                        row["C34"] = row["C34"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                        //row["C77"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                        row["C35"] = row["C35"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());

                    }
                    if (Ctr == 4)
                    {
                        // row["C78"] = rowCh["ContainerNo"].ToString();
                        row["C30"] = row["C30"] + "\r\n" + rowCh["ContainerNo"].ToString();
                        //row["C79"] = GetDescriptionDDL(rowCh["SizeNtype"].ToString()); //rowCh["SizeNtype"].ToString();
                        row["C31"] = row["C31"] + "\r\n" + GetDescriptionDDL(rowCh["SizeNtype"].ToString());
                        // row["C80"] = rowCh["Seal"].ToString();
                        row["C32"] = row["C32"] + "\r\n" + rowCh["Seal"].ToString();
                        row["C33"] = row["C33"] + "\r\n" + rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                        row["C81"] = "";// rowCh["GrossWt"].ToString();
                                        //row["C82"] = rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                        row["C34"] = row["C34"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());
                        //row["C83"] = rowCh["NetWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString()); ;
                        row["C35"] = row["C35"] + "\r\n" + rowCh["GrossWt"].ToString() + "  " + GetDescriptionDDL(rowCh["Netwtunit"].ToString());

                    }


                    Ctr = Ctr + 1;
                    //AddRow = 0;
                }

            }
        }
        catch (Exception ex)
        {
        }

        return rptDataSet;
    }
    public DataSet MoveInRptDsLoading(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            DataRow row1 = InputDataSet.Tables[0].Rows[0];
            // DataRow rowMBL = InputDataSet.Tables["TblMBL"].Rows[0];
            string Shipper = ""; string Consignee = "";






            #region Accountexpense
            MyMain oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyAccountexpense = new MyMain();
                oMyAccountexpense.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyAccountexpense.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyAccountexpense.Fld3 = row1["consignee"].ToString();

                dsConsignee = oMyAccountexpense.GetAccountExpense1();
                if (dsConsignee != null)
                {
                    Consignee = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {
            }
            #endregion

            #region shipper
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["shipper"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    Shipper = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion
            decimal TotalGrossWt = 0; decimal TotalMeasure = 0;
            foreach (DataRow rowCh in InputDataSet.Tables[1].Rows)
            {
                TotalGrossWt = TotalGrossWt + UType.MyCtoD(rowCh["GrossWt"].ToString());
                TotalMeasure = TotalMeasure + UType.MyCtoD(rowCh["Measurement"].ToString());
            }

            foreach (DataRow rowCh in InputDataSet.Tables[1].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                //qq
                row["C91"] = "AXL CONTAINER LINE PVT LTD"; //"MULTIMODAL TRANSPORT BILL OF LADING";
                row["C92"] = "Suite 603,6th Floor, Kawish Crown Plaza | Sharah-E-Faisal | Karachi | Pakistan"; //"MULTIMODAL TRANSPORT BILL OF LADING";
                row["C93"] = "Tel: +92 21 34383810-11-12 Fax: +92 21 32414388";
                row["C94"] = "Email: info@alliedxl.com Web: www.alliedxl.com";
                row["C11"] = "The Assistant Traffic Manager";
                row["C12"] = "Karachi";
                row["C14"] = UType.GetDate1(row1["JobDate"].ToString());
                row["C15"] = "SEJ-" + UType.MyFormat4(row1["JobNo"].ToString()) + "-" + UType.GetYYYY();
                row["C16"] = row1["vessel"];
                row["C17"] = row1["Voyage"];
                row["C18"] = row1["docno"]; //row1["egmno"];
                //row["C19"] = GetDescriptionDDL(row1["BerthName"].ToString()); //row1["BerthName"]; 
                row["C20"] = GetDescriptionDDL(row1["wharf"].ToString());
                row["C21"] = GetDescriptionDDL(row1["sline"].ToString());
                row["C22"] = UType.GetDate1(row1["eta"].ToString());
                row["C72"] = UType.GetDate1(row1["PreAlertDate"].ToString());

                row["C23"] = UType.GetDate1(row1["GroundDate"].ToString());
                row["C24"] = Shipper; // Convert.ToString(TotalMeasure); 
                row["C25"] = GetDescriptionDDL(row1["CustomClearance"].ToString());   //Consignee; // rowCh["Consignee"].ToString();  //Consignee;
                row["C26"] = rowCh["Package"].ToString() + " " + GetDescriptionDDL(rowCh["PackageUnit"].ToString());
                row["C27"] = GetDescriptionDDL(row1["Commodity"].ToString());
                row["C28"] = UType.GetDate1(row1["ActualETD"].ToString());
                row["C29"] = row1["ActualETA"].ToString();
                row["C30"] = GetCity_Name(row1["PortDischarge"].ToString());  //Consignee;
                row["C31"] = GetCity_Name(row1["ViaPort"].ToString());
                row["C32"] = rowCh["Seal"].ToString();
                row["C33"] = rowCh["Package"].ToString();
                row["C34"] = rowCh["GrossWt"].ToString();
                row["C35"] = rowCh["NetWt"].ToString();
                row["C73"] = rowCh["TracingNotes"].ToString();


                //row["CLogo"] = GetImageBLRpt(rowCh["IndexType"].ToString());
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);

            }
        }
        catch (Exception ex)
        {
        }

        return rptDataSet;

    }

    private string GetDescriptionCust(string poffice, string pProject, string pCustomerId)
    {
        string retVal = "";
        if (UType.MyCtoD(pCustomerId) > 0)
        {
            try
            {
                DataSet dsResult = null;

                Fld1 = poffice;
                Fld2 = pProject;
                Fld3 = pCustomerId;
                //oMy.Fld4 = pStatusId;

                dsResult = GetCustomerDescription();

                if (dsResult != null)
                {
                    retVal = dsResult.Tables[0].Rows[0]["CustomerName"].ToString();
                }
            }
            catch (Exception ex)
            {
            }

        }
        return retVal;
    }
    private string GetMaxInvoiceNoCon(string OfficeID, string ProjectID, string JobCy, string Consignee, string JobNo)
    {
        decimal retVal = 0;
        try
        {

            MyDb oMyDb = new MyDb(this);
            Fld1 = OfficeID;
            Fld2 = ProjectID;
            Fld3 = JobCy;
            Fld4 = Consignee;

            DataSet result = oMyDb.GetMaxInvoiceCon();
            if (result != null)
            {
                if (result.Tables[0].Rows.Count > 0)
                {
                    retVal = UType.MyCtoD(result.Tables[0].Rows[0]["MaxInvoiceNo"].ToString());
                }
            }
            retVal = retVal + 1;
            //update
            oMyDb = new MyDb(this);
            Fld1 = OfficeID;
            Fld2 = ProjectID;
            Fld3 = JobNo;
            Fld4 = Consignee;
            Fld5 = retVal.ToString();

            string result1 = oMyDb.UpdateMaxInvoice();

        }
        catch (Exception ex)
        {

        }


        return retVal.ToString();

    }

    public DataSet MoveInRptDsDetInvoice(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row1 = InputDataSet.Tables[0].Rows[0];
            DataRow rowCh = InputDataSet.Tables["TblCh"].Rows[0];

            string ArrivalDate = UType.GetDateTxt(row1["ArrivalDate"].ToString());  //GetActCity1(row1["PortLoading"].ToString());
            string Portofloading = GetActCity1(row1["Portloading"].ToString());
            string PortDischarge = GetActCity1(row1["PortDischarge"].ToString());
            string FinalDestination = GetActCity2(row1["FinalDestination"].ToString());
            string ShippingCompanyID = GetDescriptionDDL(row1["ShippingCompanyID"].ToString());

            string ShipperName = "";
            string ConsigneeName = ""; string Consigne = "";
            decimal LocalAmount = 0;
            #region ConsigneeName
            MyMain oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["Consignee"].ToString();
                Consigne = row1["Consignee"].ToString();
                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ConsigneeName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion

            #region ShipperName
            oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["Shipper"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ShipperName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion
            //
            decimal TotalOtherCharges = 0; decimal TotalDet = 0; decimal TotalAmount = 0; decimal TotalFC = 0; decimal TotalFCpkr = 0; decimal TotalDiscount = 0;
            decimal TotalNetAmount = 0; decimal DetDays = 0; decimal TotalPlugin = 0;
            foreach (DataRow rowEq in InputDataSet.Tables["TblEq"].Rows)
            {
                TotalDet = TotalDet + UType.MyCtoD(rowEq["totaldetention"].ToString());
                TotalFCpkr = TotalFCpkr + (UType.MyCtoD(rowEq["totaldetention"].ToString()) * UType.MyCtoD(rowEq["exrate"].ToString()));
                TotalPlugin = TotalPlugin + UType.MyCtoD(rowEq["plugin"].ToString());

                TotalOtherCharges = TotalOtherCharges + UType.MyCtoD(rowEq["WashingCharges"].ToString()) + UType.MyCtoD(rowEq["DocumentCharges"].ToString()) + UType.MyCtoD(rowEq["Demurrage"].ToString()) + UType.MyCtoD(rowEq["Damagecharges"].ToString());

                TotalAmount = TotalAmount + (UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString())) * UType.MyCtoD(rowEq["exrate"].ToString());
                DetDays = UType.MyCtoD(rowEq["logdays"].ToString()) - UType.MyCtoD(rowEq["logdays"].ToString());
                //TotalFC = TotalFC + (UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()));

                TotalDiscount = TotalDiscount + (UType.MyCtoD(rowEq["Discount"].ToString()) * UType.MyCtoD(rowEq["exrate"].ToString()));
                TotalNetAmount = TotalNetAmount + (UType.MyCtoD(rowEq["totaldetention"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString()) * UType.MyCtoD(rowEq["exrate"].ToString()));
            }
            foreach (DataRow rowEq in InputDataSet.Tables["TblEq"].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();

                oMy = new MyMain();
                try
                {
                    row["C11"] = "DET " + "-" + rowEq["jobno"].ToString() + "/" + rowEq["jobcy"].ToString();
                    row["C12"] = UType.GetDateTxt(rowEq["InvoiceDate"].ToString());
                    if (UType.MyCtoD(rowEq["InvoiceNo"].ToString()) < 1)
                    {
                        //row["C11"] = "JP " + "-" + GetMaxInvoiceNoCon(rowEq["officeid"].ToString(), rowEq["Projectid"].ToString(), UType.GetCY(), Consigne, rowEq["jobno"].ToString());
                        string cDate = DateTime.Now.ToString("yyyyMMdd");
                        row["C12"] = UType.GetDateTxt(cDate);
                    }

                    row["C13"] = ConsigneeName;
                    row["C14"] = UType.GetDateTxt(rowEq["ContainerReturnDate"].ToString());
                    row["C15"] = ShipperName;
                    row["C16"] = UType.GetDateTxt(row1["IGMDate"].ToString());
                    row["C17"] = Portofloading;
                    row["C18"] = ConsigneeName;
                    row["C19"] = PortDischarge;
                    row["C20"] = FinalDestination;
                    row["C21"] = rowEq["ExRate"].ToString();
                    row["C22"] = row1["HBLNo"].ToString();
                    row["C23"] = row1["MBLNo"].ToString();// row1["Vessel"].ToString();
                                                          //if (row1["Voyage"].ToString().Length > 0)
                                                          //{
                    row["C24"] = row1["Vessel"].ToString() + " / " + row1["Voyage"].ToString() + " / " + PortDischarge;   // row["C23"].ToString() + " / " + row1["Voyage"].ToString();
                    //}
                    //row["C24"] = UType.GetDateTxt(row1["jobDate"].ToString());
                    row["C25"] = row1["IndexNo"].ToString();
                    row["C26"] = "PKR";//GetCurrencyString(rowCh["currency"].ToString());
                    // row["C26"] = rowCh["jobno"].ToString();
                    row["C27"] = UType.GetDateTxt(row1["ArrivalDate"].ToString());
                    row["C28"] = UType.GetDateTxt(row1["ArrivalDate"].ToString());  //UType.GetDateTxt(rowEq["ContainerEntryDate"].ToString());
                    row["C29"] = UType.GetDateTxt(rowEq["ContainerReturnDate"].ToString()); ;
                    row["C30"] = "We enclose an Invoice for Container detention charges under mentioned:";
                    row["C4"] = "Detention";
                    row["C5"] = rowEq["ContainerNo"].ToString();
                    row["C6"] = rowEq["Detention"].ToString();  //rowEq["IsManual1"].ToString();
                    row["C7"] = rowEq["DetHdrFreeDays"].ToString(); //LogDays(rowEq["ContainerEntryDate"].ToString(), rowEq["ContainerReturnDate"].ToString());
                    row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                    row["C9"] = GetCurrencyString(rowEq["curren"].ToString());
                    row["C10"] = UType.MyFormat(rowEq["totaldetention"].ToString());  //rowEq["SecurityAmountReceive"].ToString();
                    row["C31"] = rowEq["Discount"].ToString();  // rowEq["SecurityAmountPaid"].ToString();
                                                                //row["C33"] = UType.MyFormat((UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString())) * UType.MyCtoD(rowEq["exrate"].ToString()));
                    row["C33"] = UType.MyFormat((UType.MyCtoD(rowEq["totaldetention"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString())) * UType.MyCtoD(rowEq["exrate"].ToString()));
                    row["C34"] = UType.MyFormat(TotalFC.ToString());


                    row["C36"] = UType.MyFormat(TotalFCpkr.ToString());
                    row["C37"] = "";
                    row["C38"] = UType.MyFormat(TotalOtherCharges.ToString());
                    //row["C39"] = UType.MyFormat(TotalAmount.ToString());
                    row["C40"] = UType.MyFormat(TotalDiscount.ToString());
                    row["C51"] = UType.MyFormat(TotalNetAmount.ToString());
                    row["C53"] = "PKR " + UType.NumberToWords(UType.MyCtoD(TotalNetAmount.ToString())) + " Only";
                    //row["C4"] = UType.MyFormat(UType.MyCtoD(rowEq["WashingCharges"].ToString()) + UType.MyCtoD(rowEq["DocumentCharges"].ToString()) + UType.MyCtoD(rowEq["Demurrage"].ToString()));
                    //row["C4"] = rowEq["WashingCharges"].ToString();
                    if (UType.MyCtoD(rowEq["PrincipalCode"].ToString()) > 0)  // if (UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()) > 0)
                    {
                        row["C4"] = "Detention";
                        row["C5"] = rowEq["ContainerNo"].ToString();
                        row["C10"] = UType.MyFormat(rowEq["PrincipalEquipInv"].ToString());
                        // if (UType.MyCtoD(rowEq["TotalDetention"].ToString()) > 0)
                        // {
                        //   row["CLogo"] = GetImage();
                        //   rptDataSet.Tables[0].Rows.Add(row);
                        //  row = rptDataSet.Tables[0].NewRow();
                        // row["C4"] = "Total Detention";
                        row["C5"] = rowEq["ContainerNo"].ToString();
                        row["C10"] = UType.MyFormat(rowEq["TotalDetention"].ToString());
                        row["C6"] = rowEq["DetHdrFreeDays"].ToString(); //"0";
                        row["C7"] = rowEq["logdays"].ToString(); //"0";
                        row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                        //row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                        // row["C31"] = "0";
                        row["C32"] = "0";
                        //row["C33"] = UType.MyFormat(rowEq["TotalDetention"].ToString());
                        // }
                        if (UType.MyCtoD(rowEq["Damagecharges"].ToString()) > 0) //
                        {
                            row["CLogo"] = GetImage();
                            rptDataSet.Tables[0].Rows.Add(row);
                            row = rptDataSet.Tables[0].NewRow();
                            row["C4"] = "Damage Charges";
                            row["C5"] = rowEq["ContainerNo"].ToString();
                            row["C10"] = UType.MyFormat(rowEq["Damagecharges"].ToString());
                            row["C6"] = "0";  //rowEq["IsManual1"].ToString();
                            row["C7"] = rowEq["DetHdrFreeDays"].ToString(); //"0";;
                            row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                            row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                            // row["C31"] = "0";
                            row["C32"] = "0";
                            //row["C33"] = UType.MyFormat(rowEq["Demurrage"].ToString());
                        }
                        if (UType.MyCtoD(rowEq["DocumentCharges"].ToString()) > 0)
                        {
                            row["CLogo"] = GetImage();
                            rptDataSet.Tables[0].Rows.Add(row);
                            row = rptDataSet.Tables[0].NewRow();
                            row["C4"] = "DocumentCharges";
                            row["C5"] = rowEq["ContainerNo"].ToString();
                            row["C10"] = UType.MyFormat(rowEq["DocumentCharges"].ToString());
                            row["C6"] = "0";  //rowEq["IsManual1"].ToString();
                            row["C7"] = "0";
                            row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                            row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                            row["C31"] = "0";
                            row["C32"] = "0";
                            //row["C33"] = UType.MyFormat(rowEq["DocumentCharges"].ToString());
                        }
                        if (UType.MyCtoD(rowEq["WashingCharges"].ToString()) > 0)
                        {
                            row["CLogo"] = GetImage();
                            rptDataSet.Tables[0].Rows.Add(row);
                            row = rptDataSet.Tables[0].NewRow();
                            row["C4"] = "WashingCharges";
                            row["C5"] = rowEq["ContainerNo"].ToString();
                            row["C10"] = UType.MyFormat(rowEq["WashingCharges"].ToString());
                            row["C6"] = "0";  //rowEq["IsManual1"].ToString();
                            row["C7"] = "0";
                            row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                            row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                            row["C31"] = "0";
                            row["C32"] = "0";
                            //row["C33"] = UType.MyFormat(rowEq["DocumentCharges"].ToString());
                        }
                    }


                    row["CLogo"] = GetImage();
                    rptDataSet.Tables[0].Rows.Add(row);
                    decimal Total33 = 0; decimal DetentionFC = 0;
                    foreach (DataRow row123 in rptDataSet.Tables[0].Rows)
                    {
                        if (row123["C4"].ToString() != "Detention")
                        {
                            row123["C33"] = row123["C10"];
                            row123["C9"] = "PKR";
                        }
                        if (row123["C4"].ToString() == "Detention")
                        {
                            DetentionFC = DetentionFC + UType.MyCtoD(row123["C10"].ToString());
                        }

                    }
                    foreach (DataRow row123 in rptDataSet.Tables[0].Rows)
                    {
                        Total33 = Total33 + UType.MyCtoD(row123["C33"].ToString());

                    }
                    int SNo = 1;
                    foreach (DataRow row123 in rptDataSet.Tables[0].Rows)
                    {
                        row123["C3"] = SNo.ToString();
                        row123["C10"] = UType.MyFormat(row123["C10"].ToString());
                        row123["C33"] = UType.MyFormat(row123["C33"].ToString());
                        row123["C39"] = UType.MyFormat(Total33.ToString());
                        row123["C51"] = UType.MyFormat(Total33.ToString());
                        row123["C53"] = "PKR " + UType.NumberToWords(UType.MyCtoD(Total33.ToString())) + " Only";
                        row123["C34"] = UType.MyFormat(DetentionFC.ToString());
                        SNo = SNo + 1;
                    }
                }

                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            //

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        try
        {
            decimal Total33 = 0;
            foreach (DataRow row123 in rptDataSet.Tables[0].Rows)
            {
                Total33 = Total33 + UType.MyCtoD(row123["C33"].ToString());

            }
            foreach (DataRow row in rptDataSet.Tables[0].Rows)
            {
                row["C39"] = UType.MyFormat(Total33.ToString());

            }
        }
        catch (Exception)
        {


        }
        return rptDataSet;

    }

    public DataSet MoveInRptDsDetInvoiceDtl(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row1 = InputDataSet.Tables[0].Rows[0];
            DataRow rowCh = InputDataSet.Tables["TblCh"].Rows[0];

            string ArrivalDate = UType.GetDateTxt(row1["ArrivalDate"].ToString());  //GetActCity1(row1["PortLoading"].ToString());
            string Portofloading = GetActCity1(row1["Portloading"].ToString());
            string PortDischarge = GetActCity1(row1["PortDischarge"].ToString());
            string FinalDestination = GetActCity2(row1["FinalDestination"].ToString());
            string ShippingCompanyID = GetDescriptionDDL(row1["ShippingCompanyID"].ToString());

            string ShipperName = "";
            string ConsigneeName = ""; string Consigne = "";
            decimal LocalAmount = 0;
            #region ConsigneeName
            MyMain oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["Consignee"].ToString();
                Consigne = row1["Consignee"].ToString();
                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ConsigneeName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion

            #region ShipperName
            oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["Shipper"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ShipperName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {
                //throw;

            }
            #endregion
            //
            decimal TotalOtherCharges = 0; decimal TotalDet = 0; decimal TotalAmount = 0; decimal TotalFC = 0; decimal TotalFCpkr = 0; decimal TotalDiscount = 0;
            decimal TotalNetAmount = 0; decimal DetDays = 0; decimal TotalPlugin = 0;

            try
            {
                foreach (DataRow rowEq in InputDataSet.Tables["TblEq"].Rows)
                {
                    TotalDet = TotalDet + UType.MyCtoD(rowEq["totaldetention"].ToString());
                    TotalFCpkr = TotalFCpkr + (UType.MyCtoD(rowEq["totaldetention"].ToString()) * UType.MyCtoD(rowEq["exrate"].ToString()));
                    TotalPlugin = TotalPlugin + UType.MyCtoD(rowEq["plugin"].ToString());

                    TotalOtherCharges = TotalOtherCharges + UType.MyCtoD(rowEq["WashingCharges"].ToString()) + UType.MyCtoD(rowEq["DocumentCharges"].ToString()) + UType.MyCtoD(rowEq["Demurrage"].ToString()) + UType.MyCtoD(rowEq["Damagecharges"].ToString());

                    TotalAmount = TotalAmount + (UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString())) * UType.MyCtoD(rowEq["exrate"].ToString());
                    DetDays = UType.MyCtoD(rowEq["logdays"].ToString()) - UType.MyCtoD(rowEq["logdays"].ToString());
                    //TotalFC = TotalFC + (UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()));

                    TotalDiscount = TotalDiscount + (UType.MyCtoD(rowEq["Discount"].ToString()) * UType.MyCtoD(rowEq["exrate"].ToString()));
                    TotalNetAmount = TotalNetAmount + (UType.MyCtoD(rowEq["totaldetention"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString()) * UType.MyCtoD(rowEq["exrate"].ToString()));
                }
            }
            catch (Exception ex)
            {

                //throw;
            }

            try
            {
                foreach (DataRow rowEq in InputDataSet.Tables["TblEq"].Rows)
                {
                    DataRow row = rptDataSet.Tables[0].NewRow();

                    try
                    {
                        oMy = new MyMain();

                        row["C1"] = "DET " + "-" + rowEq["jobno"].ToString() + "/" + rowEq["jobcy"].ToString();
                        row["C2"] = UType.GetDateTxt(rowEq["InvoiceDate"].ToString());
                        row["C3"] = row1["IGMNo"].ToString();

                        row["C13"] = ConsigneeName;
                        row["C14"] = UType.GetDateTxt(rowEq["ContainerReturnDate"].ToString());
                        row["C15"] = ShipperName;
                        row["C16"] = UType.GetDateTxt(row1["IGMDate"].ToString());
                        row["C17"] = Portofloading;
                        row["C18"] = ConsigneeName;
                        row["C19"] = PortDischarge;
                        row["C20"] = FinalDestination;
                        row["C21"] = rowEq["ExRate"].ToString();
                        row["C22"] = row1["HBLNo"].ToString();
                        row["C23"] = row1["MBLNo"].ToString();
                        row["C24"] = row1["Vessel"].ToString() + " / " + row1["Voyage"].ToString() + " / " + PortDischarge;   // row["C23"].ToString() + " / " + row1["Voyage"].ToString();

                        row["C25"] = row1["IndexNo"].ToString();
                        row["C26"] = "PKR";//GetCurrencyString(rowCh["currency"].ToString());

                        row["C27"] = UType.GetDateTxt(row1["ArrivalDate"].ToString());
                        row["C28"] = UType.GetDateTxt(rowEq["ContainerArrvalDate"].ToString());
                        row["C29"] = UType.GetDateTxt(rowEq["ContainerReturnDate"].ToString()); ;
                        row["C30"] = "We enclose an Invoice for Container detention charges under mentioned:";
                        row["C4"] = "Detention";
                        row["C5"] = rowEq["ContainerNo"].ToString();
                        row["C6"] = rowEq["Detention"].ToString();  //rowEq["IsManual1"].ToString();
                        row["C7"] = rowEq["DetHdrFreeDays"].ToString(); //LogDays(rowEq["ContainerEntryDate"].ToString(), rowEq["ContainerReturnDate"].ToString());
                        row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                        row["C9"] = GetCurrencyString(rowEq["curren"].ToString());
                        row["C10"] = UType.GetDateTxt(rowEq["ContainerReturnDate"].ToString());  //UType.MyFormat(rowEq["totaldetention"].ToString());  //rowEq["SecurityAmountReceive"].ToString();
                        row["C31"] = rowEq["Discount"].ToString();  // rowEq["SecurityAmountPaid"].ToString();
                                                                    //row["C33"] = UType.MyFormat((UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString())) * UType.MyCtoD(rowEq["exrate"].ToString()));
                        row["C33"] = UType.MyFormat((UType.MyCtoD(rowEq["totaldetention"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString())) * UType.MyCtoD(rowEq["exrate"].ToString()));
                        row["C34"] = UType.MyFormat(TotalFC.ToString());


                        row["C36"] = UType.MyFormat(TotalFCpkr.ToString());
                        row["C37"] = "";
                        row["C38"] = UType.MyFormat(TotalOtherCharges.ToString());
                        //row["C39"] = UType.MyFormat(TotalAmount.ToString());
                        row["C40"] = UType.MyFormat(TotalDiscount.ToString());
                        row["C51"] = UType.MyFormat(TotalNetAmount.ToString());
                        row["C53"] = "PKR " + UType.NumberToWords(UType.MyCtoD(TotalNetAmount.ToString())) + " Only";
                        //row["C4"] = UType.MyFormat(UType.MyCtoD(rowEq["WashingCharges"].ToString()) + UType.MyCtoD(rowEq["DocumentCharges"].ToString()) + UType.MyCtoD(rowEq["Demurrage"].ToString()));
                        //row["C4"] = rowEq["WashingCharges"].ToString();
                        if (UType.MyCtoD(rowEq["PrincipalCode"].ToString()) > 0)  // if (UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()) > 0)
                        {
                            row["C4"] = "Detention";
                            row["C5"] = rowEq["ContainerNo"].ToString();
                            row["C10"] = UType.GetDateTxt(rowEq["ContainerReturnDate"].ToString());  //UType.MyFormat(rowEq["PrincipalEquipInv"].ToString());
                            // if (UType.MyCtoD(rowEq["TotalDetention"].ToString()) > 0)
                            // {
                            //   row["CLogo"] = GetImage();
                            //   rptDataSet.Tables[0].Rows.Add(row);
                            //  row = rptDataSet.Tables[0].NewRow();
                            // row["C4"] = "Total Detention";
                            row["C5"] = rowEq["ContainerNo"].ToString();
                            row["C10"] = UType.GetDateTxt(rowEq["ContainerReturnDate"].ToString());  //UType.MyFormat(rowEq["TotalDetention"].ToString());
                            row["C6"] = rowEq["Detention"].ToString(); //"0";
                            row["C7"] = rowEq["logdays"].ToString(); //"0";
                            row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                            //row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                            // row["C31"] = "0";
                            row["C32"] = "0";
                            //row["C33"] = UType.MyFormat(rowEq["TotalDetention"].ToString());
                            // }
                            if (UType.MyCtoD(rowEq["Damagecharges"].ToString()) > 0) //
                            {
                                row["CLogo"] = GetImage();
                                row["C41"] = rowEq["PrincipalCode"].ToString();
                                row["C42"] = rowEq["ContainerArrvalDate"].ToString();
                                row["C43"] = rowEq["ContainerReturnDate"].ToString();
                                row["C44"] = Convert.ToString(UType.MyCtoD(rowEq["logDays"].ToString()) - UType.MyCtoD(rowEq["Detention"].ToString()));

                                rptDataSet.Tables[0].Rows.Add(row);
                                row = rptDataSet.Tables[0].NewRow();
                                row["C4"] = "Damage Charges";
                                row["C5"] = rowEq["ContainerNo"].ToString();
                                row["C33"] = UType.MyFormat(rowEq["Damagecharges"].ToString());
                                row["C6"] = rowEq["Detention"].ToString();  //rowEq["IsManual1"].ToString();
                                row["C7"] = rowEq["DetHdrFreeDays"].ToString(); //"0";;
                                row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                                row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                                // row["C31"] = "0";
                                row["C32"] = "0";
                                //row["C33"] = UType.MyFormat(rowEq["Demurrage"].ToString());
                            }
                            if (UType.MyCtoD(rowEq["DocumentCharges"].ToString()) > 0)
                            {
                                row["CLogo"] = GetImage();
                                row["C41"] = rowEq["PrincipalCode"].ToString();
                                row["C42"] = rowEq["ContainerArrvalDate"].ToString();
                                row["C43"] = rowEq["ContainerReturnDate"].ToString();
                                row["C44"] = Convert.ToString(UType.MyCtoD(rowEq["logDays"].ToString()) - UType.MyCtoD(rowEq["Detention"].ToString()));
                                rptDataSet.Tables[0].Rows.Add(row);
                                row = rptDataSet.Tables[0].NewRow();
                                row["C4"] = "DocumentCharges";
                                row["C5"] = rowEq["ContainerNo"].ToString();
                                row["C33"] = UType.MyFormat(rowEq["DocumentCharges"].ToString());
                                row["C6"] = rowEq["Detention"].ToString();  //rowEq["IsManual1"].ToString();
                                row["C7"] = "0";
                                row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                                row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                                row["C31"] = "0";
                                row["C32"] = "0";
                                //row["C33"] = UType.MyFormat(rowEq["DocumentCharges"].ToString());
                            }
                            if (UType.MyCtoD(rowEq["WashingCharges"].ToString()) > 0)
                            {
                                row["CLogo"] = GetImage();
                                row["C41"] = rowEq["PrincipalCode"].ToString();
                                row["C42"] = rowEq["ContainerArrvalDate"].ToString();
                                row["C43"] = rowEq["ContainerReturnDate"].ToString();
                                row["C44"] = Convert.ToString(UType.MyCtoD(rowEq["logDays"].ToString()) - UType.MyCtoD(rowEq["Detention"].ToString()));
                                rptDataSet.Tables[0].Rows.Add(row);
                                row = rptDataSet.Tables[0].NewRow();
                                row["C4"] = "WashingCharges";
                                row["C5"] = rowEq["ContainerNo"].ToString();
                                row["C33"] = UType.MyFormat(rowEq["WashingCharges"].ToString());
                                row["C6"] = rowEq["Detention"].ToString();  //rowEq["IsManual1"].ToString();
                                row["C7"] = "0";
                                row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                                row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                                row["C31"] = "0";
                                row["C32"] = "0";
                                //row["C33"] = UType.MyFormat(rowEq["DocumentCharges"].ToString());
                            }
                        }


                        row["CLogo"] = GetImage();
                        row["C41"] = rowEq["PrincipalCode"].ToString();
                        row["C42"] = rowEq["ContainerArrvalDate"].ToString();
                        row["C43"] = rowEq["ContainerReturnDate"].ToString();
                        row["C44"] = Convert.ToString(UType.MyCtoD(rowEq["logDays"].ToString()) - UType.MyCtoD(rowEq["Detention"].ToString()));
                    }
                    catch (Exception)
                    {

                        // throw;
                    }

                    rptDataSet.Tables[0].Rows.Add(row);





                }
            }
            catch (Exception ex)
            {

                //  throw;
            }
            //




            decimal DetentionDaysDisplay = 0; int SNo = 1; string sDate = ""; decimal DetentionDaysCalculation = 0;
            string RowNo = "0";

            try
            {
                foreach (DataRow rowD in InputDataSet.Tables["TblEq"].Rows)
                {
                    DetentionDaysDisplay = UType.MyCtoD(rowD["logDays"].ToString()) - UType.MyCtoD(rowD["Detention"].ToString());
                    //DetentionDaysDisplay = LessDetentionDays(rowD["logDays"].ToString(), rowD["Detention"].ToString());
                    //DetentionDaysDisplay = DetentionDaysDisplay + 1;
                    //DetentionDaysCalculation = UType.MyCtoD(rowD["logDays"].ToString()) - UType.MyCtoD(rowD["Detention"].ToString());
                    DetentionDaysCalculation = UType.MyCtoD(rowD["logDays"].ToString()) ;
                    DetentionDaysCalculation = DetentionDaysCalculation + 1;
                    //sDate = rowD["ContainerArrvalDate"].ToString();
                    //sDate = Convert.ToString(UType.MyCtoD(rowD["ContainerArrvalDate"].ToString()) + UType.MyCtoD(rowD["Detention"].ToString()));
                    sDate = AddDetentionDays(rowD["ContainerArrvalDate"].ToString(),rowD["Detention"].ToString());
                    foreach (DataRow rowR in rptDataSet.Tables[0].Rows)
                    {
                        if (rowR["C41"].ToString() == rowD["PrincipalCode"].ToString() && rowR["C4"].ToString() == "Detention")
                        {
                            if (rowD["ContainerNo"] == rowR["C5"].ToString())
                            {
                                //UType.MyCtoD(rowR["DetentionDays"].ToString()) < UType.MyCtoD(rowR["DetentionDays"].ToString())
                                if (DetentionDaysCalculation <= UType.MyCtoD(GetEndDayPlusOne(InputDataSet, RowNo, rowD["sizentype"].ToString())))  //UType.MyCtoD(rowD["EndDay"].ToString()))
                                {
                                    rowR["C3"] = SNo.ToString();
                                    rowR["C6"] = rowD["Detention"].ToString();
                                    rowR["C11"] = DetentionDaysDisplay.ToString();
                                    rowR["C12"] = GetRate(InputDataSet, RowNo, rowD["sizentype"].ToString()); ;// rowD["Rate"].ToString();
                                    rowR["C7"] = UType.GetDateTxt(sDate);
                                    //rowR["C10"] = UType.GetDateTxt(rowD["ContainerReturnDate"].ToString());
                                    rowR["C10"] = UType.GetDateTxt(rowD["ContainerReturnDate"].ToString());
                                    rowR["C33"] = Convert.ToString(DetentionDaysDisplay * UType.MyCtoD(rowR["C12"].ToString()));
                                    try
                                    {
                                        rowR["C76"] = Convert.ToString(UType.MyCtoD(rowD["exrate"].ToString()) * UType.MyCtoD(rowR["C33"].ToString()));
                                        rowR["C33"] = rowR["C76"];
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                                if (DetentionDaysCalculation > UType.MyCtoD(GetEndDayPlusOne(InputDataSet, RowNo, rowD["sizentype"].ToString())))
                                {
                                    rowR["C3"] = SNo.ToString();
                                    rowR["C6"] = rowD["Detention"].ToString();
                                    string GetEndDay = GetEndDayPlusOne(InputDataSet, RowNo, rowD["sizentype"].ToString());
                                    rowR["C11"] = GetEndDay; //GetEndDatePlusOne(InputDataSet, "0"); //rowD["EndDay"].ToString();
                                    rowR["C12"] = GetRate(InputDataSet, RowNo, rowD["sizentype"].ToString());   //rowD["Rate"].ToString();
                                                                                                                // rowR["C42"].ToString();

                                    rowR["C7"] = UType.GetDateTxt(sDate);
                                    string PrintEndDate = UType.GetEndDate1(sDate, GetEndDayNormal(InputDataSet, RowNo, rowD["sizentype"].ToString()));
                                    //rowR["C10"] = DetentionDaysCalculation.ToString() +" "+  UType.GetDateTxt(PrintEndDate) + " .PrintEndDate." + PrintEndDate +  ".. " + GetEndDayNormal(InputDataSet, RowNo) + ".sdate. " + sDate;
                                    rowR["C10"] = UType.GetDateTxt(PrintEndDate);

                                    DetentionDaysCalculation = DetentionDaysCalculation - UType.MyCtoD(GetEndDay);
                                    rowR["C33"] = Convert.ToString(UType.MyCtoD(GetEndDay) * UType.MyCtoD(rowR["C12"].ToString()));
                                    try
                                    {
                                        rowR["C76"] = Convert.ToString(UType.MyCtoD(rowD["exrate"].ToString()) * UType.MyCtoD(rowR["C33"].ToString()));
                                        //rowR["C33"] = rowR["C76"];
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                        }

                        SNo = SNo + 1;

                    }

                }
            }
            catch (Exception ex)
            {

                // throw;
            }

            decimal DetCtr = 0; decimal DetRemainingDays = 0; RowNo = "0";
            foreach (DataRow rowE in InputDataSet.Tables["TblEq"].Rows)
            {
                DetCtr = 0; DetRemainingDays = 0;  RowNo = "0";
                DetentionDaysDisplay = UType.MyCtoD(rowE["logDays"].ToString()) - UType.MyCtoD(rowE["Detention"].ToString());
                DetentionDaysDisplay = DetentionDaysDisplay - UType.MyCtoD(GetEndDayPlusOne(InputDataSet, RowNo, rowE["sizentype"].ToString()));
                DetRemainingDays = UType.MyCtoD(rowE["logDays"].ToString()) - UType.MyCtoD(rowE["Detention"].ToString());
                DetRemainingDays = DetRemainingDays - UType.MyCtoD(GetEndDayPlusOne(InputDataSet, RowNo, rowE["sizentype"].ToString()));
                //sDate =  Convert.ToString(UType.MyCtoD(rowE["ContainerArrvalDate"].ToString()) + UType.MyCtoD(rowE["Detention"].ToString()));
                sDate = AddDetentionDays(rowE["ContainerArrvalDate"].ToString(), rowE["Detention"].ToString());
                try
                
                {
                    sDate = UType.GetEndDate1(sDate, GetEndDayPlusOne(InputDataSet, RowNo, rowE["sizentype"].ToString()));
                }
                catch (Exception ex)
                { }
                while (DetRemainingDays > 0)
                {
                    DataRow row = rptDataSet.Tables[0].NewRow();
                    row["C3"] = SNo.ToString();
                    row["C4"] = "Detention";
                    row["C5"] = rowE["ContainerNo"].ToString();
                    row["C6"] = rowE["Detention"].ToString();
                    row["C7"] = UType.GetDateTxt(sDate);
                    try
                    {
                        //sDate = UType.GetEndDate1(sDate, GetEndDayPlusOne(InputDataSet, RowNo));
                        RowNo = Convert.ToString(UType.MyCtoD(RowNo) + 1);
                        DetentionDaysDisplay = UType.MyCtoD(GetEndDayPlusOne(InputDataSet, RowNo, rowE["sizentype"].ToString()));
                        //DetRemainingDays = UType.MyCtoD(GetEndDayNormal(InputDataSet, RowNo));
                        //DetRemainingDays = UType.MyCtoD(GetEndDayNormal(InputDataSet, RowNo));
                        if (DetRemainingDays < DetentionDaysDisplay)
                        {
                            DetentionDaysDisplay = DetRemainingDays + 1;
                        }
                        //DetentionDaysCalculation = DetentionDaysDisplay - UType.MyCtoD(GetEndDayPlusOne(InputDataSet, RowNo));
                    }
                    catch (Exception ex)
                    {
                    }
                    string PrintEndDate = "";
                    DetentionDaysCalculation = UType.MyCtoD(GetEndDayNormal(InputDataSet, RowNo, rowE["sizentype"].ToString()));
                    if (DetRemainingDays < DetentionDaysCalculation)
                    {
                        //string EndDatePlusFree = Convert.ToString(DetRemainingDays + UType.MyCtoD(rowE["Detention"].ToString()));
                        string EndDatePlusFree = Convert.ToString(DetRemainingDays);

                        EndDatePlusFree = Convert.ToString(UType.MyCtoD(EndDatePlusFree)-1);
                        //PrintEndDate = UType.GetEndDate1(sDate, Convert.ToString(DetRemainingDays - 1));
                        PrintEndDate = UType.GetEndDate1(sDate, EndDatePlusFree);
                    }
                    else
                    {
                        string GetEndDay = GetEndDayNormal(InputDataSet, RowNo, rowE["sizentype"].ToString());
                        //GetEndDay = Convert.ToString(UType.MyCtoD(GetEndDay) - 1);
                        //UType.MyCtoD(GetEndDay
                        //PrintEndDate = UType.GetEndDate1(sDate, GetEndDayNormal(InputDataSet, RowNo));

                        PrintEndDate = UType.GetEndDate1(sDate, GetEndDay);
                    }

                    sDate = PrintEndDate;
                    sDate = UType.GetEndDate1(PrintEndDate, "1");
                    row["C10"] = UType.GetDateTxt(PrintEndDate);
                    //row["C10"] = DetentionDaysCalculation.ToString() + " " + UType.GetDateTxt(PrintEndDate) + " .." + PrintEndDate + ".. " + GetEndDayNormal(InputDataSet, RowNo);
                    if (DetRemainingDays < DetentionDaysCalculation)
                    {
                        row["C11"] = DetRemainingDays.ToString();
                    }
                    else
                    { row["C11"] = DetentionDaysDisplay.ToString(); }
                    row["C12"] = GetRate(InputDataSet, RowNo.ToString(), rowE["sizentype"].ToString());
                    row["CLogo"] = GetImage();
                    row["C33"] = Convert.ToString(UType.MyCtoD(row["C11"].ToString()) * UType.MyCtoD(row["C12"].ToString()));
                    try
                    {
                        row["C76"] = Convert.ToString(UType.MyCtoD(rowE["exrate"].ToString()) * UType.MyCtoD(row["C33"].ToString()));
                        //row["C33"] = row["C76"];
                    }
                    catch (Exception ex)
                    {

                    }

                    rptDataSet.Tables[0].Rows.Add(row);
                    //RowNo = RowNo + 1;
                    DetRemainingDays = DetRemainingDays - UType.MyCtoD(GetEndDayPlusOne(InputDataSet, RowNo, rowE["sizentype"].ToString()));
                    if (DetentionDaysDisplay < DetRemainingDays)
                    {
                        //sDate = UType.GetEndDate1(sDate, DetentionDaysDisplay.ToString());
                    }
                    else
                    {
                        //sDate = UType.GetEndDate1(sDate, DetRemainingDays.ToString());
                    }


                    //sDate = UType.GetEndDate1(rowE["ContainerArrvalDate"].ToString(), GetEndDate(InputDataSet, DetCtr.ToString()));

                    //SNo = SNo + 1; DetCtr = DetCtr + 1;
                    //RowNo = Convert.ToString(UType.MyCtoD(RowNo) + 1);
                }

            }
        }
        catch (Exception ex)
        {
            // throw (ex);
        }

        try
        {
            decimal Total33 = 0; decimal DetentionFC = 0; decimal DetentionPKR = 0; decimal TotalOther = 0;
            foreach (DataRow row123 in rptDataSet.Tables[0].Rows)
            {
                if (row123["C4"].ToString() != "Detention")
                {
                    row123["C7"] = "";
                    //row123["C10"] = "";
                    //row123["C33"] = row123["C10"];
                    Total33 = Total33 + UType.MyCtoD(row123["C33"].ToString());
                    TotalOther = TotalOther + UType.MyCtoD(row123["C33"].ToString());
                    row123["C79"] = "Rs.";
                }
                if (row123["C4"].ToString() == "Detention")
                {
                    DetentionFC = DetentionFC + UType.MyCtoD(row123["C33"].ToString());
                    DetentionPKR = DetentionPKR + UType.MyCtoD(row123["C76"].ToString());
                    Total33 = Total33 + UType.MyCtoD(row123["C76"].ToString());
                    row123["C79"] = "$";
                }

            }

            foreach (DataRow row in rptDataSet.Tables[0].Rows)
            {
                row["C34"] = UType.MyFormat(DetentionFC.ToString());
                row["C38"] = UType.MyFormat(TotalOther.ToString());

                row["C33"] = UType.MyFormat(row["C33"].ToString());

                row["C39"] = UType.MyFormat(Total33.ToString());
                row["C51"] = UType.MyFormat(Total33.ToString());
                row["C53"] = "PKR " + UType.NumberToWords(UType.MyCtoD(Total33.ToString())) + " Only";
                row["C34"] = UType.MyFormat(DetentionFC.ToString());
                row["C36"] = UType.MyFormat(DetentionPKR.ToString());
            }

        }
        catch (Exception ex)
        {


        }
        return rptDataSet;

    }

    public DataSet MoveInRptDsDetInvoiceDet(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row1 = InputDataSet.Tables[0].Rows[0];
            DataRow rowCh = InputDataSet.Tables["TblCh"].Rows[0];

            string ArrivalDate = UType.GetDateTxt(row1["ArrivalDate"].ToString());  //GetActCity1(row1["PortLoading"].ToString());
            string Portofloading = GetActCity1(row1["Portloading"].ToString());
            string PortDischarge = GetActCity1(row1["PortDischarge"].ToString());
            string FinalDestination = GetActCity2(row1["FinalDestination"].ToString());
            string ShippingCompanyID = GetDescriptionDDL(row1["ShippingCompanyID"].ToString());

            string ShipperName = "";
            string ConsigneeName = ""; string Consigne = "";
            decimal LocalAmount = 0;
            #region ConsigneeName
            MyMain oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["Consignee"].ToString();
                Consigne = row1["Consignee"].ToString();
                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ConsigneeName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion

            #region ShipperName
            oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["Shipper"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ShipperName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {
                //throw;

            }
            #endregion
            //
            decimal TotalOtherCharges = 0; decimal TotalDet = 0; decimal TotalAmount = 0; decimal TotalFC = 0; decimal TotalFCpkr = 0; decimal TotalDiscount = 0;
            decimal TotalNetAmount = 0; decimal DetDays = 0; decimal TotalPlugin = 0;

            try
            {
                foreach (DataRow rowEq in InputDataSet.Tables["TblEq"].Rows)
                {
                    TotalDet = TotalDet + UType.MyCtoD(rowEq["totaldetention"].ToString());
                    TotalFCpkr = TotalFCpkr + (UType.MyCtoD(rowEq["totaldetention"].ToString()) * UType.MyCtoD(rowEq["exrate"].ToString()));
                    TotalPlugin = TotalPlugin + UType.MyCtoD(rowEq["plugin"].ToString());

                    TotalOtherCharges = TotalOtherCharges + UType.MyCtoD(rowEq["WashingCharges"].ToString()) + UType.MyCtoD(rowEq["DocumentCharges"].ToString()) + UType.MyCtoD(rowEq["Demurrage"].ToString()) + UType.MyCtoD(rowEq["Damagecharges"].ToString());

                    TotalAmount = TotalAmount + (UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString())) * UType.MyCtoD(rowEq["exrate"].ToString());
                    DetDays = UType.MyCtoD(rowEq["logdays"].ToString()) - UType.MyCtoD(rowEq["logdays"].ToString());
                    //TotalFC = TotalFC + (UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()));

                    TotalDiscount = TotalDiscount + (UType.MyCtoD(rowEq["Discount"].ToString()) * UType.MyCtoD(rowEq["exrate"].ToString()));
                    TotalNetAmount = TotalNetAmount + (UType.MyCtoD(rowEq["totaldetention"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString()) * UType.MyCtoD(rowEq["exrate"].ToString()));
                }
            }
            catch (Exception ex)
            {

                //throw;
            }

            try
            {
                foreach (DataRow rowEq in InputDataSet.Tables["TblEq"].Rows)
                {
                    DataRow row = rptDataSet.Tables[0].NewRow();

                    try
                    {
                        oMy = new MyMain();

                        row["C1"] = "DET " + "-" + rowEq["jobno"].ToString() + "/" + rowEq["jobcy"].ToString();
                        row["C2"] = UType.GetDateTxt(rowEq["InvoiceDate"].ToString());
                        row["C3"] = row1["IGMNo"].ToString();

                        row["C13"] = ConsigneeName;
                        row["C14"] = UType.GetDateTxt(rowEq["ContainerReturnDate"].ToString());
                        row["C15"] = ShipperName;
                        row["C16"] = UType.GetDateTxt(row1["IGMDate"].ToString());
                        row["C17"] = Portofloading;
                        row["C18"] = ConsigneeName;
                        row["C19"] = PortDischarge;
                        row["C20"] = FinalDestination;
                        row["C21"] = rowEq["ExRate"].ToString();
                        row["C22"] = row1["HBLNo"].ToString();
                        row["C23"] = row1["MBLNo"].ToString();
                        row["C24"] = row1["Vessel"].ToString() + " / " + row1["Voyage"].ToString() + " / " + PortDischarge;   // row["C23"].ToString() + " / " + row1["Voyage"].ToString();

                        row["C25"] = row1["IndexNo"].ToString();
                        row["C26"] = "PKR";//GetCurrencyString(rowCh["currency"].ToString());

                        row["C27"] = UType.GetDateTxt(row1["ArrivalDate"].ToString());
                        row["C28"] = UType.GetDateTxt(rowEq["ContainerArrvalDate"].ToString());
                        row["C29"] = UType.GetDateTxt(rowEq["ContainerReturnDate"].ToString()); ;
                        row["C30"] = "We enclose an Invoice for Container detention charges under mentioned:";
                        row["C4"] = "Detention";
                        row["C5"] = rowEq["ContainerNo"].ToString();
                        row["C6"] = rowEq["Detention"].ToString();  //rowEq["IsManual1"].ToString();
                        row["C7"] = rowEq["DetHdrFreeDays"].ToString(); //LogDays(rowEq["ContainerEntryDate"].ToString(), rowEq["ContainerReturnDate"].ToString());
                        row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                        row["C9"] = GetCurrencyString(rowEq["curren"].ToString());
                        row["C10"] = UType.MyFormat(rowEq["totaldetention"].ToString());  //rowEq["SecurityAmountReceive"].ToString();
                        row["C31"] = rowEq["Discount"].ToString();  // rowEq["SecurityAmountPaid"].ToString();
                                                                    //row["C33"] = UType.MyFormat((UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString())) * UType.MyCtoD(rowEq["exrate"].ToString()));
                        row["C33"] = UType.MyFormat((UType.MyCtoD(rowEq["totaldetention"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString())) * UType.MyCtoD(rowEq["exrate"].ToString()));
                        row["C34"] = UType.MyFormat(TotalFC.ToString());


                        row["C36"] = UType.MyFormat(TotalFCpkr.ToString());
                        row["C37"] = "";
                        row["C38"] = UType.MyFormat(TotalOtherCharges.ToString());
                        //row["C39"] = UType.MyFormat(TotalAmount.ToString());
                        row["C40"] = UType.MyFormat(TotalDiscount.ToString());
                        row["C51"] = UType.MyFormat(TotalNetAmount.ToString());
                        row["C53"] = "PKR " + UType.NumberToWords(UType.MyCtoD(TotalNetAmount.ToString())) + " Only";
                        //row["C4"] = UType.MyFormat(UType.MyCtoD(rowEq["WashingCharges"].ToString()) + UType.MyCtoD(rowEq["DocumentCharges"].ToString()) + UType.MyCtoD(rowEq["Demurrage"].ToString()));
                        //row["C4"] = rowEq["WashingCharges"].ToString();
                        if (UType.MyCtoD(rowEq["PrincipalCode"].ToString()) > 0)  // if (UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()) > 0)
                        {
                            row["C4"] = "Detention";
                            row["C5"] = rowEq["ContainerNo"].ToString();
                            row["C10"] = UType.MyFormat(rowEq["PrincipalEquipInv"].ToString());
                            // if (UType.MyCtoD(rowEq["TotalDetention"].ToString()) > 0)
                            // {
                            //   row["CLogo"] = GetImage();
                            //   rptDataSet.Tables[0].Rows.Add(row);
                            //  row = rptDataSet.Tables[0].NewRow();
                            // row["C4"] = "Total Detention";
                            row["C5"] = rowEq["ContainerNo"].ToString();
                            row["C10"] = UType.MyFormat(rowEq["TotalDetention"].ToString());
                            row["C6"] = rowEq["Detention"].ToString(); //"0";
                            row["C7"] = rowEq["logdays"].ToString(); //"0";
                            row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                            //row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                            // row["C31"] = "0";
                            row["C32"] = "0";
                            //row["C33"] = UType.MyFormat(rowEq["TotalDetention"].ToString());
                            // }

                        }


                        row["CLogo"] = GetImage();
                        row["C41"] = rowEq["PrincipalCode"].ToString();
                        row["C42"] = rowEq["ContainerArrvalDate"].ToString();
                        row["C43"] = rowEq["ContainerReturnDate"].ToString();
                        row["C44"] = Convert.ToString(UType.MyCtoD(rowEq["logDays"].ToString()) - UType.MyCtoD(rowEq["Detention"].ToString()));
                    }
                    catch (Exception)
                    {

                        // throw;
                    }

                    rptDataSet.Tables[0].Rows.Add(row);





                }
            }
            catch (Exception ex)
            {

                //  throw;
            }
            //




            decimal DetentionDays = 0; int SNo = 1; string sDate = "";

            try
            {
                foreach (DataRow rowD in InputDataSet.Tables["TblEq"].Rows)
                {
                    DetentionDays = UType.MyCtoD(rowD["logDays"].ToString()) - UType.MyCtoD(rowD["Detention"].ToString());
                    foreach (DataRow rowR in rptDataSet.Tables[0].Rows)
                    {
                        if (rowR["C41"].ToString() == rowD["PrincipalCode"].ToString() && rowR["C4"].ToString() == "Detention")
                        {
                            if (rowD["ContainerNo"] == rowR["C5"].ToString())
                            {
                                //UType.MyCtoD(rowR["DetentionDays"].ToString()) < UType.MyCtoD(rowR["DetentionDays"].ToString())
                                if (DetentionDays <= UType.MyCtoD(GetEndDayPlusOne(InputDataSet, "0", rowD["sizentype"].ToString())))  //UType.MyCtoD(rowD["EndDay"].ToString()))
                                {
                                    rowR["C3"] = SNo.ToString();
                                    rowR["C6"] = rowD["Detention"].ToString();
                                    rowR["C11"] = DetentionDays.ToString();
                                    rowR["C12"] = GetRate(InputDataSet, "0", rowD["sizentype"].ToString()); ;// rowD["Rate"].ToString();
                                    rowR["C7"] = UType.GetDateTxt(rowD["ContainerArrvalDate"].ToString());
                                    //rowR["C10"] = UType.GetDateTxt(rowD["ContainerReturnDate"].ToString());
                                    rowR["C10"] = UType.GetDateTxt(Convert.ToString(UType.MyCtoD(rowD["ContainerReturnDate"].ToString()) - 1));
                                    rowR["C33"] = Convert.ToString(UType.MyCtoD(rowR["C11"].ToString()) * UType.MyCtoD(rowR["C12"].ToString()));
                                    try
                                    {
                                        rowR["C76"] = Convert.ToString(UType.MyCtoD(rowD["exrate"].ToString()) * UType.MyCtoD(rowR["C33"].ToString()));
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                                if (DetentionDays > UType.MyCtoD(GetEndDayPlusOne(InputDataSet, "0", rowD["sizentype"].ToString())))
                                {
                                    rowR["C3"] = SNo.ToString();
                                    rowR["C6"] = rowD["Detention"].ToString();
                                    rowR["C11"] = GetEndDayPlusOne(InputDataSet, "0", rowD["sizentype"].ToString()); //rowD["EndDay"].ToString();
                                    rowR["C12"] = GetRate(InputDataSet, "0", rowD["sizentype"].ToString());   //rowD["Rate"].ToString();
                                    sDate = rowD["ContainerArrvalDate"].ToString(); // rowR["C42"].ToString();
                                    rowR["C7"] = UType.GetDateTxt(rowD["ContainerArrvalDate"].ToString());
                                    //rowR["C10"] = UType.GetDateTxt(rowD["ContainerReturnDate"].ToString());
                                    rowR["C10"] = UType.GetEndDate(sDate, Convert.ToString(UType.MyCtoD(GetEndDayPlusOne(InputDataSet, "0", rowD["sizentype"].ToString())) - 1));
                                    DetentionDays = DetentionDays - UType.MyCtoD(GetEndDayPlusOne(InputDataSet, "0", rowD["sizentype"].ToString()));
                                    rowR["C33"] = Convert.ToString(UType.MyCtoD(rowR["C11"].ToString()) * UType.MyCtoD(rowR["C12"].ToString()));
                                    try
                                    {
                                        rowR["C76"] = Convert.ToString(UType.MyCtoD(rowD["exrate"].ToString()) * UType.MyCtoD(rowR["C33"].ToString()));
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                        }

                        SNo = SNo + 1;

                    }

                }
            }
            catch (Exception ex)
            {

                // throw;
            }

            decimal DetCtr = 0; decimal DetRemainingDays = 0;
            foreach (DataRow rowE in InputDataSet.Tables["TblEq"].Rows)
            {
                DetCtr = 0; DetRemainingDays = 0;
                DetentionDays = UType.MyCtoD(rowE["logDays"].ToString()) - UType.MyCtoD(rowE["Detention"].ToString());
                DetentionDays = DetentionDays - UType.MyCtoD(GetEndDayPlusOne(InputDataSet, "0", rowE["sizentype"].ToString()));
                sDate = rowE["ContainerArrvalDate"].ToString();
                try
                {
                    sDate = UType.GetEndDate1(rowE["ContainerArrvalDate"].ToString(), GetEndDayPlusOne(InputDataSet, DetCtr.ToString(), rowE["sizentype"].ToString()));
                }
                catch (Exception ex)
                {


                }

                DetCtr = 1;
                while (DetentionDays > 0)
                {
                    DataRow row = rptDataSet.Tables[0].NewRow();
                    row["C3"] = SNo.ToString();
                    row["C4"] = "Detention";
                    row["C7"] = UType.GetDateTxt(sDate);
                    row["C5"] = rowE["ContainerNo"].ToString();
                    row["C6"] = rowE["Detention"].ToString();
                    DetRemainingDays = UType.MyCtoD(GetEndDayPlusOne(InputDataSet, DetCtr.ToString(), rowE["sizentype"].ToString()));
                    if (DetentionDays < DetRemainingDays)
                    {
                        DetRemainingDays = DetentionDays;
                    }
                    try
                    {
                        row["C10"] = UType.GetEndDate(sDate, Convert.ToString(UType.MyCtoD(DetRemainingDays.ToString()) - 1));
                    }
                    catch (Exception ex)
                    {

                    }

                    row["C11"] = DetRemainingDays.ToString();
                    row["C12"] = GetRate(InputDataSet, DetCtr.ToString(), rowE["sizentype"].ToString());
                    row["CLogo"] = GetImage();
                    row["C33"] = Convert.ToString(UType.MyCtoD(row["C11"].ToString()) * UType.MyCtoD(row["C12"].ToString()));
                    try
                    {
                        row["C76"] = Convert.ToString(UType.MyCtoD(rowE["exrate"].ToString()) * UType.MyCtoD(row["C33"].ToString()));
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        DetentionDays = DetentionDays - UType.MyCtoD(GetEndDayPlusOne(InputDataSet, DetCtr.ToString(), rowE["sizentype"].ToString()));
                    }
                    catch (Exception ex)
                    {


                    }
                    rptDataSet.Tables[0].Rows.Add(row);
                    //sDate = UType.GetEndDate1(rowE["ContainerArrvalDate"].ToString(), GetEndDate(InputDataSet, DetCtr.ToString()));
                    sDate = UType.GetEndDate1(sDate, GetEndDayPlusOne(InputDataSet, DetCtr.ToString(), rowE["sizentype"].ToString()));
                    SNo = SNo + 1; DetCtr = DetCtr + 1;

                }
            }
        }
        catch (Exception ex)
        {
            // throw (ex);
        }

        try   //last
        {
            decimal Total33 = 0; decimal DetentionFC = 0; decimal DetentionPKR = 0; decimal TotalOther = 0;
            foreach (DataRow row123 in rptDataSet.Tables[0].Rows)
            {
                if (row123["C4"].ToString() != "Detention")
                {
                    row123["C7"] = "";
                    //row123["C10"] = "";
                    //row123["C33"] = row123["C10"];
                    Total33 = Total33 + UType.MyCtoD(row123["C33"].ToString());
                    TotalOther = TotalOther + UType.MyCtoD(row123["C33"].ToString());
                }
                if (row123["C4"].ToString() == "Detention")
                {
                    DetentionFC = DetentionFC + UType.MyCtoD(row123["C33"].ToString());
                    DetentionPKR = DetentionPKR + UType.MyCtoD(row123["C76"].ToString());
                    Total33 = Total33 + UType.MyCtoD(row123["C76"].ToString());
                }

            }

            foreach (DataRow row in rptDataSet.Tables[0].Rows)
            {
                row["C34"] = UType.MyFormat(DetentionFC.ToString());
                row["C38"] = UType.MyFormat(TotalOther.ToString());

                row["C33"] = UType.MyFormat(row["C33"].ToString());

                row["C39"] = UType.MyFormat(Total33.ToString());
                row["C51"] = UType.MyFormat(Total33.ToString());
                row["C53"] = "PKR " + UType.NumberToWords(UType.MyCtoD(Total33.ToString())) + " Only";
                row["C34"] = UType.MyFormat(DetentionFC.ToString());
                row["C36"] = UType.MyFormat(DetentionPKR.ToString());

                row["C10"] = UType.GetEndDateNew(row["C7"].ToString(), row["C11"].ToString());

            }

        }
        catch (Exception ex)
        {


        }
        return rptDataSet;

    }
   
    public DataSet MoveInRptDsCostingDtl(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row1 = InputDataSet.Tables[0].Rows[0];
            DataRow rowCh = InputDataSet.Tables["TblCh"].Rows[0];
            DataRow rowD = InputDataSet.Tables["TblEq"].Rows[0];

            string ArrivalDate = UType.GetDateTxt(row1["ArrivalDate"].ToString());  //GetActCity1(row1["PortLoading"].ToString());
            string Portofloading = GetActCity1(row1["Portloading"].ToString());
            string PortDischarge = GetActCity1(row1["PortDischarge"].ToString());
            string FinalDestination = GetActCity2(row1["FinalDestination"].ToString());
            string ShippingCompanyID = GetDescriptionDDL(row1["ShippingCompanyID"].ToString());

            string ShipperName = "";
            string ConsigneeName = ""; string ClientName = "";
            decimal LocalAmount = 0; DataSet dsTem = null;



            #region ConsigneeName
            MyMain oMy = new MyMain();
            oMy = new MyMain();
            try
            {

                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["Consignee"].ToString();

                dsTem = oMyConsignee.GetAccountOneConsignee();
                if (dsTem != null)
                {
                    ConsigneeName = dsTem.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }
                dsTem = null;
            }
            catch (Exception ex)
            {


            }
            #endregion

            #region ShipperName
            oMy = new MyMain();
            oMy = new MyMain();
            try
            {

                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["Shipper"].ToString();

                dsTem = oMyConsignee.GetAccountOneConsignee();
                if (dsTem != null)
                {
                    ShipperName = dsTem.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion

            #region Client
            oMy = new MyMain();
            oMy = new MyMain();
            try
            {

                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["client"].ToString();

                dsTem = oMyConsignee.GetAccountOneConsignee();
                if (dsTem != null)
                {
                    ClientName = dsTem.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion
            //

            int Ctr = 1;
            foreach (DataRow rowC in InputDataSet.Tables["TblCh"].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                try
                {

                    row["C1"] = "DET " + "-" + rowC["jobno"].ToString() + "/" + rowC["jobcy"].ToString();
                    //row["C2"] = UType.GetDateTxt(rowC["InvoiceDate"].ToString());
                    if (rowC["ChargesType"].ToString() == "1")
                    {
                        row["C5"] = "Receivable";
                        row["C6"] = "1";
                    }
                    if (rowC["ChargesType"].ToString() == "2")
                    {
                        row["C5"] = "Payable";
                        row["C6"] = "2";
                    }
                    row["C13"] = ClientName;
                    row["C14"] = UType.GetDateTxt(rowD["ContainerReturnDate"].ToString());
                    row["C15"] = ShipperName;
                    row["C16"] = UType.GetDateTxt(row1["IGMDate"].ToString());

                    row["C17"] = Portofloading;
                    row["C18"] = ConsigneeName;
                    row["C19"] = PortDischarge;
                    row["C20"] = FinalDestination;
                    row["C21"] = rowD["exrate"].ToString();
                    row["C22"] = row1["HBLNo"].ToString();
                    row["C23"] = row1["MBLNo"].ToString();


                    #region Charges

                    MyMain oMyAccountexpense = new MyMain();
                    oMyAccountexpense.Fld1 = rowC["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                    oMyAccountexpense.Fld2 = rowC["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                    oMyAccountexpense.Fld3 = rowC["particular"].ToString();

                    dsTem = oMyAccountexpense.GetAccountExpense1();
                    if (dsTem != null)
                    {
                        row["C4"] = dsTem.Tables[0].Rows[0]["ActDesc"].ToString();
                        //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                        //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                        // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    }
                    #endregion
                    row["C3"] = Ctr;
                    row["C33"] = rowC["localamount"].ToString();

                }
                catch (Exception ex)
                {


                }
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
                Ctr = Ctr + 1;
            }

            foreach (DataRow rowEq in InputDataSet.Tables["TblEq"].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();

                oMy = new MyMain();
                try
                {
                    row["C3"] = Ctr;
                    row["C5"] = "Detention";
                    row["C6"] = "3";
                    row["C4"] = "Detention Charges";
                    row["C33"] = rowEq["TotalDetention"];
                    row["C12"] = rowEq["ContainerNo"];
                    row["CLogo"] = GetImage();
                    rptDataSet.Tables[0].Rows.Add(row);
                    Ctr = Ctr + 1;
                    if (UType.MyCtoD(rowEq["WashingCharges"].ToString()) > 0)
                    {

                        row = rptDataSet.Tables[0].NewRow();
                        row["C3"] = Ctr;
                        row["C5"] = "Detention";
                        row["C6"] = "3";
                        row["C4"] = "Washing Charges";
                        row["C33"] = rowEq["WashingCharges"];
                        row["C12"] = rowEq["ContainerNo"];
                        row["CLogo"] = GetImage();
                        rptDataSet.Tables[0].Rows.Add(row);
                        Ctr = Ctr + 1;
                    }
                    if (UType.MyCtoD(rowEq["Damagecharges"].ToString()) > 0)
                    {
                        row = rptDataSet.Tables[0].NewRow();
                        row["C3"] = Ctr;
                        row["C5"] = "Detention";
                        row["C6"] = "3";
                        row["C4"] = "Damage Charges";
                        row["C33"] = rowEq["Damagecharges"];
                        row["C12"] = rowEq["ContainerNo"];
                        row["CLogo"] = GetImage();
                        rptDataSet.Tables[0].Rows.Add(row);
                        Ctr = Ctr + 1;
                    }
                    if (UType.MyCtoD(rowEq["documentcharges"].ToString()) > 0)
                    {
                        row = rptDataSet.Tables[0].NewRow();
                        row["C3"] = Ctr;
                        row["C5"] = "Detention";
                        row["C6"] = "3";
                        row["C4"] = "Document Charges";
                        row["C33"] = rowEq["documentcharges"];
                        row["C12"] = rowEq["ContainerNo"];
                        row["CLogo"] = GetImage();
                        rptDataSet.Tables[0].Rows.Add(row);
                        Ctr = Ctr + 1;
                    }
                    if (UType.MyCtoD(rowEq["Demurrage"].ToString()) > 0)
                    {
                        row = rptDataSet.Tables[0].NewRow();
                        row["C3"] = Ctr;
                        row["C5"] = "Detention";
                        row["C6"] = "3";
                        row["C4"] = "Demurrage Charges";
                        row["C33"] = rowEq["Demurrage"];
                        row["C12"] = rowEq["ContainerNo"];
                        row["CLogo"] = GetImage();
                        rptDataSet.Tables[0].Rows.Add(row);
                        Ctr = Ctr + 1;
                    }
                    if (UType.MyCtoD(rowEq["SecurityAmountReceive"].ToString()) > 0)
                    {
                        //row = rptDataSet.Tables[0].NewRow();
                        //row["C3"] = Ctr;
                        //row["C5"] = "Security";
                        //row["C6"] = "4";
                        //row["C4"] = "Security Amount Receive";
                        //row["C33"] = rowEq["SecurityAmountReceive"];
                        //row["C12"] = rowEq["ContainerNo"];
                        //row["CLogo"] = GetImage();
                        //rptDataSet.Tables[0].Rows.Add(row);
                        //Ctr = Ctr + 1;
                    }
                    if (UType.MyCtoD(rowEq["SecurityAmountPaid"].ToString()) > 0)
                    {
                        row = rptDataSet.Tables[0].NewRow();
                        row["C3"] = Ctr;
                        row["C5"] = "Security Payable";
                        row["C6"] = "4";
                        row["C4"] = "Security Amount Paid";
                        row["C33"] = rowEq["SecurityAmountPaid"];
                        row["C12"] = rowEq["ContainerNo"];
                        row["CLogo"] = GetImage();
                        rptDataSet.Tables[0].Rows.Add(row);
                    }


                    Ctr = Ctr + 1;
                }

                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            //

            foreach (DataRow rowAct in InputDataSet.Tables["TblAccount"].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();

                oMy = new MyMain();
                try
                {
                    row["C3"] = Ctr;
                    row["C5"] = "Paid";
                    row["C6"] = "5";
                    row["C4"] = rowAct["Actdesc"];
                    row["C33"] = rowAct["Amount"];
                    //row["C12"] = rowAct["ContainerNo"];
                    row["CLogo"] = GetImage();
                    rptDataSet.Tables[0].Rows.Add(row);

                    Ctr = Ctr + 1;
                }

                catch (Exception ex)
                {
                    throw (ex);
                }

            }



        }
        catch (Exception ex)
        {
            throw (ex);
        }
        decimal TotalReceivable = 0; decimal TotalPayable = 0; decimal TotalDetention = 0; decimal TotalOther = 0;
        foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
        {
            if (row1["C6"].ToString() == "1")
            {
                TotalReceivable = TotalReceivable + UType.MyCtoD(row1["C33"].ToString());
            }
            if (row1["C6"].ToString() == "2")
            {
                TotalPayable = TotalPayable + UType.MyCtoD(row1["C33"].ToString());
            }
            if (row1["C6"].ToString() == "3")
            {
                TotalDetention = TotalDetention + UType.MyCtoD(row1["C33"].ToString());
            }
            if (row1["C6"].ToString() == "4")
            {
                TotalOther = TotalOther + UType.MyCtoD(row1["C33"].ToString());
            }

        }
        foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
        {
            row1["C33"] = UType.MyFormat(row1["C33"].ToString());
            row1["C51"] = UType.MyFormat(TotalReceivable);
            row1["C52"] = UType.MyFormat(TotalPayable);
            row1["C53"] = UType.MyFormat(TotalDetention);
            row1["C54"] = UType.MyFormat(TotalOther);
            if (row1["C6"].ToString() == "1")
            {
                row1["C34"] = UType.MyFormat(TotalReceivable);
            }
            if (row1["C6"].ToString() == "2")
            {
                row1["C34"] = UType.MyFormat(TotalPayable);
            }
            if (row1["C6"].ToString() == "3")
            {
                row1["C34"] = UType.MyFormat(TotalDetention);
            }
            if (row1["C6"].ToString() == "4")
            {
                row1["C34"] = UType.MyFormat(TotalOther);
            }
        }
        return rptDataSet;

    }

    public DataSet MoveInRptDsCostingGrid(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            foreach (DataRow rowC in InputDataSet.Tables[0].Rows)
            {
                if (rowC["C6"].ToString() == "1")
                {
                    DataRow row = rptDataSet.Tables[0].NewRow();
                    row["C4"] = row["C4"];
                    row["C5"] = row["C5"];
                    row["C6"] = row["C6"];
                    row["C12"] = row["C12"];
                    row["C33"] = row["C33"];
                    row["CLogo"] = GetImage();
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


    private string GetEndDayPlusOne(DataSet Ds1, string RowID, string SizenType)
    {
        string retVal = ""; int IntRowID = Convert.ToInt32(RowID); int RowCtr = 0;
        try
        {
            //retVal = Convert.ToString(UType.MyCtoD(Ds1.Tables["TblEqDet"].Rows[IntRowID]["EndDay"].ToString()) - UType.MyCtoD(Ds1.Tables["TblEqDet"].Rows[IntRowID]["StartDay"].ToString()));
            //retVal = Convert.ToString(UType.MyCtoD(retVal) + 1);
            foreach (DataRow row in Ds1.Tables["TblEqDet"].Rows)
            {
                if (row["sizentype"].ToString() == SizenType)
                {
                    if (UType.MyCtoD(RowID) == RowCtr)
                    {

                        //retVal = row["Rate"].ToString();
                        retVal = Convert.ToString(UType.MyCtoD(row["EndDay"].ToString()) - UType.MyCtoD(row["StartDay"].ToString()));
                        retVal = Convert.ToString(UType.MyCtoD(retVal) + 1);
                    }
                    RowCtr++;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return retVal;

      
    }

    private string GetEndDayNormal(DataSet Ds1, string RowID, string SizenType)
    {
        string retVal = ""; int IntRowID = Convert.ToInt32(RowID); int RowCtr = 0;
        try
        {
            //retVal = Convert.ToString(UType.MyCtoD(Ds1.Tables["TblEqDet"].Rows[IntRowID]["EndDay"].ToString()) - UType.MyCtoD(Ds1.Tables["TblEqDet"].Rows[IntRowID]["StartDay"].ToString()));
            //retVal = Convert.ToString(UType.MyCtoD(retVal) + 1);
            foreach (DataRow row in Ds1.Tables["TblEqDet"].Rows)
            {
                if (row["sizentype"].ToString() == SizenType)
                {
                    if (UType.MyCtoD(RowID) == RowCtr)
                    {
                       
                        //retVal = row["Rate"].ToString();
                        retVal = Convert.ToString(UType.MyCtoD(row["EndDay"].ToString()) - UType.MyCtoD(row["StartDay"].ToString()));

                    }
                    RowCtr++;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return retVal;
    }
    private string GetRate(DataSet Ds1, string RowID, string SizenType)
    {
        string retVal = ""; int IntRowID = Convert.ToInt32(RowID); int RowCtr = 0;
        try
        {
            foreach (DataRow row in Ds1.Tables["TblEqDet"].Rows)
            {
                if (row["sizentype"].ToString() == SizenType)
                {
                    if (UType.MyCtoD(RowID) == RowCtr)
                    {
                        // retVal = Ds1.Tables["TblEqDet"].Rows[IntRowID]["Rate"].ToString();
                        retVal = row["Rate"].ToString();

                    }
                    RowCtr++;
                }
            }

        }
        catch (Exception ex)
        {

        }
        return retVal;
    }
    private string LogDays(string EntryDate, string ReturnDate)
    {
        decimal logdays = 0;
        try
        {
            var sDate = new DateTime(Convert.ToInt16(EntryDate.Substring(0, 4)), Convert.ToInt16(EntryDate.Substring(4, 2)), Convert.ToInt16(EntryDate.Substring(6, 2))); //15 July 2021
            var eDate = new DateTime(Convert.ToInt16(ReturnDate.Substring(0, 4)), Convert.ToInt16(ReturnDate.Substring(4, 2)), Convert.ToInt16(ReturnDate.Substring(6, 2))); //15 July 2021
            var diffOfDates = eDate - sDate;
            logdays = diffOfDates.Days;
        }
        catch (Exception)
        {


        }

        return logdays.ToString();
    }
    private string AddDetentionDays(string EntryDate, string DetentionDays)
    {
        string retval = "";

        try
        {

            var sDate = new DateTime(Convert.ToInt16(EntryDate.Substring(0, 4)), Convert.ToInt16(EntryDate.Substring(4, 2)), Convert.ToInt16(EntryDate.Substring(6, 2))); //15 July 2021
            sDate = sDate.AddDays(Convert.ToDouble(DetentionDays));

            retval = sDate.ToString("yyyy") + sDate.ToString("MM") + sDate.ToString("dd");


        }
        catch (Exception)
        {


        }

        return retval;
    }
    private decimal LessDetentionDays(string EntryDate, string DetentionDays)
    {
        string retval = ""; double dDetentionDays= Convert.ToDouble(DetentionDays); dDetentionDays = dDetentionDays + dDetentionDays;
         
        try
        {

            var sDate = new DateTime(Convert.ToInt16(EntryDate.Substring(0, 4)), Convert.ToInt16(EntryDate.Substring(4, 2)), Convert.ToInt16(EntryDate.Substring(6, 2))); //15 July 2021
            sDate = sDate.AddDays(Convert.ToDouble(DetentionDays) - dDetentionDays);
            
            retval = sDate.ToString("yyyy") + sDate.ToString("MM")+ sDate.ToString("dd"); 


        }
        catch (Exception)
        {


        }

        return Convert.ToDecimal(retval);
    }
    private decimal GetDrTotal(DataTable IDs, decimal oActCode)
    {
        decimal retVal = 0;
        try
        {
            foreach (DataRow row1 in IDs.Rows)
            {
                if (oActCode == UType.MyCtoD(row1["ActCode"].ToString()))
                {
                    if (row1["Status"].ToString() == "D")
                    {
                        retVal += UType.MyCtoD(row1["Amount"].ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
        return retVal;
    }
    private decimal GetCrTotal(DataTable IDs, decimal oActCode)
    {
        {
            decimal retVal = 0;
            try
            {
                foreach (DataRow row1 in IDs.Rows)
                {
                    if (oActCode == UType.MyCtoD(row1["ActCode"].ToString()))
                    {
                        if (row1["Status"].ToString() == "C")
                        {
                            retVal += UType.MyCtoD(row1["Amount"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return retVal;
        }
    }

    private decimal GetPurchaseOpeningBalance(DataSet IDs, decimal oActCode, decimal sDate)
    {
        decimal retVal = 0;
        decimal sFY = UType.MyCtoD(IDs.Tables[0].Rows[0]["Sdfyear"].ToString());
        decimal tranDate = UType.MyCtoD(IDs.Tables["Detail"].Rows[0]["trandate"].ToString());

        foreach (DataRow row1 in IDs.Tables["Detail"].Rows)
        {
            if (oActCode == UType.MyCtoD(row1["ProductId"].ToString()))
            {
                if (row1["vtypeid"].ToString() == "7")
                {
                    tranDate = UType.MyCtoD(IDs.Tables["Detail"].Rows[0]["trandate"].ToString());
                    if (tranDate >= sFY && tranDate < sDate)
                        retVal = UType.MyCtoD(row1["quantity"].ToString());
                }
            }
        }
        return retVal;
    }
    private decimal GetSaleOpeningBalance(DataSet IDs, decimal oActCode, decimal sDate)
    {
        decimal retVal = 0;
        decimal sFY = UType.MyCtoD(IDs.Tables[0].Rows[0]["Sdfyear"].ToString());
        decimal tranDate = UType.MyCtoD(IDs.Tables["Detail"].Rows[0]["trandate"].ToString());

        foreach (DataRow row1 in IDs.Tables["Detail"].Rows)
        {
            if (oActCode == UType.MyCtoD(row1["ProductId"].ToString()))
            {
                if (row1["vtypeid"].ToString() == "6")
                {
                    tranDate = UType.MyCtoD(IDs.Tables["Detail"].Rows[0]["trandate"].ToString());
                    if (tranDate >= sFY && tranDate < sDate)
                        retVal = UType.MyCtoD(row1["quantity"].ToString());
                }
            }
        }
        return retVal;
    }
    private string GetName(DataTable IDs)
    {
        string retVal = "";
        foreach (DataRow row1 in IDs.Rows)
        {

            if (row1["actcode"].ToString().Substring(0, 6) == "200101" || row1["actcode"].ToString().Substring(0, 6) == "100105")
            {
                retVal = row1["actdesc"].ToString();
            }

        }
        return retVal;
    }
    public DataSet MoveInRptDsLedger(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal CurActCode = 0;
        decimal OpeningBalanceAmt = 0;
        string OpeningBalanceSts = "";

        decimal TotalDrBal = 0;
        decimal TotalCrBal = 0;
        decimal BalanceAmt = 0;
        string BalanceSts = "D";
        decimal Sno = 1;

        try
        {

            CurActCode = 0; // UType.MyCtoD(InputDataSet.Tables["Detail"].Rows[0]["ActCode1"].ToString());
            foreach (DataRow row1 in InputDataSet.Tables["Detail"].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = "ABC";

                row["C9"] = UType.GetDate1(sDate);
                row["C10"] = UType.GetDate1(eDate);
                row["C11"] = row1["ActCode1"].ToString();
                row["C12"] = row1["ActCode1"].ToString() + " " + row1["ActName"].ToString();
                // row["C32"] = row1["ActName"].ToString();
                //Get Opening Balance
                if (CurActCode != UType.MyCtoD(row1["ActCode1"].ToString()))  //UType.MyCtoD(InputDataSet.Tables["Detail"].Rows[0]["ActCode1"].ToString()))
                {
                    TotalDrBal = GetDrTotal(InputDataSet.Tables[0], UType.MyCtoD(row1["ActCode1"].ToString()));
                    TotalCrBal = GetCrTotal(InputDataSet.Tables[0], UType.MyCtoD(row1["ActCode1"].ToString()));
                    if (TotalDrBal >= TotalCrBal)
                    {
                        OpeningBalanceAmt = TotalDrBal - TotalCrBal;
                        OpeningBalanceSts = "D";
                    }
                    else
                    {
                        OpeningBalanceAmt = TotalCrBal - TotalDrBal;
                        OpeningBalanceSts = "C";
                    }
                    BalanceAmt = OpeningBalanceAmt;
                    BalanceSts = OpeningBalanceSts;
                    CurActCode = UType.MyCtoD(row1["ActCode1"].ToString());
                }
                //End of Opening Balance
                row["C13"] = OpeningBalanceAmt.ToString() + " " + OpeningBalanceSts;

                row["C14"] = UType.GetDateTxt(row1["TranDate"].ToString()); //row1["VNo"].ToString();
                row["C15"] = row1["VtypeDescription"].ToString();
                row["C30"] = row1["Vtype"].ToString() + "-" + row1["VNo"].ToString();
                row["C21"] = UType.GetDate1(row1["TranDate"].ToString());
                row["C24"] = row1["chqno"].ToString() + " " + UType.GetDateTxt(row1["chqdate"].ToString());
                // row["C25"] = UType.GetDate1(row1["chqdate"].ToString()); //row1["Narration"].ToString();
                row["C16"] = row1["Narration"].ToString();
                row["C17"] = row1["Amount"].ToString();  //Amount display in Dr side

                //row["C30"] = row1["Amount"].ToString();
                #region BalanceSts D
                if (BalanceSts == "D")
                {
                    if (row1["AmountSts"].ToString() == "D")
                    {
                        BalanceAmt += UType.MyCtoD(row1["Amount"].ToString());
                    }
                    if (row1["AmountSts"].ToString() == "C")
                    {
                        if (UType.MyCtoD(row1["Amount"].ToString()) > BalanceAmt)
                        {
                            BalanceSts = "C";
                        }
                        BalanceAmt = Math.Abs(UType.MyCtoD(row1["Amount"].ToString()) - BalanceAmt);
                    }
                }
                else if (BalanceSts == "C")
                #endregion

                #region BalanceSts C


                {
                    if (row1["AmountSts"].ToString() == "C")
                    {
                        BalanceAmt += UType.MyCtoD(row1["Amount"].ToString());
                    }
                    if (row1["AmountSts"].ToString() == "D")
                    {
                        if (UType.MyCtoD(row1["Amount"].ToString()) > BalanceAmt)
                        {
                            BalanceSts = "D";
                        }
                        BalanceAmt = Math.Abs(UType.MyCtoD(row1["Amount"].ToString()) - BalanceAmt);
                    }
                }
                #endregion

                if (row1["AmountSts"].ToString() == "C")
                {
                    row["C17"] = "0";
                    row["C18"] = row1["Amount"].ToString();
                }
                row["CLogo"] = GetImage();


                row["C19"] = UType.MyFormat(BalanceAmt) + " " + BalanceSts;
                row["C20"] = BalanceSts;
                row["C21"] = row1["ExpenseDisplay"].ToString();
                row["C22"] = row1["ExpenseReceive"].ToString();
                row["C23"] = Sno.ToString(); ///UType.MyCtoD(row1["ExpenseDisplay"].ToString()) - UType.MyCtoD(row1["ExpenseReceive"].ToString());
                row["C26"] = row1["city_short"].ToString();
                //row["C24"] = row1["Width"].ToString();
                rptDataSet.Tables[0].Rows.Add(row);
                Sno = Sno + 1;
            }
            decimal TotalDr = 0; decimal TotalCr = 0;
            foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
            {
                TotalDr = TotalDr + UType.MyCtoD(row1["C17"].ToString());
                TotalCr = TotalCr + UType.MyCtoD(row1["C18"].ToString());
            }
            foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
            {
                row1["C17"] = UType.MyFormat(row1["C17"].ToString());
                row1["C18"] = UType.MyFormat(row1["C18"].ToString());
                //row1["C19"] = UType.MyFormat(row1["C19"].ToString());
                row1["C27"] = UType.MyFormat(TotalDr.ToString());
                row1["C28"] = UType.MyFormat(TotalCr.ToString());

            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet MoveInRptDsLedgerSOA(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal CurActCode = 0;
        decimal OpeningBalanceAmt = 0;
        string OpeningBalanceSts = "";

        decimal TotalDrBal = 0;
        decimal TotalCrBal = 0;
        decimal BalanceAmt = 0;
        string BalanceSts = "D";
        decimal Sno = 1;

        try
        {

            CurActCode = 0; // UType.MyCtoD(InputDataSet.Tables["Detail"].Rows[0]["ActCode1"].ToString());
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = "ABC";
                row["C23"] = row1["Principalcode"].ToString();
                row["C15"] = row1["ActDesc"].ToString();
                row["C16"] = row1["ExpenseDisplay"].ToString();
                row["C25"] = row1["ExpenseReceive"].ToString();
                row["C26"] = row1["ProfitLoss"].ToString();
                row["C17"] = row1["VNo"].ToString();
                row["C18"] = UType.GetDateTxt(row1["adddate"].ToString());
                row["C30"] = row1["VNo"].ToString() + " " + UType.GetDateTxt(row1["adddate"].ToString());

                row["C19"] = row1["jobno"].ToString();
                row["C20"] = UType.GetDateTxt(row1["adddate"].ToString()); //row1["VNo"].ToString();

                row["C31"] = row1["jobno"].ToString() + " " + UType.GetDateTxt(row1["adddate"].ToString()); //row1["VNo"].ToString();

                row["C21"] = row1["Expense"].ToString();


                row["CLogo"] = GetImage();



                rptDataSet.Tables[0].Rows.Add(row);
                Sno = Sno + 1;
            }
            decimal TotalDr = 0; decimal TotalCr = 0; decimal TotalCr1 = 0;
            foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
            {
                TotalDr = TotalDr + UType.MyCtoD(row1["C16"].ToString());
                TotalCr = TotalCr + UType.MyCtoD(row1["C25"].ToString());
                TotalCr1 = TotalCr1 + UType.MyCtoD(row1["C26"].ToString());
            }
            decimal ctr = 1;
            foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
            {
                row1["C23"] = ctr.ToString();
                row1["C22"] = UType.MyFormat(TotalDr.ToString());
                row1["C27"] = UType.MyFormat(TotalCr.ToString());
                row1["C24"] = UType.MyFormat(TotalCr1.ToString());
                ctr = ctr + 1;

            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }


    private decimal DrCrAmount(decimal TotalDrBal, decimal TotalCrBal)
    {
        decimal BalanceAmt = 0;
        //string BalanceSts = "";
        try
        {
            if (TotalDrBal >= TotalCrBal)
            {
                BalanceAmt = TotalDrBal - TotalCrBal;
                //BalanceSts = "D";
            }
            else
            {
                BalanceAmt = TotalCrBal - TotalDrBal;
                //BalanceSts = "C";
            }
            //BalanceSts = OpeningBalanceSts;
        }
        catch (Exception ex)
        {

            throw;
        }
        return BalanceAmt;
    }

    private string DrCrStatus(decimal TotalDrBal, decimal TotalCrBal)
    {
        string retVal = "D";
        try
        {
            if (TotalDrBal >= TotalCrBal)
            {
                retVal = "D";
            }
            else
            {
                retVal = "C";
            }
        }
        catch (Exception ex)
        {

            throw;
        }
        return retVal;
    }

    public DataSet MoveInRptDsTrial(DataSet InputDataSet, string sDate, string eDate, string IsZero)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal OpeningBal = 0; string OpeningSts = "D";
        decimal ActivityBal = 0; string ActivitySts = "D";
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = UType.GetDate1(sDate);
                row["C2"] = UType.GetDate1(eDate);
                row["C3"] = row1["OfficeId"].ToString();
                row["C4"] = row1["ProjectId"].ToString();
                row["C5"] = row1["ParentCode"].ToString();
                row["C9"] = row1["ParentDesc"].ToString();
                row["C6"] = row1["ParentCode1"].ToString();
                row["C10"] = row1["ParentDesc1"].ToString();
                row["C7"] = row1["ParentCode2"].ToString();
                row["C11"] = row1["ParentDesc2"].ToString();
                if (row1["ActCode"].ToString().Length > 6)
                {
                    row["C8"] = row1["ActCode"].ToString();
                    row["C12"] = row1["ActDesc"].ToString();
                    //new
                    row["C15"] = row1["OpeningDr"].ToString() + " " + row1["OpeningDrStatus"].ToString();
                    OpeningBal = UType.MyCtoD(row1["OpeningDr"].ToString());
                    OpeningSts = row1["OpeningDrStatus"].ToString();
                    if (UType.MyCtoD(row1["OpeningCr"].ToString()) > UType.MyCtoD(row1["OpeningDr"].ToString()))
                    {
                        row["C15"] = row1["OpeningCr"].ToString() + " " + row1["OpeningCrStatus"].ToString(); ;
                        OpeningBal = UType.MyCtoD(row1["OpeningCr"].ToString());
                        OpeningSts = row1["OpeningCrStatus"].ToString();
                    }
                    if (OpeningSts == row1["OpeningDrStatus"].ToString())
                    {
                        ActivityBal = UType.MyCtoD(row1["OpeningDr"].ToString()) + UType.MyCtoD(row1["ActivityDr"].ToString());
                        ActivitySts = "D";
                        row["C16"] = Convert.ToString(ActivityBal) + " D";
                        row["C18"] = Convert.ToString(ActivityBal);
                        row["C19"] = ActivitySts;
                    }
                    if (OpeningSts == row1["OpeningCrStatus"].ToString())
                    {
                        ActivityBal = UType.MyCtoD(row1["OpeningCr"].ToString()) + UType.MyCtoD(row1["ActivityCr"].ToString());
                        ActivitySts = "C";
                        row["C17"] = Convert.ToString(ActivityBal) + " C";
                        row["C20"] = Convert.ToString(ActivityBal);
                        row["C21"] = ActivitySts;
                    }
                }

                //new
                if (IsZero == "1")
                {
                    bool isWrite = false;
                    if (row1["ActCode"].ToString().Length < 8)
                    {
                        isWrite = true;
                    }
                    if (row1["ActCode"].ToString().Length >= 8)
                    {
                        if (ActivityBal > 0)
                        {
                            isWrite = true;
                        }

                    }
                    if (isWrite)
                    {
                        row["CLogo"] = GetImage();
                        rptDataSet.Tables[0].Rows.Add(row);
                    }
                }

                if (IsZero == "0")
                {
                    row["CLogo"] = GetImage();
                    rptDataSet.Tables[0].Rows.Add(row);
                }

            }

        }
        catch (Exception ex)
        {
            string err = ex.Message.ToString();
        }


        return rptDataSet;
    }

    public DataSet MoveInRptDsTrialOld(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal OpeningBal = 0; string OpeningSts = "D";
        decimal ActivityBal = 0; string ActivitySts = "D";
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = UType.GetDate1(sDate);
                row["C2"] = UType.GetDate1(eDate);
                row["C3"] = row1["OfficeId"].ToString();
                row["C4"] = row1["ProjectId"].ToString();
                //if (row1["ActCode"].ToString().Length == 1)
                // {
                row["C5"] = row1["ParentCode"].ToString();
                row["C9"] = row1["ParentDesc"].ToString();
                // }

                // if (row1["ActCode"].ToString().Length == 3)
                // {
                row["C6"] = row1["ParentCode1"].ToString();
                row["C10"] = row1["ParentDesc1"].ToString();
                // }

                // if (row1["ActCode"].ToString().Length == 6)
                // {
                row["C7"] = row1["ParentCode2"].ToString();
                row["C11"] = row1["ParentDesc2"].ToString();
                // }

                // if (row1["ActCode"].ToString().Length > 6)
                //{
                row["C8"] = row1["ActCode"].ToString();
                row["C12"] = row1["ActDesc"].ToString();
                //}
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);
            }
            foreach (DataRow row in rptDataSet.Tables[0].Rows)
            {
                if (row["C8"].ToString().Length > 6)
                {
                    try
                    {
                        #region Opening
                        DataSet Opening = null;
                        MyMain oMy = new MyMain();
                        oMy = new MyMain();
                        oMy.Fld1 = row["C3"].ToString();
                        oMy.Fld2 = row["C4"].ToString();
                        oMy.Fld3 = row["C8"].ToString();
                        oMy.Fld4 = UType.SetDate(row["C1"].ToString());
                        oMy.Fld5 = UType.SetDate(row["C2"].ToString());
                        Opening = oMy.GetOpeningBalance();
                        if (Opening != null)
                        {
                            if (Opening.Tables[0].Rows[0]["OpeningSts"].ToString() == "D")
                            {
                                OpeningSts = "D";
                                OpeningBal = UType.MyCtoD(Opening.Tables[0].Rows[0]["OpeningBal"].ToString());
                                if (Opening.Tables[0].Rows.Count > 1)
                                {
                                    OpeningBal = OpeningBal - UType.MyCtoD(Opening.Tables[0].Rows[1]["OpeningBal"].ToString());
                                }
                            }
                            if (Opening.Tables[0].Rows[0]["OpeningSts"].ToString() == "C")
                            {
                                OpeningSts = "C";
                                OpeningBal = UType.MyCtoD(Opening.Tables[0].Rows[0]["OpeningBal"].ToString());
                                if (Opening.Tables[0].Rows.Count > 1)
                                {
                                    OpeningBal = OpeningBal - UType.MyCtoD(Opening.Tables[0].Rows[1]["OpeningBal"].ToString());
                                }
                            }
                        }
                        #endregion

                        row["C15"] = OpeningBal.ToString() + " " + OpeningSts;

                        #region Activity
                        DataSet Activity = null;
                        oMy = new MyMain();
                        oMy.Fld1 = row["C3"].ToString();
                        oMy.Fld2 = row["C4"].ToString();
                        oMy.Fld3 = row["C8"].ToString();
                        oMy.Fld4 = UType.SetDate(row["C1"].ToString());
                        oMy.Fld5 = UType.SetDate(row["C2"].ToString());
                        Activity = oMy.GetActivity();
                        if (Activity != null)
                        {
                            if (Activity.Tables[0].Rows[0]["ActivitySts"].ToString() == "D")
                            {
                                ActivitySts = "D";
                                ActivityBal = UType.MyCtoD(Activity.Tables[0].Rows[0]["ActivityBal"].ToString());
                                if (Activity.Tables[0].Rows.Count > 1)
                                {
                                    ActivityBal = ActivityBal - UType.MyCtoD(Opening.Tables[0].Rows[1]["ActivityBal"].ToString());
                                }
                            }
                            if (Opening.Tables[0].Rows[0]["ActivitySts"].ToString() == "C")
                            {
                                ActivitySts = "C";
                                ActivityBal = UType.MyCtoD(Opening.Tables[0].Rows[0]["ActivityBal"].ToString());
                                if (Opening.Tables[0].Rows.Count > 1)
                                {
                                    ActivityBal = ActivityBal - UType.MyCtoD(Opening.Tables[0].Rows[1]["ActivityBal"].ToString());
                                }
                            }
                        }
                        #endregion

                        if (OpeningSts == "D")
                        {
                            if (ActivitySts == "D")
                            {
                                ActivityBal = ActivityBal + OpeningBal;
                            }
                            if (ActivitySts == "C")
                            {
                                ActivityBal = ActivityBal - OpeningBal;
                                if (ActivityBal < 0)
                                {
                                    ActivitySts = "D";
                                }
                            }
                        }
                        if (OpeningSts == "C")
                        {
                            if (ActivitySts == "C")
                            {
                                ActivityBal = ActivityBal + OpeningBal;
                            }
                            if (ActivitySts == "D")
                            {
                                ActivityBal = ActivityBal - OpeningBal;
                                if (ActivityBal < 0)
                                {
                                    ActivitySts = "C";
                                }
                            }
                        }
                        if (ActivitySts == "D")
                        {
                            row["C16"] = ActivityBal.ToString();
                        }
                        if (ActivitySts == "C")
                        {
                            row["C17"] = ActivityBal.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }


            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        DataSet rptDataSet1 = UType.SetRptDataset();
        bool IsWrite = false;
        foreach (DataRow row in rptDataSet.Tables[0].Rows)
        {
            IsWrite = false;
            if (row["C8"].ToString().Length < 6)
            {
                IsWrite = true;
            }
            if (row["C8"].ToString().Length > 6)
            {
                if (UType.MyCtoD(row["C16"].ToString()) > 0 || UType.MyCtoD(row["C17"].ToString()) > 0)
                {
                    IsWrite = true;
                }
            }
            if (IsWrite)
            {
                DataRow row5 = rptDataSet1.Tables[0].NewRow();
                row5["CLogo"] = GetImage();
                row5["C1"] = row["C1"];
                row5["C2"] = row["C2"];
                row5["C3"] = row["C3"];
                row5["C4"] = row["C4"];
                row5["C5"] = row["C5"];
                row5["C6"] = row["C6"];
                row5["C7"] = row["C7"];
                row5["C8"] = row["C8"];
                row5["C9"] = row["C9"];
                row5["C10"] = row["C10"];
                row5["C11"] = row["C11"];
                row5["C12"] = row["C12"];
                row5["C13"] = row["C13"];
                row5["C14"] = row["C14"];
                row5["C15"] = row["C15"];
                row5["C16"] = row["C16"];
                row5["C17"] = row["C17"];
                rptDataSet1.Tables[0].Rows.Add(row5);
            }
        }

        return rptDataSet1;
    }

    public DataSet MoveInRptDsIncome(DataSet InputDataSet, string sDate, string eDate, string IsZero)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal OpeningBal = 0; string OpeningSts = "D";
        decimal ActivityBal = 0; string ActivitySts = "D";
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = UType.GetDate1(sDate);
                row["C2"] = UType.GetDate1(eDate);
                row["C3"] = row1["OfficeId"].ToString();
                row["C4"] = row1["ProjectId"].ToString();
                row["C5"] = row1["ParentCode"].ToString();
                row["C9"] = row1["ParentDesc"].ToString();
                row["C6"] = row1["ParentCode1"].ToString();
                row["C10"] = row1["ParentDesc1"].ToString();
                row["C7"] = row1["ParentCode2"].ToString();
                row["C11"] = row1["ParentDesc2"].ToString();
                if (row1["ActCode"].ToString().Length > 6)
                {
                    row["C8"] = row1["ActCode"].ToString();
                    row["C12"] = row1["ActDesc"].ToString();
                    //new
                    row["C15"] = row1["OpeningDr"].ToString() + " " + row1["OpeningDrStatus"].ToString();
                    OpeningBal = UType.MyCtoD(row1["OpeningDr"].ToString());
                    OpeningSts = row1["OpeningDrStatus"].ToString();
                    if (UType.MyCtoD(row1["OpeningCr"].ToString()) > UType.MyCtoD(row1["OpeningDr"].ToString()))
                    {
                        row["C15"] = row1["OpeningCr"].ToString() + " " + row1["OpeningCrStatus"].ToString(); ;
                        OpeningBal = UType.MyCtoD(row1["OpeningCr"].ToString());
                        OpeningSts = row1["OpeningCrStatus"].ToString();
                    }
                    if (OpeningSts == row1["OpeningDrStatus"].ToString())
                    {
                        ActivityBal = UType.MyCtoD(row1["OpeningDr"].ToString()) + UType.MyCtoD(row1["ActivityDr"].ToString());
                        ActivitySts = "D";
                        row["C16"] = Convert.ToString(ActivityBal) + " D";
                        row["C18"] = Convert.ToString(ActivityBal);
                        row["C19"] = ActivitySts;
                    }
                    if (OpeningSts == row1["OpeningCrStatus"].ToString())
                    {
                        ActivityBal = UType.MyCtoD(row1["OpeningCr"].ToString()) + UType.MyCtoD(row1["ActivityCr"].ToString());
                        ActivitySts = "C";
                        row["C17"] = Convert.ToString(ActivityBal) + " C";
                        row["C20"] = Convert.ToString(ActivityBal);
                        row["C21"] = ActivitySts;
                    }
                }

                //new
                if (IsZero == "1")
                {
                    bool isWrite = false;
                    if (row1["ActCode"].ToString().Length < 8)
                    {
                        isWrite = true;
                    }
                    if (row1["ActCode"].ToString().Length >= 8)
                    {
                        if (ActivityBal > 0)
                        {
                            isWrite = true;
                        }

                    }
                    if (isWrite)
                    {
                        row["CLogo"] = GetImage();
                        rptDataSet.Tables[0].Rows.Add(row);
                    }
                }

                if (IsZero == "0")
                {
                    row["CLogo"] = GetImage();
                    rptDataSet.Tables[0].Rows.Add(row);
                }

            }

        }
        catch (Exception ex)
        {
            string err = ex.Message.ToString();
        }


        return rptDataSet;
    }
    public DataSet MoveInCostingDtl(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row1 = InputDataSet.Tables[0].Rows[0];
            DataRow rowCh = InputDataSet.Tables["TblCh"].Rows[0];

            string ArrivalDate = UType.GetDateTxt(row1["ArrivalDate"].ToString());  //GetActCity1(row1["PortLoading"].ToString());
            string Portofloading = GetActCity1(row1["Portloading"].ToString());
            string PortDischarge = GetActCity1(row1["PortDischarge"].ToString());
            string FinalDestination = GetActCity1(row1["FinalDestination"].ToString());
            string ShippingCompanyID = GetDescriptionDDL(row1["ShippingCompanyID"].ToString());

            string ShipperName = "";
            string ConsigneeName = ""; string Consigne = "";
            decimal LocalAmount = 0;
            #region ConsigneeName
            MyMain oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["Consignee"].ToString();
                Consigne = row1["Consignee"].ToString();
                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ConsigneeName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion

            #region ShipperName
            oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["Shipper"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ShipperName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion
            //
            decimal TotalOtherCharges = 0; decimal TotalDet = 0; decimal TotalAmount = 0; decimal TotalFC = 0; decimal TotalFCpkr = 0; decimal TotalDiscount = 0;
            decimal TotalNetAmount = 0; decimal DetDays = 0; decimal TotalPlugin = 0;
            foreach (DataRow rowEq in InputDataSet.Tables["TblEq"].Rows)
            {
                TotalDet = TotalDet + UType.MyCtoD(rowEq["totaldetention"].ToString());
                TotalFCpkr = TotalFCpkr + (UType.MyCtoD(rowEq["totaldetention"].ToString()) * UType.MyCtoD(rowEq["exrate"].ToString()));
                TotalPlugin = TotalPlugin + UType.MyCtoD(rowEq["plugin"].ToString());

                TotalOtherCharges = TotalOtherCharges + UType.MyCtoD(rowEq["WashingCharges"].ToString()) + UType.MyCtoD(rowEq["DocumentCharges"].ToString()) + UType.MyCtoD(rowEq["Demurrage"].ToString());

                TotalAmount = TotalAmount + (UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString())) * UType.MyCtoD(rowEq["exrate"].ToString());
                DetDays = UType.MyCtoD(rowEq["logdays"].ToString()) - UType.MyCtoD(rowEq["logdays"].ToString());
                //TotalFC = TotalFC + (UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()));

                TotalDiscount = TotalDiscount + (UType.MyCtoD(rowEq["Discount"].ToString()) * UType.MyCtoD(rowEq["exrate"].ToString()));
                TotalNetAmount = TotalNetAmount + (UType.MyCtoD(rowEq["totaldetention"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString()) * UType.MyCtoD(rowEq["exrate"].ToString()));
            }

            foreach (DataRow rowEq in InputDataSet.Tables["TblEq"].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();

                oMy = new MyMain();
                try
                {
                    row["C1"] = "DET " + "-" + rowEq["jobno"].ToString() + "/" + rowEq["jobcy"].ToString();
                    row["C2"] = UType.GetDateTxt(rowEq["InvoiceDate"].ToString());
                    row["C3"] = row1["IGMNo"].ToString();

                    //if (UType.MyCtoD(rowEq["InvoiceNo"].ToString()) < 1)
                    //{
                    //    row["C11"] = "JP " + "-" + GetMaxInvoiceNoCon(rowEq["officeid"].ToString(), rowEq["Projectid"].ToString(), UType.GetCY(), Consigne, rowEq["jobno"].ToString());
                    //    string cDate = DateTime.Now.ToString("yyyyMMdd");
                    //    row["C12"] = UType.GetDateTxt(cDate);
                    // }

                    row["C13"] = ConsigneeName;
                    row["C14"] = UType.GetDateTxt(rowEq["ContainerReturnDate"].ToString());
                    row["C15"] = ShipperName;
                    row["C16"] = UType.GetDateTxt(row1["IGMDate"].ToString());
                    row["C17"] = Portofloading;
                    row["C18"] = ConsigneeName;
                    row["C19"] = PortDischarge;
                    row["C20"] = FinalDestination;
                    row["C21"] = rowEq["ExRate"].ToString();
                    row["C22"] = row1["HBLNo"].ToString();
                    row["C23"] = row1["MBLNo"].ToString();// row1["Vessel"].ToString();
                                                          //if (row1["Voyage"].ToString().Length > 0)
                                                          //{
                    row["C24"] = row1["Vessel"].ToString() + " / " + row1["Voyage"].ToString() + " / " + PortDischarge;   // row["C23"].ToString() + " / " + row1["Voyage"].ToString();
                    //}
                    //row["C24"] = UType.GetDateTxt(row1["jobDate"].ToString());
                    row["C25"] = row1["IndexNo"].ToString();
                    row["C26"] = "PKR";//GetCurrencyString(rowCh["currency"].ToString());
                    // row["C26"] = rowCh["jobno"].ToString();
                    row["C27"] = UType.GetDateTxt(row1["ArrivalDate"].ToString());
                    row["C28"] = UType.GetDateTxt(rowEq["ContainerArrvalDate"].ToString());
                    row["C29"] = UType.GetDateTxt(rowEq["ContainerReturnDate"].ToString()); ;
                    row["C30"] = "We enclose an Invoice for Container detention charges under mentioned:";
                    row["C4"] = "Detention";
                    row["C5"] = rowEq["ContainerNo"].ToString();
                    row["C6"] = rowEq["Detention"].ToString();  //rowEq["IsManual1"].ToString();
                    row["C7"] = rowEq["DetHdrFreeDays"].ToString(); //LogDays(rowEq["ContainerEntryDate"].ToString(), rowEq["ContainerReturnDate"].ToString());
                    row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                    row["C9"] = GetCurrencyString(rowEq["curren"].ToString());
                    row["C10"] = UType.MyFormat(rowEq["totaldetention"].ToString());  //rowEq["SecurityAmountReceive"].ToString();
                    row["C31"] = rowEq["Discount"].ToString();  // rowEq["SecurityAmountPaid"].ToString();
                                                                //row["C33"] = UType.MyFormat((UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString())) * UType.MyCtoD(rowEq["exrate"].ToString()));
                    row["C33"] = UType.MyFormat((UType.MyCtoD(rowEq["totaldetention"].ToString()) - UType.MyCtoD(rowEq["discount"].ToString())) * UType.MyCtoD(rowEq["exrate"].ToString()));
                    row["C34"] = UType.MyFormat(TotalFC.ToString());


                    row["C36"] = UType.MyFormat(TotalFCpkr.ToString());
                    row["C37"] = "";
                    row["C38"] = UType.MyFormat(TotalOtherCharges.ToString());
                    //row["C39"] = UType.MyFormat(TotalAmount.ToString());
                    row["C40"] = UType.MyFormat(TotalDiscount.ToString());
                    row["C51"] = UType.MyFormat(TotalNetAmount.ToString());
                    row["C53"] = "PKR " + UType.NumberToWords(UType.MyCtoD(TotalNetAmount.ToString())) + " Only";
                    //row["C4"] = UType.MyFormat(UType.MyCtoD(rowEq["WashingCharges"].ToString()) + UType.MyCtoD(rowEq["DocumentCharges"].ToString()) + UType.MyCtoD(rowEq["Demurrage"].ToString()));
                    //row["C4"] = rowEq["WashingCharges"].ToString();
                    if (UType.MyCtoD(rowEq["PrincipalCode"].ToString()) > 0)  // if (UType.MyCtoD(rowEq["PrincipalEquipInv"].ToString()) > 0)
                    {
                        row["C4"] = "Detention";
                        row["C5"] = rowEq["ContainerNo"].ToString();
                        row["C10"] = UType.MyFormat(rowEq["PrincipalEquipInv"].ToString());
                        // if (UType.MyCtoD(rowEq["TotalDetention"].ToString()) > 0)
                        // {
                        //   row["CLogo"] = GetImage();
                        //   rptDataSet.Tables[0].Rows.Add(row);
                        //  row = rptDataSet.Tables[0].NewRow();
                        // row["C4"] = "Total Detention";
                        row["C5"] = rowEq["ContainerNo"].ToString();
                        row["C10"] = UType.MyFormat(rowEq["TotalDetention"].ToString());
                        row["C6"] = rowEq["Detention"].ToString(); //"0";
                        row["C7"] = rowEq["logdays"].ToString(); //"0";
                        row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                        //row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                        // row["C31"] = "0";
                        row["C32"] = "0";
                        //row["C33"] = UType.MyFormat(rowEq["TotalDetention"].ToString());
                        // }
                        if (UType.MyCtoD(rowEq["Damagecharges"].ToString()) > 0) //
                        {
                            row["CLogo"] = GetImage();
                            row["C41"] = rowEq["PrincipalCode"].ToString();
                            row["C42"] = rowEq["ContainerArrvalDate"].ToString();
                            row["C43"] = rowEq["ContainerReturnDate"].ToString();
                            row["C44"] = Convert.ToString(UType.MyCtoD(rowEq["logDays"].ToString()) - UType.MyCtoD(rowEq["Detention"].ToString()));

                            rptDataSet.Tables[0].Rows.Add(row);
                            row = rptDataSet.Tables[0].NewRow();
                            row["C4"] = "Damage Charges";
                            row["C5"] = rowEq["ContainerNo"].ToString();
                            row["C33"] = UType.MyFormat(rowEq["Damagecharges"].ToString());
                            row["C6"] = "0";  //rowEq["IsManual1"].ToString();
                            row["C7"] = rowEq["DetHdrFreeDays"].ToString(); //"0";;
                            row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                            row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                            // row["C31"] = "0";
                            row["C32"] = "0";
                            //row["C33"] = UType.MyFormat(rowEq["Demurrage"].ToString());
                        }
                        if (UType.MyCtoD(rowEq["DocumentCharges"].ToString()) > 0)
                        {
                            row["CLogo"] = GetImage();
                            row["C41"] = rowEq["PrincipalCode"].ToString();
                            row["C42"] = rowEq["ContainerArrvalDate"].ToString();
                            row["C43"] = rowEq["ContainerReturnDate"].ToString();
                            row["C44"] = Convert.ToString(UType.MyCtoD(rowEq["logDays"].ToString()) - UType.MyCtoD(rowEq["Detention"].ToString()));
                            rptDataSet.Tables[0].Rows.Add(row);
                            row = rptDataSet.Tables[0].NewRow();
                            row["C4"] = "DocumentCharges";
                            row["C5"] = rowEq["ContainerNo"].ToString();
                            row["C33"] = UType.MyFormat(rowEq["DocumentCharges"].ToString());
                            row["C6"] = "0";  //rowEq["IsManual1"].ToString();
                            row["C7"] = "0";
                            row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                            row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                            row["C31"] = "0";
                            row["C32"] = "0";
                            //row["C33"] = UType.MyFormat(rowEq["DocumentCharges"].ToString());
                        }
                        if (UType.MyCtoD(rowEq["WashingCharges"].ToString()) > 0)
                        {
                            row["CLogo"] = GetImage();
                            row["C41"] = rowEq["PrincipalCode"].ToString();
                            row["C42"] = rowEq["ContainerArrvalDate"].ToString();
                            row["C43"] = rowEq["ContainerReturnDate"].ToString();
                            row["C44"] = Convert.ToString(UType.MyCtoD(rowEq["logDays"].ToString()) - UType.MyCtoD(rowEq["Detention"].ToString()));
                            rptDataSet.Tables[0].Rows.Add(row);
                            row = rptDataSet.Tables[0].NewRow();
                            row["C4"] = "WashingCharges";
                            row["C5"] = rowEq["ContainerNo"].ToString();
                            row["C33"] = UType.MyFormat(rowEq["WashingCharges"].ToString());
                            row["C6"] = "0";  //rowEq["IsManual1"].ToString();
                            row["C7"] = "0";
                            row["C8"] = GetDescriptionDDL(rowEq["sizentype"].ToString());
                            row["C9"] = GetCurrencyString(rowCh["currency"].ToString());

                            row["C31"] = "0";
                            row["C32"] = "0";
                            //row["C33"] = UType.MyFormat(rowEq["DocumentCharges"].ToString());
                        }
                    }


                    row["CLogo"] = GetImage();
                    row["C41"] = rowEq["PrincipalCode"].ToString();
                    row["C42"] = rowEq["ContainerArrvalDate"].ToString();
                    row["C43"] = rowEq["ContainerReturnDate"].ToString();
                    row["C44"] = Convert.ToString(UType.MyCtoD(rowEq["logDays"].ToString()) - UType.MyCtoD(rowEq["Detention"].ToString()));
                    rptDataSet.Tables[0].Rows.Add(row);



                }

                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            //




            decimal DetentionDays = 0; int SNo = 1; string sDate = "";
            foreach (DataRow rowD in InputDataSet.Tables["TblEq"].Rows)
            {
                DetentionDays = UType.MyCtoD(rowD["logDays"].ToString()) - UType.MyCtoD(rowD["Detention"].ToString());
                foreach (DataRow rowR in rptDataSet.Tables[0].Rows)
                {
                    if (rowR["C41"].ToString() == rowD["PrincipalCode"].ToString() && rowR["C4"].ToString() == "Detention")
                    {
                        if (rowD["ContainerNo"] == rowR["C5"].ToString())
                        {
                            //UType.MyCtoD(rowR["DetentionDays"].ToString()) < UType.MyCtoD(rowR["DetentionDays"].ToString())
                            if (DetentionDays < UType.MyCtoD(GetEndDayPlusOne(InputDataSet, "0", rowD["sizentype"].ToString())))  //UType.MyCtoD(rowD["EndDay"].ToString()))
                            {
                                rowR["C3"] = SNo.ToString();
                                rowR["C6"] = rowD["Detention"].ToString();
                                rowR["C11"] = DetentionDays.ToString();
                                rowR["C12"] = GetRate(InputDataSet, "0", rowD["sizentype"].ToString()); ;// rowD["Rate"].ToString();
                                rowR["C7"] = UType.GetDateTxt(rowD["ContainerArrvalDate"].ToString());
                                rowR["C10"] = UType.GetDateTxt(rowD["ContainerReturnDate"].ToString());
                                rowR["C33"] = Convert.ToString(UType.MyCtoD(rowR["C11"].ToString()) * UType.MyCtoD(rowR["C12"].ToString()));
                                try
                                {
                                    rowR["C76"] = Convert.ToString(UType.MyCtoD(rowD["exrate"].ToString()) * UType.MyCtoD(rowR["C33"].ToString()));
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            if (DetentionDays > UType.MyCtoD(GetEndDayPlusOne(InputDataSet, "0", rowD["sizentype"].ToString())))
                            {
                                rowR["C3"] = SNo.ToString();
                                rowR["C6"] = rowD["Detention"].ToString();
                                rowR["C11"] = GetEndDayPlusOne(InputDataSet, "0", rowD["sizentype"].ToString()); //rowD["EndDay"].ToString();
                                rowR["C12"] = GetRate(InputDataSet, "0", rowR["sizentype"].ToString());   //rowD["Rate"].ToString();
                                sDate = rowD["ContainerArrvalDate"].ToString(); // rowR["C42"].ToString();
                                rowR["C7"] = UType.GetDateTxt(rowD["ContainerArrvalDate"].ToString());
                                //rowR["C10"] = UType.GetDateTxt(rowD["ContainerReturnDate"].ToString());
                                rowR["C10"] = UType.GetEndDate(sDate, GetEndDayPlusOne(InputDataSet, "0", rowD["sizentype"].ToString()));
                                DetentionDays = DetentionDays - UType.MyCtoD(GetEndDayPlusOne(InputDataSet, "0", rowD["sizentype"].ToString()));
                                rowR["C33"] = Convert.ToString(UType.MyCtoD(rowR["C11"].ToString()) * UType.MyCtoD(rowR["C12"].ToString()));
                                try
                                {
                                    rowR["C76"] = Convert.ToString(UType.MyCtoD(rowD["exrate"].ToString()) * UType.MyCtoD(rowR["C33"].ToString()));
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }

                    SNo = SNo + 1;

                }

            }
            decimal DetCtr = 0; decimal DetRemainingDays = 0;
            foreach (DataRow rowE in InputDataSet.Tables["TblEq"].Rows)
            {
                DetCtr = 0; DetRemainingDays = 0;
                DetentionDays = UType.MyCtoD(rowE["logDays"].ToString()) - UType.MyCtoD(rowE["Detention"].ToString());
                DetentionDays = DetentionDays - UType.MyCtoD(GetEndDayPlusOne(InputDataSet, "0", rowE["sizentype"].ToString()));
                sDate = rowE["ContainerArrvalDate"].ToString();
                sDate = UType.GetEndDate1(rowE["ContainerArrvalDate"].ToString(), GetEndDayPlusOne(InputDataSet, DetCtr.ToString(), rowE["sizentype"].ToString()));

                DetCtr = 1;
                while (DetentionDays > 0)
                {
                    DataRow row = rptDataSet.Tables[0].NewRow();
                    row["C3"] = SNo.ToString();
                    row["C4"] = "Detention";
                    row["C7"] = UType.GetDateTxt(sDate);
                    row["C5"] = rowE["ContainerNo"].ToString();
                    row["C6"] = rowE["Detention"].ToString();
                    DetRemainingDays = UType.MyCtoD(GetEndDayPlusOne(InputDataSet, DetCtr.ToString(), rowE["sizentype"].ToString()));
                    if (DetentionDays < DetRemainingDays)
                    {
                        DetRemainingDays = DetentionDays;
                    }
                    row["C10"] = UType.GetEndDate(sDate, DetRemainingDays.ToString());

                    row["C11"] = DetRemainingDays.ToString();
                    row["C12"] = GetRate(InputDataSet, DetCtr.ToString(), rowE["sizentype"].ToString());
                    row["CLogo"] = GetImage();
                    row["C33"] = Convert.ToString(UType.MyCtoD(row["C11"].ToString()) * UType.MyCtoD(row["C12"].ToString()));
                    try
                    {
                        row["C76"] = Convert.ToString(UType.MyCtoD(rowE["exrate"].ToString()) * UType.MyCtoD(row["C33"].ToString()));
                    }
                    catch (Exception ex)
                    {

                    }
                    DetentionDays = DetentionDays - UType.MyCtoD(GetEndDayPlusOne(InputDataSet, DetCtr.ToString(), rowE["sizentype"].ToString()));
                    rptDataSet.Tables[0].Rows.Add(row);
                    //sDate = UType.GetEndDate1(rowE["ContainerArrvalDate"].ToString(), GetEndDate(InputDataSet, DetCtr.ToString()));
                    sDate = UType.GetEndDate1(sDate, GetEndDayPlusOne(InputDataSet, DetCtr.ToString(), rowE["sizentype"].ToString()));
                    SNo = SNo + 1; DetCtr = DetCtr + 1;

                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        try
        {
            decimal Total33 = 0; decimal DetentionFC = 0; decimal DetentionPKR = 0; decimal TotalOther = 0;
            foreach (DataRow row123 in rptDataSet.Tables[0].Rows)
            {
                if (row123["C4"].ToString() != "Detention")
                {
                    row123["C7"] = "";
                    //row123["C10"] = "";
                    //row123["C33"] = row123["C10"];
                    Total33 = Total33 + UType.MyCtoD(row123["C33"].ToString());
                    TotalOther = TotalOther + UType.MyCtoD(row123["C33"].ToString());
                }
                if (row123["C4"].ToString() == "Detention")
                {
                    DetentionFC = DetentionFC + UType.MyCtoD(row123["C33"].ToString());
                    DetentionPKR = DetentionPKR + UType.MyCtoD(row123["C76"].ToString());
                    Total33 = Total33 + UType.MyCtoD(row123["C76"].ToString());
                }

            }

            foreach (DataRow row in rptDataSet.Tables[0].Rows)
            {
                row["C34"] = UType.MyFormat(DetentionFC.ToString());
                row["C38"] = UType.MyFormat(TotalOther.ToString());

                row["C33"] = UType.MyFormat(row["C33"].ToString());

                row["C39"] = UType.MyFormat(Total33.ToString());
                row["C51"] = UType.MyFormat(Total33.ToString());
                row["C53"] = "PKR " + UType.NumberToWords(UType.MyCtoD(Total33.ToString())) + " Only";
                row["C34"] = UType.MyFormat(DetentionFC.ToString());
                row["C36"] = UType.MyFormat(DetentionPKR.ToString());
            }

        }
        catch (Exception ex)
        {


        }
        return rptDataSet;

    }

    public DataSet MoveInRptDsBalance(DataSet InputDataSet, string sDate, string eDate, string IsZero)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal OpeningBal = 0; string OpeningSts = "D";
        decimal ActivityBal = 0; string ActivitySts = "D";
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = UType.GetDate1(sDate);
                row["C2"] = UType.GetDate1(eDate);
                row["C3"] = row1["OfficeId"].ToString();
                row["C4"] = row1["ProjectId"].ToString();
                row["C5"] = row1["ParentCode"].ToString();
                row["C9"] = row1["ParentDesc"].ToString();
                row["C6"] = row1["ParentCode1"].ToString();
                row["C10"] = row1["ParentDesc1"].ToString();
                row["C7"] = row1["ParentCode2"].ToString();
                row["C11"] = row1["ParentDesc2"].ToString();
                if (row1["ActCode"].ToString().Length > 6)
                {
                    row["C8"] = row1["ActCode"].ToString();
                    row["C12"] = row1["ActDesc"].ToString();
                    //new
                    row["C15"] = row1["OpeningDr"].ToString() + " " + row1["OpeningDrStatus"].ToString();
                    OpeningBal = UType.MyCtoD(row1["OpeningDr"].ToString());
                    OpeningSts = row1["OpeningDrStatus"].ToString();
                    if (UType.MyCtoD(row1["OpeningCr"].ToString()) > UType.MyCtoD(row1["OpeningDr"].ToString()))
                    {
                        row["C15"] = row1["OpeningCr"].ToString() + " " + row1["OpeningCrStatus"].ToString(); ;
                        OpeningBal = UType.MyCtoD(row1["OpeningCr"].ToString());
                        OpeningSts = row1["OpeningCrStatus"].ToString();
                    }
                    if (OpeningSts == row1["OpeningDrStatus"].ToString())
                    {
                        ActivityBal = UType.MyCtoD(row1["OpeningDr"].ToString()) + UType.MyCtoD(row1["ActivityDr"].ToString());
                        ActivitySts = "D";
                        row["C16"] = Convert.ToString(ActivityBal) + " D";
                        row["C18"] = Convert.ToString(ActivityBal);
                        row["C19"] = ActivitySts;
                    }
                    if (OpeningSts == row1["OpeningCrStatus"].ToString())
                    {
                        ActivityBal = UType.MyCtoD(row1["OpeningCr"].ToString()) + UType.MyCtoD(row1["ActivityCr"].ToString());
                        ActivitySts = "C";
                        row["C17"] = Convert.ToString(ActivityBal) + " C";
                        row["C20"] = Convert.ToString(ActivityBal);
                        row["C21"] = ActivitySts;
                    }
                }

                //new
                if (IsZero == "1")
                {
                    bool isWrite = false;
                    if (row1["ActCode"].ToString().Length < 8)
                    {
                        isWrite = true;
                    }
                    if (row1["ActCode"].ToString().Length >= 8)
                    {
                        if (ActivityBal > 0)
                        {
                            isWrite = true;
                        }

                    }
                    if (isWrite)
                    {
                        row["CLogo"] = GetImage();
                        rptDataSet.Tables[0].Rows.Add(row);
                    }
                }

                if (IsZero == "0")
                {
                    row["CLogo"] = GetImage();
                    rptDataSet.Tables[0].Rows.Add(row);
                }

            }

        }
        catch (Exception ex)
        {
            string err = ex.Message.ToString();
        }


        return rptDataSet;
    }

    public DataSet MoveInRptDsIncomeOld(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal TotalDrBalOpen = 0;
        decimal TotalCrBalOpen = 0;

        decimal OpenBal = 0;
        string OpenSts = "D";
        decimal TotalOpenDr = 0;
        decimal TotalOpenCr = 0;


        decimal TotalDrBal = 0;
        decimal TotalCrBal = 0;
        decimal TotalActivityDr = 0;
        decimal TotalActivityCr = 0;

        decimal Bal = 0;
        string Sts = "D";

        decimal CloseBal = 0;
        string CloseSts = "D";
        decimal TotalCloseDr = 0;
        decimal TotalCloseCr = 0;
        decimal CurActCode = 0;


        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                string LsIncomeStatment = row1["LSISTAT"].ToString();
                string RsIncomeStatment = row1["RSISTAT"].ToString();
                int LenLsIncomeStatment = row1["LSISTAT"].ToString().Length;
                string ISActCode = row1["ActCode"].ToString().Substring(0, LenLsIncomeStatment);
                if (ISActCode == LsIncomeStatment || ISActCode == RsIncomeStatment)
                {
                    row["C1"] = "ABC";

                    row["C9"] = UType.GetDate1(sDate);
                    row["C10"] = UType.GetDate1(eDate);
                    row["C11"] = row1["ActCode"].ToString();
                    row["C12"] = row1["ActDesc"].ToString();
                    CurActCode = UType.MyCtoD(row1["ActCode"].ToString());
                    //Get Opening Balance
                    TotalDrBalOpen = GetDrTotal(InputDataSet.Tables["tOpen"], CurActCode);
                    TotalCrBalOpen = GetCrTotal(InputDataSet.Tables["tOpen"], CurActCode);
                    OpenBal = DrCrAmount(TotalDrBalOpen, TotalCrBalOpen);
                    OpenSts = DrCrStatus(TotalDrBalOpen, TotalCrBalOpen);
                    if (OpenSts == "D")
                    {
                        row["C13"] = OpenBal.ToString();
                    }
                    if (OpenSts == "C")
                    {
                        row["C14"] = OpenBal.ToString();
                    }
                    if (CurActCode == 30010101)
                    {
                        //string aaa = "";
                    }
                    TotalDrBal = GetDrTotal(InputDataSet.Tables["tActivity"], CurActCode);
                    TotalCrBal = GetCrTotal(InputDataSet.Tables["tActivity"], CurActCode);
                    Bal = DrCrAmount(TotalDrBal, TotalCrBal);
                    Sts = DrCrStatus(TotalDrBal, TotalCrBal);
                    if (Sts == "D")
                    {
                        row["C15"] = Bal.ToString();
                    }
                    if (Sts == "C")
                    {
                        row["C16"] = Bal.ToString();
                    }

                    //Getting Closing Balance

                    if (OpenSts == Sts)
                    {
                        CloseBal = OpenBal + Bal;
                        CloseSts = Sts;
                    }
                    if (OpenSts != Sts)
                    {
                        if (OpenBal > Bal)
                        {
                            CloseBal = OpenBal - Bal;
                            CloseSts = OpenSts;
                        }
                        if (Bal > OpenBal)
                        {
                            CloseBal = Bal - OpenBal;
                            CloseSts = Sts;
                        }
                        if (CloseBal == 0)
                        {
                            //if (CurActCode.ToString().Trim().Substring(0, 2) == "10")
                            //{
                            //    CloseSts = "D";
                            // }
                            // if (CurActCode.ToString().Trim().Substring(0, 2) == "20")
                            // {
                            //    CloseSts = "C";
                            // }
                            if (CurActCode.ToString().Trim().Substring(0, 2) == LsIncomeStatment) //"30")
                            {
                                CloseSts = "D";
                            }
                            if (CurActCode.ToString().Trim().Substring(0, 2) == RsIncomeStatment) //"40")
                            {
                                CloseSts = "C";
                            }
                        }
                    }

                    if (CloseSts == "D")
                    {
                        row["C19"] = OpenBal.ToString();
                    }
                    if (CloseSts == "C")
                    {
                        row["C20"] = OpenBal.ToString();
                    }
                    row["CLogo"] = GetImage();

                    if (IsAdd(row["C11"].ToString(), UType.MyCtoD(row["C13"].ToString()), UType.MyCtoD(row["C14"].ToString()), UType.MyCtoD(row["C15"].ToString()), UType.MyCtoD(row["C16"].ToString())))
                    {
                        rptDataSet.Tables[0].Rows.Add(row);

                    }
                }
            } //input recordset
            //Getting Total
            decimal SumDrOpen = 0;
            decimal SumCrOpen = 0;
            decimal SumDrActivity = 0;
            decimal SumCrActivity = 0;
            decimal SumDrClose = 0;
            decimal SumCrClose = 0;
            foreach (DataRow row11 in rptDataSet.Tables[0].Rows)
            {
                SumDrOpen = SumDrOpen + UType.MyCtoD(row11["C13"].ToString());
                SumCrOpen = SumCrOpen + UType.MyCtoD(row11["C14"].ToString());
                SumDrActivity = SumDrActivity + UType.MyCtoD(row11["C15"].ToString());
                SumCrActivity = SumCrActivity + UType.MyCtoD(row11["C16"].ToString());
                SumDrClose = SumDrClose + UType.MyCtoD(row11["C19"].ToString());
                SumCrClose = SumCrClose + UType.MyCtoD(row11["C20"].ToString());
            }
            //Setting in table
            foreach (DataRow row11 in rptDataSet.Tables[0].Rows)
            {
                row11["C22"] = UType.MyFormat(SumDrOpen.ToString());
                row11["C23"] = UType.MyFormat(SumCrOpen.ToString());
                row11["C24"] = UType.MyFormat(SumDrActivity.ToString());
                row11["C25"] = UType.MyFormat(SumCrActivity.ToString());
                row11["C26"] = UType.MyFormat(SumDrClose.ToString());
                row11["C27"] = UType.MyFormat(SumCrClose.ToString());
            }

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        //foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
        //{
        //    TotalOpenDr += UType.MyCtoD(row1["C13"].ToString());
        //    TotalOpenCr += UType.MyCtoD(row1["C14"].ToString());

        //    TotalActivityDr += UType.MyCtoD(row1["C15"].ToString());
        //    TotalActivityCr += UType.MyCtoD(row1["C16"].ToString());

        //    TotalCloseDr += UType.MyCtoD(row1["C19"].ToString());
        //    TotalCloseCr += UType.MyCtoD(row1["C20"].ToString());
        //}
        //foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
        //{
        //    row1["C22"] = UType.MyFormat(TotalOpenDr.ToString());
        //    row1["C23"] = UType.MyFormat(TotalOpenCr.ToString());

        //    row1["C24"] = UType.MyFormat(TotalActivityDr.ToString());
        //    row1["C25"] = UType.MyFormat(TotalActivityCr.ToString());

        //    row1["C24"] = UType.MyFormat(TotalCloseDr.ToString());
        //    row1["C25"] = UType.MyFormat(TotalCloseCr.ToString());
        //}

        return rptDataSet;
    }

    public DataSet MoveInRptDsIncome22102021Old(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal TotalDrBalOpen = 0;
        decimal TotalCrBalOpen = 0;

        decimal OpenBal = 0;
        string OpenSts = "D";
        decimal TotalOpenDr = 0;
        decimal TotalOpenCr = 0;


        decimal TotalDrBal = 0;
        decimal TotalCrBal = 0;
        decimal TotalActivityDr = 0;
        decimal TotalActivityCr = 0;

        decimal Bal = 0;
        string Sts = "D";

        decimal CloseBal = 0;
        string CloseSts = "D";
        decimal TotalCloseDr = 0;
        decimal TotalCloseCr = 0;
        decimal CurActCode = 0;


        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                string LsIncomeStatment = row1["LSISTAT"].ToString();
                string RsIncomeStatment = row1["RSISTAT"].ToString();
                int LenLsIncomeStatment = row1["LSISTAT"].ToString().Length;
                string ISActCode = row1["ActCode"].ToString().Substring(0, LenLsIncomeStatment);
                if (ISActCode == LsIncomeStatment || ISActCode == RsIncomeStatment)
                {
                    row["C1"] = "ABC";

                    row["C9"] = UType.GetDate1(sDate);
                    row["C10"] = UType.GetDate1(eDate);
                    row["C11"] = row1["ActCode"].ToString();
                    row["C12"] = row1["ActDesc"].ToString();
                    CurActCode = UType.MyCtoD(row1["ActCode"].ToString());
                    //Get Opening Balance
                    TotalDrBalOpen = GetDrTotal(InputDataSet.Tables["tOpen"], CurActCode);
                    TotalCrBalOpen = GetCrTotal(InputDataSet.Tables["tOpen"], CurActCode);
                    OpenBal = DrCrAmount(TotalDrBalOpen, TotalCrBalOpen);
                    OpenSts = DrCrStatus(TotalDrBalOpen, TotalCrBalOpen);
                    if (OpenSts == "D")
                    {
                        row["C13"] = OpenBal.ToString();
                    }
                    if (OpenSts == "C")
                    {
                        row["C14"] = OpenBal.ToString();
                    }
                    if (CurActCode == 30010101)
                    {
                        //string aaa = "";
                    }
                    TotalDrBal = GetDrTotal(InputDataSet.Tables["tActivity"], CurActCode);
                    TotalCrBal = GetCrTotal(InputDataSet.Tables["tActivity"], CurActCode);
                    Bal = DrCrAmount(TotalDrBal, TotalCrBal);
                    Sts = DrCrStatus(TotalDrBal, TotalCrBal);
                    if (Sts == "D")
                    {
                        row["C15"] = Bal.ToString();
                    }
                    if (Sts == "C")
                    {
                        row["C16"] = Bal.ToString();
                    }

                    //Getting Closing Balance

                    if (OpenSts == Sts)
                    {
                        CloseBal = OpenBal + Bal;
                        CloseSts = Sts;
                    }
                    if (OpenSts != Sts)
                    {
                        if (OpenBal > Bal)
                        {
                            CloseBal = OpenBal - Bal;
                            CloseSts = OpenSts;
                        }
                        if (Bal > OpenBal)
                        {
                            CloseBal = Bal - OpenBal;
                            CloseSts = Sts;
                        }
                        if (CloseBal == 0)
                        {
                            //if (CurActCode.ToString().Trim().Substring(0, 2) == "10")
                            //{
                            //    CloseSts = "D";
                            // }
                            // if (CurActCode.ToString().Trim().Substring(0, 2) == "20")
                            // {
                            //    CloseSts = "C";
                            // }
                            if (CurActCode.ToString().Trim().Substring(0, 2) == LsIncomeStatment) //"30")
                            {
                                CloseSts = "D";
                            }
                            if (CurActCode.ToString().Trim().Substring(0, 2) == RsIncomeStatment) //"40")
                            {
                                CloseSts = "C";
                            }
                        }
                    }

                    if (CloseSts == "D")
                    {
                        row["C19"] = OpenBal.ToString();
                    }
                    if (CloseSts == "C")
                    {
                        row["C20"] = OpenBal.ToString();
                    }
                    row["CLogo"] = GetImage();

                    if (IsAdd(row["C11"].ToString(), UType.MyCtoD(row["C13"].ToString()), UType.MyCtoD(row["C14"].ToString()), UType.MyCtoD(row["C15"].ToString()), UType.MyCtoD(row["C16"].ToString())))
                    {
                        rptDataSet.Tables[0].Rows.Add(row);

                    }
                }
            } //input recordset
            //Getting Total
            decimal SumDrOpen = 0;
            decimal SumCrOpen = 0;
            decimal SumDrActivity = 0;
            decimal SumCrActivity = 0;
            decimal SumDrClose = 0;
            decimal SumCrClose = 0;
            foreach (DataRow row11 in rptDataSet.Tables[0].Rows)
            {
                SumDrOpen = SumDrOpen + UType.MyCtoD(row11["C13"].ToString());
                SumCrOpen = SumCrOpen + UType.MyCtoD(row11["C14"].ToString());
                SumDrActivity = SumDrActivity + UType.MyCtoD(row11["C15"].ToString());
                SumCrActivity = SumCrActivity + UType.MyCtoD(row11["C16"].ToString());
                SumDrClose = SumDrClose + UType.MyCtoD(row11["C19"].ToString());
                SumCrClose = SumCrClose + UType.MyCtoD(row11["C20"].ToString());
            }
            //Setting in table
            foreach (DataRow row11 in rptDataSet.Tables[0].Rows)
            {
                row11["C22"] = UType.MyFormat(SumDrOpen.ToString());
                row11["C23"] = UType.MyFormat(SumCrOpen.ToString());
                row11["C24"] = UType.MyFormat(SumDrActivity.ToString());
                row11["C25"] = UType.MyFormat(SumCrActivity.ToString());
                row11["C26"] = UType.MyFormat(SumDrClose.ToString());
                row11["C27"] = UType.MyFormat(SumCrClose.ToString());
            }

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        //foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
        //{
        //    TotalOpenDr += UType.MyCtoD(row1["C13"].ToString());
        //    TotalOpenCr += UType.MyCtoD(row1["C14"].ToString());

        //    TotalActivityDr += UType.MyCtoD(row1["C15"].ToString());
        //    TotalActivityCr += UType.MyCtoD(row1["C16"].ToString());

        //    TotalCloseDr += UType.MyCtoD(row1["C19"].ToString());
        //    TotalCloseCr += UType.MyCtoD(row1["C20"].ToString());
        //}
        //foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
        //{
        //    row1["C22"] = UType.MyFormat(TotalOpenDr.ToString());
        //    row1["C23"] = UType.MyFormat(TotalOpenCr.ToString());

        //    row1["C24"] = UType.MyFormat(TotalActivityDr.ToString());
        //    row1["C25"] = UType.MyFormat(TotalActivityCr.ToString());

        //    row1["C24"] = UType.MyFormat(TotalCloseDr.ToString());
        //    row1["C25"] = UType.MyFormat(TotalCloseCr.ToString());
        //}

        return rptDataSet;
    }

    public DataSet MoveInRptDsBalance(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal OpeningBal = 0; string OpeningSts = "D";
        decimal ActivityBal = 0; string ActivitySts = "D";
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                string LsBSHEET = row1["LSBSHEET"].ToString();
                string RsBSHEET = row1["RSBSHEET"].ToString();
                int LenLsBSHEET = row1["LSBSHEET"].ToString().Length;
                string ISActCode = row1["ActCode"].ToString().Substring(0, LenLsBSHEET);
                if (ISActCode == LsBSHEET || ISActCode == RsBSHEET)
                {

                    DataRow row = rptDataSet.Tables[0].NewRow();
                    row["C1"] = UType.GetDate1(sDate);
                    row["C2"] = UType.GetDate1(eDate);
                    row["C3"] = row1["OfficeId"].ToString();
                    row["C4"] = row1["ProjectId"].ToString();
                    if (row1["ParentCode"].ToString().Length == 1)
                    {
                        row["C5"] = row1["ParentCode"].ToString();
                        row["C9"] = row1["ParentDesc"].ToString();
                    }

                    if (row1["ParentCode1"].ToString().Length == 3)
                    {
                        row["C6"] = row1["ParentCode1"].ToString();
                        row["C10"] = row1["ParentDesc1"].ToString();
                    }

                    if (row1["ParentCode2"].ToString().Length == 6)
                    {
                        row["C7"] = row1["ParentCode2"].ToString();
                        row["C11"] = row1["ParentDesc2"].ToString();
                    }

                    if (row1["ActCode"].ToString().Length > 6)
                    {
                        row["C8"] = row1["ActCode"].ToString();
                        row["C12"] = row1["ActDesc"].ToString();
                    }
                    row["CLogo"] = GetImage();
                    if (row["C12"].ToString().Length > 0)
                    {
                        rptDataSet.Tables[0].Rows.Add(row);
                    }

                }
            }

            foreach (DataRow row in rptDataSet.Tables[0].Rows)
            {
                if (row["C8"].ToString().Length > 6)
                {
                    try
                    {
                        #region Opening
                        DataSet Opening = null;
                        MyMain oMy = new MyMain();
                        oMy = new MyMain();
                        oMy.Fld1 = row["C3"].ToString();
                        oMy.Fld2 = row["C4"].ToString();
                        oMy.Fld3 = row["C8"].ToString();
                        oMy.Fld4 = UType.SetDate(row["C1"].ToString());
                        oMy.Fld5 = UType.SetDate(row["C2"].ToString());
                        Opening = oMy.GetOpeningBalance();
                        if (Opening != null)
                        {
                            if (Opening.Tables[0].Rows[0]["OpeningSts"].ToString() == "D")
                            {
                                OpeningSts = "D";
                                OpeningBal = UType.MyCtoD(Opening.Tables[0].Rows[0]["OpeningBal"].ToString());
                                if (Opening.Tables[0].Rows.Count > 1)
                                {
                                    OpeningBal = OpeningBal - UType.MyCtoD(Opening.Tables[0].Rows[1]["OpeningBal"].ToString());
                                }
                            }
                            if (Opening.Tables[0].Rows[0]["OpeningSts"].ToString() == "C")
                            {
                                OpeningSts = "C";
                                OpeningBal = UType.MyCtoD(Opening.Tables[0].Rows[0]["OpeningBal"].ToString());
                                if (Opening.Tables[0].Rows.Count > 1)
                                {
                                    OpeningBal = OpeningBal - UType.MyCtoD(Opening.Tables[0].Rows[1]["OpeningBal"].ToString());
                                }
                            }
                        }
                        #endregion

                        row["C15"] = OpeningBal.ToString() + " " + OpeningSts;

                        #region Activity
                        DataSet Activity = null;
                        oMy = new MyMain();
                        oMy.Fld1 = row["C3"].ToString();
                        oMy.Fld2 = row["C4"].ToString();
                        oMy.Fld3 = row["C8"].ToString();
                        oMy.Fld4 = UType.SetDate(row["C1"].ToString());
                        oMy.Fld5 = UType.SetDate(row["C2"].ToString());
                        Activity = oMy.GetActivity();
                        if (Activity != null)
                        {
                            if (Activity.Tables[0].Rows[0]["ActivitySts"].ToString() == "D")
                            {
                                ActivitySts = "D";
                                ActivityBal = UType.MyCtoD(Activity.Tables[0].Rows[0]["ActivityBal"].ToString());
                                if (Activity.Tables[0].Rows.Count > 1)
                                {
                                    ActivityBal = ActivityBal - UType.MyCtoD(Opening.Tables[0].Rows[1]["ActivityBal"].ToString());
                                }
                            }
                            if (Opening.Tables[0].Rows[0]["ActivitySts"].ToString() == "C")
                            {
                                ActivitySts = "C";
                                ActivityBal = UType.MyCtoD(Opening.Tables[0].Rows[0]["ActivityBal"].ToString());
                                if (Opening.Tables[0].Rows.Count > 1)
                                {
                                    ActivityBal = ActivityBal - UType.MyCtoD(Opening.Tables[0].Rows[1]["ActivityBal"].ToString());
                                }
                            }
                        }
                        #endregion

                        if (OpeningSts == "D")
                        {
                            if (ActivitySts == "D")
                            {
                                ActivityBal = ActivityBal + OpeningBal;
                            }
                            if (ActivitySts == "C")
                            {
                                ActivityBal = ActivityBal - OpeningBal;
                                if (ActivityBal < 0)
                                {
                                    ActivitySts = "D";
                                }
                            }
                        }
                        if (OpeningSts == "C")
                        {
                            if (ActivitySts == "C")
                            {
                                ActivityBal = ActivityBal + OpeningBal;
                            }
                            if (ActivitySts == "D")
                            {
                                ActivityBal = ActivityBal - OpeningBal;
                                if (ActivityBal < 0)
                                {
                                    ActivitySts = "C";
                                }
                            }
                        }
                        if (ActivitySts == "D")
                        {
                            row["C16"] = ActivityBal.ToString();
                        }
                        if (ActivitySts == "C")
                        {
                            row["C17"] = ActivityBal.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }


            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }


        return rptDataSet;
    }
    public DataSet MoveInRptDsBalanceOld(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal TotalDrBalOpen = 0;
        decimal TotalCrBalOpen = 0;

        decimal OpenBal = 0;
        string OpenSts = "D";
        decimal TotalOpenDr = 0;
        decimal TotalOpenCr = 0;


        decimal TotalDrBal = 0;
        decimal TotalCrBal = 0;
        decimal TotalActivityDr = 0;
        decimal TotalActivityCr = 0;

        decimal Bal = 0;
        string Sts = "D";

        decimal CloseBal = 0;
        string CloseSts = "D";
        decimal TotalCloseDr = 0;
        decimal TotalCloseCr = 0;
        decimal CurActCode = 0;


        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                string LsBSHEET = row1["LSBSHEET"].ToString();
                string RsBSHEET = row1["RSBSHEET"].ToString();
                int LenLsBSHEET = row1["LSBSHEET"].ToString().Length;
                string ISActCode = row1["ActCode"].ToString().Substring(0, LenLsBSHEET);
                if (ISActCode == LsBSHEET || ISActCode == RsBSHEET)
                {
                    DataRow row = rptDataSet.Tables[0].NewRow();
                    row["C1"] = "ABC";

                    row["C9"] = UType.GetDate1(sDate);
                    row["C10"] = UType.GetDate1(eDate);
                    row["C11"] = row1["ActCode"].ToString();
                    row["C12"] = row1["ActDesc"].ToString();
                    CurActCode = UType.MyCtoD(row1["ActCode"].ToString());
                    //Get Opening Balance
                    TotalDrBalOpen = GetDrTotal(InputDataSet.Tables["tOpen"], CurActCode);
                    TotalCrBalOpen = GetCrTotal(InputDataSet.Tables["tOpen"], CurActCode);
                    OpenBal = DrCrAmount(TotalDrBalOpen, TotalCrBalOpen);
                    OpenSts = DrCrStatus(TotalDrBalOpen, TotalCrBalOpen);
                    if (OpenSts == "D")
                    {
                        row["C13"] = OpenBal.ToString();
                    }
                    if (OpenSts == "C")
                    {
                        row["C14"] = OpenBal.ToString();
                    }
                    if (CurActCode == 30010101)
                    {
                        //string aaa = "";
                    }
                    TotalDrBal = GetDrTotal(InputDataSet.Tables["tActivity"], CurActCode);
                    TotalCrBal = GetCrTotal(InputDataSet.Tables["tActivity"], CurActCode);
                    Bal = DrCrAmount(TotalDrBal, TotalCrBal);
                    Sts = DrCrStatus(TotalDrBal, TotalCrBal);
                    if (Sts == "D")
                    {
                        row["C15"] = Bal.ToString();
                    }
                    if (Sts == "C")
                    {
                        row["C16"] = Bal.ToString();
                    }

                    //Getting Closing Balance

                    if (OpenSts == Sts)
                    {
                        CloseBal = OpenBal + Bal;
                        CloseSts = Sts;
                    }
                    if (OpenSts != Sts)
                    {
                        if (OpenBal > Bal)
                        {
                            CloseBal = OpenBal - Bal;
                            CloseSts = OpenSts;
                        }
                        if (Bal > OpenBal)
                        {
                            CloseBal = Bal - OpenBal;
                            CloseSts = Sts;
                        }
                        if (CloseBal == 0)
                        {
                            if (CurActCode.ToString().Trim().Substring(0, 2) == "10")
                            {
                                CloseSts = "D";
                            }
                            if (CurActCode.ToString().Trim().Substring(0, 2) == "20")
                            {
                                CloseSts = "C";
                            }
                            //if (CurActCode.ToString().Trim().Substring(0, 2) == "30")
                            //{
                            //    CloseSts = "D";
                            //}
                            //if (CurActCode.ToString().Trim().Substring(0, 2) == "40")
                            //{
                            //    CloseSts = "C";
                            //}
                        }
                    }

                    if (CloseSts == "D")
                    {
                        row["C19"] = OpenBal.ToString();
                    }
                    if (CloseSts == "C")
                    {
                        row["C20"] = OpenBal.ToString();
                    }
                    row["CLogo"] = GetImage();

                    if (IsAdd(row["C11"].ToString(), UType.MyCtoD(row["C13"].ToString()), UType.MyCtoD(row["C14"].ToString()), UType.MyCtoD(row["C15"].ToString()), UType.MyCtoD(row["C16"].ToString())))
                    {
                        rptDataSet.Tables[0].Rows.Add(row);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        try
        {
            foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
            {
                TotalOpenDr += UType.MyCtoD(row1["C13"].ToString());
                TotalOpenCr += UType.MyCtoD(row1["C14"].ToString());

                TotalActivityDr += UType.MyCtoD(row1["C15"].ToString());
                TotalActivityCr += UType.MyCtoD(row1["C16"].ToString());

                TotalCloseDr += UType.MyCtoD(row1["C19"].ToString());
                TotalCloseCr += UType.MyCtoD(row1["C20"].ToString());
            }
        }
        catch (Exception ex)
        {

            string asdf = "aa";
        }
        try
        {
            foreach (DataRow row1 in rptDataSet.Tables[0].Rows)
            {
                row1["C22"] = UType.MyFormat(TotalOpenDr.ToString());
                row1["C23"] = UType.MyFormat(TotalOpenCr.ToString());

                row1["C24"] = UType.MyFormat(TotalActivityDr.ToString());
                row1["C25"] = UType.MyFormat(TotalActivityCr.ToString());

                row1["C26"] = UType.MyFormat(TotalCloseDr.ToString());
                row1["C27"] = UType.MyFormat(TotalCloseCr.ToString());
            }
        }
        catch (Exception ex)
        {

            throw;
        }

        return rptDataSet;
    }

    public DataSet MoveInRptDsItemReport(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal PrvProductID = 0;
        decimal CurProductID = 0;

        decimal OpeningBalanceQty = 0;
        string OpeningBalanceQtySts = "";

        decimal TotalDrBal = 0;
        decimal TotalCrBal = 0;
        decimal BalanceQty = 0;
        string BalanceQtySts = "D";

        try
        {

            PrvProductID = 0; // UType.MyCtoD(InputDataSet.Tables["Detail"].Rows[0]["ActCode1"].ToString());
            foreach (DataRow row1 in InputDataSet.Tables["Detail"].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = "ABC";

                row["C9"] = UType.GetDate1(sDate);
                row["C10"] = UType.GetDate1(eDate);
                row["C11"] = row1["itemid"].ToString();
                row["C12"] = row1["productdescription"].ToString();
                CurProductID = UType.MyCtoD(row1["Productid"].ToString().Trim());
                //Get Opening Balance
                if (PrvProductID != CurProductID)  //UType.MyCtoD(InputDataSet.Tables["Detail"].Rows[0]["ActCode1"].ToString()))
                {
                    TotalDrBal = GetPurchaseOpeningBalance(InputDataSet, UType.MyCtoD(row1["Productid"].ToString()), UType.MyCtoD(sDate));
                    TotalCrBal = GetSaleOpeningBalance(InputDataSet, UType.MyCtoD(row1["Productid"].ToString()), UType.MyCtoD(sDate));
                    if (TotalDrBal >= TotalCrBal)
                    {
                        OpeningBalanceQty = TotalDrBal - TotalCrBal;
                        OpeningBalanceQtySts = "D";
                    }
                    else
                    {
                        OpeningBalanceQty = TotalCrBal - TotalDrBal;
                        OpeningBalanceQtySts = "C";
                    }
                    BalanceQty = OpeningBalanceQty;
                    BalanceQtySts = OpeningBalanceQtySts;
                    PrvProductID = UType.MyCtoD(row1["ProductId"].ToString());
                }
                //End of Opening Balance
                decimal tDate = UType.MyCtoD(row1["trandate"].ToString());
                if (tDate >= UType.MyCtoD(sDate) && tDate <= UType.MyCtoD(eDate))
                {
                    row["C13"] = OpeningBalanceQty.ToString() + " " + OpeningBalanceQtySts;
                    row["C14"] = row1["productdescription"].ToString();
                    row["C15"] = GetName(InputDataSet.Tables["Detail"]);
                    row["C21"] = UType.GetDate1(row1["TranDate"].ToString());
                    row["C16"] = row1["Narration"].ToString();

                    //row["C30"] = row1["Amount"].ToString();

                    decimal pQty = UType.MyCtoD(row1["quantity"].ToString());
                    #region BalanceQtySts D
                    if (BalanceQtySts == "D")
                    {
                        if (row1["vtypeid"].ToString() == "7")   //Purchase voucher type
                        {
                            BalanceQty += pQty;
                            if (pQty > 0)
                            {
                                row["C17"] = row1["quantity"].ToString() + " " + row1["uomdescription"].ToString();  //Amount display in Dr side
                                row["C22"] = "Rs. " + row1["itemamount"].ToString();
                            }
                        }
                        if (row1["vtypeid"].ToString() == "6")   //Sale Voucher Type
                        {
                            if (pQty > 0)
                            {
                                row["C18"] = row1["quantity"].ToString() + " " + row1["uomdescription"].ToString();  //Amount display in Dr side
                                row["C23"] = "Rs. " + row1["itemamount"].ToString();
                            }
                            if (pQty > BalanceQty)
                            {
                                BalanceQtySts = "C";
                                BalanceQty = pQty - BalanceQty;
                            }
                            else
                            {
                                BalanceQty = BalanceQty - pQty;
                            }

                            //BalanceQty = Math.Abs(pQty - BalanceQty);
                        }
                    }
                    #endregion
                    #region BalanceQtySts C
                    if (BalanceQtySts == "C")
                    {
                        if (row1["vtypeid"].ToString() == "6")   // Sale Voucher Type
                        {
                            BalanceQty += pQty;
                            if (pQty > 0)
                            {
                                row["C18"] = row1["quantity"].ToString() + " " + row1["uomdescription"].ToString();  //Amount display in Dr side
                                row["C23"] = "Rs. " + row1["itemamount"].ToString();
                            }
                        }
                        if (row1["vtypeid"].ToString() == "7") //Purchase
                        {
                            if (pQty > 0)
                            {
                                row["C17"] = row1["quantity"].ToString() + " " + row1["uomdescription"].ToString();  //Amount display in Dr side
                                row["C22"] = "Rs. " + row1["itemamount"].ToString();
                            }
                            if (pQty > BalanceQty)
                            {
                                BalanceQtySts = "D";
                                BalanceQty = pQty - BalanceQty;
                            }
                            else
                            {
                                BalanceQty = pQty - BalanceQty;
                            }
                            //BalanceQty = Math.Abs(pQty - BalanceQty);
                        }
                    }
                }
                #endregion
                row["CLogo"] = GetImage();
                row["C19"] = BalanceQty;
                row["C20"] = BalanceQtySts;

                rptDataSet.Tables[0].Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            // throw (ex);
        }
        return rptDataSet;
    }
    private bool IsAdd(string cActCode, decimal cOpenActCodeAmountDr, decimal cOpenActCodeAmountCr, decimal cActCodeAmountDr, decimal cActCodeAmountCr)
    {
        bool retVal = false;
        if (cActCode.Length < 7)
        {
            retVal = true;
        }
        if (cActCode.Length > 6)
        {
            if (cOpenActCodeAmountDr > 0)
            {
                retVal = true;
            }
            if (cOpenActCodeAmountCr > 0)
            {
                retVal = true;
            }

            //
            if (cActCodeAmountDr > 0)
            {
                retVal = true;
            }
            if (cActCodeAmountCr > 0)
            {
                retVal = true;
            }
        }
        return retVal;
    }
    public DataSet MoveInRptDsProofList(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            decimal TotalAmt = 0;
            decimal TotalDr = 0;
            decimal TotalCr = 0;
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                //row["C1"] = "ABC";
                row["C2"] = "Voucher";
                row["C4"] = UType.GetDate(sDate);
                row["C5"] = UType.GetDate(eDate);
                row["C11"] = row1["vtype"].ToString();
                row["C12"] = row1["VNo"];
                row["C13"] = UType.GetDate1(row1["TranDate"].ToString());
                row["C14"] = row1["ActName"];//row1["ChqNo"];
                                             //row["C15"] = UType.GetDate1(row1["ChqDate"].ToString());
                                             //row["C16"] = row1["ActCode"];
                row["C17"] = row1["Narration"]; // row1["ActName"];
                if (row1["AmountSts"].ToString() == "D")
                {
                    row["C15"] = UType.MyFormat(row1["Amount"].ToString());
                    TotalDr += UType.MyCtoD(row1["Amount"].ToString());
                }
                if (row1["AmountSts"].ToString() == "C")
                {
                    row["C16"] = UType.MyFormat(row1["Amount"].ToString());
                    TotalCr += UType.MyCtoD(row1["Amount"].ToString());
                }
                row["C20"] = row1["Narration"];
                //TotalAmt += UType.MyCtoD(row1["Amount"].ToString());
                row["C22"] = TotalDr.ToString();
                row["C23"] = TotalDr.ToString();
                row["C25"] = UType.NumberToWords(TotalDr);

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

    public DataSet MoveInRptDsTrialBalance(DataSet InputDataSet, string sDate, string eDate, string Ref)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal oBalD = 0;
        decimal oBalC = 0;
        decimal oTotalBalD = 0;
        decimal oTotalBalC = 0;
        decimal BalD = 0;
        decimal BalC = 0;
        decimal TotalBalD = 0;
        decimal TotalBalC = 0;
        decimal cBalD = 0;
        decimal cBalC = 0;


        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[2].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = "Trial Balance";
                row["C2"] = "";
                try
                {
                    if (InputDataSet.Tables[3].Rows[0][0].ToString().Length > 0)
                    {
                        row["C2"] = "For " + InputDataSet.Tables[3].Rows[0][0].ToString();
                    }
                }
                catch (Exception ex)
                {

                }
                //row["C3"] = row1["ActDesc"].ToString();
                row["C4"] = UType.GetDate1(sDate);
                row["C5"] = UType.GetDate1(eDate);
                row["C11"] = row1["ActCode"].ToString();
                row["C12"] = row1["ActDesc"].ToString();

                oBalD = 0;
                oBalC = 0;

                if (row1["ACtCode"].ToString() == "10010101")
                {
                    string aaa = BalC.ToString();
                }

                #region Opening Balance
                foreach (DataRow row3 in InputDataSet.Tables[0].Rows)
                {
                    if (row1["ActCode"].ToString() == row3["ActCode"].ToString())
                    {
                        if (row3["Status"].ToString() == "D")
                        {
                            oBalD = oBalD + UType.MyCtoD(row3["Amount"].ToString());
                        }
                        if (row3["Status"].ToString() == "C")
                        {
                            oBalC = oBalC + UType.MyCtoD(row3["Amount"].ToString());
                        }
                    }
                }
                if (oBalD > oBalC)
                {
                    oBalD = oBalD - oBalC;
                    oBalC = 0;
                    row["C13"] = oBalD.ToString();
                    row["C14"] = "0";
                    oTotalBalD += oBalD;
                }
                else
                {
                    oBalC = oBalC - oBalD;
                    oBalD = 0;
                    row["C13"] = "0";
                    row["C14"] = oBalC.ToString();
                    oTotalBalC += oBalC;
                }
                //row["C13"] = oBalD.ToString();
                //row["C14"] = oBalC.ToString();
                row["C15"] = oTotalBalD.ToString();
                row["C16"] = oTotalBalC.ToString();

                #endregion

                BalD = 0;
                BalC = 0;

                #region Activity
                foreach (DataRow row2 in InputDataSet.Tables[1].Rows)
                {
                    if (row1["ActCode"].ToString().Trim() == row2["ActCode"].ToString().Trim())
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

                row["C17"] = BalD.ToString();
                row["C18"] = BalC.ToString();

                if (BalD > BalC)
                {
                    BalD = BalD - BalC;
                    BalC = 0;
                    //row["C17"] = BalD.ToString();
                    //row["C18"] = "0";
                    TotalBalD += BalD;
                }
                else
                {
                    BalC = BalC - BalD;
                    BalD = 0;
                    //row["C17"] = "0";
                    //row["C18"] = BalC.ToString();
                    TotalBalC += BalC;
                }
                row["C19"] = TotalBalD.ToString();
                row["C20"] = TotalBalC.ToString();

                #endregion

                cBalD = oBalD + BalD;
                cBalC = oBalC + BalC;
                if (cBalD > cBalC)
                {
                    cBalD = cBalD - cBalC;
                    row["C21"] = cBalD.ToString();
                    row["C22"] = "0";
                }
                else
                {
                    cBalC = cBalC - cBalD;
                    row["C21"] = "0";
                    row["C22"] = cBalC.ToString();
                }
                row["C23"] = Convert.ToString(oTotalBalD + TotalBalD);
                row["C24"] = Convert.ToString(oTotalBalC + TotalBalC);


                row["CLogo"] = GetImage();
                //row["C18"] = BalanceSts;
                if (row1["ActCode"].ToString().Length == 2 || BalD > 0 || BalC > 0 || oBalD > 0 || oBalC > 0)
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
                row["C2"] = "Income Statement";
                row["C3"] = "";
                try
                {
                    if (InputDataSet.Tables[2].Rows[0][0].ToString().Length > 0)
                    {
                        row["C3"] = "For " + InputDataSet.Tables[2].Rows[0][0].ToString();
                    }
                }
                catch (Exception ex)
                {

                }
                //row["C3"] = "Income Statement";
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
                decimal pNet = 0;
                if (TotalBalD > TotalBalC)
                {
                    pNet = TotalBalD - TotalBalC;
                    row["C23"] = "Net Loss";
                    row["C24"] = pNet;
                }
                if (TotalBalC > TotalBalD)
                {
                    pNet = TotalBalC - TotalBalD;
                    row["C23"] = "Net Profit";
                    row["C24"] = pNet;
                }
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

    public DataSet MoveInRptDsIncStatement(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal TotalBalD = 0;
        decimal TotalBalC = 0;
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C2"] = "Income Statement";
                row["C3"] = "";
                try
                {
                    //if (InputDataSet.Tables[2].Rows[0][0].ToString().Length > 0)
                    //{
                    //    row["C3"] = "For " + InputDataSet.Tables[2].Rows[0][0].ToString();
                    //}
                }
                catch (Exception ex)
                {

                }
                if (row1["FldLevel"].ToString() == "1")
                {
                    row["C11"] = row1["ActDesc"].ToString();
                    row["C16"] = row1["AmountTotal"].ToString();
                }
                if (row1["FldLevel"].ToString() == "2")
                {
                    row["C12"] = row1["ActDesc"].ToString();
                    row["C15"] = row1["AmountTotal"].ToString();
                }
                if (row1["FldLevel"].ToString() == "3")
                {
                    row["C13"] = row1["ActDesc"].ToString();
                    row["C14"] = row1["AmountTotal"].ToString();
                }
                if (row1["FldLevel"].ToString() == "4")
                {
                    row["C17"] = row1["ActDesc"].ToString();
                    row["C18"] = row1["AmountTotal"].ToString();
                }

                //decimal BalD = 0;
                //decimal BalC = 0;

                //foreach (DataRow row2 in InputDataSet.Tables[0].Rows)
                //{
                //    if (row1["ActCode"].ToString() == row2["ActCode"].ToString())
                //    {
                //        if (row2["Status"].ToString() == "D")
                //        {
                //            BalD = BalD + UType.MyCtoD(row2["Amount"].ToString());
                //        }
                //        if (row2["Status"].ToString() == "C")
                //        {
                //            BalC = BalC + UType.MyCtoD(row2["Amount"].ToString());
                //        }
                //    }
                //}
                //if (BalD > BalC)
                //{
                //    BalD = BalD - BalC;
                //    row["C13"] = BalD.ToString();
                //    row["C14"] = "0";
                //    TotalBalD += BalD;
                //}
                //else
                //{
                //    BalC = BalC - BalD;
                //    row["C13"] = "0";
                //    row["C14"] = BalC.ToString();
                //    TotalBalC += BalC;
                //}
                //row["C15"] = TotalBalD.ToString();
                //row["C16"] = TotalBalC.ToString();
                row["CLogo"] = GetImage();
                //row["C18"] = BalanceSts;
                //decimal pNet = 0;
                //if (TotalBalD > TotalBalC)
                //{
                //    pNet = TotalBalD - TotalBalC;
                //    row["C23"] = "Net Loss";
                //    row["C24"] = pNet;
                //}
                //if (TotalBalC > TotalBalD)
                //{
                //    pNet = TotalBalC - TotalBalD;
                //    row["C23"] = "Net Profit";
                //    row["C24"] = pNet;
                //}
                //if (row1["ActCode"].ToString().Length == 2 || BalD > 0 || BalC > 0)
                //{
                //    rptDataSet.Tables[0].Rows.Add(row);
                //}
                rptDataSet.Tables[0].Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet MoveInRptDsBalanceSheet(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal TotalBalD = 0;
        decimal TotalBalC = 0;
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C2"] = "Balance Sheet";
                row["C3"] = "";
                try
                {
                    //if (InputDataSet.Tables[2].Rows[0][0].ToString().Length > 0)
                    //{
                    //    row["C3"] = "For " + InputDataSet.Tables[2].Rows[0][0].ToString();
                    //}
                }
                catch (Exception ex)
                {

                }
                if (row1["FldLevel"].ToString() == "1")
                {
                    row["C11"] = row1["ActDesc"].ToString();
                    row["C16"] = row1["AmountTotal"].ToString();
                }
                if (row1["FldLevel"].ToString() == "2")
                {
                    row["C12"] = row1["ActDesc"].ToString();
                    row["C15"] = row1["AmountTotal"].ToString();
                }
                if (row1["FldLevel"].ToString() == "3")
                {
                    row["C13"] = row1["ActDesc"].ToString();
                    row["C14"] = row1["AmountTotal"].ToString();
                }
                if (row1["FldLevel"].ToString() == "4")
                {
                    row["C17"] = row1["ActDesc"].ToString();
                    row["C18"] = row1["AmountTotal"].ToString();
                }

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

    public DataSet MoveInRptDsPaySlip(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal TotalGrossPay = 0;
        decimal TotalDeduction = 0;
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["CLogo"] = GetImage();
                row["C2"] = "Pay-Slip for the Month of " + UType.GetMonthName(row1["YearMonth"].ToString().Substring(4, 2)) + " " + row1["YearMonth"].ToString().Substring(0, 4);
                row["C3"] = row1["EmpName"].ToString();
                row["C4"] = row1["EmpId"].ToString();
                //row["C5"] = row1[""].ToString();
                row["C6"] = row1["DepartDescription"].ToString();
                row["C7"] = row1["DesignationDescription"].ToString();
                row["C8"] = row1["EmpDateofJoining"].ToString();
                row["C9"] = row1["EmpAccountNo"].ToString();
                row["C10"] = row1["EmpAddress"].ToString();
                if (row1["AllDedSts"].ToString() == "1")
                {
                    row["C11"] = row1["AllDedDescription"].ToString();
                    row["C12"] = row1["AllDedAmount"].ToString();
                    TotalGrossPay += UType.MyCtoD(row1["AllDedAmount"].ToString());
                }
                if (row1["AllDedSts"].ToString() == "2")
                {
                    row["C13"] = row1["AllDedDescription"].ToString();
                    row["C14"] = row1["AllDedAmount"].ToString();
                    TotalDeduction += UType.MyCtoD(row1["AllDedAmount"].ToString());
                }
                row["C15"] = TotalGrossPay.ToString();
                row["C16"] = TotalDeduction.ToString();
                rptDataSet.Tables[0].Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public DataSet MoveInRptDsClrRt(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            if (InputDataSet.Tables[0].Rows.Count > 0)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                DataRow row1 = InputDataSet.Tables[0].Rows[0];
                row["CLogo"] = GetImage();
                row["C2"] = "Bill Statement ";
                row["C3"] = "Bill For";
                row["C4"] = row1["customerName"].ToString();
                row["C5"] = "Exporter Name";
                row["C6"] = row1["ExporterName"].ToString();
                row["C11"] = row1["ImporterName"].ToString();
                row["C12"] = row1["Commodity"].ToString();
                row["C13"] = row1["Vessel"].ToString();
                row["C14"] = row1["Port"].ToString();
                row["C21"] = row1["boeno"].ToString() + " " + row1["boedate"].ToString();
                row["C22"] = row1["fileno"].ToString();
                row["C23"] = row1["sbno"].ToString() + " " + row1["sbdate"].ToString();
                row["C24"] = row1["formeno"].ToString();
                row["C25"] = row1["airlinename"].ToString();
                int ctr = 1;
                foreach (DataRow row2 in InputDataSet.Tables["Cntr"].Rows)
                {
                    if (ctr == 1)
                    {
                        row["C15"] = row2["Cntrnrno"].ToString();
                    }
                    if (ctr == 2)
                    {
                        row["C16"] = row2["Cntrnrno"].ToString();
                    }
                    if (ctr == 3)
                    {
                        row["C17"] = row2["Cntrnrno"].ToString();
                    }
                    if (ctr == 4)
                    {
                        row["C18"] = row2["Cntrnrno"].ToString();
                    }
                    if (ctr == 5)
                    {
                        row["C19"] = row2["Cntrnrno"].ToString();
                    }
                    if (ctr == 6)
                    {
                        row["C20"] = row2["Cntrnrno"].ToString();
                    }
                    ctr++;
                }
                rptDataSet.Tables[0].Rows.Add(row);
                decimal total = 0;
                foreach (DataRow row3 in InputDataSet.Tables["AllExpense"].Rows)
                {
                    row = rptDataSet.Tables[0].NewRow();
                    row["CLogo"] = GetImage();
                    row["C31"] = row3["AllDedId"].ToString();
                    row["C32"] = row3["AllDedDescription"].ToString();
                    row["C33"] = "";
                    foreach (DataRow row33 in InputDataSet.Tables["Expense"].Rows)
                    {
                        if (row3["AllDedId"].ToString() == row33["ExpenseId"].ToString())
                        {
                            row["C33"] = UType.MyFormat(row33["ExpenseIdAmount"].ToString());
                            total = total + UType.MyCtoD(row33["ExpenseIdAmount"].ToString());
                        }
                        row["C34"] = UType.MyFormat(total);
                    }
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

    public DataSet MoveInRptDsClrRt2(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal ctr = 1;
        try
        {
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["CLogo"] = GetImage();
                //row["C2"] = "Bill Statement ";
                //row["C3"] = row1["ImporterName"].ToString();
                row["C11"] = ctr.ToString(); //row1["fileno"].ToString();
                row["C12"] = row1["billno"].ToString();
                row["C13"] = row1["port"].ToString();
                row["C14"] = row1["boeno"].ToString();
                row["C15"] = row1["boedate"].ToString();
                row["C16"] = row1["sbno"].ToString();
                row["C17"] = row1["sbdate"].ToString();
                row["C18"] = row1["formeno"].ToString();
                //row["C25"] = row1["shipcompany"].ToString();           
                foreach (DataRow row2 in InputDataSet.Tables["expense"].Rows)
                {
                    if (UType.MyCtoD(row1["Tranid"].ToString()) == UType.MyCtoD(row2["Tranid"].ToString()))
                    {
                        if (row2["ExpenseId"].ToString().Trim() == "50")
                        {
                            row["C20"] = row2["ExpenseIdAmount"].ToString();
                        }
                        if (row2["ExpenseId"].ToString().Trim() == "51")
                        {
                            row["C21"] = row2["ExpenseIdAmount"].ToString();
                        }
                        if (row2["ExpenseId"].ToString().Trim() == "52")
                        {
                            row["C22"] = row2["ExpenseIdAmount"].ToString();
                        }
                        if (row2["ExpenseId"].ToString().Trim() == "53")
                        {
                            row["C23"] = row2["ExpenseIdAmount"].ToString();
                        }
                        if (row2["ExpenseId"].ToString().Trim() == "54")
                        {
                            row["C24"] = row2["ExpenseIdAmount"].ToString();
                        }
                        if (row2["ExpenseId"].ToString().Trim() == "55")
                        {
                            row["C25"] = row2["ExpenseIdAmount"].ToString();
                        }
                        if (row2["ExpenseId"].ToString().Trim() == "56")
                        {
                            row["C26"] = row2["ExpenseIdAmount"].ToString();
                        }
                        if (row2["ExpenseId"].ToString().Trim() == "57")
                        {
                            row["C27"] = row2["ExpenseIdAmount"].ToString();
                        }
                    }
                }
                rptDataSet.Tables[0].Rows.Add(row);
                ctr++;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }


    private string GetDescription(string pActCode)
    {

        string retVal = string.Empty;
        if (UType.MyCtoD(pActCode) > 0)
        {
            _SqlConnection = Connection.SqlConnection;
            DataSet ds = GetDescriptionbyActCode(pActCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = ds.Tables[0].Rows[0]["ActDesc"].ToString();
            }
        }
        return retVal;
    }

    private string GetOfficeName(string pOfficeId)
    {
        string retVal = string.Empty;
        _SqlConnection = Connection.SqlConnection;
        DataSet ds = GetOfficeNamebyOfficeId(pOfficeId);
        if (ds.Tables[0].Rows.Count > 0)
        {
            retVal = ds.Tables[0].Rows[0]["OfficeDescription"].ToString();
        }
        return retVal;
    }

    public DataSet GetLogin()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetLogin();
        return result;
    }
    public string UpdateLogin()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateLogin();
        return result;
    }


    public DataSet Sp_GetLogin(string pUserId, string pPassword)
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.Sp_GetLogin(pUserId, pPassword);
        //DisposeSQLConnection();
        return result;
    }
    public DataSet Sp_GetMenu()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMenu();
        try
        {
            string Mlevel = result.Tables[0].Rows[0]["menulevel"].ToString();
            if (Mlevel == "1")
            {
                DataRow row = result.Tables[0].NewRow();
                row["menutext"] = "Logout";
                row["menupath"] = "~/Pages/Login.aspx";
                row["MenuLevel"] = "1";
                row["ImagePath"] = "~/Images/Icon/Logout.png";
                row["MenuId"] = "98";

                result.Tables[0].Rows.Add(row);
                row = null;
            }
            if (Mlevel == "2")
            {
                DataRow row = result.Tables[0].NewRow();
                row["menutext"] = "Home";
                row["menupath"] = "~/Pages/Main.aspx";
                row["MenuLevel"] = "2";
                row["ImagePath"] = "~/Images/Icon/Home.png";
                row["MenuId"] = "99";

                result.Tables[0].Rows.Add(row);
                row = null;
            }
        }
        catch (Exception ex)
        {

        }
        //	delete from RfUserMenu where MenuId=98 	delete from RfUserMenu where MenuId=99
        return result;
    }


    public DataSet Sp_GetActCode()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetActCode();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetList(string pActLevel)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetList(pActLevel);
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetListbyActCode(string pActCode)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetListByActCode(pActCode);
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetDescriptionbyActCode(string pActCode)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetDescriptionByActCode(pActCode);
        DisposeSQLConnection();
        return result;
    }

    public DataSet FillComboLevel()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetFillComboLevel();
        DisposeSQLConnection();
        return result;
    }

    public DataSet FillComboProject()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetFillComboProject();

        return result;
    }






    public DataSet FillComboVtype()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetFillComboVtype();

        return result;
    }

    public DataSet GetActChartReference()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetActChartReference();

        return result;
    }

    public DataSet GetActChart()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetActChart();

        return result;
    }
    public DataSet GetActChartEntry()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetActChartEntry();

        return result;
    }
    public DataSet GetActChartCode()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetActChartCode();

        return result;
    }
    public DataSet GetMaxActCode()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxActCode();
        return result;
    }

    public DataSet GetMaxActCodeNew()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxActCodeNew();
        return result;
    }

    //GetMaxActCode

    public DataSet FillComboPayGroup()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetFillComboPayGroup();

        return result;
    }
    public DataSet FillComboEmpStatus()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetFillComboEmpStatus();

        return result;
    }
    public DataSet FillComboUOM()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetFillComboUOM();

        return result;
    }

    public DataSet FillComboDebtor()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.FillComboDebtor();

        return result;
    }
    public DataSet FillComboPurchase()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.FillComboPurchase();

        return result;
    }
    public DataSet FillComboLeave()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetFillComboLeave();

        return result;
    }


    public DataSet GetDesignation(string DepartmentId)
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.Sp_GetFillComboDesignation(DepartmentId);

        return result;
    }

    public DataSet FillComboActLevel()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.Sp_GetFillComboActLevel();

        return result;
    }

    public DataSet FillComboPaymentMode()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.Sp_GetFillComboPaymentMode();

        return result;
    }

    #region Chart
    public DataSet Sp_SelectChart()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_SelectChart();
        DisposeSQLConnection();
        return result;
    }

    public DataSet Sp_PrintChartOfAccount()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_PrintChartOfAccount();
        DisposeSQLConnection();
        return result;

    }
    public DataSet PrintChartOfAccount1()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.PrintChartOfAccount1();
        DisposeSQLConnection();
        return result;

    }

    public string InsertChart()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertChart();
        DisposeSQLConnection();
        return result;

    }

    public string UpdateChart()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateChart();
        DisposeSQLConnection();
        return result;

    }

    public DataSet GetActCodeDesc(string pActCode)
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetActCodeDesc(pActCode);

        return result;
    }
    #endregion

    #region MyRegion TranTbl
    public DataSet SelectTranTbl()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.SelectTranTbl();
        DisposeSQLConnection();
        return result;
    }

    public string InsertTranTbl()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertTranTbl();
        DisposeSQLConnection();
        return result;

    }

    public string InsertTranTblAuto()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertTranTblAuto();
        DisposeSQLConnection();
        return result;

    }
    public string InsertTranTblDtl()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertTranTblDtl();
        DisposeSQLConnection();
        return result;

    }
    public string InsertTranTblLog()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertTranTblLog();
        DisposeSQLConnection();
        return result;

    }
    public string UpdateActTranId()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.UpdateActTranId();
        DisposeSQLConnection();
        return result;

    }
    public string UpdateActTranIdSale()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.UpdateActTranIdSale();
        DisposeSQLConnection();
        return result;

    }

    public string UpdateTranTbl()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateTranTbl();
        DisposeSQLConnection();
        return result;

    }
    public string UpdateTranTblAuto()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateTranTblAuto();
        DisposeSQLConnection();
        return result;

    }
    public string DeleteTranTbl()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteTranTbl();
        DisposeSQLConnection();
        return result;
    }

    public string DeleteTranTblDtl()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.DeleteTranTblDtl();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetMaxVno()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetMaxVno();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetTransectionData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetTransectionData();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetProofListData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetProofListData();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetLedgerData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetLedgerDataOpen();
        DataSet result1 = oMyDb.GetLedgerData();

        addTableinDataSet(result, result1.Tables[0], "Detail");
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetLedgerDataSOA()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetLedgerDataSOA();



        DisposeSQLConnection();
        return result;
    }
    public DataSet GetTrialData()
    {
        MyDb oMyDb = new MyDb(this);

        DataSet result = oMyDb.GetDatasetSpTrial();

        DisposeSQLConnection();
        return result;
    }

    public DataSet GetIncomeData()
    {
        MyDb oMyDb = new MyDb(this);

        DataSet result = oMyDb.GetDatasetSpIncome();

        DisposeSQLConnection();
        return result;
    }

    public DataSet GetBalanceData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDatasetSpIncome();

        DisposeSQLConnection();
        return result;
    }
    public DataSet GetCostingeData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDatasetSpIncome();

        DisposeSQLConnection();
        return result;
    }
    public DataSet GetItemData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetItemDataOpen();
        DataSet result1 = oMyDb.GetItemData();
        addTableinDataSet(result, result1.Tables[0], "Detail");
        DisposeSQLConnection();
        return result;
    }


    public DataSet GetGridvNumber()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetGridvNumber();
        DisposeSQLConnection();
        return result;
    }

    //
    public DataSet GetTransectionDataDtlAttached()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetTransectionDataDtlAttached();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetTransectionDataDtlAttached1()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetTransectionDataDtlAttached1();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetTransectionDataDtlAttachedExp()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetTransectionDataDtlAttachedExp();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetTransectionDataDtl()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetTransectionDataDtl();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetAllItem()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetAllItem();
        DisposeSQLConnection();
        return result;
    }


    #endregion

    #region Reference

    public DataSet SelectRef()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.SelectRef();
        DisposeSQLConnection();
        return result;

    }
    public string GetRefCfy()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.GetRefCfy();
        DisposeSQLConnection();
        return result;

    }
    public string GetRefCfyStart()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.GetRefCfyStart();
        DisposeSQLConnection();
        return result;

    }
    public string GetRefsDatae()
    {
        string retVal = "";
        MyDb oMyDb = new MyDb(this);
        DataSet ds = oMyDb.SelectRef();
        if (ds.Tables[0].Rows.Count > 0)
        { retVal = ds.Tables[0].Rows[0]["sdate"].ToString(); }

        DisposeSQLConnection();
        return retVal;

    }

    public string InsertRef()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertRef();
        DisposeSQLConnection();
        return result;

    }

    public string UpdateRef()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateRef();
        DisposeSQLConnection();
        return result;

    }
    public string InsertRefNew()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertRefNew();
        DisposeSQLConnection();
        return result;

    }
    #endregion

    #region Table Function
    public static DataSet addTableinDataSet(DataSet ds, DataTable dt, string dtName)
    {
        dt.TableName = dtName;
        if (ds.Tables.Contains(dtName))
        {
            ds.Tables.Remove(dtName);
            ds.AcceptChanges();
        }
        ds.Tables.Add(dt.Copy());

        return ds;
    }

    public static DataTable AddCol(DataTable dtName, string Col)
    {
        //DataTable myDataTable = new DataTable();    

        DataColumn myDataColumn;

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = Col;
        dtName.Columns.Add(myDataColumn);

        return dtName;
    }

    #endregion

    public DataSet GetOfficeNamebyOfficeId(string pOfficeId)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetOfficeNamebyOfficeId(pOfficeId);
        DisposeSQLConnection();
        return result;
    }

    public DataSet VoucherPrintSp(string pVtype, string pVno)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.VoucherPrintSp(pVtype, pVno);
        DisposeSQLConnection();
        return result;

    }
    public DataSet VoucherPSp()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.VoucherPSp();
        DisposeSQLConnection();
        return result;
    }
    public DataSet VoucherSSp()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.VoucherSSp();
        DisposeSQLConnection();
        return result;

    }

    public DataSet LedgerPrintSp(string pActCode, string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.LedgerPrintSp(pActCode, pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;

    }
    public DataSet LedgerPrintSp(string pOfficeId, string pActCode, string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.LedgerPrintSp(pOfficeId, pActCode, pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;

    }
    public DataSet LedgerPrintSpAll()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.LedgerPrintSpAll();
        DisposeSQLConnection();
        return result;

    }

    public DataSet ProofListSp(string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.ProofListSp(pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;

    }

    public DataSet TrialBalanceSp(string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.TrialBalanceSp(pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;

    }
    public DataSet TrialBalanceSp(string pOfficeId, string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.TrialBalanceSp(pOfficeId, pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;

    }

    public DataSet IncomeStatementSp(string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.IncomeStatementSp(pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;

    }
    public DataSet IncomeStatementSp(string pOfficeId, string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.IncomeStatementSp(pOfficeId, pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;

    }

    public DataSet IncStatementSp(string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.IncStatementSp(pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;

    }

    public DataSet BalStatementSp(string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.BalStatementSp(pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;

    }
    public DataSet BalStatementSp(string pOfficeId, string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.BalStatementSp(pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;

    }

    public DataSet BalanceSheetSp(string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.BalanceSheetSp(pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;

    }
    public DataSet BalanceSheetSp(string pOfficeId, string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.BalanceSheetSp(pOfficeId, pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;

    }


    public DataSet GetEmpInfo()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetEmpInfo();
        DisposeSQLConnection();
        return result;
    }
    public DataSet Sp_GetEmpInfo()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetEmpInfo();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetEmpInfo1()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetEmpInfo1();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetRefTbl()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GeRefTbl();

        return result;
    }
    public string InsertRfEmployee()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertRfEmployee();
        DisposeSQLConnection();
        return result;

    }
    public string UpdateRfEmployee()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.UpdateRfEmployee();
        DisposeSQLConnection();
        return result;

    }

    public DataSet GetCustomerInfoAll()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCustomerInfoAll();
        return result;
    }


    public DataSet GetCustomerInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCustomerInfo();
        return result;
    }
    public DataSet GetCustomerInfoMax()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCustomerInfoMax();
        return result;
    }
    public DataSet GetCustomerStatusMax()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCustomerStatusMax();
        return result;
    }

    public string InsertRfCustomer()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertRfCustomer();
        return result;

    }
    public string UpdateRfCustomer()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateRfCustomer();
        return result;

    }
    public DataSet GetRefTblInfo(string TblName, string IdName)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetRefTblInfo(TblName, IdName);
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetRfProduct()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetRfProduct();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetAllRefTbl(string TblName, string FldName)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetAllRefTbl(TblName, FldName);
        //DisposeSQLConnection();
        return result;
    }

    public DataSet GetAllDed()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetAllDed();
        //DisposeSQLConnection();
        return result;
    }

    public string UpdateRfProduct()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateRfProduct();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetUnitRate()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetUnitRate();
        //DisposeSQLConnection();
        return result;
    }
    public DataSet GetProductType1()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetProductType();
        //DisposeSQLConnection();
        return result;
    }

    #region CustomerHdr
    public DataSet GetCustomerHdrInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCustomerHdrInfo();

        return result;
    }
    public DataSet GetCustomerHdrInfo(string ProductId)
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCustomerHdrInfo(ProductId);

        return result;
    }

    public DataSet GetCustomerHdrInfoAll()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCustomerHdrInfoAll();

        return result;
    }

    public string InsertCustomerHdr(string ProductID)
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertCustomerHdr(ProductID);

        return result;

    }
    public string UpdateCustomerHdr()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateCustomerHdr();

        return result;

    }

    #endregion




    public string DeleteCustomerTran()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteCustomerTran();
        //DisposeSQLConnection();
        return result;
    }
    public string InsertCustomerTran()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertCustomerTran();

        return result;

    }

    public DataSet CreateRptDataSet(DataSet InputDataSet)
    {
        DataSet rptDataSet = new DataSet();
        rptDataSet.ReadXmlSchema(UType.FileReportXsd);
        rptDataSet = MoveDataInReportDataSet(rptDataSet, InputDataSet);
        return rptDataSet;
    }
    public DataSet CreateRptDataSetInv1(DataSet InputDataSet)
    {
        DataSet rptDataSet = new DataSet();
        rptDataSet.ReadXmlSchema(UType.FileReportXsd);
        rptDataSet = MoveDataInReportDataSetInv1(rptDataSet, InputDataSet);
        return rptDataSet;
    }


    public DataSet CreateRptDataSetInv2(DataSet InputDataSet)
    {
        DataSet rptDataSet = new DataSet();
        rptDataSet.ReadXmlSchema(UType.FileReportXsd);
        rptDataSet = MoveDataInReportDataSetInv2(rptDataSet, InputDataSet);
        return rptDataSet;
    }

    public DataSet CreateRptDataSetSales(DataSet InputDataSet)
    {
        DataSet rptDataSet = new DataSet();
        rptDataSet.ReadXmlSchema(UType.FileReportXsd);
        rptDataSet = MoveDataInReportDataSetInv1(rptDataSet, InputDataSet);
        return rptDataSet;
    }



    public DataSet Sp_GetLeaveTranData()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetLeaveTranData();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetEmpLeaveData()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetEmpLeaveData();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetEmpAllLeaveData()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetEmpAllLeaveData();
        DisposeSQLConnection();
        return result;
    }

    public string InsertLeaveTran()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertLeaveTran();
        DisposeSQLConnection();
        return result;

    }
    public string UpdateLeaveTran()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.UpdateLeaveTran();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetEmpIdByUserId(string pUserId)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetEmpIdByUserId(pUserId);
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetEmpLeaveDataForPrint()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetEmpLeaveDataForPrint();
        DisposeSQLConnection();
        return result;
    }

    public DataSet Sp_GetAllLeaveTranData(string sDate, string eDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetAllLeaveTranData(sDate, eDate);
        DisposeSQLConnection();
        return result;
    }
    public DataSet Sp_GetLeaveTranDataNew(string pUserId, string pTranDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetLeaveTranDataNew(pUserId, pTranDate);
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetAllEmployee()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetAllEmployee();

        return result;
    }
    public DataSet Sp_GetAllCustomerTran()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.Sp_GetAllCustomerTran();

        return result;
    }
    public DataSet Sp_GetCustomerDefault(string pCustomerId)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetCustomerDefault(pCustomerId);
        DisposeSQLConnection();
        return result;
    }


    public DataSet Sp_GetCustomerTran(string StartDate, string EndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetCustomerTran(StartDate, EndDate);
        DisposeSQLConnection();
        return result;
    }

    public DataSet Sp_GetCustomerTranSingle(string pCustomerId, string StartDate, string EndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetCustomerTranSingle(pCustomerId, StartDate, EndDate);
        DisposeSQLConnection();
        return result;
    }

    public DataSet Sp_GetCustomerTranSingle(string pCustomerId, string pSdate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetCustomerSingle(pCustomerId, pSdate);
        DisposeSQLConnection();
        return result;
    }
    public string InsertRefTbl(string TblName)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertRefTbl(TblName);
        DisposeSQLConnection();
        return result;

    }
    public string InsertRfProduct()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertRfProduct();
        DisposeSQLConnection();
        return result;

    }

    public string UpdateRefTbl(string TblName)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.UpdateRefTbl(TblName);
        DisposeSQLConnection();
        return result;
    }

    public DataSet Sp_GetUom(string pUomId)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetUom(pUomId);
        DisposeSQLConnection();
        return result;
    }

    public DataSet Sp_GetCustomerDefaultProduct(string pCustomerId)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetCustomerDefaultProduct(pCustomerId);
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetEmailUser()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GeEmailUser();

        return result;
    }
    public DataSet GetUserInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.Sp_GetUserInfo();
        //DisposeSQLConnection();
        return result;
    }

    public string InsertUserInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertUserInfo();
        DisposeSQLConnection();
        return result;
    }

    public string UpdateUserInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateUserInfo();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetAllUser()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetAllUser();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetAllRole()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetAllRole();
        DisposeSQLConnection();
        return result;

    }

    public DataSet GetRoleInfo1()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetRoleInfo1();
        DisposeSQLConnection();
        return result;
    }
    public string InsertRoleInfo()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertRfRole();
        DisposeSQLConnection();
        return result;
    }

    public string UpdateRoleInfo()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.UpdateRoleInfo();
        DisposeSQLConnection();
        return result;

    }

    public DataSet GetMenuInfo()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetMenuInfo();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetMenuAll()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetMenuAll();
        DisposeSQLConnection();
        return result;

    }
    public string InsertMenuInfo()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertMenuInfo();
        DisposeSQLConnection();
        return result;
    }
    public string UpdateMenuInfo()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.UpdateMenuInfo();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetEmailId()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetEmailId();
        DisposeSQLConnection();
        return result;
    }

    public string InsertEmailId()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertEmailId();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetAutoEmailId()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GeAutoEmailId();

        return result;
    }

    public DataSet GetAutoEmailId(string Sts1)
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GeAutoEmailId(Sts1);

        return result;
    }
    public DataSet GetAutoEmailId(string Sts1, string EmailSno)
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GeAutoEmailId(Sts1, EmailSno);

        return result;
    }
    public DataSet Sp_GetPayHeaderNew()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetPayHeaderNew();
        DisposeSQLConnection();
        return result;
    }
    public DataSet Sp_GetPayHeaderNewAll()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetPayHeaderNewAll();
        DisposeSQLConnection();
        return result;
    }
    public DataSet Sp_GetPayHeader()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetPayHeader();
        DisposeSQLConnection();
        return result;
    }
    public DataSet Sp_GetPayHeaderMonth()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetPayHeaderMonth();
        DisposeSQLConnection();
        return result;
    }
    public string InsertPayHeader()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertPayHeader();
        DisposeSQLConnection();
        return result;
    }
    public string UpdatePayHeader()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.UpdatePayHeader();
        DisposeSQLConnection();
        return result;
    }
    public DataSet SalesReport(string pStartDate, string pEndDate)
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_SalesReport(pStartDate, pEndDate);
        DisposeSQLConnection();
        return result;
    }
    public string SendLog()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.SendLog();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetJobType()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetJobType();
        DisposeSQLConnection();
        return result;
    }

    public DataSet MoveInRptSales(DataSet InputDataSet, string sDate, string eDate)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        decimal BalanceAmt = 0;
        string BlanceSts = "D";
        try
        {
            //DataRow row = rptDataSet.Tables[0].NewRow();
            //DataRow row1 = InputDataSet.Tables[0].Rows[0];
            foreach (DataRow row1 in InputDataSet.Tables[0].Rows)
            {
                decimal OpeningBalance = UType.MyCtoD(row1["DrTotal"].ToString()) - UType.MyCtoD(row1["CrTotal"].ToString());
                BalanceAmt = OpeningBalance;
                DataRow row = rptDataSet.Tables[0].NewRow();
                row["C1"] = "ABC";
                row["C2"] = "Sakles Report";
                row["C3"] = row1["ActDesc"].ToString();
                row["C4"] = UType.GetDate1(sDate);
                row["C5"] = UType.GetDate1(eDate);
                if (UType.MyCtoD(row1["CrTotal"].ToString()) > UType.MyCtoD(row1["DrTotal"].ToString()))
                {
                    BlanceSts = "C";
                }
                row["C6"] = OpeningBalance.ToString();
                row["C7"] = BlanceSts;
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
                            BlanceSts = "C";
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
                            BlanceSts = "D";
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
                rptDataSet.Tables[0].Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    #region Accounts
    public DataSet GetRefData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetRefData();

        return result;
    }
    public DataSet GetVoucherData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetVoucherData();

        return result;
    }
    #endregion

    public DataSet GetShipmentType()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.ShipmentType();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetTranIdFromPurCosting()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetTranIdFromPurCosting();
        DisposeSQLConnection();
        return result;
    }
    public DataSet ChkVnoInActTran()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_ChkVnoInActTran();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetSaleDataByAwbNo()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.Sp_GetSaleDataByAwbNo();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetProductType()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.ProductType();
        DisposeSQLConnection();
        return result;
    }
    public string InsertPurCosting()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertPurCosting();
        DisposeSQLConnection();
        return result;

    }
    public string InsertPurCostingDtl()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertPurCostingDtl();
        DisposeSQLConnection();
        return result;

    }
    public string DeleteFromPurCosting()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.DeleteFromPurCosting();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetTranIdFromSaleCosting()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetTranIdFromSaleCosting();
        DisposeSQLConnection();
        return result;
    }
    public string DeleteFromSaleCosting()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.DeleteFromSaleCosting();
        DisposeSQLConnection();
        return result;

    }
    public string InsertSaleCosting()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertSaleCosting();
        DisposeSQLConnection();
        return result;

    }
    public string InsertSaleCostingDtl()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertSaleCostingDtl();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetPurchaseData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetPurchaseData();

        return result;
    }

    public DataSet GetSaleR2Data()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetSaleR2Data();

        return result;
    }

    public DataSet GetSaleR3Data_Sp()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetSaleR3Data_Sp();

        return result;
    }
    public DataSet GetSaleR3Data_Sp1()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetSaleR3Data_Sp1();

        return result;
    }
    public DataSet GetSaleR4Data_Sp()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetSaleR4Data_Sp();

        return result;
    }


    public DataSet GetPurchaseCostingData()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetPurchaseCostingData();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetItem()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetItem();
        // DisposeSQLConnection();
        return result;
    }

    public DataSet GetVoucherNo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetVoucherNo();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetVoucherNoSel()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetVoucherNoSel();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetVoucherNoAuto()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetVoucherNoAuto();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetActTranId()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetActTranId();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetTranIdFromPurCostingDtl()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetTranIdFromPurCostingDtl();
        DisposeSQLConnection();
        return result;
    }
    public string UpdatePurchaseId()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.UpdatePurchaseId();
        DisposeSQLConnection();
        return result;

    }
    public DataSet FillComboCreditor()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.FillComboCreditor();

        return result;
    }
    public DataSet FillComboSale()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.FillComboSale();

        return result;
    }
    public DataSet FillComboExpense()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.FillComboExpense();

        return result;
    }
    public DataSet GetTranIdFromSaleCostingDtl()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetTranIdFromSaleCostingDtl();
        DisposeSQLConnection();
        return result;
    }
    public DataSet FillComboProduct()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.FillComboProduct();

        return result;
    }
    public DataSet FillComboVehType()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.FillComboVehType();

        return result;
    }

    public DataSet GetCount()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCount();
        return result;
    }
    public string InsertLoginHistory()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertLoginHistory();
        return result;
    }

    public bool pValidationOld16082021()
    {
        bool retVal = false;
        decimal tDate = 0;
        int Ctr = 0;
        string tSts = string.Empty;
        string cDate = DateTime.Now.ToString("yyyyMMdd");
        try
        {
            MyDb oMyDb = new MyDb(this);  //this._SqlConnection, this);
            DataSet result = oMyDb.pValidation();
            if (result.Tables[0].Rows.Count == 0)
            {
                return retVal;
            }
            if (result.Tables[0].Rows.Count > 0)
            {
                tSts = result.Tables[0].Rows[0]["Fld3"].ToString();
                if (tSts == "0")
                {
                    return retVal;
                }
                string aa1 = MyEncrption.Encrypt("20211231");
                string pCtr = MyEncrption.Encrypt("100");

                tDate = UType.MyCtoD(MyEncrption.DeCrypt(result.Tables[0].Rows[0]["Fld1"].ToString(), 8));
                Ctr = Convert.ToInt16(MyEncrption.DeCrypt(result.Tables[0].Rows[0]["Fld2"].ToString(), 3));
            }
            if (UType.MyCtoD(cDate) > tDate)
            {
                oMyDb = new MyDb(this._SqlConnection, this);
                Fld1 = "Fld3";
                Fld2 = "0";
                string Res = oMyDb.UpdateRfFld();
                return false;
            }
            if (UType.MyCtoD(cDate) < tDate)
            {
                oMyDb = new MyDb(this._SqlConnection, this);
                Fld1 = "Fld2";
                Fld2 = Convert.ToString(Ctr + 1);
                Fld2 = MyEncrption.Encrypt(Fld2);
                string Res = oMyDb.UpdateRfFld();
            }
            if (Ctr > 999)
            {
                oMyDb = new MyDb(this._SqlConnection, this);
                Fld1 = "Fld3";
                Fld2 = "0";
                string Res = oMyDb.UpdateRfFld();
                return false;
            }
            retVal = true;

            //DisposeSQLConnection();
        }
        catch (Exception ex)
        {

            retVal = false;
        }
        return retVal;
    }

    public string pValidation()
    {
        string retVal = "Error-0";
        decimal tDate = 0;
        decimal Ctr = 0;
        string tSts = string.Empty;
        string cDate = DateTime.Now.ToString("yyyyMMdd");
        try
        {
            MyDb oMyDb = new MyDb(this);  //this._SqlConnection, this);
            DataSet result = oMyDb.pValidationStatus();
            if (result.Tables[0].Rows.Count == 0)
            {
                return retVal;
            }
            if (result.Tables[0].Rows.Count > 0)
            {
                tSts = result.Tables[0].Rows[0]["Fld3"].ToString();
                if (tSts == "0")
                {

                    return "Error-1";
                }
                string aa1  = MyEncrption.Encrypt("20251231");
                string pCtr = MyEncrption.Encrypt("10001000");

                tDate = UType.MyCtoD(MyEncrption.DeCrypt(result.Tables[0].Rows[0]["Fld1"].ToString(), 8));
                //Ctr = Convert.ToInt16(MyEncrption.DeCrypt(result.Tables[0].Rows[0]["Fld2"].ToString(), 3));
                Ctr = UType.MyCtoD(MyEncrption.DeCrypt(result.Tables[0].Rows[0]["Fld2"].ToString(), 8));
            }
            if (UType.MyCtoD(cDate) > tDate)
            {
                oMyDb = new MyDb(this._SqlConnection, this);
                Fld1 = "Fld3";
                Fld2 = "0";
                string Res = oMyDb.UpdateRfFld();
                return "Error-2";
            }
            if (UType.MyCtoD(cDate) < tDate)
            {
                oMyDb = new MyDb(this._SqlConnection, this);
                Fld1 = "Fld2";
                Fld2 = Convert.ToString(Ctr + 1);
                Fld2 = MyEncrption.Encrypt(Fld2);
                string Res = oMyDb.UpdateRfFld();
            }
            if (Ctr > 10002000)
            {
                oMyDb = new MyDb(this._SqlConnection, this);
                Fld1 = "Fld3";
                Fld2 = "0";
                string Res = oMyDb.UpdateRfFld();
                return "Error-3";
            }
            retVal = "ok";

            //DisposeSQLConnection();
        }
        catch (Exception ex)
        {

            retVal = "ok";
        }
        return retVal;
    }
    public DataSet GetClearanceData()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetClearanceData();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetByExporter()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetByExporter();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetDupFileNo()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetDupFileNo();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetDupCntr()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetDupCntr();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetDupBOE()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetDupBOE();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetDupFE()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetDupFE();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetDupVehicle()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetDupVehicle();
        DisposeSQLConnection();
        return result;
    }

    public string InsertClearanceHdr()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertClearanceHdr();
        return result;
    }


    public string UpdateClearanceHdr()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateClearanceHdr();
        return result;
    }
    public DataSet GetCustomer()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetCustomer();

        return result;
    }
    public DataSet GetCntr()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetCntr();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetExpense()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetExpense();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetClrRpt()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetClrRpt();
        DataSet result1 = oMyDb.GetCntr();
        addTableinDataSet(result, result1.Tables[0], "Cntr");
        DataSet result2 = oMyDb.GetExpense();
        addTableinDataSet(result, result2.Tables[0], "Expense");
        DataSet result3 = oMyDb.GetAllDedPrint();
        addTableinDataSet(result, result3.Tables[0], "AllExpense"); DisposeSQLConnection();
        return result;
    }
    public DataSet GetClrRpt2()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetClrRpt2();
        DataSet result2 = oMyDb.GetExpenseClrRpt2();
        addTableinDataSet(result, result2.Tables[0], "Expense");
        DisposeSQLConnection();
        return result;
    }
    public string DeleteClearanceCntr()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteClearanceCntr();
        return result;
    }
    public string InsertClearanceCntr()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertClearanceCntr();
        return result;
    }
    public string DeleteClearanceAll()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteClearanceBill();
        return result;
    }
    public string InsertClearanceAll()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertClearanceBill();
        return result;
    }
    public DataSet GetRfCustomer()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetRfCustomer();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetFileNo()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetFileNo();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetInvRpt()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetInvRpt1();
        // DisposeSQLConnection();
        return result;
    }
    public string MaxUserID()
    {
        string retVal = "0";
        MyDb oMyDb = new MyDb(this);
        DataSet dsResult = oMyDb.MaxUserID();
        if (dsResult.Tables[0].Rows.Count > 0)
        {
            retVal = dsResult.Tables[0].Rows[0]["Maxuserid"].ToString();
        }
        retVal = Convert.ToString(Convert.ToDecimal(retVal) + 1);
        return retVal;
    }
    public string MaxEmpID()
    {
        string retVal = "0";
        MyDb oMyDb = new MyDb(this);
        DataSet dsResult = oMyDb.MaxEmpID();
        if (dsResult.Tables[0].Rows.Count > 0)
        {
            retVal = dsResult.Tables[0].Rows[0]["MaxEmpid"].ToString();
        }
        retVal = Convert.ToString(Convert.ToDecimal(retVal) + 1);
        return retVal;
    }
    public string InsertUserIPInfo()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertUserIPInfo();
        DisposeSQLConnection();
        return result;
    }
    public string GetUserIPID(string UserId)
    {
        string retVal = "0";
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        Fld1 = UserId;
        DataSet dsResult = oMyDb.GetIPId();
        if (dsResult.Tables[0].Rows.Count > 0)
        {
            retVal = dsResult.Tables[0].Rows[0]["IPID"].ToString();
        }
        if (dsResult.Tables[0].Rows.Count == 0)
        {
            retVal = MaxIPID();
        }
        return retVal;
    }
    public string InsertUserMac()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        string result = oMyDb.InsertUserMac();
        DisposeSQLConnection();
        return result;
    }
    public string MaxIPID()
    {
        string retVal = "0";
        MyDb oMyDb = new MyDb(this);
        DataSet dsResult = oMyDb.MaxIPID();
        if (dsResult.Tables[0].Rows.Count > 0)
        {
            retVal = dsResult.Tables[0].Rows[0]["MaxIPid"].ToString();
        }
        retVal = Convert.ToString(Convert.ToDecimal(retVal) + 1);
        return retVal;
    }
    public DataSet GetInventoryReport()
    {

        MyDb oMyDb = new MyDb(this);
        DataSet dsResult = oMyDb.InventoryReport();
        return dsResult;
    }
    public DataSet GetQuestion()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetQuestion();
        return result;
    }
    public string UpdateProject()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateProject();
        return result;
    }
    public string InsertProject()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertProject();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetMaxUserID()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxUserID();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetModuleFromUserID()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetModuleFromUserID();
        DisposeSQLConnection();
        return result;
    }

    public string UpdateUserMac()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateUserMac();
        return result;
    }

    //Urdu
    public DataSet GetUrduMenu()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetUrduMenu();
        return result;
    }
    public string InsertOfficeChart()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertOfficeChart();
        DisposeSQLConnection();
        return result;

    }
    public string InsertOffChart()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertOffChart();
        DisposeSQLConnection();
        return result;

    }
    public string UpdateRef1()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateRef1();
        DisposeSQLConnection();
        return result;

    }
    public string InsertDemo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertDemo();
        DisposeSQLConnection();
        return result;

    }
    public string UpdateDemo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateDemo();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetMainGridData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMainGridData();

        return result;
    }
    public DataSet GetQuerydData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetQueryData();

        return result;
    }
    public DataSet GetQuerydAccount()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetQueryAccount();

        return result;
    }
    public DataSet GetInventorydData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetInventorydData();

        return result;
    }
    public DataSet GetVesselMBLData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetVesselMBLData();

        return result;
    }

    public DataSet GetVesselMBLJobData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetVesselMBLJobData();

        return result;
    }
    public DataSet GetCustomerData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCustomerData();
        return result;
    }

    public DataSet GetArrivalInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetArrivalInfo();
        return result;
    }


    public DataSet GetCustomerDescription()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCustomerDescription();
        return result;
    }
    public DataSet GetRfPort()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetRfPort();
        return result;
    }
    public DataSet GetLocationData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetLocationData();

        return result;
    }
    public DataSet GetCountryData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCountryData();

        return result;
    }
    public DataSet GetCountriesData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCountriesData();

        return result;
    }
    public string GetCityddl()
    {
        string retVal = "";
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCityddl();
        if (result != null)
        {
            retVal = result.Tables[0].Rows[0]["city_name"].ToString();
        }
        return retVal;


    }
    public string GetCountriesddl()
    {
        string retVal = "";
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCountriesddl();
        if (result != null)
        {
            retVal = result.Tables[0].Rows[0]["country_name"].ToString();
        }
        return retVal;
    }
    public DataSet GetCountryManifest()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCountryManifest();

        return result;
    }

    public DataSet GetCountryDataNew()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCountryDataNew();

        return result;
    }
    public DataSet GetCityData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCityData();

        return result;
    }
    public DataSet GetCityData1()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCityData1();

        return result;
    }
    public DataSet GetCityDataNew()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCityDataNew();

        return result;
    }
    public DataSet GetCountryDataNew1()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCountryDataNew1();

        return result;
    }
    public DataSet GetCityDataAct()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCityDataAct();

        return result;
    }
    public DataSet GetIncoTermData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetIncoTermData();
        return result;
    }
    public DataSet GetCustomerStatus()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCustomerStatus();
        return result;
    }

    public DataSet GetCustomerStatusInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCustomerStatus();
        return result;
    }

    public string InsertRfCustomerStatus()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertRfCustomerStatus();
        return result;

    }
    public string UpdateRfCustomerStatus()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateRfCustomerStatus();
        return result;

    }

    public DataSet GetTranDetail()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetTranDetail();
        return result;
    }
    public DataSet GetTran()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetTran();
        return result;
    }
    public DataSet GetCurrencyData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCurrencyData();
        return result;
    }
    public string UpdateJob()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateJobInfo();
        DisposeSQLConnection();
        return result;

    }

    #region Region Manifest
    public DataSet GetManifest()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetManifest();
        return result;
    }
    public DataSet GetExpManifestGrid()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetManifestGrid();
        return result;
    }
    public DataSet GetMaxManifest()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxManifest();
        return result;
    }
    public string InsertManifest()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertManifest();
        return result;

    }
    public string UpdateManifest()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateManifest();
        return result;

    }

    #endregion


    #region Region MBLInfo
    public DataSet GetMBLInfoGrid()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMBLInfoGrid();
        return result;
    }
    public DataSet GetBookInfoGrid()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetBookInfoGrid();
        return result;
    }
    public DataSet GetMBLInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMBLInfo();
        return result;
    }
    public DataSet GetMaxMBLInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxMBLInfo();
        return result;
    }

    public string InsertMBLInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertMBLInfo();
        return result;

    }
    public string UpdateMBLInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateMBLInfo();
        return result;

    }

    #endregion

    #region Region HBLInfo
    public DataSet GetHBLInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetHBLInfo();
        return result;
    }
    public DataSet GetMaxHBLInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxHBLInfo();
        return result;
    }

    public string InsertHBLInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertHBLInfo();
        return result;

    }
    public string UpdateHBLInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateHBLInfo();
        return result;

    }
    public string DeleteHBLInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteHBLInfo();
        return result;

    }
    #endregion

    #region MyRegion Arrival Note
    public DataSet GetArrivalNote()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetJobInfo();

        return result;
    }
    public string InsertArrivalNote()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertArrivalNote();
        return result;

    }
    public string UpdateArrivalNote()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateArrivalNote();
        return result;

    }

    #endregion

    #region Region JobInfo
    public DataSet GetJobInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetJobInfo();

        return result;
    }

    public DataSet GetJobnEquipmentInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetJobnEquipmentInfo();

        return result;
    }
    public DataSet GetEquipmentWeb()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEquipmentWeb();

        return result;
    }
    public DataSet GetEquipmentWeb1()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEquipmentWeb1();

        return result;
    }
    public DataSet GetRoutingWeb()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetRoutingWeb();

        return result;
    }
    public DataSet GetMaxJob()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxJobInfo();

        return result;
    }

    public string InsertJobInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertJobInfo();
        return result;

    }
    public string UpdateJobInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateJobInfo();
        return result;

    }

    #endregion

    #region Region Equipment
    public DataSet GetEquipment()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEquipment();
        return result;
    }
    public DataSet GetMaxEquipment()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxEquipment();
        return result;
    }

    public string InsertEquipment()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertEquipment();
        return result;

    }
    public string UpdateEquipment()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateEquipment();
        return result;

    }
    public string UpdateEquipmentNew()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateEquipmentNew();
        return result;

    }
    public string UpdateEquipmentNew2()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateEquipmentNew2();
        return result;

    }
    public string UpdateEquipment1()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateEquipment1();
        return result;

    }
    public string DeleteEquipment()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteEquipment();
        return result;

    }
    #endregion

    #region Region EquipmentSummary
    public DataSet GetEquipmentSummary()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEquipmentSummary();
        return result;
    }
    public DataSet GetMaxEquipmentSummary()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxEquipmentSummary();
        return result;
    }

    public string InsertEquipmentSummary()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertEquipmentSummary();
        return result;

    }
    public string UpdateEquipmentSummary()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateEquipmentSummary();
        return result;

    }

    #endregion


    #region Region Product
    public DataSet GetProduct()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetProduct();
        return result;
    }
    public DataSet GetMaxProduct()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxProduct();
        return result;
    }

    public string InsertProduct()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertProduct();
        return result;

    }
    public string UpdateProduct()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateProduct();
        return result;

    }

    #endregion

    #region Region ChargesHdr
    public DataSet GetChargesHdr()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetChargesHdr();
        return result;
    }


    public string InsertChargesHdr()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertChargesHdr();
        return result;

    }
    public string UpdateChargesHdr()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateChargesHdr();
        return result;

    }
    public string DeleteVoucherActtran()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteVoucherActtran();
        return result;

    }

    #endregion

    #region Region Charges
    public DataSet GetCharges()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCharges();
        return result;
    }
    public DataSet GetChargesEq()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetChargesEq();
        return result;
    }
    public DataSet GetMaxCharges()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxCharges();
        return result;
    }
    public DataSet GetMaxChargesPaymentID()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxChargesPaymentID();
        return result;
    }
    public string InsertCharges()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertCharges();
        return result;

    }
    public string UpdateCharges()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateCharges();
        return result;

    }
    #endregion

    #region Region ExpChargesHdr
    public DataSet GetExpChargesHdr()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetExpChargesHdr();
        return result;
    }


    public string InsertExpChargesHdr()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertExpChargesHdr();
        return result;

    }
    public string UpdateExpChargesHdr()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateExpChargesHdr();
        return result;

    }
    public string DeleteExpVoucherActtran()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteExpVoucherActtran();
        return result;

    }

    #endregion

    #region Region ExpCharges
    public DataSet GetExpCharges()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetExpCharges();
        return result;
    }
    public DataSet GetMaxExpCharges()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxExpCharges();
        return result;
    }
    public DataSet GetMaxExpChargesPaymentID()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxChargesPaymentID();
        return result;
    }
    public string InsertExpCharges()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertExpCharges();
        return result;

    }
    public string UpdateExpCharges()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateExpCharges();
        return result;

    }
    #endregion

    #region Region UserMenu
    public DataSet GetUserMenu1()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetUserMenu1();
        return result;
    }
    public string AddUserMenu()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.AddUserMenu();
        return result;

    }
    public string UpdateMenu()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateUserMenu();
        return result;

    }
    #endregion

    #region Region BLDetail
    public DataSet GetBLDetail()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetBLDetail();
        return result;
    }

    public DataSet GetBLDetailSelected()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetBLDetailSelected();
        return result;
    }
    public DataSet GetMaxBLDetail()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxBLDetail();
        return result;
    }

    public string InsertBLDetail()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertBLDetail();
        return result;

    }
    public string UpdateBLDetail()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateBLDetail();
        return result;

    }
    #endregion

    #region Region Routing
    public DataSet GetRouting()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetRouting();
        return result;
    }
    public string InsertRouting()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertRouting();
        return result;

    }
    public string UpdateRouting()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateRouting();
        DisposeSQLConnection();
        return result;

    }
    #endregion

    #region Region OtherInfo
    public DataSet GetOtherInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetOtherInfo();
        return result;
    }

    public string InsertOtherInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertOtherInfo();
        return result;

    }

    public string UpdateOtherInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateOtherInfo();
        DisposeSQLConnection();
        return result;

    }
    #endregion

    #region Region Invoice
    public DataSet GetInvoice()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetInvoice();
        return result;
    }


    public string InsertInvoice()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertInvoice();
        return result;

    }
    public string UpdateInvoice()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateInvoice();
        return result;

    }

    #endregion

    #region Region InvoiceDtl
    public DataSet GetInvoiceDtl()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetInvoiceDtl();
        return result;
    }
    public DataSet GetInvoiceDtlgrid()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetInvoiceDtlgrid();
        return result;
    }


    public string InsertInvoiceDtl()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertInvoiceDtl();
        return result;

    }
    public string UpdateInvoiceDtl()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateInvoiceDtl();
        return result;

    }
    public DataSet GetMaxInvoiceDtl()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxInvoiceDtl();
        return result;
    }
    #endregion
    public DataSet GetOffice()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetOffice();
        return result;
    }
    public DataSet GetProject()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetProject();
        return result;
    }
    public DataSet GetRoleInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetAllRole();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetUserRole()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetUserRole();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetUserRole1()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetUserRole1();
        DisposeSQLConnection();
        return result;
    }
    public string InsertUserrole()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertUserrole();
        DisposeSQLConnection();
        return result;
    }
    public string UpdateUserRole()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateUserRole();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetAllMenu()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetAllMenu();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetRfRoleMenu()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetRfRoleMenu();
        DisposeSQLConnection();
        return result;

    }
    public string InsertRfRoleMenu()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertRfRoleMenu();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetRoleMenu()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetRoleMenu();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetRoleMenuRole()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMenuByRole();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetRoleMenuByRole()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetRoleMenuByRole();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetAccountExpense()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetAccountExpense();
        return result;
    }
    public DataSet GetAccountExpense1()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetAccountExpense1();
        return result;
    }
    public DataSet GetAccountConsignee()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetAccountConsignee();
        return result;
    }

    public DataSet GetAccountOneConsignee()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetAccountOneConsignee();
        return result;
    }
    public DataSet GetAccountnCity()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetAccountnCity();
        return result;
    }
    public string GetAccounDescription()
    {
        string retVal = "";
        MyDb oMyDb = new MyDb(this);
        DataSet ds1 = oMyDb.GetAccountOneConsignee();
        //DataSet ds1 = GetCustomerInfo();
        if (ds1.Tables[0].Rows.Count > 0)
        {
            retVal = ds1.Tables[0].Rows[0]["actdesc"].ToString();
        }
        return retVal;

    }


    #region Region Receipt
    public DataSet GetReceipt()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetReceipt();
        return result;
    }


    public string InsertReceipt()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertReceipt();
        return result;

    }
    public string UpdateReceipt()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateReceipt();
        return result;

    }

    #endregion

    #region Region ReceiptDtl
    public DataSet GetReceiptDtl()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetReceiptDtl();
        return result;
    }


    public string InsertReceiptDtl()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertReceiptDtl();
        return result;

    }
    public string UpdateReceiptDtl()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateReceiptDtl();
        return result;

    }
    public DataSet GetMaxReceiptDtl()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxReceiptDtl();
        return result;
    }
    #endregion
    public DataSet GetUserMenu()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetUserMenu();
        DisposeSQLConnection();
        return result;

    }
    public DataSet DoCheckEdit()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.DoCheckEdit();

        return result;
    }
    #region Region Delivery Order
    public DataSet GetDeliveryOrder()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDeliveryOrder();
        return result;
    }

    public string InsertDeliveryOrder()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertDeliveryOrder();
        return result;

    }
    public string UpdateDeliveryOrder()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateDeliveryOrder();
        return result;

    }
    #endregion
    public DataSet GetHBLInfoByJobNo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetHBLInfoByJobNo();
        return result;
    }
    public DataSet GetDdlDescrition()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDdlDescrition();
        return result;
    }
    public DataSet GetMenuAll1()
    {
        MyDb oMyDb = new MyDb(this._SqlConnection, this);
        DataSet result = oMyDb.GetMenuAll();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetCountryIDFromCustomer()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCountryIDFromCustomer();

        return result;
    }
    public DataSet GetCurrency()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCurrency();

        return result;
    }
    public string DeleteWeBOCIndex()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteWeBOCIndex();

        return result;
    }
    public string InsertWeBOCIndex()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertWeBOCIndex();

        return result;
    }

    public string UpdateWeBOCIndex()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateWeBOCIndex();

        return result;
    }
    public string InsertWeBOCItems()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertWeBOCItems();

        return result;
    }

    public string UpdateWeBOCItems()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateWeBOCItems();

        return result;
    }

    public string InsertContainerItems()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertContainerItems();

        return result;
    }
    public string InsertContainer()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertContainer();

        return result;
    }

    public string InsertEmptyContainer()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertEmptyContainer();

        return result;
    }
    public DataSet GetDOinfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDOinfo();

        return result;
    }

    #region Region DeliveryOrder
    public DataSet GetDoDetailInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEpassDataJob();

        DataSet result1 = oMyDb.GetEquipmentWeb1();
        addTableinDataSet(result, result1.Tables[0], "TblEq");
        return result;
    }

    public DataSet GetDetInvoiceDetailInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEpassDataJob();

        DataSet result1 = oMyDb.GetDetInvoiceWeb();
        addTableinDataSet(result, result1.Tables[0], "TblEq");
        return result;
    }

    public DataSet GetJobRecInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetJobInfoAccounts();

        DataSet result1 = oMyDb.GetDetInvoiceWebRecPrint();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        DataSet result2 = oMyDb.GetDetInvoiceWeb1();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetDetInvoiceWebSmry();  //     GetEquipmentWeb();
        addTableinDataSet(result, result3.Tables[0], "TblSmry");
        DataSet result4 = oMyDb.GetDetInvoiceWebPayMBL();  //     GetEquipmentWeb();
        addTableinDataSet(result, result4.Tables[0], "TblMBL");
        return result;
    }



    public DataSet GetExpJobRecInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetExpJobInfoAccounts();

        DataSet result1 = oMyDb.GetExpDetInvoiceWebRecPrint();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        DataSet result2 = oMyDb.GetExpDetInvoiceWeb1();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetExpDetInvoiceWebSmry();  //     GetEquipmentWeb();
        addTableinDataSet(result, result3.Tables[0], "TblSmry");
        DataSet result4 = oMyDb.GetExpDetInvoiceWebPayMBL();  //     GetEquipmentWeb();
        addTableinDataSet(result, result4.Tables[0], "TblMBL");
        return result;
    }
    public DataSet GetJobPayInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetJobInfoAccounts();
        DataSet result1 = oMyDb.GetDetInvoiceWebPayNew();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        // Fld5 = "0";
        // foreach (DataRow row in result1.Tables[0].Rows)
        //  {
        ////      if (UType.MyCtoD(row["Vno"].ToString()) > 0)
        //     {
        //        Fld5 = row["Vno"].ToString();
        //    }
        // }
        DataSet result2 = oMyDb.GetDetInvoiceWeb1();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetChargesHdrReport();  //     GetEquipmentWeb();
        addTableinDataSet(result, result3.Tables[0], "TblSmry");
        DataSet result4 = oMyDb.GetDetInvoiceWebPayMBL();  //     GetEquipmentWeb();
        addTableinDataSet(result, result4.Tables[0], "TblMBL");
        try
        {
            DataSet result5 = oMyDb.GetActTranPayInvoice();  //     GetEquipmentWeb();
            addTableinDataSet(result, result5.Tables[0], "TblActTran");
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public DataSet GetExpJobPayInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetExpJobInfoAccounts();
        DataSet result1 = oMyDb.GetExpDetInvoiceWebPayNew();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        // Fld5 = "0";
        // foreach (DataRow row in result1.Tables[0].Rows)
        //  {
        ////      if (UType.MyCtoD(row["Vno"].ToString()) > 0)
        //     {
        //        Fld5 = row["Vno"].ToString();
        //    }
        // }
        DataSet result2 = oMyDb.GetExpDetInvoiceWeb1();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetExpChargesHdrReport();  //     GetEquipmentWeb();
        addTableinDataSet(result, result3.Tables[0], "TblSmry");
        DataSet result4 = oMyDb.GetExpDetInvoiceWebPayMBL();  //     GetEquipmentWeb();
        addTableinDataSet(result, result4.Tables[0], "TblMBL");
        try
        {
            DataSet result5 = oMyDb.GetActTranPayInvoice();  //     GetEquipmentWeb();
            addTableinDataSet(result, result5.Tables[0], "TblActTran");
        }
        catch (Exception ex)
        {

        }
        return result;
    }


    public DataSet GetJobPayInfo1712()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetJobInfoAccounts();
        DataSet result1 = oMyDb.GetDetInvoiceWebPayNew1712();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        // Fld5 = "0";
        // foreach (DataRow row in result1.Tables[0].Rows)
        //  {
        ////      if (UType.MyCtoD(row["Vno"].ToString()) > 0)
        //     {
        //        Fld5 = row["Vno"].ToString();
        //    }
        // }
        DataSet result2 = oMyDb.GetDetInvoiceWeb1();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetChargesHdrReport();  //     GetEquipmentWeb();
        addTableinDataSet(result, result3.Tables[0], "TblSmry");
        DataSet result4 = oMyDb.GetDetInvoiceWebPayMBL();  //     GetEquipmentWeb();
        addTableinDataSet(result, result4.Tables[0], "TblMBL");
        DataSet result5 = oMyDb.GetActTranPayInvoice();  //     GetEquipmentWeb();
        addTableinDataSet(result, result5.Tables[0], "TblActTran");
        return result;
    }

    public DataSet GetExpJobPayInfo1712()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetExpJobInfoAccounts();
        DataSet result1 = oMyDb.GetExpDetInvoiceWebPayNew1712();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        // Fld5 = "0";
        // foreach (DataRow row in result1.Tables[0].Rows)
        //  {
        ////      if (UType.MyCtoD(row["Vno"].ToString()) > 0)
        //     {
        //        Fld5 = row["Vno"].ToString();
        //    }
        // }
        DataSet result2 = oMyDb.GetExpDetInvoiceWeb1();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetExpChargesHdrReport();  //     GetEquipmentWeb();
        addTableinDataSet(result, result3.Tables[0], "TblSmry");
        DataSet result4 = oMyDb.GetExpDetInvoiceWebPayMBL();  //     GetEquipmentWeb();
        addTableinDataSet(result, result4.Tables[0], "TblMBL");
        DataSet result5 = oMyDb.GetActTranPayInvoice();  //     GetEquipmentWeb();
        addTableinDataSet(result, result5.Tables[0], "TblActTran");
        return result;
    }

    public DataSet GetNOCInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEpassDataJobRec();

        //DataSet result1 = oMyDb.GetEquipmentWeb();
        // addTableinDataSet(result, result1.Tables[0], "TblEq");
        return result;
    }
    public DataSet GetGPDetailInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEpassDataJob();

        DataSet result1 = oMyDb.GetEquipmentGatePass();
        addTableinDataSet(result, result1.Tables[0], "TblEq");
        return result;
    }
    public DataSet GetMaxDeliveryOrder()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxDeliveryOrder();
        return result;
    }

    public string GetDescriptionDDL(string Para1)
    {
        string retVal = "";
        if (Para1 == "")
        {
            return retVal;
        }
        Fld20 = Para1;
        Fld1 = "0";
        DataSet ds1 = GetCustomerInfo();
        if (ds1.Tables[0].Rows.Count > 0)
        {
            retVal = ds1.Tables[0].Rows[0]["CustomerName"].ToString();
        }
        return retVal;
    }
    public string GetDescriptionDDL1(string Para1)
    {
        string retVal = "";
        if (Para1 == "")
        {
            return retVal;
        }
        _EmpId = Para1;
        Fld1 = "0";
        DataSet ds1 = GetCustomerInfo();
        if (ds1.Tables[0].Rows.Count > 0)
        {
            retVal = ds1.Tables[0].Rows[0]["CustomerName"].ToString() + "\r\n" + ds1.Tables[0].Rows[0]["CustomerAddress"].ToString();
        }
        return retVal;
    }
    #endregion
    public void CloseConnection()
    {


        OleDbCommand oCmd = null;
        OleDbDataAdapter oAdapter = null;
        OleDbConnection oCon = Connection.OleDbConnection;


        try
        {

            //oCmd.Dispose();
            //oAdapter.Dispose();

            oCon.Close();
            oCon.Dispose();
            oCon = null;
            GC.Collect();


        }
        catch (Exception ex)
        {

            throw;
        }
        finally
        {


        }



    }

    public DataSet GetEpassData()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEpassData();

        return result;
    }
    public DataSet GetEpassDataBlDetail()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEpassDataBLDetail();

        return result;
    }

    public DataSet GetEpassDataPartial()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEpassDataPartial();

        return result;
    }


    public DataSet GetEpassDataJob()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEpassDataJob();

        return result;
    }
    public DataSet GetEpassDataArrival()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEpassDataArrival();

        return result;
    }
    public DataSet GetEpassDataNOC()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEpassDataNOC();

        DataSet result1 = oMyDb.GetEquipmentGatePass();
        addTableinDataSet(result, result1.Tables[0], "TblEq");
        return result;
    }
    public DataSet GetEpassDataTest1()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEpassDataTest1();

        return result;
    }
    public string MyColor()
    {
        string retVal = "";
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetRfColor();
        if (result.Tables[0].Rows.Count == 0)
        {
            retVal = "#bfdbff";
        }
        if (result.Tables[0].Rows.Count > 0)
        {
            if (_Fld2 == "1")
            {
                retVal = result.Tables[0].Rows[0]["BackColor"].ToString();

            }
        }
        return retVal;

    }
    public DataSet MyColorDS()
    {

        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.MyColor();

        return result;

    }
    public string InsertColor()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertColor();

        return result;
    }

    public string UpdateColor()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateColor();

        return result;
    }
    public DataSet GetRfColor()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetRfColor();

        return result;
    }

    public string Inertlog()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertLog();

        return result;
    }
    public DataSet Sp_GetGraphInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.Sp_GetGraphInfo();
        return result;
    }
    public DataSet Sp_GetGraphInfo1()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.Sp_GetGraphInfo1();
        return result;
    }
    public DataSet GetMaxPaymentID()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxPaymentID();

        return result;
    }
    public string UpdateChargesPaymentID()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateChargesPaymentID();
        return result;

    }
    public string UpdateExpChargesPaymentID()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateExpChargesPaymentID();
        return result;

    }
    #region Export
    public DataSet GetBLReport()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCROReportBookInfo();
        DataSet result1 = oMyDb.GetCROReportEquipment();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblEq");

        return result;
    }
    public DataSet GetLoadingReport()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetCROReportBookInfo();
        DataSet result1 = oMyDb.GetCROReportEquipment();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblEq");

        return result;
    }
    #region BookInfo
    public DataSet GetJobBalanceReport()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetJobBalanceReport();
        DataSet result1 = oMyDb.GetCROReportEquipment();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblEq");

        return result;
    }


    public DataSet GetBookInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetBookInfo();

        return result;
    }



    public DataSet GetMaxBookInfo()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxBookInfo();

        return result;
    }

    public string InsertBookInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertBookInfo();
        return result;

    }
    public string UpdateBookInfo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateBookInfo();
        DisposeSQLConnection();
        return result;

    }
    #endregion

    #region Region Export BLDetail
    public DataSet GetExpBLDetail()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetExpBLDetail();
        return result;
    }

    public DataSet GetExpBLDetailSelected()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetExpBLDetailSelected();
        return result;
    }
    public DataSet GetMaxExpBLDetail()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxExpBLDetail();
        return result;
    }
    public DataSet GetDupExpBLDetail()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDupExpBLDetail();
        return result;
    }
    public string InsertExpBLDetail()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertExpBLDetail();
        return result;

    }
    public string UpdateExpBLDetail()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateExpBLDetail();
        return result;

    }

    #endregion

    #region Region ExpRouting
    public DataSet GetExpRouting()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetExpRouting();
        return result;
    }
    public string InsertExpRouting()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertExpRouting();
        return result;

    }
    public string UpdateExpRouting()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateExpRouting();
        DisposeSQLConnection();
        return result;

    }

    #endregion

    #region  ExpManifest
    public DataSet GetExpManifest()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetExpManifest();

        return result;
    }
    public string InsertExpManifest()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertExpManifest();
        return result;

    }
    public string UpdateExpManifest()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateExpManifest();
        return result;

    }
    public DataSet GetMaxExpManifest()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxExpManifest();
        return result;
    }

    #endregion

    #region Region Export Equipment
    public DataSet GetExpEquipment()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetExpEquipment();
        return result;
    }
    public DataSet GetMaxExpEquipment()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxExpEquipment();
        return result;
    }

    public string InsertExpEquipment()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertExpEquipment();
        return result;

    }
    public string UpdateExpEquipment()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateExpEquipment();
        return result;

    }
    public string UpdateExpEquipment1()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateExpEquipment1();
        return result;

    }
    #endregion

    #region Region ExpEquipmentSummary
    public DataSet GetExpEquipmentSummary()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetEquipmentSummary();
        return result;
    }
    public DataSet GetMaxExpEquipmentSummary()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxEquipmentSummary();
        return result;
    }

    public string InsertExpEquipmentSummary()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InsertEquipmentSummary();
        return result;

    }
    public string UpdateExpEquipmentSummary()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateEquipmentSummary();
        return result;

    }

    #endregion

    #endregion
    public DataSet GetDetReportRec()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDetInvoiceReport();
        DataSet result1 = oMyDb.GetDetInvoiceWebRecReport();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        DataSet result2 = oMyDb.GetDetReportEq();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetRecReportAct();
        addTableinDataSet(result, result3.Tables[0], "TblAct");
        return result;
    }

    public DataSet GetExpDetReportRec()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetExpDetInvoiceReport();
        DataSet result1 = oMyDb.GetExpDetInvoiceWebRecReport();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        DataSet result2 = oMyDb.GetDetReportEq();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetRecReportAct();
        addTableinDataSet(result, result3.Tables[0], "TblAct");
        return result;
    }
    public DataSet GetRemarksPay()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDetInvoiceReport();
        DataSet result1 = oMyDb.GetDetRemarksPay();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        DataSet result2 = oMyDb.GetDetReportEq();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetRecReportAct();
        addTableinDataSet(result, result3.Tables[0], "TblAct");
        return result;
    }
    public DataSet GetDetReportPay()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDetInvoiceReport();
        DataSet result1 = oMyDb.GetDetInvoiceWebPayReport();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        DataSet result2 = oMyDb.GetDetReportEq();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetRecReportAct();
        addTableinDataSet(result, result3.Tables[0], "TblAct");
        return result;
    }

    public DataSet GetDetReportPay0606()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDetInvoiceReport();
        DataSet result1 = oMyDb.GetDetInvoiceWebPayReport();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        DataSet result2 = oMyDb.GetDetReportEq();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetRecReportAct();
        addTableinDataSet(result, result3.Tables[0], "TblAct");
        return result;
    }
    public DataSet GetDetReportRecNew()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDetInvoiceWebRecRemarks();

        return result;
    }
    public DataSet GetDetReportRecNew1()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDetInvoiceWebRecRemarks1();

        return result;
    }
    public DataSet GetDetReport()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDetInvoiceReport();
        DataSet result1 = oMyDb.GetDetReportCh();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        DataSet result2 = oMyDb.GetDetReportEq();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetDetReportEqDetention();
        addTableinDataSet(result, result3.Tables[0], "TblEqDet");
        return result;
    }
    public DataSet GetCostingReport()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDetInvoiceReport();
        DataSet result1 = oMyDb.GetDetReportChCosting();  //     GetEquipmentWeb();
        addTableinDataSet(result, result1.Tables[0], "TblCh");
        DataSet result2 = oMyDb.GetDetReportEq();
        addTableinDataSet(result, result2.Tables[0], "TblEq");
        DataSet result3 = oMyDb.GetCostingAccount();
        addTableinDataSet(result, result3.Tables[0], "TblAccount");
        return result;
    }

    public DataSet MoveInRptDsJobReceiptVoucher(DataSet InputDataSet, string Customer)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row11 = InputDataSet.Tables[0].Rows[0];  
             
            DataRow rowEq = InputDataSet.Tables["TblEq"].Rows[0]; 
            DataRow row2 = InputDataSet.Tables["TblCh"].Rows[0];
            

            string ArrivalDate = UType.GetDateTxt(row11["ArrivalDate"].ToString());  //GetActCity1(row1["PortLoading"].ToString());
            string Portofloading = GetActCity1(row11["Portloading"].ToString());
            string PortDischarge = GetActCity1(row11["PortDischarge"].ToString());
            string FinalDestination = GetActCity(row11["FinalDestination"].ToString());
            string ShippingCompanyID = GetDescriptionDDL(row11["ShippingCompanyID"].ToString());
            string Consignee = "";
            string ShipperName = "";
            string ConsigneeName = "";
            decimal LocalAmount = 0; decimal TotalLocalAmount = 0;




            MyMain oMy = new MyMain();
            oMy = new MyMain();
            
            
            
            int Ctr = 1;
            string HBLNo = "";
            bool IsSecurity = false;
            
            #region loop
            foreach (DataRow row1 in InputDataSet.Tables["TblCh"].Rows)
            {
                HBLNo = row1["HBLNo"].ToString();
                bool IsNew = true;
                DataRow row = rptDataSet.Tables[0].NewRow();
                foreach (DataRow rowN in rptDataSet.Tables[0].Rows)
                {
                    if (HBLNo == rowN["C90"].ToString())
                    {
                        row = rowN;
                        IsNew = false;
                    }
                }

                // DataRow row = rptDataSet.Tables[0].NewRow();
                oMy = new MyMain();
                try
                {
                    row["C90"] = HBLNo;
                    try
                    {
                        row["C11"] = UType.GetDateTxt(row1["trandate"].ToString()); //row2["paymentidyear"].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                    //row["C12"] = "JR " + row2["paymentid"].ToString() + "/" + row2["paymentidyear"].ToString();
                    row["C12"] = "JR " + row2["vno"].ToString() + "/" + row2["paymentidyear"].ToString();
                    row["C13"] = ConsigneeName;
                    row["C15"] = "Cash";
                    try
                    {
                        if (row1["ChqNo"].ToString().Length > 0)
                        {
                            row["C15"] = "Bank / Cheque";
                        }
                        row["C16"] = row1["ChqNo"].ToString();
                        row["C17"] = UType.GetDateTxt(row1["ChqDate"].ToString());
                    }
                    catch (Exception ex)
                    {
                    }
                    row["C18"] = row1["particularname"].ToString();
                    row["C19"] = "PKR"; // GetCurrencyString(row2["currency"].ToString());
                    row["C6"] = rowEq["sno"].ToString();
                    row["C1"] = row1["billInvoice"].ToString();
                    row["C2"] = UType.GetDateTxt(rowEq["adddate"].ToString());
                    row["C3"] = "";
                    row["C4"] = row1["MBLNo"].ToString() + " " + row1["HBLNo"].ToString();
                    row["C5"] = row1["jobno"].ToString();
                    row["C7"] = "Regular";
                    row["C9"] = row2["exrate"].ToString();
                    row["CLogo"] = GetImage();
                    row["C24"] = row1["Narration"].ToString(); // row11["RemarksRec"].ToString();

                    try
                    {
                        row["C71"] = row11["OfficeId"].ToString();
                        row["C72"] = row11["ProjectId"].ToString();
                    }
                    catch (Exception ex)
                    { }
                    try
                    {
                        //For Receipt voucher
                        if (row1["vtypeid"].ToString() == "2" || row1["vtypeid"].ToString() == "4")
                        {
                            if (row1["particular"].ToString() != "4010040007")
                            {
                                if (UType.MyCtoD(row1["exrate"].ToString()) > 0)
                                {
                                    row["C8"] = Convert.ToString(UType.MyCtoD(row["C8"].ToString()) + (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString())));
                                    row["C10"] = row["C8"];
                                    TotalLocalAmount = TotalLocalAmount + (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString()));

                                    //row["C14"] = TotalLocalAmount.ToString();
                                    row["C14"] = "PKR " + UType.numb1(Convert.ToString(TotalLocalAmount)) + " Only";
                                    row["C25"] = TotalLocalAmount.ToString();
                                }
                            }
                        }
                        //For Security Deposite
                        if (row1["vtypeid"].ToString() == "8")
                        {
                            if (row1["particular"].ToString() == "4010040007")
                            {
                                if (UType.MyCtoD(row1["exrate"].ToString()) > 0)
                                {
                                    row["C8"] = Convert.ToString(UType.MyCtoD(row["C8"].ToString()) + (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString())));
                                    row["C10"] = row["C8"];
                                    TotalLocalAmount = TotalLocalAmount + (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString()));
                                    //row["C14"] = TotalLocalAmount.ToString();
                                    row["C14"] = "PKR " + UType.numb1(Convert.ToString(TotalLocalAmount)) + " Only";
                                    row["C25"] = TotalLocalAmount.ToString();
                                    row["C7"] = "Security Container";
                                    row["C18"] = "Security in Hand";
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                    if (IsNew)
                    {
                        rptDataSet.Tables[0].Rows.Add(row);
                    }
                    Ctr = Ctr + 1;
                }

                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            #endregion
            foreach (DataRow row in rptDataSet.Tables[0].Rows)
            {
                row["C8"] = UType.MyFormat1(row["C8"].ToString());
                row["C10"] = UType.MyFormat1(row["C10"].ToString());
                //row["C14"] = UType.NumberToWords(TotalLocalAmount);
                row["C14"] = "PKR " + UType.numb1(Convert.ToString(TotalLocalAmount)) + " Only";
                row["C25"] = UType.MyFormat1(row["C25"].ToString());
            }
            //

        }
        catch (Exception ex)
        {
            //   throw (ex);
        }
        //Getting Remarks
        //rptDataSet = DoRecRemarks(rptDataSet, "1");
        //End of Remarks
        return rptDataSet;
    }

    public DataSet MoveInRptDsJobReceiptVoucherBk(DataSet InputDataSet, string Customer)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row11 = InputDataSet.Tables[0].Rows[0]; DataRow rowAct = null;
            try
            {
                rowAct = InputDataSet.Tables["TblAct"].Rows[0];
            }
            catch (Exception ex)
            {
            }
            DataRow rowEq = InputDataSet.Tables["TblEq"].Rows[0]; DataRow row2 = InputDataSet.Tables["TblCh"].Rows[0];
            //DataRow row2 = InputDataSet.Tables[0].Rows[0]; DataRow row3 = InputDataSet.Tables["TblMBL"].Rows[0];

            string ArrivalDate = UType.GetDateTxt(row11["ArrivalDate"].ToString());  //GetActCity1(row1["PortLoading"].ToString());
            string Portofloading = GetActCity1(row11["Portloading"].ToString());
            string PortDischarge = GetActCity1(row11["PortDischarge"].ToString());
            string FinalDestination = GetActCity(row11["FinalDestination"].ToString());
            string ShippingCompanyID = GetDescriptionDDL(row11["ShippingCompanyID"].ToString());
            string Consignee = "";
            string ShipperName = "";
            string ConsigneeName = "";
            decimal LocalAmount = 0; decimal TotalLocalAmount = 0;




            MyMain oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                #region dsConsignee
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row11["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row11["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row2["Customer"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ConsigneeName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }
                #endregion
            }
            catch (Exception ex)
            {
            }
            string particularName = "";
            try
            {
                #region dsparticular
                DataSet dsparticular = null;
                MyMain oMyparticular = new MyMain();
                oMyparticular.Fld1 = row11["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyparticular.Fld2 = row11["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyparticular.Fld3 = row2["particular"].ToString();

                dsparticular = oMyparticular.GetAccountOneConsignee();
                if (dsparticular != null)
                {
                    particularName = dsparticular.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }
                #endregion
            }
            catch (Exception ex)
            {
            }
            int Ctr = 1;
            string HBLNo = "";
            bool IsSecurity = false;
            //foreach (DataRow row1 in InputDataSet.Tables["TblCh"].Rows)
            //{
            //    if (row1["Particular"].ToString() == UType.GetSecurityAmount())
            //    {
            //        IsSecurity = true;
            //    }
            //}
            #region loop
            foreach (DataRow row1 in InputDataSet.Tables["TblCh"].Rows)
            {
                HBLNo = row1["HBLNo"].ToString();
                bool IsNew = true;
                DataRow row = rptDataSet.Tables[0].NewRow();
                foreach (DataRow rowN in rptDataSet.Tables[0].Rows)
                {
                    if (HBLNo == rowN["C90"].ToString())
                    {
                        row = rowN;
                        IsNew = false;
                    }
                }

                // DataRow row = rptDataSet.Tables[0].NewRow();
                oMy = new MyMain();
                try
                {
                    row["C90"] = HBLNo;
                    try
                    {
                        row["C11"] = UType.GetDateTxt(rowAct["trandate"].ToString()); //row2["paymentidyear"].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                    //row["C12"] = "JR " + row2["paymentid"].ToString() + "/" + row2["paymentidyear"].ToString();
                    row["C12"] = "JR " + row2["vno"].ToString() + "/" + row2["paymentidyear"].ToString();
                    row["C13"] = ConsigneeName;
                    row["C15"] = "Cash";
                    try
                    {
                        if (rowAct["ChqNo"].ToString().Length > 0)
                        {
                            row["C15"] = "Bank / Cheque";
                        }
                        row["C16"] = rowAct["ChqNo"].ToString();
                        row["C17"] = UType.GetDateTxt(rowAct["ChqDate"].ToString());
                    }
                    catch (Exception ex)
                    {
                    }
                    row["C18"] = particularName;  //row2["particular"].ToString();
                    row["C19"] = "PKR"; // GetCurrencyString(row2["currency"].ToString());
                    row["C6"] = rowEq["sno"].ToString();
                    row["C1"] = row1["billInvoice"].ToString();
                    row["C2"] = UType.GetDateTxt(rowEq["adddate"].ToString());
                    row["C3"] = "";
                    row["C4"] = row1["MBLNo"].ToString() + " " + row1["HBLNo"].ToString();
                    row["C5"] = row1["jobno"].ToString();
                    row["C7"] = "Regular";
                    row["C9"] = row2["exrate"].ToString();
                    row["CLogo"] = GetImage();
                    row["C24"] = rowAct["Narration"].ToString(); // row11["RemarksRec"].ToString();

                    try
                    {
                        row["C71"] = row11["OfficeId"].ToString();
                        row["C72"] = row11["ProjectId"].ToString();
                    }
                    catch (Exception ex)
                    { }
                    try
                    {
                        //For Receipt voucher
                        if (rowAct["vtypeid"].ToString() == "2" || rowAct["vtypeid"].ToString() == "4")
                        {
                            if (row1["particular"].ToString() != "4010040007")
                            {
                                if (UType.MyCtoD(row1["exrate"].ToString()) > 0)
                                {
                                    row["C8"] = Convert.ToString(UType.MyCtoD(row["C8"].ToString()) + (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString())));
                                    row["C10"] = row["C8"];
                                    TotalLocalAmount = TotalLocalAmount + (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString()));

                                    //row["C14"] = TotalLocalAmount.ToString();
                                    row["C14"] = "PKR " + UType.numb1(Convert.ToString(TotalLocalAmount)) + " Only";
                                    row["C25"] = TotalLocalAmount.ToString();
                                }
                            }
                        }
                        //For Security Deposite
                        if (rowAct["vtypeid"].ToString() == "8")
                        {
                            if (row1["particular"].ToString() == "4010040007")
                            {
                                if (UType.MyCtoD(row1["exrate"].ToString()) > 0)
                                {
                                    row["C8"] = Convert.ToString(UType.MyCtoD(row["C8"].ToString()) + (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString())));
                                    row["C10"] = row["C8"];
                                    TotalLocalAmount = TotalLocalAmount + (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString()));
                                    //row["C14"] = TotalLocalAmount.ToString();
                                    row["C14"] = "PKR " + UType.numb1(Convert.ToString(TotalLocalAmount)) + " Only";
                                    row["C25"] = TotalLocalAmount.ToString();
                                    row["C7"] = "Security Container";
                                    row["C18"] = "Security in Hand";
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                    if (IsNew)
                    {
                        rptDataSet.Tables[0].Rows.Add(row);
                    }
                    Ctr = Ctr + 1;
                }

                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            #endregion
            foreach (DataRow row in rptDataSet.Tables[0].Rows)
            {
                row["C8"] = UType.MyFormat1(row["C8"].ToString());
                row["C10"] = UType.MyFormat1(row["C10"].ToString());
                //row["C14"] = UType.NumberToWords(TotalLocalAmount);
                row["C14"] = "PKR " + UType.numb1(Convert.ToString(TotalLocalAmount)) + " Only";
                row["C25"] = UType.MyFormat1(row["C25"].ToString());
            }
            //

        }
        catch (Exception ex)
        {
            //   throw (ex);
        }
        //Getting Remarks
        //rptDataSet = DoRecRemarks(rptDataSet, "1");
        //End of Remarks
        return rptDataSet;
    }


    public DataSet MoveInRptDsJobReceiptVoucher1(DataSet InputDataSet, string Customer)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row11 = InputDataSet.Tables[0].Rows[0]; DataRow rowAct = null;
            try
            {
                rowAct = InputDataSet.Tables["TblAct"].Rows[0];
            }
            catch (Exception ex)
            {
            }
            DataRow rowEq = InputDataSet.Tables["TblEq"].Rows[0]; DataRow row2 = InputDataSet.Tables["TblCh"].Rows[0];
            //DataRow row2 = InputDataSet.Tables[0].Rows[0]; DataRow row3 = InputDataSet.Tables["TblMBL"].Rows[0];

            string ArrivalDate = UType.GetDateTxt(row11["ArrivalDate"].ToString());  //GetActCity1(row1["PortLoading"].ToString());
            string Portofloading = GetActCity1(row11["Portloading"].ToString());
            string PortDischarge = GetActCity1(row11["PortDischarge"].ToString());
            string FinalDestination = GetActCity(row11["FinalDestination"].ToString());
            string ShippingCompanyID = GetDescriptionDDL(row11["ShippingCompanyID"].ToString());
            string Consignee = "";
            string ShipperName = "";
            string ConsigneeName = "";
            decimal LocalAmount = 0; decimal TotalLocalAmount = 0;




            MyMain oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                #region dsConsignee
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row11["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row11["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row2["Customer"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ConsigneeName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }
                #endregion
            }
            catch (Exception ex)
            {
            }
            string particularName = "";
            try
            {
                #region dsparticular
                DataSet dsparticular = null;
                MyMain oMyparticular = new MyMain();
                oMyparticular.Fld1 = row11["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyparticular.Fld2 = row11["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyparticular.Fld3 = row2["particular"].ToString();

                dsparticular = oMyparticular.GetAccountOneConsignee();
                if (dsparticular != null)
                {
                    particularName = dsparticular.Tables[0].Rows[0]["ActDesc"].ToString(); //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();  //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }
                #endregion
            }
            catch (Exception ex)
            {
            }
            int Ctr = 1;
            string HBLNo = "";
            bool IsSecurity = false;
            //foreach (DataRow row1 in InputDataSet.Tables["TblCh"].Rows)
            //{
            //    if (row1["Particular"].ToString() == UType.GetSecurityAmount())
            //    {
            //        IsSecurity = true;
            //    }
            //}
            #region loop
            foreach (DataRow row1 in InputDataSet.Tables["TblCh"].Rows)
            {
                HBLNo = row1["HBLNo"].ToString();
                bool IsNew = true;
                DataRow row = rptDataSet.Tables[0].NewRow();
                foreach (DataRow rowN in rptDataSet.Tables[0].Rows)
                {
                    if (HBLNo == rowN["C90"].ToString())
                    {
                        row = rowN;
                        IsNew = false;
                    }
                }

                // DataRow row = rptDataSet.Tables[0].NewRow();
                oMy = new MyMain();
                try
                {
                    row["C90"] = HBLNo;
                    row["C11"] = UType.GetDateTxt(rowAct["trandate"].ToString()); //row2["paymentidyear"].ToString();
                    //row["C12"] = "JR " + row2["paymentid"].ToString() + "/" + row2["paymentidyear"].ToString();
                    row["C12"] = "JR " + row2["vno"].ToString() + "/" + row2["paymentidyear"].ToString();
                    row["C13"] = ConsigneeName;
                    try
                    {
                        if (rowAct["ChqNo"].ToString().Length > 0)
                        {
                            row["C15"] = "Bank / Cheque";
                        }
                        row["C16"] = rowAct["ChqNo"].ToString();
                        row["C17"] = UType.GetDateTxt(rowAct["ChqDate"].ToString());
                    }
                    catch (Exception ex)
                    {
                    }
                    row["C18"] = particularName;  //row2["particular"].ToString();
                    row["C19"] = GetCurrencyString(row2["currency"].ToString());
                    row["C6"] = rowEq["sno"].ToString();
                    row["C1"] = row1["billInvoice"].ToString();
                    row["C2"] = UType.GetDateTxt(rowEq["adddate"].ToString());
                    row["C3"] = "";
                    row["C4"] = row1["MBLNo"].ToString() + " " + row1["HBLNo"].ToString();
                    row["C5"] = row1["jobno"].ToString();
                    //row["C7"] = row1["cntrno"].ToString();
                    row["C8"] = row11["vessel"].ToString();
                    row["C9"] = row11["voyage"].ToString();
                    row["CLogo"] = GetImage();
                    row["C24"] = row11["RemarksRec"].ToString();

                    try
                    {
                        row["C71"] = row11["OfficeId"].ToString();
                        row["C72"] = row11["ProjectId"].ToString();
                    }
                    catch (Exception ex)
                    { }
                    try
                    {
                        //For Receipt voucher
                        if (rowAct["vtypeid"].ToString() == "1" || rowAct["vtypeid"].ToString() == "3")
                        {
                            if (row1["particular"].ToString() != "4010040007")
                            {
                                if (UType.MyCtoD(row1["exrate"].ToString()) > 0)
                                {
                                    //row["C8"] = Convert.ToString(UType.MyCtoD(row["C8"].ToString()) + (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString())));
                                    row["C10"] = row1["localamount"].ToString();// row["C8"];
                                    //TotalLocalAmount = TotalLocalAmount + (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString()));
                                    TotalLocalAmount = TotalLocalAmount + UType.MyCtoD(row1["localamount"].ToString()); // () * UType.MyCtoD(row1["exrate"].ToString()));
                                                                                                                        //row["C14"] = TotalLocalAmount.ToString();
                                                                                                                        //row["C14"] = UType.NumberToWords(TotalLocalAmount);
                                    row["C14"] = "PKR " + UType.numb1(Convert.ToString(TotalLocalAmount)) + " Only";
                                    row["C25"] = TotalLocalAmount.ToString();

                                }
                            }
                        }
                        //For Security Deposite
                        if (rowAct["vtypeid"].ToString() == "8")
                        {
                            if (row1["particular"].ToString() == "4010040007")
                            {
                                if (UType.MyCtoD(row1["exrate"].ToString()) > 0)
                                {
                                    //row["C8"] = Convert.ToString(UType.MyCtoD(row["C8"].ToString()) + (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString())));
                                    row["C10"] = row["localamount"];
                                    TotalLocalAmount = TotalLocalAmount + (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString()));
                                    //row["C14"] = TotalLocalAmount.ToString();
                                    //row["C14"] = UType.NumberToWords(TotalLocalAmount);
                                    row["C14"] = "PKR " + UType.numb1(Convert.ToString(TotalLocalAmount)) + " Only";
                                    row["C25"] = TotalLocalAmount.ToString();

                                }
                            }
                        }
                        if (IsNew)
                        {
                            rptDataSet.Tables[0].Rows.Add(row);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    Ctr = Ctr + 1;
                }

                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            #endregion
            string CntrDetail = ""; decimal Ft20 = 0; decimal Ft40 = 0;
            int RowCount = InputDataSet.Tables["TblEq"].Rows.Count;
            if (RowCount == 1)
            {
                CntrDetail = InputDataSet.Tables["TblEq"].Rows[0]["ContainerNo"].ToString();
            }
            if (RowCount > 1)
            {
                foreach (DataRow row in InputDataSet.Tables["TblEq"].Rows)
                {
                    CntrDetail = CntrDetail + GetDescriptionDDL(row2["sizentype"].ToString());
                    if (CntrDetail.ToString().Substring(0, 2) == "20")
                    {
                        Ft20 = Ft20 + 1;
                    }
                    if (CntrDetail.ToString().Substring(0, 2) == "40")
                    {
                        Ft40 = Ft40 + 1;
                    }
                }
                if (Ft20 > 0)
                {
                    CntrDetail = Ft20.ToString() + " 20Ft ";
                }
                if (Ft40 > 0)
                {
                    CntrDetail = Ft40.ToString() + " 40Ft ";
                }
            }
            decimal ctr12 = 1; decimal PKRAmountTotal = 0;
            foreach (DataRow row in rptDataSet.Tables[0].Rows)
            {
                row["C6"] = ctr12;
                row["C7"] = CntrDetail;
                PKRAmountTotal = PKRAmountTotal + UType.MyCtoD(row["C10"].ToString());
                row["C10"] = UType.MyFormat2(row["C10"].ToString());
                //row["C14"] = UType.NumberToWords(TotalLocalAmount);
                row["C14"] = "PKR " + UType.numb1(Convert.ToString(TotalLocalAmount)) + " Only";
                row["C25"] = UType.MyFormat2(row["C25"].ToString());
                ctr12 = ctr12 + 1;
            }
            foreach (DataRow row in rptDataSet.Tables[0].Rows)
            {
                row["C74"] = UType.MyFormat2(PKRAmountTotal.ToString());
            }
                //

            }
        catch (Exception ex)
        {
            //   throw (ex);
        }
        //Getting Remarks
        rptDataSet = DoRecRemarks(rptDataSet, "2");
        //End of Remarks
        return rptDataSet;
    }
    private DataSet DoRecRemarks(DataSet RptDs, string Sts)
    {

        string Rem = "";
        MyMain oMy1 = new MyMain();
        oMy1.Fld1 = RptDs.Tables[0].Rows[0]["C71"].ToString();
        oMy1.Fld2 = RptDs.Tables[0].Rows[0]["C72"].ToString();
        oMy1.Fld3 = RptDs.Tables[0].Rows[0]["C5"].ToString();
        string tChqNo = RptDs.Tables[0].Rows[0]["C16"].ToString();
        oMy1.Fld4 = Sts;

        //oMy1.Fld5 = Session["sVno"].ToString();
        DataSet inputDataSet = oMy1.GetDetReportRecNew();
        // # 11 Dated 2023 Against Inv/HBL/MBL/Job No/File No/Reference No/Vehicle No  From shiping Receipt #";


        try
        {
            foreach (DataRow row1 in inputDataSet.Tables[0].Rows)
            {
                //row11["MBLNo"].ToString() + " " + row11["HBLNo"].ToString();
                //Rem = Rem + row1["TotalNetAmount"].ToString() + " Against MBL No:" + row1["MBLNo"].ToString() + " , HBL No: " + row1["HBLNo"].ToString();
                try
                {
                    if (Sts == "1")
                    {
                        Rem = Rem + " Received ";
                    }
                    if (Sts == "2")
                    {
                        Rem = Rem + " Pay ";
                    }
                    Rem = Rem + " Cheque ";
                    Rem = Rem + tChqNo; //row1["Chqno"].ToString();
                }
                catch (Exception)
                { }
                Rem = Rem + " Against MBL No:" + row1["MBLNo"].ToString() + " , HBL No: " + row1["HBLNo"].ToString();
                Rem = Rem + " , Vessel :" + row1["vessel"].ToString() + "  Job No: " + row1["JobNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message.ToString();
        }

        foreach (DataRow row1 in RptDs.Tables[0].Rows)
        {
            if (Sts == "1")
            {
                row1["C24"] = Rem; // this.TxtRecRemarks.Text = Rem; //C24
            }
            if (Sts == "2")
            {
                row1["C24"] = Rem; //this.txtPayRemarks.Text = Rem;
            }
        }

        return RptDs;
    }
    //new


    public DataSet GetDetention()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetDetention();
        return result;
    }
    public string FillMisHdr()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.FillMisHdr();
        return result;
    }
    public string UpdateMisHdr()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateMisHdr();
        return result;

    }

    public DataSet GetNewAttached()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetNewAttached();
        return result;
    }
    public DataSet GetNewAttachedDet()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetNewAttachedDet();
        return result;
    }
    public DataSet GetNewAttachedBank()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetNewAttachedBank();
        return result;
    }
    public DataSet GetNewAttachedExp()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetNewAttachedExp();
        return result;
    }
    public string Updateattach()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateAttach();
        DisposeSQLConnection();
        return result;

    }
    public string UpdateattachExp()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateAttachExp();
        DisposeSQLConnection();
        return result;

    }
    public string UpdateattachBank()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateAttachBank();
        DisposeSQLConnection();
        return result;

    }
    public string DeleteAttach()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteAttach();
        DisposeSQLConnection();
        return result;

    }
    public string DeleteAttachBank()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteAttachBank();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetRfPCT()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetRfPCT();

        return result;
    }
    public string DeleteRfDetention()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteRfDetention();

        DisposeSQLConnection();
        return result;

    }
    public string InsertRfDetention()
    {
        MyDb oMyDb = new MyDb(this);
        //string result = oMyDb.DeleteRfDetention();
        string result = oMyDb.InsertRfDetention();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetRfDetention()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetRfDetention();

        return result;
    }
    public string GetCity(string Officeid, string ProjectID, string CityID)
    {
        string retVal = "";
        try
        {
            DataSet dsResult = null;
            string tem1 = Fld20;
            MyMain oMy = new MyMain();


            oMy.Fld1 = CityID;

            dsResult = oMy.GetCityData();

            if (dsResult != null)
            {

                retVal = dsResult.Tables[0].Rows[0]["City_name"].ToString();
                if (tem1 == "1")
                {
                    retVal = retVal + " " + dsResult.Tables[0].Rows[0]["Country_name"].ToString();
                }
            }

        }
        catch (Exception)
        {

            retVal = "";
        }
        return retVal;
    }
    public string GetCityCountry(string Officeid, string ProjectID, string CityID)
    {
        string retVal = "";
        try
        {
            DataSet dsResult = null;
            MyMain oMy = new MyMain();


            oMy.Fld1 = CityID;

            dsResult = oMy.GetCityData();

            if (dsResult != null)
            {

                retVal = dsResult.Tables[0].Rows[0]["City_name"].ToString();
            }

        }
        catch (Exception)
        {

            retVal = "";
        }
        return retVal;
    }
    public string GetExpenseRate()
    {
        string retVal = "";
        try
        {
            MyDb oMyDb = new MyDb(this);
            DataSet result = oMyDb.GetExpenseRate();
            if (result != null)
            {

                retVal = result.Tables[0].Rows[0]["Expenserate"].ToString();
            }
        }
        catch (Exception)
        {
            retVal = "";
        }
        return retVal;
    }
    public string DeleteDatainTable()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteDatainTable();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetOpeningBalance()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetOpeningBalance();
        return result;
    }
    public DataSet GetActivity()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetActivity();
        return result;
    }
    public DataSet GetJobWiseContainerList()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetJobContainer();


        return result;
    }
    public DataSet GetReceiptList()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetReceiptList();

        return result;
    }
    public DataSet MoveInRptDsReceiptList(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row1 = InputDataSet.Tables[0].Rows[0];

            string ClearingAgent = ""; string Client = "";

            #region Accountexpense
            MyMain oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyAccountexpense = new MyMain();
                oMyAccountexpense.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyAccountexpense.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyAccountexpense.Fld3 = row1["Client"].ToString();

                dsConsignee = oMyAccountexpense.GetAccountExpense1();
                if (dsConsignee != null)
                {
                    Client = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {
            }
            #endregion

            #region ClearingAgent
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["shipper"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ClearingAgent = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion


            foreach (DataRow rowCh in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                //qq


                row["C7"] = "SEJ-" + UType.MyFormat4(row1["JobNo"].ToString()) + "-" + UType.GetYYYY();
                row["C8"] = UType.GetDate1(row1["JobDate"].ToString());
                row["C9"] = Client;
                row["C10"] = row1["billInvoice"].ToString();
                row["C11"] = UType.MyFormat4(row1["JobNo"].ToString());
                row["C12"] = row1["HBLNo"].ToString();
                row["C13"] = ClearingAgent;
                try
                {
                    if (row1["ChequeNo"].ToString().Length > 0)
                    {
                        row["C14"] = "Bank / Cheque";
                    }
                    row["C15"] = row1["ChequeNo"].ToString();
                    row["C16"] = UType.GetDateTxt(row1["ChequeDate"].ToString());
                }
                catch (Exception ex)
                {
                }

                row["C18"] = GetCurrencyString(row1["currency"].ToString());
                if (UType.MyCtoD(row1["exrate"].ToString()) > 0)
                {
                    row["C19"] = (UType.MyCtoD(row1["netamount"].ToString()) * UType.MyCtoD(row1["exrate"].ToString()));
                }

                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);

            }
        }
        catch (Exception ex)
        {
        }

        return rptDataSet;

    }

    public DataSet MoveInRptDeliveryOrderList(DataSet InputDataSet)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row1 = InputDataSet.Tables[0].Rows[0];

            string ClearingAgent = ""; string Client = "";

            #region Accountexpense
            MyMain oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyAccountexpense = new MyMain();
                oMyAccountexpense.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyAccountexpense.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyAccountexpense.Fld3 = row1["Client"].ToString();

                dsConsignee = oMyAccountexpense.GetAccountExpense1();
                if (dsConsignee != null)
                {
                    Client = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {
            }
            #endregion

            #region ClearingAgent
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["shipper"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ClearingAgent = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion


            foreach (DataRow rowCh in InputDataSet.Tables[0].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                //qq

                row["C6"] = row1["SNo"].ToString();
                row["C7"] = "SEJ-" + UType.MyFormat4(row1["JobNo"].ToString()) + "-" + UType.GetYYYY();
                row["C8"] = UType.GetDate1(row1["dono"].ToString());
                row["C9"] = UType.GetDateTxt(row1["doDate"].ToString());  //Client;
                row["C10"] = row1["MBL"].ToString();
                row["C11"] = row1["HBLNo"].ToString();  //UType.MyFormat4(row1["JobNo"].ToString());
                Fld1 = row1["portofloading"].ToString();
                row["C12"] = GetCityData();
                row["C13"] = Client; //ClearingAgent;
                row["C14"] = "";
                row["C15"] = ClearingAgent;
                row["C16"] = "";

                row["C17"] = row1["Container"].ToString() + " " + row1["seal"].ToString() + " " + row1["sizentype"].ToString();
                row["C18"] = UType.GetDateTxt(row1["invDate"].ToString());
                row["C19"] = "";
                row["C20"] = "";
                row["C21"] = "";
                row["CLogo"] = GetImage();
                rptDataSet.Tables[0].Rows.Add(row);

            }
        }
        catch (Exception ex)
        {
        }

        return rptDataSet;

    }

    public string UpdateIndexNo()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateIndexNo();

        return result;
    }
    public DataSet GetAccountSearch()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetAccountSearch();
        return result;
    }
    public string InsertInventory()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.InserInventory();
        DisposeSQLConnection();
        return result;
    }
    public string UpdateInventory()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.UpdateInventory();
        return result;

    }
    public DataSet GetMaxBillInvoice()
    {
        MyDb oMyDb = new MyDb(this);
        DataSet result = oMyDb.GetMaxBillInvoice();
        return result;
    }
    public string DeleteUsereMenu()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteUserMenu();
        DisposeSQLConnection();
        return result;

    }
    public DataSet MoveInRptDsJobPayinfo(DataSet InputDataSet, string Customer)
    {
        DataSet rptDataSet = UType.SetRptDataset();
        try
        {
            DataRow row1 = null;
            DataRow rowMBL = null;
            row1 = InputDataSet.Tables[0].Rows[0];
            string ArrivalDate = UType.GetDateTxt(row1["ArrivalDate"].ToString());  //GetActCity1(row1["PortLoading"].ToString());
            string Portofloading = GetActCity1(row1["Portloading"].ToString());
            string PortDischarge = GetActCity1(row1["PortDischarge"].ToString());
            string FinalDestination = GetActCity1(row1["FinalDestination"].ToString());
            string ShippingCompanyID = GetDescriptionDDL(row1["ShippingCompanyID"].ToString());
            string Customer1 = "";
            string ShipperName = "";
            string ConsigneeName = ""; string Continers = "";

            foreach (DataRow rowEq1 in InputDataSet.Tables["TblEq"].Rows)
                try
                {
                    {
                        Continers = Continers + " " + rowEq1["ContainerNo"];
                    }
                }
                catch (Exception ex)
                {

                }
            #region shipper
            MyMain oMy = new MyMain();
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["shipper"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ShipperName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            {


            }
            #endregion
            #region Consignee
            oMy = new MyMain();
            try
            {
                DataSet dsConsignee = null;
                MyMain oMyConsignee = new MyMain();
                oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                oMyConsignee.Fld3 = row1["Consignee"].ToString();

                dsConsignee = oMyConsignee.GetAccountOneConsignee();
                if (dsConsignee != null)
                {
                    ConsigneeName = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                    oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                    oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                    oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                }

            }
            catch (Exception ex)
            { }
            #endregion



            int SnoCtr = 1;
            foreach (DataRow rowCh in InputDataSet.Tables[1].Rows)
            {
                DataRow row = rptDataSet.Tables[0].NewRow();
                #region Accountexpense
                oMy = new MyMain();
                try
                {

                    row["C19"] = ArrivalDate;
                    row["C20"] = row1["jobno"];
                    row["C21"] = UType.GetDateTxt(row1["JobDate"].ToString());
                    row["C22"] = row1["Vessel1"].ToString() + " " + row1["voyage1"].ToString();
                    row["C23"] = Portofloading; //  GetActCity1(row1["Portofloading"].ToString());
                    #region Customer
                    oMy = new MyMain();
                    try
                    {
                        DataSet dsCustomer = null;
                        MyMain oMyConsignee = new MyMain();
                        oMyConsignee.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                        oMyConsignee.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                        oMyConsignee.Fld3 = rowCh["Customer"].ToString();

                        dsCustomer = oMyConsignee.GetAccountOneConsignee();
                        if (dsCustomer != null)
                        {
                            Customer1 = dsCustomer.Tables[0].Rows[0]["ActDesc"].ToString();
                            oMy.Fld10 = dsCustomer.Tables[0].Rows[0]["country_name"].ToString();
                            oMy.Fld11 = oMy.Fld10 + dsCustomer.Tables[0].Rows[0]["city_name"].ToString();
                            oMy.Fld12 = dsCustomer.Tables[0].Rows[0]["ActAddress"].ToString();
                        }

                    }
                    catch (Exception ex)
                    { }
                    #endregion
                    row["C24"] = Customer1;  //PortDischarge; //ShipperName; //   PortDischarge; // GetActCity1(row1["PortDischarge"].ToString());
                    row["C51"] = FinalDestination; // GetActCity1(row1["FinalDestination"].ToString());
                    row["C52"] = ShippingCompanyID; // GetDescriptionDDL(row1["ShippingCompanyID"].ToString());

                    row["C40"] = PortDischarge;
                    row["C25"] = row1["volume"].ToString();

                    DataSet dsConsignee = null;
                    MyMain oMyAccountexpense = new MyMain();
                    oMyAccountexpense.Fld1 = row1["OfficeId"].ToString();  //Session["sOfficeId"].ToString();
                    oMyAccountexpense.Fld2 = row1["ProjectId"].ToString(); // Session["sProjectId"].ToString();
                    oMyAccountexpense.Fld3 = rowCh["particular"].ToString();

                    dsConsignee = oMyAccountexpense.GetAccountExpense1();
                    if (dsConsignee != null)
                    {
                        row["C4"] = dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                        //oMy.Fld10 = dsConsignee.Tables[0].Rows[0]["country_name"].ToString();
                        //oMy.Fld11 = oMy.Fld10 + dsConsignee.Tables[0].Rows[0]["city_name"].ToString();
                        // oMy.Fld12 = dsConsignee.Tables[0].Rows[0]["ActAddress"].ToString();
                    }

                }
                catch (Exception ex)
                { }
                #endregion




                //qq
                row["C17"] = ShipperName; // dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                row["C18"] = ConsigneeName; // dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                row["C2"] = rowCh["SNo"];
                row["C3"] = Convert.ToString(SnoCtr);  //rowCh["sno"].ToString();
                // row["C4"] = rowCh["charges"].ToString();  //Consignee; // dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString();
                // row["C4"] = ""; //dsConsignee.Tables[0].Rows[0]["ActDesc"].ToString(); //rowCh["charges"].ToString();

                row["C5"] = rowCh["Quantity"];
                row["C6"] = rowCh["rate"];
                row["C7"] = GetCurrencyString(rowCh["currency"].ToString());
                row["C8"] = rowCh["exRate"];
                row["C9"] = rowCh["NetAmount"]; // GetDescriptionDDL(row1["ShippingLineID"].ToString());
                row["C10"] = rowCh["LocalAmount"];
                // row["C11"] = row2["LocalAmount"];
                //qq
                //header
                row["C11"] = GetActCity(row1["PortDischarge"].ToString());
                row["C12"] = row1["jobno"].ToString();
                row["C13"] = UType.GetDateTxt(row1["jobDate"].ToString());

                row["C14"] = GetCurrencyString(rowCh["Currency"].ToString());
                row["C15"] = rowCh["ExRate"].ToString();

                row["C16"] = "";




                try
                {
                    //rowMBL = InputDataSet.Tables["TblMBL"].Rows[0];
                    row["C53"] = row1["MBLNo"].ToString(); //rowMBL["MBLNo"].ToString();
                    row["C54"] = row1["HBLNo"].ToString(); //rowMBL["HBLNo"].ToString();
                }
                catch (Exception ex)
                { }



                row["C26"] = "";

                row["C27"] = "";
                row["C28"] = "";
                row["C79"] = Continers;
                //row["C29"] = row1["Measurement"];
                row["CLogo"] = GetImage();
                if (rowCh["Customer"].ToString() == Customer)
                {
                    rptDataSet.Tables[0].Rows.Add(row);
                }
                SnoCtr = SnoCtr + 1;

            }


            decimal TotalTaxAmount = 0; decimal TotalDiscountAmount = 0; decimal TotalNetAmount = 0; decimal TotalLocalAmount = 0;

            foreach (DataRow row11 in InputDataSet.Tables["TblCH"].Rows)
            {
                if (row11["Customer"].ToString() == Customer)
                {
                    TotalTaxAmount += UType.MyCtoD(row11["TaxAmount"].ToString());
                    TotalNetAmount += UType.MyCtoD(row11["NetAmount"].ToString());
                    TotalLocalAmount += UType.MyCtoD(row11["LocalAmount"].ToString());
                    TotalNetAmount += UType.MyCtoD(row11["NetAmount"].ToString());
                    TotalDiscountAmount += UType.MyCtoD(row11["discount"].ToString());

                }
            }

            decimal TotalWt = 0; decimal TotalPCs = 0;

            foreach (DataRow row11 in InputDataSet.Tables["TblEq"].Rows)
            {
                TotalWt += UType.MyCtoD(row11["GrossWt"].ToString());
                TotalWt += UType.MyCtoD(row11["package"].ToString());

            }
            foreach (DataRow row11 in rptDataSet.Tables[0].Rows)
            {
                row11["C26"] = UType.MyFormat(TotalWt.ToString());
                row11["C27"] = UType.MyFormat(TotalWt.ToString());
                row11["C29"] = UType.MyFormat(TotalDiscountAmount.ToString());
                row11["C30"] = UType.MyFormat(TotalTaxAmount.ToString());
                row11["C31"] = UType.MyFormat(TotalLocalAmount.ToString()); //UType.MyFormat(TotalNetAmount.ToString());
                row11["C32"] = UType.MyFormat(TotalLocalAmount.ToString());
                row11["C33"] = UType.NumberToWords(UType.MyCtoD(row11["C31"].ToString()));
            }


        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return rptDataSet;
    }

    public string DeleteRfDeten()
    {
        MyDb oMyDb = new MyDb(this);
        string result = oMyDb.DeleteRfDeten();
        DisposeSQLConnection();
        return result;
    }
}

