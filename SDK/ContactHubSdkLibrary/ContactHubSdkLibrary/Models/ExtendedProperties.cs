using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;

/// <summary>
/// The extended property are dynamic on contactHub database.
/// Properties on sdk client side can be represented as a tree of typed objects that inherit from ExtendedProperty and specialize "value" attribute
/// The serialization and de-serialization of these objects is completely custom and passes through two attributes in the Customer class
/// - _extended: type is object, is the object representation as contanhub json format. This  is really attribute  serialized and deserialized
/// - Extended: type is List<ExtendedProperty>: represents the extendend properties in client SDK format, that is as the tree ExendedProperty.This attribute is only read / write
///   sdk side because then his serialization is performed transparently by the _extended attribute that should never be read / written from side sdk. This attribute
///   is marked as "ignore" on serializer
/// </summary>
namespace ContactHubSdkLibrary.Models
{
    /// <summary>
    /// Base class inherited by all specializations 
    /// </summary>
    public class ExtendedProperty
    {
        public string name { get; set; }
    }

    /// <summary>
    /// Type String
    /// </summary>
    public class ExtendedPropertyString : ExtendedProperty
    {
        public string value { get; set; }
    }

    /// <summary>
    /// Type List of Strings
    /// </summary>
    public class ExtendedPropertyStringArray : ExtendedProperty
    {
        public List<string> value { get; set; }
    }

    /// <summary>
    /// Type Number
    /// </summary>
    public class ExtendedPropertyNumber : ExtendedProperty
    {
        public Double value { get; set; }
    }

    /// <summary>
    /// Type Array of Number
    /// </summary>
    public class ExtendedPropertyNumberArray : ExtendedProperty
    {
        public List<Double> value { get; set; }
    }

    /// <summary>
    /// Type Boolean
    /// </summary>
    public class ExtendedPropertyBoolean : ExtendedProperty
    {
        public Boolean value { get; set; }
    }

    /// <summary>
    /// Type Object
    /// </summary>
    public class ExtendedPropertyObject : ExtendedProperty
    {
        //ExtendedPropertyObject definition appears identical to that of ExtendedPropertyObjectArray, in fact it is a set of property, only changes the rendering json
        public List<ExtendedProperty> value { get; set; }
    }

    /// <summary>
    /// Type Array of Objects
    /// </summary>
    public class ExtendedPropertyObjectArray : ExtendedProperty
    {
        public List<ExtendedProperty> value { get; set; }
    }

    /// <summary>
    /// Type DateTime
    /// </summary>
    public class ExtendedPropertyDateTime : ExtendedProperty
    {
        public DateTime value { get; set; }
    }

    /// <summary>
    /// Type Array of Date
    /// </summary>
    public class ExtendedPropertyDateTimeArray : ExtendedProperty
    {
        public List<DateTime> value { get; set; }
    }

    #region serialization util

    /// <summary>
    /// Static functions for serialization and de-serialization of the extended properties
    /// </summary>
    public static class ExtendedPropertiesUtil
    {
        /// <summary>
        /// Deserialization of object jtoken (single node) in List<ExtendedProperty> type
        /// </summary>
        public static List<ExtendedProperty> DeserializeExtendedOject(JToken j)
        {
            List<ExtendedProperty> returnValue = new List<ExtendedProperty>();
            foreach (JToken item in j.Children<JToken>())
            {
                if (item.Type == JTokenType.Property)
                {
                    returnValue.Add(getExtendedProperty(item));
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Deserialization from object to List<ExtendedProperty> type 
        /// </summary>
        public static List<ExtendedProperty> DeserializeExtendedProperties(JObject jsonObj)
        {

            JToken extended = jsonObj.First;

            List<ExtendedProperty> ext = new List<ExtendedProperty>();
            foreach (JToken item in extended.Children<JToken>())
            {
                ext.AddRange(DeserializeExtendedOject(item));
            }

            return ext;
        }

        /// <summary>
        /// Conversion from Jtoken node to ExtendedProperty type 
        /// </summary>
        public static ExtendedProperty getExtendedProperty(JToken j)
        {
            ExtendedProperty returnValue = null;
            JProperty prop = (JProperty)j;
            switch (prop.Value.Type)
            {
                case JTokenType.Integer:
                    returnValue = new ExtendedPropertyNumber()
                    {
                        name = prop.Name,
                        value = (double)prop.Value
                    };
                    break;
                case JTokenType.String:
                    returnValue = new ExtendedPropertyString()
                    {
                        name = prop.Name,
                        value = (string)prop.Value
                    };
                    break;
                case JTokenType.Boolean:
                    returnValue = new ExtendedPropertyBoolean()
                    {
                        name = prop.Name,
                        value = (bool)prop.Value
                    };
                    break;
                case JTokenType.Date:
                    returnValue = new ExtendedPropertyDateTime()
                    {
                        name = prop.Name,
                        value = (DateTime)prop.Value
                    };
                    break;
                case JTokenType.Object:
                    returnValue = new ExtendedPropertyObject();
                    returnValue.name = prop.Name;
                    JObject jObj = (JObject)prop.Value;
                    foreach (JToken child in jObj.Children())
                    {
                        if (((ExtendedPropertyObject)returnValue).value == null) ((ExtendedPropertyObject)returnValue).value = new List<ExtendedProperty>();
                        ((ExtendedPropertyObject)returnValue).value.Add(getExtendedProperty(child));
                    }
                    break;
                case JTokenType.Array:
                    //determines the type of array to be created depending on the type of the first element
                    JTokenType arrayType = getArrayType(prop.Value);
                    switch (arrayType)
                    {
                        case JTokenType.String:
                            returnValue = new ExtendedPropertyStringArray();
                            returnValue.name = prop.Name;
                            ((ExtendedPropertyStringArray)returnValue).value = ((JArray)prop.Value).ToObject<List<String>>();
                            break;
                        case JTokenType.Integer:
                        case JTokenType.Float:
                            returnValue = new ExtendedPropertyNumberArray();
                            returnValue.name = prop.Name;
                            ((ExtendedPropertyNumberArray)returnValue).value = ((JArray)prop.Value).ToObject<List<Double>>();
                            break;
                        case JTokenType.Date:
                            returnValue = new ExtendedPropertyDateTimeArray();
                            returnValue.name = prop.Name;
                            ((ExtendedPropertyDateTimeArray)returnValue).value = ((JArray)prop.Value).ToObject<List<DateTime>>();
                            break;
                        case JTokenType.Object:
                            returnValue = new ExtendedPropertyObjectArray();
                            returnValue.name = prop.Name;

                            JArray jObjx = (JArray)prop.Value;
                            foreach (JToken child in jObjx)
                            {
                                JToken item = child.First;
                                if (((ExtendedPropertyObjectArray)returnValue).value == null) ((ExtendedPropertyObjectArray)returnValue).value = new List<ExtendedProperty>();
                                ((ExtendedPropertyObjectArray)returnValue).value.Add(getExtendedProperty((JToken)item));
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return returnValue;
        }

        /// <summary>
        /// Detect array type
        /// </summary>
        private static JTokenType getArrayType(JToken prop)
        {
            JArray array = (JArray)prop;
            JTokenType returnValue = JTokenType.None;
            try
            {
                returnValue = prop.First.Type;
            }
            catch { }
            return returnValue;
        }

        /// <summary>
        /// Custom serialization from List<ExtendedProperty> to json string
        /// </summary>
        public static string SerializeExtendedProperties(List<ExtendedProperty> extendendProperties, string propertyName, Type parentType, bool first = true)
        {
            if (extendendProperties == null) return null;

            string returnValue = null;
            //verify if it is an array
            bool isArray = parentType.Name.ToLowerInvariant().EndsWith("array");
            if (!isArray)
            {
                returnValue = (first ? "{" : string.Empty) + "\"" + propertyName + "\":{";
            }
            else
            {
                returnValue = (first ? "{" : string.Empty) + "\"" + propertyName + "\":[";
            }
            foreach (ExtendedProperty ex in extendendProperties)
            {
                //if it is an array divides each element with a {}
                if (isArray) returnValue += "{";
                switch (ex.GetType().Name)
                {
                    //String
                    case "ExtendedPropertyString":
                        {
                            returnValue += String.Format("\"{0}\":\"{1}\"", ex.name, ((ExtendedPropertyString)ex).value);
                        }
                        break;
                    case "ExtendedPropertyStringArray":
                        {
                            returnValue += String.Format("\"{0}\":", ex.name);
                            returnValue += "[";
                            foreach (string s in ((ExtendedPropertyStringArray)ex).value)
                            {
                                returnValue += String.Format("\"{0}\"", s);
                                returnValue += ",";
                            }
                            Common.CleanComma(ref returnValue);
                            returnValue += "]";
                        }
                        break;
                    //Number
                    case "ExtendedPropertyNumber":
                        {
                            returnValue += String.Format("\"{0}\":{1}", ex.name, ((ExtendedPropertyNumber)ex).value, new CultureInfo("en-US"));
                        }
                        break;
                    case "ExtendedPropertyNumberArray":
                        {
                            returnValue += String.Format("\"{0}\":", ex.name);
                            returnValue += "[";
                            foreach (Double s in ((ExtendedPropertyNumberArray)ex).value)
                            {
                                returnValue += String.Format("{0}", s);
                                returnValue += ",";
                            }
                            Common.CleanComma(ref returnValue);
                            returnValue += "]";
                        }
                        break;

                    case "ExtendedPropertyBoolean":
                        {
                            returnValue += String.Format("\"{0}\":{1}", ex.name, ((ExtendedPropertyBoolean)ex).value.ToString().ToLowerInvariant(), new CultureInfo("en-US"));
                        }
                        break;
                    case "ExtendedPropertyObject": //is a Property Set contained in an object, is not an array
                        {
                            returnValue += SerializeExtendedProperties(((ExtendedPropertyObject)ex).value, ex.name, ex.GetType(), false);
                        }
                        break;
                    case "ExtendedPropertyObjectArray": //is a Property Set contained in an array
                        {
                            returnValue += SerializeExtendedProperties(((ExtendedPropertyObjectArray)ex).value, ex.name, ex.GetType(), false);
                        }
                        break;

                    case "ExtendedPropertyDateTime":
                        {
                            returnValue += String.Format("\"{0}\":\"{1}\"", ex.name, ((ExtendedPropertyDateTime)ex).value.ToString("o"));
                        }
                        break;
                    case "ExtendedPropertyDateTimeArray":
                        {
                            returnValue += String.Format("\"{0}\":", ex.name);
                            returnValue += "[";
                            foreach (DateTime d in ((ExtendedPropertyDateTimeArray)ex).value)
                            {
                                returnValue += String.Format("\"{0}\"", d.ToString("o"));
                                returnValue += ",";
                            }
                            Common.CleanComma(ref returnValue);
                            returnValue += "]";
                        }
                        break;
                    default:
                        {
                            return "Error: SerializeExtendedProperties() unknown datatype";
                        }
                        break;

                }
                //if it is an array divides each element with a {}
                if (isArray) returnValue += "}";

                returnValue += ",";
            }
            Common.CleanComma(ref returnValue);
            if (!parentType.Name.ToLowerInvariant().EndsWith("array"))
            {
                returnValue += "}" + (first ? "}" : string.Empty);
            }
            else
            {
                returnValue += "]" + (first ? "}" : string.Empty);
            }
            return returnValue;
        }
    }

    #endregion

}
