using System;
using System.Configuration;

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

        #region get configuration

        public static string getTestWorkspace()
        {
            return ConfigurationManager.AppSettings["workspaceID"].ToString();
        }

        public static string getTestNode()
        {
            return ConfigurationManager.AppSettings["nodeID"].ToString();
        }

        public static string getTestToken()
        {
            return ConfigurationManager.AppSettings["token"].ToString();
        }
        #endregion

    }
}
