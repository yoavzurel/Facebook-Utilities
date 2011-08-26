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
    public enum eTabelsInDataBase
    {
        ApplicationUser,
        Friend,
        Birthday_Messages,
        FacebookUser
    }

    /// <summary>
    /// this enum holds the column names of the application user table
    /// </summary>
    public enum eTableApplicationUser_Columns
    {
        Application_User_ID,
        Access_Token,
        Registration_Date,
        Email
    }

    /// <summary>
    /// this enum holds all of the columns of the facebook user table
    /// </summary>
    public enum eTable_FacebookUser
    {
        ID,
        First_Name,
        Last_Name,
        Full_Name,
        Pic,
        Pic_Big,
        Pic_Small,
        Pic_Square,
        Birthday
    }

    /// <summary>
    /// this enum holds the column names of the birthday messages table
    /// </summary>
    public enum eTbale_Birthday_Messages_Columns
    {
        Birthday_Greet,
        From_Application_User_ID,
        To_Friend_ID
    }

    public enum eTable_Friend
    {
        Friend_ID
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
        /// this method returns true if the facebook user is in the database
        /// </summary>
        /// <param name="i_FacebookUser"></param>
        /// <returns></returns>
        public bool IsUserInDataBase(FacebookUser i_FacebookUser)
        {
            openConnection();
            bool result = checkIfUserIsInDataBaseWithOutConnection(i_FacebookUser);
            closeConnection();
            return result;
        }


        /// <summary>
        /// checks if the user is in the data base without connecting to data base
        /// </summary>
        /// <param name="i_FacebookUser"></param>
        /// <returns></returns>
        private bool checkIfUserIsInDataBaseWithOutConnection(FacebookUser i_FacebookUser)
        {
            bool result = false;
            SqlDataReader dbReader = null;

            if (i_FacebookUser is ApplicationUser)
            {
                dbReader = commandDataBase(string.Format
                  ("SELECT * FROM {0} WHERE {1} = {2} ",
                  eTabelsInDataBase.ApplicationUser.ToString(),
                  eTableApplicationUser_Columns.Application_User_ID.ToString(),
                  i_FacebookUser.Id), true);
            }
            else
            {
                if (i_FacebookUser is Friend)
                {
                    //facebook user is friend
                    dbReader = commandDataBase(string.Format
                      ("SELECT * FROM {0} WHERE {1} = {2} ",
                      eTabelsInDataBase.Friend.ToString(),
                      eTable_Friend.Friend_ID.ToString(),
                      i_FacebookUser.Id), true);
                }
                else
                {
                    //only facebook user
                    dbReader = commandDataBase(string.Format
                      ("SELECT * FROM {0} WHERE {1} = {2} ",
                      eTabelsInDataBase.FacebookUser.ToString(),
                      eTable_FacebookUser.ID.ToString(),
                      i_FacebookUser.Id), true);
                }
            }

            if (dbReader.HasRows)
            {
                result = true;
            }

            closeDataBaseReader(dbReader);
            return result;
        }

        /// <summary>
        /// closes the database reader and data base connection
        /// </summary>
        /// <param name="i_DbReader"></param>
        private void closeReaderAndConnection(SqlDataReader i_DbReader)
        {
            closeDataBaseReader(i_DbReader);
            closeConnection();
        }

        private static void closeDataBaseReader(SqlDataReader i_DbReader)
        {
            i_DbReader.Close();
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
        private SqlDataReader commandDataBase(string i_Query, bool i_IsQuery)
        {
            m_DbCommand = m_DbConnection.CreateCommand();
            m_DbCommand.CommandText = i_Query;
            SqlDataReader dbReader = null;
            if (i_IsQuery)
            {
                dbReader = m_DbCommand.ExecuteReader();
            }
            else
            {
                m_DbCommand.ExecuteNonQuery();
            }
            return dbReader;
        }

        /// <summary>
        /// This method returns the friends of the user that are already in the database.
        /// Returns a dictionary by {ID,Friend}.
        /// if the user dosn't have any friends in the DB, The count field of the dictionary will 
        /// be 0
        /// Query:
        /// SELECT FacebookUser.*,Birthday_Greet
        /// FROM Friend, Birthday_Messages,FacebookUser
        /// WHERE From_Application_User_ID = '2' AND Friend_ID = To_Friend_ID and ID = Friend_ID
        /// </summary>
        /// <param name="i_User"></param>
        /// <returns></returns>
        public Dictionary<string, Friend> GetUserFriendsThatAreInDataBase(ApplicationUser i_User)
        {
            Dictionary<string, Friend> result = new Dictionary<string, Friend>();

            //build query
            string query = string.Format(
                "SELECT {0}, {1} FROM {2}, {3}, {4} WHERE {5} = {6} AND {7} = {8} AND {9} = {10}",
                addDotAndStarToString(eTabelsInDataBase.FacebookUser.ToString()),
                eTbale_Birthday_Messages_Columns.Birthday_Greet.ToString(),
                eTabelsInDataBase.Friend.ToString(),
                eTabelsInDataBase.Birthday_Messages.ToString(),
                eTabelsInDataBase.FacebookUser.ToString(),
                eTbale_Birthday_Messages_Columns.From_Application_User_ID.ToString(),
                i_User.Id,
                eTable_Friend.Friend_ID.ToString(),
                eTbale_Birthday_Messages_Columns.To_Friend_ID.ToString(),
                eTable_FacebookUser.ID.ToString(),
                eTable_Friend.Friend_ID.ToString());

            openConnection();
            SqlDataReader dbReader = commandDataBase(query, true);

            //create friends from result
            while (dbReader.Read())
            {
                Friend tempFriend = new Friend();
                tempFriend.Id = dbReader[eTable_FacebookUser.ID.ToString()].ToString();
                tempFriend.FirstName = (string)dbReader[eTable_FacebookUser.First_Name.ToString()];
                tempFriend.LastName = (string)dbReader[eTable_FacebookUser.Last_Name.ToString()];
                tempFriend.FullName = (string)dbReader[eTable_FacebookUser.Full_Name.ToString()];
                tempFriend.BirthdayDateTime = (DateTime)dbReader[eTable_FacebookUser.Birthday.ToString()];
                tempFriend.BirthdayMessage = (string)dbReader[eTbale_Birthday_Messages_Columns.Birthday_Greet.ToString()];

                //build pictures
                Dictionary<string, string> tempFriendPics = new Dictionary<string, string>();
                tempFriendPics[ePictureTypes.pic.ToString()] = (string)dbReader[eTable_FacebookUser.Pic.ToString()];
                tempFriendPics[ePictureTypes.pic_big.ToString()] = (string)dbReader[eTable_FacebookUser.Pic_Big.ToString()];
                tempFriendPics[ePictureTypes.pic_small.ToString()] = (string)dbReader[eTable_FacebookUser.Pic_Small.ToString()];
                tempFriendPics[ePictureTypes.pic_square.ToString()] = (string)dbReader[eTable_FacebookUser.Pic_Square.ToString()];
                tempFriend.Pictures = tempFriendPics;
                result[tempFriend.Id] = tempFriend;
            }
            closeReaderAndConnection(dbReader);
            return result;
        }


        /// <summary>
        /// This mehtod recieves a tabel name and returns a table name with .*
        /// input: a output a.*
        /// </summary>
        /// <param name="i_TableName"></param>
        /// <returns></returns>
        private string addDotAndStarToString(string i_TableName)
        {
            return string.Format("{0}.*", i_TableName);
        }

        /// <summary>
        /// Inserts a single application user to the data base
        /// </summary>
        /// <param name="i_ApplicationUser"></param>
        public void InsertSingleApplicationUser(ApplicationUser i_ApplicationUser)
        {
            if (!IsUserInDataBase(i_ApplicationUser))
            {
                openConnection();
                commandDataBase(getInsertCommandForFacebookUserInDataBase(i_ApplicationUser), false);
                commandDataBase(getInsertCommandForApplicationUserIntoDataBase(i_ApplicationUser), false);
                closeConnection();
            }
        }

        private string getInsertCommandForFriendIntoDataBase(FacebookUser i_FacebookUser)
        {
            return string.Format(
                                "INSERT INTO {0} VALUES ({1})",
                                eTabelsInDataBase.Friend.ToString(),
                                getValuesOfTableForUser(eTabelsInDataBase.Friend, i_FacebookUser));
        }

        private string getInsertCommandForApplicationUserIntoDataBase(FacebookUser i_FacebookUser)
        {
            return string.Format(
                                    "INSERT INTO {0} VALUES ({1})",
                                    eTabelsInDataBase.ApplicationUser.ToString(),
                                    getValuesOfTableForUser(eTabelsInDataBase.ApplicationUser, i_FacebookUser));
        }

        private string getInsertCommandForFacebookUserInDataBase(FacebookUser i_FacebookUser)
        {
            return string.Format(
                                "INSERT INTO {0} VALUES ({1})",
                                eTabelsInDataBase.FacebookUser.ToString(),
                                getValuesOfTableForUser(eTabelsInDataBase.FacebookUser, i_FacebookUser));
        }

        private string getInsertCommandForBirthdayMessageIntoDataBase(FacebookUser i_FacebookUser, ApplicationUser i_ApplicationUser)
        {
            return string.Format(
                                "INSERT INTO {0} VALUES ({1}, {2})",
                                eTabelsInDataBase.Birthday_Messages.ToString(),
                                getValuesOfTableForUser(eTabelsInDataBase.Birthday_Messages, i_FacebookUser),
                                i_ApplicationUser.Id);
        }

        private void closeConnection()
        {
            m_DbConnection.Close();
        }

        /// <summary>
        /// returns the values for a given user and a table in INSERT syntex
        /// </summary>
        /// <param name="i_Table"></param>
        /// <param name="i_FacebookUser"></param>
        /// <returns></returns>
        private string getValuesOfTableForUser(eTabelsInDataBase i_Table, FacebookUser i_FacebookUser)
        {
            string result = null;
            if (i_Table == eTabelsInDataBase.FacebookUser)
            {
                result = string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'",
                    i_FacebookUser.Id,
                    i_FacebookUser.FirstName,
                    i_FacebookUser.LastName,
                    i_FacebookUser.FullName,
                    i_FacebookUser.Pictures[ePictureTypes.pic.ToString()],
                    i_FacebookUser.Pictures[ePictureTypes.pic_big.ToString()],
                    i_FacebookUser.Pictures[ePictureTypes.pic_small.ToString()],
                    i_FacebookUser.Pictures[ePictureTypes.pic_square.ToString()],
                    i_FacebookUser.BirthdayDateTime.ToShortDateString());
            }

            if (i_Table == eTabelsInDataBase.ApplicationUser)
            {
                ApplicationUser tempUser = i_FacebookUser as ApplicationUser;
                result = string.Format("{0}, '{1}', '{2}', '{3}'",
                tempUser.Id,
                tempUser.Email,
                tempUser.RegistrationDate,
                tempUser.AccessToken);
            }

            if (i_Table == eTabelsInDataBase.Friend)
            {
                Friend tempUser = i_FacebookUser as Friend;
                result = string.Format("{0}",
                    tempUser.Id);
            }

            if (i_Table == eTabelsInDataBase.Birthday_Messages)
            {
                Friend tempUser = i_FacebookUser as Friend;
                result = string.Format("'{0}', {1}",
                    tempUser.BirthdayMessage,
                    tempUser.Id);
            }

            return result;
        }


        /// <summary>
        /// this methods inserts a list of friends into the db.
        /// will only work if the application user is already in db.
        /// </summary>
        /// <param name="i_ApplicationUser"></param>
        /// <param name="i_FriendsToInsert"></param>
        public void InsertFriendsIntoDataBase(ApplicationUser i_ApplicationUser, ICollection<Friend> i_FriendsToInsert)
        {
            string[] insertFacebookUsersCommands = new string[i_FriendsToInsert.Count];
            string[] insertFriendsCommands = new string[i_FriendsToInsert.Count];
            string[] insertBirthdayMessageCommands = new string[i_FriendsToInsert.Count];
            int i = 0;
           
            //building queries foreach friend
            foreach (Friend friend in i_FriendsToInsert)
            {
                insertFacebookUsersCommands[i] = (getInsertCommandForFacebookUserInDataBase(friend));
                insertFriendsCommands[i] = (getInsertCommandForFriendIntoDataBase(friend));
                insertBirthdayMessageCommands[i] = (getInsertCommandForBirthdayMessageIntoDataBase(friend, i_ApplicationUser));
                i++;
            }

            openConnection();

            i = 0;
            //inserting users
            FacebookUser tempFacebookUser = new FacebookUser();
            foreach (Friend friend in i_FriendsToInsert)
            {

                //check if the facebook user isn't in db
                tempFacebookUser.Id = friend.Id;
                if (!checkIfUserIsInDataBaseWithOutConnection(tempFacebookUser))
                {
                    commandDataBase(insertFacebookUsersCommands[i], false);
                }

                //check if the friend isn't in db
                if (!checkIfUserIsInDataBaseWithOutConnection(friend))
                {
                    commandDataBase(insertFriendsCommands[i], false);
                }

                //check if message isn't in db
                if (!messageIsInDatabaseWithoutConnection(i_ApplicationUser, friend))
                {
                    commandDataBase(insertBirthdayMessageCommands[i], false);
                }
                i++;
            }

            closeConnection();
        }

        /// <summary>
        /// checks if a given messgae i.e. combination of appuser-friend is in db
        /// </summary>
        /// <param name="i_ApplicationUser"></param>
        /// <param name="i_Friend"></param>
        /// <returns></returns>
        private bool messageIsInDatabaseWithoutConnection(ApplicationUser i_ApplicationUser, Friend i_Friend)
        {
            bool result = false;
            SqlDataReader dbReader = null;
            string query = string.Format(
                "SELECT * FROM {0} WHERE {1} = {2} AND {3} = {4}",
                   eTabelsInDataBase.Birthday_Messages.ToString(),
                   eTbale_Birthday_Messages_Columns.From_Application_User_ID.ToString(),
                   i_ApplicationUser.Id,
                   eTbale_Birthday_Messages_Columns.To_Friend_ID.ToString(),
                   i_Friend.Id);

            dbReader = commandDataBase(query, true);
            if (dbReader.HasRows)
            {
                result = true;
            }

            closeDataBaseReader(dbReader);
            return result;
        }
    }
}


