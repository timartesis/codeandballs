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
     String pad = System.Environment.CurrentDirectory;
     Response.Write("<p>"+pad+"</p>");
     /*
     List<int> lijst = DatabankConnector.test();
     foreach (int id in lijst) 
     {
         Response.Write("<p>"+id+"</p>");
        
     }
      * */
%>
    </div>
    </form>
</body>
</html>
