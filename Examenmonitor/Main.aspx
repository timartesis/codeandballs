<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Examenmonitor.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Resources/Site.css" rel="stylesheet" type="text/css" />
    <link href="Resources/bootstrap.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script>

    </script>
    
</head>
<body>
    <div class="PageContent">
        <form id="formMain" runat="server">
        <div class="Header" id="HeaderMain">
            <div id="ButtonsAccount">
                <asp:Button ID="resendMailButton" class="btn" runat="server" CommandName="MoveNext" Text="Wachtwoord wijzigen" 
                                        OnClientClick="window.location.href='PassWijzigen.aspx'" 
                                        PostBackUrl="~/PassWijzigen.aspx" CausesValidation="false"/>
                <asp:LinkButton id="myLink" class="btn" Text ="Logout" OnClick="LinkButton_Click" runat="server"/>
            </div>
        </div>
        
            <div id="Menu">
                <div id="MenuFilter">
                    <asp:Label ID="titelLabel" runat="server" Text="Filter"></asp:Label>
                    <asp:Panel  ID="PanelFilter" runat="server" Height="50px" >
                    </asp:Panel>
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />
            <div id="Data">
                <asp:Label ID="DataLabel" runat="server" Text="Examens"></asp:Label>
                <br />
                <br />
                <br />
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
