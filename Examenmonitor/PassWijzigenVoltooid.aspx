<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PassWijzigenVoltooid.aspx.cs" Inherits="Examenmonitor.PassWijzigenVoltooid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Resources/Site.css" rel="stylesheet" type="text/css" />
    <link href="Resources/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Resources/center.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="PageContent">
        <div class="Header">

        </div>
    
    <form id="form1" class="form-horizontal" runat="server">
    <div>
    
        <asp:Label ID="succesLabel" runat="server" Text="Passwoord is succesvol gewijzigd!"></asp:Label>
        <asp:Button ID="Button1" class="btn" runat="server" OnClick="Button1_Click" Text="Ga terug naar main" />
    
    </div>

    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://code.jquery.com/jquery.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
    </form>
        </div>
</body>
</html>
