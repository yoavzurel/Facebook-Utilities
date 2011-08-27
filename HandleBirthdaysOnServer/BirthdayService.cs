using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookUtilitiesWebForms;

namespace HandleBirthdaysOnServer
{

    /// <summary>
    /// this class works once a day and handles the birthday service
    /// </summary>
    class BirthdayService
    {
        /// <summary>
        /// this dictionarry holds for each application user the list of friends he wants to greet
        /// </summary>
        Dictionary<ApplicationUser, List<Friend>> m_ListOfFriendsToGreetForEachApplicationUser;

        /// <summary>
        /// this method starts the birthday service
        /// </summary>
        internal void Run()
        {
            if (serviceNeedsToWork())
            {
                foreach (ApplicationUser applicationUser in m_ListOfFriendsToGreetForEachApplicationUser.Keys)
                {
                    BirthdayMessageSender birthdayMessageSender = new BirthdayMessageSender(applicationUser, m_ListOfFriendsToGreetForEachApplicationUser[applicationUser]);
                    List<Friend> listOfFriendsThatGotGreetedSuccessfully = birthdayMessageSender.SendBirthdayMessages();
                    EmailSender emailSender = new EmailSender(applicationUser, listOfFriendsThatGotGreetedSuccessfully);
                    emailSender.SendApplicationUserTodaysGreetingStatus();
                }
            }
        }

        /// <summary>
        /// this method checks with the data base if there are friends who needs to be greeted.
        /// If there are it initiates the m_WorkList
        /// </summary>
        /// <returns></returns>
        private bool serviceNeedsToWork()
        {
            bool result = false;
            DataBaseHandler db = new DataBaseHandler();
            m_ListOfFriendsToGreetForEachApplicationUser = db.GetFriendsOfApplicationUsersWithBirthdaysToday();
            if (m_ListOfFriendsToGreetForEachApplicationUser.Count != 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
