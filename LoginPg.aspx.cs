using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Add 
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Drawing;

//Created by:   Dukes Solutions
//Date:         October 19, 2021
// Purpose:     Users can use this page to log in to their accouts.

namespace Lab2
{
    public partial class LoginPg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if user logged out or tried to access a page without logging in.           
            if(Request.QueryString.Get("loggedout")=="true")  
            {
                lblStatus.ForeColor=Color.Green;
                lblStatus.Font.Bold = false;
                lblStatus.Text="User Successfully Logged Out!";
            }
            if(Session["InvalidUse"] != null)
            {
                lblStatus.Text = Session["InvalidUse"].ToString();
                lblStatus.ForeColor = Color.Red;
                lblStatus.Font.Bold = true;
            }               
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // connect to database to retrieve stored password string
            try
            {
                System.Data.SqlClient.SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString());

                System.Data.SqlClient.SqlCommand findPass = new System.Data.SqlClient.SqlCommand();
                findPass.Connection = sc;
                // read the password string if the username exists and match the password in the asp.net
                // use stored procedures to get search for username 
                findPass.CommandType = CommandType.StoredProcedure;
                findPass.CommandText = "JeremyEzellLab3";
                findPass.Parameters.Add(new SqlParameter("@Username", HttpUtility.HtmlEncode(txtUsername.Text)));

                sc.Open();
                SqlDataReader reader = findPass.ExecuteReader(); // create a reader

                if (reader.HasRows) // if the username exists, it will continue
                {
                    while (reader.Read()) // this will read the single record that matches the entered username
                    {
                        string storedHash = reader["UserPassword"].ToString(); // store the database password into this variable

                        if (PasswordHash.ValidatePassword(HttpUtility.HtmlEncode(txtPassword.Text), storedHash)) // if the entered password matches what is stored, it will show success
                        {
                            //HttpUtility.HtmlEncode(txtPassword.Text)
                            Session["username"] = HttpUtility.HtmlEncode(txtUsername.Text);
                            Response.Redirect("UserProfilePage.aspx");
                        }
                        else
                            lblStatus.Text = "Incorrect Username or Password! Please try again!";
                    }
                }
                else // if the username doesn't exist, it will show failure
                    lblStatus.Text = "Login failed.";

                sc.Close();
            }
            catch
            {
                lblStatus.Text = "Database Error.";
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserRegistration.aspx");
        }
    }
}