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
    
    // Github SSH: git@github.com:yoavzurel/Facebook-Utilities.git
    public partial class defualt : System.Web.UI.Page
    {
        private bool m_UserIsLoggedIn = false;
        private FacebookLoginAuthintication m_FacebookLoginClient = new FacebookLoginAuthintication();
        private string m_Code;

        protected void Page_Load(object sender, EventArgs e)
        {
            string response = HttpContext.Current.Request.Url.AbsoluteUri;

            //validating user authurization
            FacebookOAuthResult userOAuthResult;
            if (FacebookOAuthResult.TryParse(response, out userOAuthResult))
            {
                m_UserIsLoggedIn = userOAuthResult.IsSuccess;
                m_Code = userOAuthResult.Code;
            }
            else
            {
                // if this is the first load of the page, we need to initiate a user login sequence
                Uri loginUri = m_FacebookLoginClient.GetLoginUri();
                Response.Redirect(loginUri.AbsoluteUri);
            }

            if (m_UserIsLoggedIn)
            {
                //redirect to the selection stage with the access token
                dynamic m_AccessToken = m_FacebookLoginClient.RetrieveAccessToken(m_Code);
                string accessToken = m_AccessToken.access_token;
                Response.Redirect(string.Format("welcomeStage.aspx?access_token={0}", accessToken));
            }
            else
            {
                Response.Write("problem logging in");
            }
        }
    }
}