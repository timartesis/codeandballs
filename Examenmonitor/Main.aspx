<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Examenmonitor.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class="PageContent">
        <form id="form1" runat="server">
            <div id="Menu">
                <div id="MenuFilter">
                    <asp:Label ID="titelLabel" runat="server" Text="Filter"></asp:Label>
                    <asp:Panel ID="Panel1" runat="server" Height="50px" >
                    </asp:Panel>
                </div>
            </div>

            <div id="Data">

            </div>
        </form>
    </div>
</body>
</html>
