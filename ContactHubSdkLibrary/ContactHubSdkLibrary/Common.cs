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
        if (json != null  && (json.ToLower().Contains("logref") || json.ToLower().Contains("error")))
        {
            try
            {
                Error error = JsonConvert.DeserializeObject<Error>(json);
                return error;
            }
            catch (Exception ex)
            {
                return null;
            }
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
    public static TOut CreateObject<TOut>(this Object objIn)
    {

        TOut ret = System.Activator.CreateInstance<TOut>();

        PropertyInfo[] propTo = ret.GetType().GetProperties();
        PropertyInfo[] propFrom = objIn.GetType().GetProperties();

        // for each property check whether this data item has an equivalent property
        // and copy over the property values as neccesary.
        foreach (PropertyInfo propT in propTo)
        {
            foreach (PropertyInfo propF in propFrom)
            {
                if (propT.Name == propF.Name)
                {
                    propF.SetValue(ret, propF.GetValue(objIn));
                    break;
                }
            }
        }

        return ret;
    }

    /// <summary>
    /// Verify if string is json (simple check)
    /// </summary>
    public static bool isJson(string str)
    {
        if (string.IsNullOrEmpty(str)) return false;
        return (str.Contains("{") && str.Contains("}"));
    }

    /// <summary>
    /// Verify is status code is critical (50x)
    /// </summary>
    public static bool isCriticalError(String str)
    {

        if (string.IsNullOrEmpty(str)) return false;
        if (str.StartsWith("50")) return true;
        return false;
    }
}

