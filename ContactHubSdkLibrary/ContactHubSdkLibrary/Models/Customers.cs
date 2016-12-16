using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

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
                    return Convert.ToDateTime(_registeredAt).ToUniversalTime();
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
                    return Convert.ToDateTime(_updatedAt).ToUniversalTime();
                }
            }
            set { }
        }
      //  public Links _links { get; set; }

        /// <summary>
        /// Return PostCustomer subclass from Customer
        /// </summary>
        /// <returns></returns>
        public PostCustomer ToPostCustomer()
        {
            return this.CreateObject<PostCustomer>();
        }

        public virtual T CreateObject<T>()
        {
            return Common.CreateObject<T>(this);

        }
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
        //        public EmbeddedCustomers _embedded { get; set; }
        public List<Customer> elements;
        // public PageLink _links { get; set; }
        public Page page { get; set; }
        [JsonIgnore]
        public  PagedCustomerFilter filter {get;set;}  //query string for relative paging
    }

    public class PagedCustomerFilter
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public string externalId { get; set; }
        public string query { get; set; }
        public string fields { get; set; }
    }

    public class Error
    {
        public string logref { get; set; }
        public string message { get; set; }
        public string error { get; set; }
        public string status { get; set; }
      //  public CustomerPageLink _links { get; set; }
        public CustomerDataError data { get; set; }
    }

    public class CustomerDataError
    {
        public CustomerDataErrorDetail customer { get; set; }
    }

    public class CustomerDataErrorDetail
    {
        public string id { get; set; }
        public string href { get; set; }
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
      //  public SessionPageLink _links { get; set; }
    }

    //public class EmbeddedCustomers
    //{
    //    public List<Customer> customers;
    //}
    //public class EmbeddedSessions
    //{
    //    public List<Session> sources;
    //}

    //public class EmbeddedEvents
    //{
    //    public List<Event> events;
    //}

    public class _Event : Event
    {

    }

    public class Event : PostEvent
    {
      //  public EventPageLink _links { get; set; }
        public string id { get; set; }

        /// <summary>
        /// Return PostCustomer subclass from Customer
        /// </summary>
        /// <returns></returns>
        public PostEvent ToPostEvent()
        {
            return this.CreateObject<PostEvent>();
        }

        public virtual T CreateObject<T>()
        {
            if (typeof(T).IsSubclassOf(this.GetType()))
            {
                throw new InvalidCastException(this.GetType().ToString() + " does not inherit from " + typeof(T).ToString());
            }

            T ret = System.Activator.CreateInstance<T>();

            PropertyInfo[] propTo = ret.GetType().GetProperties();
            PropertyInfo[] propFrom = this.GetType().GetProperties();

            // for each property check whether this data item has an equivalent property
            // and copy over the property values as neccesary.
            foreach (PropertyInfo propT in propTo)
            {
                foreach (PropertyInfo propF in propFrom)
                {
                    if (propT.Name == propF.Name)
                    {
                        propF.SetValue(ret, propF.GetValue(this));
                        break;
                    }
                }
            }

            return ret;

        }
    }

    public class CustomerPageLink
    {
        public Link customer { get; set; }
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
