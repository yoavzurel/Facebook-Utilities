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
        private User m_ApplicationUser;
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
                    tablePopulate();
                    //DataBaseHandler dbHandle = new DataBaseHandler();
                    //User Yoav = new User();
                    //Yoav.Id = "1";
                    //bool result = dbHandle.IsUserInDataBase(Yoav);
                    //Response.Write(result.ToString());
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

        private static void createUserFromDynamicUser(dynamic i_DynamicUser, User i_User)
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
            m_ApplicationUser = new User();
            dynamic me = m_FacebookClient.Query(
             "SELECT uid, name, first_name, last_name, pic_small, pic_big, pic_square, pic FROM user WHERE uid = me()");
            createUserFromDynamicUser(me[0], m_ApplicationUser);
            m_ApplicationUser.AccessToken = m_AccessToken;
        }

        /// <summary>
        /// This method populates the friends table.
        /// </summary>
        private void tablePopulate()
        {

            // Table styling
            friendsTable.CellPadding = 5;
            friendsTable.CellSpacing = 5;
            //friendsTable.Attributes.Add("style", "border-bottom: #999999 solid 1px;");

            TableRow temporaryRow;
            TableCell temporaryCell;
            Friend temporaryFriend;
            

            foreach (String stringiD in m_FriendsStringArray)
            {
                temporaryFriend = m_UserFriends[stringiD];
                temporaryRow = new TableRow();
                temporaryRow.HorizontalAlign = HorizontalAlign.Center;
                temporaryRow.VerticalAlign = VerticalAlign.Middle;

                // Retrieves friends picture
                Image userPic = new Image();
                userPic.ImageUrl = temporaryFriend.Pictures[ePictureTypes.pic_small.ToString()];

                populateRow(userPic, temporaryRow, out temporaryCell);

                // Add the name label
                Label friendNameLabel = new Label();
                friendNameLabel.Text = temporaryFriend.FullName;
                friendNameLabel.Attributes.Add("class", "name");

                populateRow(friendNameLabel, temporaryRow, out temporaryCell);

                TextBox friendTextBox = new TextBox();
                applyTextBoxSettings(friendTextBox);

                // Adds the new textBox to the list
                m_ListOfTextBoxes.Add(friendTextBox);

                populateRow(friendTextBox, temporaryRow, out temporaryCell);

                Button confirmationButton = new Button();
                confirmationButton.CssClass = "button";
                confirmationButton.Text = "Confirm";

                populateRow(confirmationButton, temporaryRow, out temporaryCell);

                // Adds the row
                friendsTable.Rows.Add(temporaryRow);
            }
        }

        private static void applyTextBoxSettings(TextBox friendTextBox)
        {
            friendTextBox.Width = 210;
            friendTextBox.Height = 52;
            friendTextBox.TextMode = TextBoxMode.MultiLine;
            friendTextBox.CssClass = "ta5";
        }

        /// <summary>
        /// This method populates a row inside the table of friends.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="temporaryRow"></param>
        /// <param name="temporaryCell"></param>
        private static void populateRow(dynamic data, TableRow temporaryRow, out TableCell temporaryCell)
        {
            // Creates and populates a cell with the image
            temporaryCell = new TableCell();
            temporaryCell.HorizontalAlign = HorizontalAlign.Left;
            temporaryCell.VerticalAlign = VerticalAlign.Middle;

            temporaryCell.Controls.Add(data);

            // Adds the created cell to the current row
            temporaryRow.Cells.Add(temporaryCell);
        }
    }
}