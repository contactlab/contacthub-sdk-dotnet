using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ContactHubSdkLibrary.Models
{
    #region definition
    public class Customer : PostCustomer
    {
        public string id { get; set; }
        [JsonProperty("registeredAt")]

        public string _registeredAt { get; set; }
        [JsonIgnore]
        [JsonProperty("_registeredAt")]

        public DateTime registeredAt
        {
            get
            {
                var currentValue = _updatedAt;
                if (currentValue == null)
                {
                    return DateTime.MinValue;
                }
                else
                {
                    return Convert.ToDateTime(_registeredAt);
                }
            }
            set { }
        }
        [JsonProperty("updatedAt")]
        public string _updatedAt { get; set; }
        [JsonIgnore]
        [JsonProperty("_updatedAt")]

        public DateTime updatedAt
        {
            get
            {
                var currentValue = _updatedAt;
                if (currentValue == null)
                {
                    return DateTime.MinValue;
                }
                else
                {
                    return Convert.ToDateTime(_updatedAt);
                }
            }
            set { }
        }
        public Links _links { get; set; }
    }
    public class Link
    {
        public string href { get; set; }
    }
    public class Links
    {
        public Link customers { get; set; }
        public Link events { get; set; }
        public Link sources { get; set; }
        public Link sessions { get; set; }
        public Link self { get; set; }
    }
    public class Tags
    {
        public List<String> auto { get; set; }
        public List<String> manual { get; set; }
    }

    public class PostCustomer
    {
        public string externalId { get; set; }
        public string nodeId { get; set; }
        public BaseProperties @base { get; set; }
        [JsonIgnore]
        [JsonProperty("_extended")]
        public List<ExtendedProperty> extended
        {
            get
            {
                if (_extended != null)
                {
                    try
                    {
                        JObject jObj = JObject.FromObject(_extended);
                        List<ExtendedProperty> list = ExtendedPropertiesUtil.DeserializeExtendedProperties(jObj);
                        return list;
                    }
                    catch { return null; }
                }
                else
                {
                    return null;
                }
            }
            set
            {
                string extendedPropertiesData = ExtendedPropertiesUtil.SerializeExtendedProperties(value, "extended", typeof(Customer));
                if (!string.IsNullOrEmpty(extendedPropertiesData))
                {
                    JObject extendedProperties = JObject.Parse(extendedPropertiesData);
                    //create the node to be added
                    JToken jValue = null;
                    extendedProperties.TryGetValue("extended", out jValue);
                    _extended = jValue;
                }
                else
                {
                    _extended = null;
                }
            }
        }
        [JsonProperty("extended")]
        public object _extended { get; set; }
        public string extra { get; set; }
        public Tags tags { get; set; }
        public bool? enabled { get; set; }
    }
    public class PagedCustomer
    {
        public EmbeddedCustomers _embedded { get; set; }
        public PageLink _links { get; set; }
        public Page page { get; set; }
    }

    public class Session
    {
        public Session()
        {
            value = Guid.NewGuid().ToString();
        }

        public void ResetID()
        {
            value = Guid.NewGuid().ToString();
        }

        public string id { get; set; }
        public string value { get; set; }
        public SessionPageLink _links { get; set; }
    }

    public class EmbeddedCustomers
    {
        public List<Customer> customers;
    }
    public class EmbeddedSessions
    {
        public List<Session> sources;
    }

    public class EmbeddedEvents
    {
        public List<Event> events;
    }

    public class _Event : Event
    {

    }

    public class Event : PostEvent
    {
        public EventPageLink _links { get; set; }
        public string id { get; set; }
    }

    public class EventPageLink
    {
        public Link customer { get; set; }
        public Link events { get; set; }
        public Link self { get; set; }
    }

    public class SessionPageLink
    {
        public Link customer { get; set; }
        public Link sessions { get; set; }
        public Link self { get; set; }
    }
    public class PageLink
    {
        public Link first { get; set; }
        public Link last { get; set; }
        public Link next { get; set; }
        public Link prev { get; set; }
        public Link self { get; set; }
    }

    public class Page
    {
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int number { get; set; }
    }
    #endregion

}
