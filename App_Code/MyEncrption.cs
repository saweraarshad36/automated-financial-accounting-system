using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Win32;

/// <summary>
/// Summary description for MyEncrption
/// </summary>
public static class MyEncrption
{
    //string MyKey = MyEncrption.Encrypt("300");
    //string DeMyKey = MyEncrption.DeCrypt(MyKey, 3);

    #region New Encryption 
    public static string EncrptNew(string Var1)
    {
        string retVal = "";
        string oneC = "";
        decimal Var1Len;
        string Tem1 = "";
        decimal mReminder;
        int OneD = 0;
        Var1Len = Var1.Length;
        char[] Var1Arr = Var1.ToCharArray();
        for (int i = 0; i < Var1Len; i++)
        {
            //char tChar = Var1Arr[i]; //Var1.Substring(i, 1);
            OneD = Convert.ToInt32(Var1Arr[i]);  //Asc(Mid(Var1, i, 1))
            mReminder = i % 2;
            if (mReminder == 0) //If mReminder = 0 Then
            {
                OneD = OneD + 10;
            }
            else
            {
                OneD = OneD + 15;
            }
            Tem1 = Tem1 + OneD.ToString().Trim(); //Trim(Str(OneD))
            Tem1 = Tem1 + "^";
        }

        string Tem2 = "";
        string[] arrCode = { "~", "!", "@", "#", "$", "%", "<", "&", "*", ">" };
        oneC = ""; // As String
        int Tem1Len = Tem1.Length; // //Len(Tem1)
        for (int j = 0; j < Tem1Len; j++)
        {
            //For j = 1 To Tem1Len
            oneC = Tem1.Trim().Substring(j, 1); //oneC = Mid(Trim(Tem1), j, 1)
            if (oneC != "^")   //If oneC<> "^" Then
            {
                Tem2 = Tem2 + arrCode[Convert.ToInt32(oneC)].Trim();  //Tem2 + LTrim(arrCode(oneC))
            }
            if (oneC == "^") //If oneC = "^" Then
            {
                Tem2 = Tem2 + "^";
            } //            End If
        }
        retVal = Tem2;
        return retVal;
    }

    public static string DeCrptNew(string Tem1) // Function MyDec(Tem1 As String)
    {
        string retVal = "";
        int Tem1Len = 0;
        string Tem2 = "";
        string oneC = "";
        Tem1Len = Tem1.Length;    // Len(Tem1)
        for (int j = 0; j < Tem1Len; j++) //For j = 1 To Tem1Len
        {
            oneC = Tem1.Trim().Substring(j, 1); //Mid(Trim(Tem1), j, 1)
            if (oneC == "~")
            {
                Tem2 = Tem2 + "0";
            }
            if (oneC == "!")
            {
                Tem2 = Tem2 + "1";
            }
            if (oneC == "@") //Then
            {
                Tem2 = Tem2 + "2";
            }
            if (oneC == "#")
            {
                Tem2 = Tem2 + "3";
            }
            if (oneC == "$")
            {
                Tem2 = Tem2 + "4";
            }
            if (oneC == "%")
            {
                Tem2 = Tem2 + "5";
            }
            if (oneC == "<")
            {
                Tem2 = Tem2 + "6";
            }
            if (oneC == "&")
            {
                Tem2 = Tem2 + "7";
            }
            if (oneC == "*")
            {
                Tem2 = Tem2 + "8";
            }
            if (oneC == ">")
            {
                Tem2 = Tem2 + "9";
            }
            if (oneC == "^")
            {
                Tem2 = Tem2 + "^";
            }
        }  //        Next
           //new code
        string[] Separ = new string[] { "^" };
        String[] strlist = Tem2.Split(Separ, StringSplitOptions.RemoveEmptyEntries);
        int cTr = 0;
        int mReminder = 0;
        string oneDig = "";
        string Tem3 = "";
        int oneDigint = 0;
        foreach (String s in strlist)
        {
            mReminder = cTr % 2;
            if (mReminder == 0)
            {
                oneDigint = Convert.ToInt32(s) - 10;  //    -10;
                //oneDig = Convert.ToString(Convert.ToDecimal(oneDig) - 10); // Str(CDec(oneDig) - 10);
            }
            else
            {
                oneDigint = Convert.ToInt32(s) - 15;  // 15;
                // oneDig = Convert.ToString(Convert.ToDecimal(oneDig) - 15); //Str(CDec(oneDig) - 15);
            }
            char oneDigChr = (char)oneDigint;
            Tem3 = Tem3 + oneDigChr.ToString(); //Tem3 + LTrim(Chr(oneDig));
            cTr++;
        }

        //end of new code
        retVal = Tem3;
        return retVal;
    }


    #endregion
    public static string GetEncryptKey()
    {
        string retVal = string.Empty;
        string v1 = UType.GetPinCode1();
        string v2 = DateTime.Now.ToString("ssff");
        if (v2.Length < 4)
        { v2 = "0" + v2; }
        string v3 = UType.GetPinCode2();
        string vKey = v1 + v2 + v3;
        return retVal = Encrypt(vKey);
    }

    public static string Encrypt(string vKey)
    {
        string v4 = string.Empty;
        string v5 = string.Empty;
        string v6 = string.Empty;
        //string[] MyCode ={ "#", "%", "A", "B", "C", "D", "E", "F", "!", "@" };
        string[] MyCode = GetCode();
        int[] MyVar ={ 110, 10, 115, 20, 120, 30, 125, 40, 130, 50 };
        string retVal = string.Empty;
        int i = 0;
        int iLen = 0;
        int Ctr = 0;
        foreach (char aa in vKey)
        {
            i = (int)aa;
            i = i * MyVar[Ctr];
            iLen = i.ToString().Length;
            v4 = v4 + iLen.ToString();
            v5 = v5 + i.ToString();
            Ctr++;
        }
        v6 = v4 + v5;
        retVal = string.Empty;
        foreach (char aa in v6)
        {
            decimal a = Convert.ToDecimal(Convert.ToString(aa));
            retVal = retVal + MyCode[Convert.ToInt16(a)];
        }
        return retVal;
    }

    public static string DeCrypt(string val1,int val2)
    {
        string retVal = string.Empty;
        string v1 = string.Empty;
        int val1Len = val1.Length;
        string v2 = string.Empty;
        string v3 = string.Empty;
        string v4 = string.Empty;
        int v5 = 0;
        string[] MyDeCode ={"0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };       
        int[] MyVar ={ 110, 10, 115, 20, 120, 30, 125, 40, 130, 50 };
        int Ctr=0;
        int MyVarCtr=0;
        foreach (char aa in val1)
        {
            int a = GetCode(aa);
            v1 = v1 + MyDeCode[Convert.ToInt16(a)];
        }
        v2 = v1.Substring(0, val2);
        v3 = v1.Substring((val2),v1.Length-val2 );
        foreach (char aa in v2)
        {
            string a = Convert.ToString(aa);
            v4=v3.Substring(Ctr,Convert.ToInt16(a));
            v5 = Convert.ToInt16((UType.MyCtoD(v4) / MyVar[MyVarCtr]));
            char v55 = Convert.ToChar(v5);
            retVal = retVal + Convert.ToString(v55);
            MyVarCtr++;
            Ctr += Convert.ToInt16(a);
        }       
        return retVal;
    }

    private static int GetCode(char a)
    {
        string MyCd = GetMyKey();
        int retVal = MyCd.IndexOf(a);
        return retVal;
 
    }
    private static string[] GetCode()
    {
        string MyCd = GetMyKey();
        string[] retVal = new string[10];
        for (int i = 0; i < 10; i++)
        {
            retVal[i] = MyCd.Substring(i, 1);
        }
        return retVal;

    }
    private static string GetMyKey()
    {

        //string strSubkey = @"System";
        //RegistryKey Mykey = Registry.LocalMachine.OpenSubKey(strSubkey);
        //string[] strSearcKey = Mykey.GetValueNames();
        //string EncryptionKey = "";
        //foreach (string strSearch in strSearcKey)
        //{
        //    if (strSearch.Equals("MyEncKey"))
        //    {
        //        EncryptionKey = Mykey.GetValue("MyEncKey").ToString();
        //        break;
        //    }
        //}
        //Mykey.Close();
        //return EncryptionKey;
        return "#%ABCDEF!@";
    }

}
