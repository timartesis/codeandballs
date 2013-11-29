<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PassWijzigen.aspx.cs" Inherits="Examenmonitor.PassWijzigen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wijzigen passwoord</title>

    <%-- javascript--%>
    <script type="text/javascript" >

        </script>
    <%-- CSS sheet link--%>
    <link href="Resources/Site.css" rel="stylesheet" type="text/css" />
    <link href="Resources/bootstrap.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <div class="PageContent">
        <div class="Header">

        </div>
        <form id="formWijzigenPasswoord" class="form-horizontal" runat="server">
        <div style="height: 94px; width: 920px">
            <%-- Begin fieldset --%>
                <fieldset>
                        <legend>Passwoord wijzigen</legend>
                    <ol>
                    <li>
                                <%-- Passwoord label en textbox --%>
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="OudPasswoord">Oud Wachtwoord: </asp:Label>
                                <asp:TextBox runat="server" ID="OudPasswoord" TextMode="Password" />
                                <%-- Validator om te zien of het veld is ingevuld--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="OudPasswoord"
                                    CssClass="field-validation-error" ErrorMessage="Wachtwoord is een verplicht veld!" />
                                <%-- Validator om te zien of er geen illegale expressions inzitten --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="Password" CssClass="field-validation-error" Display="Dynamic" 
                                    ValidationExpression="(?=^.{8,20}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$"
                                    ErrorMessage="Een wachtwoord moet bestaan uit 8 tot 20 tekens en moet minstens 1 Uppercase, 1 lowercase en 1 cijfer bevatten."></asp:RegularExpressionValidator>
                            </li>
                    <li>
                                <%-- Passwoord label en textbox --%>
                                <asp:Label ID="Label3" runat="server" AssociatedControlID="Password">Nieuw Wachtwoord: </asp:Label>
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
                                <asp:Label ID="Label4" runat="server" AssociatedControlID="ConfirmPassword">Bevestig Nieuw Wachtwoord</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                <%-- Validator om te zien of het veld is ingevuld --%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Bevestig wachtwoord is een verplicht veld!" />
                                <%-- Validator om te zien of het passwoord hetzelfde is --%>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Het wachtwoord komt niet overeen!" />
                            </li>
                    </ol>
                        <%-- Confirm button --%>
                        <asp:Button ID="buttonWijzig" class="btn" runat="server" CommandName="MoveNext" Text="Wijzigen" OnClick="buttonWijzig_Click" />
                        <asp:Label ID="incorrectLabel" runat="server" ForeColor="Red" Visible="False"></asp:Label>
        </fieldset>
            <%-- Einde fieldset --%>
    
        </div>
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://code.jquery.com/jquery.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
        </form>
    </div>
    <p>
        &nbsp;</p>
</body>
</html>
