using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using System;

namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class Node
    {
        #region customers educations
        /// <summary>
        /// Get education detail
        /// </summary>
        public Educations GetCustomerEducation(string customerID, string eduID, ref Error error)
        {
            Educations returnEdu = null;
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string url = String.Format("/customers/{0}/educations/{1}", customerID, eduID);
            string jsonResponse = DoGetWebRequest(url);
            Common.WriteLog("-> GetCustomerEducation() get data:", "querystring:" + url);
            Common.WriteLog("<- GetCustomerEducation() return data:", jsonResponse);
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                returnEdu = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Educations>(jsonResponse));
            }
            else
            {
                returnEdu = null;
            }
            return returnEdu;

        }
        /// <summary>
        /// Add customers Education
        /// </summary>
        public Educations AddCustomerEducation(string customerID, Educations edu,ref Error error)
        {
            Educations returnEdu = null;
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(edu, settings);
            string statusCode = null;
            string url = String.Format("/customers/{0}/educations", customerID);
            string jsonResponse = DoPostWebRequest(url, postData, ref statusCode);
            Common.WriteLog("-> AddCustomerEducation() put data:", "querystring:" + url + " data:" + postData);
            Common.WriteLog("<- AddCustomerEducation() return data:", jsonResponse);
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                 returnEdu = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Educations>(jsonResponse));
            }
            else
            {
                returnEdu = null;
            }
            return returnEdu;
        }
        /// <summary>
        /// Update customers Education
        /// </summary>
        public Educations UpdateCustomerEducation(string customerID, Educations education,ref Error error)
        {
            Educations returnSubscription = null;
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(education, settings);
            string statusCode = null;
            string url = String.Format("/customers/{0}/educations/{1}", customerID, education.id);
            string jsonResponse = DoPutWebRequest(url, postData, ref statusCode);
            Common.WriteLog("-> UpdateCustomerEducation() put data:", "querystring:" + url + " data:" + postData);
            Common.WriteLog("<- UpdateCustomerEducation() return data:", jsonResponse);
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                 returnSubscription = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Educations>(jsonResponse));
            }
            else
            {
                returnSubscription = null;
            }
            return returnSubscription;
        }

        /// <summary>
        /// Delete education from customer
        /// </summary>
        public bool DeleteCustomerEducation(string customerID, string eduID, ref Error error)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string url = String.Format("/customers/{0}/educations/{1}", customerID, eduID);
            string jsonResponse = DoDeleteWebRequest(url);
            Common.WriteLog("-> DeleteCustomerEducation() delete data:", "querystring:" + url);
            Common.WriteLog("<- DeleteCustomerEducation() return data:", jsonResponse);

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
