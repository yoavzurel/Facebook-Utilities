using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacebookUtilitiesWebForms
{
    public partial class welcomeStage : System.Web.UI.Page
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

        private DataBaseHandler m_DataBaseHandlerObject;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["access_token"]))
            {
                m_AccessToken = Request.QueryString["access_token"];
                m_ApplicationUser = FacebookUtilities.GetUser(m_AccessToken);
                m_UserFriends = FacebookUtilities.GetUsersFriends(m_AccessToken);
                populateTableWithControls();
            }
            else
            {
                Response.Write("problem with access token. Please try again later");
            }
        }

        private void populateTableWithControls()
        {
            TableRow firstRow = new TableRow();
            TableCell firstCell = new TableCell();
            TableCell secondCell = new TableCell();

            applyCustomizationToRow(ref firstRow);
            applyCustomizationToCell(ref firstCell);
            applyCustomizationToCell(ref secondCell);

            Image userPic = new Image();
            userPic.ImageUrl = m_ApplicationUser.Pictures[ePictureTypes.pic.ToString()];
            firstCell.Controls.Add(userPic);

            Label usernameLabel = new Label();
            usernameLabel.Text = string.Format("Welcome {0} {1} You have {2} friends.",
                m_ApplicationUser.FullName, "<br>", m_UserFriends.Count);
            secondCell.Controls.Add(usernameLabel);
            
            firstRow.Cells.Add(firstCell);
            firstRow.Cells.Add(secondCell);
            
            
            TableRow controlsRow = new TableRow();
            applyCustomizationToRow(ref controlsRow);

            TableCell firstButtonCell = new TableCell();
            TableCell secondButtonCell = new TableCell();
            TableCell thirdButtonCell = new TableCell();
            applyCustomizationToCell(ref firstButtonCell);
            applyCustomizationToCell(ref secondButtonCell);
            applyCustomizationToCell(ref thirdButtonCell);

            Button addButton = new Button();
            addButton.Text = "Add";
            addButton.CssClass = "button";
            addButton.Click += new EventHandler(addButton_Click);

            Button updateButton = new Button();
            updateButton.Text = "Update";
            updateButton.CssClass = "button";
            updateButton.Click += new EventHandler(updateButton_Click);

            Button deleteButton = new Button();
            deleteButton.Text = "Delete";
            deleteButton.CssClass = "button";
            deleteButton.Click += new EventHandler(deleteButton_Click);

            secondButtonCell.Controls.Add(updateButton);
            thirdButtonCell.Controls.Add(deleteButton);
            firstButtonCell.Controls.Add(addButton);

            controlsRow.Cells.Add(firstButtonCell);
            controlsRow.Cells.Add(secondButtonCell);
            controlsRow.Cells.Add(thirdButtonCell);

            welcomeTable.Controls.Add(firstRow);
            welcomeTable.Controls.Add(controlsRow);
        }

        public void deleteButton_Click(object sender, EventArgs e)
        {
            // Returns to the welcomeStage
            Response.Redirect(string.Format("deleteStage.aspx?access_token={0}", m_AccessToken));
        }

        public void updateButton_Click(object sender, EventArgs e)
        {
            // Returns to the welcomeStage
            Response.Redirect(string.Format("updateStage.aspx?access_token={0}", m_AccessToken));
        }

        public void addButton_Click(object sender, EventArgs e)
        {
            // Returns to the welcomeStage
            Response.Redirect(string.Format("selectionStage.aspx?access_token={0}", m_AccessToken));
        }

        private void applyCustomizationToCell(ref TableCell i_Cell)
        {
            i_Cell.HorizontalAlign = HorizontalAlign.Center;
            i_Cell.VerticalAlign = VerticalAlign.Middle;
            i_Cell.Width = 200;
        }

        private void applyCustomizationToRow(ref TableRow i_Row)
        {
            i_Row.HorizontalAlign = HorizontalAlign.Center;
            i_Row.VerticalAlign = VerticalAlign.Middle;
        }
    }
}