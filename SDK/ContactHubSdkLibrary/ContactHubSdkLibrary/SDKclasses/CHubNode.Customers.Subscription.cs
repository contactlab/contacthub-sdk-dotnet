using Newtonsoft.Json;
using System;


namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class CHubNode
    {
        #region customers subscriptions
        // get subscription detail
        public Subscriptions GetCustomerSubscription(string customerID, string subscriptionID)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string jsonResponse = DoGetWebRequest(String.Format("/customers/{0}/subscriptions/{1}", customerID, subscriptionID));
            Subscriptions returnLike = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Subscriptions>(jsonResponse));
            return returnLike;
        }
        //add like to customer
        public Subscriptions AddCustomerSubscription(string customerID, Subscriptions like)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(like, settings);
            string statusCode = null;
            string jsonResponse = DoPostWebRequest(String.Format("/customers/{0}/subscriptions", customerID), postData,ref statusCode);
            Subscriptions returnLike = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Subscriptions>(jsonResponse));
            return returnLike;
        }
        #endregion
    }
}
