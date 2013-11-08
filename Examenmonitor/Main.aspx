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

                    <asp:Button ID="btnAddNewRow" runat="server" OnClick="btnAddNewRow_Click" Style="z-index: 100;
                    left: 66px; position: absolute; top: 287px" Text="Add New Row" Width="145px" />

                    <asp:Panel ID="Panel1" runat="server" Height="50px" Style="z-index: 102; left: 73px;
                    position: absolute; top: 8px" Width="125px">
                    </asp:Panel>
                </fieldset>
            </div>

            <div id="Data">

            </div>
        </form>
    </div>
</body>
</html>
