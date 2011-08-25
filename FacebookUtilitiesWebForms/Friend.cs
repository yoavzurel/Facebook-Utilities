using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookUtilitiesWebForms
{
    /// <summary>
    /// This calls represent the friend of an application user.
    /// </summary>
    public class Friend: FacebookUser
    {
        private string m_BirthdayMessage;
        /// <summary>
        /// gets and sets the user birthday message. 
        /// This message will be displayed on the users wall apon his birthday.
        /// </summary>
        public string BirthdayMessage
        {
            get
            {
                return m_BirthdayMessage;
            }

            set
            {
                m_BirthdayMessage = value;
            }
        }
    }
}