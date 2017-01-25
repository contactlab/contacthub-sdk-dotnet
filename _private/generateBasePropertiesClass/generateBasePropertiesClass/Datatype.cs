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

        /* deserialize properties items with array definition  */
        public static BasePropertiesItem deserializeItems(dynamic list)
        {
            if (list == null) return null;
            BasePropertiesItem returnValue = JsonConvert.DeserializeObject<BasePropertiesItem>(list.ToString());
            //if returnValue contains external ref, read external schema and replace it
            if (!string.IsNullOrEmpty (returnValue.reference))
            {
                string externalSchema = Connection.DoGetWebRequest(JSONUtilities.FixReference( returnValue.reference),false);
                BasePropertiesItem c = null;
                c = JsonConvert.DeserializeObject<BasePropertiesItem>(externalSchema);
                returnValue = c;
            }
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

    #region Event Properties

    public class EventPropertiesSchemaRoot
    {
        public List<Event> elements { get; set; }
        //[JsonProperty("_embedded")]
        //        public EventPropertySchema embedded { get; set; }
        public Page page { get; set; }
    }

    public class Page
    {
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int number { get; set; }
    }

    //public class EventPropertySchema
    //{
    //    public List<Event> events { get; set; }
    //}

    public class Event
    {
        public string id { get; set; }
        public string type { get; set; }
        //public string mode { get; set; }
        //public string label { get; set; }
        public string description { get; set; }
        //public bool enabled { get; set; }
        public object propertiesSchema { get; set; }
    }
    #endregion

    #region Event Context

    public class EventContextPropertiesSchemaRoot
    {
        public List<Context> elements { get; set; }
        //[JsonProperty("_embedded")]
        //public EventContextPropertySchema embedded { get; set; }
        //  public Page page { get; set; }
    }

    //public class EventContextPropertySchema
    //{
    //    public List<Context> contexts { get; set; }
    //}

    public class Context
    {
        public string id { get; set; }
        public string type { get; set; }
        public string mode { get; set; }
        public string label { get; set; }
        public string description { get; set; }
        public bool enabled { get; set; }
        public object propertiesSchema { get; set; }
    }
    #endregion

    public class BasePropertiesItem
    {
        public string name { get; set; }
        public string description { get; set; }
        public string format { get; set; }
        public string pattern { get; set; }
        public dynamic type { get; set; }
        public string[] @enum { get; set; }
        [JsonProperty("ref")]
        public string reference {get;set;}


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


    public class Enums
    {
        public Dictionary<string,string> definitions { get; set; }
    }

    //public class Definition
    //{
    //    public string description { get; set; }
    //    public string type { get; set; }
    //    //[JsonProperty("enum")]
    //    //public List<String> enumValues { get;set; }
    //}
}
