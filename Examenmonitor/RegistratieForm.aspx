<%@ Page Language="C#" AutoEventWireup="true"   CodeBehind="RegistratieForm.aspx.cs" Inherits="Examenmonitor.RegistratieForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registratie nieuwe gebruiker</title>

    <%-- javascript--%>
    <script type="text/javascript" >
        
    </script>
    <%-- CSS sheet link--%>
    <link href="Resources/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="PageContent">
        <div class="Header">

            </div>
        <form id="formRegistratie" runat="server">
        
            <div style="height: 94px; width: 920px">
    
                <%-- Begin fieldset --%>
                <fieldset>
                        <legend>Registration Form</legend>
                        <ol>
                            <li>
                                <%-- Gebruikersnaam label, textbox en validator --%>&nbsp;<asp:Label ID="Label1" runat="server" AssociatedControlID="Email">Voornaam: </asp:Label>
                                <asp:TextBox runat="server" ID="Voornaam" />
                                <%-- Validator om te zien of het veld is ingevuld --%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Voornaam"
                                    CssClass="field-validation-error" ErrorMessage="Voornaam is een verplicht veld" />
                                <%-- Regex validator --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorVoorNaam" runat="server" ControlToValidate="Voornaam" 
                                    CssClass="field-validation-error" Display="Dynamic" ValidationExpression="^[a-zA-Zéëäïöü]{1,40}$"
                                    ErrorMessage="Gebruik geen ongeldige tekens in uw naam aub!"></asp:RegularExpressionValidator>
                            </li>
                            <li>
                                <asp:Label ID="Label5" runat="server" Text="Achternaam: "></asp:Label>
                                <asp:TextBox ID="AchterNaam" runat="server"></asp:TextBox>
                                <%-- Validator om te zien of het veld is ingevuld --%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAchterNaam" runat="server" ControlToValidate="AchterNaam" 
                                    CssClass="field-validation-error" Display="Dynamic" 
                                    ErrorMessage="Achternaam is een verplicht veld!"></asp:RequiredFieldValidator>
                                <%-- Regex validator --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorAchternaam" runat="server" 
                                    ControlToValidate="AchterNaam" CssClass="field-validation-error" Display="Dynamic" 
                                    ValidationExpression="^[a-zA-Zéëäïöü\s]{1,40}$"
                                    ErrorMessage="Gebruik geen ongeldige tekens in uw naam aub!"></asp:RegularExpressionValidator>
                            </li>
                            <li>
                                <%-- Email label, textbox en validator --%>
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="Email">Email: </asp:Label>
                                <asp:TextBox runat="server" ID="Email" TextMode="Email" />
                                <%-- Validator om te zien of het veld is ingevuld --%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="Email is een verplicht veld!" />
                                <asp:Label ID="dubbeleEmail" runat="server"></asp:Label>
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
                            <li>
                                <%-- Confirm Passwoord label en textbox --%>
                                <asp:Label ID="Label4" runat="server" AssociatedControlID="ConfirmPassword">Bevestig Wachtwoord</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                <%-- Validator om te zien of het veld is ingevuld --%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Bevestig wachtwoord is een verplicht veld!" />
                                <%-- Validator om te zien of het passwoord hetzelfde is --%>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Het wachtwoord komt niet overeen!" />
                            </li>
                        </ol>
                        <%-- Registreer button --%>
                        <asp:Button ID="buttonRegistreer" runat="server" CommandName="MoveNext" Text="Registreer" OnClick="buttonRegistreer_Click" />
        </fieldset>
        <%-- Einde fieldset --%>
    
    </div>
    </form>
    </div>
</body>
</html>
