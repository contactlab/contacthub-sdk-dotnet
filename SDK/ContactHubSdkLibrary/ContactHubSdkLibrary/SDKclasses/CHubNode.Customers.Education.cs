using Newtonsoft.Json;
using System;

namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class CHubNode
    {
        #region customers educations
        // get education detail
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
        //add education to customer
        public Educations AddCustomerEducation(string customerID, Educations edu)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(edu, settings);
            string jsonResponse = DoPostWebRequest(String.Format("/customers/{0}/educations", customerID), postData);
            Educations returnEdu = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Educations>(jsonResponse));
            return returnEdu;
        }
        #endregion
    }
}
