using Newtonsoft.Json;
using System;


namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class CHubNode
    {
        #region customers likes
        /// get like detail
        public Likes GetCustomerLike(string customerID, string likeID)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string jsonResponse = DoGetWebRequest(String.Format("/customers/{0}/likes/{1}", customerID, likeID));
            Likes returnLike = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Likes>(jsonResponse));
            return returnLike;
        }
        ///add like to customer
        public Likes AddCustomerLike(string customerID, Likes like)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(like, settings);
            string jsonResponse = DoPostWebRequest(String.Format("/customers/{0}/likes", customerID), postData);
            Likes returnLike = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Likes>(jsonResponse));
            return returnLike;
        }
        #endregion
    }
}
