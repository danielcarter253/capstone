<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPg.aspx.cs" Inherits="Lab2.LoginPg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />



</head>

<body>
    <form id="form1" runat="server">

        <div class="row justify-content-center align-items-center" style="height: 600px">
            <div class="col-md-4">
                <div class="p-3 border border-dark bg-light">
                    <div class="row justify-content-center">
                        <asp:Image ID="Image1" runat="server" ImageUrl="/Images/logo.jpg" Width="200" Height="200" />
                    </div>
                    <br />
                    <div class="row justify-content-center">
                        <asp:Table ID="Table1" runat="server">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="btnRegister" runat="server" Text="Create Account" OnClick="btnRegister_Click" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/bootstrap.bundle.min.js"></script>



</body>
</html>
