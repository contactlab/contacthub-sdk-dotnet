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
        public static List<BasePropertiesItem2> deserializeProperties(dynamic list)
        {
            if (list == null) return null;

            List<BasePropertiesItem2> returnValue = new List<BasePropertiesItem2>();
            foreach (var property in list)
            {
                BasePropertiesItem2 prop = JsonConvert.DeserializeObject<BasePropertiesItem2>(property.Value.ToString());
                prop.name = property.Name;
                returnValue.Add(prop);
            }
            return returnValue;
        }

        /* deserializza la proprietà items che contiene la definizione degli array */
        public static BasePropertiesItem2 deserializeItems(dynamic list)
        {
            if (list == null) return null;

            BasePropertiesItem2 returnValue = JsonConvert.DeserializeObject<BasePropertiesItem2>(list.ToString());
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

    public class BasePropertiesItem2
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
        private List<BasePropertiesItem2> _properties;

        private BasePropertiesItem2 _items;

    }


}
