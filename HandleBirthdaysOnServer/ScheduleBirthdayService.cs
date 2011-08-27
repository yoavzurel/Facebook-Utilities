using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandleBirthdaysOnServer
{

    /// <summary>
    /// this class runs once a day and at a given time activates the birthday service
    /// </summary>
    class ScheduleBirthdayService
    {
        bool m_TimeToWork = true;
        internal void Run()
        {
            //TODO: implement schedule
            if (m_TimeToWork)
            {
                activateBirthdayService();
            }

        }


        /// <summary>
        /// if the daily time has arrived activate the birthday service
        /// </summary>
        private void activateBirthdayService()
        {
            (new BirthdayService()).Run();
        }
    }
}
