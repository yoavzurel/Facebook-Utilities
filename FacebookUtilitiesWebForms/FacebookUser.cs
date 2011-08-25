using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookUtilitiesWebForms
{

    /// <summary>
    /// this class represents a Facebook User
    /// </summary>
    public class FacebookUser
    {
        private string m_Id;
        private string m_FullName;
        private string m_FirstName;
        private string m_LastName;
        private Dictionary<string, string> m_Pictures;
        private string m_Birthday;
        private DateTime m_DateTimeBirthday;

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

        /// <summary>
        /// gets or sets all of the users photos. 
        /// Listes accordingly to ePictureTypes
        /// Might hold null values
        /// </summary>
        public Dictionary<string, string> Pictures
        {
            get
            {
                return m_Pictures;
            }
            set
            {
                m_Pictures = value;
            }
        }

        /// <summary>
        /// The friend birthday date. 
        /// FQL field: birthday_date.
        /// Representation: MM/DD/YYYY.
        /// </summary>
        public string Birthday
        {
            set
            {
                m_Birthday = value;
                DateTime friendBirthday;

                if (DateTime.TryParse(m_Birthday, out friendBirthday))
                {
                    m_DateTimeBirthday = friendBirthday;
                }
                else
                {
                    m_DateTimeBirthday = DateTime.MinValue;
                }
            }

            get
            {
                return m_Birthday;
            }
        }

        /// <summary>
        /// returns the user birthday represented as DateTime.
        /// If this field is not assinged it will hold DateTime.MinValue
        /// </summary>
        public DateTime BirthdayDateTime
        {
            get
            {
                return m_DateTimeBirthday;
            }
            set
            {
                m_DateTimeBirthday = value;
            }
        }
    }
}