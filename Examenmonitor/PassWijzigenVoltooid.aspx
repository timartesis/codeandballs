<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PassWijzigenVoltooid.aspx.cs" Inherits="Examenmonitor.PassWijzigenVoltooid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class="PageContent">
        <div class="Header">

        </div>
    
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="succesLabel" runat="server" Text="Passwoord is succesvol gewijzigd!"></asp:Label>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ga terug naar main" />
    
    </div>
    </form>
        </div>
</body>
</html>
