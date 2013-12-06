<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailCheck.aspx.cs" Inherits="Examenmonitor.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Resources/Site.css" rel="stylesheet" type="text/css" />
    <link href="Resources/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Resources/center.css" rel="stylesheet" type="text/css" />
    <title>Mail verstuurd</title>
</head>
<body>
    <div class="PageContent">
        <div class="Header">

        </div>
        <form id="form1" class="form-horizontal" runat="server">
            <div>
                <p>Controleer uw email om uw account te activeren!</p>
                <asp:Button ID="buttonLogin" runat="server" CommandName="MoveNext" Text="Login" OnClick="buttonLogin_Click" />
            </div>
     <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://code.jquery.com/jquery.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
        </form>
        </div>
</body>
</html>
