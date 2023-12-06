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
    public partial class EditInformation : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_name"] == null)
            {
                Response.Redirect("~/SignIn");
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string selectQuery = "SELECT Information FROM tblDropDowns";
            string query = "INSERT INTO [tblDropDowns] (InfoType, Information) VALUES (@InfoType, @Information)";
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
                                if (txtNewLocation.Value.ToString().Trim() == reader["Information"].ToString().Trim())
                                {
                                    usernameTaken = true;
                                    break;
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
                            command.Parameters.AddWithValue("@InfoType", "Facility");
                            command.Parameters.AddWithValue("@Information", txtNewLocation.Value.ToString());

                            int added = command.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                }
                catch (Exception er)
                {
                    //output.InnerText = er.ToString();
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}