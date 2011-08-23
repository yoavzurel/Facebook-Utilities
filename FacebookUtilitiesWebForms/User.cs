using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookUtilitiesWebForms
{
    public class User
    {
        private string m_Id;
        private string m_FullName;
        private string m_FirstName;
        private string m_LastName;
        private string m_Birthday;  //MM/DD/YYYY
        private string m_BirthdayMessage;
        private Dictionary<string, string> m_UserPictures;

        public string Id
        {
            get
            {
                return m_Id;
            }

            set
            {
                m_Id = value;
            }
        }

        public string FullName
        {
            get
            {
                return m_FullName;
            }
            set
            {
                m_FullName = value;
            }
        }

        public string FirstName
        {
            get
            {
                return m_FirstName;
            }
            set
            {
                m_FirstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return m_LastName;
            }
            set
            {
                m_LastName = value;
            }
        }

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
        /// gets or sets all of the users photos. 
        /// Listes accordingly to ePictureTypes
        /// Might hold null values
        /// </summary>
        public Dictionary<string, string> Pictures
        {
            get
            {
                return m_UserPictures;
            }
            set
            {
                m_UserPictures = value;
            }
        }
    }
}
