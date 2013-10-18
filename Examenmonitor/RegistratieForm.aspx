<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistratieForm.aspx.cs" Inherits="Examenmonitor.RegistratieForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 94px; width: 920px">
    
        <p>
            <asp:Label ID="voornaamLabel" runat="server" Text="Voornaam: "></asp:Label>
            <asp:TextBox ID="voornaamTextBox" runat="server"></asp:TextBox>

        </p>
        <p>
            <asp:Label ID="achternaamLabel" runat="server" Text="Achternaam: "></asp:Label>
            <asp:TextBox ID="achternaamTextBox" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
        </p>

        <p>
            <asp:Label ID="emailLabel" runat="server" Text="E-mail: "></asp:Label>
            <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
        </p>

        <p>
            <asp:Label ID="wachtwoord1Label" runat="server" Text="Wachtwoord: "></asp:Label>
            <asp:TextBox ID="wachtwoord1TextBox" runat="server"></asp:TextBox>
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
