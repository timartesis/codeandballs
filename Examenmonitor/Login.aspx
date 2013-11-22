<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Examenmonitor.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%-- CSS sheet link--%>
    <link href="Resources/Site.css" rel="stylesheet" type="text/css" />
    <title>Login pagina</title>

</head>
<body>
    <div class="PageContent">
        <form id="form1" runat="server">
            <div class="Header">

            </div>
            <div>
                <%-- Begin div --%>
                    <div>
                        <asp:Table ID="Table1" runat="server" CellPadding="5"
                            GridLines="horizontal" BorderColor="Transparent" >
                            <asp:TableRow>
                                <asp:TableCell>
                                    <%-- Email label en textbox --%>
                                    <asp:Label ID="Label2" runat="server" AssociatedControlID="Email">Email: </asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox runat="server" ID="Email" TextMode="Email" />
                                    <%-- Validator om te zien of het veld is ingevuld --%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="Email is een verplicht veld!" />
                                    <%-- REGEX Validator --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" 
                                    ControlToValidate="Email" CssClass="field-validation-error" Display="Dynamic" 
                                    ValidationExpression="^((?>[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+\x20*|((?=[\x01-\x7f])[^\\]|\\[\x01-\x7f])*\x20*)*(?<angle><))?((?!\.)(?>\.?[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+)+|((?=[\x01-\x7f])[^\\]|\\[\x01-\x7f])*)@(((?!-)[a-zA-Z\d\-]+(?<!-)\.)+[a-zA-Z]{2,}|\[(((?(?<!\[)\.)(25[0-5]|2[0-4]\d|[01]?\d?\d)){4}|[a-zA-Z\d\-]*[a-zA-Z\d]:((?=[\x01-\x7f])[^\\\[\]]|\\[\x01-\x7f])+)\])(?(angle)>)$"
                                    ErrorMessage="Gebruik geen illegale tekens in uw email aub!"></asp:RegularExpressionValidator>
                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                <asp:TableCell>
                                    <%-- Passwoord label en textbox --%>
                                    <asp:Label ID="Label3" runat="server" AssociatedControlID="Password">Wachtwoord: </asp:Label>
                                </asp:TableCell><asp:TableCell>
                                    <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                    <%-- Validator om te zien of het veld is ingevuld--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Password"
                                    CssClass="field-validation-error" ErrorMessage="Wachtwoord is een verplicht veld!" />
                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                <asp:TableCell>
                                    <%-- Inlogbutton --%>
                                    <asp:Button ID="buttonLogIn" runat="server" CommandName="MoveNext" Text="Log in" OnClick="buttonLogin_Click" />
                                </asp:TableCell></asp:TableRow></asp:Table></div><br />
        <%-- Email label en textbox --%><div id="RegisterRecovery" >
            <%-- Validator om te zien of het veld is ingevuld --%><asp:Button ID="buttonRegistreerHier" runat="server" CommandName="MoveNext" Text="Registreer hier!!" 
                    OnClick="buttonRegistreerHier_Click" OnClientClick="window.location.href='RegistratieForm.aspx'" 
                    PostBackUrl="~/RegistratieForm.aspx" CausesValidation="false"/>
            <%-- REGEX Validator --%><asp:Button ID="wachtwoordVergetenButton" runat="server" CommandName="MoveNext" Text="Wachtwoord vergeten?"
            OnClientClick="window.location.href='WachtwoordVergeten.aspx'" PostBackUrl="~/WachtwoordVergeten.aspx" CausesValidation="false"/><asp:Label ID="Debug" runat="server" Text="label"></asp:Label>

            <asp:Button ID="resendMailButton" runat="server" CommandName="MoveNext" Text="Activatiemail opnieuw verzenden." 
                     OnClientClick="window.location.href='ResendMail.aspx'" 
                    PostBackUrl="~/ResendMail.aspx" CausesValidation="false"/>

                                        </div></div> </form></div></body></html>