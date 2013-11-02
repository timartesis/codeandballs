<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailCheck.aspx.cs" Inherits="Examenmonitor.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mail verstuurd</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Controleer uw email om uw account te activeren!
        <asp:Button ID="buttonLogin" runat="server" CommandName="MoveNext" Text="Login" OnClick="buttonLogin_Click" />
    </div>
    </form>
</body>
</html>
