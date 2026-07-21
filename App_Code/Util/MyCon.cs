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
using Microsoft.Win32;

/// <summary>
/// Summary description for Connection
/// </summary>
public class MyCon
{
#region Data Member

    private static SqlConnection _sqlConnection;
    private static SqlConnection _sqlConnectionref;
    private static SqlConnection _sqlConnectionOC;


#endregion


    #region Data Member

    private static SqlConnection _sqlConnectionISB;
    

    #endregion

    #region Data Member iSeries

    private static OleDbConnection _OleDbConnection;
    //private static iDB2Connection _iDB2Connection;

    #endregion

    public static OleDbConnection OleDbConnection
    {
        get { return GetOleDbConnection(); }
    }

    //public static iDB2Connection iDB2Connection
    //{
    //    get { return GetiDB2Connection(); }
    //}

    private static OleDbConnection GetOleDbConnectionPrv()
    {
        if ((_OleDbConnection == null) || (_OleDbConnection.ConnectionString == ""))
        {

            _OleDbConnection = new OleDbConnection(MyConfig.GetKey("Conn400", ""));
            _OleDbConnection.Open();
        }

        return _OleDbConnection;
    }

    private static OleDbConnection GetOleDbConnection()
    {
        if ((_OleDbConnection == null) || (_OleDbConnection.ConnectionString == ""))
        {
            bool Condition = true;
            while (Condition)
            {
                try
                {
                    //_OleDbConnection = new OleDbConnection(MyConfig.GetKey("Conn400", ""));
                    //_sqlConnection = new SqlConnection(GetDecryptedConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectionStringFbr"].ToString()));
                    _OleDbConnection = new OleDbConnection(GetDecryptedConnectionString(MyConfig.GetKey("Conn400", "")).ToString());
                    _OleDbConnection.Open();
                    Condition = false;
                }
                catch (Exception Ex)
                {              
                }
            }           
        }

        return _OleDbConnection;
    }

    //private static iDB2Connection GetiDB2Connection()
    //{
    //    if ((_iDB2Connection == null) || (_iDB2Connection.State.ToString() == "Closed"))
    //    {
    //        bool Condition = true;
    //        while (Condition)
    //        {
    //            try
    //            {
    //                //_iDB2Connection = new iDB2Connection(MyConfig.GetKey("Conn400iDB2", ""));
    //                _iDB2Connection = new iDB2Connection(GetDecryptedConnectionString(MyConfig.GetKey("Conn400iDB2", "")).ToString());
    //                _iDB2Connection.Open();
    //                Condition = false;
    //            }
    //            catch (Exception Ex)
    //            {
    //            }
    //        }
    //    }

    //    return _iDB2Connection;
    //}

    #region Get Decrypted Connection String

    protected static string GetDecryptedConnectionString(string encryptedConnectionString)
    {
        string encryptedPassword = string.Empty;
        string decryptedPassword = string.Empty;
        string decryptedConnectionString = string.Empty;

        encryptedPassword = (encryptedConnectionString.Substring(encryptedConnectionString.IndexOf("Password=") + 9).ToString());
        if ((encryptedConnectionString.Substring(encryptedConnectionString.IndexOf("Password=") + 9).ToString()).IndexOf(' ') > 0)
            encryptedPassword = encryptedPassword.Remove((encryptedConnectionString.Substring(encryptedConnectionString.IndexOf("Password=") + 9).ToString()).IndexOf(' ')).Trim(';');
        else
            encryptedPassword = encryptedPassword.Trim(';');
            //decryptedPassword = DecryptString(encryptedPassword);
            decryptedPassword = MyEncrption.DeCrypt(encryptedPassword, 8);

        decryptedConnectionString = encryptedConnectionString.Replace(encryptedPassword, decryptedPassword);
        return decryptedConnectionString;
    }

    //public static string DecryptString(string strKey)
    //{

    //    string strSubkey = @"System";
    //    RegistryKey Mykey = Registry.LocalMachine.OpenSubKey(strSubkey);
    //    string[] strSearcKey = Mykey.GetValueNames();
    //    string EncryptionKey = "";
    //    foreach (string strSearch in strSearcKey)
    //    {
    //        if (strSearch.Equals("MyEncKey"))  //  if (strSearch.Equals("EncryptionKey"))
    //        {
    //            EncryptionKey = Mykey.GetValue("MyEncKey").ToString();
    //            break;
    //        }
    //    }
    //    Mykey.Close();

    //    return Encryption.Decrypt(strKey, EncryptionKey, true);
    //}
 
    #endregion

    public static string GetOleDbConnectionString()
    {
        return MyConfig.GetKey("Conn400", "");
    }


    //#region iSeries Data Member

    //private static SqlConnection _sqlConnection;


    //#endregion

    public static SqlConnection SqlConnection
    {
        get { return GetSqlConnection(); }
    }

    public static SqlConnection SqlConnectionOC    //1
    {
        get { return GetSqlConnectionOC(); }
    }

    public static SqlConnection SqlConnectionISB
    {
        get { return GetSqlConnectionISB(); }
    }



    #region Transaction Method


    public static SqlTransaction TransactionBegin(SqlConnection pSqlConnection)
    {

        SqlConnection connection = pSqlConnection;
        if (connection == null)
        {
            throw new Exception("Unable to create connection to database server");
        }

        if (connection.State == ConnectionState.Closed)
            connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        if (transaction == null)
        {
            throw new Exception("Unable to start transaction on database server");
        }

        return transaction;
    }


    public static void TransactionCommit(SqlTransaction transaction)
    {
        if (transaction != null)
        {
            transaction.Commit();

            transaction.Dispose();
        }

        if (transaction.Connection != null)
        {
            if (transaction.Connection.State == ConnectionState.Open)
                transaction.Connection.Close();
        }
    }


    public static void TransactionRollback(SqlTransaction transaction)
    {
        if (transaction != null)
        {
            transaction.Rollback();

            transaction.Dispose();
        }

        if (transaction.Connection != null)
        {
            if (transaction.Connection.State == ConnectionState.Open)
                transaction.Connection.Close();
        }
    }

    #endregion

    private static SqlConnection GetSqlConnection()
    {
        if (_sqlConnection == null || _sqlConnection.ConnectionString=="")
        {
            //_sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectionString"].ToString());
            _sqlConnection = new SqlConnection(MyConfig.GetKey("ConnKHI", ""));
            _sqlConnection.Open();
        }

        return _sqlConnection;
    }

    private static SqlConnection GetSqlConnectionOC()
    {
        if (_sqlConnectionOC == null || _sqlConnectionOC.ConnectionString == "")
        {
            //_sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectionString"].ToString());
            _sqlConnectionOC = new SqlConnection(MyConfig.GetKey("ConnKHIoc", ""));
            _sqlConnectionOC.Open();
        }
        return _sqlConnectionOC;
    }

    private static SqlConnection GetSqlConnectionISB()
    {
        if (_sqlConnectionISB == null)
        {
            //_sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectionString"].ToString());
            _sqlConnectionISB = new SqlConnection(MyConfig.GetKey("ConnISB", ""));
            _sqlConnectionISB.Open();
        }

        return _sqlConnectionISB;
    }


    #region Get Param

    public static SqlParameter GetParam(string paramName, object paramValue, ParameterDirection direction, SqlDbType dbType)
    {
        SqlParameter param = new SqlParameter();

        param.ParameterName = paramName;
        if (paramValue != null)
        {
            param.Value = paramValue;
        }
        param.Direction = direction;
        param.SqlDbType = dbType;

        return param;
    }
    #endregion

    //New Code
    public static SqlConnection SqlConnectionRef
    {
        get { return GetSqlConnectionRef(); }
    }

    private static SqlConnection GetSqlConnectionRef()
    {
        if (_sqlConnectionref == null)
        {
            _sqlConnectionref = new SqlConnection(MyConfig.GetKey("ConnRef", ""));
            _sqlConnectionref.Open();
        }

        return _sqlConnectionref;
    }


    //End New Code

}
