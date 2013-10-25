<%@ Page Language="C#" AutoEventWireup="true"   CodeBehind="RegistratieForm.aspx.cs" Inherits="Examenmonitor.RegistratieForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registratie nieuwe gebruiker</title>

    <%-- javascript--%>
    <script type="text/javascript" >
        
    </script>
    <%-- CSS sheet link--%>
    <link href="Content/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="formRegistratie" runat="server">
    <div style="height: 94px; width: 920px">
    
        <%-- Begin fieldset --%>
        <fieldset>
                        <legend>Inlog Form</legend>
                        <ol>
                            <li>
                                <%-- Email label, textbox en validator --%>
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="Email">Email: </asp:Label>
                                <asp:TextBox runat="server" ID="Email" TextMode="Email" />
                                <%-- Validator om te zien of het veld is ingevuld --%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="Email is een verplicht veld!" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" 
                                    ControlToValidate="Email" CssClass="field-validation-error" Display="Dynamic" 
                                    ValidationExpression="^((?>[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+\x20*|((?=[\x01-\x7f])[^\\]|\\[\x01-\x7f])*\x20*)*(?<angle><))?((?!\.)(?>\.?[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+)+|((?=[\x01-\x7f])[^\\]|\\[\x01-\x7f])*)@(((?!-)[a-zA-Z\d\-]+(?<!-)\.)+[a-zA-Z]{2,}|\[(((?(?<!\[)\.)(25[0-5]|2[0-4]\d|[01]?\d?\d)){4}|[a-zA-Z\d\-]*[a-zA-Z\d]:((?=[\x01-\x7f])[^\\\[\]]|\\[\x01-\x7f])+)\])(?(angle)>)$"
                                    ErrorMessage="Gebruik geen illegale tekens in uw email aub!"></asp:RegularExpressionValidator>
                            </li>
                            <li>
                                <%-- Passwoord label en textbox --%>
                                <asp:Label ID="Label3" runat="server" AssociatedControlID="Password">Wachtwoord: </asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <%-- Validator om te zien of het veld is ingevuld--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Password"
                                    CssClass="field-validation-error" ErrorMessage="Wachtwoord is een verplicht veld!" />
                                <%-- Validator om te zien of er geen illegale expressions inzitten --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorWachtwoord" runat="server" 
                                    ControlToValidate="Password" CssClass="field-validation-error" Display="Dynamic" 
                                    ValidationExpression="(?=^.{8,20}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$"
                                    ErrorMessage="Een wachtwoord moet bestaan uit 8 tot 20 tekens en moet minstens 1 Uppercase, 1 lowercase en 1 cijfer bevatten."></asp:RegularExpressionValidator>
                            </li>
                        </ol>
                        <%-- Registreer button --%>
                        <asp:Button ID="buttonLogIn" runat="server" CommandName="MoveNext" Text="Log in" OnClick="buttonLogIn_Click" />
        </fieldset>
        <%-- Einde fieldset --%>
    
    </div>
    </form>
</body>
</html>
