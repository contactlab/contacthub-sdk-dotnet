using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;

namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class CHubNode
    {
        #region Events

        public PostEvent AddEvent(PostEvent event_)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            string postData = JsonConvert.SerializeObject(event_, settings);
            string jsonResponse = DoPostWebRequest("/events", postData);
            PostEvent returnCustomer = (jsonResponse == null ? null : JsonConvert.DeserializeObject<PostEvent>(jsonResponse));
            return returnCustomer;
        }

        #endregion
    }
}
