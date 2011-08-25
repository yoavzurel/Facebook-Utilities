using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;

namespace FacebookUtilitiesWebForms
{
    public class FacebookLoginAuthintication
    {
        private FacebookOAuthClient m_AuthurazationClient;
        private string k_CanvasUrl = "http://localhost/FacebookUtilitiesWebForms/";
        private static string[] m_Permissions = new string[] { "friends_birthday", "publish_stream", "offline_access", "email"};

        public FacebookLoginAuthintication()
        {
            m_AuthurazationClient = new FacebookOAuthClient(FacebookApplication.Current);
            m_AuthurazationClient.RedirectUri = new Uri(k_CanvasUrl);
        }
        /// <summary>
        /// returns the login uri
        /// </summary>
        /// <returns></returns>
        internal Uri GetLoginUri()
        {
            //begin authurization
            var OAuthorizationParameters = new Dictionary<string, object>();
            OAuthorizationParameters.Add("display", "page");

            //set permissions
            var scope = string.Join(",", m_Permissions);
            OAuthorizationParameters.Add("scope", scope);
            return m_AuthurazationClient.GetLoginUrl(OAuthorizationParameters);
        }

        internal object RetrieveAccessToken(string m_Code)
        {
            return m_AuthurazationClient.ExchangeCodeForAccessToken(m_Code);
        }
    }
}
