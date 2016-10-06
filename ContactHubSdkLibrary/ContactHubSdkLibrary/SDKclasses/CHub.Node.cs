using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using System;


namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class Node
    {
        //private bool _isValid;
        private string _node = null;
        private string _workspaceID = null;
        private string _token = null;
        private const string _baseURL = Const.APIURL;
        //public bool isValid { get { return _isValid; } }
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
            //_isValid = true;  //NEXT IMPLEMENTATION
        }
        #endregion

        #region extendedProperties

        public string GetExtendedPropertiesConfiguration()
        {
            string jsonResponse = DoGetWebRequest(String.Format("/configuration/properties/extended"));
            var returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<Customer>(jsonResponse) : null);
            return null;
        }



        public string GetMe()
        {
            string jsonResponse = DoGetWebRequest(Const.APIBASEURL + String.Format("/me"), false);
            var returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<UserInfo>(jsonResponse) : null);
            return null;
        }

        //public string SetExtendedPropertiesConfiguration()
        //{
        //    //            /configuration/properties/extended
        //    //var returnValue = null;
        //    string jsonData = @"{
        //                ""membership_card_nr"": {
        //                    ""description"": ""il numero della membership card"",
        //                    ""type"": ""number"",
        //                    ""contactlabProperties"":{
        //                                  ""label"": ""Membership Card"",
        //                                  ""mergePolicy"": ""OBJ_PRIORITY"",
        //                                  ""enabled"": true
        //                         }
        //                   }
        //                }";
        //    string jsonResponse = DoPutWebRequest(String.Format("/configuration/properties/extended"),jsonData);

        //    var returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<Customer>(jsonResponse) : null);
        //    return null;
        //}



        #endregion


    }
}

