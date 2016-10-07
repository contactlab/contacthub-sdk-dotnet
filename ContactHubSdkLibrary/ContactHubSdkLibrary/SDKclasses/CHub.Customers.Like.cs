using Newtonsoft.Json;
using System;


namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class Node
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
            string url = String.Format("/customers/{0}/likes/{1}", customerID, likeID);
            string jsonResponse = DoGetWebRequest(url);
            Common.WriteLog("-> GetCustomerLike() put data:", "querystring:" + url );
            Common.WriteLog("<- GetCustomerLike() return data:", jsonResponse);

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
            string url = String.Format("/customers/{0}/likes", customerID);
            string jsonResponse = DoPostWebRequest(url, postData, ref statusCode);
            Common.WriteLog("-> AddCustomerLike() post data:", "querystring:" + url + " data:" +postData);
            Common.WriteLog("<- AddCustomerLike() return data:", jsonResponse);

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
            string url = String.Format("/customers/{0}/likes/{1}", customerID, like.id);
            string jsonResponse = DoPutWebRequest(url, postData, ref statusCode);
            Common.WriteLog("-> UpdateCustomerLike() put data:", "querystring:" + url + " data:" + postData);
            Common.WriteLog("<- UpdateCustomerLike() return data:", jsonResponse);

            Likes returnJobs = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Likes>(jsonResponse));
            return returnJobs;
        }
        #endregion
    }
}
