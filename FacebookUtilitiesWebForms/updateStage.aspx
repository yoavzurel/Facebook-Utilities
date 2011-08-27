<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updateStage.aspx.cs" Inherits="FacebookUtilitiesWebForms.updateStage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" href="CSS/jquery.facebook.multifriend.select.css" /> 
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="top_area"Update the personal message for each friend"</div>
    <div id="search_area"></div>
    <div>
        <p>Update stage</p>
        <asp:Table ID="friendsTable" runat="server">
        </asp:Table>
        <asp:Button ID="finishButton" runat="server" text="Finish" CssClass="button" 
            onclick="finishButton_Click" />
    </div>
    </form>
</body>
</html>
