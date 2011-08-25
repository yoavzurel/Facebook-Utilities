using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;

namespace FacebookUtilitiesWebForms
{
    public partial class messageWriteStage : System.Web.UI.Page
    {
        private String[] m_FriendsStringArray;
        private string m_AccessToken;

        /// <summary>
        /// the user friends dictionary is orderd by : {id,friend} 
        /// </summary>
        private Dictionary<string, Friend> m_UserFriends;
        private ApplicationUser m_ApplicationUser;
        private FacebookClient m_FacebookClient;

        /// <summary>
        /// The list contains the text boxes of each friend.
        /// </summary>
        private readonly List<TextBox> m_ListOfTextBoxes = new List<TextBox>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["friends"] != null)
            {
                m_FriendsStringArray = Request.QueryString["friends"].Split(',');

                m_AccessToken = Request.QueryString["access_token"];
                if (!string.IsNullOrEmpty(m_AccessToken))
                {
                    m_FacebookClient = new FacebookClient(m_AccessToken);
                    aquireUser();
                    aquireUserFriends();
                    populateTableWithFriends();
                }
                else
                {
                    Response.Write("problem with access token. Please try again later");
                }
            }
        }
        /// <summary>
        /// setting up the friends list
        /// </summary>
        private void aquireUserFriends()
        {
            m_UserFriends = new Dictionary<string, Friend>();
            dynamic userFriends = m_FacebookClient.Query(
             "SELECT uid, name, first_name, last_name, pic_small, pic_big, pic_square, pic, birthday_date FROM user WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = me())");
            foreach (dynamic friend in userFriends)
            {
                Friend tempFriend = new Friend();
                createUserFromDynamicUser(friend, tempFriend);
                m_UserFriends.Add(tempFriend.Id, tempFriend);
            }
        }

        private static void createUserFromDynamicUser(dynamic i_DynamicUser, FacebookUser i_User)
        {
            i_User.Id = i_DynamicUser.uid;
            i_User.FullName = i_DynamicUser.name;
            i_User.FirstName = i_DynamicUser.first_name;
            i_User.LastName = i_DynamicUser.last_name;

            if (i_User is Friend)
            {
                (i_User as Friend).Birthday = i_DynamicUser.birthday_date;
            }

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
            m_ApplicationUser = new ApplicationUser();
            dynamic me = m_FacebookClient.Query(
             "SELECT uid, name, first_name, last_name, pic_small, pic_big, pic_square, pic FROM user WHERE uid = me()");
            createUserFromDynamicUser(me[0], m_ApplicationUser);
            m_ApplicationUser.AccessToken = m_AccessToken;
        }

        private void populateTableWithFriends()
        {
            TableMessageRow temporaryMessageRow;

            foreach (String stringiD in m_FriendsStringArray)
            {
                temporaryMessageRow = new TableMessageRow(m_UserFriends[stringiD]);

                // Adds the row
                friendsTable.Rows.Add(temporaryMessageRow);
            }
        }
    }
}