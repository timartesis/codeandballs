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
     //string sql = "select * from tblUsers";
     //DBController controller = new DBController(sql);
     //var test = controller.ExecuteReaderQueryReturnMultipleResultsMultipleRow("id", "email", "wachtwoord");
     //foreach (var t in test)
     //{
     //    foreach (var b in t)
     //    {
     //        Response.Write(b.ToString());
     //    }
     //}

     string naam = DatabankConnector.GetVoornaamEnAchternaam("codeandballs@gmail.com");
     Response.Write(naam); 
     
     
      
%>
    </div>
    </form>
</body>
</html>
