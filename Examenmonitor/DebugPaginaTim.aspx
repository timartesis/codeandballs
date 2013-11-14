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

     bool naam = DatabankConnector.ControleerPassresetHash("1486767e166298e99d0f85d42a040b42a3939d4c308ebc6d78ccee429ec2526b");
     Response.Write(naam); 
     
     
      
%>
    </div>
    </form>
</body>
</html>
