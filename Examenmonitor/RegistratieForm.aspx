<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistratieForm.aspx.cs" Inherits="Examenmonitor.RegistratieForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <form method="get" action="simpleform.asp">
        Voornaam: <input type="text" name="voornaam"><br>
        Achternaam: <input type="text" name="achternaam"><br>
        E-Mail: <input type="text" name="e-mail"><br>
        E-Mail: <input type="text" name="e-mail"><br>
        Wachtwoord: <input type="password" name="wachtwoord1"><br>
        Wachtwoord: <input type="password" name="wachtwoord2"><br>
    <input type="submit" value="Registreer">
</form>
    </div>
    </form>
</body>
</html>
