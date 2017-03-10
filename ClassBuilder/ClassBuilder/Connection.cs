using System;
using System.Configuration;

namespace generateBasePropertiesClass
{
    public static class Connection
    {
        private static string GetUrl(string functionPath)
        {
            return GetBaseUrl() + functionPath;
        }

        private static string GetBaseUrl()
        {
            string returnValue = null;
            returnValue = "https://api.contactlab.it/hub/v1";
            return returnValue;
        }

        public static string DoGetWebRequest(string functionPath, bool addBasePath = true)
        {
            string jsonResponse = null;
            try
            {
                //verify if is relative or absolute path
                string url = (addBasePath? GetUrl(functionPath) : functionPath);
                var webRequest = System.Net.WebRequest.Create(url);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 30000;
                    using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            jsonResponse = sr.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }
            return jsonResponse;
        }
    }
}
