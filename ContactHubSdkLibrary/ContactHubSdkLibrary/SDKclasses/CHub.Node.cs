using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using System;


namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class Node
    {
        private string _node = null;
        private string _workspaceID = null;
        private string _token = null;
        private const string _baseURL = Const.APIURL;
        public string node { get { return _node; } }
        public string id;
        public string token { get { return _token; } }

        public object Util { get; private set; }
        public object HttpServerUtility { get; private set; }

        #region Node builder
        public Node(string workspaceID, string tokenID, string nodeID)
        {
            Init(workspaceID, tokenID, nodeID);
            id = nodeID;
        }
        private void Init(string workspaceID, string tokenID, string nodeID)
        {
            _workspaceID = workspaceID;
            _token = tokenID;
            _node = nodeID;
        }
        #endregion

        #region extendedProperties

        public void GetExtendedPropertiesConfiguration(ref Error error)
        {
            string url = String.Format("/configuration/properties/extended");
            string jsonResponse = DoGetWebRequest(url);
            Common.WriteLog("-> GetExtendedPropertiesConfiguration() get data:", "querystring:" + url);
            Common.WriteLog("<- GetExtendedPropertiesConfiguration() return data:", jsonResponse);
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                var returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<Customer>(jsonResponse) : null);
            }
        }

        public void GetMe(ref Error error)
        {
            string url = Const.APIBASEURL + String.Format("/me");
            string jsonResponse = DoGetWebRequest(url, false);
            Common.WriteLog("-> GetMe() get data:", "querystring:" + url);
            Common.WriteLog("<- GetMe() return data:", jsonResponse);
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                var returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<UserInfo>(jsonResponse) : null);
            }
        }

        #endregion
    }
}

