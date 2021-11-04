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
// Purpose:     This is the master page that will show who is logged in. It contains navigation buttons and the log out button. 

namespace Lab2
{
    public partial class AladinPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create log in aware function in the master page so that we dont have to write for every other page.
            if (Session["username"] != null)
            {
                // Query to get userID, user name to store then in the session variable 
                SqlConnection sqlCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString());
                String sqlQuery = "SELECT UserID, FirstName, LastName from Users where Username = @username";
                SqlCommand sqlCom = new SqlCommand(sqlQuery, sqlCon);

                sqlCom.Parameters.AddWithValue("@username", Session["username"].ToString());

                sqlCon.Open();              
                SqlDataReader read_Tdd = sqlCom.ExecuteReader();
                
                while (read_Tdd.Read())
                {
                    Session["userID"] = read_Tdd.GetValue(0).ToString();
                    Session["firstName"] = read_Tdd.GetValue(1).ToString();
                    Session["lastName"] = read_Tdd.GetValue(2).ToString();
                }
                sqlCon.Close();

                // Fill in this lblLoggedInUser label with the session variable we got.
                LblLoggedInUser.ForeColor = Color.Purple;
                LblLoggedInUser.Font.Bold = true;
                LblLoggedInUser.Text = "Current User: " + Session["firstName"].ToString() + " " + Session["lastName"].ToString();
            }
            else
            {
                // create InvalidUse variable in the session memory and assign the string, this is when a user tries to access a page without logging in.
                Session["InvalidUse"] = "You must first login before accessing this page!";
                Response.Redirect("LoginPg.aspx");
            }         
        }

        // Code to redirect user to different pages of the website       
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            //Pass URL data to notify the login page that user logged out. 
            Response.Redirect("LoginPg.aspx?loggedout=true");
        }

        protected void btnCommons_Click(object sender, EventArgs e)
        {
            Response.Redirect("AnalysisCommons.aspx");
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfilePage.aspx");
        }

        protected void btnStoryText_Click(object sender, EventArgs e)
        {
            Response.Redirect("StoryText.aspx");
        }

        protected void btnAnalysis_Click(object sender, EventArgs e)
        {
            Response.Redirect("AnalysisPg.aspx");
        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("AnalysisDetails.aspx");
        }
    }
}