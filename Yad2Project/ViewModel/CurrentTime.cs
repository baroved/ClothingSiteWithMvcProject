using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yad2Project.ViewModel
{
    public static class CurrentTime
    {
        static int currentTime;
        static CurrentTime()
        {
            currentTime = DateTime.Now.Hour;

        }
        public static string GetStatus()
        {
            string s;
            if (currentTime >= 0 && currentTime < 12)
                s = "Good morning";
            else
             if (currentTime >= 12 && currentTime < 18)
                s = "Good afternoon";
            else
                s = "Good night";

            return s;
        }
    }
}