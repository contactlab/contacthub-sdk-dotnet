using Newtonsoft.Json;
using System;


namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class Node
    {
        #region customers job
        /// <summary>
        /// Get the details of customer jobs
        /// </summary>
        public Jobs GetCustomerJob(string customerID, string jobID)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string url = String.Format("/customers/{0}/jobs/{1}", customerID, jobID);
            string jsonResponse = DoGetWebRequest(url);
            Common.WriteLog("-> GetCustomerJob() get data:", "querystring:" + url);
            Common.WriteLog("<- GetCustomerJob() return data:", jsonResponse);

            Jobs returnJobs = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Jobs>(jsonResponse));
            return returnJobs;
        }
        /// <summary>
        /// Add a job object to customer
        /// </summary>
        public Jobs AddCustomerJob(string customerID, Jobs job)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(job, settings);
            string statusCode = null;
            string url = String.Format("/customers/{0}/jobs", customerID);
            string jsonResponse = DoPostWebRequest(url, postData, ref statusCode);
            Common.WriteLog("-> AddCustomerJob() post data:", "querystring:" + url + " data:" +postData);
            Common.WriteLog("<- AddCustomerJob() return data:", jsonResponse);

            Jobs returnJobs = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Jobs>(jsonResponse));
            return returnJobs;
        }

        /// <summary>
        /// Update customers job
        /// </summary>
        public Jobs UpdateCustomerJob(string customerID, Jobs job)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(job, settings);
            string statusCode = null;
            string url = String.Format("/customers/{0}/jobs/{1}", customerID, job.id);
            string jsonResponse = DoPutWebRequest(url, postData, ref statusCode);
            Common.WriteLog("-> UpdateCustomerJob() put data:", "querystring:" + url + " data:" + postData);
            Common.WriteLog("<- UpdateCustomerJob() return data:", jsonResponse);

            Jobs returnJobs = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Jobs>(jsonResponse));
            return returnJobs;
        }

        /// <summary>
        /// Delete  job from customer
        /// </summary>
        public Jobs RemoveCustomerJob(string customerID, string jobID)
        {
            /* DA FINIRE IMPLEMENTAZIONE, MANCANO I METODI DELETE IN HUB*/

            //var settings = new JsonSerializerSettings()
            //{
            //    NullValueHandling = NullValueHandling.Ignore
            //};
            //string postData = JsonConvert.SerializeObject(job, settings);
            //string statusCode = null;
            //string jsonResponse = DoPostWebRequest(String.Format("/customers/{0}/jobs", customerID), postData, ref statusCode);
            //Common.WriteLog("-> AddEvent() get data:", "querystring:" + url + " data:" + postData);
            //Common.WriteLog("<- AddEvent() return data:", jsonResponse);

            Jobs returnJobs = null; //(jsonResponse == null ? null : JsonConvert.DeserializeObject<Jobs>(jsonResponse));
            return returnJobs;
        }
        #endregion
    }
}
