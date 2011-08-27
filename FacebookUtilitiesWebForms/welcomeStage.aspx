<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="welcomeStage.aspx.cs" Inherits="FacebookUtilitiesWebForms.welcomeStage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" href="CSS/jquery.facebook.multifriend.select.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="top_area">Welcome to the utility</div>
    <div id="search_area"></div>
    <div>
        <asp:Table ID="welcomeTable" runat="server">
        </asp:Table>
    </div>
    </form>
</body>
</html>
