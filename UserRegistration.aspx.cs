using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//addition
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;


//Created by:   Dukes Solutions
//Date:         October 19, 2021
//Purpose:      Create new account. The functionality on this page hashes and stores user password and validates their input. 

namespace Lab2
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void valEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // make sure Regular expression is valid before running this validator --> if statement

            SqlConnection sqlC = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString());
            string selectQuery = "SELECT COUNT(*) FROM Users WHERE email = @email";
            SqlCommand sCommand = new SqlCommand(selectQuery, sqlC);
            sCommand.Parameters.AddWithValue("@email", txtEmail.Text);

            sqlC.Open();
            var emailExists = (Int32)sCommand.ExecuteScalar() > 0;
            sqlC.Close();

            //Check if the email exists.
            if (emailExists)
            {
                valEmail.ForeColor = Color.Red;
                valEmail.Font.Bold = true;
                valEmail.Text = "Email already used!";
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void valUsername_ServerValidate(object source, ServerValidateEventArgs args)
        {
            SqlConnection sqlC = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString());
            string selectQuery = "SELECT COUNT(*) FROM Account WHERE username = @username";
            SqlCommand sCommand = new SqlCommand(selectQuery, sqlC);
            sCommand.Parameters.AddWithValue("@username", txtUsername.Text);

            sqlC.Open();
            var usernameExists = (Int32)sCommand.ExecuteScalar() > 0;
            sqlC.Close();

            //Check if username already exists 
            if (!usernameExists)
            {
                args.IsValid = true;
            }
            else
            {
                valUsername.ForeColor = Color.Red;
                valUsername.Font.Bold = true;
                valUsername.Text = "username taken!";
                args.IsValid = false;
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //check for required fields
            if (txtFirstName.Text != "" && txtLastName.Text != "" && txtPassword.Text != "" && txtUsername.Text != "" && txtEmail.Text != "" && txtPassword.Text != "") 
            {
                // Check for duplicates! (email, username) 
                if (valUsername.IsValid && valEmail.IsValid && cmpPhoneNumber.IsValid)
                {
                    SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString());

                    // Insert statement for creating new user in AUTH database              
                    String sqlQuery = "INSERT INTO Users (FirstName, LastName, Email, PhoneNumber, Username, MailingAddress, OrgID) VALUES " +
                        "(@fName, @lName, @email, @phoneNum, @username, @address, @OrgID); " +
                        "INSERT INTO Account (UserID, Username, UserPassword) VALUES ((select max(UserID) from Users), @username, @password)";

                    SqlCommand sqlCom = new SqlCommand(sqlQuery, sqlConnect);
                    sqlCom.Parameters.AddWithValue("@fName", txtFirstName.Text);
                    sqlCom.Parameters.AddWithValue("@lName", txtLastName.Text);
                    sqlCom.Parameters.AddWithValue("@email", txtEmail.Text);
                    sqlCom.Parameters.AddWithValue("@phoneNum", txtPhoneNumber.Text);
                    sqlCom.Parameters.AddWithValue("@address", txtAddress.Text);

                    // I have inserted an empty row in organization table if users chose the empty it means they dont have an organization.                 
                    sqlCom.Parameters.AddWithValue("@OrgID", ddlOrg.SelectedValue);

                    sqlCom.Parameters.AddWithValue("@username", txtUsername.Text);
                    sqlCom.Parameters.AddWithValue("@password", PasswordHash.HashPassword(txtPassword.Text));

                    sqlConnect.Open();
                    int i = sqlCom.ExecuteNonQuery();
                    sqlConnect.Close();

                    if (i > 0)
                    {
                        lblResult.Text = "Account Created!!!";
                        lblResult.ForeColor = Color.Green;
                        lblResult.Font.Bold = true;
                        txtFirstName.Enabled = false;
                        txtLastName.Enabled = false;
                        txtAddress.Enabled = false;
                        txtConfirmPass.Enabled = false;
                        txtEmail.Enabled = false;
                        ddlOrg.Enabled = false;
                        txtPassword.Enabled = false;
                        txtPhoneNumber.Enabled = false;
                        txtUsername.Enabled = false;
                        SaveButton.Enabled = false;
                        btnAnother.Visible = true;

                    }
                    else
                    {
                        // something went wrong
                        lblResult.ForeColor = Color.Red;
                        lblResult.Font.Bold = true;
                        lblResult.Text = "Error, action failed!!";
                    }
                }
                else
                {
                    lblResult.ForeColor = Color.Red;
                    lblResult.Font.Bold = true;
                    lblResult.Text = "Please provide valid information";
                }
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Font.Bold = true;
                lblResult.Text = "Required field missing!";
            }
        }

            protected void btnLogin_Click(object sender, EventArgs e)
            {
                Response.Redirect("loginPg.aspx");

            }

            protected void btnAnother_Click(object sender, EventArgs e)
            {
                txtFirstName.Enabled = true;
                txtLastName.Enabled = true;
                txtAddress.Enabled = true;
                txtConfirmPass.Enabled = true;
                txtEmail.Enabled = true;
                ddlOrg.Enabled = true;
                txtPassword.Enabled = true;
                txtPhoneNumber.Enabled = true;
                txtUsername.Enabled = true;
                SaveButton.Enabled = true;
                btnAnother.Visible = false;

                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtAddress.Text = "";
                txtConfirmPass.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtPhoneNumber.Text = "";
                txtUsername.Text = "";
                //lblResult.Text = "";
            }

            protected void txtEmail_TextChanged(object sender, EventArgs e)
            {
                regEmail.Enabled = true;
            }
        }
    }