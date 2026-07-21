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

/// <summary>
/// Summary description for MyRef
/// </summary>
public class MyRef
{
    #region Defaults

    private SqlConnection _SqlConnection;
    private OleDbConnection _OleDbConnection;


    #endregion

    #region Column
    private string _Fld1 = string.Empty;
    private string _Fld2 = string.Empty;
    private string _Fld3 = string.Empty;
    private string _Fld4 = string.Empty;
    private string _Fld5 = string.Empty;
    
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

    public bool DisposeSQLConnection()
    {
       // _SqlConnection.Close();
       // _SqlConnection.Dispose();
        return true;
    }

    public bool DisposeOleDbConnection()
    {
        _OleDbConnection.Close();
        _OleDbConnection.Dispose();
        return true;
    }

	public MyRef()
	{
	}
    public MyRef(SqlConnection con)
    {
        _SqlConnection = con;
    }

    public DataSet GetRoleInfo_Department()
    {
        MyRefDb oMyRefDb = new MyRefDb(this);
        DataSet result = oMyRefDb.Sp_GetRoleInfo_Department();
        DisposeSQLConnection();
        return result;
    }
    public DataSet FillComboMenu()
    {
        MyRefDb oMyRefDb = new MyRefDb(this);
        DataSet result = oMyRefDb.Sp_GetAllMenu();

        return result;
    }
    public DataSet GetUserRole()
    {
        MyRefDb oMyRefDb = new MyRefDb(this);
        DataSet result = oMyRefDb.Sp_UserRole();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetMenu_Dep_Role()
    {
        MyRefDb oMyRefDb = new MyRefDb(this);
        DataSet result = oMyRefDb.Sp_GetMenu_Dep_Role();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetMenu_Dep_Role_Menu()
    {
        MyRefDb oMyRefDb = new MyRefDb(this);
        DataSet result = oMyRefDb.Sp_GetMenu_Dep_Role_Menu();
        DisposeSQLConnection();
        return result;
    }
    public string InsertUserRoleInfo()
    {
        MyRefDb oMyRefDb = new MyRefDb(this);
        string result = oMyRefDb.InsertUserRoleInfo();
        DisposeSQLConnection();
        return result;
    }
    public string DeleteUserRoleInfo()
    {
        MyRefDb oMyRefDb = new MyRefDb(this);
        string result = oMyRefDb.DeleteUserRoleInfo();
        DisposeSQLConnection();
        return result;

    }
    public DataSet GetRoleId()
    {
        MyRefDb oMyRefDb = new MyRefDb(this);
        DataSet result = oMyRefDb.Sp_GetRoleId();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetRoleId1()
    {
        MyRefDb oMyRefDb = new MyRefDb(this);
        DataSet result = oMyRefDb.Sp_GetRoleId1();
        DisposeSQLConnection();
        return result;
    }

    public string InsertUserRoleInfoNew()
    {
        MyRefDb oMyRefDb = new MyRefDb(this);
        string result = oMyRefDb.InsertUserRoleInfoNew();
        DisposeSQLConnection();
        return result;
    }
    public string DeleteUserRoleInfoNew()
    {
        MyRefDb oMyRefDb = new MyRefDb(this);
        string result = oMyRefDb.DeleteUserRoleInfoNew();
        DisposeSQLConnection();
        return result;

    }
    public DataSet FillComboDepartment()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.Sp_GetFillComboDepartment();

        return result;
    }

    public DataSet FillComboPayGroup()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.Sp_GetFillComboPayGroup();

        return result;
    }
    public DataSet GetDepartment()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.Sp_GetDepartment();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetPayGroup()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.Sp_GetPayGroup();
        DisposeSQLConnection();
        return result;
    }
    public string InsertDepart()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        string result = oMyDb.InsertDepart();
        DisposeSQLConnection();
        return result;
    }

    public string InsertPayGroup()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        string result = oMyDb.InsertPayGroup();
        DisposeSQLConnection();
        return result;
    }

    public string UpdateDepart()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        string result = oMyDb.UpdateDepart();
        DisposeSQLConnection();
        return result;
    }
    public string UpdateOffice()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        string result = oMyDb.UpdateOffice();
        DisposeSQLConnection();
        return result;
    }
    public string UpdatePayGroup()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        string result = oMyDb.UpdatePayGroup();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetRfDesignationAll()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.Sp_GetRfDesignationAll();

        return result;
    }
    public DataSet GetDepartDesignation()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.Sp_GetDepartDesignation();
        DisposeSQLConnection();
        return result;
    }

    public DataSet GetPayGroupAllDed()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.Sp_GetPayGroupAllDed();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetPayGroupAllDed1()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.Sp_GetPayGroupAllDed1();
        DisposeSQLConnection();
        return result;
    }


    public string InsertDesignation()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        string result = oMyDb.InsertDesignation();
        DisposeSQLConnection();
        return result;
    }
    public string UpdateDesignation()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        string result = oMyDb.UpdateDesignation();
        DisposeSQLConnection();
        return result;
    }

    public string InsertPayGroupAllDed()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        string result = oMyDb.InsertPayGroupAllDed();
        DisposeSQLConnection();
        return result;
    }

    public string UpdatePayGroupAllDed()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        string result = oMyDb.UpdatePayGroupAllDed();
        DisposeSQLConnection();
        return result;
    }


    public DataSet FillComboAllDed()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.Sp_GetFillComboAllDed();

        return result;
    }
    public DataSet FillComboAllDedType()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.Sp_GetFillComboAllDedType();

        return result;
    }

    public DataSet FillComboOffice()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.GetComboOffice();

        return result;
    }
    public DataSet GetOffice()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.GetOffice();
        DisposeSQLConnection();
        return result;
    }
    public string InsertOffice()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        string result = oMyDb.InsertOffice();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetVnoByOfficeId()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.GetVnoByOfficeId();

        return result;
    }
    public DataSet GetProject()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.GetProject();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetCity()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.GetCity();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetArea()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.GetArea();
        DisposeSQLConnection();
        return result;
    }
    public DataSet GetModule()
    {
        MyRefDb oMyDb = new MyRefDb(this);
        DataSet result = oMyDb.GetModule();
        DisposeSQLConnection();
        return result;
    }
}
