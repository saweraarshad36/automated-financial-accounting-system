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

/// <summary>
/// Summary description for Filter
/// </summary>
public class Filter
{
    #region Constructors

    public Filter()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #endregion

    #region Fill Combo

    public static void FillCombo(ref DropDownList cmb, string applivationValiableName, string filter, string valueField, string textField, string toolTipField)
    {
        FillCombo(ref cmb, ApplicationDataTable(applivationValiableName), filter, valueField, textField, toolTipField);
    }

    public static void FillCombo(ref DropDownList cmb, DataTable table, string filter, string valueField, string textField, string toolTipField)
    {
        if (!string.IsNullOrEmpty(filter))
        {
            table = FilterTable(table, filter, textField);
        }

        cmb.Items.Clear();

        if (!string.IsNullOrEmpty(toolTipField))
        {
            foreach (DataRow row in table.Rows)
            {
                ListItem item = new ListItem();

                item.Text = row[textField].ToString();
                item.Value = row[valueField].ToString();
                item.Attributes.Add("title", row[toolTipField].ToString());

                cmb.Items.Add(item);
            }
        }
        else
        {
            cmb.DataSource = table;
            cmb.DataValueField = valueField;
            cmb.DataTextField = textField;
            cmb.DataBind();
        }
    }

    public static void FillCombo(ref DropDownList cmb, int maximumYear, int minimumYear)
    {
        int j = 0;

        for (int i = maximumYear; i >= minimumYear; i--)
        {
            cmb.Items.Insert(j, i.ToString());
            j++;
        }
    }

    public static void InsertBlankItemInCombo(ref DropDownList cmb)
    {
        cmb.Items.Insert(0, new ListItem("---SELECT ITEM---", "0"));
    }

    public static void InsertBlankItemInCombo(ref DropDownList cmb, string text)
    {
        cmb.Items.Insert(0, new ListItem(text, "0"));
    }

    #region Searching Combo

    public static void FillCombo(DropDownList cmb, string applivationValiableName, string filter, string valueField, string textField, string toolTipField)
    {
        FillCombo(cmb, ApplicationDataTable(applivationValiableName), filter, valueField, textField, toolTipField);
    }

    public static void FillCombo(DropDownList cmb, DataTable table, string filter, string valueField, string textField, string toolTipField)
    {
        if (!string.IsNullOrEmpty(filter))
        {
            table = FilterTable(table, filter, textField);
        }

        cmb.Items.Clear();

        if (!string.IsNullOrEmpty(toolTipField))
        {
            foreach (DataRow row in table.Rows)
            {
                ListItem item = new ListItem();

                item.Text = row[textField].ToString();
                item.Value = row[valueField].ToString();
                item.Attributes.Add("title", row[toolTipField].ToString());

                cmb.Items.Add(item);
            }
        }
        else
        {
            cmb.DataSource = table;
            cmb.DataValueField = valueField;
            cmb.DataTextField = textField;
            cmb.DataBind();
        }
    }

    public static void FillCombo(DropDownList cmb, int maximumYear, int minimumYear)
    {
        int j = 0;

        for (int i = maximumYear; i >= minimumYear; i--)
        {
            cmb.Items.Insert(j, i.ToString());
            j++;
        }
    }

    public static void InsertBlankItemInCombo(DropDownList cmb)
    {
        cmb.Items.Insert(0, new ListItem("---SELECT ITEM---", "0"));
    }

    public static void InsertBlankItemInCombo(DropDownList cmb, string text)
    {
        cmb.Items.Insert(0, new ListItem(text, "0"));
    }

    #endregion

    #endregion

    #region Application DataTable

    public static DataTable ApplicationDataTable(string key)
    {
        if (HttpContext.Current.Application[key] == null)
        {
            return null;
        }

        return (DataTable)HttpContext.Current.Application[key];
    }

    public static string ApplicationDataTableValue(string key, string getColumnName, params object[] filterColVal)
    {
        DataTable table = ApplicationDataTable(key);

        table = FilterTable(table, FilterString(" = ", " AND ", filterColVal));

        if (table != null && table.Rows.Count > 0)
        {
            return table.Rows[0][getColumnName].ToString();
        }

        return "";
    }

    #endregion

    #region Filter Table

    public static DataTable FilterTable(DataTable table, string filter)
    {
        DataTable tablex = null;

        if (table == null)
        {
            return null;
        }

        try
        {
            table.DefaultView.RowFilter = filter;
            tablex = table.DefaultView.ToTable(table.TableName);
            table.DefaultView.RowFilter = "";
        }
        catch (Exception ex)
        {
            tablex = table.Clone();
        }

        return tablex;
    }

    public static DataTable FilterTable(DataTable table, string filter, string sortColumn)
    {
        DataTable tablex = null;

        if (table == null)
        {
            return null;
        }

        try
        {
            table.DefaultView.RowFilter = filter;
            table.DefaultView.Sort = sortColumn + " ASC";
            tablex = table.DefaultView.ToTable(table.TableName);
            table.DefaultView.RowFilter = "";
        }
        catch (Exception ex)
        {
            tablex = table.Clone();
        }

        return tablex;
    }

    public static string FilterString(string sep1, string sep2, params object[] s)
    {
        string ret = "";
        bool s1 = false;

        if (s == null)
        {
            return "";
        }

        foreach (object v in s)
        {
            s1 = !s1;

            if (v != null)
            {
                ret += v.ToString() + (s1 ? sep1 : sep2);
            }
        }

        return RemoveLast(ret, s1 ? sep1 : sep2);
    }

    public static string RemoveLast(object s, object delimiter)
    {
        string sx = s.ToString();

        int len = delimiter.ToString().Length;

        if (len > sx.Length)
        {
            return sx;
        }

        return sx.Remove(sx.Length - len, len);
    }

    public static string Quote(object s)
    {
        return "'" + s.ToString() + "'";
    }

    #endregion

    #region Add by Baig
    public static void SetDropDownListByValue(ref DropDownList ddlSelector, string text)
    {

        if (ddlSelector.Items.Count == 0)
            return;

        if (string.IsNullOrEmpty(text))
            return;

        if (ddlSelector.SelectedItem != null)
            ddlSelector.SelectedItem.Selected = false;

        for (int i = 0; i < ddlSelector.Items.Count; i++)
        {
            if (ddlSelector.Items[i].Value == text)
            {
                ddlSelector.SelectedIndex = i;
                ddlSelector.Items[i].Selected = true;
                break;
            }
        }
    }
    public static void SetDropDownListByValue(DropDownList ddlSelector, string text)
    {

        if (ddlSelector.Items.Count == 0)
            return;

        if (string.IsNullOrEmpty(text))
            return;

        if (ddlSelector.SelectedItem != null)
            ddlSelector.SelectedItem.Selected = false;

        for (int i = 0; i < ddlSelector.Items.Count; i++)
        {
            if (ddlSelector.Items[i].Value == text)
            {
                ddlSelector.SelectedIndex = i;
                ddlSelector.Items[i].Selected = true;
                break;
            }
        }
    }


    public static void SetDropDownListByText(ref DropDownList ddlSelector, string text)
    {

        if (ddlSelector.Items.Count == 0)
            return;

        if (string.IsNullOrEmpty(text))
            return;

        if (ddlSelector.SelectedItem != null)
            ddlSelector.SelectedItem.Selected = false;

        for (int i = 0; i < ddlSelector.Items.Count; i++)
        {
            if (ddlSelector.Items[i].Text == text)
            {
                ddlSelector.SelectedIndex = i;
                ddlSelector.Items[i].Selected = true;
                break;
            }
        }
    }

    public static void SetDropDownListByText(DropDownList ddlSelector, string text)
    {

        if (ddlSelector.Items.Count == 0)
            return;

        if (string.IsNullOrEmpty(text))
            return;

        if (ddlSelector.SelectedItem != null)
            ddlSelector.SelectedItem.Selected = false;

        for (int i = 0; i < ddlSelector.Items.Count; i++)
        {
            if (ddlSelector.Items[i].Text == text)
            {
                ddlSelector.SelectedIndex = i;
                ddlSelector.Items[i].Selected = true;
                break;
            }
        }
    }
    #endregion

    #region Kaleem
    public DataSet GetAllUnits()
    {
        return SqlHelper.ExecuteDataset(Connection.SqlConnection, CommandType.StoredProcedure, "sp_GetAllUnits");

    }
    public DataSet GetAllUOM()
    { 
        return SqlHelper.ExecuteDataset(Connection.SqlConnection, CommandType.StoredProcedure, "sp_GetAllUOM");

    }

    #endregion
}
