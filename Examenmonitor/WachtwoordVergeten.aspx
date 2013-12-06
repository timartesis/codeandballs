<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WachtwoordVergeten.aspx.cs" Inherits="Examenmonitor.WachtwoordVergeten" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vergeten wachtwoord opvragen</title>

    <%-- CSS sheet link--%>
    <link href="Resources/Site.css" rel="stylesheet" type="text/css" />
    <link href="Resources/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Resources/center.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <div class="PageContent">
    <div class="Header">

    </div>
    <form id="formWachtwoordVergeten" class="form-horizontal" runat="server">
    <div style="height: 94px; width: 920px">
        <asp:Table ID="Table1" runat="server" CellPadding="5"
                            GridLines="horizontal" BorderColor="Transparent" >
            <asp:TableRow BorderColor="Transparent"> 
                <asp:TableCell BorderColor="Transparent">                
                    <%-- Email label, textbox en validator --%>
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="Email">Email: </asp:Label>
                    </asp:TableCell>
                    <asp:TableCell BorderColor="Transparent"> 
                    <asp:TextBox runat="server" ID="Email" TextMode="Email" />
                    <%-- Validator om te zien of het veld is ingevuld --%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email"
                         CssClass="field-validation-error" ErrorMessage="Email is een verplicht veld!" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" 
                         ControlToValidate="Email" CssClass="field-validation-error" Display="Dynamic" 
                         ValidationExpression="^((?>[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+\x20*|((?=[\x01-\x7f])[^\\]|\\[\x01-\x7f])*\x20*)*(?<angle><))?((?!\.)(?>\.?[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+)+|((?=[\x01-\x7f])[^\\]|\\[\x01-\x7f])*)@(((?!-)[a-zA-Z\d\-]+(?<!-)\.)+[a-zA-Z]{2,}|\[(((?(?<!\[)\.)(25[0-5]|2[0-4]\d|[01]?\d?\d)){4}|[a-zA-Z\d\-]*[a-zA-Z\d]:((?=[\x01-\x7f])[^\\\[\]]|\\[\x01-\x7f])+)\])(?(angle)>)$"
                         ErrorMessage="Gebruik geen illegale tekens in uw email aub!"></asp:RegularExpressionValidator>
                    <asp:Label ID="mailBestaatLabel" runat="server" Text="Deze email is nog niet geregistreerd!" Visible="False"></asp:Label>
                    <asp:Label ID="activatieLabel" runat="server" Text="Deze email is nog niet geactiveerd!" Visible="False"></asp:Label>
                                
                    </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow BorderColor="Transparent">
                    <asp:TableCell BorderColor="Transparent">       
                    <asp:Button ID="buttonWachtwoordResetten" class="btn" runat="server" CommandName="MoveNext" Text="Wachtwoord resetten" 
                                        OnClick="buttonWachtwoordResetten_Click" />
                    </asp:TableCell>
                    <asp:TableCell BorderColor="Transparent">  
                    <asp:Button ID="Terug" class="btn" runat="server" Text="Terug naar login" 
                                        OnClientClick="window.location.href='Login.aspx'" 
                                        PostBackUrl="~/Login.aspx" CausesValidation="false"/>
                    </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    </div>
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://code.jquery.com/jquery.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
    </form>
   </div>
</body>
</html>
