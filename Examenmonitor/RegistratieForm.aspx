<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistratieForm.aspx.cs" Inherits="Examenmonitor.RegistratieForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registratie nieuwe gebruiker</title>


</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 94px; width: 920px">
    
        <p>
            <asp:Label ID="voornaamLabel" runat="server" Text="Voornaam: "></asp:Label>
            <asp:TextBox ID="voornaamTextBox" runat="server"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidatorVoorNaam" runat="server" ControlToValidate="voornaamTextBox" Display="Dynamic" ErrorMessage="Voornaam is Verplicht" ValidationGroup="AllValidators">Verplicht veld</asp:RequiredFieldValidator>
            <asp:CustomValidator ID="CustomValidatorVoorNaam" runat="server" ControlToValidate="voornaamTextBox"  Display="Dynamic" ErrorMessage="Gebruik enkel het alfabet" ValidationGroup="AllValidators" OnServerValidate="CustomValidatorVoorNaam_ServerValidate">Enkel A-Z</asp:CustomValidator>

        </p>
        <p>
            <asp:Label ID="achternaamLabel" runat="server" Text="Achternaam: "></asp:Label>
            <asp:TextBox ID="achternaamTextBox" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAchterNaam" runat="server" ControlToValidate="achternaamTextBox" Display="Dynamic" ErrorMessage="Achternaam is een verplicht veld" ValidationGroup="AllValidators">Verplicht veld!</asp:RequiredFieldValidator>
        </p>

        <p>
            <asp:Label ID="emailLabel" runat="server" Text="E-mail: "></asp:Label>
            <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="emailTextBox" Display="Dynamic" ErrorMessage="Email is verplicht in te vullen" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="emailTextBox" Display="Dynamic" ErrorMessage="Email moet in het formaat naam@site.com" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AllValidators">Ongeldig formaat</asp:RegularExpressionValidator>
        </p>

        <p>
            <asp:Label ID="wachtwoord1Label" runat="server" Text="Wachtwoord: "></asp:Label>
            <asp:TextBox ID="wachtwoord1TextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorWW1" runat="server" ControlToValidate="wachtwoord1TextBox" Display="Dynamic" ErrorMessage="Wachtwoord is een verplicht veld!" ValidationGroup="AllValidators">Wachtwoord is een verplicht veld!</asp:RequiredFieldValidator>
            <asp:CustomValidator ID="CustomValidatorWW1" runat="server" ControlToValidate="wachtwoord1TextBox" onServerValidate="wwValidator" Display="Dynamic" ErrorMessage="Gebruik enkel het alfabet of cijfers" ValidationGroup="AllValidators">Gebruik enkel het alfabet of cijfers!</asp:CustomValidator>
        </p>

        <p>
            <asp:Label ID="wachtwoord2Label" runat="server" Text="Wachtwoord: "></asp:Label>
            <asp:TextBox ID="wachtwoord2TextBox" runat="server"></asp:TextBox>
        </p>

        <p><asp:Button ID="registeerButton" runat="server" Text="Registreer" /></p>
    
    </div>
    </form>
</body>
</html>
