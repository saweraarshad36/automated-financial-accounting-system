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

public partial class UpdateProgressUc : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
    }

    public string AssociatedUpdatePanelID
    {
        get { return this.UpdateProgress1.AssociatedUpdatePanelID; }
        set { this.UpdateProgress1.AssociatedUpdatePanelID = value; }
    }

    protected void UpdateProgress1_Load(object sender, EventArgs e)
    {

    }
}
