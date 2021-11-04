<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="Lab2.UserRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="row justify-content-center align-items-center" style="height: 700px">
            <div class="col-md-5">
                <div class="p-3 border border-dark bg-light">
                    <div class="p-3 border border-dark bg-info">
                        <h2>Create an Account</h2>
                    </div>
                    <br />

                    <asp:Table ID="Table1" runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblFirstName" runat="server" Text="First Name *" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblLastName" runat="server" Text="Last Name *" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblPhoneNum" runat="server" Text="Phone Number"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                                <asp:CompareValidator ID="cmpPhoneNumber" runat="server" ControlToValidate="txtPhoneNumber" Text="(Invalid Phone Number)" Operator="DataTypeCheck" Type="Integer" ForeColor="Red" Font-Bold="true" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblEmail" runat="server" Text="Email *" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtEmail" runat="server" OnTextChanged="txtEmail_TextChanged"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="regEmail" ControlToValidate="txtEmail" Text="(Invalid Email)" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*.\w+([-.]\w+)*" runat="server" ForeColor="Red" Font-Bold="true" />
                                <asp:CustomValidator ID="valEmail" runat="server" ControlToValidate="txtEmail" Text="" OnServerValidate="valEmail_ServerValidate" ValidateEmptyText="true" ForeColor="Purple" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblAddress" runat="server" Text="Mailing Address"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblOrganization" runat="server" Text="Organization"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="ddlOrg" runat="server" DataSourceID="src_ddlOrg" DataTextField="OrgName" DataValueField="OrgID"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblUsername" runat="server" Text="Username *" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                                <asp:CustomValidator ID="valUsername" runat="server" ControlToValidate="txtUsername" Text="Email already used!" OnServerValidate="valUsername_ServerValidate" ForeColor="Purple" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblPassword" runat="server" Text="Password *" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblConfirmPass" runat="server" Text="Confirm Passcode"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtConfirmPass" runat="server"></asp:TextBox>
                                <asp:CompareValidator ID="cmpPassword" Text="Passwords must match" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPass"
                                    Type="String" Operator="Equal" runat="server" ForeColor="Red" Font-Bold="true" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button ID="btnAnother" runat="server" OnClick="btnAnother_Click" Visible="false" Text="Create Another" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                    <asp:SqlDataSource ID="src_ddlOrg" runat="server" ConnectionString="<%$ ConnectionStrings:Lab3CS %>" SelectCommand="SELECT OrgID, OrgName FROM Organization"></asp:SqlDataSource>
                </div>
            </div>
        </div>
    </form>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/bootstrap.bundle.min.js"></script>
</body>
</html>
