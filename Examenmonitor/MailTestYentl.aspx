<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailTestYentl.aspx.cs" Inherits="Examenmonitor.MailTestYentl" %>
<%@ import Namespace="System.Net.Mail" %>
<%@ import Namespace="System.Net" %>
<%@ import Namespace="Examenmonitor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <%
            
            RegistratieMail registratieMail = RegistratieMail.getInstance();
            registratieMail.ZendRegistratieMail("Yentl", "yentlfeys@hotmail.com", "teststringvoorhtmlrandomgeneratedlink");
            
        %>

    </div>
    </form>
</body>
</html>
