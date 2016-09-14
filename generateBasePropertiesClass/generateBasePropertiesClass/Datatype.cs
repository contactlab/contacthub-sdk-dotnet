using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace generateBasePropertiesClass
{
    public static class JsonUtil
    {
        public static string fixName(string label)
        {
            if (label == null) return null;

            return label.Replace("+", String.Empty);
        }
        public static List<BasePropertiesItem> deserializeProperties(dynamic list)
        {
            if (list == null) return null;

            List<BasePropertiesItem> returnValue = new List<BasePropertiesItem>();
            foreach (var property in list)
            {
                BasePropertiesItem prop = JsonConvert.DeserializeObject<BasePropertiesItem>(property.Value.ToString());
                prop.name = property.Name;
                returnValue.Add(prop);
            }
            return returnValue;
        }

        /* deserializza la proprietà items che contiene la definizione degli array */
        public static BasePropertiesItem deserializeItems(dynamic list)
        {
            if (list == null) return null;

            BasePropertiesItem returnValue = JsonConvert.DeserializeObject<BasePropertiesItem>(list.ToString());
            return returnValue;
        }

        public static ContactLabProperty deserializeCLabProperties(dynamic prop)
        {
            if (prop == null) return null;

            ContactLabProperty returnValue = JsonConvert.DeserializeObject<ContactLabProperty>(prop.ToString());
            //   prop.name = property.Name;
            return returnValue;
        }
    }

    public class ContactLabProperty
    {
        public string label;
        public bool enabled;
    }


    public class EventPropertiesSchemaRoot
    {
        [JsonProperty("_embedded")]
        public EventPropertySchema embedded { get; set; }
    }

    public class EventPropertySchema
    {
        public List<Event> events { get; set; }
    }

    public class Event
    {
        public string id { get; set; }
        public string type { get; set; }
        public string mode { get; set; }
        public string label { get; set; }
        public string description { get; set; }
        public bool enabled { get; set; }
        public object propertiesSchema { get; set; }
      
    }

    public class BasePropertiesItem
    {
        public string name { get; set; }
        public string description { get; set; }
        public string format { get; set; }
        public string pattern { get; set; }
        public string type { get; set; }
        public string[] @enum { get; set; }

        public dynamic items
        {
            get
            {
                return this._items;
            }
            set
            {
                _items = JsonUtil.deserializeItems(value);
            }
        }

        public dynamic contactlabProperties
        {
            get
            {
                return this._contactlabProperties;
            }
            set
            {
                _contactlabProperties = JsonUtil.deserializeCLabProperties(value);
            }
        }
        private ContactLabProperty _contactlabProperties;

        public dynamic properties
        {
            get
            {
                return this._properties;
            }
            set
            {
                _properties = JsonUtil.deserializeProperties(value);
            }
        }
        private List<BasePropertiesItem> _properties;

        private BasePropertiesItem _items;

    }


}
