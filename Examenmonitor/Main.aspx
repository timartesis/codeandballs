<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Examenmonitor.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class="PageContent">
        <form id="form1" runat="server">
            <div id="Filter">
                <fieldset>
                    <legend>Filter</legend>

                    <asp:Label ID="debugLabel" runat="server" Text="Label"></asp:Label>
                    <div id="checkboxContainer">
                        <asp:Panel ID="Panel1" runat="server" Height="50px" >
                        </asp:Panel>
                    </div>
                    
                </fieldset>
            </div>

            <div id="Data">

            </div>
        </form>
    </div>
</body>
</html>
