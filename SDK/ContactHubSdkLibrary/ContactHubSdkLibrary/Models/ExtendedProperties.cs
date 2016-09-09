using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;

/* Le extended property sono dinamiche lato contactHub. Lato client sdk possono essere rappresentate come un tree di oggetti tipizzati che ereditano
 * da ExtendedProperty e che specializzano il "value".
 * La serializzazione e deserializzazione di questi oggetti è completamente custom e passa attraverso due attributi nella classe Customer
 * - _extended: di tipo object, è rappresentazione come oggetto del json nel formato contanhub. Tale attributo è quello serializzato/deserializzato
 * - extended : di tipo List<ExtendedProperty>, rappresenta le extendend nel formato client SDK, cioè come tree di ExendedProperty. Questo attributo serve solo in lettura/scrittura
 *              lato sdk perchè poi la sua serializzazione avviene in modo trasparente tramite l'attributo _extended che non deve mai essere letto/scritto lato sdk. Per questo l'attributo
 *              extended è marcato come "ignore" lato serializzazione
 */

namespace ContactHubSdkLibrary.Models
{
    /* classe base ereditata da tutte le specializzazioni */

    public class ExtendedProperty
    {
        public string name { get; set; }
    }

    //tipizzazione String
    public class ExtendedPropertyString : ExtendedProperty
    {
        public string value { get; set; }
    }

    //tipizzazione List of Strings
    public class ExtendedPropertyStringArray : ExtendedProperty
    {
        public List<string> value { get; set; }
    }

    //tipizzazione Number
    public class ExtendedPropertyNumber : ExtendedProperty
    {
        public Double value { get; set; }
    }

    //tipizzazione Array of number
    public class ExtendedPropertyNumberArray : ExtendedProperty
    {
        public List<Double> value { get; set; }
    }

    //tipizzazione Boolean
    public class ExtendedPropertyBoolean : ExtendedProperty
    {
        public Boolean value { get; set; }
    }

    //tipizzazione Object
    public class ExtendedPropertyObject : ExtendedProperty
    {
        /* la definizione di propertyobject appare identica a quella di propertyobjectarray, di fatto è un insieme di property, cambia solo la renderizzazione json */
        public List<ExtendedProperty> value { get; set; }
    }

    //tipizzazione Array of objects
    public class ExtendedPropertyObjectArray : ExtendedProperty
    {
        public List<ExtendedProperty> value { get; set; }
    }

    //tipizzazione DateTime
    public class ExtendedPropertyDateTime : ExtendedProperty
    {
        public DateTime value { get; set; }
    }

    //tipizzazione Array of date
    public class ExtendedPropertyDateTimeArray : ExtendedProperty
    {
        public List<DateTime> value { get; set; }
    }

    #region serialization util

    /* funzioni statiche per serializzazione e deserializzazione delle extended properties */
    public static class ExtendedPropertiesUtil
    {
        /* deserializzazione  da oggetto jtoken (singolo nodo) a List<ExtendedProperty> tipizzata */
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

        /* deserializzazione  da oggetto jobject a List<ExtendedProperty> tipizzata */
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

        /* conversione da nodo Jtoken a ExtendedProperty tipizzata */

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
                    //determina il tipo di array da creare in funzione della tipologia del primo elemento presente
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

        //detect dell'array type
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

        /* serializzazione custom da List<ExtendeProperty> a string json */

        public static string SerializeExtendedProperties(List<ExtendedProperty> extendendProperties, string propertyName, Type parentType, bool first = true)
        {
            if (extendendProperties == null) return null;

            string returnValue = null;
            //verifica se è un array
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
                //se è un array divide ogni elemento con una {}
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
                    case "ExtendedPropertyObject": //è un set di property contenute in un oggetto, non è un array
                        {
                            returnValue += SerializeExtendedProperties(((ExtendedPropertyObject)ex).value, ex.name, ex.GetType(), false);
                        }
                        break;
                    case "ExtendedPropertyObjectArray": //è un set di property contenute in un array
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
                //se è un array divide ogni elemento con una {}
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
