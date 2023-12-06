using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI.DataVisualization.Charting;

namespace USADataRoundabout
{
    public partial class Contact : Page
    {
        bool schAlreadyGenned = false;
        bool pgAlreadyGenned = false;
        bool facAlreadyGenned = false;
        protected string[,] tableInfo;
        //protected string tableInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_name"] == null)
            {
                Response.Redirect("~/SignIn");
            }
            SchoolTotals.Visible = false;
            ProtGroupsTotals.Visible = false;
            FacilityTotals.Visible = false;
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            //output.InnerText += ddlSortBy.Value + ddlDisplay.Value;
            switch(ddlDisplay.Value)
            {
                case "Pie":
                    this.chrtGraph.Series[0].ChartType = SeriesChartType.Pie;
                    break;
                case "Bar":
                    this.chrtGraph.Series[0].ChartType = SeriesChartType.Column;
                    break;
                default:
                    //how did you get here?
                    break;
            }
            selectStatement();
        }
        protected void selectStatement()
        {
            string schools = "";
            string protClass = "";
            string facility = "";
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
            string query = "SELECT ComplaintID, ProtectedClass, Facility, DateSubmitted FROM [tblComplaintInfo] WHERE DateIncidence BETWEEN '" + dteStart.Value + "' AND '" + dteEnd.Value + "'";
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
                        string dateSubmitted = reader["DateSubmitted"].ToString().Trim() + " ";
                        int index = dateSubmitted.IndexOf(" ");
                        dateSubmitted = dateSubmitted.Substring(0, index);
                        string temp = reader["ComplaintID"].ToString().Trim();
                        temp = temp.Substring(0, (temp.Length - dateSubmitted.Length + 1));
                        int tempn;
                        bool check = int.TryParse(temp.Substring((temp.Length - 1), 1), out tempn);
                        if (check)
                        {
                            temp = temp.Substring(0, temp.Length - 1);
                        }
                        schools += temp + " ";
                        protClass += reader["ProtectedClass"].ToString().Trim() + " ";
                        facility += reader["Facility"].ToString().Trim() + "|";
                    }
                }
            }
            catch (Exception er)
            {
                //output.InnerText += er.ToString();
            }
            switch (ddlSortBy.Value)
            {
                case "School":
                    schoolGroup(schools);
                    break;
                case "ProtGroup":
                    protGroupsGroup(protClass);
                    break;
                case "Facility":
                    facilityGroup(facility);
                    break;
                default:
                    //how did you get here?
                    break;
            }
        }
        protected void schoolGroup(string schools)
        {
            string[] schoolArr = schools.Split(' ');
            string[] uniqueSchools = schoolArr.Distinct().ToArray();
            int[] uniqueCount = new int[uniqueSchools.Count()];
            for (int i = 0; i < uniqueCount.Count(); i++)
            {
                uniqueCount[i] = 0;
            }
            for (int i = 0; i < uniqueSchools.Count(); i++)
            {
                for (int j = 0; j < schoolArr.Count(); j++)
                {
                    if (uniqueSchools[i] == schoolArr[j])
                    {
                        uniqueCount[i] += 1;
                    }
                }
            }
            if (pgAlreadyGenned == false)
            {
                string codeHold = "";
                string schoolHold = "";
                string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daltheer\Downloads\USADataRoundabout\USADataRoundabout\App_Data\Database.mdf;Integrated Security=True";
                string query = "SELECT SchoolCode, SchoolName FROM [tblSchools]";
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
                            codeHold += reader["SchoolCode"].ToString().Trim() + "|";
                            schoolHold += reader["SchoolName"].ToString().Trim() + "|";
                        }
                    }
                }
                catch (Exception er)
                {
                    //output.InnerText += er.ToString();
                }

                string[] codeArr = codeHold.Split('|');
                string[] namesArr = schoolHold.Split('|');
                string[] replacement = new string[uniqueSchools.Count()];
                for (int i = 0; i < uniqueSchools.Count(); i++)
                {
                    for (int j = 0; j < codeArr.Count(); j++)
                    {
                        if (uniqueSchools[i].Trim() == codeArr[j].Trim())
                        {
                            replacement[i] = namesArr[j].Trim();
                        }
                    }
                }

                HtmlGenericControl liControl;
                for (int i = 0; i < uniqueSchools.Count() - 1; i++)
                {
                    this.chrtGraph.Series[0].Points.AddXY(uniqueSchools[i], uniqueCount[i]);
                    liControl = new HtmlGenericControl("li");
                    liControl.InnerHtml = replacement[i] + ": " + uniqueCount[i].ToString();
                    ulSchool.Controls.Add(liControl);
                }
                pgAlreadyGenned = true;
            }
            else
            {
                for (int i = 0; i < uniqueSchools.Count() - 1; i++)
                {
                    this.chrtGraph.Series[0].Points.AddXY(uniqueSchools[i], uniqueCount[i]);
                }
            }
            SchoolTotals.Visible = true;
            ProtGroupsTotals.Visible = false;
            FacilityTotals.Visible = false;
        }
        protected void protGroupsGroup(string protClass)
        {
            string[] uniqueProts = { "Race", "Religion", "Sex", "Pregnancy", "Sexual Orientation", "Gender Identity", "National Origin", "Age (40 or Older)", "Disability", "Genetic Information" };

            string[] protGroups = protClass.Split(' ');
            int[] uniqueCount = new int[uniqueProts.Count()];
            for (int i = 0; i < uniqueCount.Count(); i++)
            {
                uniqueCount[i] = 0;
            }
            for (int i = 0; i < protGroups.Count(); i++)
            {
                for (int j = 0; j < protGroups[i].Length; j++)
                {
                    if (protGroups[i].Substring(j, 1) == "T")
                    {
                        uniqueCount[j] += 1;
                    }
                }
            }
            if (pgAlreadyGenned == false)
            {
                HtmlGenericControl liControl;
                for (int i = 0; i < uniqueProts.Count(); i++)
                {
                    this.chrtGraph.Series[0].Points.AddXY(uniqueProts[i], uniqueCount[i]);
                    liControl = new HtmlGenericControl("li");
                    liControl.InnerHtml = uniqueProts[i] + ": " + uniqueCount[i].ToString();
                    ulProtGroup.Controls.Add(liControl);
                }
                pgAlreadyGenned = true;
            }
            else
            {
                for (int i = 0; i < uniqueProts.Count(); i++)
                {
                    this.chrtGraph.Series[0].Points.AddXY(uniqueProts[i], uniqueCount[i]);
                }
            }
            SchoolTotals.Visible = false;
            ProtGroupsTotals.Visible = true;
            FacilityTotals.Visible = false;
        }
        protected void facilityGroup(string facility)
        {
            string[] factilityArr = facility.Split('|');
            string[] uniqueFacilities = factilityArr.Distinct().ToArray();
            int[] uniqueCount = new int[uniqueFacilities.Count()];
            for (int i = 0; i < uniqueCount.Count(); i++)
            {
                uniqueCount[i] = 0;
            }
            for (int i = 0; i < uniqueFacilities.Count(); i++)
            {
                for (int j = 0; j < factilityArr.Count(); j++)
                {
                    if (uniqueFacilities[i] == factilityArr[j])
                    {
                        uniqueCount[i] += 1;
                    }
                }
            }
            if (pgAlreadyGenned == false)
            {
                HtmlGenericControl liControl;
                for (int i = 0; i < uniqueFacilities.Count() - 1; i++)
                {
                    this.chrtGraph.Series[0].Points.AddXY(uniqueFacilities[i], uniqueCount[i]);
                    liControl = new HtmlGenericControl("li");
                    liControl.InnerHtml = uniqueFacilities[i] + ": " + uniqueCount[i].ToString();
                    ulFacility.Controls.Add(liControl);
                }
                pgAlreadyGenned = true;
            }
            else
            {
                for (int i = 0; i < uniqueFacilities.Count() - 1; i++)
                {
                    this.chrtGraph.Series[0].Points.AddXY(uniqueFacilities[i], uniqueCount[i]);
                }
            }
            SchoolTotals.Visible = false;
            ProtGroupsTotals.Visible = false;
            FacilityTotals.Visible = true;
        }
    }
}