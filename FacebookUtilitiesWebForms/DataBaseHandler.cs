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
            bool result = false;
            openConnection();
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
                //facebook user is friend
                dbReader = commandDataBase(string.Format
                  ("SELECT * FROM {0} WHERE {1} = {2} ",
                  eTabelsInDataBase.Friend.ToString(),
                  eTable_Friend.Friend_ID.ToString(),
                  i_FacebookUser.Id), true);
            }

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
            closeConnection();
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
                tempFriend.Id = (string)dbReader[eTable_FacebookUser.ID.ToString()];
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
        /// Inserts the facebook user to the data base
        /// </summary>
        /// <param name="i_FacebookUser"></param>
        public void InsertFacebookUser(FacebookUser i_FacebookUser)
        {
            if (!IsUserInDataBase(i_FacebookUser))
            {
                string insertToFacebookUserTableCommand = string.Format(
                    "INSERT INTO {0} VALUES ({1})",
                    eTabelsInDataBase.FacebookUser.ToString(),
                    getValuesOfTableForUser(eTabelsInDataBase.FacebookUser, i_FacebookUser));

                string insertAsPolimophicTypeCommand;
                if (i_FacebookUser is ApplicationUser)
                {
                    insertAsPolimophicTypeCommand = string.Format(
                        "INSERT INTO {0} VALUES ({1})",
                        eTabelsInDataBase.ApplicationUser.ToString(),
                        getValuesOfTableForUser(eTabelsInDataBase.ApplicationUser, i_FacebookUser));
                }
                else
                {
                    //insert facebook user as friend
                    insertAsPolimophicTypeCommand = string.Format(
                    "INSERT INTO {0} VALUES ({1})",
                    eTabelsInDataBase.Friend.ToString(),
                    getValuesOfTableForUser(eTabelsInDataBase.Friend, i_FacebookUser));
                }

                openConnection();
                commandDataBase(insertToFacebookUserTableCommand, false);
                commandDataBase(insertAsPolimophicTypeCommand, false);
                closeConnection();
            }
        }

        private void closeConnection()
        {
            m_DbConnection.Close();
        }

        private string getValuesOfTableForUser(eTabelsInDataBase i_Table, FacebookUser i_ApplicationUser)
        {
            string result = null;
            if (i_Table == eTabelsInDataBase.FacebookUser)
            {
                result = string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'",
                    i_ApplicationUser.Id,
                    i_ApplicationUser.FirstName,
                    i_ApplicationUser.LastName,
                    i_ApplicationUser.FullName,
                    i_ApplicationUser.Pictures[ePictureTypes.pic.ToString()],
                    i_ApplicationUser.Pictures[ePictureTypes.pic_big.ToString()],
                    i_ApplicationUser.Pictures[ePictureTypes.pic_small.ToString()],
                    i_ApplicationUser.Pictures[ePictureTypes.pic_square.ToString()],
                    i_ApplicationUser.Birthday);
            }

            if (i_Table == eTabelsInDataBase.ApplicationUser)
            {
                ApplicationUser tempUser = i_ApplicationUser as ApplicationUser;
                result = string.Format("'{0}', '{1}', '{2}', '{3}'",
                tempUser.Id,
                tempUser.Email,
                tempUser.RegistrationDate,
                tempUser.AccessToken);
            }


            if (i_Table == eTabelsInDataBase.Friend)
            {
                Friend tempUser = i_ApplicationUser as Friend;
                result = string.Format("'{0}'",
                    tempUser.Id);
            }

            if (i_Table == eTabelsInDataBase.Birthday_Messages)
            {


            }

            return result;
        }



        /// <summary>
        /// this method recieves a user and his friends and insert them into the data base
        /// </summary>
        /// <param name="i_User"></param>
        /// <param name="i_FriendsToGreet"></param>
        public void AddUserAndFriendsToDataBase(ApplicationUser i_User, List<Friend> i_FriendsToGreet)
        {
            if (!IsUserInDataBase(i_User))
            {
                openConnection();
                string insertUserQuery = string.Format(
                    "INSERT INTO {0} VALUES ({1})",
                    eTabelsInDataBase.ApplicationUser.ToString(),
                createValuesFromUserForInsertQuery(i_User));
                SqlDataReader dbReader = commandDataBase(insertUserQuery, false);
                closeReaderAndConnection(dbReader);
            }
        }

        private string createValuesFromUserForInsertQuery(ApplicationUser i_User)
        {
            string[] arrayOfValues = new string[]{
                i_User.Id, i_User.FirstName, i_User.LastName, i_User.FullName,
                i_User.AccessToken, i_User.Pictures[ePictureTypes.pic.ToString()],
                i_User.Pictures[ePictureTypes.pic_big.ToString()],
                i_User.Pictures[ePictureTypes.pic_small.ToString()],
                i_User.Pictures[ePictureTypes.pic_square.ToString()],
                i_User.RegistrationDate.ToShortDateString(), i_User.Email};
            return string.Join(", ", arrayOfValues);
        }

    }
}
