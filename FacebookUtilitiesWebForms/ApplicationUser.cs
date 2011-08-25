using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookUtilitiesWebForms
{
    /// <summary>
    /// this class represents a user of the application
    /// </summary>
    public class ApplicationUser: FacebookUser
    {
        
        private string m_AccessToken;
        private string m_Email;
        private DateTime m_RegistrationDate;

        public DateTime RegistrationDate
        {
            get
            {
                return m_RegistrationDate;
            }

            set
            {
                m_RegistrationDate = value;
            }
        }

        public string Email
        {
            get
            {
                return m_Email;
            }

            set
            {
                m_Email = value;
            }
        }

        /// <summary>
        /// this property assign an access token to the user.
        /// the access token is individual per user.
        /// </summary>
        public string AccessToken 
        {
            get
            {
                return m_AccessToken;
            }
            set
            {
                m_AccessToken = value;
            }
        }
    }
}
