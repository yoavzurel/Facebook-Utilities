<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectionStage.aspx.cs" Inherits="FacebookUtilitiesWebForms.selectionStage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script> 
        <script type="text/javascript" src="http://github.com/mbrevoort/jquery-facebook-multi-friend-selector/raw/master/jquery.facebook.multifriend.select.js"></script> 
        <link rel="stylesheet" href="CSS/jquery.facebook.multifriend.select.css" /> 

    <title>Facebook Birthday Utility</title>
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
	}
.name{
	font-size:14px;
	color:#3B5998;
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
     <div id="pageBody">
            <div id="fb-root"></div> 
            <script src="http://connect.facebook.net/en_US/all.js"></script> 
            <script>
                FB.init({ appId: '127571767338990', cookie: true });

                FB.getLoginStatus(function (response) {
                    if (response.session) {
                        init();
                    } else {
                        // no user session available, someone you dont know
                    }
                });


                function login() {
                    FB.login(function (response) {
                        if (response.session) {
                            init();
                        } else {
                            alert('Login Failed!');
                        }
                    });
                }

                function init() {
                    FB.api('/me', function (response) {
                        $("#username").html("<img src='https://graph.facebook.com/" + response.id + "/picture'/><div>" + response.name + "</div>");
                        $("#jfmfs-container").jfmfs({ max_selected: 20, max_selected_message: "{0} of {1} selected" });
                        $("#logged-out-status").hide();
                        $("#show-friends").show();

                    });
                }


                $("#show-friends").live("click", function () {
                    var friendSelector = $("#jfmfs-container").data('jfmfs');
                    $("#selected-friends").html(friendSelector.getSelectedIds().join(', '));
                });                  
 
 
              </script>

              <div>
                  <!--<div id="username"></div>--> 
                  <a href="#" id="show-friends" style="display:none;font-size=x-large;">Next</a> 
                  <div id="selected-friends" style="height:30px"></div> 
                  <div id="jfmfs-container"></div> 
              </div> 
    </div>
    <div class="clear"></div>
    <!--
    <form id="form1" runat="server">
    <div class="top_area">Select friends</div>
    <div id="search_area"></div>
    <div style="overflow-y: scroll; height: 370px; margin-top: 3px;">
        <div class="friends_area">
             <asp:Table ID="friendsTable" runat="server" Width="594px">
            </asp:Table>
        </div>
    </div>
    </form>
    -->
</body>
</html>
