using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace FacebookUtilitiesWebForms
{
    /// <summary>
    /// This class makes calls to the database
    /// This class don't handle exceptions. 
    /// Must use try catch block with every function
    /// </summary>
    public class DataBaseHandler
    {
        //Yoav Connection String: "Data Source=YOAVZUREL-PC;Initial Catalog=FacebookBirthdayUtility;Integrated Security=True";
        //David Connection String: {Fill Here}

        private User m_ApplicationUser;
        private Dictionary<string, Friend> m_FriendsToGreet;
        private string m_ConnectionString = "Data Source=YOAVZUREL-PC;Initial Catalog=FacebookBirthdayUtility;Integrated Security=True";
        public DataBaseHandler(User i_ApplicationUser, Dictionary<string, Friend> i_FriendsToGreet)
        {
            m_ApplicationUser = i_ApplicationUser;
            m_FriendsToGreet = i_FriendsToGreet;
        }

        /// <summary>
        /// this method returns true if the user is in the database
        /// </summary>
        /// <param name="i_User"></param>
        /// <returns></returns>
        public bool IsUserInDataBase(User i_User)
        {
            bool result = false;

            SqlConnection dbConnection = new SqlConnection(m_ConnectionString);
            dbConnection.Open();
            SqlCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = string.Format("SELECT * FROM Application_Users WHERE ID = {0}", i_User.Id);
            SqlDataReader dbReader = dbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                result = true;
            }

            dbReader.Close();
            dbConnection.Close();
            return result;
        }


    }
}