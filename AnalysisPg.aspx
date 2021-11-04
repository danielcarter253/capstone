﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AladinPage.Master" AutoEventWireup="true" CodeBehind="AnalysisPg.aspx.cs" Inherits="Lab2.AnalysisPg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="row justify-content-center align-items-center" style="height: 300px">
            <div class="col-md-8">
                <div class="p-3 border border-dark bg-light">
                    <div class="p-3 border border-dark bg-info">
                        <h2>Request Analysis</h2>
                    </div>
                    <asp:Label ID="lblPCkText" runat="server" Text="Select Text"></asp:Label>
                    <asp:DropDownList ID="ddlTextTitle" runat="server" DataSourceID="ddlSendText" DataTextField="textTitle" DataValueField="TextID"></asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="lblReason" runat="server" Text="Analysis Reason"></asp:Label>
                    <asp:TextBox ID="txtReason" runat="server"></asp:TextBox>
                    <asp:Label ID="lblReqField" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnSendReq" runat="server" Text="Send" OnClick="btnSendReq_Click" />
                    <asp:Label ID="lblPckResult" runat="server" Text=""></asp:Label>

                    <asp:SqlDataSource ID="ddlSendText" runat="server" ConnectionString="<%$ ConnectionStrings:Lab3CS %>"
                        SelectCommand="SELECT TextID, TextTitle FROM StoryText where userID =@userID">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UserID" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
               
            </div>
        </div>
    </div>


    <div class="row justify-content-center align-items-center" style="height: 450px">
        <div class="col-md-8">
            <div class="p-3 border border-dark bg-light">
                <div class="p-3 border border-dark bg-info">
                    <h2>Analysis Record</h2>
                </div>
                <asp:GridView ID="grdRecord" runat="server" DataSourceID="srcAnalysis" AutoGenerateColumns="false" AutoGenerateDeleteButton="true"
                    DataKeyNames="AnalysisID" AllowSorting="true" EmptyDataText="No Records yet!">
                    <Columns>
                        <asp:BoundField HeaderText="Analysis" DataField="AnalysisContent" />
                        <asp:BoundField HeaderText="Analysis Reason" DataField="AnalysisReason" />
                        <asp:BoundField HeaderText="Analysis Date" DataField="AnalysisDate" />
                        <asp:BoundField HeaderText="Text Title" DataField="TextTitle" />
                        <asp:BoundField HeaderText="Text Status" DataField="Status" SortExpression="Status" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

                <asp:SqlDataSource ID="srcAnalysis" runat="server" ConnectionString="<%$ ConnectionStrings:Lab3CS %>"
                    SelectCommand="SELECT AnalysisID, AnalysisContent, AnalysisReason, AnalysisDate, TextTitle, 
                        CASE when A.AnalysisContent not like 'NULL' then 'Ready' else 'Pending' END AS 'Status'
                        FROM TextAnalysis A RIGHT OUTER JOIN StoryText S on S.TextID=A.TextID where S.UserID = @UserID;"
                    DeleteCommand="DELETE SharedAnalysis where AnalysisID = @AnalysisID; Delete TextAnalysis where AnalysisID = @AnalysisID">
                    <SelectParameters>
                        <asp:SessionParameter Name="UserID" SessionField="UserID" Type="String" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Type="String" Name="AnalysisID" />
                    </DeleteParameters>
                </asp:SqlDataSource>
            </div>
         
        </div>
    </div>

    <div class="row justify-content-center align-items-center" style="height: 250px">
        <div class="col-md-8">
            <div class="p-3 border border-dark bg-light">
                <div class="p-3 border border-dark bg-info">
                    <h2>Shared Analysis</h2>
                </div>
                <asp:GridView ID="grdSharedA" runat="server" DataSourceID="src_grdSharedA" AutoGenerateColumns="false" AutoGenerateDeleteButton="true"
                    DataKeyNames="ShareID" AllowSorting="true" EmptyDataText="No Records yet!">
                    <Columns>
                        <asp:BoundField HeaderText="Analysis" DataField="AnalysisContent" />
                        <asp:BoundField HeaderText="Text Title" DataField="TextTitle" />
                        <asp:BoundField HeaderText="From" DataField="sender" ItemStyle-Width="100" ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="Message" DataField="ShareReason" />
                        <asp:BoundField HeaderText="Date Shared" DataField="ShareDate" SortExpression="ShareDate" DataFormatString="{0:d}" NullDisplayText="Me" />
                    </Columns>
                </asp:GridView>



                <asp:SqlDataSource ID="src_grdSharedA" runat="server" ConnectionString="<%$ ConnectionStrings:Lab3CS %>"
                    SelectCommand="	select sender, TextTitle, AnalysisContent, ShareDate, ShareReason, ShareID 
                            FROM SharedAnalysis AS S INNER JOIN TextAnalysis AS T ON S.AnalysisID = T.AnalysisID INNER JOIN StoryText AS ST ON T.TextID = ST.TextID  
                            WHERE recipient = @username"
                    DeleteCommand="Delete SharedAnalysis where ShareID = @ShareID">
                    <SelectParameters>
                        <asp:SessionParameter Name="username" SessionField="username" Type="String" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Type="String" Name="ShareID" />
                    </DeleteParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>

</asp:Content>
