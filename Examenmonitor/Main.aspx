<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Examenmonitor.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Resources/Site.css" rel="stylesheet" type="text/css" />
    <link href="Resources/AccountManager.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script>

    </script>
</head>
<body>
    <div class="PageContent">
        <form id="form1" runat="server">
        <div class="Header" id="HeaderMain">

            <div class="btn-group">
  <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
    Action <span class="caret"></span>
  </button>
  <ul class="dropdown-menu" role="menu">
    <li><a href="#">Action</a></li>
    <li><a href="#">Another action</a></li>
    <li><a href="#">Something else here</a></li>
    <li class="divider"></li>
    <li><a href="#">Separated link</a></li>
  </ul>
</div>

            <ul id="menu">
	            <li>
		            <a data-toggle="dropdown" href="#"><%= Session["User"].ToString() %></a>
		            <ul>
			            <li><a href="PassWijzigen.aspx">Wachtwoord wijzigen</a></li>
			            <li><asp:LinkButton id="myLink" Text="Logout" OnClick="LinkButton_Click" runat="server"/></li>
		            </ul>
	            </li>
            </ul>
        </div>
        
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
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://code.jquery.com/jquery.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
        </form>
    </div>
</body>
</html>
