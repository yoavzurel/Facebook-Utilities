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
                    m_ApplicationUser = FacebookUtilities.GetUser(m_AccessToken);
                    m_UserFriends = FacebookUtilities.GetUsersFriends(m_AccessToken);
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

                // Adds the row
                friendsTable.Rows.Add(temporaryMessageRow);
            }
        }
    }
}