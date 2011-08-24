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
        User_ID,
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
        Friend_ID,
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
            openConnection();

              SqlDataReader dbReader = queryDataBase(string.Format
                ("SELECT * FROM {0} WHERE {1} = {2}",
                eDataBaseTables.Application_Users.ToString(),
                eTableApplication_User_Columns.User_ID.ToString(),
                i_User.Id));

            if (dbReader.HasRows)
            {
                result = true;
            }

            closeReaderAndConnection(dbReader);
            return result;
        }

        /// <summary>
        /// closes the database reader and data base connection
        /// </summary>
        /// <param name="i_DbReader"></param>
        private void closeReaderAndConnection(SqlDataReader i_DbReader)
        {
            i_DbReader.Close();
            m_DbConnection.Close();
        }

        /// <summary>
        /// opens the connection to db
        /// </summary>
        private void openConnection()
        {
            m_DbConnection.Open();
        }

        /// <summary>
        /// this method returns an sql database reader with the result of the given query
        /// </summary>
        /// <param name="i_Query"></param>
        /// <returns></returns>
        private SqlDataReader queryDataBase(string i_Query)
        {
            m_DbCommand = m_DbConnection.CreateCommand();
            m_DbCommand.CommandText = i_Query;
            SqlDataReader dbReader = m_DbCommand.ExecuteReader();
            return dbReader;
        }

        /// <summary>
        /// This method returns the friends of the user that are already in the database.
        /// Returns a dictionary by {ID,Friend}.
        /// if the user dosn't have any friends in the DB, The count field of the dictionary will 
        /// be 0
        /// </summary>
        /// <param name="i_User"></param>
        /// <returns></returns>
        public Dictionary<string, Friend> GetUserFriendsThatAreInDataBase(User i_User)
        {
            Dictionary<string, Friend> result = new Dictionary<string, Friend>();
            i_User.Id = "2";
            openConnection();
            string query = string.Format(
                "SELECT {0}, {1} FROM {2} WHERE {3} = {4} AND {5} = {6} AND {7} = {8}",
                string.Join(", ", Enum.GetNames(typeof(eTable_Friends_To_Greet_Columns))),
                eTbale_Birthday_Messages_Columns.Birthday_Greet.ToString(),
                string.Join(", ", Enum.GetNames(typeof(eDataBaseTables))),
                string.Join(".", new string[] { eDataBaseTables.Birthday_Messages.ToString(), eTbale_Birthday_Messages_Columns.Application_User_ID.ToString() }),
                string.Join(".", new string[] { eDataBaseTables.Application_Users.ToString(), eTableApplication_User_Columns.User_ID.ToString() }),
                string.Join(".", new string[] { eDataBaseTables.Birthday_Messages.ToString(), eTbale_Birthday_Messages_Columns.Friend_To_Greet_ID.ToString() }),
                string.Join(".", new string[] { eDataBaseTables.Friends_To_Greet.ToString(), eTable_Friends_To_Greet_Columns.Friend_ID.ToString() }),
                string.Join(".", new string[] { eDataBaseTables.Application_Users.ToString(), eTableApplication_User_Columns.User_ID.ToString() }),
                i_User.Id);
            SqlDataReader dbReader = queryDataBase(query);
            while (dbReader.Read())
            {
                Friend tempFriend = new Friend();
                tempFriend.Id = (string)dbReader[eTable_Friends_To_Greet_Columns.Friend_ID.ToString()];
                tempFriend.FirstName = (string)dbReader[eTable_Friends_To_Greet_Columns.Friend_First_Name.ToString()];
                tempFriend.LastName = (string)dbReader[eTable_Friends_To_Greet_Columns.Friend_Last_Name.ToString()];
                tempFriend.FullName = (string)dbReader[eTable_Friends_To_Greet_Columns.Friend_Full_Name.ToString()];
                tempFriend.Birthday = ((DateTime)dbReader[eTable_Friends_To_Greet_Columns.Friend_Birthday_Date.ToString()]).ToShortDateString();
                tempFriend.BirthdayMessage = (string)dbReader[eTbale_Birthday_Messages_Columns.Birthday_Greet.ToString()];
                
                Dictionary<string, string> tempFriendPics = new Dictionary<string, string>();
                tempFriendPics[ePictureTypes.pic.ToString()] = (string)dbReader[eTable_Friends_To_Greet_Columns.Friend_Pic.ToString()];
                tempFriendPics[ePictureTypes.pic_big.ToString()] = (string)dbReader[eTable_Friends_To_Greet_Columns.Friend_Pic_Big.ToString()];
                tempFriendPics[ePictureTypes.pic_small.ToString()] = (string)dbReader[eTable_Friends_To_Greet_Columns.Friend_Pic_Small.ToString()];
                tempFriendPics[ePictureTypes.pic_square.ToString()] = (string)dbReader[eTable_Friends_To_Greet_Columns.Friend_Pic_Square.ToString()];
                tempFriend.Pictures = tempFriendPics;
                result[tempFriend.Id] = tempFriend;
            } 
            return result;
        }
    }
}