<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Examenmonitor.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Resources/Site.css" rel="stylesheet" type="text/css" />
    <link href="Resources/test.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <div class="PageContent">
        <div class="Header" id="HeaderMain">
            <ul id="menu">
	            <li>
		            <a href="#">ACCOUNTNAMELALATEST</a>
		            <ul>
			            <li><a href="#">Wachtwoord wijzigen</a></li>
			            <li><a href="#">Logout</a></li>
		            </ul>
	            </li>
            </ul>
        </div>
        <form id="form1" runat="server">
            <div id="Menu">
                <div id="MenuFilter">
                    <asp:Label ID="titelLabel" runat="server" Text="Filter"></asp:Label>
                    <asp:Panel ID="PanelFilter" runat="server" Height="50px" >
                    </asp:Panel>
                </div>
            </div>
            <br />
            <div id="Data">
                <asp:Label ID="DataLabel" runat="server" Text="Examens"></asp:Label>
                <asp:Panel ID="PanelData" runat="server" Height="50px" >
                </asp:Panel>
            </div>
        </form>
    </div>
</body>
</html>
