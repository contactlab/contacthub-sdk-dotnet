using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHubSdkLibrary.Test
{
    public static class Util
    {
        public static int GetWaitTime()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["waitTime"]);
        }

        public static int GetExitTime()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["exitTime"]);
        }

    }
}
