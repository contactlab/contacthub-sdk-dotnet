﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class Node
    {
        #region Connections
        /// <summary>
        /// Execute GET request on hub
        /// </summary>
        private string DoGetWebRequest(string functionPath, bool relativePath = true)
        {
            string jsonResponse = null;
            try
            {
                int reTryCount = 3;
                while (reTryCount > 0)
                {
                    jsonResponse = null;
                    //verify if url is relative or absolute
                    string url = (relativePath ? GetUrl(functionPath) : functionPath);
                    Common.FixApiUrl(ref url);
                    var webRequest = System.Net.WebRequest.Create(url);
                    string statusCode = null;
                    string status = null;
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
                         statusCode = (webRequest.Headers["StatusCode"] != null ? webRequest.Headers["StatusCode"].ToString() : null);
                         status = (webRequest.Headers["Status"] != null ? webRequest.Headers["Status"].ToString() : null);
                        if (!string.IsNullOrEmpty(statusCode) && (string.IsNullOrEmpty(jsonResponse) || !Common.isJson(jsonResponse)))
                        {
                            jsonResponse = "{\"status\":\"" + statusCode + "\",\"error\":\"" + status + "\"}";
                        }

                    }
                    //critical error retry
                    if (Common.isCriticalError(statusCode))
                    {
                        Thread.Sleep(Const.RETRYTIME);
                        reTryCount--;
                    }
                    else
                    {
                        reTryCount = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                jsonResponse = "{\"error\":\"" + ex.Message + "\"}";
            }
            return jsonResponse;
        }
        /// <summary>
        /// Execute DELETE request on hub
        /// </summary>
        private string DoDeleteWebRequest(string functionPath)
        {
            string jsonResponse = null;
            try
            {
                int reTryCount = 3;
                while (reTryCount > 0)
                {
                    jsonResponse = null;
                    string url = GetUrl(functionPath);
                    Common.FixApiUrl(ref url);

                    var webRequest = System.Net.WebRequest.Create(url);
                    if (webRequest != null)
                    {
                        webRequest.Method = "DELETE";
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
                        string statusCode = (webRequest.Headers["StatusCode"] != null ? webRequest.Headers["StatusCode"].ToString() : null);
                        string status = (webRequest.Headers["Status"] != null ? webRequest.Headers["Status"].ToString() : null);
                        if (!string.IsNullOrEmpty(statusCode) && (string.IsNullOrEmpty(jsonResponse) || !Common.isJson(jsonResponse)))
                        {
                            jsonResponse = "{\"status\":\"" + statusCode + "\",\"error\":\"" + status + "\"}";
                        }
                        //critical error retry
                        if (Common.isCriticalError(statusCode))
                        {
                            Thread.Sleep(Const.RETRYTIME);
                            reTryCount--;
                        }
                        else
                        {
                            reTryCount = 0;
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

        /// <summary>
        /// Execute PATCH request on hub
        /// </summary>
        private string DoPatchWebRequest(string functionPath, string jsonData, ref string statusCode)
        {
            string jsonResponse = null;
            try
            {
                int reTryCount = 3;
                while (reTryCount > 0)
                {
                    jsonResponse = null;
                    string url = GetUrl(functionPath);
                    Common.FixApiUrl(ref url);

                    HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);

                    Encoding encoding = new UTF8Encoding();

                    string postData = jsonData;

                    byte[] data = encoding.GetBytes(postData);

                    httpWReq.Method = "PATCH";
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
                    statusCode = (response.Headers["StatusCode"] != null ? response.Headers["StatusCode"].ToString() : null);
                    string status = (response.Headers["Status"] != null ? response.Headers["Status"].ToString() : null);
                    if (!string.IsNullOrEmpty(statusCode) && (string.IsNullOrEmpty(jsonResponse) || !Common.isJson(jsonResponse)))
                    {
                        jsonResponse = "{\"status\":\"" + statusCode + "\",\"error\":\"" + status + "\"}";
                    }
                    //critical error retry
                    if (Common.isCriticalError(statusCode))
                    {
                        Thread.Sleep(Const.RETRYTIME);
                        reTryCount--;
                    }
                    else
                    {
                        reTryCount = 0;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                jsonResponse = "{\"error\":\"" + ex.Message + "\"}";
            }
            return jsonResponse;
        }
        /// <summary>
        /// Execute POST request on hub
        /// </summary>
        private string DoPostWebRequest(string functionPath, string jsonData, ref string statusCode)
        {
            string jsonResponse = null;
            try
            {
                int reTryCount = 3;
                while (reTryCount > 0)
                {
                    jsonResponse = null;
                    string url = GetUrl(functionPath);
                    Common.FixApiUrl(ref url);

                    //url = "http://httpstat.us/503";  //simulate 503
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
                    statusCode = (response.Headers["StatusCode"] != null ? response.Headers["StatusCode"].ToString() : null);
                    string status = (response.Headers["Status"] != null ? response.Headers["Status"].ToString() : null);
                    if (!string.IsNullOrEmpty(statusCode) && (string.IsNullOrEmpty(jsonResponse) || !Common.isJson(jsonResponse)))
                    {
                        jsonResponse = "{\"status\":\"" + statusCode + "\",\"error\":\"" + status + "\"}";
                    }
                    //critical error retry
                    if (Common.isCriticalError(statusCode))
                    {
                        Thread.Sleep(Const.RETRYTIME);
                        reTryCount--;
                    }
                    else
                    {
                        reTryCount = 0;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                jsonResponse = "{\"error\":\"" + ex.Message + "\"}";
            }
            return jsonResponse;
        }

        /// <summary>
        /// Execute PUT request on hub
        /// </summary>
        private string DoPutWebRequest(string functionPath, string jsonData, ref string statusCode)
        {
            string jsonResponse = null;
            try
            {
                int reTryCount = 3;
                while (reTryCount > 0)
                {
                    jsonResponse = null;
                    string url = GetUrl(functionPath);
                    Common.FixApiUrl(ref url);

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
                    HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponseWithoutException();
                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonResponse += temp;
                    }
                    statusCode = (response.Headers["StatusCode"] != null ? response.Headers["StatusCode"].ToString() : null);
                    string status = (response.Headers["Status"] != null ? response.Headers["Status"].ToString() : null);
                    if (!string.IsNullOrEmpty(statusCode) && (string.IsNullOrEmpty(jsonResponse) || !Common.isJson(jsonResponse)))
                    {
                        jsonResponse = "{\"status\":\"" + statusCode + "\",\"error\":\"" + status + "\"}";
                    }
                    //critical error retry
                    if (Common.isCriticalError(statusCode))
                    {
                        Thread.Sleep(Const.RETRYTIME);
                        reTryCount--;
                    }
                    else
                    {
                        reTryCount = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                jsonResponse = "{\"error\":\"" + ex.Message + "\"}";
            }
            return jsonResponse;
        }
        /// <summary>
        /// Get Base Url with workspace
        /// </summary>
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
