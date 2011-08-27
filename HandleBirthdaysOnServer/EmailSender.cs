using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookUtilitiesWebForms;

namespace HandleBirthdaysOnServer
{

    /// <summary>
    /// this class is incharge of sending emails to application users
    /// </summary>
    class EmailSender
    {
        private ApplicationUser applicationUser;
        private List<Friend> listOfFriendsThatGotGreetedSuccessfully;

        public EmailSender(ApplicationUser applicationUser, List<Friend> listOfFriendsThatGotGreetedSuccessfully)
        {
            // TODO: Complete member initialization
            this.applicationUser = applicationUser;
            this.listOfFriendsThatGotGreetedSuccessfully = listOfFriendsThatGotGreetedSuccessfully;
        }

        internal void SendApplicationUserTodaysGreetingStatus()
        {
            throw new NotImplementedException();
        }
    }
}
