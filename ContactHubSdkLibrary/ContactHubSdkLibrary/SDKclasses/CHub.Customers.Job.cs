using ContactHubSdkLibrary.Models;
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
        public Jobs GetCustomerJob(string customerID, string jobID, ref Error error)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string url = String.Format("/customers/{0}/jobs/{1}", customerID, jobID);
            string jsonResponse = DoGetWebRequest(url);
            Common.WriteLog("-> GetCustomerJob() get data:", "querystring:" + url);
            Common.WriteLog("<- GetCustomerJob() return data:", jsonResponse);
            Jobs returnJobs = null;

            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                returnJobs = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Jobs>(jsonResponse));
            }
            else
            {
                returnJobs = null;
            }
            return returnJobs;
        }
        /// <summary>
        /// Add a job object to customer
        /// </summary>
        public Jobs AddCustomerJob(string customerID, Jobs job, ref Error error)
        {
            Jobs returnJobs = null;
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(job, settings);
            string statusCode = null;
            string url = String.Format("/customers/{0}/jobs", customerID);
            string jsonResponse = DoPostWebRequest(url, postData, ref statusCode);
            Common.WriteLog("-> AddCustomerJob() post data:", "querystring:" + url + " data:" + postData);
            Common.WriteLog("<- AddCustomerJob() return data:", jsonResponse);
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                returnJobs = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Jobs>(jsonResponse));
            }
            else
            {
                returnJobs = null;
            }
            return returnJobs;
        }

        /// <summary>
        /// Update customers job
        /// </summary>
        public Jobs UpdateCustomerJob(string customerID, Jobs job, ref Error error)
        {
            Jobs returnJobs = null;
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
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                returnJobs = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Jobs>(jsonResponse));
            }
            else
            {
                returnJobs = null;
            }
            return returnJobs;
        }

        /// <summary>
        /// Delete  job from customer
        /// </summary>
        public bool DeleteCustomerJob(string customerID, string jobID,ref Error error)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string url = String.Format("/customers/{0}/jobs/{1}", customerID, jobID);
            string jsonResponse = DoDeleteWebRequest(url);
            Common.WriteLog("-> DeleteCustomerJob() delete data:", "querystring:" + url );
            Common.WriteLog("<- DeleteCustomerJob() return data:", jsonResponse);

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
