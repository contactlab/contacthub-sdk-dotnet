using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ContactHubSdkLibrary.SDKclasses
{
    public enum PageRefEnum
    {
        first,
        last,
        next,
        previous,
        none
    }

    public partial class Node
    {
        #region Customers
        /// <summary>
        /// Get the first page of customers list, to be made when you call the customer first
        /// <summary>
        public bool GetCustomers(ref PagedCustomer pagedCustomer, int pageSize, string externalId, string query, string fields, ref Error error)
        {
            pagedCustomer = null; //resets before the call in order to get the page 0
            return GetCustomers(ref pagedCustomer, PageRefEnum.first, pageSize, 0, externalId, query, fields, ref error);
        }
        /// <summary>
        /// Get other customers pages, to be made when you call the customer after the first page
        /// </summary>
        public bool GetCustomers(ref PagedCustomer pagedCustomer, PageRefEnum page, ref Error error)
        {
            if (pagedCustomer == null)
                return false;
            else
                return GetCustomers(ref pagedCustomer, page, pagedCustomer.page.size, 0, null, null, null, ref error);
        }
        /// <summary>
        /// Get other customers pages specifying the  page number
        /// </summary>
        public bool GetCustomers(ref PagedCustomer pagedCustomer, int pageNumber, ref Error error)
        {
            if (pagedCustomer == null)
                return false;
            else
                return GetCustomers(ref pagedCustomer, PageRefEnum.none, pagedCustomer.page.size, pageNumber, null, null, null, ref error);
        }

        private bool GetCustomers(ref PagedCustomer pagedCustomer, PageRefEnum page, int pageSize, int pageNumber, string externalId, string query, string fields, ref Error error)
        {
            /* parameters:            
             * nodeId: Identifier of the node where you want to do the search  Required:Yes   (
             * externaId: The external id assigned to the customers Required: No
             * query: Query for filter the customers Required: No	
             * fields: Comma - separated list of properties to include in the response  Required:No
             */

            if (pagedCustomer == null && page == PageRefEnum.first)
            {
                string querySTR = String.Format("/customers?nodeId={0}", _node);
                if (!string.IsNullOrEmpty(externalId))
                {
                    querySTR += String.Format("&externalId={0}", WebUtility.UrlEncode(externalId));
                }
                if (!string.IsNullOrEmpty(query))
                {
                    querySTR += String.Format("&query={0}", WebUtility.UrlEncode(query));
                }
                if (!string.IsNullOrEmpty(fields))
                {
                    querySTR += String.Format("&fields={0}", WebUtility.UrlEncode(fields));
                }
                querySTR += String.Format("&size={0}", pageSize);
                querySTR += String.Format("&page={0}", 0); //first page
                string jsonResponse = DoGetWebRequest(querySTR);
                Common.WriteLog("-> GetCustomers() get data:", "querystring:" + querySTR);
                Common.WriteLog("<- GetCustomers() return data:", jsonResponse);
                if (jsonResponse != null)
                {
                    error = Common.ResponseIsError(jsonResponse);
                    if (error == null)
                    {
                        pagedCustomer = JsonConvert.DeserializeObject<PagedCustomer>(jsonResponse);
                    }
                    else
                    {
                        pagedCustomer = null;
                        return false;
                    }
                }
                if (pagedCustomer._embedded == null || pagedCustomer._embedded.customers == null) return false;

                return true;
            }
            else if (page != PageRefEnum.none)  //relative page first|last|next|prev
            {
                //in these cases the link also contains the applied filters previously
                string otherPageUrl = null;
                switch (page)
                {
                    case PageRefEnum.first:
                        otherPageUrl = pagedCustomer._links.first.href;
                        break;
                    case PageRefEnum.last:
                        otherPageUrl = pagedCustomer._links.last.href;
                        break;
                    case PageRefEnum.next:
                        if (pagedCustomer._links.next != null)
                        {
                            otherPageUrl = pagedCustomer._links.next.href;
                        }
                        else
                        {
                            return false; //return invalid page
                        }
                        break;
                    case PageRefEnum.previous:
                        if (pagedCustomer._links.prev != null)
                        {
                            otherPageUrl = pagedCustomer._links.prev.href;
                        }
                        else
                        {
                            return false; //return invalid page
                        }
                        break;
                    default:
                        otherPageUrl = pagedCustomer._links.first.href;
                        break;
                }
                //calls the link that represents the other page, as previously returned by Contacthub
                string jsonResponse = DoGetWebRequest(otherPageUrl, false);
                Common.WriteLog("-> GetCustomers() get data:", "querystring:" + otherPageUrl);
                Common.WriteLog("<- GetCustomers() return data:", jsonResponse);
                if (jsonResponse != null)
                {
                    error = Common.ResponseIsError(jsonResponse);
                    if (error == null)
                    {
                        pagedCustomer = JsonConvert.DeserializeObject<PagedCustomer>(jsonResponse);
                    }
                    else
                    {
                        pagedCustomer = null;
                        return false;
                    }
                }
                if (pagedCustomer._embedded == null || pagedCustomer._embedded.customers == null) return false;

                return true;
            }
            else if (page == PageRefEnum.none)
            {
                //is a specific page number. You can get the link simply by replacing the page number from 'self' link
                string currentUrl = pagedCustomer._links.self.href;

                //if the page number is invalid, the page returns an error and the current customer
                if (pageNumber < 0 || pageNumber >= pagedCustomer.page.totalPages)
                {
                    return false; //return invalid page
                }

                Uri currentUri = new Uri(currentUrl);
                string currentQuery = currentUri.Query;
                if (currentQuery.StartsWith("?"))
                {
                    currentQuery = currentQuery.Substring(1);
                }
                string[] currentParameters = currentQuery.Split('&');
                currentQuery = "";
                bool isFirst = true;
                foreach (string param in currentParameters)
                {
                    if (!param.StartsWith("page="))
                    {
                        currentQuery += String.Format("{0}{1}", (isFirst ? "?" : "&"), param);
                    }
                    else
                    {
                        currentQuery += String.Format("{0}{1}{2}", (isFirst ? "?" : "&"), "page=", pageNumber);
                    }
                    isFirst = false;
                }
                if (pagedCustomer._embedded == null || pagedCustomer._embedded.customers == null) return false;

                return true; //return valid page
            }
            return false; //return invalid page
        }


        /// <summary>
        /// Add a new customer (force update if exists)
        /// </summary>
        public Customer AddCustomer(PostCustomer customer, ref Error error, bool forceUpdate = false)
        {
            if (string.IsNullOrEmpty(customer.nodeId))
            {
                customer.nodeId = this._node;
            }
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            string postData = JsonConvert.SerializeObject(customer, settings);

            string statusCode = null;
            string jsonResponse = DoPostWebRequest("/customers", postData, ref statusCode);
            Common.WriteLog("-> Addcustomer() post data:", postData);
            Common.WriteLog("<- Addcustomer() return data:", jsonResponse);
            Customer returnCustomer = null;
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                returnCustomer = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Customer>(jsonResponse));
            }
            else
            {
                returnCustomer = null;
            }
            bool isError = (returnCustomer == null || returnCustomer.id == null);


            //if add failed due conflict (duplication), try to update the customer
            if (isError && forceUpdate && statusCode != null && statusCode.ToLowerInvariant().Contains("(409)"))
            {
                //trasforma postcustomer in customer
                string[] links = error._links.customer.href.Split('/');
                string existingID = links[links.Length - 1];
                Customer c = Common.CreateObject<Customer>(customer);
                c.id = existingID;
                postData = JsonConvert.SerializeObject(c, settings);

                string url = String.Format("/customers/{0}", existingID);
                jsonResponse = DoPutWebRequest(url, postData, ref statusCode);
                Common.WriteLog("-> Addcustomer() put data:", "querystring:" + url + " data:" + postData);
                Common.WriteLog("<- Addcustomer() return data:", jsonResponse);

                error = Common.ResponseIsError(jsonResponse);
                //if (error == null)
                //{
                //}

            }

            return returnCustomer;
        }
        /// <summary>
        /// Return a customer by internal ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomerByID(string id, ref Error error)
        {
            Customer returnValue = null;
            string queryString = String.Format("/customers/{1}?nodeId={0}", _node, id);
            string jsonResponse = DoGetWebRequest(queryString);
            Common.WriteLog("-> GetCustomerByID() get data:", "querystring:" + queryString);
            Common.WriteLog("<- GetCustomerByID() return data:", jsonResponse);
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<Customer>(jsonResponse) : null);
            }
            else
            {
                return null;
            }
            //The function takes a customer from any node, using his unique id. To avoid reading a customer belonging to another node by his customer ID, 
            //the system checks if the client nodeID returned corresponds to the current nodeID
            if (returnValue != null && returnValue.nodeId != this.id)
            {
                returnValue = null;
            }
            return returnValue;
        }

        /// <summary>
        /// Return a customer by external ID
        /// </summary>
        /// <param name="externalID"></param>
        /// <returns></returns>
        public List<Customer> GetCustomerByExternalID(string externalID, ref Error error)
        {
            List<Customer> returnValue = null;
            PagedCustomer pagedCustomers = null;
            //get customers filtered by external ID
            GetCustomers(ref pagedCustomers, 1, externalID, null, null, ref error);

            if (pagedCustomers._embedded != null && pagedCustomers._embedded.customers != null && pagedCustomers._embedded.customers.Count > 0)
            {
                returnValue = pagedCustomers._embedded.customers;
            }
            else
            {
                returnValue = null;
            }
            return returnValue;
        }

        /// <summary>
        /// Update customer 
        /// </summary>
        public Customer UpdateCustomer(PostCustomer customer, string customerID, ref Error error, bool fullUpdate = false)
        {
            Customer returnCustomer = null;
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };


            string postData = null;
            if (fullUpdate == false) //partial
            {
                postData = JsonConvert.SerializeObject(customer, settings);
            }
            else //full
            {
                //if full update, post model is different, require ID
                Customer c = Common.CreateObject<Customer>(customer);
                c.id = customerID;
                postData = JsonConvert.SerializeObject(c, settings);
            }
            string statusCode = null;
            string jsonResponse = null;
            if (fullUpdate)
            {
                //update the entire customer
                string queryString = String.Format("/customers/{0}", customerID);
                jsonResponse = DoPutWebRequest(queryString, postData, ref statusCode);
                Common.WriteLog("-> UpdateCustomer() put data:", "querystring:" + queryString + " " + "data:" + postData);
                Common.WriteLog("<- UpdateCustomer() return data:", jsonResponse);
            }
            else
            {
                //upgrade only those valued fields of customer
                string queryString = String.Format("/customers/{0}", customerID);
                jsonResponse = DoPatchWebRequest(queryString, postData, ref statusCode);
                Common.WriteLog("-> UpdateCustomer() patch data:", "querystring:" + queryString + " " + "data:" + postData);
                Common.WriteLog("<- UpdateCustomer() return data:", jsonResponse);
            }

            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                returnCustomer = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Customer>(jsonResponse));
            }
            else
            {
                returnCustomer = null;
            }
            return returnCustomer;
        }

        /// <summary>
        /// Delete Customer from node
        /// </summary>
        public bool DeleteCustomer(string id, ref Error error)
        {
            string queryString = String.Format("/customers/{1}?nodeId={0}", _node, id);
            string jsonResponse = DoDeleteWebRequest(queryString);
            Common.WriteLog("-> DeleteCustomer() delete data:", "querystring:" + queryString + " ");

            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                if (string.IsNullOrEmpty(jsonResponse))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
