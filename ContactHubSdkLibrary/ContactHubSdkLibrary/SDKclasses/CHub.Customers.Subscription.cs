﻿using Newtonsoft.Json;
using System;


namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class Node
    {
        #region customers subscriptions
        // get subscription detail
        public Subscriptions GetCustomerSubscription(string customerID, string subscriptionID)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string url = String.Format("/customers/{0}/subscriptions/{1}", customerID, subscriptionID);
            string jsonResponse = DoGetWebRequest(url);
            Common.WriteLog("-> GetCustomerSubscription() post data:", "querystring:" + url );
            Common.WriteLog("<- GetCustomerSubscription() return data:", jsonResponse);

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
            string url = String.Format("/customers/{0}/subscriptions", customerID);
            string jsonResponse = DoPostWebRequest(url, postData,ref statusCode);
            Common.WriteLog("-> AddCustomerSubscription() post data:", "querystring:" + url + " data:" + postData);
            Common.WriteLog("<- AddCustomerSubscription() return data:", jsonResponse);

            Subscriptions returnLike = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Subscriptions>(jsonResponse));
            return returnLike;
        }

        /// <summary>
        /// Update customers subscription
        /// </summary>
        public Subscriptions UpdateCustomerSubscription(string customerID, Subscriptions subscription)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(subscription, settings);
            string statusCode = null;
            string url = String.Format("/customers/{0}/subscriptions/{1}", customerID, subscription.id);
            string jsonResponse = DoPutWebRequest(url, postData, ref statusCode);
            Common.WriteLog("-> UpdateCustomerSubscription() put data:", "querystring:" + url + " data:" + postData);
            Common.WriteLog("<- UpdateCustomerSubscription() return data:", jsonResponse);

            Subscriptions returnSubscription = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Subscriptions>(jsonResponse));
            return returnSubscription;
        }
        #endregion
    }
}
