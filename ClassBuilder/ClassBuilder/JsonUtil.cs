using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generateBasePropertiesClass
{
    public static class JSONUtilities
    {
        /// <summary>
        /// replace external "$ref" with a normal "ref" attribute 
        /// </summary>
        public static string FixReference(string json)
        {
            return json.Replace("$ref", "ref");
        }


        public static void SaveJsonSchema(string name, string json)
        {
            File.WriteAllText( "JsonSchema\\" + name,json);
        }

        public static List<String> GetEnumList(string jsonString)
        {
            List<String> list = new List<String>();
            JObject definitions = JObject.Parse(jsonString);
            foreach (var c in definitions["definitions"])
            {
                if (c.Path == "definitions.EventType")
                {
                    var listType = c.First["enum"];
                    foreach (string eType in listType)
                    {
                        list.Add(eType);
                    }
                }
            }
            return list;
        }
    }
}
