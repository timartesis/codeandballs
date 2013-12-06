<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistratieVoltooid.aspx.cs" Inherits="Examenmonitor.RegistratieVoltooid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Resources/Site.css" rel="stylesheet" type="text/css" />
    <link href="Resources/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Resources/center.css" rel="stylesheet" type="text/css" />
    <title>Registratie voltooid</title>
</head>
<body>
    <div class="PageContent">
        <div class="Header">

        </div>
    <form id="form1" class="form-horizontal" runat="server">
    <div>
    </div>
        <asp:Label ID="hash" runat="server" ></asp:Label>
         <asp:Label ID="hashControle" runat="server" ></asp:Label>
        <asp:Button ID="buttonLogin" class="btn" runat="server" CommandName="MoveNext" Text="Login" OnClick="buttonLogin_Click" />
        <asp:Button ID="buttonResend" class="btn" runat="server" CommandName="MoveNext" Text="Mail opnieuw versturen" OnClick="buttonResend_Click" />
       <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://code.jquery.com/jquery.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
         </form>
        </div>
</body>
</html>
