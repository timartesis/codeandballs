<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistratieForm.aspx.cs" Inherits="Examenmonitor.RegistratieForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        Voornaam: <input type="text" name="voornaam"/><br/>
        Achternaam: <input type="text" name="achternaam"/><br/>
        E-Mail: <input type="text" name="e-mail"/><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        <br/>
        E-Mail: <input type="text" name="e-mail"/><br/>
        Wachtwoord: <input type="password" name="wachtwoord1"/><br/>
        Wachtwoord: <input type="password" name="wachtwoord2"/><br/>
    <input type="submit" value="Registreer"/>
   
    </div>
    </form>
</body>
</html>
