using Microsoft.CSharp;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace generateBasePropertiesClass
{

    class Program
    {
        static CSharpCodeProvider cs = new CSharpCodeProvider();
        /* genera le classi per le base properties basandosi sulla definizione dinamica ottenuta da /models/properties/base */
        static void Main(string[] args)
        {
            generate();
        }


        static void generate()
        {
            //carica il file localmente (è stato scaricato in precedenza)
            //BasePropertiesRoot properties = null;

            //BasePropertiesRoot ro = new BasePropertiesRoot();
            BasePropertiesItem2 propertiesTree = null;
            try
            {
                StreamReader sr = new StreamReader("./properties_base.json");
                string jsonString = sr.ReadToEnd();
                // properties = JsonConvert.DeserializeObject<BasePropertiesRoot>(jsonString);
                propertiesTree = JsonConvert.DeserializeObject<BasePropertiesItem2>(jsonString);
                propertiesTree.name = "BaseProperties";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

            if (propertiesTree == null) return;

            //genera il file baseProperties.cs 
            string outputFileStr = String.Empty;


            outputFileStr += "/* version 0.0.0.1 */\n\n\n";
            outputFileStr += "using System;\n";
            outputFileStr += "using System.Collections.Generic;\n";
            outputFileStr += "using System.ComponentModel;\n";
            outputFileStr += "using Newtonsoft.Json;\n";
            outputFileStr += "using System.ComponentModel.DataAnnotations;\n";
            outputFileStr += "namespace ContactHubSdklibrary {\n";

            //            outputFileStr += @"
            //[AttributeUsage(AttributeTargets.Field)]
            //public class EnumDisplayNameAttribute : System.ComponentModel.DisplayNameAttribute
            //{
            //    public EnumDisplayNameAttribute(string data) : base(data) { }
            //}
            //            ";
            //            outputFileStr += @"
            //public class FieldDisplayNameAttribute : System.ComponentModel.DisplayNameAttribute
            //{
            //    public FieldDisplayNameAttribute(string data) : base(data) { }
            //}
            //            ";
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



        private static string processObject(BasePropertiesItem2 p)
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

        private static string processString(BasePropertiesItem2 p)
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
            processString += String.Format("    public string {0} {{get;set;}}\n", JsonUtil.fixName(name));
            return processString;
        }

        private static string processNumber(BasePropertiesItem2 p)
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

        private static string processEnum(BasePropertiesItem2 parent, BasePropertiesItem2 p)
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

        private static string processArray(BasePropertiesItem2 p)
        {
            string name = p.name;
            if (!cs.IsValidIdentifier(name))
            {
                name = "@" + name;
            }
            string processArray = "";
            if (p.description != null)
                processArray += String.Format("\t[Display(Name=\"{0}\")]\n", p.description);
            processArray += String.Format("    public List<{0}> {1} {{get;set;}}\n", uppercaseFirst(JsonUtil.fixName(name)), JsonUtil.fixName(name));
            return processArray;
        }

        private static void createClassFile(BasePropertiesItem2 outputProperties, ref string outputFileStr, int level = 0)
        {
            if (outputProperties.name == "geo")
            {

            }
            outputFileStr += String.Format("\npublic class {0}\n", uppercaseFirst(outputProperties.name));
            outputFileStr += "{\n";
            List<BasePropertiesItem2> classToGenerate = new List<BasePropertiesItem2>();
            Hashtable enumToGenerate = new Hashtable();
            switch (outputProperties.type)
            {

                case "object"://è un oggetto che contiene delle properties
                    {
                        foreach (BasePropertiesItem2 p in outputProperties.properties)
                        {
                            //se è un contenitore, allora processa le properties all'interno
                            switch (p.type)
                            {
                                case "object":
                                    {
                                        outputFileStr += processObject(p);
                                        classToGenerate.Add(p);
                                    }
                                    break;
                                case "number":
                                    {
                                        outputFileStr += processNumber(p);
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
                                        classToGenerate.Add(p.items);
                                    }
                                    break;

                            }
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
            foreach (BasePropertiesItem2 p in classToGenerate)
            {
                createClassFile(p, ref outputFileStr, level++);
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
            outputFileStr += String.Format("\t{0},\n",Common.NO_VALUE);
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
    }
}
