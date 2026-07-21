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
public class Connection
{
    #region Data Member

    private static SqlConnection _sqlConnection;
    private static OleDbConnection _OleDbConnection;

    #endregion


    private static OleDbConnection GetOleDbConnection()
    {
        if ((_OleDbConnection == null) || (_OleDbConnection.ConnectionString == ""))
        {

            try
            {
                _OleDbConnection = new OleDbConnection(MyConfig.GetKey("ConAcs", ""));
                
                _OleDbConnection.Open();
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        return _OleDbConnection;
    }

    public static string GetOleDbConnectionString()
    {
        return MyConfig.GetKey("Conn400", "");
    }

    public static OleDbConnection OleDbConnection
    {
        get { return GetOleDbConnection(); }
    }


    public static SqlConnection SqlConnection
    {
        get { return GetSqlConnection(); }
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
        if (_sqlConnection == null || _sqlConnection.ConnectionString == "")
        {
            //_sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectionString"].ToString());
            //_sqlConnection = new SqlConnection(MyConfig.GetKey("ConSql", ""));
            if (UType.IsWeb())
            {
                _sqlConnection = new SqlConnection(MyConfig.GetKey("ConSqlWeb", ""));
                _sqlConnection.Open();
            }
            else
            {
                _sqlConnection = new SqlConnection(GetDecryptedConnectionString(MyConfig.GetKey("ConSql", "")));
                _sqlConnection.Open();

            }
        }

        return _sqlConnection;
    }

    private static SqlConnection GetAcsConnection()
    {
        if (_sqlConnection == null || _sqlConnection.ConnectionString == "")
        {
            //_sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectionString"].ToString());
            //_sqlConnection = new SqlConnection(MyConfig.GetKey("ConSql", ""));
            _sqlConnection = new SqlConnection(GetDecryptedConnectionString(MyConfig.GetKey("ConSql", "")));
            _sqlConnection.Open();
        }

        return _sqlConnection;
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

    #region Get Decrypted Connection String

    protected static string GetDecryptedConnectionString(string encryptedConnectionString)
    {
        string encryptedPassword = string.Empty;
        string decryptedPassword = string.Empty;
        string decryptedConnectionString = string.Empty;

        encryptedPassword = (encryptedConnectionString.Substring(encryptedConnectionString.IndexOf("Password=") + 9).ToString());
        if ((encryptedConnectionString.Substring(encryptedConnectionString.IndexOf("Password=") + 9).ToString()).IndexOf(' ') > 0)
            encryptedPassword = encryptedPassword.Remove((encryptedConnectionString.Substring(encryptedConnectionString.IndexOf("Password=") + 9).ToString()).IndexOf(';')).Trim(';');
        else
            encryptedPassword = encryptedPassword.Trim(';');
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
    //        if (strSearch.Equals("MyEncKey"))
    //        {
    //            EncryptionKey = Mykey.GetValue("MyEncKey").ToString();
    //            break;
    //        }
    //    }
    //    Mykey.Close();

    //    return Encryption.Decrypt(strKey, EncryptionKey, true);
    //}

    #endregion
}
