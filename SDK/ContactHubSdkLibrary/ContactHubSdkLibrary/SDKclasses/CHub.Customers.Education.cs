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
        public Educations GetCustomerEducation(string customerID, string eduID)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string jsonResponse = DoGetWebRequest(String.Format("/customers/{0}/educations/{1}", customerID, eduID));
            Educations returnEdu = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Educations>(jsonResponse));
            return returnEdu;
        }
        /// <summary>
        /// Add customers Education
        /// </summary>
        public Educations AddCustomerEducation(string customerID, Educations edu)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(edu, settings);
            string statusCode = null;
            string jsonResponse = DoPostWebRequest(String.Format("/customers/{0}/educations", customerID), postData, ref statusCode);
            Educations returnEdu = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Educations>(jsonResponse));
            return returnEdu;
        }
        /// <summary>
        /// Update customers Education
        /// </summary>
        public Educations UpdateCustomerEducation(string customerID, Educations education)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(education, settings);
            string statusCode = null;
            string jsonResponse = DoPutWebRequest(String.Format("/customers/{0}/educations/{1}", customerID, education.id), postData, ref statusCode);
            Educations returnSubscription = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Educations>(jsonResponse));
            return returnSubscription;
        }
        #endregion
    }
}
