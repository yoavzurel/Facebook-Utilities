using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacebookUtilitiesWebForms
{
    public partial class deleteStage : System.Web.UI.Page
    {
        // The update stage needs to receive the access token as parameter in address
        private string m_AccessToken;

        /// <summary>
        /// the user friends dictionary is orderd by : {id,friend} 
        /// </summary>
        private Dictionary<string, Friend> m_UserFriends;

        /// <summary>
        /// List of friends where the message was changed.
        /// </summary>
        private List<Friend> m_ListOfFriendsToInsertMessage = new List<Friend>();

        private ApplicationUser m_ApplicationUser;

        /// <summary>
        /// The list contains the text boxes of each friend.
        /// </summary>
        private readonly List<TextBox> m_ListOfTextBoxes = new List<TextBox>();

        private DataBaseHandler m_DataBaseHandlerObject = new DataBaseHandler();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["access_token"]))
            {
                m_AccessToken = Request.QueryString["access_token"];
                m_ApplicationUser = FacebookUtilities.GetUser(m_AccessToken);
                m_UserFriends = m_DataBaseHandlerObject.GetUserFriendsThatAreInDataBase(m_ApplicationUser);

                // Checks if the user has friends in database
                if (m_UserFriends.Count == 0)
                {
                    Response.Write("User doesn't appear to have friends in database!");

                    // Returns to the welcomeStage
                    Response.Redirect(string.Format("welcomeStage.aspx?access_token={0}", m_AccessToken));
                }

                populateTableWithFriends();
            }
            else
            {
                Response.Write("problem with access token. Please try again later");
            }
        }


        private void populateTableWithFriends()
        {
            TableMessageRow temporaryMessageRow;

            foreach (KeyValuePair<string, Friend> pair in m_UserFriends)
            {
                temporaryMessageRow = new TableMessageRow(pair.Value);

                // Adding the message in the database to the textbox of the table
                temporaryMessageRow.TextBox.Text = pair.Value.BirthdayMessage;

                // Changing the text of the button from "confirm" to "update"
                temporaryMessageRow.ConfirmButton.Text = "Delete";

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
            Friend temporaryFriend = m_UserFriends[i_FriendWithBirthday.Id];
            // Adds the birthday wish to the correct friend.
            if (temporaryFriend != null)
            {
                // NEEDS TO CREATE THIS METHOD
                m_DataBaseHandlerObject.DeleteFriendFromDatabase(m_ApplicationUser, temporaryFriend);
            }

        }

        /// <summary>
        /// Executed when the finish button is clicked.
        /// This method should invoke a call to the database and add
        /// new messages to user's friend.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void finishButton_Click(object sender, EventArgs e)
        {
            // Returns to the welcomeStage
            Response.Redirect(string.Format("welcomeStage.aspx?access_token={0}", m_AccessToken));
        }
    }
}