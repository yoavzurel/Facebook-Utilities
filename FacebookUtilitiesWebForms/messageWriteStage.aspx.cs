using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook; 

namespace FacebookUtilitiesWebForms
{
    public delegate void SubmitWishesClicked(string i_BirthdayWish, Friend i_FriendWithBirthday);

    public partial class messageWriteStage : System.Web.UI.Page
    {
        private String[] m_FriendsStringArray;
        private string m_AccessToken;

        /// <summary>
        /// the user friends dictionary is orderd by : {id,friend} 
        /// </summary>
        private Dictionary<string, Friend> m_UserFriends;
        private ApplicationUser m_ApplicationUser;

        /// <summary>
        /// The list contains the text boxes of each friend.
        /// </summary>
        private readonly List<TextBox> m_ListOfTextBoxes = new List<TextBox>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["friends"]))
            {
                m_FriendsStringArray = Request.QueryString["friends"].Split(',');

                m_AccessToken = Request.QueryString["access_token"];
                if (!string.IsNullOrEmpty(m_AccessToken))
                {
                    m_ApplicationUser = FacebookUtilities.GetUser(m_AccessToken);
                    m_UserFriends = FacebookUtilities.GetUsersFriends(m_AccessToken);
                    testyoav();
                    populateTableWithFriends();
                }
                else
                {
                    Response.Write("problem with access token. Please try again later");
                }
            }
        }

   



        private void populateTableWithFriends()
        {
            TableMessageRow temporaryMessageRow;

            foreach (String stringiD in m_FriendsStringArray)
            {
                temporaryMessageRow = new TableMessageRow(m_UserFriends[stringiD]);
                temporaryMessageRow.SubmitClicked += new SubmitWishesClicked(temporaryMessageRow_SubmitClicked);

                // Adds the row
                friendsTable.Rows.Add(temporaryMessageRow);
            }
        }

        /// <summary>
        /// The event that takes care of the clicked button inside the table of friends.
        /// </summary>
        /// <param name="i_BirthdayWish">The wish that the user wants to send</param>
        /// <param name="i_FriendWithBirthday">The friend that needs to receive the wish</param>
        public void temporaryMessageRow_SubmitClicked(string i_BirthdayWish, Friend i_FriendWithBirthday)
        {
            Friend tempFriend = i_FriendWithBirthday;
            tempFriend.BirthdayMessage = i_BirthdayWish;

            // Needs to send them to DB
        }











        private void testyoav()
        {

        }
    }
}