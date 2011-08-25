<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectionStage.aspx.cs" Inherits="FacebookUtilitiesWebForms.selectionStage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script> 
        <script type="text/javascript" src="http://github.com/mbrevoort/jquery-facebook-multi-friend-selector/raw/master/jquery.facebook.multifriend.select.js"></script> 
        <link rel="stylesheet" href="CSS/jquery.facebook.multifriend.select.css" /> 

    <title>Facebook Birthday Utility</title>
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
                    var selectedFriends = friendSelector.getSelectedIds();
                    var hiddenControl = '<%= friendsLabelHidden.ClientID %>';
                    document.getElementById(hiddenControl).value = selectedFriends;
                });

              </script>

              <div>
                  <!--<div id="username"></div>-->
                  <!--  -->
                  <a href="#" id="show-friends" style="display:none;font-size=x-large;">Submit</a>
                  <form id="form1" runat="server">
                        <div>
                            <input id="friendsLabelHidden" type="hidden" runat="server" />
                            <asp:Button ID="button23" runat="server" OnClick="nextButton_Click" Text="Next" CssClass="button"/>

                        </div>
                  </form>
                  <div id="selected-friends" style="height:30px"></div> 
                  <div id="jfmfs-container"></div> 
              </div> 
    </div>
    <div class="clear"/>
    <!--
    <div class="top_area">Select friends</div>
    <div id="search_area"></div>
    <div style="overflow-y: scroll; height: 370px; margin-top: 3px;">
        <div class="friends_area">
             <asp:Table ID="friendsTable" runat="server" Width="594px">
            </asp:Table>
        </div>
    </div>
    -->
</body>
</html>
