using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;

namespace FacebookUtilitiesWebForms
{
    /// <summary>
    /// this enum holds the picture types
    /// </summary>
    public enum ePictureTypes
    {
        pic,
        pic_small,
        pic_big,
        pic_square
    }


    /// <summary>
    /// this web form will hold the selection stage
    /// </summary>
    public partial class selectionStage : System.Web.UI.Page
    {
        private string m_AccessToken;
        private List<User> m_UserFriends;
        private User m_User;
        private FacebookClient m_FacebookClient;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //first time in the stage
                m_AccessToken = Request.QueryString["access_token"];
                if (!string.IsNullOrEmpty(m_AccessToken))
                {
                    m_FacebookClient = new FacebookClient(m_AccessToken);
                    aquireUser();
                    aquireUserFriends();
                    Response.Write("hello " + m_User.FullName + Environment.NewLine);
                    Response.Write("You have " +m_UserFriends.Count.ToString() + " friends"); 
                }
                else
                {
                    Response.Write("problem with access token. Please try again later");
                }
            }
            else
            {
                //not first time in the stage
            }
        }

        private void aquireUserFriends()
        {
            m_UserFriends = new List<User>();
            dynamic userFriends = m_FacebookClient.Query(
             "SELECT uid, name, first_name, last_name, pic_small, pic_big, pic_square, pic, birthday_date FROM user WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = me())");
            foreach (dynamic friend in userFriends)
            {
                User tempFriend = new User();
                createUserFromDynamicUser(friend, tempFriend);
                m_UserFriends.Add(tempFriend);
            }
        }

        private static void createUserFromDynamicUser(dynamic i_DynamicUser, User i_User)
        {
            i_User.Id = i_DynamicUser.uid;
            i_User.FullName = i_DynamicUser.name;
            i_User.FirstName = i_DynamicUser.first_name;
            i_User.LastName = i_DynamicUser.last_name;
            i_User.Birthday = i_DynamicUser.birthday_date;

            //create picture dictionary
            Dictionary<string, string> tempFriendPics = new Dictionary<string, string>();
            tempFriendPics[ePictureTypes.pic_small.ToString()] = i_DynamicUser.pic_small;
            tempFriendPics[ePictureTypes.pic_big.ToString()] = i_DynamicUser.pic_big;
            tempFriendPics[ePictureTypes.pic_square.ToString()] = i_DynamicUser.pic_square;
            tempFriendPics[ePictureTypes.pic.ToString()] = i_DynamicUser.pic;
            i_User.Pictures = tempFriendPics;
        }

        /// <summary>
        /// setting up the user field
        /// </summary>
        private void aquireUser()
        {
            m_User = new User();
            dynamic me = m_FacebookClient.Query(
             "SELECT uid, name, first_name, last_name, pic_small, pic_big, pic_square, pic, birthday_date FROM user WHERE uid = me()");
            createUserFromDynamicUser(me[0], m_User);
        }
    }
}