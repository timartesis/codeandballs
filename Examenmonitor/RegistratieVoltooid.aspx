<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistratieVoltooid.aspx.cs" Inherits="Examenmonitor.RegistratieVoltooid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registratie voltooid</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
        <asp:Label ID="hash" runat="server" ></asp:Label>
         <asp:Label ID="hashControle" runat="server" ></asp:Label>
        <asp:Button ID="buttonLogin" runat="server" CommandName="MoveNext" Text="Login" OnClick="buttonLogin_Click" />
        <asp:Button ID="buttonResend" runat="server" CommandName="MoveNext" Text="Mail opnieuw versturen" OnClick="buttonResend_Click" />
    </form>
</body>
</html>
