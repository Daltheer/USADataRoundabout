using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace USADataRoundabout
{
    public partial class ViewComplaint : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_name"] == null)
            {
                Response.Redirect("~/SignIn");
            }
            else
            {
                complaintIDTitle.InnerText = Request.QueryString["ComplaintID"];
                btnRemoveBookmark.Visible = false;
                btnBookmark.Visible = false;
                populateView();
            }
        }
        protected void btnBookmark_Click(object sender, EventArgs e)
        {
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "SELECT BookmarkedComplaints FROM [tblUsers] WHERE Username = @username";
            string query2 = "UPDATE [tblUsers] SET BookmarkedComplaints = @Info WHERE Username = @username";
            string bookmarked = "";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@username", Session["user_name"]);
                        SqlDataReader reader = null;

                        reader = command.ExecuteReader();

                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                            {
                                bookmarked = reader["BookmarkedComplaints"].ToString();
                            }
                        }
                    }
                    conn.Close();
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query2, conn))
                    {
                        command.Parameters.AddWithValue("@Info", bookmarked + complaintIDTitle.InnerText.ToString() + " ");
                        command.Parameters.AddWithValue("@username", Session["user_name"]);

                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                catch (Exception er)
                {
                    //output.InnerText = er.ToString();
                    //txtLocation.Value = dateSubmitted + dateIncidence;
                }
                finally
                {
                    conn.Close();
                }
                btnBookmark.Visible = false;
                btnRemoveBookmark.Visible = true;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string dateIncidence = dtDate.Value;
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
            string query = "UPDATE [tblComplaintInfo] SET ComplaintSource = @ComplaintSource, Facility = @Facility, DateIncidence = @DateIncidence, ComplaintLevel = @ComplaintLevel, InvolvedStakeholders = @InvolvedStakeholders, ProtectedClass = @ProtectedClass, ActionTaken = @ActionTaken, AdditionalNotes = @AdditionalNotes, Resolved = @Resolved WHERE ComplaintID = @ComplaintID";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ComplaintID", complaintIDTitle.InnerText.ToString());
                        command.Parameters.AddWithValue("@ComplaintSource", complaintSource);
                        command.Parameters.AddWithValue("@Facility", facility);
                        command.Parameters.AddWithValue("@DateIncidence", dateIncidence);
                        command.Parameters.AddWithValue("@ComplaintLevel", complaintLevel);
                        command.Parameters.AddWithValue("@InvolvedStakeholders", involvedStakeholders);
                        command.Parameters.AddWithValue("@ProtectedClass", protectedClass);
                        command.Parameters.AddWithValue("@ActionTaken", actionTaken);
                        command.Parameters.AddWithValue("@AdditionalNotes", additionalNotes);
                        command.Parameters.AddWithValue("@Resolved", resolved);

                        int added = command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                catch (Exception er)
                {
                    //output.InnerText = er.ToString();
                    //txtLocation.Value = dateSubmitted + dateIncidence;
                }
                finally
                {
                    conn.Close();
                }
            }
            Response.Redirect("~/ViewComplaint.aspx?ComplaintID=" + complaintIDTitle.InnerText.ToString());
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            editSection.Visible = true;
            btnEdit.Visible = false;
            btnCancel.Visible = true;
            viewSection.Visible = false;
            editButton.Visible = true;

            string complaintID = complaintIDTitle.InnerHtml.ToString();

            string dateSubmitted = "";
            string dateIncidence = "";
            string complaintSource = "";
            string facility = "";
            string complaintLevel = "";
            string involvedStakeholders = "";
            string protectedClass = "";
            string actionTaken = "";
            string additionalNotes = "";
            string resolved = "";

            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "SELECT * FROM [tblComplaintInfo] WHERE ComplaintID = @complaintID";
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlDataReader reader = null;
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@complaintID", complaintID);
                reader = command.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        complaintSource = reader["ComplaintSource"].ToString();
                        facility = reader["Facility"].ToString();
                        dateIncidence = DateTime.Parse(reader["DateIncidence"].ToString()).ToString("yyyy-MM-dd");
                        dateSubmitted = reader["DateSubmitted"].ToString();
                        complaintLevel = reader["ComplaintLevel"].ToString();
                        involvedStakeholders = reader["InvolvedStakeholders"].ToString();
                        protectedClass = reader["ProtectedClass"].ToString();
                        actionTaken = reader["ActionTaken"].ToString();
                        additionalNotes = reader["AdditionalNotes"].ToString();
                        resolved = reader["Resolved"].ToString();
                    }
                }
                conn.Close();
            }
            catch (Exception er)
            {
                //output.InnerText += er.ToString();
            }
            dropDownPopulator();
            ddlFacility.SelectedIndex = ddlFacility.Items.IndexOf(ddlFacility.Items.FindByValue(facility.Trim()));
            ddlComplaintImpact.SelectedIndex = ddlComplaintImpact.Items.IndexOf(ddlComplaintImpact.Items.FindByValue(complaintLevel.Trim()));
            txtAddNotes.Value = additionalNotes;
            dtDate.Value = dateIncidence;
            ddlSource.SelectedIndex = ddlSource.Items.IndexOf(ddlSource.Items.FindByValue(complaintSource.Trim()));
            txtActionTaken.Value = actionTaken;
            if (protectedClass.Substring(0, 1) == "T")
            {
                chkRace.Checked = true;
            }
            if (protectedClass.Substring(1, 1) == "T")
            {
                chkReligion.Checked = true;
            }
            if (protectedClass.Substring(2, 1) == "T")
            {
                chkSex.Checked = true;
            }
            if (protectedClass.Substring(3, 1) == "T")
            {
                chkPregnancy.Checked = true;
            }
            if (protectedClass.Substring(4, 1) == "T")
            {
                chkSexOrient.Checked = true;
            }
            if (protectedClass.Substring(5, 1) == "T")
            {
                chkGenderIden.Checked = true;
            }
            if (protectedClass.Substring(6, 1) == "T")
            {
                chkNational.Checked = true;
            }
            if (protectedClass.Substring(7, 1) == "T")
            {
                chkAge.Checked = true;
            }
            if (protectedClass.Substring(8, 1) == "T")
            {
                chkDisability.Checked = true;
            }
            if (protectedClass.Substring(9, 1) == "T")
            {
                chkGenetic.Checked = true;
            }
            if (involvedStakeholders.Substring(0, 1) == "T")
            {
                chkStudent.Checked = true;
            }
            if (involvedStakeholders.Substring(1, 1) == "T")
            {
                chkFaculty.Checked = true;
            }
            if (involvedStakeholders.Substring(2, 1) == "T")
            {
                chkStaff.Checked = true;
            }
            if (involvedStakeholders.Substring(3, 1) == "T")
            {
                chkAlumni.Checked = true;
            }
            if (involvedStakeholders.Substring(4, 1) == "T")
            {
                chkCampusGuest.Checked = true;
            }
            if (involvedStakeholders.Substring(5, 1) == "T")
            {
                chkCommunityMember.Checked = true;
            }
            if (resolved.Substring(0, 1) == "T")
            {
                chkResolved.Checked = true;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewComplaint.aspx?ComplaintID=" + complaintIDTitle.InnerText.ToString());
        }
        protected void populateView()
        {
            string complaintID = complaintIDTitle.InnerText.ToString();

            string dateSubmitted = "";
            string dateIncidence = "";
            string complaintSource = "";
            string facility = "";
            string complaintLevel = "";
            string involvedStakeholders = "";
            string protectedClass = "";
            string actionTaken = "";
            string additionalNotes = "";
            string resolved = "";

            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "SELECT * FROM [tblComplaintInfo] WHERE ComplaintID = @complaintID";
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlDataReader reader = null;
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@complaintID", complaintID);
                reader = command.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        complaintSource = reader["ComplaintSource"].ToString();
                        facility = reader["Facility"].ToString();
                        dateIncidence = reader["DateIncidence"].ToString();
                        dateSubmitted = reader["DateSubmitted"].ToString();
                        complaintLevel = reader["ComplaintLevel"].ToString();
                        involvedStakeholders = reader["InvolvedStakeholders"].ToString();
                        protectedClass = reader["ProtectedClass"].ToString();
                        actionTaken = reader["ActionTaken"].ToString();
                        additionalNotes = reader["AdditionalNotes"].ToString();
                        resolved = reader["Resolved"].ToString();
                    }
                }
                conn.Close();
            }
            catch (Exception er)
            {
                //output.InnerText += er.ToString();
            }
            int index = dateSubmitted.IndexOf(" ");
            originalDate.InnerText = dateSubmitted.Substring(0, index);
            txtFacilityView.Value = facility;
            txtCompImpactView.Value = complaintLevel;
            txtAddNotesView.Value = additionalNotes;
            index = dateIncidence.IndexOf(" ");
            txtDateIncView.Value = dateIncidence.Substring(0, index);
            txtSourceCompView.Value = complaintSource;
            txtActionTakenView.Value = actionTaken;
            HtmlGenericControl liControl;
            for (int i = 0; i < protectedClass.Substring(0, 10).Length; i++)
            {
                liControl = new HtmlGenericControl("li");
                switch (i)
                {
                    case 0:
                        liControl.InnerHtml = "Race";
                        break;
                    case 1:
                        liControl.InnerHtml = "Religion";
                        break;
                    case 2:
                        liControl.InnerHtml = "Sex";
                        break;
                    case 3:
                        liControl.InnerHtml = "Pregnancy";
                        break;
                    case 4:
                        liControl.InnerHtml = "Sexual Orientation";
                        break;
                    case 5:
                        liControl.InnerHtml = "Gender Identity";
                        break;
                    case 6:
                        liControl.InnerHtml = "National Origin";
                        break;
                    case 7:
                        liControl.InnerHtml = "Age (40 or Older)";
                        break;
                    case 8:
                        liControl.InnerHtml = "Disability";
                        break;
                    case 9:
                        liControl.InnerHtml = "Genetic Information";
                        break;
                    default:
                        break;
                }
                if (protectedClass.Substring(i, 1) == "T")
                {
                    ulProtClassView.Controls.Add(liControl);
                }
            }
            for (int i = 0; i < involvedStakeholders.Substring(0, 6).Length; i++)
            {
                liControl = new HtmlGenericControl("li");
                switch (i)
                {
                    case 0:
                        liControl.InnerHtml = "Student";
                        break;
                    case 1:
                        liControl.InnerHtml = "Faculty";
                        break;
                    case 2:
                        liControl.InnerHtml = "Staff";
                        break;
                    case 3:
                        liControl.InnerHtml = "Alumni";
                        break;
                    case 4:
                        liControl.InnerHtml = "Campus Guest";
                        break;
                    case 5:
                        liControl.InnerHtml = "Community Member";
                        break;
                    default:
                        break;
                }
                if (involvedStakeholders.Substring(i, 1) == "T")
                {
                    ulStakeholdersView.Controls.Add(liControl);
                }
            }
            switch (resolved)
            {
                case "T":
                    chkResolvedView.Checked = true;
                    break;
                case "F":
                    chkResolvedView.Checked = false;
                    break;
                default:
                    break;
            }
            editSection.Visible = false;
            btnEdit.Visible = true;
            btnCancel.Visible = false;
            viewSection.Visible = true;
            editButton.Visible = false;
            checkBookmark();
        }
        protected void checkBookmark()
        {
            string bookmarks = "";
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "SELECT BookmarkedComplaints FROM [tblUsers] WHERE Username = @username";
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
                        bookmarks = reader["BookmarkedComplaints"].ToString();
                    }
                }
                conn.Close();
            }
            catch (Exception er)
            {
                //output.InnerText += er.ToString();
            }
            string[] bookmarksArr = bookmarks.Trim().Split(' ');
            bool isBookmarked = false;
            for (int i = 0; i < bookmarksArr.Count(); i++)
            {
                if (complaintIDTitle.InnerText.ToString().Trim() == bookmarksArr[i].Trim())
                {
                    btnRemoveBookmark.Visible = true;
                    isBookmarked = true;
                }
            }
            if (isBookmarked == false)
            {
                btnBookmark.Visible = true;
            }
        }
        protected void btnRemoveBookmark_Click(object sender, EventArgs e)
        {
            string bookmarks = "";
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "SELECT BookmarkedComplaints FROM [tblUsers] WHERE Username = @username";
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
                        bookmarks = reader["BookmarkedComplaints"].ToString();
                    }
                }
                conn.Close();
            }
            catch (Exception er)
            {
                //output.InnerText += er.ToString();
            }
            string[] bookmarksArr = bookmarks.Trim().Split(' ');
            bookmarks = "";
            for (int i = 0; i < bookmarksArr.Count(); i++)
            {
                if (complaintIDTitle.InnerText.ToString().Trim() != bookmarksArr[i].Trim())
                {
                    bookmarks += bookmarksArr[i] + " ";
                }
            }
            string query2 = "UPDATE [tblUsers] SET BookmarkedComplaints = @Info WHERE Username = @username";
            using (conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query2, conn))
                    {
                        command.Parameters.AddWithValue("@Info", bookmarks);
                        command.Parameters.AddWithValue("@username", Session["user_name"]);

                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                catch (Exception er)
                {
                    //output.InnerText = er.ToString();
                    //txtLocation.Value = dateSubmitted + dateIncidence;
                }
                finally
                {
                    conn.Close();
                }
            }
            Response.Redirect("~/ViewComplaint.aspx?ComplaintID=" + complaintIDTitle.InnerText.ToString());
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