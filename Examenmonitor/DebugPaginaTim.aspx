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
     ExamenModel model = ExamenModel.getInstance();
     //ExamenModel.ReloadDataAsync();
     ExamenModel.ReloadData();
     List<Examen> list = ExamenModel.getExamens();
     //foreach (Examen item in list )
     //{
     //    Response.Write("<p>");
     //    Response.Write(item.ToString());
     //    Response.Write("</p>");
     //}

     List<Examen> list1 = FilterModel.filterExamensCities(ExamenModel.getExamens(), "Bouwmeesterstraat");
     foreach (Examen item in list1)
     {
         Response.Write("<p>");
         Response.Write(item.ToString());
         Response.Write("</p>");
     }
     
      
%>
    </div>
    </form>
</body>
</html>
