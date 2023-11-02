using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace USADataRoundabout
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lstProtList.Visible = false;
            lstProtSel.Visible = false;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //string strSQL = "INSERT INTO tblComplaints (Location, Incidence, Submitted, Facility, Source, Impact, Involved, Related, Action, Protected, Notes, Resolved)" +
            //    "VALUES (" + txtLocation.Value + ", " + dtDate.Value + ", " + txtWhoSubm.Value + ", " + ddlFacility.Value + ", " + txtSourceComplaint.Value + ", " + ddlComplaintImpact.Value + ", " + txtInvolved.Value + ", " + txtRelSchools.Value + ", " + txtActionTaken.Value + ", " + chkProtClass.Checked + ", " + txtAddNotes.Value + ", " + chkProtClass.Checked + ")";
        }
        protected void chkProtClass_OnChange(object sender, EventArgs e)
        {
            if (chkProtClass.Checked == true)
            {
                lstProtList.Visible = true;
                lstProtSel.Visible = true;
            }
            else
            {
                lstProtList.Visible = false;
                lstProtSel.Visible = true;
            }
        }
    }
}