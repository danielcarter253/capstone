using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//add
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

//Created by:   Dukes Solutions
//Date:         October 19, 2021
// Purpose:     The user can send analysis request and manage recieved analysis.  Users can also view friend's shared analysis
namespace Lab2
{
    public partial class UserProfilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null)
                {
                    // Query to get userID, user name to store then in the session variable 
                    SqlConnection sqlCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString());
                    String sqlQuery = "SELECT  FirstName, LastName, Email, PhoneNumber, MailingAddress, OrgID " +
                        "from Users where Username = @username";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlCon);

                    sqlCommand.Parameters.AddWithValue("@username", Session["username"].ToString());

                    sqlCon.Open();
                    SqlDataReader readUser = sqlCommand.ExecuteReader();

                    while (readUser.Read())
                    {
                        txtFName.Text = readUser.GetValue(0).ToString();
                        txtLName.Text = readUser.GetValue(1).ToString();
                        txtEmail.Text = readUser.GetValue(2).ToString();
                        txtPhoneNum.Text = readUser.GetValue(3).ToString();
                        txtAddress.Text = readUser.GetValue(4).ToString();
                        Session["orgID"] = readUser.GetValue(5).ToString();
                    }
                    sqlCon.Close();

                    if (Session["orgID"] != null)
                    {
                        SqlConnection sqlLabConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3CS"].ConnectionString.ToString());
                        String sqllabQuery = "SELECT  OrgName, OrgBio from Organization where OrgID= @orgID";
                        SqlCommand sqlCommand3 = new SqlCommand(sqllabQuery, sqlLabConnect);

                        sqlCommand3.Parameters.AddWithValue("@orgID", Session["orgID"].ToString());
                        sqlLabConnect.Open();
                        SqlDataReader readOrg = sqlCommand3.ExecuteReader();
                        while (readOrg.Read())
                        {
                            txtOrg.Text = readOrg.GetValue(0).ToString();
                            txtOrgBio.Text = readOrg.GetValue(1).ToString();
                        }

                    }
                    else
                    {
                        txtOrg.Text = "";
                        txtOrgBio.Text = "";
                    }


                }
            }
            else
            {
                
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtFName.Text != "" && txtLName.Text != "")
            {
                SqlConnection sqlConnectLab = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3CS"].ConnectionString.ToString());
                String sqlQuery1 = "UPDATE Organization SET OrgBio=@OrgBio from Organization where OrgID = @orgID;";
                SqlCommand sqlCommand1 = new SqlCommand(sqlQuery1, sqlConnectLab);

                sqlCommand1.Parameters.AddWithValue("@orgID", Session["orgID"].ToString());
                sqlCommand1.Parameters.AddWithValue("@OrgBio", txtOrgBio.Text);

                sqlConnectLab.Open();
                int i = sqlCommand1.ExecuteNonQuery();
                sqlConnectLab.Close();

                SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString());
                String sqlQuery2 = "UPDATE Users SET FirstName = @firstName, LastName =@lastName, MailingAddress =@MailingAddress WHERE userID =@userID;";
                SqlCommand sqlCommand2 = new SqlCommand(sqlQuery2, sqlConnect);

                sqlCommand2.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                sqlCommand2.Parameters.AddWithValue("@firstName", txtFName.Text);
                sqlCommand2.Parameters.AddWithValue("@lastName", txtLName.Text);
                sqlCommand2.Parameters.AddWithValue("@MailingAddress", txtAddress.Text);
                sqlConnect.Open();
                int x = sqlCommand2.ExecuteNonQuery();
                sqlConnect.Close();

                if (i > 0 && x > 0)
                {
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Font.Bold = true;
                    lblStatus.Text = "Information updated :)";
                }
                else
                {
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Font.Bold = true;
                    lblStatus.Text = "Something went wrong!";
                }
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Font.Bold = true;
                lblStatus.Text = "Required field missing!";
            }
        }
    }
}