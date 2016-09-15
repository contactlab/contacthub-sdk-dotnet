﻿using System;
using System.IO;
using System.Net;
using System.Text;

namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class CHubNode
    {
        #region Connections
        private string DoGetWebRequest(string functionPath, bool relativePath = true)
        {
            string jsonResponse = null;
            try
            {
                //controlla se è stato passato un url relativo oppure assoluto
                string url = (relativePath ? GetUrl(functionPath) : functionPath);
                var webRequest = System.Net.WebRequest.Create(url);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 30000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("Authorization", "Bearer " + _token);
                    using (System.IO.Stream s = webRequest.GetResponseWithoutException().GetResponseStream())
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
        private string DoDeleteWebRequest(string functionPath)
        {
            string jsonResponse = null;
            try
            {
                string url = GetUrl(functionPath);
                var webRequest = System.Net.WebRequest.Create(url);
                if (webRequest != null)
                {
                    webRequest.Method = "DELETE";
                    webRequest.Timeout = 30000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("Authorization", "Bearer " + _token);
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
        private string DoPostWebRequest(string functionPath, string jsonData)
        {
            string jsonResponse = null;
            try
            {
                string url = GetUrl(functionPath);


                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);

                Encoding encoding = new UTF8Encoding();

                string postData = jsonData;

                byte[] data = encoding.GetBytes(postData);

                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/json";
                httpWReq.Headers.Add("Authorization", "Bearer " + _token);
                httpWReq.ContentLength = data.Length;

                Stream stream = httpWReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponseWithoutException();
                string s = response.ToString();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                String temp = null;
                while ((temp = reader.ReadLine()) != null)
                {
                    jsonResponse += temp;
                }

                //Se il json è vuoto come nel caso del post di eventi che vengono accodati in modo asyncrono, restituisce lo status code, ad esempio post eventi è ok se restituisce 202
                if (string.IsNullOrEmpty(jsonResponse))
                {
                    jsonResponse += "{\"statusCode\":\"" + response.StatusCode + "\"}";
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                jsonResponse = "{\"error\":\"" + ex.Message + "\"}";
            }
            return jsonResponse;
        }
        private string DoPutWebRequest(string functionPath, string jsonData)
        {
            string jsonResponse = null;
            try
            {
                string url = GetUrl(functionPath);
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);
                Encoding encoding = new UTF8Encoding();
                string postData = jsonData;
                byte[] data = encoding.GetBytes(postData);
                httpWReq.Method = "PUT";
                httpWReq.ContentType = "application/json";
                httpWReq.Headers.Add("Authorization", "Bearer " + _token);
                httpWReq.ContentLength = data.Length;
                Stream stream = httpWReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                string s = response.ToString();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                String temp = null;
                while ((temp = reader.ReadLine()) != null)
                {
                    jsonResponse += temp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                jsonResponse = "{\"error\":\"" + ex.Message + "\"}";
            }
            return jsonResponse;
        }
        private string GetBaseUrl()
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
        private string GetUrl(string functionPath)
        {
            return GetBaseUrl() + functionPath;
        }
        #endregion
    }
}
