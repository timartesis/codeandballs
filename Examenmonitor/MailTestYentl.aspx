<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailTestYentl.aspx.cs" Inherits="Examenmonitor.MailTestYentl" %>
<%@ import Namespace="System.Net.Mail" %>
<%@ import Namespace="System.Net" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <%
        
            StringBuilder bodyMsg = new StringBuilder();
            bodyMsg.Append("laatste test");
            
            NetworkCredential loginInfo = new NetworkCredential("codeandballs@gmail.com", "plantesis");
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("test@codeandballs.be");
            msg.To.Add("yentlfeys@hotmail.com");
            msg.Subject = "Account Activation";
            msg.Body = bodyMsg.ToString();
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = loginInfo;
            client.Send(msg);    
            
        %>

    </div>
    </form>
</body>
</html>
