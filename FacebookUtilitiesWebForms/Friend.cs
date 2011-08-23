using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookUtilitiesWebForms
{
    /// <summary>
    /// This class represent the user of the application friend. 
    /// A friens is also a user.
    /// </summary>
    public class Friend: User
    {
        private string m_BirthdayMessage;
        private string m_Birthday; 
        
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

        /// <summary>
        /// The friend birthday date. 
        /// FQL field: birthday_date.
        /// Representation: MM/DD/YYYY.
        /// </summary>
        public string Birthday
        {
            get
            {
                return m_Birthday;
            }
            set
            {
                m_Birthday = value;
            }
        }
    }
}