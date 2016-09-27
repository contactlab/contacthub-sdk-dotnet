using Newtonsoft.Json;
using System;


namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class CHubNode
    {
        #region customers likes
        /// <summary>
        /// Get like detail
        /// </summary>
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
        /// <summary>
        /// Add like to customer
        /// </summary>
        public Likes AddCustomerLike(string customerID, Likes like)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(like, settings);
            string statusCode = null;
            string jsonResponse = DoPostWebRequest(String.Format("/customers/{0}/likes", customerID), postData, ref statusCode);
            Likes returnLike = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Likes>(jsonResponse));
            return returnLike;
        }
        /// <summary>
        /// Update customers job
        /// </summary>
        public Likes UpdateCustomerLike(string customerID, Likes like)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(like, settings);
            string statusCode = null;
            string jsonResponse = DoPutWebRequest(String.Format("/customers/{0}/likes/{1}", customerID, like.id), postData, ref statusCode);
            Likes returnJobs = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Likes>(jsonResponse));
            return returnJobs;
        }
        #endregion
    }
}
