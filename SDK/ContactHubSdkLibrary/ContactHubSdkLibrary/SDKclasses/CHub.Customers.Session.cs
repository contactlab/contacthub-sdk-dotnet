using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using System;


namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class Node
    {
        #region customers session

        public Session AddCustomerSession(string customerID, Session session)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(session, settings);
            string statusCode = null;
            string jsonResponse = DoPostWebRequest(String.Format("/customers/{0}/sessions", customerID), postData, ref statusCode);
            Session returnSession = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Session>(jsonResponse));
            return returnSession;
        }
      
        #endregion
    }
}
