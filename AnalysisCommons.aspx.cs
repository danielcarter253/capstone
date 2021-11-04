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
//Purpose:      User can use the this page to send and accept friend request. They can also share their analysis usigng this page. 

namespace Lab2
{
    public partial class AnalysisCommons : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            SqlConnection Lab3SC2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3CS"].ConnectionString.ToString());

            // accept friend request by changing the status to 'done' we will use this status to disply the friend list            
            String sqlQAcceptInv = "UPDATE Friendship SET AcceptDate =@acceptDate, InviteStatus = 'done' WHERE ReceiverUsername=@receiver AND SenderUsername = @sender;";
            SqlCommand acceptInv = new SqlCommand(sqlQAcceptInv, Lab3SC2);

            acceptInv.Parameters.AddWithValue("@receiver", Session["username"].ToString());
            acceptInv.Parameters.AddWithValue("@sender", ddlInvitation.SelectedValue);
            // we dont need the time to keep record of the friendship
            acceptInv.Parameters.AddWithValue("@acceptDate", DateTime.Today.ToString());

            Lab3SC2.Open();
            int i = acceptInv.ExecuteNonQuery();
            Lab3SC2.Close();

            if (i > 0)
            {
                lblAcptResult.ForeColor = Color.Green;
                lblAcptResult.Font.Bold = true;
                lblAcptResult.Text = "Accepted";
            }
            else
            {
                lblAcptResult.ForeColor = Color.Red;
                lblAcptResult.Font.Bold = true;
                lblAcptResult.Text = "No invitation to accept!";
            }
        }

        protected void btnShare_Click(object sender, EventArgs e)
        {
            // conditional logical OR operator false if both condition are true        
            //bool x = (ddlShareAnalysis.Items.Count == 0 && ddlShareFr.Items.Count == 0);            
            if (ddlShareFr.Items.Count == 0 || ddlShareAnalysis.Items.Count == 0 || String.IsNullOrEmpty(txtShareReason.Text))
            {
                lblShareConfirm.ForeColor = Color.Red;
                lblShareConfirm.Font.Bold = true;
                lblShareConfirm.Text = "required field empty!";
            }
            else
            {
                SqlConnection Lab3SC3 = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3CS"].ConnectionString.ToString());

                // Check if the analysis was shared or not. 
                String sqlQcheckDup = "SELECT COUNT(1) FROM SharedAnalysis WHERE AnalysisID = @analysisID AND sender= @sender AND recipient = @recipient;";

                SqlCommand checkShareDup = new SqlCommand(sqlQcheckDup, Lab3SC3);

                checkShareDup.Parameters.AddWithValue("@analysisID", ddlShareAnalysis.SelectedValue);
                checkShareDup.Parameters.AddWithValue("@sender", Session["username"].ToString());
                checkShareDup.Parameters.AddWithValue("@recipient", ddlShareFr.SelectedValue);

                Lab3SC3.Open();
                // get results and convert it into int32 
                int count = Convert.ToInt32(checkShareDup.ExecuteScalar());
                Lab3SC3.Close();


                // had to change it to !=0 because the count was returning numbers other than 1 (7,8,9)
                if (count != 0)
                {
                    lblShareConfirm.ForeColor = Color.Red;
                    lblShareConfirm.Font.Bold = true;
                    lblShareConfirm.Text = "previously shared!";
                }
                else
                {
                    // accept friend request by changing the status to 'done' we will use this status to disply the friend list            
                    String sqlQShare = "INSERT INTO SharedAnalysis (AnalysisID, sender, recipient, ShareDate, ShareReason) VALUES (@analysisID, @username, @recipient, @shareDate, @ShareReason );";
                    SqlCommand cmdShare = new SqlCommand(sqlQShare, Lab3SC3);

                    cmdShare.Parameters.AddWithValue("@analysisID", ddlShareAnalysis.SelectedValue);
                    cmdShare.Parameters.AddWithValue("@username", Session["username"].ToString());
                    cmdShare.Parameters.AddWithValue("@recipient", ddlShareFr.SelectedValue);
                    cmdShare.Parameters.AddWithValue("@shareDate", DateTime.Today.ToString());
                    cmdShare.Parameters.AddWithValue("@ShareReason", txtShareReason.Text);

                    Lab3SC3.Open();
                    int i = cmdShare.ExecuteNonQuery();
                    Lab3SC3.Close();
                    if (i > 0)
                    {
                        lblShareConfirm.ForeColor = Color.Green;
                        lblShareConfirm.Font.Bold = true;
                        lblShareConfirm.Text = "Analysis Shared";
                    }
                    else
                    {
                        lblShareConfirm.ForeColor = Color.Red;
                        lblShareConfirm.Font.Bold = true;
                        lblShareConfirm.Text = "error";
                    }
                }


            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblInviteResult.Text = "";
            txtSrResult.Text = "";
            lblMessage.Text = "";
            btnSend.Enabled = false;

            if (txtFrEmail.Text == String.Empty)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Font.Bold = true;
                lblMessage.Text = "Field Empty!";
            }
            else
            {
                SqlConnection AUTHSC = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString());
                // Check if the analysis was shared or not. 
                String sqlQuery = "SELECT username FROM Users WHERE email = @email;";
                SqlCommand findFriend = new SqlCommand(sqlQuery, AUTHSC);

                findFriend.Parameters.AddWithValue("@email", txtFrEmail.Text);

                AUTHSC.Open();
                SqlDataReader read_Tdd = findFriend.ExecuteReader();
                
                while (read_Tdd.Read())
                {
                    txtSrResult.Text = read_Tdd.GetValue(0).ToString();                    
                }
                AUTHSC.Close();

                if (String.IsNullOrEmpty(txtSrResult.Text))
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Font.Bold = true;
                    lblMessage.Text = "email does not exist";
                }
                else
                {
                    btnSend.Enabled = true;
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            SqlConnection Lab3SC = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3CS"].ConnectionString.ToString());
            // Check whether the logged in user has pending request or friends. 
            String sqlQcheckDup = "SELECT COUNT(1) FROM Friendship " +
                "WHERE(SenderUsername = @sender AND receiverUsername = @receiver) OR (SenderUsername = @receiver AND receiverUsername = @sender)";

            SqlCommand checkInvDuplicate = new SqlCommand();
            checkInvDuplicate.Connection = Lab3SC;
            checkInvDuplicate.CommandText = sqlQcheckDup;
            // Difine Paramenters 
            checkInvDuplicate.Parameters.AddWithValue("@sender", Session["username"].ToString());
            checkInvDuplicate.Parameters.AddWithValue("@receiver", txtSrResult.Text);

            Lab3SC.Open();
            // get results and convert it into int32 
            int count = Convert.ToInt32(checkInvDuplicate.ExecuteScalar());
            Lab3SC.Close();

            if (count == 1)
            {
                lblInviteResult.ForeColor = Color.Red;
                lblInviteResult.Font.Bold = true;
                lblInviteResult.Text = "Friend request or frienship exists!";
                btnSend.Enabled = false;

            }
            else
            {
                // if the friend request or friednship does not exist then insert friend request in the friendship table
                // sql code for inserting new friendship with 'pending' as the status. 
                String sqlQSendInv = "INSERT INTO Friendship (SenderUsername, ReceiverUsername, acceptDate, InviteStatus) VALUES (@sender, @receiver, '', 'pending'); ";
                System.Data.SqlClient.SqlCommand sendRequest = new System.Data.SqlClient.SqlCommand();

                // use already initialized sql connection
                sendRequest.Connection = Lab3SC;
                sendRequest.CommandText = sqlQSendInv;

                sendRequest.Parameters.AddWithValue("@sender", Session["username"].ToString());
                sendRequest.Parameters.AddWithValue("@receiver", txtSrResult.Text);

                Lab3SC.Open();
                int i = sendRequest.ExecuteNonQuery();
                Lab3SC.Close();

                if (i > 0)
                {
                    lblInviteResult.ForeColor = Color.Green;
                    lblInviteResult.Font.Bold = true;
                    lblInviteResult.Text = "Request sent to" + txtSrResult.Text;
                    btnSend.Enabled = false;
                }
                else
                {
                    lblInviteResult.ForeColor = Color.Red;
                    lblInviteResult.Font.Bold = true;
                    lblInviteResult.Text = "Error!, your request was not sent";
                    btnSend.Enabled = false;
                }
            }
        }
    }
}
