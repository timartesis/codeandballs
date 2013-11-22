<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailCheck.aspx.cs" Inherits="Examenmonitor.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mail verstuurd</title>
</head>
<body>
    <div class="PageContent">
        <div class="Header">

        </div>
        <form id="form1" runat="server">
            <div>
                <p>Controleer uw email om uw account te activeren!</p>
                <asp:Button ID="buttonLogin" runat="server" CommandName="MoveNext" Text="Login" OnClick="buttonLogin_Click" />
            </div>
        </form>
        </div>
</body>
</html>
