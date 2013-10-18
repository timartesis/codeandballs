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
     
     //Response.Write("<p>"+ConfigDB.getPad()+"</p>");

     DatabankConnector.Insert(0, 3, "greg", "greg", "greg","greg");

     List<int> lijst = DatabankConnector.GetData();
     foreach (int id in lijst) 
     {
         Response.Write("<p>"+id+"</p>");
        
     }
     
     
     
     
      
%>
    </div>
    </form>
</body>
</html>
