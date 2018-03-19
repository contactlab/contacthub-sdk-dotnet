using Microsoft.CSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace generateBasePropertiesClass
{

    class Program
    {
        static CSharpCodeProvider cs = new CSharpCodeProvider();
        public static List<String> generatedClass = new List<string>();

        //generate classes for properties based on the basic dynamic definition obtained from / models / properties / base
        static void Main(string[] args)
        {
            generateBaseProperties();  //generate the classes corresponding to the properties of the customer base
            generateConsentsProperties();  //generate the classes corresponding to the properties of the customer base
            generateEventProperties(); //generate the classes corresponding to properties of events
            generateEventContext();    //generate the classes corresponding to properties  in the events contextInfo field
            generateEventTrackingProperties(); //generate the classed corresponding to properties in the event's tracking attribute 
            Console.WriteLine("press key...");
            Console.ReadKey();
        }

        static void generateEventContext()
        {
            BasePropertiesItem propertiesTree = null;
            //creates an enum with the types of context. 

            List<String> enumList = new List<string>();
            List<Context> contextList = new List<Context>();

            //read enum with eventType list
            string jsonString = Connection.DoGetWebRequest("/docs/schema/enums.json");
            JSONUtilities.SaveJsonSchema("docs.schema.enums.json", jsonString);
            JObject definitions = JObject.Parse(jsonString);


            foreach (var c in definitions["definitions"])
            {
                if (c.Path == "definitions.EventContext")
                {
                    var listType = c.First["enum"];
                    foreach (string eType in listType)
                    {
                        enumList.Add(eType);
                    }
                }
            }

            Context e = null;

            //request single context schema

            foreach (string c in enumList)
            {


                jsonString = Connection.DoGetWebRequest("/docs/schema/event/context/" + c.ToLowerInvariant().Replace("_", "-") + ".json");  //get specific context schema
                //                jsonString = Connection.DoGetWebRequest("/docs/schema/event/context/" + c.ToLowerInvariant().Replace("_", "-") + ".post.json");  //get specific context schema
                JSONUtilities.SaveJsonSchema("docs.schema.event.context." + c.ToLowerInvariant().Replace("_", "-") + ".json", jsonString);

                if (jsonString != null)
                {
                    JObject eventObj = JObject.Parse(jsonString);

                    e = new Context()
                    {
                        id = c,
                        description = c,
                        //type = eventObj["type"].ToString(),
                        propertiesSchema = eventObj
                    };

                    contextList.Add(e);
                }

            }

            string outputFileStr = "";

            outputFileStr += "/* selfgenerated from version 0.0.0.1 " + DateTime.Now.ToString() + " */\n\n";
            outputFileStr += "using System;\n";
            outputFileStr += "using System.Collections.Generic;\n";
            outputFileStr += "using System.Globalization;\n";
            outputFileStr += "using Newtonsoft.Json;\n";
            outputFileStr += "using System.ComponentModel.DataAnnotations;\n";
            outputFileStr += "using ContactHubSdkLibrary.Events;\n";
            outputFileStr += "using ContactHubSdkLibrary;\n";
            outputFileStr += "using Newtonsoft.Json.Linq;\n";
            outputFileStr += "namespace ContactHubSdkLibrary.Events {\n";

            string contextJson = null;
            foreach (Context ev in contextList)
            {
                //force object when type is null
                if (string.IsNullOrEmpty(ev.type))
                {
                    ev.type = "object";
                }
                outputFileStr += "//context class '" + ev.id + "': " + ev.description;
                if (ev.propertiesSchema != null)
                {
                    contextJson = ev.propertiesSchema.ToString();
                    propertiesTree = JsonConvert.DeserializeObject<BasePropertiesItem>(contextJson);
                }
                else
                {
                    propertiesTree = new BasePropertiesItem();
                }
                propertiesTree.name = "EventContextProperty" + uppercaseFirst(ev.id) + ": EventBaseProperty";

                createClassFile(propertiesTree, ref outputFileStr);
                outputFileStr += "\n";
            }
            outputFileStr += "\n}\n";
            //generate enum with the list of context
            DictionaryEntry dic = new DictionaryEntry();
            dic.Key = "EventContextEnum";
            dic.Value = enumList.ToArray(); //converte a string[]
            createEnumFile(dic, ref outputFileStr);

            //generate casting function for context properties
            outputFileStr += @"
                public static class EventPropertiesContextUtil
                {
                    /// <summary>
                    /// Return events context properties with right cast, event type based
                    /// </summary>

                  public static object GetEventContext(JObject jo, JsonSerializer serializer)
                    {
                        var typeName = jo[""context""].ToString().ToLowerInvariant();
                        switch (typeName)
                        {
                    ";
            foreach (String s in generatedClass)
            {
                if (s.Contains("EventContextProperty"))
                {
                    outputFileStr += @" case """ + s.Replace(": EventBaseProperty", "").Replace("EventContextProperty", "").ToLowerInvariant() + @""": return jo[""contextInfo""].ToObject<" + s.Replace(": EventBaseProperty", "") + " > (serializer);break;\n\n";
                }
            }
            outputFileStr += "}\n";
            outputFileStr += " return null;\n}\n";
            outputFileStr += "\n}\n";
            File.WriteAllText("eventContextClass.cs", outputFileStr);
        }

        static void generateBaseProperties()
        {
            BasePropertiesItem propertiesTree = null;

            /* download dynamically updated based properties */
            // string jsonString = Connection.DoGetWebRequest("/models/properties/base");
            string jsonString = Connection.DoGetWebRequest("/docs/schema/base-property/base-properties.json");
            JSONUtilities.SaveJsonSchema("docs.schema.base-property.base-properties.json", jsonString);


            jsonString = JSONUtilities.FixReference(jsonString);


            if (string.IsNullOrEmpty(jsonString))
            {
                Console.WriteLine("Error: not valid token");
                Console.ReadKey();
            }
            propertiesTree = JsonConvert.DeserializeObject<BasePropertiesItem>(jsonString);
            propertiesTree.name = "BaseProperties";

            if (propertiesTree == null) return;

            //generate file baseProperties.cs 
            string outputFileStr = String.Empty;

            outputFileStr += "/* selfgenerated from version 0.0.0.1 " + DateTime.Now.ToString() + " */\n\n";
            outputFileStr += "using System;\n";
            outputFileStr += "using System.Collections.Generic;\n";
            outputFileStr += "using System.Globalization;\n";
            outputFileStr += "using Newtonsoft.Json;\n";
            outputFileStr += "using System.ComponentModel.DataAnnotations;\n";
            outputFileStr += "namespace ContactHubSdkLibrary {\n";

            outputFileStr += @"
                                public class ValidatePatternAttribute : System.ComponentModel.DisplayNameAttribute
                                {
                                    public ValidatePatternAttribute(string data) : base(data) { }
                                }
            ";

            //generate class
            createClassFile(propertiesTree, ref outputFileStr);

            outputFileStr += "\n}\n";
            File.WriteAllText("basePropertiesClass.cs", outputFileStr);
        }
        static void generateConsentsProperties()
        {
            BasePropertiesItem propertiesTree = null;

            /* download dynamically updated based properties */
            string jsonString = Connection.DoGetWebRequest("/docs/schema/consents/consents.json");
            JSONUtilities.SaveJsonSchema("docs.schema.consents.consents.json", jsonString);

            jsonString = JSONUtilities.FixReference(jsonString);

            if (string.IsNullOrEmpty(jsonString))
            {
                Console.WriteLine("Error: not valid token");
                Console.ReadKey();
            }
            propertiesTree = JsonConvert.DeserializeObject<BasePropertiesItem>(jsonString);
            propertiesTree.name = "ConsentsProperties";

            if (propertiesTree == null) return;

            //generate file baseProperties.cs 
            string outputFileStr = String.Empty;

            outputFileStr += "/* selfgenerated from version 0.0.0.1 " + DateTime.Now.ToString() + " */\n\n";
            outputFileStr += "using System;\n";
            outputFileStr += "using System.Collections.Generic;\n";
            outputFileStr += "using System.Globalization;\n";
            outputFileStr += "using Newtonsoft.Json;\n";
            outputFileStr += "using System.ComponentModel.DataAnnotations;\n";
            outputFileStr += "namespace ContactHubSdkLibrary {\n";

            //removed: 
            //outputFileStr += @"
            //                    public class ValidatePatternAttribute : System.ComponentModel.DisplayNameAttribute
            //                    {
            //                        public ValidatePatternAttribute(string data) : base(data) { }
            //                    }
            //";

            //generate class
            createClassFile(propertiesTree, ref outputFileStr);

            outputFileStr += "\n}\n";
            File.WriteAllText("consentsPropertiesClass.cs", outputFileStr);
        }

        static void generateEventTrackingProperties()
        {
            //this function is identical to generateBaseProperties...

            BasePropertiesItem propertiesTree = null;

            /* download dynamically updated based properties */
            string jsonString = Connection.DoGetWebRequest("/docs/schema/event/tracking.json");
            JSONUtilities.SaveJsonSchema("docs.schema.event.tracking.json", jsonString);

            jsonString = JSONUtilities.FixReference(jsonString);

            if (string.IsNullOrEmpty(jsonString))
            {
                Console.WriteLine("Error: not valid token");
                Console.ReadKey();
            }
            propertiesTree = JsonConvert.DeserializeObject<BasePropertiesItem>(jsonString);
            propertiesTree.name = "TrackingProperties";

            if (propertiesTree == null) return;

            //generate file baseProperties.cs 
            string outputFileStr = String.Empty;

            outputFileStr += "/* selfgenerated from version 0.0.0.1 " + DateTime.Now.ToString() + " */\n\n";
            outputFileStr += "using System;\n";
            outputFileStr += "using System.Collections.Generic;\n";
            outputFileStr += "using System.Globalization;\n";
            outputFileStr += "using Newtonsoft.Json;\n";
            outputFileStr += "using System.ComponentModel.DataAnnotations;\n";
            outputFileStr += "namespace ContactHubSdkLibrary {\n";

            //outputFileStr += @"
            //                    public class ValidatePatternAttribute : System.ComponentModel.DisplayNameAttribute
            //                    {
            //                        public ValidatePatternAttribute(string data) : base(data) { }
            //                    }
            //";

            //generate class
            createClassFile(propertiesTree, ref outputFileStr);
            outputFileStr += "\n}\n";
            File.WriteAllText("trackingClass.cs", outputFileStr);
        }

        static void generateEventProperties()
        {
            BasePropertiesItem propertiesTree = null;
            List<String> list = new List<String>();
            List<Event> eventList = new List<Event>();

            //read enum with eventType list
            string jsonString = Connection.DoGetWebRequest("/docs/schema/enums.json");

            list = JSONUtilities.GetEnumList(jsonString);
            Event e = null;
            //request single eventType schema
            foreach (string c in list)
            {
                jsonString = Connection.DoGetWebRequest("/docs/schema/event/" + c + ".post.json");  //get specific eventType schema
                jsonString = JSONUtilities.FixReference(jsonString);

                JSONUtilities.SaveJsonSchema("docs.schema.event." + c + ".post.json", jsonString);

                JObject eventObj = JObject.Parse(jsonString);

                e = new Event()
                {
                    id = c,
                    description = eventObj["description"].ToString(),
                    type = eventObj["type"].ToString(),
                    propertiesSchema = eventObj["properties"]["properties"]
                };

                eventList.Add(e);
            }


            string eventJson = "";
            //scroll through the events and generates the corresponding classes

            //generate file baseProperties.cs 
            string outputFileStr = String.Empty;

            outputFileStr += "/* selfgenerated from version 0.0.0.1 " + DateTime.Now.ToString() + " */\n\n";
            outputFileStr += "using System;\n";
            outputFileStr += "using System.Collections.Generic;\n";
            outputFileStr += "using System.Globalization;\n";
            outputFileStr += "using Newtonsoft.Json;\n";
            outputFileStr += "using Newtonsoft.Json.Linq;\n";
            outputFileStr += "using System.ComponentModel.DataAnnotations;\n";
            outputFileStr += "namespace ContactHubSdkLibrary.Events {\n";
            outputFileStr += "public class EventBaseProperty {}\n";

            foreach (Event ev in eventList)
            {

                outputFileStr += "/// <summary>\n";
                outputFileStr += "/// Event class '" + ev.id + "': " + ev.description + "\n";
                outputFileStr += "/// </summary>";

                //
                eventJson = ev.propertiesSchema.ToString();
                propertiesTree = JsonConvert.DeserializeObject<BasePropertiesItem>(eventJson);
                propertiesTree.name = "EventProperty" + uppercaseFirst(ev.id) + ": EventBaseProperty";

                if (propertiesTree == null) return;


                createClassFile(propertiesTree, ref outputFileStr);
                outputFileStr += "\n";
            }

            //generate a enum with the list of generated classes event type
            List<String> classEnum = new List<string>();
            foreach (String s in generatedClass)
            {
                if (s.Contains(": EventBaseProperty"))
                    classEnum.Add(lowercaseFirst(s.Replace(": EventBaseProperty", "").Replace("EventProperty", "")));
            }
            DictionaryEntry dic = new DictionaryEntry();
            dic.Key = "EventTypeEnum";
            dic.Value = classEnum.ToArray(); //convert to string[]
            createEnumFile(dic, ref outputFileStr);
            //generate casting function for event properties
            outputFileStr += @"
                public static class EventPropertiesUtil
                {
                    /// <summary>
                    /// Return events properties with right cast, event type based
                    /// </summary>

                  public static object GetEventProperties(JObject jo, JsonSerializer serializer)
                    {
                        var typeName = jo[""type""].ToString().ToLowerInvariant();
                        switch (typeName)
                        {
                    ";
            string className = "";
            foreach (String s in generatedClass)
            {
                if (s.Contains(": EventBaseProperty"))
                {
                    className = s.Replace(": EventBaseProperty", "");
                    outputFileStr += @" case """ + className.Replace(": EventBaseProperty", "").Replace("EventProperty", "").ToLowerInvariant() + @""": return jo[""properties""].ToObject<" + className + ">(serializer);break;\n\n";
                }
            }
            outputFileStr += "}\n";
            outputFileStr += " return null;\n}\n";
            outputFileStr += "\n}\n}\n";

            File.WriteAllText("eventPropertiesClass.cs", outputFileStr);
        }

        private static string processObject(BasePropertiesItem p)
        {
            string name = p.name;
            if (!cs.IsValidIdentifier(name))
            {
                name = "@" + name;
            }

            string processObject = "";

            if (p.description != null)
                processObject = String.Format("\t[Display(Name=\"{0}\")]\n", p.description);
            processObject = String.Format("    public {0} {1} {{get;set;}}\n", uppercaseFirst(JsonUtil.fixName(name)), JsonUtil.fixName(name));
            return processObject;
        }
        private static string processDynamicObject(BasePropertiesItem p)
        {
            string name = p.name;
            if (!cs.IsValidIdentifier(name))
            {
                name = "@" + name;
            }

            string processObject = "";

            if (p.description != null)
                processObject = String.Format("\t[Display(Name=\"{0}\")]\n", p.description);
            processObject = String.Format("    public dynamic {0} {{get;set;}}\n", JsonUtil.fixName(name));
            return processObject;
        }
        private static string processString(BasePropertiesItem p)
        {
            string DATEPATTERN = "^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$";
            List<String> dateTimeListField = new List<String> {
                "createdTime",
                "updatedAt", "registeredAt",
                "startDate", "endDate",
                "date"
            };
            List<String> dateListField = new List<String>
            {
                "startDate", "endDate"
            };

            string name = p.name;
            if (!cs.IsValidIdentifier(name))
            {
                name = "@" + name;
            }
            string processString = "";

            if (name == "startDate")
            {

            }
            if (!dateTimeListField.Contains(name) && !dateListField.Contains(name))
            {
                if (p.pattern != null)
                {
                    processString += String.Format("\t[ValidatePattern(@\"{0}\")]\n", p.pattern);
                }
                if (p.description != null)
                    processString += String.Format("\t[Display(Name=\"{0}\")]\n", p.description);
                if (p.format != null) processString += "    //format: " + p.format + "\n";
                processString += String.Format("    public string {0} {{get;set;}}\n", JsonUtil.fixName(name));
            }
            else if (dateTimeListField.Contains(name) && p.format.ToLowerInvariant().Trim() == "date-time")//is a list of specific fields ranging rendered as datetime
            {
                processString += String.Format("    [JsonProperty(\"{0}\")]\n", name);
                processString += String.Format("    public string _{0} {{get;set;}}\n", JsonUtil.fixName(name));
                processString += String.Format("    [JsonProperty(\"_{0}\")]\n", name);
                processString += String.Format("    [JsonIgnore]\n");
                processString += @" 
                 public DateTime $NAME$
        {
            get
            {
                if (_$NAME$ != null)
                {
                    if (_$NAME$.Contains(""+""))  //date format: 2017-01-25T17:14:01.000+0000
                        {
                           return Convert.ToDateTime(_$NAME$).ToUniversalTime();
                        }
                    else  //date format yyyy-MM-dd'T'HH:mm:ssZ
                    {
                        if (_$NAME$.Contains(""T"")) 
                        {
                         return
                         DateTime.ParseExact(_$NAME$,
                                       ""yyyy-MM-dd'T'HH:mm:ss'Z'"",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal);
                       }
                       else
                       {
                            return DateTime.MinValue;
                       }
                    }
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                try
                {
                    _$NAME$ = value.ToString(""yyyy-MM-ddTHH\\:mm\\:ssZ"");
                }
                catch { _$NAME$ = null; }
            }
        }
            ";
            }
            else if (dateListField.Contains(name) && p.format.ToLowerInvariant().Trim() == "date")//is a list of special fields that are to be rendered as dates
            {

                processString += String.Format("    [JsonProperty(\"{0}\")]\n", name);
                processString += String.Format("    public string _{0} {{get;set;}}\n", JsonUtil.fixName(name));
                processString += String.Format("    [JsonProperty(\"_{0}\")]\n", name);
                processString += String.Format("    [JsonIgnore]\n");
                processString += @" 
                 public DateTime $NAME$
        {
            get
            {
                if (_$NAME$ != null)
                {
                    return
                         DateTime.ParseExact(_$NAME$,
                                       ""yyyy-MM-dd"",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                try
                {
                    _$NAME$ = value.ToString(""yyyy-MM-dd"");
                }
                catch { _$NAME$ = null; }
            }
        }
            ";
            }
            processString = processString.Replace("$NAME$", name);
            return processString;
        }
        private static string processNumberDecimal(BasePropertiesItem p)
        {
            string name = p.name;
            if (!cs.IsValidIdentifier(name))
            {
                name = "@" + name;
            }
            string processString = "";
            if (p.pattern != null)
            {
                processString += String.Format("\t[ValidatePattern(@\"{0}\")]\n", p.pattern);
            }
            if (p.description != null)
                processString += String.Format("\t[Display(Name=\"{0}\")]\n", p.description);
            processString += String.Format("    public decimal? {0} {{get;set;}}\n", JsonUtil.fixName(name));
            return processString;
        }
        private static string processNumberInteger(BasePropertiesItem p)
        {
            string name = p.name;
            if (!cs.IsValidIdentifier(name))
            {
                name = "@" + name;
            }
            string processString = "";
            if (p.pattern != null)
            {
                processString += String.Format("\t[ValidatePattern(@\"{0}\")]\n", p.pattern);
            }
            if (p.description != null)
                processString += String.Format("\t[Display(Name=\"{0}\")]\n", p.description);
            processString += String.Format("    public int {0} {{get;set;}}\n", JsonUtil.fixName(name));
            return processString;
        }
        private static string processBoolean(BasePropertiesItem p)
        {
            string name = p.name;
            if (!cs.IsValidIdentifier(name))
            {
                name = "@" + name;
            }
            string processString = "";
            if (p.description != null)
                processString += String.Format("\t[Display(Name=\"{0}\")]\n", p.description);
            processString += String.Format("    public Boolean {0} {{get;set;}}\n", JsonUtil.fixName(name));
            return processString;
        }
        private static string processEnum(BasePropertiesItem parent, BasePropertiesItem p)
        {
            string name = p.name;
            if (!cs.IsValidIdentifier(name))
            {
                name = "@" + name;
            }
            string processEnum = "";
            if (p.description != null)
            {
                processEnum += String.Format("\t[Display(Name=\"{0}\")]\n", p.description);
            }
            processEnum += String.Format("[JsonProperty(\"{0}\")]", JsonUtil.fixName(name));
            processEnum += String.Format("public string _{0} {{get;set;}}\n", JsonUtil.fixName(name));
            processEnum += String.Format("[JsonProperty(\"hidden_{0}\")]", JsonUtil.fixName(name));
            processEnum += String.Format("[JsonIgnore]");
            processEnum += String.Format(@"
                    public {0} {1} 
            {{
                get
                {{
                        {0} enumValue =ContactHubSdkLibrary.EnumHelper<{0}>.GetValueFromDisplayName(_{1});
                        return enumValue;
                }}
                set
                {{
                        var displayValue = ContactHubSdkLibrary.EnumHelper<{0}>.GetDisplayValue(value);
                        _{1} = (displayValue==""{2}""? null : displayValue);
                }}
            }}
            ", uppercaseFirst(parent.name) + uppercaseFirst(JsonUtil.fixName(name) + "Enum"), JsonUtil.fixName(name), Common.NO_VALUE);
            return processEnum;
        }
        private static string processArray(BasePropertiesItem p)
        {
            string name = p.name;
            if (!cs.IsValidIdentifier(name))
            {
                name = "@" + name;
            }
            string processArray = "";
            if (p.description != null)
                processArray += String.Format("\t[Display(Name=\"{0}\")]\n", p.description);
            var itemType = p.items.type;
            switch ((String)itemType)
            {
                case "object":
                    {//It is a custom object with the properties, so we will generate the relevant class
                        processArray += String.Format("    public List<{0}> {1} {{get;set;}}\n", uppercaseFirst(JsonUtil.fixName(name)), JsonUtil.fixName(name));
                    }
                    break;
                case "string":
                    processArray += String.Format("    public List<{0}> {1} {{get;set;}}\n", "String", JsonUtil.fixName(name));
                    break;
            }
            return processArray;
        }
        private static void createClassFile(BasePropertiesItem outputProperties, ref string outputFileStr)
        {
            //avoids creating a class already previously written
            if (generatedClass.Contains(outputProperties.name)) return;
            generatedClass.Add(outputProperties.name);

            outputFileStr += String.Format("\npublic class {0}\n", uppercaseFirst(outputProperties.name));
            outputFileStr += "{\n";
            List<BasePropertiesItem> classToGenerate = new List<BasePropertiesItem>();
            Hashtable enumToGenerate = new Hashtable();
            string localType = (outputProperties.type is String ? outputProperties.type.ToString() : outputProperties.type[0].ToString());

            switch (localType)
            {
                case "object"://It is an object that contains properties
                    {

                        if (outputProperties.properties != null)
                        {
                            foreach (BasePropertiesItem pItem in outputProperties.properties)
                            {
                                if (pItem != null)
                                {
                                    BasePropertiesItem p = pItem;

                                    if (pItem.type == null)
                                    {
                                        //object definition is in external ref files, probably is a Enum
                                        //reference url is in p.reference attribute
                                        string jsonString = "";
                                        if (p.reference != null) // && p.reference.Contains("#")) 
                                        {
                                            //there are two types of reference
                                            //a) reference splitted with #  with format: "https://api.contactlab.it/hub/v1/docs/schema/enums.json#/definitions/Currency
                                            //b) direct reference: https://api.contactlab.it/hub/v1/docs/schema/event/user.json
                                            if (p.reference.Contains("#"))
                                            {
                                                string[] tmp = p.reference.Split('#');
                                                jsonString = Connection.DoGetWebRequest(tmp[0], false);
                                                string pathName = tmp[1].Replace("/", ".");
                                                pathName = pathName.Substring(1, pathName.Length - 1);
                                                JObject obj = JObject.Parse(jsonString);
                                                foreach (var c in obj["definitions"])
                                                {
                                                    if (c.Path == pathName)  //path name with format: "definitions.Currency"
                                                    {
                                                        string externalSchema = c.First.ToString();
                                                        BasePropertiesItem externalSchemaObj = JsonConvert.DeserializeObject<BasePropertiesItem>(externalSchema);
                                                        externalSchemaObj.name = p.name; //hold orig name
                                                        p = externalSchemaObj;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                jsonString = Connection.DoGetWebRequest(p.reference, false);
                                                BasePropertiesItem externalSchemaObj = JsonConvert.DeserializeObject<BasePropertiesItem>(jsonString);
                                                externalSchemaObj.name = p.name; //hold orig name
                                                p = externalSchemaObj;

                                            }
                                        }
                                        //this section is similar to previous: object definition is in external ref files, probably is a Enum
                                        //reference url is in "OneOf" json structure
                                        if (p.referenceOneOf != null) // && p.referenceOneOf[0].reference.Contains("#")) //reference with format: "https://api.contactlab.it/hub/v1/docs/schema/enums.json#/definitions/Currency"
                                        {
                                            //string[] tmp = p.referenceOneOf[0].reference.Split('#');
                                            //jsonString = Connection.DoGetWebRequest(tmp[0], false);
                                            //string pathName = tmp[1].Replace("/", ".");
                                            //pathName = pathName.Substring(1, pathName.Length - 1);
                                            //JObject obj = JObject.Parse(jsonString);
                                            //foreach (var c in obj["definitions"])
                                            //{
                                            //    if (c.Path == pathName)  //path name with format: "definitions.Currency"
                                            //    {
                                            //        string externalSchema = c.First.ToString();
                                            //        BasePropertiesItem externalSchemaObj = JsonConvert.DeserializeObject<BasePropertiesItem>(externalSchema);
                                            //        externalSchemaObj.name = p.name; //hold orig name
                                            //        p = externalSchemaObj;
                                            //    }
                                            //}

                                            //$ref in OneOf
                                            if (p.referenceOneOf[0].reference.Contains("#"))
                                            {
                                                string[] tmp = p.referenceOneOf[0].reference.Split('#');
                                                jsonString = Connection.DoGetWebRequest(tmp[0], false);
                                                string pathName = tmp[1].Replace("/", ".");
                                                pathName = pathName.Substring(1, pathName.Length - 1);
                                                JObject obj = JObject.Parse(jsonString);
                                                foreach (var c in obj["definitions"])
                                                {
                                                    if (c.Path == pathName)  //path name with format: "definitions.Currency"
                                                    {
                                                        string externalSchema = c.First.ToString();
                                                        BasePropertiesItem externalSchemaObj = JsonConvert.DeserializeObject<BasePropertiesItem>(externalSchema);
                                                        externalSchemaObj.name = p.name; //hold orig name
                                                        p = externalSchemaObj;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                jsonString = Connection.DoGetWebRequest(p.referenceOneOf[0].reference, false);
                                                BasePropertiesItem externalSchemaObj = JsonConvert.DeserializeObject<BasePropertiesItem>(jsonString);
                                                //extenal reference could be a multiple definition (AnyOf)
                                                if (externalSchemaObj.anyOf==null)
                                                {
                                                    externalSchemaObj.name = p.name; //hold orig name
                                                    p = externalSchemaObj;
                                                }
                                                else
                                                {
                                                    //use first definition
                                                    externalSchemaObj.anyOf[0].name = p.name; //hold orig name
                                                    p = externalSchemaObj.anyOf[0];
                                                }
                                            }
                                        }

                                        //if (p.anyOf != null) // && p.referenceAnyOf[0].reference.Contains("#")) //reference with format: "https://api.contactlab.it/hub/v1/docs/schema/enums.json#/definitions/Currency"
                                        //{
                                        //    BasePropertiesItem externalSchemaObj = p.anyOf[0];
                                        //    externalSchemaObj.name = p.anyOf[0].name; //hold orig name
                                        //    p = externalSchemaObj;

                                        //    ////$ref in AnyOf
                                        //    //if (p.referenceAnyOf[0].reference.Contains("#"))
                                        //    //{
                                        //    //    string[] tmp = p.referenceAnyOf[0].reference.Split('#');
                                        //    //    jsonString = Connection.DoGetWebRequest(tmp[0], false);
                                        //    //    string pathName = tmp[1].Replace("/", ".");
                                        //    //    pathName = pathName.Substring(1, pathName.Length - 1);
                                        //    //    JObject obj = JObject.Parse(jsonString);
                                        //    //    foreach (var c in obj["definitions"])
                                        //    //    {
                                        //    //        if (c.Path == pathName)  //path name with format: "definitions.Currency"
                                        //    //        {
                                        //    //            string externalSchema = c.First.ToString();
                                        //    //            BasePropertiesItem externalSchemaObj = JsonConvert.DeserializeObject<BasePropertiesItem>(externalSchema);
                                        //    //            externalSchemaObj.name = p.name; //hold orig name
                                        //    //            p = externalSchemaObj;
                                        //    //        }
                                        //    //    }
                                        //    //}
                                        //    //else
                                        //    //{
                                        //    //    jsonString = Connection.DoGetWebRequest(p.referenceAnyOf[0].reference, false);
                                        //    //    BasePropertiesItem externalSchemaObj = JsonConvert.DeserializeObject<BasePropertiesItem>(jsonString);
                                        //    //    externalSchemaObj.name = p.name; //hold orig name
                                        //    //    p = externalSchemaObj;

                                        //    //}
                                        //}
                                    }


                                    if (p.type == null)
                                    {
                                        p.type = "object";
                                    }

                                    localType = (p.type is String ? p.type.ToString() : p.type[0].ToString());
                                    //if it is a container, then processes the properties inside

                                    switch (localType)
                                    {
                                        case "object":
                                            {
                                                if (p.properties != null)
                                                {
                                                    outputFileStr += processObject(p);
                                                    classToGenerate.Add(p);
                                                }
                                                else
                                                {
                                                    //It is a generic object, dynamic, untyped
                                                    outputFileStr += processDynamicObject(p);
                                                }
                                            }
                                            break;
                                        case "number":
                                            {
                                                outputFileStr += processNumberDecimal(p);
                                            }
                                            break;
                                        case "integer":
                                            {
                                                outputFileStr += processNumberInteger(p);
                                            }
                                            break;
                                        case "boolean":
                                            {
                                                outputFileStr += processBoolean(p);
                                            }
                                            break;
                                        case "string":
                                            {
                                                if (p.@enum == null) //normal string
                                                {

                                                    outputFileStr += processString(p);
                                                }
                                                else  //It is a string base enum
                                                {
                                                    //if  class name contains inheritance, remove parent class
                                                    if (outputProperties.name.Contains(":"))
                                                    {
                                                        outputProperties.name = outputProperties.name.Split(':')[0];
                                                    }
                                                    outputFileStr += processEnum(outputProperties, p);
                                                    //creates an enum that has the name of the father, to avoid multiple definitions of different objects with the same name
                                                    enumToGenerate.Add(uppercaseFirst(outputProperties.name) + uppercaseFirst(JsonUtil.fixName(p.name) + "Enum"), p.@enum);
                                                }
                                            }
                                            break;
                                        case "array":
                                            {

                                                //items contains the definition of the class that makes up the array
                                                p.items.name = p.name;
                                                p.items.description = p.description;
                                                outputFileStr += processArray(p);
                                                //items containing the model of the array elements. If it is an object you must then fill, provided it is really an object with the properties
                                                var itemType = p.items.type;
                                                if ((String)itemType == "object")
                                                {
                                                    classToGenerate.Add(p.items);
                                                }
                                            }
                                            break;
                                        default:
                                            //error other types
                                            break;

                                    }
                                }
                            }
                        }
                        else //It is a generic object
                        {

                        }
                    }
                    break;
                default:
                    {
                        var x = outputProperties.type;
                    }
                    break;
            }

            outputFileStr += "}\n\n";
            //processa le classi figli che ha trovato
            foreach (BasePropertiesItem p in classToGenerate)
            {
                createClassFile(p, ref outputFileStr);
            }

            //processes the enum that has found
            foreach (DictionaryEntry e in enumToGenerate)
            {
                createEnumFile(e, ref outputFileStr);
            }
        }
        static void createEnumFile(DictionaryEntry enumObj, ref string outputFileStr)
        {

            string key = enumObj.Key.ToString();
            if (!cs.IsValidIdentifier(key))
            {
                key = "@" + key;
            }
            outputFileStr += String.Format("public enum {0} {{\n", key);
            outputFileStr += String.Format("\t{0},\n", Common.NO_VALUE);
            string str = "";
            foreach (string enumItem in (string[])enumObj.Value)
            {
                if (!string.IsNullOrEmpty(enumItem))
                {
                    outputFileStr += String.Format("\t[Display(Name=\"{0}\")]\n", enumItem);
                    outputFileStr += String.Format("\t{0},\n", Common.makeValidFileName(enumItem));
                }
            }
            if (outputFileStr.EndsWith(",\n"))
            {
                outputFileStr = outputFileStr.Substring(0, outputFileStr.Length - 2);
            }

            outputFileStr += String.Format("\n}}");
        }
        static string uppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        static string lowercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToLower(s[0]) + s.Substring(1);
        }
    }
}
