<%@ Import Namespace="Examenmonitor" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DebugPaginaTim.aspx.cs" Inherits="Examenmonitor.DebugPaginaTim" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
 <%
     //code hier fucker
     bool test = DatabankConnector.ControleerEmail("tim@gmail.com");
     bool test2 = DatabankConnector.ControleerEmail("yentlfeys@hotmail.com");

     if (test)
     {
         Response.Write("<p>er klopt iets ni</p>");
     }
     if (test2)
     {
         Response.Write("<p>het klopt</p>");
     }
     
     
      
%>
    </div>
    </form>
</body>
</html>
