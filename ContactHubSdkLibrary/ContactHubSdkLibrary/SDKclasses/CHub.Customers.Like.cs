using ContactHubSdkLibrary.Models;
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
        public Likes GetCustomerLike(string customerID, string likeID, ref Error error)
        {
            Likes returnLike = null;
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string url = String.Format("/customers/{0}/likes/{1}", customerID, likeID);
            string jsonResponse = DoGetWebRequest(url);
            Common.WriteLog("-> GetCustomerLike() put data:", "querystring:" + url);
            Common.WriteLog("<- GetCustomerLike() return data:", jsonResponse);
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                returnLike = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Likes>(jsonResponse));
            }
            else
            {
                returnLike = null;
            }
            return returnLike;
        }
        /// <summary>
        /// Add like to customer
        /// </summary>
        public Likes AddCustomerLike(string customerID, Likes like, ref Error error)
        {
            Likes returnLike = null;
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(like, settings);
            string statusCode = null;
            string url = String.Format("/customers/{0}/likes", customerID);
            string jsonResponse = DoPostWebRequest(url, postData, ref statusCode);
            Common.WriteLog("-> AddCustomerLike() post data:", "querystring:" + url + " data:" + postData);
            Common.WriteLog("<- AddCustomerLike() return data:", jsonResponse);
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                returnLike = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Likes>(jsonResponse));
            }
            else
            {
                returnLike = null;
            }
            return returnLike;
        }
        /// <summary>
        /// Update customers like
        /// </summary>
        public Likes UpdateCustomerLike(string customerID, Likes like, ref Error error)
        {
            Likes returnJobs = null;
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
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                returnJobs = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Likes>(jsonResponse));
            }
            else
            {
                returnJobs = null;
            }
            return returnJobs;
        }

        /// <summary>
        /// Delete like from customer
        /// </summary>
        public bool DeleteCustomerLike(string customerID, string likeID, ref Error error)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string url = String.Format("/customers/{0}/likes/{1}", customerID, likeID);
            string jsonResponse = DoDeleteWebRequest(url);
            Common.WriteLog("-> DeleteCustomerLike() delete data:", "querystring:" + url);
            Common.WriteLog("<- DeleteCustomerLike() return data:", jsonResponse);

            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
