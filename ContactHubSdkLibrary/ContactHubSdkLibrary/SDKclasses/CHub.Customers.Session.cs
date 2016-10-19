using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using System;


namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class Node
    {
        #region customers session

        public Session AddCustomerSession(string customerID, Session session, ref Error error)
        {
            Session returnSession = null;
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(session, settings);
            string statusCode = null;
            string url = String.Format("/customers/{0}/sessions", customerID);
            string jsonResponse = DoPostWebRequest(url, postData, ref statusCode);
            Common.WriteLog("-> AddCustomerSession() post data:", "querystring:" + url + " data:" + postData);
            Common.WriteLog("<- AddCustomerSession() return data:", jsonResponse);
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                 returnSession = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Session>(jsonResponse));
            }
            else
            {
                returnSession = null;
            }
            return returnSession;
        }
      
        #endregion
    }
}
