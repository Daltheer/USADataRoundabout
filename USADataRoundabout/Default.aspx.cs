using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace USADataRoundabout
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_name"] == null)
            {
                Response.Redirect("~/SignIn");
            }
            else
            {
                string submComps = "";
                string bookComps = "";
                string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
                string query = "SELECT SubmittedComplaints, BookmarkedComplaints, Permissions, LastLogin FROM [tblUsers] WHERE Username = @username";

                SqlConnection conn = new SqlConnection(connection);
                try
                {
                    conn.Open();
                    SqlDataReader reader = null;
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@username", Session["user_name"]);
                    reader = command.ExecuteReader();

                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            submComps = reader["SubmittedComplaints"].ToString();
                            bookComps = reader["BookmarkedComplaints"].ToString();
                            sinceLastLogin(reader["Permissions"].ToString(), reader["LastLogin"].ToString());
                        }
                    }
                    conn.Close();
                }
                catch (Exception er)
                {

                }
                string[] submCompsArr = submComps.Trim().Split(' ');
                string[] bookCompsArr = bookComps.Trim().Split(' ');
                HtmlGenericControl ddControl;
                for (int i = 0; i < submCompsArr.Count(); i++)
                {
                    ddControl = new HtmlGenericControl("dd");
                    ddControl.InnerHtml = String.Format("<a href=\"/ViewComplaint.aspx?ComplaintID={0}\"><strong>{0}</strong></a>", submCompsArr[i]);
                    dlSubmittedComps.Controls.Add(ddControl);
                }
                for (int i = 0; i < bookCompsArr.Count(); i++)
                {
                    ddControl = new HtmlGenericControl("dd");
                    ddControl.InnerHtml = String.Format("<a href=\"/ViewComplaint.aspx?ComplaintID={0}\"><strong>{0}</strong></a>", bookCompsArr[i]);
                    dlBookmarkedComps.Controls.Add(ddControl);
                }
            }
        }
        protected void sinceLastLogin(string permissions, string lastLogin)
        {
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "";

            if (permissions.Contains("admin"))
            {
                query = "SELECT ComplaintID FROM [tblComplaintInfo] WHERE DateSubmitted >= '" + lastLogin + "'";
            }
            else
            {
                query = "SELECT ComplaintID FROM [tblComplaintInfo] WHERE DateSubmitted >= '" + lastLogin + "' AND ComplaintID LIKE '%" + Session["user_name"] + "%'";
            }

            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlDataReader reader = null;
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", Session["user_name"]);
                reader = command.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        HtmlGenericControl ddControl;
                        ddControl = new HtmlGenericControl("dd");
                        ddControl.InnerHtml = String.Format("<a href=\"/ViewComplaint.aspx?ComplaintID={0}\"><strong>{0}</strong></a>", reader["ComplaintID"].ToString().Trim());
                        dlCompsSinceLastLogin.Controls.Add(ddControl);
                    }
                }
                conn.Close();
            }
            catch (Exception er)
            {
                sinceLastLoginComps.InnerText = er.ToString();
            }
        }
    }
}