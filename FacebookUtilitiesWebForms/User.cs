using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookUtilitiesWebForms
{
    /// <summary>
    /// this class represents the user (facebok user) of the application.
    /// </summary>
    public class User
    {
        private string m_Id;
        private string m_FullName;
        private string m_FirstName;
        private string m_LastName;
        private Dictionary<string, string> m_Pictures;
        private string m_AccessToken;

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
