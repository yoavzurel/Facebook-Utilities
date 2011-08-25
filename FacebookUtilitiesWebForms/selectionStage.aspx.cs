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
    /// this web form will hold the selection stage
    /// </summary>
    public partial class selectionStage : System.Web.UI.Page
    {
        private string m_AccessToken;
        private FacebookClient m_FacebookClient;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (m_AccessToken == null)
            {
                m_AccessToken = Request.QueryString["access_token"];
                testyoav();
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["action"] == "next")
                {
                    nextButton_Click(this, null);
                }

                //first time in the stage
                //m_AccessToken = Request.QueryString["access_token"];
                if (!string.IsNullOrEmpty(m_AccessToken))
                {
                    m_FacebookClient = new FacebookClient(m_AccessToken);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nextButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("messageWriteStage.aspx?friends={0}&access_token={1}", 
                friendsLabelHidden.Value, m_AccessToken));
        }











        private void testyoav()
        {
            ApplicationUser me = FacebookUtilities.GetUser(m_AccessToken);
            Dictionary<string, Friend> friends = FacebookUtilities.GetUsersFriends(m_AccessToken);
            DataBaseHandler db = new DataBaseHandler();
            bool shouldBeFalse = db.IsUserInDataBase(me);
        }
    }
}