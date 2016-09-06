using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHubSdkLibrary
{
    public static class Common
    {
        private static string _workspaceID;

        public static string workspaceID
        {
            get { return _workspaceID; }
            set { _workspaceID = value; }
        }


    }
}
