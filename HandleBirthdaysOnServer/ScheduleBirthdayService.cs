using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HandleBirthdaysOnServer
{

    /// <summary>
    /// this class runs once a day and at a given time activates the birthday service
    /// </summary>
    class ScheduleBirthdayService
    {
        internal void Run()
        {
            Timer timer = new Timer(activateBirthdayService, null, timeToStartTheTimerFrom(), timeBetweenTimerCalls());

            //run infinantly
            Thread.Sleep(Timeout.Infinite);
        }

        private TimeSpan timeBetweenTimerCalls()
        {
            return new TimeSpan(24, 0, 0);
        }

        private TimeSpan timeToStartTheTimerFrom()
        {
            return new TimeSpan(0, 0, 0);
        }


        /// <summary>
        /// if the daily time has arrived activate the birthday service
        /// </summary>
        private static void activateBirthdayService(Object stateInfo)
        {
            System.Console.WriteLine("starting birthday service");
            BirthdayService birthdayService = new BirthdayService();
            birthdayService.Run();
        }
    }
}
