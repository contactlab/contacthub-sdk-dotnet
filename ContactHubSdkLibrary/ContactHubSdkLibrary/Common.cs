using System;
using System.IO;
using System.Text.RegularExpressions;
using ContactHubSdkLibrary;
using System.Configuration;

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


    public static void  WriteLog(string function, string data)
    {
        StreamWriter log;
        bool enabled = (ConfigurationManager.AppSettings["ContactHubSdkEnableLog"] != null ? Convert.ToBoolean(ConfigurationManager.AppSettings["ContactHubSdkEnableLog"]): false);
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
}

