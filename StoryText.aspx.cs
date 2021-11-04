using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Drawing;


//Created by:   Dukes Solutions
//Date:         October 19, 2021
// Purpose:     Logged in user can upload new text by uploading a txt file or copy pasting the text. 
namespace Lab2
{
    public partial class StoryText : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                String fpath = Request.PhysicalApplicationPath + "TextFiles\\" +
                fileUpload.FileName;
                fileUpload.SaveAs(fpath);
                lblUploadStatus.Text = "Success";

                if (File.Exists(fpath))
                {
                    string str = File.ReadAllText(fpath);
                    txtDisplay.Text = str;
                    File.Delete(fpath);
                }
            }
            else
            {
                lblUploadStatus.ForeColor = Color.Red;
                lblUploadStatus.Font.Bold = true;
                lblUploadStatus.Text = "Please select file";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlC = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Lab3CS"].ConnectionString))
            {
 
                string selectQuery = "SELECT COUNT(*) FROM StoryText WHERE TextTitle = @TextTitle";
                SqlCommand sCommand = new SqlCommand(selectQuery, sqlC);
                sCommand.Parameters.AddWithValue("@TextTitle", txtTitle.Text);
               
                sqlC.Open();               
                var idExists = (Int32)sCommand.ExecuteScalar() > 0;
                sqlC.Close();

                //Check if the text already exists if it exists deisplay message. 
                if (!idExists)
                {
                    // Required filed validator. We created the validator this way to use other buttons on the page. 
                    if (txtTitle.Text != String.Empty && txtDisplay.Text != String.Empty)
                    {
                        SqlConnection MySqlC = new SqlConnection(WebConfigurationManager.ConnectionStrings["Lab3CS"].ConnectionString.ToString());
                        String sQuery = "INSERT INTO StoryText (textBody, textTitle, UploadDate, userID) VALUES (@TextBody, @Texttitle, @UploadDate, @UserID)";

                        SqlCommand cmd = new SqlCommand(sQuery, MySqlC);

                        cmd.Parameters.AddWithValue("@TextTitle", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@TextBody", txtDisplay.Text);
                        cmd.Parameters.AddWithValue("@UploadDate", DateTime.Today.ToString());
                        cmd.Parameters.AddWithValue("@UserID", Session["userID"].ToString());
                        MySqlC.Open();
                        int cond_duke = cmd.ExecuteNonQuery();
                        MySqlC.Close();

                        if (cond_duke == 1)
                        {
                            // This is to clear the fields after the text is saved, we also confirmation text to the user.
                            lblNotice.Text = "";
                            lblResult.Text = txtTitle.Text + " successfully saved!";                           
                            txtTitle.Text = "";
                            txtDisplay.Text = "";                          
                        }
                    }
                    else
                    {
                        // cleaer the lblresult incase it has text  
                        lblResult.Text = "";
                        lblNotice.Text = "One or more required fields above is missing.";
                    }
                }
                else
                {
                    lblResult.Text = "";
                    lblNotice.Text = "Text Story exists! Please enter a new title.";
                }
            }
        }
    }
}