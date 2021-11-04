<%@ Page Title="" Language="C#" MasterPageFile="~/AladinPage.Master" AutoEventWireup="true" CodeBehind="StoryText.aspx.cs" Inherits="Lab2.StoryText" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="row justify-content-center align-items-center" style="height: 600px">
            <div class="col-md-5">
                <div class="p-3 border border-dark bg-light">
                    <div class="p-3 border border-dark bg-info">
    <asp:Label ID="lblPgTitle" runat="server" Text="Story Management" style="font-weight: 700; font-size: x-large"></asp:Label>
                        </div>
    <br />
    <br />
    <asp:Label ID="lblTextTitle" runat="server" Text="Text Title: "></asp:Label>
    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblTextBody" runat="server" Text="Text Body: "></asp:Label>
    <br />
    <asp:TextBox ID="txtDisplay" runat="server" TextMode="MultiLine" Rows="20" Height="200" Width="400"></asp:TextBox>
    <br />
    <asp:Label ID="lblUploadFile" runat="server" Text="Choose text file to upload: "></asp:Label>
    <asp:FileUpload ID="fileUpload" runat="server" />
    <br />
    <asp:Button ID="btnUploadFile" runat="server" Text="Upload File"  OnClick="btnUploadFile_Click"/>
    <asp:Label ID="lblUploadStatus" runat="server" Text=""></asp:Label>
    
    <br />
    <br />
    <asp:Button ID="btnSave" runat="server" Text="Save"  OnClick="btnSave_Click"/>
    <asp:Label ID="lblResult" runat="server" Text="" Font-Bold="true" ForeColor="Green" Font-Size="Larger"></asp:Label>
    <asp:Label ID="lblNotice" runat="server" Text="" Font-Bold="true" ForeColor="Red" Font-Size="Larger"></asp:Label>
                    </div>
                </div>
          </div>
</asp:Content>
