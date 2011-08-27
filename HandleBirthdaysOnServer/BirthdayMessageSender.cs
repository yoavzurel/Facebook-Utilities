using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookUtilitiesWebForms;

namespace HandleBirthdaysOnServer
{
    /// <summary>
    /// this class posts on the application user friends wall thier birthday message from the application user
    /// </summary>
    class BirthdayMessageSender
    {
        private ApplicationUser applicationUser;
        private List<Friend> list;

        public BirthdayMessageSender(ApplicationUser applicationUser, List<Friend> list)
        {
            // TODO: Complete member initialization
            this.applicationUser = applicationUser;
            this.list = list;
        }

        internal List<Friend> SendBirthdayMessages()
        {
            throw new NotImplementedException();
        }
    }
}
