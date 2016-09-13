using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generateBasePropertiesClass
{
    public static class Connection
    {

        private  const string _baseURL = "https://api.contactlab.it/hub/v1/workspaces/{id-workspace}";
        private static string _workspaceID = ConfigurationManager.AppSettings["workspaceID"].ToString();
        private static string token = ConfigurationManager.AppSettings["token"].ToString();


        private static string GetUrl(string functionPath)
        {
            return GetBaseUrl() + functionPath;
        }

        private static  string GetBaseUrl()
        {
            string returnValue = null;
            if (string.IsNullOrEmpty(_baseURL))
            {
                returnValue = null;
            }
            else
            {
                returnValue = _baseURL.Replace("{id-workspace}", _workspaceID);
            }
            return returnValue;
        }

        public static string DoGetWebRequest(string functionPath)
        {
            string jsonResponse = null;
            try
            {
                //controlla se è stato passato un url relativo oppure assoluto
                string url = GetUrl(functionPath);
                var webRequest = System.Net.WebRequest.Create(url);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 30000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("Authorization", "Bearer " + token);
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
                Console.WriteLine(ex.ToString());
            }
            return jsonResponse;
        }
    }
}
