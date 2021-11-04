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
// Purpose:     The user can send analysis request and manage recieved analysis.  Users can also view friend's shared analysis

namespace Lab2
{
    public partial class AnalysisPg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // this is to enable the button after a user upload a text and comes back to the page. 
            btnSendReq.Enabled = true;  
        }

        protected void btnSendReq_Click(object sender, EventArgs e)
        {
            // This in required field validator. We created it here to use other button on this page. 
            if(String.IsNullOrEmpty(txtReason.Text))
            {
                lblReqField.Text = "Please enter input in this field!";                                      
            }
            else   
            {
               
                if(ddlTextTitle.Items.Count == 0)
                {
                    // disable the button so that the user can't press it until they upload text and comeback to this page 
                    btnSendReq.Enabled = false;
                    lblPckResult.Text = "Please upload a text";
                }
                else
                {
                    SqlConnection sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3CS"].ConnectionString.ToString());

                    // Create the dummy results and add them in the data base. 
                    String sqlQuery = "INSERT INTO TextAnalysis (AnalysisContent, AnalysisDate, AnalysisReason, TextID) VALUES ('Dummy Result', @sendDate, @Reason, @TextID); ";
                    SqlCommand sqlCom = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCom.Parameters.AddWithValue("@Reason", txtReason.Text);
                    // this value comes from the ddl   
                    sqlCom.Parameters.AddWithValue("@TextID", ddlTextTitle.SelectedValue);
                    sqlCom.Parameters.AddWithValue("@sendDate", DateTime.Today.ToString());

                    sqlConnection.Open();
                    int i = sqlCom.ExecuteNonQuery();
                    sqlConnection.Close();

                    if (i > 0)
                    {
                        // I created this reload command to update the analysis record table after the analysis is created in the table
                        Response.Redirect("AnalysisPg.aspx");  
                    }
                    else
                    {
                        // Show this message in case the command did not work
                        lblPckResult.ForeColor = Color.Red;
                        lblPckResult.Font.Bold = true;
                        lblPckResult.Text = "Sorry, your request was not sent";
                    }
                }
                

            }
        }
       
       
    }
}