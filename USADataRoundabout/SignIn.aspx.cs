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
    public partial class SignIn : Page
    {
        protected string genSessionID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            loginFailure.Visible = false;
            loginSuccess.Visible = false;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "SELECT Username, Password, Permissions FROM [tblUsers]";
            bool loginFail = true;
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
                        if (txtUsername.Value.ToString().Trim() == reader["Username"].ToString().Trim() && txtPassword.Value.ToString() == reader["Password"].ToString())
                        {
                            Session["user_name"] = txtUsername.Value.ToString().Trim();
                            Session["Permissions"] = reader["Permissions"].ToString().Trim();
                            updateLastLogin(txtUsername.Value.ToString().Trim());
                            loginSuccess.Visible = true;
                            loginFail = false;
                            break;
                        }
                        else
                        {
                            Session["user_name"] = null;
                        }
                    }
                }
                conn.Close();
                if (loginFail == true)
                {
                    loginFailure.Visible = true;
                }
            }
            catch (Exception er)
            {
                //output.InnerText += "UHHHHHHHHOHHHHHHH";
            }
        }
        protected string getLastLogin(string username)
        {
            string lastLogin = "2000-01-01";

            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "SELECT CurrentLogin FROM [tblUsers] WHERE Username = @username";

            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlDataReader reader = null;
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@username", username);

                    reader = command.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            lastLogin = reader["CurrentLogin"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception er)
            {

            }
            return lastLogin;
        }
        protected void updateLastLogin(string username)
        {
            string lastDate = getLastLogin(username);
            Random rnd = new Random();
            for (int i = 0; i < rnd.Next(1,100); i++)
            {
                genSessionID = rnd.Next(1, 1000000).ToString();
            }
            
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "UPDATE [tblUsers] SET CurrentLogin = @Today, LastLogin = @LastLogin WHERE Username = @Login";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Login", username);
                        command.Parameters.AddWithValue("@Today", DateTime.Now.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@LastLogin", lastDate);

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
            Session["user_name"] = txtUsername.Value;
            //output.InnerText = "Success" + genSessionID;
        }
    }
}