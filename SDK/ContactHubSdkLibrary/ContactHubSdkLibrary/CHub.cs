using System;

namespace ContactHubSdkLibrary
{
    public  class CHubNode
    {
        private string _workspaceID = null;
        private string _token = null;
        private string _node = null;
        private const string _baseURL = "https://api.contactlab.it/hub/v1/workspaces/{id-workspace}";

        public  void Init(string workspaceID, string token,string node)
        {
            _workspaceID = workspaceID;
            _token = token;
            _node = node;
        }

        private  string GetBaseUrl()
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

        private  string GetUrl(string functionPath)
        {
            return GetBaseUrl()  + functionPath;
        }

        public string GetCustomers()
        {
            string returnValue = null;
            string customers = DoGetWebRequest(String.Format("/customers?nodeId={0}", _node)); 
            return returnValue;
        }

        private  string DoGetWebRequest(string functionPath)
        {
            string jsonResponse = null;
            try
            {
                string url = GetUrl(functionPath);
                var webRequest = System.Net.WebRequest.Create(url);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 30000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("Authorization", "Bearer " + _token);
                    using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            jsonResponse = sr.ReadToEnd();
                            Console.WriteLine(String.Format("Response: {0}", jsonResponse));
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

