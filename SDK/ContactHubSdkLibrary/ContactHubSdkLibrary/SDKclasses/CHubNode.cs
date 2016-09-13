using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using System;


namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class CHubNode
    {
        public bool isValid; //da implementare
        private string _workspaceID = null;
        private string _token = null;
        private string _node = null;
        private const string _baseURL = "https://api.contactlab.it/hub/v1/workspaces/{id-workspace}";

        public object Util { get; private set; }
        public object HttpServerUtility { get; private set; }

        #region Node builder
        public CHubNode(string workspaceID, string token, string node)
        {
            Init(workspaceID, token, node);
        }
        private void Init(string workspaceID, string token, string node)
        {
            _workspaceID = workspaceID;
            _token = token;
            _node = node;
            isValid = true;  //da implementare controllo
        }
        #endregion

        #region extendedProperties

        public string GetExtendedPropertiesConfiguration()
        {
            //            /configuration/properties/extended
            //var returnValue = null;
            string jsonResponse = DoGetWebRequest(String.Format("/configuration/properties/extended"));

            var returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<Customer>(jsonResponse) : null);
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

