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
        List<UserAndFriendsRelationship> m_GreetingJobs;

        /// <summary>
        /// this method starts the birthday service
        /// </summary>
        internal void Run()
        {
            if (serviceNeedsToWork())
            {
                foreach (UserAndFriendsRelationship job in m_GreetingJobs)
                {
                    BirthdayMessageSender birthdayMessageSender = new BirthdayMessageSender(job);
                    List<Friend> listOfFriendsThatGotGreetedSuccessfully = birthdayMessageSender.SendBirthdayMessages();
                    EmailSender emailSender = new EmailSender(job.ApplicationUser, listOfFriendsThatGotGreetedSuccessfully);
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
            m_GreetingJobs = db.GetFriendsOfApplicationUsersWithBirthdaysToday();
            if (m_GreetingJobs.Count != 0)
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
