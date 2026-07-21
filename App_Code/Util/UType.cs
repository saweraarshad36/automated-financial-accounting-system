using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;
using System.Text;

public static class UType
{
    private static readonly string[] Ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
    private static readonly string[] Teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
    private static readonly string[] Tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
    private static readonly string[] Thousands = { "", "Thousand", "Million", "Billion", "Trillion" };


    #region Parse

    //public static DateTime ParseYYYYMMDD(string date)
    //{
    //    int y = UType.ToInt32(UStr.Substring(date, 0, 4));
    //    int m = UType.ToInt32(UStr.Substring(date, 4, 2));
    //    int d = UType.ToInt32(UStr.Substring(date, 6, 2));

    //    DateTime dx = new DateTime(y, m, d);

    //    return dx;
    //}

    //public static DateTime ParsePakDate(string pakDate)
    //{
    //    int d = UType.ToInt32(UStr.Substring(pakDate, 0, 2));
    //    int m = UType.ToInt32(UStr.Substring(pakDate, 3, 2));
    //    int y = UType.ToInt32(UStr.Substring(pakDate, 6, 4));

    //    DateTime dx = new DateTime(y, m, d);

    //    return dx;
    //}

    #endregion

    #region Format

    //public static string FormatDDMMYYYY(string date)
    //{
    //    try
    //    {
    //        DateTime dx = UType.ParseYYYYMMDD(date);

    //        return dx.ToString("dd/MM/yyyy");
    //    }
    //    catch
    //    {
    //        return "";
    //    }
    //}


    public static string DateDifference(string pDate)
    {
        int pYear = Convert.ToInt16(pDate.Substring(0, 4));
        int pMonth = Convert.ToInt16(pDate.Substring(4, 2));
        int pDay = Convert.ToInt16(pDate.Substring(6, 2));
        var prevDate = new DateTime(pYear, pMonth, pDay);
        DateTime cDate = DateTime.Now;
        var diffOfDates = cDate.Subtract(prevDate);
        int dDays =  diffOfDates.Days;
        return dDays.ToString();
    }
    public static string FormatMMDDYYYY(DateTime date)
    {
        return date.ToString("MM/dd/yyyy");
    }

    public static string FormatDDMMYYYY(DateTime date)
    {
        return date.ToString("dd/MM/yyyy");
    }
    public static string GetYYYY()
    {

        return DateTime.Now.ToString("yyyy");
    }
    public static string FormatMMDDYYYY(string ddmmyyyy)
    {
        if (!string.IsNullOrEmpty(ddmmyyyy))
        {
            string[] date = ddmmyyyy.Split('/');
            string dd = date[0];
            string mm = date[1];
            string yy = date[2];

            ddmmyyyy = mm + "/" + dd + "/" + yy;
        }

        return ddmmyyyy;
    }

    public static string DateChk(string cDt)
    {
        string retVal = "ok";
        try
        {

            if (cDt.Length > 1)
            {
                if (cDt.Length != 10)
                {
                    retVal = "Date Must be of 10 digits";
                }
                if (string.IsNullOrEmpty(cDt))
                {
                    retVal = "Invlid Date";
                }
                if (!string.IsNullOrEmpty(cDt))
                {
                    string[] date = cDt.Split('-');
                    string dd = date[0];
                    string mm = date[1];
                    string yy = date[2];

                    if (MyCtoD(dd) < 1)
                    {
                        retVal = "Invlid Day";
                    }

                    if (MyCtoD(dd) > 31)
                    {
                        retVal = "Invlid Day";
                    }
                    if (MyCtoD(mm) < 1)
                    {
                        retVal = "Invlid Month";
                    }
                    if (MyCtoD(mm) > 12)
                    {
                        retVal = "Invlid Month";
                    }
                    if (MyCtoD(yy) < 2000)
                    {
                        retVal = "Invlid Year";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //
            retVal = ex.Message.ToString();
            string[] date = cDt.Split('/');
            string dd = date[0];
            string mm = date[1];
            string yy = date[2];

            if (MyCtoD(dd) < 1)
            {
                retVal = "Invlid Day";
            }
            retVal = "ok";
            if (MyCtoD(dd) > 31)
            {
                retVal = "Invlid Day";
            }
            if (MyCtoD(mm) < 1)
            {
                retVal = "Invlid Month";
            }
            if (MyCtoD(mm) > 12)
            {
                retVal = "Invlid Month";
            }
            if (MyCtoD(yy) < 2000)
            {
                retVal = "Invlid Year";
            }
            //
           
        }

        return retVal;
    }
    public static DateTime DDMMYYYYToDateTime(string ddmmyyyy)
    {
        if (!string.IsNullOrEmpty(ddmmyyyy))
        {
            string[] date = ddmmyyyy.Split('/');
            string dd = date[0];
            string mm = date[1];
            string yy = date[2];

            ddmmyyyy = mm + "/" + dd + "/" + yy;
        }

        return DateTime.Parse(ddmmyyyy);
    }

    public static string DateConverter(string pDate)
    {
        if (!string.IsNullOrEmpty(pDate))
        {
            try
            {
                string[] date = pDate.Split('/');
                string dd = date[0];
                string mm = date[1];
                string yy = date[2];
                DateTime dx = new DateTime(int.Parse(yy), int.Parse(mm), int.Parse(dd));
                pDate = dx.ToShortDateString();

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        return pDate;
    }

    public static DateTime ParseYYYYMMDD(string date)
    {
        int y = int.Parse(date.Substring(0, 4));
        int m = int.Parse(date.Substring(4, 2));
        int d = int.Parse(date.Substring(6, 2));

        DateTime dx = new DateTime(y, m, d);

        return dx;
    }

    public static string FormatYYYYMMDD(DateTime date)
    {
        string str = string.Empty;

        str += date.Year.ToString();
        str += date.Month.ToString().PadLeft(2, '0');
        str += date.Day.ToString().PadLeft(2, '0');

        return str;
    }

    public static string FormatHHMMSSFF(DateTime date)
    {
        string str = string.Empty;

        str += date.Hour.ToString();
        str += date.Minute.ToString().PadLeft(2, '0');
        str += date.Second.ToString().PadLeft(2, '0');
        str += date.Millisecond.ToString().PadLeft(2, '0');

        return str.Substring(0, 8);
    }

    #endregion

    public static string Chk1(string InputStr)
    {
        return InputStr.Replace("'", "");
    }
    public static string Chk10(string InputStr)
    {
        //string retVal = InputStr.Replace("'", "");
        //retVal = InputStr.Replace('&', ' ');
        //retVal = InputStr.Replace('*', ' ');
        //retVal = InputStr.Replace("'", "");

        //return retVal;
        string retVal = "";
        foreach (char c in InputStr)
        {
            if (ChkString(c.ToString()) == false)  //if (c == '&' || c == '*' )
            {

                retVal = retVal + c.ToString();
            }

        }
        retVal = retVal.Replace("'", "");
        return retVal;
    }
    public static string Chk10(string InputStr,int inputLen)
    {
        //string retVal = InputStr.Replace("'", "");
        //retVal = InputStr.Replace('&', ' ');
        //retVal = InputStr.Replace('*', ' ');
        //retVal = InputStr.Replace("'", "");

        //return retVal;
        string retVal = "";
        foreach (char c in InputStr)
        {
            if (ChkString(c.ToString()) == false)  //if (c == '&' || c == '*' )
            {

                retVal = retVal + c.ToString();
            }

        }

        retVal = retVal.Replace("'", "");
        try
        {
            retVal = retVal.Substring(0, inputLen);
        }
        catch (Exception ex)
        {
             
        }
        return retVal;
    }
    public static bool ChkString(string Chk1)
    {
        bool retVal = false;
        if (Chk1 == "&")
        {
            retVal = true;
        }
        if (Chk1 == "*")
        {
            retVal = true;
        }
        if (Chk1 == "'")
        {
            retVal = true;
        }


        return retVal;
    }
    public static string RemoveExpense(string InputStr)
    {
        string retVal = InputStr.Replace("Expense", "");
        retVal = retVal.Replace("EXPESE", "");
        retVal = retVal.Replace("EXPENSE", "");

        return retVal;
    }
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

    public static string FileReportGdXsd
    {
        get
        {
            string FolderReport = MapPath("~/Reports/");
            return FolderReport + "MyXml.xsd";
        }
    }

    public static string MapPath(string url)
    {
        return HttpContext.Current.Server.MapPath(url);
    }
    public static bool myIsNumeric(string Fld1)
    {
        bool retVal = false;
        if (MyCtoD(Fld1) > 0)
        {
            retVal = true;
        }
        return retVal;
    }
    public static int MyCtoI(string Fld1)
    {
        int retVal = 0;
        try
        {
            if (Fld1 == "")
            {
                retVal = 0;
            }
            retVal = Convert.ToInt16(Fld1);
        }
        catch (Exception)
        {
            retVal = 0;
        }
        return retVal;
    }
    public static decimal MyCtoD(string Fld1)
    {
        decimal retVal = 0;
        try
        {
            if (Fld1 == "")
            {
                retVal = 0;
            }
            retVal = Convert.ToDecimal(Fld1);
        }
        catch (Exception)
        {
            retVal = 0;
        }
        return retVal;
    }

    public static string MyCtoDs(string Fld1)
    {
        string retVal = "0";
        try
        {
            if (Fld1 == "")
            {
                retVal = "0";
            }
            retVal = Convert.ToString(Convert.ToDecimal(Fld1));
        }
        catch (Exception)
        {
            retVal = "0";
        }
        return retVal;
    }

    public static string GetCFYreport()
    {
        string cYear = DateTime.Now.ToString("yyyy");
        string cYy = DateTime.Now.ToString("yy");
        string cMM = DateTime.Now.ToString("MM");
        if (UType.MyCtoD(cMM) > 6)
        {
            cYear += "-" + (UType.MyCtoD(cYy) + 1);
        }
        else
        {
            cYear = UType.MyCtoD(cYear) - 1 + "-" + cYy;
        }
        return cYear;
    }

    public static string GetPFY()
    {
        string pYear = DateTime.Now.ToString("yyyy");
        string cMM = DateTime.Now.ToString("MM");
        if (UType.MyCtoD(cMM) > 6)
        {
            pYear = UType.MyCtoD(pYear) - 1 + DateTime.Now.ToString("yy");
        }
        else
        {

            pYear = Convert.ToString(UType.MyCtoD(pYear) - 2) + Convert.ToString(UType.MyCtoD(pYear) - 1);
        }
        return pYear;
    }

    public static string GetPFYreport()
    {
        string pYear = DateTime.Now.ToString("yyyy");
        string cMM = DateTime.Now.ToString("MM");
        if (UType.MyCtoD(cMM) > 6)
        {
            pYear = UType.MyCtoD(pYear) - 1 + "-" + DateTime.Now.ToString("yyyy");
        }
        else
        {
            pYear = Convert.ToString(UType.MyCtoD(pYear) - 2) + "-" + Convert.ToString(UType.MyCtoD(pYear) - 1);
        }
        return pYear;
    }
    private static bool myIsNumber(string mval)
    {
        try
        {
            if (mval == string.Empty)
            {
                return false;
            }
            if (mval == "-SELECT-")
            {
                return false;
            }
            if (Convert.ToDecimal(mval) <= 0)
            {
                return false;
            }
            return true;
        }
        catch (Exception Ex)
        {
            return false;
        }
    }

    public static string GetUptoDate()
    {
        string retVal = DateTime.Now.ToString("dd") + "," + UType.GetMonthName(DateTime.Now.ToString("MM")) + " " + DateTime.Now.ToString("yyyy");
        return retVal;
    }
    public static string GetUptoDatePFY()
    {
        string retVal = DateTime.Now.ToString("dd") + "," + UType.GetMonthName(DateTime.Now.ToString("MM")) + " " + Convert.ToString(UType.MyCtoD(DateTime.Now.ToString("yyyy")) - 1);
        return retVal;
    }
    public static string GetMonthName(string dMM)
    {
        string retVal = string.Empty;
        switch (Convert.ToInt16(dMM))
        {
            case 1:
                retVal = "January";
                break;
            case 2:
                retVal = "February";
                break;
            case 3:
                retVal = "March";
                break;
            case 4:
                retVal = "April";
                break;
            case 5:
                retVal = "May";
                break;
            case 6:
                retVal = "June";
                break;
            case 7:
                retVal = "July";
                break;
            case 8:
                retVal = "August";
                break;
            case 9:
                retVal = "September";
                break;
            case 10:
                retVal = "October";
                break;
            case 11:
                retVal = "November";
                break;
            case 12:
                retVal = "December";
                break;

        }
        return retVal;
    }

    public static string MyFormat(string var1)
    {
        double var2 = 0;
        string retVal = string.Empty;
        try
        {
            var2 = Convert.ToDouble(var1);
            retVal = var2.ToString("###,###,###,###,###");
        }
        catch (Exception)
        {
            retVal = "0";
        }
        return retVal;
    }

    public static string MyFormat(decimal var1)
    {
        string var11 = Convert.ToString(var1);
        double var2 = 0;
        string retVal = string.Empty;
        try
        {
            var2 = Convert.ToDouble(var11);
            retVal = var2.ToString("###,###,###,###,###");
        }
        catch (Exception)
        {
            retVal = "0";
        }
        return retVal;
    }

    public static string MyFormat1(string var1)
    {
        double var2 = 0;
        string retVal = string.Empty;
        try
        {
            var2 = Convert.ToDouble(var1);
            retVal = var2.ToString("###,###,###,###,###.000");
        }
        catch (Exception)
        {
            retVal = "0";
        }
        return retVal;
    }
    public static string MyFormat2(string var1)
    {
        double var2 = 0;
        string retVal = string.Empty;
        try
        {
            var2 = Convert.ToDouble(var1);
            retVal = var2.ToString("###,###,###,###,###.00");
        }
        catch (Exception)
        {
            retVal = "0";
        }
        return retVal;
    }
    public static string MyFormat1N(string var1)
    {
        double var2 = 0;
        string retVal = string.Empty;
        try
        {
            var2 = Convert.ToDouble(var1);
            retVal = var2.ToString("###,###,###,###,###.0");
        }
        catch (Exception)
        {
            retVal = "0";
        }
        return retVal;
    }
    public static string MyFormat4(string var1)
    {
        double var2 = 0;
        string retVal = string.Empty;
        try
        {
            var2 = Convert.ToDouble(var1);
            retVal = var2.ToString("000#");
        }
        catch (Exception)
        {
            retVal = "0";
        }
        return retVal;
    }
    public static string MyUnFormat(string InputStr)
    {
        return InputStr.Replace(",", "");
    }

    public static string SetParentClt(string tCltCd)
    {
        string retVal = string.Empty;
        if (tCltCd == "")
        {
            return retVal;
        }
        retVal = tCltCd.Substring(0, 1).ToString() + "CUS";
        if (tCltCd.ToString() == "IDRY")
        {
            retVal = "RCUS";
        }
        return retVal;
    }

    //public static void SetDll(DropDownList pDll, DataSet pDs, string pValueFld, string pTextFld)
    //{
    //    pDll.DataSource = pDs;
    //    pDll.DataValueField = pValueFld;
    //    pDll.DataTextField = pTextFld;
    //    pDll.DataBind();
    //}

    public static void InsertAll(DropDownList ddl1)
    {
        ddl1.Items.Insert(0, "ALL");
    }
    public static void InsertRegion(DropDownList ddl1, string tRoleId)
    {
        if (UType.MyCtoD(tRoleId) < 4)
        {
            ddl1.Items.Insert(1, "North");
            ddl1.Items.Insert(2, "South");
        }
        return;
    }

    public static void InsertSelect(DropDownList ddl1)
    {
        ddl1.Items.Insert(0, "-Select-");
    }

    public static string GetMenuName(string RoleId)
    {
        string retVal = string.Empty;
        decimal dRoleId = UType.MyCtoD(RoleId);

        if (dRoleId > 6)
        {
            retVal = "CltOffice.aspx";
        }
        if (dRoleId > 1 && dRoleId < 7)
        {
            retVal = "MyMenuUsr.aspx";
        }
        if (dRoleId == 1)
        {
            retVal = "MyMenu.aspx";
        }
        return retVal;
    }
    public static string GetUserId(DataSet Ds, string tRoleId)
    {
        string retVal = string.Empty;
        if (tRoleId.Length == 0)
        {
            return retVal;
        }
        foreach (DataRow row in Ds.Tables[0].Rows)
        {
            if (row["RoleId"].ToString().Trim() == tRoleId.Trim())
            {
                retVal = row["UserId"].ToString();
            }
        }
        return retVal;
    }

    public static DataTable MyFilter(DataTable Dt1, string FldVar, string MemVar)
    {
        DataTable nDt1 = Dt1.Copy();
        //nDt1.Rows.Clear();
        foreach (DataRow row1 in nDt1.Rows)
        {
            if (row1[FldVar].ToString() == MemVar)
            {
                //nrow = nDt1.NewRow();
                //row1["ParentClt"] = "I";
            }
            else
            {
                row1.Delete();
            }
            //if (row1["BLKSTS"].ToString() == "TEST" || row1["BLKSTS"].ToString() == "TESP" || row1["BLKSTS"].ToString() == "KPRC" || row1["BLKSTS"].ToString() == "KPRV")
            //{
            //    row1["BLKSTS"] = "Z";
            //}
        }
        //        fCltCode = Filter.FilterTable(fCltCode, "BLKSTS = " + "'" + "I" + "'");
        return nDt1;


    }
    public static string GetBackPageName(string tRoleId)
    {
        string retVal = string.Empty;
        decimal dRoleId = UType.MyCtoD(tRoleId);
        if (dRoleId < 4)
        {
            retVal = "MyMenu.aspx";
        }
        if (dRoleId > 3)
        {
            retVal = "MyMenuUsr.aspx";
        }
        return retVal;
    }

    public static string GetDate(string pDate)
    {
        string retVal = string.Empty;
        try
        {

            if (pDate.Length <= 1)
            {
                retVal = "";
            }
            else
            {
                retVal = pDate.Substring(6, 2).ToString() + "-" + pDate.Substring(4, 2).ToString() + "-" + pDate.Substring(0, 4).ToString();
            }


        }
        catch (Exception)
        {

        }
        return retVal;
    }
    public static string GetDate1(string pDate)
    {
        string retVal = string.Empty;
        if (pDate.Length <= 1 || pDate == "0")
        {
            retVal = "";
        }
        else
        {
            retVal = pDate.Substring(6, 2).ToString() + "/" + pDate.Substring(4, 2).ToString() + "/" + pDate.Substring(0, 4).ToString();
        }
        return retVal;
    }
    public static string GetDateTxt(string pDate)
    {
        string retVal = string.Empty;
        try
        {

            if (pDate.Length <= 1)
            {
                retVal = "";
            }
            else
            {
                retVal = pDate.Substring(6, 2).ToString() + "-" + GetMonthText(pDate.Substring(4, 2).ToString()) + "-" + pDate.Substring(0, 4).ToString();
            }


        }
        catch (Exception)
        {

        }
        return retVal;
    }
    public static string GetDateYear(string pDate)
    {
        string retVal = string.Empty;
        try
        {

            if (pDate.Length <= 1)
            {
                retVal = "";
            }
            else
            {
                retVal = pDate.Substring(2, 2).ToString() ;
                //retVal = pDate.Substring(6, 2).ToString() + "-" + GetMonthText(pDate.Substring(4, 2).ToString()) + "-" + pDate.Substring(0, 4).ToString();
            }


        }
        catch (Exception)
        {

        }
        return retVal;
    }
    public static string GetMonthText(string pMonth)
    {
        string retVal = "";
        switch (Convert.ToString(UType.MyCtoD(pMonth)))
        {
            case "1":
                retVal = "JAN";
                break;
            case "2":
                retVal = "FEB";
                break;
            case "3":
                retVal = "MAR";
                break;
            case "4":
                retVal = "APR";
                break;
            case "5":
                retVal = "MAY";
                break;
            case "6":
                retVal = "JUN";
                break;
            case "7":
                retVal = "JUL";
                break;
            case "8":
                retVal = "AUG";
                break;
            case "9":
                retVal = "SEP";
                break;
            case "10":
                retVal = "OCT";
                break;
            case "11":
                retVal = "NOV";
                break;
            case "12":
                retVal = "DEC";
                break;
        }
        return retVal;

    }
    public static string GetMonthNumber(string pMonth)
    {
        string retVal = "";
        switch (pMonth)
        {
            case "JAN":
                retVal = "01";
                break;
            case "FEB":
                retVal = "02";
                break;
            case "MAR":
                retVal = "03";
                break;
            case "APR":
                retVal = "04";
                break;
            case "MAY":
                retVal = "05";
                break;
            case "JUN":
                retVal = "06";
                break;
            case "JUL":
                retVal = "07";
                break;
            case "AUG":
                retVal = "08";
                break;
            case "SEP":
                retVal = "09";
                break;
            case "OCT":
                retVal = "10";
                break;
            case "NOV":
                retVal = "11";
                break;
            case "DEC":
                retVal = "12";
                break;
        }
        return retVal;

    }
    public static string FileReportXsd
    {
        get
        {
            string FolderReport = MapPath("~/Reports/");
            return FolderReport + "MyXml.xsd";
        }
    }
    public static string FileReportXsd1
    {
        get
        {
            string FolderReport = MapPath("~/Reports/");
            return FolderReport + "MyXml1.xsd";
        }
    }

    public static DataTable ToTable2(string tableName, params string[] columns)
    {
        DataSet ds = new DataSet();

        DataTable table = ds.Tables.Add(tableName);

        for (int i = 0; i < columns.Length; i++)
        {
            string name = columns.GetValue(i) as string;

            Type type = System.Type.GetType("System.String");

            table.Columns.Add(new DataColumn(name, type));
        }

        return table;
    }
    public static string SetDate(string pDate)
    {
        string retVal = "0";
        try
        {
            if (pDate.Length > 1)
            {
                retVal = pDate.Substring(6, 4).ToString() + pDate.Substring(3, 2).ToString() + pDate.Substring(0, 2).ToString();
            }
        }
        catch (Exception ex)
        {


        }
        return retVal;
    }
    public static string SetTime(string pDate)
    {
        string retVal = "0";
        if (pDate.Length > 1)
        {
            retVal = pDate.Substring(0, 2).ToString() + pDate.Substring(3, 2).ToString() + pDate.Substring(6, 2).ToString();
        }
        return retVal;
    }

    public static decimal SetDateD(string pDate)
    {
        string retVal = "0";
        if (pDate.Length > 0)
        {
            retVal = pDate.Substring(6, 4).ToString() + pDate.Substring(3, 2).ToString() + pDate.Substring(0, 2).ToString();
        }
        return Convert.ToDecimal(retVal);
    }

    public static string MoveDdlNumValue(string pDdl)
    {
        string retVal = string.Empty;
        if (pDdl.Length == 0)
        { retVal = "0"; }
        retVal = pDdl;
        if (pDdl == "-SELECT-")
        { retVal = "0"; }

        return retVal;
    }
    public static string GetActLevel(string pActCode)
    {
        decimal lActCode = pActCode.Length;
        string retVal = string.Empty;
        if (lActCode < 11)
        {
            retVal = "4";
        }
        if (lActCode < 7)
        {
            retVal = "3";
        }
        if (lActCode < 4)
        {
            retVal = "2";
        }
        if (lActCode < 2)
        {
            retVal = "1";
        }

        return retVal;
    }
    public static string GetParentActCode(DataSet ds1, string pActCode)
    {
        decimal lActCode = pActCode.Length;
        Int16 lParentActCode = 0;
        string retVal = string.Empty;
        foreach (DataRow row1 in ds1.Tables[0].Rows)
        {
            if (MyCtoD(row1["ActLevelDigit"].ToString()) == lActCode)
            {
                lParentActCode = Convert.ToInt16(row1["ActLevelParentDigit"].ToString());
            }
        }
        retVal = Convert.ToString(pActCode.Substring(0, lParentActCode));


        return retVal;
    }
    public static bool CheckActCode(DataSet ds1, string pActCode)
    {
        bool retVal = false;
        foreach (DataRow row1 in ds1.Tables[0].Rows)
        {
            if (row1["ActCode"].ToString() == pActCode)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    public static DropDownList SetDll(DropDownList pDll, DataSet pDs, string pValueFld, string pTextFld)
    {
        pDll.DataSource = pDs;
        pDll.DataValueField = pValueFld;
        pDll.DataTextField = pTextFld;
        pDll.DataBind();
        //InsertBlank(pDll);
        return pDll;

    }
    public static DropDownList SetDll1(DropDownList pDll, DataSet pDs, string pValueFld, string pTextFld)
    {
        pDll.DataSource = pDs;
        pDll.DataValueField = pValueFld;
        pDll.DataTextField = pTextFld;
        pDll.DataBind();
        pDll = InsertBlank(pDll);
        return pDll;

    }

    public static DropDownList InsertBlank(DropDownList ddl1)
    {
        ddl1.Items.Insert(0, "-SELECT-");
        return ddl1;
    }

    public static string GetCFY()
    {
        string cYear = DateTime.Now.ToString("yyyy");
        string cMM = DateTime.Now.ToString("MM");
        if (UType.MyCtoD(cMM) > 6)
        {
            cYear += (UType.MyCtoD(cYear) + 1);
        }
        else
        {
            cYear = UType.MyCtoD(cYear) - 1 + DateTime.Now.ToString("yy");
        }
        return cYear;
    }
    public static string GetCFYOne()
    {
        string cYear = DateTime.Now.ToString("yyyy");
        string cMM = DateTime.Now.ToString("MM");
        if (UType.MyCtoD(cMM) > 6)
        {
            cYear = Convert.ToString(UType.MyCtoD(cYear) + 1);
        }
        
        return cYear;
    }
    public static string GetCFYStart()
    {
        string retVal = GetCFYOne();
        return "01/07/" + retVal;
    }
    public static string GetCDate()
    {
        string cYear = DateTime.Now.ToString("dd/MM/yyyy");
        return cYear;
    }
    public static string GetCY()
    {
        string cYear = DateTime.Now.ToString("yyyy");
        return cYear;
    }
    public static string GetActName(DataTable dt, string pActCode)
    {
        string retVal = string.Empty;
        foreach (DataRow row in dt.Rows)
        {
            if (UType.MyCtoD(row["Actcode"].ToString()) == UType.MyCtoD(pActCode))
            {
                retVal = row["ActDesc"].ToString();
            }
        }
        return retVal;
    }

    public static string GetUnitRate(DataTable dt, string pActCode)
    {
        string retVal = string.Empty;
        foreach (DataRow row in dt.Rows)
        {
            if (UType.MyCtoD(row["Actcode"].ToString()) == UType.MyCtoD(pActCode))
            {
                retVal = row["UnitRate"].ToString();
            }
        }
        return retVal;
    }

    public static string GetOfficeName(DataTable dt, string pOfficeId)
    {
        string retVal = string.Empty;
        foreach (DataRow row in dt.Rows)
        {
            if (UType.MyCtoD(row["OfficeId"].ToString()) == UType.MyCtoD(pOfficeId))
            {
                retVal = row["OfficeDescription"].ToString();
            }
        }
        return retVal;
    }
    public static string GetOfficeName(DataSet ds, string pOfficeId)
    {
        string retVal = string.Empty;
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            if (UType.MyCtoD(row["OfficeId"].ToString()) == UType.MyCtoD(pOfficeId))
            {
                retVal = row["OfficeDescription"].ToString();
            }
        }
        return retVal;
    }


    public static string GetProjectName(DataTable dt, string pProjectId)
    {
        string retVal = string.Empty;
        foreach (DataRow row in dt.Rows)
        {
            if (UType.MyCtoD(row["ProjectId"].ToString()) == UType.MyCtoD(pProjectId))
            {
                retVal = row["ProjectDescription"].ToString();
            }
        }
        return retVal;
    }
    public static string GetProjectName(DataSet ds, string pProjectId)
    {
        string retVal = string.Empty;
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            if (UType.MyCtoD(row["ProjectId"].ToString()) == UType.MyCtoD(pProjectId))
            {
                retVal = row["ProjectDescription"].ToString();
            }
        }
        return retVal;
    }
    public static string GetDiffDays(string sDate, string eDate)
    {
        decimal retVal = 0;
        retVal = MyCtoD(UType.SetDate(eDate)) - MyCtoD(UType.SetDate(sDate));
        return retVal.ToString();
    }
    public static string GetDiffDays1(string sDate, string eDate)
    {
        decimal retVal = 0;
        retVal = MyCtoD(eDate) - MyCtoD(sDate);
        return retVal.ToString();
    }

    public static bool CheckDdl(DropDownList ddl1)
    {
        bool retVal = false;
        if (UType.MyCtoD(ddl1.SelectedValue.ToString()) > 0)
        {
            retVal = true;
        }
        return retVal;
    }
    public static string GetDllValue(DropDownList ddl1)
    {
        string retVal = "0";
        if (UType.MyCtoD(ddl1.SelectedValue.ToString()) > 0)
        {
            retVal = ddl1.SelectedValue.ToString();
        }
        return retVal;
    }

    public static string GetLeaveType(DataTable dt, string pLeaveId)
    {
        string retVal = string.Empty;
        foreach (DataRow row in dt.Rows)
        {
            if (row["LeaveId"].ToString() == pLeaveId)
            {
                retVal = row["LeaveDescription"].ToString();
            }
        }
        return retVal;
    }
    public static string GetcBoxValue(CheckBox cBox)
    {
        string retVal = "0";
        if (cBox.Checked)
        {
            retVal = "1";
        }
        return retVal;
    }
    public static string GetcRdoValue(RadioButton cBox)
    {
        string retVal = "0";
        if (cBox.Checked)
        {
            retVal = "1";
        }
        return retVal;
    }
    public static DataSet SetRptDataset()
    {
        DataSet ds1 = new DataSet();
        ds1.ReadXmlSchema(UType.FileReportXsd);
        return ds1;
    }
    public static DataSet SetRptDataset1()
    {
        DataSet ds1 = new DataSet();
        ds1.ReadXmlSchema(UType.FileReportXsd1);
        return ds1;
    }

    public static bool IsSql()
    {
        bool retVal = Convert.ToBoolean(MyConfig.GetKey("IsSql", "").ToString());
        return retVal;
    }
    public static bool IsWeb()
    {
        bool retVal = Convert.ToBoolean(MyConfig.GetKey("IsWeb", "").ToString());
        return retVal;
    }

    public static bool IsWebImage()
    {
        bool retVal = Convert.ToBoolean(MyConfig.GetKey("IsWebImg", "").ToString());
        return retVal;
    }

    public static void SetDll2(DropDownList pDll, DataSet pDs, string pValueFld, string pTextFld)
    {
        pDll.DataSource = pDs;
        pDll.DataValueField = pValueFld;
        pDll.DataTextField = pTextFld;
        pDll.DataBind();
    }

    public static void SetDll2(DropDownList pDll, DataTable pDs, string pValueFld, string pTextFld)
    {
        pDll.DataSource = pDs;
        pDll.DataValueField = pValueFld;
        pDll.DataTextField = pTextFld;
        pDll.DataBind();
        //InsertBlank(pDll); 
    }

    public static void DoSendEmail(string Sts, string pBody, string pSubject, string pFromEmailId, string pFromPassword, string pToEmailId)
    {
        string str = "";
        Boolean state = false;
        if (Sts.ToLower() == "yahoo")
        {
            str = "smtp.mail.yahoo.com";
            state = false;
        }
        if (Sts.ToLower() == "gmail")
        {
            str = "smtp.gmail.com";
            state = true;
        }
        //state = false;
        System.Net.Mail.MailAddress From = new System.Net.Mail.MailAddress(pFromEmailId);
        System.Net.Mail.MailAddress To = new System.Net.Mail.MailAddress(pToEmailId);
        System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage(From, To);
        Message.Subject = pSubject;
        Message.Body = pBody;
        //Message.Attachments.Add(new System.Net.Mail.Attachment("C:\\qry.txt"));
        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(str);
        client.EnableSsl = Convert.ToBoolean(state);
        client.Credentials = new System.Net.NetworkCredential(pFromEmailId, pFromPassword);

        client.Send(Message);
        //MessageBox.Show("Send Email Successfully");      
    }

    public static void DoSendEmail(string Sts, string pBody, string pSubject, string pFromEmailId, string pFromPassword, string pToEmailId, string pAttach)
    {
        string str = "";
        Boolean state = false;
        if (Sts.ToLower() == "yahoo")
        {
            str = "smtp.mail.yahoo.com";
            state = false;
        }
        if (Sts.ToLower() == "gmail")
        {
            str = "smtp.gmail.com";
            state = true;
        }
        state = false;
        //pToEmailId = "prlarshad@yahoo.com";
        System.Net.Mail.MailAddress From = new System.Net.Mail.MailAddress(pFromEmailId);
        System.Net.Mail.MailAddress To = new System.Net.Mail.MailAddress(pToEmailId);
        System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage(From, To);
        Message.Subject = pSubject;
        //
        //string str1 = "<html><body><h1>Picture</h1><br/><imgsrc=\"cid:image1\"></body></html>";
        string str1 = "<html><body><h1></h1><br/><imgsrc=\"cid:image1\"></body></html>";
        AlternateView av1 = AlternateView.CreateAlternateViewFromString(str1, null, System.Net.Mime.MediaTypeNames.Text.Html);
        //LinkedResource lr1 = new LinkedResource("C:\\PakistanTaxes\\DocsOffline\\PT.jpg", System.Net.Mime.MediaTypeNames.Image.Jpeg);
        LinkedResource lr1 = new LinkedResource(pAttach, System.Net.Mime.MediaTypeNames.Image.Jpeg);
        lr1.ContentId = "image1";
        av1.LinkedResources.Add(lr1);
        Message.AlternateViews.Add(av1);

        //AlternateView av2 = AlternateView.CreateAlternateViewFromString(str1, null, System.Net.Mime.MediaTypeNames.Text.Html);
        //LinkedResource lr2 = new LinkedResource("C:\\PakistanTaxes\\DocsOffline\\company.jpg", System.Net.Mime.MediaTypeNames.Image.Jpeg);
        //lr1.ContentId = "image1";
        //av2.LinkedResources.Add(lr2);
        //Message.AlternateViews.Add(av2);

        //
        //11111
        //Message.Body = pBody;
        //Message.Body = "<img src='C:\\Namaz.jpg'/>";                      
        //Message.IsBodyHtml = true;     
        //Message.Attachments.Add(new System.Net.Mail.Attachment(pAttach1));
        Message.Attachments.Add(new System.Net.Mail.Attachment(pAttach));

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(str);
        client.EnableSsl = Convert.ToBoolean(state);
        client.Credentials = new System.Net.NetworkCredential(pFromEmailId, pFromPassword);

        client.Send(Message);

        //MessageBox.Show("Send Email Successfully");      
    }

    public static void DoSendEmailtest(string Sts, string pBody, string pSubject, string pFromEmailId, string pFromPassword, string pToEmailId)
    {
        string str = "";
        Boolean state = false;
        str = "mail.fbrmail.com";
        state = false;
        //ilMessage Email = new MailMessage("pral@fbrmail.com", pEmailId);
        state = false;
        System.Net.Mail.MailAddress From = new System.Net.Mail.MailAddress("pral@fbrmail.com");
        System.Net.Mail.MailAddress To = new System.Net.Mail.MailAddress(pToEmailId);
        System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage("pral@fbrmail.com", pToEmailId);
        Message.Subject = pSubject;
        Message.Body = pBody;
        Message.Attachments.Add(new System.Net.Mail.Attachment("C:\\qry.txt"));
        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(str);
        client.EnableSsl = Convert.ToBoolean(state);
        client.Credentials = new System.Net.NetworkCredential("pral@fbrmail.com", "omega%67");
        client.Send(Message);
        //MessageBox.Show("Send Email Successfully");      
    }

    public static void DoSendEmailFbr(string pHdr, string pSubject, string pEmailId)
    {
        string TxtBody = pSubject;
        string TxtSubject = pHdr;

        //MailMessage Email = new MailMessage("customs@fbr.gov.pk", "prlyousuf@gmail.com");
        //MailMessage Email = new MailMessage("customs@fbr.gov.pk", pEmailId); close on 20120730
        //MailMessage Email = new MailMessage("pral@fbrmail.com", pEmailId); close on 20130213
        MailMessage Email = new MailMessage("customs@fbr.gov.pk", pEmailId);

        SmtpClient mailClient = new SmtpClient();
        //NetworkCredential basicAuthenticationInfo = new NetworkCredential("customs@fbr.gov.pk", "smotsuc"); //close on 20120730
        //NetworkCredential basicAuthenticationInfo = new NetworkCredential("pral@fbrmail.com", "omega%67"); // close on 20130213
        NetworkCredential basicAuthenticationInfo = new NetworkCredential("customs@fbr.gov.pk", "smotsuc7100");

        mailClient.Host = "mail.fbr.gov.pk";
        //mailClient.Host = "mail.fbrmail.com";
        mailClient.UseDefaultCredentials = false;
        //mailClient.Port = 25;
        mailClient.Port = 587;
        mailClient.Credentials = basicAuthenticationInfo;


        Email.Priority = MailPriority.High;
        Email.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        Email.Subject = TxtSubject; // "test";

        Email.Body = TxtBody;       // "hello";

        try
        {
            mailClient.Send(Email);
        }
        catch (Exception Ex)
        {

            string aaa = Ex.Message.ToString();
        }
    }

    #region Old mail
    //public static void DoSendEmailFbr(string pHdr, string pSubject, string pEmailId)
    //{
    //    string TxtBody = pSubject;
    //    string TxtSubject = pHdr;

    //    //MailMessage Email = new MailMessage("customs@fbr.gov.pk", "prlyousuf@gmail.com");
    //    //MailMessage Email = new MailMessage("customs@fbr.gov.pk", pEmailId); close on 20120730
    //    MailMessage Email = new MailMessage("pral@fbrmail.com", pEmailId);

    //    SmtpClient mailClient = new SmtpClient();
    //    //NetworkCredential basicAuthenticationInfo = new NetworkCredential("customs@fbr.gov.pk", "smotsuc"); //close on 20120730
    //    NetworkCredential basicAuthenticationInfo = new NetworkCredential("pral@fbrmail.com", "omega%67");

    //    //mailClient.Host = "mail.fbr.gov.pk";
    //    mailClient.Host = "mail.fbrmail.com";
    //    mailClient.UseDefaultCredentials = false;
    //    mailClient.Port = 25;
    //    mailClient.Credentials = basicAuthenticationInfo;
    //    Email.Priority = MailPriority.High;
    //    Email.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
    //    Email.Subject = TxtSubject; // "test";

    //    Email.Body = TxtBody;       // "hello";

    //    try
    //    {
    //        mailClient.Send(Email);
    //    }
    //    catch (Exception Ex)
    //    {

    //        string aaa = Ex.Message.ToString();
    //    }
    //} 
    #endregion

    public static DropDownList InsertMonthName(DropDownList ddl1)
    {
        ddl1.Items.Add(new ListItem("January", "1"));
        ddl1.Items.Add(new ListItem("February", "2"));
        ddl1.Items.Add(new ListItem("March", "3"));
        ddl1.Items.Add(new ListItem("April", "4"));
        ddl1.Items.Add(new ListItem("May", "5"));
        ddl1.Items.Add(new ListItem("June", "6"));
        ddl1.Items.Add(new ListItem("July", "7"));
        ddl1.Items.Add(new ListItem("August", "8"));
        ddl1.Items.Add(new ListItem("September", "9"));
        ddl1.Items.Add(new ListItem("October", "10"));
        ddl1.Items.Add(new ListItem("November", "11"));
        ddl1.Items.Add(new ListItem("December", "12"));
        return ddl1;
    }

    public static DropDownList InsertYear(DropDownList ddl1)
    {
        string cYear = DateTime.Now.ToString("yyyy");
        ddl1.Items.Add(new ListItem(cYear, cYear));
        cYear = Convert.ToString(UType.MyCtoD(cYear) - 1);
        ddl1.Items.Add(new ListItem(cYear, cYear));
        return ddl1;
    }
    public static string GetEndDate(string pDate, string AddDays)
    {
        int pYear = Convert.ToInt16(pDate.Substring(0, 4));
        int pMonth = Convert.ToInt16(pDate.Substring(4, 2));
        int pDay = Convert.ToInt16(pDate.Substring(6, 2));
        var CurDate = new DateTime(pYear, pMonth, pDay);
       // DateTime cDate = DateTime.Now;
        var NewDate = CurDate.AddDays(Convert.ToDouble(AddDays));
        string retVal= UType.GetDateTxt(Convert.ToString(UType.SetDate(NewDate.ToString())));
        return retVal;
    }
    public static string GetEndDateNew(string pDate, string AddDays)
    {
        
        int pDay = Convert.ToInt16(pDate.Substring(0, 2));
        string pMonthc = pDate.Substring(3, 3);
        pMonthc = UType.GetMonthNumber(pMonthc);

        int pMonth = Convert.ToInt16(pMonthc);
        int pYear = Convert.ToInt16(pDate.Substring(7, 4));
        
        var CurDate = new DateTime(pYear, pMonth, pDay);
        // DateTime cDate = DateTime.Now;
        var NewDate = CurDate.AddDays(Convert.ToDouble(AddDays));
        string NewDate1 = NewDate.ToString("yyyyMMdd");
        //string retVal = UType.GetDateTxt(Convert.ToString(UType.SetDate(NewDate.ToString())));
        string retVal = UType.GetDateTxt(NewDate1);
        return retVal;
    }
    public static string GetEndDate1(string pDate, string AddDays)
    {
        int pYear = Convert.ToInt16(pDate.Substring(0, 4));
        int pMonth = Convert.ToInt16(pDate.Substring(4, 2));
        int pDay = Convert.ToInt16(pDate.Substring(6, 2));
        if (pMonth > 12)
        {
            pMonth = Convert.ToInt16(pDate.Substring(6, 2));
            pDay = Convert.ToInt16(pDate.Substring(4, 2));
        }
        var CurDate = new DateTime(pYear, pMonth, pDay);
        // DateTime cDate = DateTime.Now;
        var NewDate = CurDate.AddDays(Convert.ToDouble(AddDays));
        //string retVal =UType.SetDate(NewDate.ToString());
        string retVal = NewDate.ToString("yyyyMMdd");
        return retVal;
    }

    public static int GetDay(string pDate)
    {
        int retVal = 0;
        if (pDate.Length > 0)
        {
            retVal = Convert.ToInt16(pDate.Substring(6, 2).ToString());
        }
        return retVal;
    }

    public static string GetUomDescription(DataTable dt, string pUomId)
    {
        string retVal = string.Empty;
        foreach (DataRow row in dt.Rows)
        {
            if (UType.MyCtoD(row["UomId"].ToString()) == UType.MyCtoD(pUomId))
            {
                retVal = row["UomDescription"].ToString();
            }
        }
        return retVal;
    }
    public static string GetPinCode1()
    {
        int retVal = 0;
        Random random = new Random();
        retVal = random.Next(101, 499);
        return retVal.ToString();
    }
    public static string GetPinCode2()
    {
        int retVal = 0;
        Random random = new Random();
        retVal = random.Next(500, 999);
        return retVal.ToString();
    }
    public static DropDownList SetBillYear(DropDownList Ddl1)
    {
        Ddl1.Items.Insert(0, DateTime.Now.ToString("yyyy"));
        Ddl1.Items.Insert(1, Convert.ToString(UType.MyCtoD(DateTime.Now.ToString("yyyy")) - 1));
        return Ddl1;
    }
    public static string PurchaseCode()
    {
        string retVal = "1005";
        return retVal;
    }
    public static string SaleCode()
    {
        string retVal = "3005";
        return retVal;
    }
    public static bool IsPurchaseCode(string Inp1)
    {
        bool retVal = false;
        if (Inp1 == PurchaseCode())
        {
            retVal = true;
        }
        return retVal;
    }

    public static string NumberToWords(decimal num)
    {
        string retVal = NumberToWords(Convert.ToInt64(num));
        


        return retVal;
    }
    public static string NumberToWords(long num)
    {
         
        string retVal = "";  
       string[] dessert = new string[] { "Cupcake", "Cake", "Candy" };

        string[] oneToNineteen = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        string[] tens = new string[]  { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        if (num >= 1 && num <= 19)
        {
              
            retVal = oneToNineteen[num];
        }
        else if (num >= 20 && num <= 99)
        {
              
            retVal = tens[num / 10] + (num % 10 != 0 ? " " + oneToNineteen[num % 10] : "");
        }
        else if (num >= 100 && num <= 999)
        {
            
            retVal = oneToNineteen[num / 100] + " Hundred" + (num % 100 != 0 ? " and " + NumberToWords(num % 100) : "");
        }
        else if (num >= 1000 && num <= 99999)
        {
           
             retVal = NumberToWords(num / 1000) + " Thousand" + (num % 1000 != 0 ? " " + NumberToWords(num % 1000) : "");
        }
        else if (num >= 100000 && num <= 9999999)
        {

            retVal = NumberToWords(num / 100000) + " Lakh" + (num % 100000 != 0 ? " " + NumberToWords(num % 100000) : "");
        }
        else if (num >= 10000000 && num <= 999999999)
        {
             
           retVal = NumberToWords(num / 10000000) + " Crore" + (num % 10000000 != 0 ? " " + NumberToWords(num % 10000000) : "");
        }


        return retVal;
    }
    public static string GetTwo(decimal number)
    {
        string words = "";

        if (Convert.ToString(number) == "11")
        {
            words = "Eleven";
        }
        if (Convert.ToString(number) == "12")
        {
            words = "Twelve";
        }
        if (Convert.ToString(number) == "13")
        {
            words = "Thirteen";
        }
        if (number > 13)
        {
            words = Get1s(Convert.ToString(number).Substring(0, 1));
            words = words + "teen";
        }

        return words;
    }

    public static string Get1s(string number)
    {
        string No0 = " Zero ";
        string retVal = "";
        string No1 = " One ";
        string No2 = " Two ";
        string No3 = " Three ";
        string No4 = " Four ";
        string No5 = " Five ";
        string No6 = " Six ";
        string No7 = " Seven ";
        string No8 = " Eight ";
        string No9 = " Nine ";
        string No10 = " Ten ";
        string No11 = " Eleven ";
        string No12 = " Twelve ";

        switch (number)
        {
            case "0":
                retVal = No0;
                break;
            case "1":
                retVal = No1;
                break;
            case "2":
                retVal = No2;
                break;
            case "3":
                retVal = No3;
                break;
            case "4":
                retVal = No4;
                break;
            case "5":
                retVal = No5;
                break;
            case "6":
                retVal = No6;
                break;
            case "7":
                retVal = No7;
                break;
            case "8":
                retVal = No8;
                break;
            case "9":
                retVal = No9;
                break;
        }
        return retVal;
    }
    public static string Get10s(string number)
    {
        string retVal = "";
        string No1 = " Ten ";
        string No2 = " Twenty ";
        string No3 = " Thirty ";
        string No4 = " Fourty ";
        string No5 = " Fifty ";
        string No6 = " Sixty ";
        string No7 = " Seventy ";
        string No8 = " Eighty ";
        string No9 = " Ninety ";

        switch (number)
        {
            case "10":
                retVal = No1;
                break;
            case "20":
                retVal = No2;
                break;
            case "30":
                retVal = No3;
                break;
            case "40":
                retVal = No4;
                break;
            case "50":
                retVal = No5;
                break;
            case "60":
                retVal = No6;
                break;
            case "70":
                retVal = No7;
                break;
            case "80":
                retVal = No8;
                break;
            case "90":
                retVal = No9;
                break;
        }
        return retVal;
    }

    public static string NumberToWordsUrdu(decimal number)
    {
        string retVal = "";
        string H1 = " Hundred ";
        string t1 = " Thousand ";
        string m1 = " Million ";
        string b1 = " Billion ";
        string L1 = " Lakh ";
        string C1 = " Crore ";
        string a1 = " Arab ";
        string An1 = " and ";

        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string wordsUrdu = "";

        switch (Convert.ToString(number).Length)
        {
            case 4:
                wordsUrdu = Convert.ToString(number).Substring(0, 1) + t1;
                break;
            case 5:
                wordsUrdu = Convert.ToString(number).Substring(0, 2) + t1;
                break;
            case 6:
                wordsUrdu = Convert.ToString(number).Substring(0, 1) + L1;
                if (UType.MyCtoD(Convert.ToString(number).Substring(1, 2)) > 0)
                {
                    wordsUrdu += An1 + Convert.ToString(number).Substring(1, 2) + t1;
                }
                break;
            case 7:
                wordsUrdu = Convert.ToString(number).Substring(0, 2) + L1;
                if (UType.MyCtoD(Convert.ToString(number).Substring(2, 2)) > 0)
                {
                    wordsUrdu += An1 + Convert.ToString(number).Substring(2, 2) + t1;
                }
                break;
            case 8:

                wordsUrdu = Convert.ToString(number).Substring(0, 1) + C1;
                if (UType.MyCtoD(Convert.ToString(number).Substring(1, 2)) > 0)
                {
                    wordsUrdu += Convert.ToString(number).Substring(1, 2) + L1;
                }
                if (UType.MyCtoD(Convert.ToString(number).Substring(3, 2)) > 0)
                {
                    wordsUrdu += An1 + Convert.ToString(number).Substring(3, 2) + t1;
                }
                break;
            case 9:
                wordsUrdu = Convert.ToString(number).Substring(0, 2) + C1;
                if (UType.MyCtoD(Convert.ToString(number).Substring(2, 2)) > 0)
                {
                    wordsUrdu += Convert.ToString(number).Substring(2, 2) + L1;
                }
                if (UType.MyCtoD(Convert.ToString(number).Substring(4, 2)) > 0)
                {
                    wordsUrdu += An1 + Convert.ToString(number).Substring(4, 2) + t1;
                }
                break;
            case 10:
                wordsUrdu = Convert.ToString(number).Substring(0, 1) + a1;
                wordsUrdu += Convert.ToString(number).Substring(1, 2) + C1;
                wordsUrdu += Convert.ToString(number).Substring(3, 2) + L1;
                wordsUrdu += An1 + Convert.ToString(number).Substring(5, 2) + t1;
                break;
            case 11:
                wordsUrdu = Convert.ToString(number).Substring(0, 2) + a1;
                wordsUrdu += Convert.ToString(number).Substring(2, 2) + C1;
                wordsUrdu += Convert.ToString(number).Substring(4, 2) + L1;
                wordsUrdu += An1 + Convert.ToString(number).Substring(6, 2) + t1;
                break;

        }

        retVal = wordsUrdu;
        return retVal;
    }

    public static string NumberToWordsBk(decimal number)
    {
        string retVal = "";
        decimal tNumber = number;
        decimal NumberLen = Convert.ToString(tNumber).Length;
        string H1 = " Hundred ";
        string t1 = " Thousand ";
        string m1 = " Million ";
        string b1 = " Billion ";
        string L1 = " Lakh ";
        string C1 = " Crore ";
        string a1 = " Arab ";
        string An1 = " and ";
        string words = "";
        int PickFromNumner = 0;
        if (number < 0)
        { words = "minus"; }

        if (NumberLen >= 10 && NumberLen <= 11)
        {
            if (NumberLen == 10)
            {
                PickFromNumner = 1;
                words = Get1s(Convert.ToString(tNumber).Substring(0, PickFromNumner)) + b1;
            }
            if (NumberLen == 11)
            {
                PickFromNumner = 2;
                words = Get10s(Convert.ToString(tNumber).Substring(0, PickFromNumner)) + b1;
            }
            tNumber = Convert.ToDecimal(Convert.ToString(tNumber).Substring(PickFromNumner, 9));
            NumberLen = Convert.ToString(tNumber).Length;
        }
        if (NumberLen >= 7 && NumberLen <= 9)
        {
            if (NumberLen == 7)
            {
                PickFromNumner = 1;
                words = words + Convert.ToString(tNumber).Substring(0, PickFromNumner) + m1;
            }

            if (NumberLen == 8)
            {
                PickFromNumner = 2;
                words = words + Convert.ToString(tNumber).Substring(0, PickFromNumner) + m1;
            }
            if (NumberLen == 9)
            {
                PickFromNumner = 3;
                words = words + Convert.ToString(tNumber).Substring(0, PickFromNumner) + m1;
            }
            tNumber = Convert.ToDecimal(Convert.ToString(tNumber).Substring(PickFromNumner, 6));
            NumberLen = Convert.ToString(tNumber).Length;
        }
        if (NumberLen >= 4 && NumberLen <= 6)
        {
            if (NumberLen == 4)
            {
                PickFromNumner = 1;
                words = words + Convert.ToString(tNumber).Substring(0, PickFromNumner) + t1;
            }
            if (NumberLen == 5)
            {
                PickFromNumner = 2;
                words = words + Convert.ToString(tNumber).Substring(0, PickFromNumner) + t1;
            }
            if (NumberLen == 6)
            {
                PickFromNumner = 3;
                words = words + Convert.ToString(tNumber).Substring(0, PickFromNumner) + t1;
            }
            tNumber = Convert.ToDecimal(Convert.ToString(tNumber).Substring(PickFromNumner, 3));
            NumberLen = Convert.ToString(tNumber).Length;
        }
        if (NumberLen >= 1 && NumberLen <= 3)
        {
            if (NumberLen == 1)
            {
                PickFromNumner = 1;
                words = words + Convert.ToString(tNumber).Substring(0, PickFromNumner) + H1;
            }
            if (NumberLen == 2)
            {
                PickFromNumner = 2;
                words = words + Convert.ToString(tNumber).Substring(0, PickFromNumner) + H1;
            }
            if (NumberLen == 6)
            {
                PickFromNumner = 3;
                words = words + Convert.ToString(tNumber).Substring(0, PickFromNumner) + H1;
            }
            tNumber = Convert.ToDecimal(Convert.ToString(tNumber).Substring(PickFromNumner, 3));
            NumberLen = Convert.ToString(tNumber).Length;
        }



        retVal = words;
        return retVal;
    }
    public static string numb1(string Snumber)
    {

        Int64 number = Convert.ToInt64(Snumber);
        if (number == 0)
            return "Zero";

        string words = "";

        for (int i = 0; number > 0; i++)
        {
            if (number % 1000 != 0)
                words = ConvertHundreds(number % 1000) + Thousands[i] + " " + words;

            number /= 1000;
        }

        return words.Trim();
    }

    private static string ConvertHundreds(Int64 number)
    {

        string words = "";

        if (number >= 100)
        {
            words += Ones[number / 100] + " Hundred ";
            number %= 100;
        }

        if (number >= 10 && number <= 19)
        {
            words += Teens[number % 10] + " ";
        }
        else
        {
            words += Tens[number / 10] + " ";
            words += Ones[number % 10] + " ";
        }

        return words;
    }

    public static string GetAccountTypeCode(string cOffice, string cProject, string cType)
    {
        string retVal = "";
        // if (cOffice == "1" && cProject == "1")
        // {

        if (cType == "A/R")
        {
            retVal = "102003";
        }
        //
        if (cType == "A/P")
        {
            retVal = "202001";
        }
        if (cType == "C/A")
        {
            retVal = "102003";
        }
        //}
        return retVal;
    }

    public static bool IsBankAccount(string cOffice, string cProject, string cAccount)
    {
        bool retVal = false;
        // if (cOffice == "1" && cProject == "1")
        // {
        if (cAccount.Length < 10)
        {
            return retVal;
        }

        if (cAccount.Substring(0, 6) == "102002") // Bank Account
        {
            retVal = true;
        }

        return retVal;
    }

    public static bool IsCashAccount(string cOffice, string cProject, string cAccount)
    {
        bool retVal = false;
        // if (cOffice == "1" && cProject == "1")
        // {
        if (cAccount.Length < 10)
        {
            return retVal;
        }

        if (cAccount.Substring(0, 6) == "102001") // Bank Account
        {
            retVal = true;
        }

        return retVal;
    }

    public static string GetSaleNarration()
    {
        string retVal = "Sale to Walk in Customer";
        return retVal;
    }
    public static string GetActCode(string mAct)
    {
        string retVal = "";
        if (mAct == "D")
        {
            retVal = "10010101";
        }
        if (mAct == "C")
        {
            retVal = "10010501";
        }

        return retVal;
    }
    public static string GetMenuType(string mName)
    {
        string retVal = "";
        mName = mName.ToLower();
        if (mName == "purchase" || mName == "purchases")
        {
            retVal = "38";
        }
        //
        if (mName == "sale" || mName == "sales")
        {
            retVal = "48";
        }

        if (mName == "je")
        {
            retVal = "26";
        }
        //}
        return retVal;
    }

    public static string GetReportPath()
    {
        return "Docs";
    }
    public static DropDownList InsertNature(DropDownList ddl1)
    {
        ddl1.Items.Clear();
        ddl1.Items.Insert(0, "None");
        ddl1.Items.Insert(1, "Parent");
        ddl1.Items.Insert(2, "Child");
        return ddl1;
    }
    public static string GetNature(string NatureID)
    {
        string retVal = "";
        if (NatureID == "0")
        {
            retVal = "None";
        }
        if (NatureID == "1")
        {
            retVal = "Parent";
        }

        if (NatureID == "2")
        {
            retVal = "Child";
        }

        return retVal;
    }

    public static DropDownList InsertType(DropDownList ddl1)
    {
        ddl1.Items.Clear();
        ddl1.Items.Insert(0, "None");
        ddl1.Items.Insert(1, "Direct");
        ddl1.Items.Insert(2, "CoLoaded");
        ddl1.Items.Insert(3, "Cross Trade");
        ddl1.Items.Insert(4, "Linear Agency");
        return ddl1;
    }
    public static string GetType(string NatureID)
    {
        string retVal = "";
        if (NatureID == "0")
        {
            retVal = "None";
        }
        if (NatureID == "1")
        {
            retVal = "Direct";
        }

        if (NatureID == "3")
        {
            retVal = "Cross Trade";
        }
        if (NatureID == "4")
        {
            retVal = "Linear Agency";
        }
        return retVal;
    }

    public static DropDownList InsertSubType(DropDownList ddl1)
    {
        ddl1.Items.Clear();
        ddl1.Items.Insert(0, "None");
        ddl1.Items.Insert(1, "FCL");
        ddl1.Items.Insert(2, "LCL");

        return ddl1;
    }
    public static string GetSubType(string NatureID)
    {
        string retVal = "";

        if (NatureID == "1")
        {
            retVal = "FCL";
        }

        if (NatureID == "2")
        {
            retVal = "LCL";
        }

        return retVal;
    }

    public static DropDownList InsertDG(DropDownList ddl1)
    {
        ddl1.Items.Clear();
        ddl1.Items.Insert(0, "None");
        ddl1.Items.Insert(1, "DG");
        ddl1.Items.Insert(2, "NON-DG");
        ddl1.Items.Insert(3, "Mix");

        return ddl1;
    }
    public static string GetDG(string NatureID)
    {
        string retVal = "";
        if (NatureID == "0")
        {
            retVal = "None";
        }
        if (NatureID == "1")
        {
            retVal = "DG";
        }

        if (NatureID == "2")
        {
            retVal = "NON-DG";
        }
        if (NatureID == "3")
        {
            retVal = "Mix";
        }
        return retVal;
    }
    public static DropDownList InsertJobKind(DropDownList ddl1)
    {
        ddl1.Items.Clear();
        ddl1.Items.Insert(0, "None");
        ddl1.Items.Insert(1, "Current");
        ddl1.Items.Insert(2, "Opening");


        return ddl1;
    }
    public static string GetJobKind(string NatureID)
    {
        string retVal = "";
        if (NatureID == "0")
        {
            retVal = "None";
        }
        if (NatureID == "1")
        {
            retVal = "Current";
        }

        if (NatureID == "2")
        {
            retVal = "Opening";
        }

        return retVal;
    }
    public static DropDownList InsertSecurity(DropDownList ddl1)
    {
        ddl1.Items.Clear();
        ddl1.Items.Insert(0, "Not Applicable");
        ddl1.Items.Insert(1, "Company");
        ddl1.Items.Insert(2, "Client");
        ddl1.Items.Insert(3, "Transporter");


        return ddl1;
    }
    public static string GetSecurity(string NatureID)
    {
        string retVal = "";
        if (NatureID == "0")
        {
            retVal = "Not Applicable";
        }
        if (NatureID == "1")
        {
            retVal = "Company";
        }

        if (NatureID == "2")
        {
            retVal = "Client";
        }
        if (NatureID == "3")
        {
            retVal = "Transporter";
        }
        return retVal;
    }

    public static DropDownList InsertJobStatus(DropDownList ddl1)
    {
        ddl1.Items.Clear();
        ddl1.Items.Insert(0, "None");
        ddl1.Items.Insert(1, "Opened");
        ddl1.Items.Insert(2, "Closed");
        ddl1.Items.Insert(3, "Cancel");


        return ddl1;
    }
    public static string GetJobStatus(string NatureID)
    {
        string retVal = "";
        if (NatureID == "0")
        {
            retVal = "None";
        }
        if (NatureID == "1")
        {
            retVal = "Opened";
        }

        if (NatureID == "2")
        {
            retVal = "Closed";
        }
        if (NatureID == "3")
        {
            retVal = "Cancel";
        }
        return retVal;
    }

    public static DropDownList InsertShipStatus(DropDownList ddl1)
    {
        ddl1.Items.Clear();
        ddl1.Items.Insert(0, "None");
        ddl1.Items.Insert(1, "Shipped");
        ddl1.Items.Insert(2, "Hold");
        ddl1.Items.Insert(3, "Delivered");
        ddl1.Items.Insert(4, "Booked");
        ddl1.Items.Insert(5, "Closed");


        return ddl1;
    }
    public static string GetShipStatus(string NatureID)
    {
        string retVal = "";
        if (NatureID == "0")
        {
            retVal = "None";
        }
        if (NatureID == "1")
        {
            retVal = "Shipped";
        }

        if (NatureID == "2")
        {
            retVal = "Hold";
        }
        if (NatureID == "3")
        {
            retVal = "Delivered";
        }
        if (NatureID == "4")
        {
            retVal = "Booked";
        }
        if (NatureID == "5")
        {
            retVal = "Closed";
        }
        return retVal;
    }

    public static DropDownList InsertNomination(DropDownList ddl1)
    {
        ddl1.Items.Clear();
        ddl1.Items.Insert(0, "None");
        ddl1.Items.Insert(1, "Free Hand");
        ddl1.Items.Insert(2, "Nominated");
        ddl1.Items.Insert(3, "B2B");



        return ddl1;
    }
    public static string GetNomination(string NatureID)
    {
        string retVal = "";
        if (NatureID == "0")
        {
            retVal = "None";
        }
        if (NatureID == "1")
        {
            retVal = "Free Hand";
        }

        if (NatureID == "2")
        {
            retVal = "Nominated";
        }
        if (NatureID == "3")
        {
            retVal = "B2B";
        }

        return retVal;
    }

    public static DropDownList InsertFreightType(DropDownList ddl1)
    {
        ddl1.Items.Clear();
        ddl1.Items.Insert(0, "None");
        ddl1.Items.Insert(1, "Prepaid");
        ddl1.Items.Insert(2, "Collect");




        return ddl1;
    }
    public static string GetFreightType(string NatureID)
    {
        string retVal = "";
        if (NatureID == "0")
        {
            retVal = "None";
        }
        if (NatureID == "1")
        {
            retVal = "Prepaid";
        }

        if (NatureID == "2")
        {
            retVal = "Collect";
        }


        return retVal;
    }
    public static string GetStringValue(TextBox InputStr)
    {

        return Chk10(InputStr.Text.Trim());
    }


    public static string GetCheckBox(CheckBox Cb1)
    {
        string retVal = "0";
        if (Cb1.Checked == true)
        {
            retVal = "1";
        }
        return retVal;
    }

    public static bool SetCheckBox(string sCheckBox)
    {
        bool retVal = false;
        if (sCheckBox == "1")
        {
            retVal = true;
        }
        return retVal;
    }
    public static CheckBox SetCheckBoxValue(string cbValue)
    {
        CheckBox cb1 = new CheckBox();
        cb1.Checked = false;
        if (cbValue == "1")
        {
            cb1.Checked = true;
        }
        return cb1;
    }
    public static string GetMainPath()
    {
        return "../../Pages/main.aspx";
    }
    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";
            int[] scores = new int[] { 97, 92, 81, 60 };
            string[] unitsMap = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tensMap = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }

    public static string GetSecurityAmount()
    {
        return "4010040007";
    }
}
