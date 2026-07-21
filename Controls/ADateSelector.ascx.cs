using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;

public partial class ADateSelector : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public string Text
    {
        get
        {
            return txtDate.Text;
        }
        set
        {
            txtDate.Text = value;
        }
    }

    public Unit Width
    {
        get
        {
            return txtDate.Width;
        }
        set
        {
            txtDate.Width = value;
        }
    }

    public string GetText()
    {
        if (txtDate.Text != "")
        {
            return txtDate.Text;    //UType.FormatYYYYMMDD(txtDate.Text);
        }

        return "";
    }

    public string SetText(string textDate)
    {
        if (textDate != "")
        {
            txtDate.Text = textDate;    //UType.FormatDDMMYYYY(textDate);
        }

        return txtDate.Text;
    }
    public bool Enabled
    {
        set
        {
            txtDate.ReadOnly = !value;
            imgCalendar.Enabled = value;
        }
    }
    public bool IsRequired
    {
        set
        {
            vldDate.Enabled = value;
        }
    }

    public string SetCssClass
    {
        set
        {
            txtDate.CssClass = value;
        }
    }

    public FontUnit SetFontSize
    {
        set
        {
            txtDate.Font.Size = value;
        }
    }
}
