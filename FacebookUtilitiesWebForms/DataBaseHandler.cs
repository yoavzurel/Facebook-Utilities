using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace FacebookUtilitiesWebForms
{
    /// <summary>
    /// this enum hold the data base table names
    /// </summary>
    public enum eDataBaseTables
    {
        Application_Users,
        Friends_To_Greet,
        Birthday_Messages
    }

    /// <summary>
    /// this enum holds the column names of the application user table
    /// </summary>
    public enum eTableApplication_User_Columns
    {
        ID,
        User_First_Name,
        User_Last_Name,
        User_Full_Name,
        User_Access_Token,
        User_Pic,
        User_Pic_Big,
        User_Pic_Small,
        User_Pic_Square,
        User_Registration_Date,
        User_Email
    }

    /// <summary>
    /// this enum holds the column names of the friends to greet table
    /// </summary>
    public enum eTable_Friends_To_Greet_Columns
    {
        ID,
        Friend_First_Name,
        Friend_Last_Name,
        Friend_Full_Name,
        Friend_Pic,
        Friend_Pic_Big,
        Friend_Pic_Small,
        Friend_Pic_Square,
        Friend_Birthday_Date
    }

    /// <summary>
    /// this enum holds the column names of the birthday messages table
    /// </summary>
    public enum eTbale_Birthday_Messages_Columns
    {
        Birthday_Greet,
        Application_User_ID,
        Friend_To_Greet_ID
    }

    //Yoav Connection String: "Data Source=YOAVZUREL-PC;Initial Catalog=FacebookBirthdayUtility;Integrated Security=True";
    //David Connection String: {Fill Here}

 
    /// <summary>
    /// This class makes calls to the database
    /// This class don't handle exceptions. 
    /// Must use try catch block with every function
    /// </summary>
    public class DataBaseHandler
    {
        private const string m_ConnectionString = "Data Source=YOAVZUREL-PC;Initial Catalog=FacebookBirthdayUtility;Integrated Security=True";
        private SqlConnection m_DbConnection = new SqlConnection(m_ConnectionString);
        SqlCommand m_DbCommand;

        /// <summary>
        /// this method returns true if the user is in the database
        /// </summary>
        /// <param name="i_User"></param>
        /// <returns></returns>
        public bool IsUserInDataBase(User i_User)
        {
            bool result = false;
            m_DbConnection.Open();
            m_DbCommand = m_DbConnection.CreateCommand();
            m_DbCommand.CommandText = string.Format
                ("SELECT * FROM {0} WHERE {1} = {2}",
                eDataBaseTables.Application_Users.ToString(),
                eTableApplication_User_Columns.ID.ToString(),
                i_User.Id);
            SqlDataReader dbReader = m_DbCommand.ExecuteReader();
            if (dbReader.HasRows)
            {
                result = true;
            }

            dbReader.Close();
            m_DbConnection.Close();
            return result;
        }

        /// <summary>
        /// This method returns the friends of the user that are already in the database.
        /// Returns a dictionary by {ID,Friend}
        /// </summary>
        /// <param name="i_User"></param>
        /// <returns></returns>
        public Dictionary<string, Friend> GetUserFriendsThatAreInDataBase(User i_User)
        {
            Dictionary<string, Friend> result = new Dictionary<string, Friend>();
            return result;
        }

    }
}