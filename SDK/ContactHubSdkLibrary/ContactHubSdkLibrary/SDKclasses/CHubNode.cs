using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;


namespace ContactHubSdkLibrary
{
    public class CHubNode
    {
        public bool isValid; //da implementare
        private string _workspaceID = null;
        private string _token = null;
        private string _node = null;
        private const string _baseURL = "https://api.contactlab.it/hub/v1/workspaces/{id-workspace}";

        public object Util { get; private set; }

        #region Node
        public CHubNode(string workspaceID, string token, string node)
        {
            Init(workspaceID, token, node);
        }
        private void Init(string workspaceID, string token, string node)
        {
            _workspaceID = workspaceID;
            _token = token;
            _node = node;
            isValid = true;  //da implementare controllo
        }
        #endregion

        #region extendedProperties

        public string GetExtendedPropertiesConfiguration()
        {
            //            /configuration/properties/extended
            //var returnValue = null;
            string jsonResponse = DoGetWebRequest(String.Format("/configuration/properties/extended"));

            var returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<Customer>(jsonResponse) : null);
            return null;
        }

        public string SetExtendedPropertiesConfiguration()
        {
            //            /configuration/properties/extended
            //var returnValue = null;
            string jsonData = @"{
                        ""membership_card_nr"": {
                            ""description"": ""il numero della membership card"",
                            ""type"": ""number"",
                            ""contactlabProperties"":{
                                          ""label"": ""Membership Card"",
                                          ""mergePolicy"": ""OBJ_PRIORITY"",
                                          ""enabled"": true
                                 }
                           }
                        }";
            string jsonResponse = DoPutWebRequest(String.Format("/configuration/properties/extended"),jsonData);

            var returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<Customer>(jsonResponse) : null);
            return null;
        }



        #endregion

        #region Customers

        public PagedCustomer GetCustomers()
        {
            PagedCustomer returnValue = null;
            string jsonResponse = DoGetWebRequest(String.Format("/customers?nodeId={0}", _node));
            returnValue = JsonConvert.DeserializeObject<PagedCustomer>(jsonResponse);
            return returnValue;
        }

        public Customer AddCustomer(PostCustomer customer)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            string postData = JsonConvert.SerializeObject(customer, settings);

            //ottiene un JObject in modo da poterlo modificare aggiungendoci le extended properties
            JObject o = JObject.Parse(postData);
            //serializza le extendedProperties in modo dinamico 
            string extendedPropertiesData =  ExtendedPropertiesUtil.SerializeExtendedProperties(customer.extended,"extended",customer.GetType());
            JObject extendedProperties = JObject.Parse(extendedPropertiesData);
            //crea il nodo da aggiungere
            JToken jValue = null;
            extendedProperties.TryGetValue("extended", out jValue);
            o.AddFirst(new JProperty("extended", jValue ));
            //ottiene il json finale
            postData = o.ToString();
            string jsonResponse =  DoPostWebRequest("/customers", postData);
            Customer returnCustomer = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Customer>(jsonResponse));
            return returnCustomer;
        }

        public Customer GetCustomer(string id)
        {
           Customer returnValue = null;
            string jsonResponse = DoGetWebRequest(String.Format("/customers/{1}?nodeId={0}", _node,id));

            returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<Customer>(jsonResponse) : null);
            return returnValue;
        }

        public void DeleteCustomer(string id)
        {
            string jsonResponse = DoDeleteWebRequest(String.Format("/customers/{1}?nodeId={0}", _node, id));
        }

        #endregion

        #region Connections
        private string DoGetWebRequest(string functionPath)
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
                jsonResponse="{\"error\":\"" + ex.Message + "\"}";
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

