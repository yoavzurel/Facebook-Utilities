<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectionStage.aspx.cs" Inherits="FacebookUtilitiesWebForms.selectionStage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
        .style1
        {
            text-align: right;
        }
        .style4
        {
            width: 593px;
        }
        .uiButtonLarge{
               padding:2px 6px;
}
input{
               font-size:13px;
}
a#link{
	font-family:Arial, Helvetica, sans-serif;
	color:#3B5998;
	font-weight:bolder;
}
.uiButtonConfirm{
               background-color:#5B74A8;
               background-position:0 -48px;
               border-color:#29447E #29447E #1A356E;
               font-weight:bold;
               color:#FFFFFF;
}
.friends_area{
    width:500px;
	padding:5px;
    height:50px;
    }
.top_area{
	background:#627AAD;
	font-size:14px;
	color:#FFFFFF;
	font-weight:bold;
	padding:6px 6px 6px 12px;
}
.name b{
	font-size:14px;
	color:#3B5998;
	cursor:pointer;
	}
.name{
	font-size:14px;
	color:#666;
	padding:8px;
	}
#search_area{
	background:#F2F2F2 none repeat scroll 0 0;
	padding:5px;
	height:30px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="top_area">Select friends</div>
    <div id="search_area"></div>
    <div style="overflow-y: scroll; height: 370px; margin-top: 3px;">
    <div class="friends_area">
         <asp:Table ID="friendsTable" runat="server" Width="594px">
         </asp:Table>
    </div>
    </div>
    </form>
</body>
</html>
