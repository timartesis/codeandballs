<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WachtwoordVergeten.aspx.cs" Inherits="Examenmonitor.WachtwoordVergeten" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vergeten wachtwoord opvragen</title>

    <%-- CSS sheet link--%>
    <link href="Resources/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="PageContent">
    <form id="formWachtwoordVergeten" runat="server">
    <div style="height: 94px; width: 920px">
        <fieldset>
                        <legend>Wachtwoord resetten</legend>
                        <ol>
                            <li>
                                <%-- Email label, textbox en validator --%>
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="Email">Email: </asp:Label>
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
                            </li>
                        </ol>
            <asp:Button ID="buttonWachtwoordResetten" runat="server" CommandName="MoveNext" Text="Wachtwoord resetten" OnClick="buttonWachtwoordResetten_Click" />
    </fieldset>
    </div>
    </form>
        </div>
</body>
</html>
