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

/// <summary>
/// Summary description for MyRefDb
/// </summary>
public class MyRefDb
{
    private SqlConnection _SqlConnection;
    private MyRef _MyRef;
    string SqlStr = string.Empty;
    string Comma = ",";
    string Sqote = "'";


    #region Constructors
    public MyRefDb(MyRef objMyRef)
    {
        _MyRef = objMyRef;
    }

    public MyRefDb(SqlConnection Sqlcon, MyRef objMyRef)
    {
        _SqlConnection = Sqlcon;
        _MyRef = objMyRef;
    }

    #endregion

    #region Properties

    private SqlConnection SqlConnection
    {
        get { return _SqlConnection; }
    }

    private MyRef MyRef
    {
        get { return _MyRef; }
    }

    #endregion

    public DataSet Sp_GetRoleInfo_Department()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pVar1", MyRef.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetRoleInfo_Department", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet Sp_GetAllMenu()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[0];
            //parameters[0] = Connection.GetParam("@pVar1", MyRef.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetAllMenu");
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_UserRole()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[0];
            //parameters[0] = Connection.GetParam("@pVar1", MyRef.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetAllMenu");
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet Sp_GetMenu_Dep_Role()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pVar1", MyRef.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pVar2", MyRef.Fld2, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetMenu_Dep_Role", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetMenu_Dep_Role_Menu()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = Connection.GetParam("@pVar1", MyRef.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pVar2", MyRef.Fld2, ParameterDirection.Input, SqlDbType.Decimal);
            parameters[2] = Connection.GetParam("@pVar3", MyRef.Fld2, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetMenu_Dep_Role_Menu", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public string InsertUserRoleInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfRoleMenu ";
            SqlStr += "(";
            SqlStr += "DepartId ";
            SqlStr += ",RoleId ";
            SqlStr += ",MenuId ";
            SqlStr += ",MenuSno";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyRef.Fld1;
            SqlStr += Comma + MyRef.Fld2;
            SqlStr += Comma + MyRef.Fld3;
            SqlStr += Comma + MyRef.Fld4;
            SqlStr += ")";
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string DeleteUserRoleInfo()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "DELETE FROM RfRoleMenu ";
            SqlStr += " WHERE";
            //SqlStr += " DepartId =" + MyRef.Fld1;
            SqlStr += " RoleId   =" + MyRef.Fld2;
            SqlStr += " AND MenuId       =" + MyRef.Fld3;
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string NonQryCmd(string SqlString)
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
    public DataSet Sp_GetRoleId()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pVar1", MyRef.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pVar2", MyRef.Fld2, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetRoleId", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetRoleId1()
    {
        DataSet ds1 = null;
        try
        {
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetRoleId1");
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public string InsertUserRoleInfoNew()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO UserRole ";
            SqlStr += "(";
            SqlStr += "UserId ";
            SqlStr += ",DepartId ";
            SqlStr += ",RoleId ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyRef.Fld1;
            SqlStr += Comma + MyRef.Fld2;
            SqlStr += Comma + MyRef.Fld3;
            SqlStr += ")";
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string DeleteUserRoleInfoNew()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "DELETE FROM UserRole ";
            SqlStr += " WHERE";
            SqlStr += " UserId =" + MyRef.Fld1;
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet Sp_GetFillComboDepartment()
    {
        SqlStr = "Sp_GetRfDepartment";
        DataSet ds = GetDatasetSp(SqlStr);
        return ds;
    }

    public DataSet Sp_GetFillComboPayGroup()
    {
        SqlStr = "Sp_GetRfPayGroup";
        DataSet ds = GetDatasetSp(SqlStr);
        return ds;
    }

    public DataSet Sp_GetFillComboAllDed()
    {
        SqlStr = "Sp_GetRfAllDed";
        DataSet ds = GetDatasetSp(SqlStr);
        return ds;
    }

    public DataSet Sp_GetFillComboAllDedType()
    {
        SqlStr = "Sp_GetRfAllDedType";
        DataSet ds = GetDatasetSp(SqlStr);
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

    public DataSet Sp_GetDepartment()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pVar1", MyRef.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetDepartment", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetPayGroup()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pVar1", MyRef.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetPayGroup", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public string InsertDepart()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfDepart ";
            SqlStr += "(";
            SqlStr += "DepartId ";
            SqlStr += ",DepartDescription ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyRef.Fld1;
            SqlStr += Comma + Sqote + MyRef.Fld2 + Sqote;
            SqlStr += ")";
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateDepart()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "UPDATE RfDepart ";
            SqlStr += " SET ";
            SqlStr += " DepartDescription =" + Sqote + MyRef.Fld2 + Sqote;
            SqlStr += " WHERE ";
            SqlStr += " DepartId =" + MyRef.Fld1;

            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string UpdateOffice()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "UPDATE RfOffice ";
            SqlStr += " SET ";
            SqlStr += " OfficeDescription =" + Sqote + MyRef.Fld2 + Sqote;
            SqlStr += " WHERE ";
            SqlStr += " OfficeId =" + MyRef.Fld1;

            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public string InsertPayGroup()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfPayGroup ";
            SqlStr += "(";
            SqlStr += "PayGroupId ";
            SqlStr += ",PayGroupDescription ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyRef.Fld1;
            SqlStr += Comma + Sqote + MyRef.Fld2 + Sqote;
            SqlStr += ")";
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdatePayGroup()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "UPDATE RfPayGroup ";
            SqlStr += " SET ";
            SqlStr += " PayGroupDescription =" + Sqote + MyRef.Fld2 + Sqote;
            SqlStr += " WHERE ";
            SqlStr += " PayGroupId =" + MyRef.Fld1;

            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public DataSet Sp_GetRfDesignationAll()
    {
        SqlStr = "Sp_GetRfDesignationAll";
        DataSet ds = GetDatasetSp(SqlStr);
        return ds;
    }

    public DataSet Sp_GetDepartDesignation()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pVar1", MyRef.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pVar2", MyRef.Fld2, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetDepartDesignation", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }

    public DataSet Sp_GetPayGroupAllDed()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = Connection.GetParam("@pVar1", MyRef.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            parameters[1] = Connection.GetParam("@pVar2", MyRef.Fld2, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetPayGroupAllDed", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }
    public DataSet Sp_GetPayGroupAllDed1()
    {
        DataSet ds1 = null;
        try
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = Connection.GetParam("@pVar1", MyRef.Fld1, ParameterDirection.Input, SqlDbType.Decimal);
            _SqlConnection = Connection.SqlConnection;
            ds1 = SqlHelper.ExecuteDataset(_SqlConnection, CommandType.StoredProcedure, "Sp_GetPayGroupAllDed1", parameters);
        }
        catch (Exception ex)
        {
            ds1 = null;
        }
        return ds1;
    }


    public string InsertDesignation()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfDesignation ";
            SqlStr += "(";
            SqlStr += "DepartId ";
            SqlStr += ",DesignationId ";
            SqlStr += ",DesignationDescription ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyRef.Fld1;
            SqlStr += Comma + MyRef.Fld2;
            SqlStr += Comma + Sqote + MyRef.Fld3 + Sqote;
            SqlStr += ")";
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdateDesignation()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "UPDATE RfDesignation ";
            SqlStr += " SET ";
            SqlStr += " DesignationDescription =" + Sqote + MyRef.Fld3 + Sqote;
            SqlStr += " WHERE ";
            SqlStr += " DepartId =" + MyRef.Fld1;
            SqlStr += " AND DesignationId =" + MyRef.Fld2;

            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string InsertPayGroupAllDed()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfPayGroupAllDed ";
            SqlStr += "(";
            SqlStr += "PayGroupId ";
            SqlStr += ",AllDedId ";
            //SqlStr += ",AllDedTypeId ";
            SqlStr += ",AllDedPer ";
            SqlStr += ",AllDedAmount ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyRef.Fld1;
            SqlStr += Comma + MyRef.Fld2;
            //SqlStr += Comma + MyRef.Fld3;
            SqlStr += Comma + MyRef.Fld4;
            SqlStr += Comma + MyRef.Fld5;
            SqlStr += ")";
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }

    public string UpdatePayGroupAllDed()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "UPDATE RfPayGroupAllDed ";
            SqlStr += " SET ";
            //SqlStr += " AllDedTypeId =" + MyRef.Fld3;
            SqlStr += " AllDedPer =" + MyRef.Fld4;
            SqlStr += " ,AllDedAmount =" + MyRef.Fld5;
            SqlStr += " WHERE ";
            SqlStr += " PayGroupId =" + MyRef.Fld1;
            SqlStr += " AND AllDedId =" + MyRef.Fld2;

            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetComboExpense()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfExpense ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public DataSet GetComboExpense(string pExp)
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfExpense ";
        SqlStr += " Where ExpenseId = " + pExp;
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }

    public DataSet GetDataset(string SqlString)
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
    public DataSet GetComboOffice()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfOffice ";
        if (UType.MyCtoD(MyRef.Fld1) > 0)
        {
            SqlStr += " Where officeid = " + MyRef.Fld1;
        }
        if (MyRef.Fld2.Length > 0)
        {
            SqlStr += " Where officedescription like '%" + MyRef.Fld2 + "%'";
        }
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public DataSet GetOffice()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfOffice ";
        if (MyRef.Fld1.Length > 0)
        {
            SqlStr += " where officeid = " + MyRef.Fld1;
        }
        SqlStr += " order by officeid desc ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public string InsertOffice()
    {
        string retVal = string.Empty;
        try
        {
            SqlStr = "INSERT INTO RfOffice ";
            SqlStr += "(";
            SqlStr += "OfficeId ";
            SqlStr += ",OfficeDescription ";
            SqlStr += ")";
            SqlStr += "VALUES( ";
            SqlStr += MyRef.Fld1;
            SqlStr += Comma + Sqote + MyRef.Fld2 + Sqote;
            SqlStr += ")";
            retVal = NonQryCmd(SqlStr);

        }
        catch (Exception ex)
        {
            retVal = ex.Message;
        }
        return retVal;
    }
    public DataSet GetVnoByOfficeId()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM acttran ";
        if (UType.MyCtoD(MyRef.Fld1) > 0)
        {
            SqlStr += " Where officeid = " + MyRef.Fld1;
        }
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public DataSet GetProject()
    {
        SqlStr = "SELECT  ";
        SqlStr += "[OfficeId]";
        SqlStr += ",[ProjectId]";
        SqlStr += ",[ProjectDescription]";
        SqlStr += ",[oAddress]";
        SqlStr += ",[oAreaId]";
        SqlStr += ",[oCityId]";
        SqlStr += ",[oTel]";
        SqlStr += ",[oCell]";
        SqlStr += ",[oEmail]";
        SqlStr += ",[Effdate]";
        SqlStr += ",[Enddate]";
        SqlStr += ",[ModuleId]";
        SqlStr += " FROM RfProject ";
        SqlStr += " where Officeid = " + MyRef.Fld1;
        if (MyRef.Fld2.Length > 0)
        {
            SqlStr += " and Projectid = " + MyRef.Fld2;
        }
        SqlStr += " order by Projectid desc ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public DataSet GetCity()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfCity ";
        SqlStr += " order by Cityid desc ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public DataSet GetArea()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfArea ";
        SqlStr += " where cityid = " + MyRef.Fld1;
        SqlStr += " order by Areaid desc ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
    public DataSet GetModule()
    {
        SqlStr = "SELECT * ";
        SqlStr += " FROM RfModule ";
        SqlStr += " order by Moduleid desc ";
        DataSet ds = GetDataset(SqlStr);
        return ds;
    }
}



