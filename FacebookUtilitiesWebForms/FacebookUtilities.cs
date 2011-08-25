using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook;

namespace FacebookUtilitiesWebForms
{

    /// <summary>
    /// this class provides all communication from facebook
    /// </summary>
    public class FacebookUtilities
    {
        /// <summary>
        /// gets the Application User representation of "me"
        /// </summary>
        /// <param name="i_AccessToken"></param>
        /// <returns></returns>
        public static ApplicationUser GetUser(string i_AccessToken)
        {
            ApplicationUser result = new ApplicationUser();
            FacebookClient fbClient = new FacebookClient(i_AccessToken); ;
            dynamic me = fbClient.Query(
             "SELECT uid, name, first_name, last_name, pic_small, pic_big, pic_square, pic, email, birthday_date FROM user WHERE uid = me()");
            result = createUserFromDynamicUser(me[0], result);
            result.AccessToken = i_AccessToken;
            return result;
        }

        private static FacebookUser createUserFromDynamicUser(dynamic i_DynamicUser, FacebookUser i_User)
        {
            i_User.Id = i_DynamicUser.uid;
            i_User.FullName = i_DynamicUser.name;
            i_User.FirstName = i_DynamicUser.first_name;
            i_User.LastName = i_DynamicUser.last_name;
            i_User.Birthday = i_DynamicUser.birthday_date;

            Dictionary<string, string> tempFriendPics = new Dictionary<string, string>();
            tempFriendPics[ePictureTypes.pic_small.ToString()] = i_DynamicUser.pic_small;
            tempFriendPics[ePictureTypes.pic_big.ToString()] = i_DynamicUser.pic_big;
            tempFriendPics[ePictureTypes.pic_square.ToString()] = i_DynamicUser.pic_square;
            tempFriendPics[ePictureTypes.pic.ToString()] = i_DynamicUser.pic;
            i_User.Pictures = tempFriendPics;

            if (i_User is ApplicationUser)
            {
                (i_User as ApplicationUser).Email = i_DynamicUser.email;
            }

            return i_User;
        }

        public static Dictionary<string, Friend> GetUsersFriends(string i_AccessToken)
        {
            Dictionary<string, Friend>  result = new Dictionary<string, Friend>();
            FacebookClient fbClient = new FacebookClient(i_AccessToken);
          
            dynamic userFriends = fbClient.Query(
             "SELECT uid, name, first_name, last_name, pic_small, pic_big, pic_square, pic, birthday_date FROM user WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = me())");
            foreach (dynamic friend in userFriends)
            {
                Friend tempFriend = new Friend();
                createUserFromDynamicUser(friend, tempFriend);
                result.Add(tempFriend.Id, tempFriend);
            }

            return result;
        }
    }
}