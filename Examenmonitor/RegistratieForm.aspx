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
    <link href="Resources/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Resources/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Resources/center.css" rel="stylesheet" type="text/css" />
    <link href="Resources/center.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="PageContent">
        <div class="Header">

            </div>
        <form id="formRegistratie" class="form-horizontal" runat="server">
        
            <div style="height: 94px; width: 920px">
    
                <asp:Table class="FormTables" ID="Table1" runat="server" CellPadding="5"
                            GridLines="horizontal" BorderColor="Transparent" >
                    <asp:TableRow BorderColor="Transparent">
                        <asp:TableCell BorderColor="Transparent">
                
                                <%-- Gebruikersnaam label, textbox en validator --%>&nbsp;<asp:Label ID="Label1" runat="server" AssociatedControlID="Email">Voornaam: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell BorderColor="Transparent">
                                <asp:TextBox runat="server" ID="Voornaam" />
                                <%-- Validator om te zien of het veld is ingevuld --%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Voornaam"
                                    CssClass="field-validation-error" ErrorMessage="Voornaam is een verplicht veld" />
                                <%-- Regex validator --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorVoorNaam" runat="server" ControlToValidate="Voornaam" 
                                    CssClass="field-validation-error" Display="Dynamic" ValidationExpression="^[a-zA-Zéëäïöü]{1,40}$"
                                    ErrorMessage="Gebruik geen ongeldige tekens in uw naam aub!"></asp:RegularExpressionValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow BorderColor="Transparent">
                        <asp:TableCell BorderColor="Transparent">
                                <asp:Label ID="Label5" runat="server" Text="Achternaam: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell BorderColor="Transparent">
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
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow BorderColor="Transparent">
                        <asp:TableCell BorderColor="Transparent">
                                <%-- Email label, textbox en validator --%>
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="Email">Email: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell BorderColor="Transparent">
                                <asp:TextBox runat="server" ID="Email" TextMode="Email" />
                                <%-- Validator om te zien of het veld is ingevuld --%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="Email is een verplicht veld!" />
                                <asp:Label ID="dubbeleEmail" runat="server"></asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" 
                                    ControlToValidate="Email" CssClass="field-validation-error" Display="Dynamic" 
                                    ValidationExpression="^((?>[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+\x20*|((?=[\x01-\x7f])[^\\]|\\[\x01-\x7f])*\x20*)*(?<angle><))?((?!\.)(?>\.?[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+)+|((?=[\x01-\x7f])[^\\]|\\[\x01-\x7f])*)@+ap\.be$" 
                                    ErrorMessage="U kan enkel registreren met een emailadres van de AP hoge school inloggen als lector"></asp:RegularExpressionValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow BorderColor="Transparent">
                        <asp:TableCell BorderColor="Transparent">

                                <%-- Passwoord label en textbox --%>
                                <asp:Label ID="Label3" runat="server" AssociatedControlID="Password">Wachtwoord: </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell BorderColor="Transparent">
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <%-- Validator om te zien of het veld is ingevuld--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Password"
                                    CssClass="field-validation-error" ErrorMessage="Wachtwoord is een verplicht veld!" />
                                <%-- Validator om te zien of er geen illegale expressions inzitten --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorWachtwoord" runat="server" 
                                    ControlToValidate="Password" CssClass="field-validation-error" Display="Dynamic" 
                                    ValidationExpression="(?=^.{8,20}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$"
                                    ErrorMessage="Een wachtwoord moet bestaan uit 8 tot 20 tekens en moet minstens 1 Uppercase, 1 lowercase en 1 cijfer bevatten."></asp:RegularExpressionValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow BorderColor="Transparent">
                        <asp:TableCell BorderColor="Transparent">
                                <%-- Confirm Passwoord label en textbox --%>
                                <asp:Label ID="Label4" runat="server" AssociatedControlID="ConfirmPassword">Bevestig Wachtwoord</asp:Label>
                        </asp:TableCell>
                        <asp:TableCell BorderColor="Transparent">
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                <%-- Validator om te zien of het veld is ingevuld --%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Bevestig wachtwoord is een verplicht veld!" />
                                <%-- Validator om te zien of het passwoord hetzelfde is --%>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Het wachtwoord komt niet overeen!" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow BorderColor="Transparent">
                        <asp:TableCell BorderColor="Transparent">
                            <%-- Registreer button --%>
                            <asp:Button ID="buttonRegistreer" class="btn" runat="server" CommandName="MoveNext" Text="Registreer" OnClick="buttonRegistreer_Click" />
                        </asp:TableCell>
                        <asp:TableCell BorderColor="Transparent">  
                            <asp:Button ID="Terug" class="btn" runat="server" Text="Terug naar login" 
                                        OnClientClick="window.location.href='Login.aspx'" 
                                        PostBackUrl="~/Login.aspx" CausesValidation="false"/>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
    </div>
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://code.jquery.com/jquery.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
    </form>
    </div>
</body>
</html>
