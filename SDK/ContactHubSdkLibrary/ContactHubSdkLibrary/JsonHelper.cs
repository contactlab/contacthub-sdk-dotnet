using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContactHubSdkLibrary
{

    public static class JsonHelper
    {
        //rimuove i valori nulli dal json
        public static JToken RemoveEmptyChildren(JToken token)
        {
            if (token.Type == JTokenType.Object)
            {
                JObject copy = new JObject();
                foreach (JProperty prop in token.Children<JProperty>())
                {
                    JToken child = prop.Value;
                    if (child.HasValues)
                    {
                        child = RemoveEmptyChildren(child);
                    }
                    if (!IsEmpty(child))
                    {
                        copy.Add(prop.Name, child);
                    }
                }
                return copy;
            }
            else if (token.Type == JTokenType.Array)
            {
                JArray copy = new JArray();
                foreach (JToken item in token.Children())
                {
                    JToken child = item;
                    if (child.HasValues)
                    {
                        child = RemoveEmptyChildren(child);
                    }
                    if (!IsEmpty(child))
                    {
                        copy.Add(child);
                        if (child.Type == JTokenType.Object)
                        {
                            RemoveEmptyChildren(child);
                        }
                    }
                }
                return copy;
            }
            return token;
        }

        //converte il valore numerico dell'enum nel relativo valore in string

        //public static JToken ConvertEnum2String(JToken token)
        //{
        //    if (token.Type == JTokenType.Object)
        //    {
        //        JObject copy = new JObject();
        //        foreach (JProperty prop in token.Children<JProperty>())
        //        {
        //            JToken child = prop.Value;

        //            if (child.Path=="base.timezone")
        //            {
        //                child = "Acre Time";
        //                Type clsType = typeof(PostCustomer);
        //                MethodInfo mInfo = clsType.GetMethod("timezone");
        //            }
        //            if (!IsEmpty(child))
        //            {
        //                if (child.Type == JTokenType.Object)
        //                {
        //                    child = ConvertEnum2String(child);
        //                }
        //                copy.Add(prop.Name, child);

        //            }
        //        }
        //        return copy;
        //    }
        //    else if (token.Type == JTokenType.Array)
        //    {
        //        JArray copy = new JArray();
        //        foreach (JToken item in token.Children())
        //        {
        //            JToken child = item;
        //            if (child.HasValues)
        //            {
        //                child = ConvertEnum2String(child);
        //            }
        //            if (!IsEmpty(child))
        //            {
        //                copy.Add(child);
        //            }
        //        }
        //        return copy;
        //    }
        //    else
        //    {

        //    }
        //    return token;
        //}

        public static bool IsEmpty(JToken token)
        {
            return (token.Type == JTokenType.Null);
        }
    }

}
