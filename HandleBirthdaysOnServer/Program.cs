using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HandleBirthdaysOnServer
{
    class Program
    {
        //program enrty point
        static void Main()
        {
            (new ScheduleBirthdayService()).Run();
        }
    }
}