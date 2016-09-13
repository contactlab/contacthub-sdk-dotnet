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
                    //crea il nodo da aggiungere
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
        public Embedded _embedded { get; set; }
        public PageLink _links { get; set; }
        public Page page { get; set; }
    }

    public class SessionList
    {
        public Embedded _embedded { get; set; }
        public PageLink _links { get; set; }
    }

    public class Session
    {
        public string id { get; set; }
        public string value { get; set; }
        public PageLink _links { get; set; }
    }

    /* embedded è usato in contesti diversi, dentro customers e come oggetto da passare a add session */
    public class Embedded
    {
        public List<Customer> customers;
        public List<Session> sources;
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
