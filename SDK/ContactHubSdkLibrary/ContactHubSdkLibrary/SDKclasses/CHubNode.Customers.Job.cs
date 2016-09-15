using Newtonsoft.Json;
using System;


namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class CHubNode
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
            string jsonResponse = DoGetWebRequest(String.Format("/customers/{0}/jobs/{1}", customerID, jobID));
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
            string jsonResponse = DoPostWebRequest(String.Format("/customers/{0}/jobs", customerID), postData,ref statusCode);
            Jobs returnJobs = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Jobs>(jsonResponse));
            return returnJobs;
        }
        #endregion
    }
}
