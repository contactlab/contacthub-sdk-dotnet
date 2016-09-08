using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHubSdkLibrary.Models
{

    public class ExtendedProperty
    {
        public string name { get; set; }
    }

    //String
    public class ExtendedPropertyString : ExtendedProperty
    {
        public string value { get; set; }
    }

    //List of Strings
    public class ExtendedPropertyStringArray : ExtendedProperty
    {
        public List<string> value { get; set; }
    }

    //Number
    public class ExtendedPropertyNumber : ExtendedProperty
    {
        public decimal value { get; set; }
    }

    //array of number
    public class ExtendedPropertyNumberArray : ExtendedProperty
    {
        public List<Double> value { get; set; }
    }

    //Boolean
    public class ExtendedPropertyBoolean : ExtendedProperty
    {
        public Boolean value { get; set; }
    }


    //Object
    public class ExtendedPropertyObject : ExtendedProperty
    {
        /* la definizione di propertyobject appare identica a quella di propertyobjectarray, di fatto è un insieme di property, cambia solo la renderizzazione json */
        public List<ExtendedProperty> value { get; set; }
    }

    //array of objects
    public class ExtendedPropertyObjectArray : ExtendedProperty
    {
        public List<ExtendedProperty> value { get; set; }
    }

    //Date
    public class ExtendedPropertyDate : ExtendedProperty
    {
        public DateTime value { get; set; }
    }

    //DateTime
    public class ExtendedPropertyDateTime : ExtendedProperty
    {
        public DateTime value { get; set; }
    }

    //Array of date
    public class ExtendedPropertyDateArray : ExtendedProperty
    {
        public List<DateTime> value { get; set; }
    }

  

    #region serialization util
    public static class ExtendedPropertiesUtil
    {
        public static string SerializeExtendedProperties(List<ExtendedProperty> extendendProperties, string propertyName,Type parentType, bool first = true)
        {
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

                    case "ExtendedPropertyNumber":
                        {
                            returnValue += String.Format("\"{0}\":{1}", ex.name, ((ExtendedPropertyNumber)ex).value, new CultureInfo("en-US"));
                        }
                        break;
                    case "ExtendedPropertyNumberArray":
                        {                            returnValue += String.Format("\"{0}\":", ex.name);
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
                            returnValue += SerializeExtendedProperties(((ExtendedPropertyObject)ex).value, ex.name,ex.GetType(), false);
                        }
                        break;
                    case "ExtendedPropertyObjectArray": //è un set di property contenute in un array
                        {
                            returnValue += SerializeExtendedProperties(((ExtendedPropertyObjectArray)ex).value, ex.name, ex.GetType(), false) ;
                        }
                        break;
                    case "ExtendedPropertyDateTime":
                        {
                            returnValue += String.Format("\"{0}\":\"{1}\"", ex.name, ((ExtendedPropertyDateTime)ex).value.ToString("o"));
                        }
                        break;
                    case "ExtendedPropertyDate":
                        {
                            returnValue += String.Format("\"{0}\":\"{1}\"", ex.name, ((ExtendedPropertyDate)ex).value.ToString("o"));
                        }
                        break;
                    case "ExtendedPropertyDateArray":
                        {
                            returnValue += String.Format("\"{0}\":", ex.name);
                            returnValue += "[";
                            foreach (DateTime d in ((ExtendedPropertyDateArray)ex).value)
                            {
                                returnValue += String.Format("\"{0}\"", d.ToString("o"));
                                returnValue += ",";
                            }
                            Common.CleanComma(ref returnValue);
                            returnValue += "]";
                        }
                        break;

                    //                        DateTime.UtcNow.ToString("o"))
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
