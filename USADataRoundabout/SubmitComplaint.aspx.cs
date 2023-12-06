using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace USADataRoundabout
{
    public partial class About : Page
    {
        protected string sessionID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_name"] == null)
            {
                Response.Redirect("~/SignIn");
            }
            dropDownPopulator();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string dateSubmitted = DateTime.Now.ToString("yyyy-MM-dd");
            string dateIncidence = dtDate.Value;
            string complaintID = Session["user_name"] + DateTime.Now.ToString("yyyyMMdd");
            complaintID = ComplaintIDChecker(complaintID);
            string complaintSource = ddlSource.Value.ToString();
            string facility = ddlFacility.SelectedValue.ToString();
            string complaintLevel = ddlComplaintImpact.Value.ToString();
            string involvedStakeholders = chkStudent.Checked.ToString().Substring(0, 1) +
                chkFaculty.Checked.ToString().Substring(0, 1) + chkStaff.Checked.ToString().Substring(0, 1) +
                chkAlumni.Checked.ToString().Substring(0, 1) + chkCampusGuest.Checked.ToString().Substring(0, 1) +
                chkCommunityMember.Checked.ToString().Substring(0, 1);
            string protectedClass = chkRace.Checked.ToString().Substring(0, 1) + 
                chkReligion.Checked.ToString().Substring(0, 1) + chkSex.Checked.ToString().Substring(0, 1) + 
                chkPregnancy.Checked.ToString().Substring(0, 1) + chkSexOrient.Checked.ToString().Substring(0, 1) +
                chkGenderIden.Checked.ToString().Substring(0, 1) + chkNational.Checked.ToString().Substring(0, 1) +
                chkAge.Checked.ToString().Substring(0, 1) + chkDisability.Checked.ToString().Substring(0, 1) +
                chkGenetic.Checked.ToString().Substring(0, 1);
            string actionTaken = txtActionTaken.Value.ToString();
            string additionalNotes = txtAddNotes.Value.ToString();
            string resolved = chkResolved.Checked.ToString().Substring(0, 1);
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "INSERT INTO [tblComplaintInfo] (ComplaintID, ComplaintSource, Facility, DateIncidence, DateSubmitted, ComplaintLevel, InvolvedStakeholders, ProtectedClass, ActionTaken, AdditionalNotes, Resolved) VALUES(@ComplaintID, @ComplaintSource, @Facility, @DateIncidence, @DateSubmitted, @ComplaintLevel, @InvolvedStakeholders, @ProtectedClass, @ActionTaken, @AdditionalNotes, @Resolved)";
            string query2 = "SELECT SubmittedComplaints FROM [tblUsers] WHERE Username = @username";
            string query3 = "UPDATE [tblUsers] SET SubmittedComplaints = @Info WHERE Username = @username";
            string submitted = "";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ComplaintID", complaintID);
                        command.Parameters.AddWithValue("@ComplaintSource", complaintSource);
                        command.Parameters.AddWithValue("@Facility", facility);
                        command.Parameters.AddWithValue("@DateIncidence", dateIncidence);
                        command.Parameters.AddWithValue("@DateSubmitted", dateSubmitted);
                        command.Parameters.AddWithValue("@ComplaintLevel", complaintLevel);
                        command.Parameters.AddWithValue("@InvolvedStakeholders", involvedStakeholders);
                        command.Parameters.AddWithValue("@ProtectedClass", protectedClass);
                        command.Parameters.AddWithValue("@ActionTaken", actionTaken);
                        command.Parameters.AddWithValue("@AdditionalNotes", additionalNotes);
                        command.Parameters.AddWithValue("@Resolved", resolved);

                        int added = command.ExecuteNonQuery();
                    }
                    conn.Close();
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query2, conn))
                    {
                        command.Parameters.AddWithValue("@username", Session["user_name"]);
                        SqlDataReader reader = null;

                        reader = command.ExecuteReader();

                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                            {
                                submitted = reader["SubmittedComplaints"].ToString();
                            }
                        }
                    }
                    conn.Close();
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query3, conn))
                    {
                        command.Parameters.AddWithValue("@Info", submitted + complaintID + " ");
                        command.Parameters.AddWithValue("@username", Session["user_name"]);

                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                catch (Exception er)
                {
                    output.InnerText = er.ToString();
                    //txtLocation.Value = dateSubmitted + dateIncidence;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected string ComplaintIDChecker(string complaintID)
        {
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "SELECT ComplaintID FROM [tblComplaintInfo]";
            int i = 0;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlDataReader reader = null;
                SqlCommand command = new SqlCommand(query, conn);
                reader = command.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        i++;
                    }
                }
                string[] categories = new string[i];
                i = 0;
                conn.Close();

                conn.Open();
                reader = null;
                command = new SqlCommand(query, conn);
                reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        categories[i] = reader["ComplaintID"].ToString();
                        i++;
                    }
                }
                conn.Close();
                for (int j = 0; j < categories.Length; j++)
                {
                    if (categories[j].Contains(complaintID))
                    {
                        complaintID = complaintID.Substring(0, Session["user_name"].ToString().Length + 8) + (j + 1).ToString();
                    }
                }
            }
            catch (Exception er)
            {
                //output.InnerText = er.ToString();
            }
            return complaintID;
        }
        protected void dropDownPopulator()
        {
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "SELECT Information FROM [tblDropDowns]";
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlDataReader reader = null;
                SqlCommand command = new SqlCommand(query, conn);
                reader = command.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        ddlFacility.Items.Add(new ListItem(reader["Information"].ToString().Trim().Trim(), reader["Information"].ToString().Trim().Trim()));
                    }
                }
            }
            catch (Exception er)
            {
                //output.InnerText = er.ToString();
            }
        }
    }
}