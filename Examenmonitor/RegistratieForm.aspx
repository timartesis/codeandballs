<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistratieForm.aspx.cs" Inherits="Examenmonitor.RegistratieForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registratie nieuwe gebruiker</title>

    <script >
        

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 94px; width: 920px">
    
        <%-- Begin fieldset --%>
        <fieldset>
                        <legend>Registration Form</legend>
                        <ol>
                            <li>
                                <%-- Gebruikersnaam label, textbox en validator --%>
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName">User name</asp:Label>
                                <asp:TextBox runat="server" ID="UserName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                                    CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                            </li>
                            <li>
                                <%-- Email label, textbox en validator --%>
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="Email">Email address</asp:Label>
                                <asp:TextBox runat="server" ID="Email" TextMode="Email" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="The email address field is required." />
                            </li>
                            <li>
                                <%-- Passwoord label, textbox en validator --%>
                                <asp:Label ID="Label3" runat="server" AssociatedControlID="Password">Password</asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Password"
                                    CssClass="field-validation-error" ErrorMessage="The password field is required." />
                            </li>
                            <li>
                                <%-- Passwoord label, textbox en validators --%>
                                <asp:Label ID="Label4" runat="server" AssociatedControlID="ConfirmPassword">Confirm password</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                            </li>
                        </ol>
                        <%-- Registreer button --%>
                        <asp:Button ID="Button1" runat="server" CommandName="MoveNext" Text="Register" />
        </fieldset>
        <%-- Einde fieldset --%>
    
    </div>
    </form>
</body>
</html>
