using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;

namespace FacebookUtilitiesWebForms
{
    public delegate void AccessTokenRetrieved(string accessToken);
    public class FacebookLoginAuthintication
    {
        private FacebookOAuthClient m_AuthurazationClient;
        private string k_AppId = "127571767338990";
        private string k_AppSecret = "b984099795f7b1ba7aeb2c4c8ba196d3";
        private string k_CanvasUrl = "http://localhost/FacebookUtilitiesWebForms/";
        private static string[] m_Permissions = new string[] { "friends_birthday", "user_birthday", "publish_stream", "offline_access" };
        public event AccessTokenRetrieved accessTokenRetrieved;

        public FacebookLoginAuthintication()
        {
            m_AuthurazationClient = new FacebookOAuthClient();
            m_AuthurazationClient.AppId = k_AppId;
            m_AuthurazationClient.AppSecret = k_AppSecret;
            m_AuthurazationClient.RedirectUri = new Uri(k_CanvasUrl);
            m_AuthurazationClient.ExchangeCodeForAccessTokenCompleted += new EventHandler<FacebookApiEventArgs>(m_AuthurazationClient_ExchangeCodeForAccessTokenCompleted);

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

        void m_AuthurazationClient_ExchangeCodeForAccessTokenCompleted(object sender, FacebookApiEventArgs e)
        {
            if (accessTokenRetrieved != null)
            {
                accessTokenRetrieved(e.GetResultData().ToString());
            }
        }
    }
}
