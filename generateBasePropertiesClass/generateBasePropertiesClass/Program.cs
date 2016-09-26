﻿using Microsoft.CSharp;
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


        /* genera le classi per le base properties basandosi sulla definizione dinamica ottenuta da /models/properties/base */
        static void Main(string[] args)
        {
            generateBaseProperties();
            generateEventProperties();
            generateEventContext();
        }


        static void generateEventContext()
        {

            //crea un enum con i tipi di context. E' sicuramente una procedura provvisoria perchè probabilmente andrà generato anche le relative classi in base allo schema
            //da sistemare probabilmente dopo il 19/9
            string jsonString = Connection.DoGetWebRequest("/models/contexts");
            if (string.IsNullOrEmpty(jsonString))
            {
                Console.WriteLine("Error: not valid token");
                Console.ReadKey();
            }
            JObject propertiesTree = JObject.Parse(jsonString);
            JToken embedded = propertiesTree["_embedded"];
            JToken contexts = embedded["contexts"];
            List<String> enumList = new List<string>();
            foreach (JToken c in contexts.Children())
            {
                var item = c["id"].ToString();

                enumList.Add(item);
            }
            string outputFileStr = "";
            outputFileStr += "/* autogenerated from version 0.0.0.1 */\n\n\n";
            outputFileStr += "using System;\n";
            outputFileStr += "using System.Collections.Generic;\n";
            outputFileStr += "using System.Globalization;\n";
            outputFileStr += "using Newtonsoft.Json;\n";
            outputFileStr += "using System.ComponentModel.DataAnnotations;\n";

            //genera l'enum con l'elenco dei context
            DictionaryEntry dic = new DictionaryEntry();
            dic.Key = "EventContextEnum";
            dic.Value = enumList.ToArray(); //converte a string[]
            createEnumFile(dic, ref outputFileStr);

            File.WriteAllText("eventContextClass.cs", outputFileStr);
        }


        static void generateBaseProperties()
        {
            BasePropertiesItem propertiesTree = null;
            //carica il file localmente (è stato scaricato in precedenza)
            /*
            try
            {
                StreamReader sr = new StreamReader("./example.properties_base.json");
                string jsonStr = sr.ReadToEnd();
                // properties = JsonConvert.DeserializeObject<BasePropertiesRoot>(jsonString);
                propertiesTree = JsonConvert.DeserializeObject<BasePropertiesItem>(jsonStr);
                //propertiesTree.name = "BaseProperties";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            */

            /* scarica dinamicamente le base properties aggiornate */
            string jsonString = Connection.DoGetWebRequest("/models/properties/base");
            if (string.IsNullOrEmpty(jsonString))
            {
                Console.WriteLine("Error: not valid token");
                Console.ReadKey();
            }
            propertiesTree = JsonConvert.DeserializeObject<BasePropertiesItem>(jsonString);
            propertiesTree.name = "BaseProperties";



            if (propertiesTree == null) return;

            //genera il file baseProperties.cs 
            string outputFileStr = String.Empty;


            outputFileStr += "/* autogenerated from version 0.0.0.1 */\n\n\n";
            //         outputFileStr += "using System;\n";
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

            createClassFile(propertiesTree, ref outputFileStr);

            outputFileStr += "\n}\n";
            File.WriteAllText("basePropertiesClass.cs", outputFileStr);
        }
        static void generateEventProperties()
        {
            BasePropertiesItem propertiesTree = null;

            /* scarica dinamicamente le base properties aggiornate */
            string jsonString = Connection.DoGetWebRequest("/configuration/events");

            //ottiene la lista dei tipi di eventi

            EventPropertiesSchemaRoot root = JsonConvert.DeserializeObject<EventPropertiesSchemaRoot>(jsonString);
            string eventJson = "";
            //scorre gli eventi e genera le relative classi

            //genera il file baseProperties.cs 
            string outputFileStr = String.Empty;

            outputFileStr += "/* autogenerated from version 0.0.0.1 */\n\n\n";
            outputFileStr += "using System;\n";
            outputFileStr += "using System.Collections.Generic;\n";
            outputFileStr += "using System.Globalization;\n";
            outputFileStr += "using Newtonsoft.Json;\n";
            outputFileStr += "using System.ComponentModel.DataAnnotations;\n";
            outputFileStr += "namespace ContactHubSdkLibrary.Events {\n";
            outputFileStr += "public class EventBaseProperty {}\n";

            foreach (Event ev in root.embedded.events)
            {

                outputFileStr += "//event class '" + ev.id + "': " + ev.description;
                eventJson = ev.propertiesSchema.ToString();
                propertiesTree = JsonConvert.DeserializeObject<BasePropertiesItem>(eventJson);
                propertiesTree.name = "EventProperty" + uppercaseFirst(ev.id) + ": EventBaseProperty";

                if (propertiesTree == null) return;


                createClassFile(propertiesTree, ref outputFileStr);
                outputFileStr += "\n";
            }
            outputFileStr += "\n}\n";

            //genera l'enum con l'elenco delle classi generate di tipo evento
            List<String> classEnum = new List<string>();
            foreach (String s in generatedClass)
            {
                if (s.Contains(": EventBaseProperty"))
                    classEnum.Add(lowercaseFirst(s.Replace(": EventBaseProperty", "").Replace("EventProperty", "")));
            }
            DictionaryEntry dic = new DictionaryEntry();
            dic.Key = "EventTypeEnum";
            dic.Value = classEnum.ToArray(); //converte a string[]
            createEnumFile(dic, ref outputFileStr);
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
            List<String> dateTimeListField = new List<String> { "createdTime", "dateStart", "dateEnd", "updatedAt", "registeredAt" };
            List<String> dateListField = new List<String> { "start_date", "end_date" };

            string name = p.name;
            if (!cs.IsValidIdentifier(name))
            {
                name = "@" + name;
            }
            string processString = "";

            if (!dateTimeListField.Contains(name) && !dateListField.Contains(name))
            {
                if (p.pattern != null)
                {
                    processString += String.Format("\t[ValidatePattern(@\"{0}\")]\n", p.pattern);
                }
                if (p.description != null)
                    processString += String.Format("\t[Display(Name=\"{0}\")]\n", p.description);
                processString += String.Format("    public string {0} {{get;set;}}\n", JsonUtil.fixName(name));
            }
            else if (dateTimeListField.Contains(name))//è un elenco di campi particolari che vanno renderizzati come datetime
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
            else if (dateListField.Contains(name))//è un elenco di campi particolari che vanno renderizzati come date
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
            processString += String.Format("    public decimal {0} {{get;set;}}\n", JsonUtil.fixName(name));
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
                    {//è un oggetto custom con delle properties, per cui verrà generata la relativa classe
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
            //evita di rigenererare una classe già precedentemente scritturata
            if (generatedClass.Contains(outputProperties.name)) return;
            generatedClass.Add(outputProperties.name);

            outputFileStr += String.Format("\npublic class {0}\n", uppercaseFirst(outputProperties.name));
            outputFileStr += "{\n";
            List<BasePropertiesItem> classToGenerate = new List<BasePropertiesItem>();
            Hashtable enumToGenerate = new Hashtable();
            switch (outputProperties.type)
            {
                case "object"://è un oggetto che contiene delle properties
                    {
                        if (outputProperties.properties != null)
                        {
                            foreach (BasePropertiesItem p in outputProperties.properties)
                            {
                                if (p.name.Contains("extraProperties"))
                                {

                                }
                                //se è un contenitore, allora processa le properties all'interno
                                switch (p.type)
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
                                                //è un oggetto generico, dinamico, non tipizzato
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
                                            if (p.@enum == null) //stringa normale
                                            {
                                                outputFileStr += processString(p);
                                            }
                                            else  //è un enum su base stringa 
                                            {
                                                outputFileStr += processEnum(outputProperties, p);
                                                //crea un enum che ha nel nome anche il padre, per evitare definizioni doppie di oggetti diversi con lo stesso nome
                                                enumToGenerate.Add(uppercaseFirst(outputProperties.name) + uppercaseFirst(JsonUtil.fixName(p.name) + "Enum"), p.@enum);
                                            }
                                        }
                                        break;
                                    case "array":
                                        {

                                            //items contiene la definizione della classe che compone l'arrayy
                                            p.items.name = p.name;
                                            p.items.description = p.description;
                                            outputFileStr += processArray(p);
                                            //items contiene il model degli elementi dell'array. Se è un object lo deve poi scritturare , a patto che sia realmente un oggetto con delle properties
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
                        else //è un object generico
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

            //processa gli enum che ha trovato
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
            foreach (string enumItem in (string[])enumObj.Value)
            {
                outputFileStr += String.Format("\t[Display(Name=\"{0}\")]\n", enumItem);
                outputFileStr += String.Format("\t{0},\n", Common.makeValidFileName(enumItem));
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
