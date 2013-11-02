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
     string test = DatabankConnector.GetHuidigeDatum();
     DateTime test2 = DatabankConnector.StringDatumNaarDateTime(test);
     Response.Write(test2.ToString());
     
     
      
%>
    </div>
    </form>
</body>
</html>
