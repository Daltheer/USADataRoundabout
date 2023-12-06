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
    public partial class EditUsers : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            addUsers.Visible = false;
            if (Session["user_name"] == null)
            {
                Response.Redirect("~/SignIn");
            }
            if (Session["Permissions"].ToString() == "admin")
            {
                addUsers.Visible = true;
            }
        }
        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string selectQuery = "SELECT Username FROM tblUsers";
            string query = "INSERT INTO [tblUsers] (Username, Password, Permissions, CurrentLogin, LastLogin) VALUES (@Username, @Password, @Permissions, @CurrentDate, @LastLogin)";
            string query2 = "INSERT INTO [tblSchools] (SchoolCode, SchoolName) VALUES (@SchoolCode, @SchoolName)";
            bool usernameTaken = false;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(selectQuery, conn))
                    {
                        SqlDataReader reader = null;
                        reader = command.ExecuteReader();
                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                            {
                                if (txtNewUsername.Value.ToString().Trim() == reader["Username"].ToString().Trim())
                                {
                                    usernameTaken = true;
                                    //output.InnerText = "made it";
                                }    
                            }
                        }
                    }
                    conn.Close();
                    if (usernameTaken == false)
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.AddWithValue("@Username", txtNewUsername.Value.ToString().Trim());
                            command.Parameters.AddWithValue("@Password", txtNewTempPassword.Value.ToString());
                            command.Parameters.AddWithValue("@Permissions", ddlPermissions.Value.ToString());
                            command.Parameters.AddWithValue("@CurrentDate", DateTime.Now.ToString("yyyy-MM-dd"));
                            command.Parameters.AddWithValue("@LastLogin", DateTime.Now.ToString("yyyy-MM-dd"));

                            int added = command.ExecuteNonQuery();
                        }
                        conn.Close();
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(query2, conn))
                        {
                            command.Parameters.AddWithValue("@SchoolCode", txtNewUsername.Value.ToString().Trim());
                            command.Parameters.AddWithValue("@SchoolName", txtRepresentedSchool.Value.ToString().Trim());

                            int added = command.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
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
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string selectQuery = "SELECT Password FROM tblUsers WHERE Username = @Username";
            string query = "UPDATE tblUsers SET Password = @Password WHERE Username = @Username";
            bool correctPswd = false;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(selectQuery, conn))
                    {
                        command.Parameters.AddWithValue("Username", Session["user_name"].ToString().Trim());
                        SqlDataReader reader = null;
                        reader = command.ExecuteReader();
                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                            {
                                if (txtOldPassword.Value.ToString() == reader["Password"].ToString().Trim())
                                {
                                    correctPswd = true;
                                }
                            }
                        }
                    }
                    conn.Close();
                    if (correctPswd == true && txtNewPassword.Value.ToString() == txtConfNewPassword.Value.ToString())
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.AddWithValue("@Username", Session["user_name"].ToString().Trim());
                            command.Parameters.AddWithValue("@Password", txtNewPassword.Value.ToString());

                            int added = command.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
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
    }
}