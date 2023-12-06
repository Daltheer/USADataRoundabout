using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace USADataRoundabout
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string isAdmin = ((Session["Permissions"] != null) ? Session["Permissions"].ToString() : "0");
            if (isAdmin == "0" || !(isAdmin=="admin"))
            {
                titleEditInfo.Visible = false;
            }
        }
    }
}