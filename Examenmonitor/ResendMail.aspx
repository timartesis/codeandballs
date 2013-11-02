<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResendMail.aspx.cs" Inherits="Examenmonitor.ResendMail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Opnieuw verzenden</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label2" runat="server" AssociatedControlID="Email">Email: </asp:Label>
        <asp:TextBox runat="server" ID="Email" TextMode="Email" />
        <%-- Validator om te zien of het veld is ingevuld --%>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email" CssClass="field-validation-error" ErrorMessage="Email is een verplicht veld!" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="Email" CssClass="field-validation-error" Display="Dynamic" 
        ValidationExpression="^((?>[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+\x20*|((?=[\x01-\x7f])[^\\]|\\[\x01-\x7f])*\x20*)*(?<angle><))?((?!\.)(?>\.?[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+)+|((?=[\x01-\x7f])[^\\]|\\[\x01-\x7f])*)@(((?!-)[a-zA-Z\d\-]+(?<!-)\.)+[a-zA-Z]{2,}|\[(((?(?<!\[)\.)(25[0-5]|2[0-4]\d|[01]?\d?\d)){4}|[a-zA-Z\d\-]*[a-zA-Z\d]:((?=[\x01-\x7f])[^\\\[\]]|\\[\x01-\x7f])+)\])(?(angle)>)$"
        ErrorMessage="Gebruik geen illegale tekens in uw email aub!"></asp:RegularExpressionValidator>

         <%-- Registreer button --%>
                        <asp:Button ID="buttonResendMail" runat="server" CommandName="MoveNext" Text="Opnieuw versturen" OnClick="buttonResendMail_Click" />
    </div>
    </form>
</body>
</html>
