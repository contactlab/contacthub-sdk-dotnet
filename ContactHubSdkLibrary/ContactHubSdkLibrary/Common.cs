using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

public static class Common
{
    /// <summary>
    /// Clean filename from invalid chars
    /// </summary>
    public static string MakeValidFileName(string name)
    {
        name = name.Replace("-", "Minus");
        Regex illegalInFileName = new Regex(@"[^a-zA-Z0-9]");
        name = illegalInFileName.Replace(name, "");
        return name;
    }

    /// <summary>
    /// Delete last comma from comma-separated string array
    /// </summary>
    public static void CleanComma(ref string returnValue)
    {
        if (returnValue.EndsWith(","))
        {
            returnValue = returnValue.Substring(0, returnValue.Length - 1);
        }
    }

    /// <summary>
    /// Force url to https
    /// </summary>
    public static void FixApiUrl(ref string url)
    {
        //fix url
        if (url.Contains("http://"))
        {
            url = url.Replace("http://", "https://");
        }
    }

    /// <summary>
    /// Converte la data in stringa formato Iso8601
    /// </summary>
    public static string ConvertToIso8601Date(DateTime d)
    {
        return d.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }
    /// <summary>
    /// Check if json contains error and return error details
    /// </summary>
    public static Error ResponseIsError(string json)
    {
        if (json != null && json.ToLower().Contains("logref"))
        {
            Error error = JsonConvert.DeserializeObject<Error>(json);
            return error;
        }
        else
        {
            return null;
        }
    }

    public static void WriteLog(string function, string data)
    {
        StreamWriter log;
        bool enabled = (ConfigurationManager.AppSettings["ContactHubSdkEnableLog"] != null ? Convert.ToBoolean(ConfigurationManager.AppSettings["ContactHubSdkEnableLog"]) : false);
        if (enabled)
        {
            try
            {
                string fileName = ConfigurationManager.AppSettings["ContactHubSdkPathLog"];

                if (!File.Exists(fileName))
                {

                    log = new StreamWriter(fileName);

                }

                else

                {

                    log = File.AppendText(fileName);

                }
                log.WriteLine(DateTime.Now.ToString("yyy-MM-dd HH:mm:ss:zzz") + " " + function + " " + data);
                log.Close();
            }
            catch { };
        }
    }

    /// <summary>
    /// Object conversion
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static TOut GetShallowCopyByReflection<TOut>(this Object objIn)
    {
        Type inputType = objIn.GetType();
        Type outputType = typeof(TOut);
        if (!outputType.Equals(inputType) && !outputType.IsSubclassOf(inputType)) throw new ArgumentException(String.Format("{0} is not a sublcass of {1}", outputType, inputType));
        PropertyInfo[] properties = inputType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
        FieldInfo[] fields = inputType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
        TOut objOut = (TOut)Activator.CreateInstance(typeof(TOut));
        foreach (PropertyInfo property in properties)
        {
            try
            {
                property.SetValue(objIn, property.GetValue(objIn, null), null);
            }
            catch (ArgumentException) { } // For Get-only-properties
        }
        foreach (FieldInfo field in fields)
        {
            field.SetValue(objOut, field.GetValue(objIn));
        }
        return objOut;
    }


}

