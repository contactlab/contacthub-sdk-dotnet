using ContactHubSdkLibrary;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;


public static class Common
{
    public static string MakeValidFileName(string name)
    {
        name = name.Replace("-", "Minus");
        Regex illegalInFileName = new Regex(@"[^a-zA-Z0-9]");
        name = illegalInFileName.Replace(name, "");
        return name;
    }

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
    /// <param name="url"></param>
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
    /// <param name="d"></param>
    /// <returns></returns>
    public static string ConvertToIso8601Date(DateTime d)
    {
        return d.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }
}

