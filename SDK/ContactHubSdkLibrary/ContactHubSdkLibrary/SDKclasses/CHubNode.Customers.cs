using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Net;

namespace ContactHubSdkLibrary.SDKclasses
{
    public enum CustomersPage
    {
        first,
        last,
        next,
        previous,
        none
    }

    public partial class CHubNode
    {
        #region Customers

        //chiamata per ottenere la prima pagina, da effettuarsi quando si chiamano i customer la prima volta
        public bool GetCustomers(ref PagedCustomer pagedCustomer, int pageSize, string externalId, string query, string fields)
        {
            pagedCustomer = null; //lo azzera prima della chiamata in modo da ottenere la pagina 0
            return GetCustomers(ref pagedCustomer, CustomersPage.first, pageSize, 0, externalId, query, fields);
        }
        //chiamata per ottenere le altre pagine, da effetuarsi quando si chiamano i customer successivamente alla prima pagina
        public bool GetCustomers(ref PagedCustomer pagedCustomer, CustomersPage page)
        {
            if (pagedCustomer == null)
                return false;
            else
                return GetCustomers(ref pagedCustomer, page, pagedCustomer.page.size, 0, null, null, null);
        }
        //chiamata per ottenere le altre pagine specificando il n. di pagina esatto
        public bool GetCustomers(ref PagedCustomer pagedCustomer, int pageNumber)
        {
            if (pagedCustomer == null)
                return false;
            else
                return GetCustomers(ref pagedCustomer, CustomersPage.none, pagedCustomer.page.size, pageNumber, null, null, null);
        }
        private bool GetCustomers(ref PagedCustomer pagedCustomer, CustomersPage page, int pageSize, int pageNumber, string externalId, string query, string fields)
        {
            /* parameters:            
             * nodeId: Identifier of the node where you want to do the search  Required:Yes   (
             * externaId: The external id assigned to the customers Required: No
             * query: Query for filter the customers Required: No	
             * fields: Comma - separated list of properties to include in the response  Required:No
             */

            if (pagedCustomer == null && page == CustomersPage.first)
            {
                string querySTR = String.Format("/customers?nodeId={0}", _node);
                /* crea la stringa */
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
                if (jsonResponse != null)
                {
                    pagedCustomer = JsonConvert.DeserializeObject<PagedCustomer>(jsonResponse);
                }
                return true;
            }
            else if (page != CustomersPage.none)  //pagine relative first|last|next|prev
            {
                //in questi casi il link contiene anche i filtri applicati precedentemente 
                string otherPageUrl = null;
                switch (page)
                {
                    case CustomersPage.first:
                        otherPageUrl = pagedCustomer._links.first.href;
                        break;
                    case CustomersPage.last:
                        otherPageUrl = pagedCustomer._links.last.href;
                        break;
                    case CustomersPage.next:
                        if (pagedCustomer._links.next != null)
                        {
                            otherPageUrl = pagedCustomer._links.next.href;
                        }
                        else
                        {
                            return false; //ritorna pagina non valida
                        }
                        break;
                    case CustomersPage.previous:
                        if (pagedCustomer._links.prev != null)
                        {
                            otherPageUrl = pagedCustomer._links.prev.href;
                        }
                        else
                        {
                            return false; //ritorna pagina non valida
                        }
                        break;
                    default:
                        otherPageUrl = pagedCustomer._links.first.href;
                        break;
                }
                //chiama il link che rappresenta l'altra pagina, così come restituito precedentemente dal contactlab
                string jsonResponse = DoGetWebRequest(otherPageUrl, false);
                if (jsonResponse != null)
                {
                    pagedCustomer = JsonConvert.DeserializeObject<PagedCustomer>(jsonResponse);
                }
                return true;
            }
            else if (page == CustomersPage.none)
            {
                //è un page number specifico. Può ottenere il link semplicemente sostituendo il page number da link 'self'
                string currentUrl = pagedCustomer._links.self.href;

                //se il page number non è valido, restituisce la pagecustomer corrente e un errore
                if (pageNumber < 0 || pageNumber >= pagedCustomer.page.totalPages)
                {
                    return false; //ritorna pagina non valida
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
                return true; //ritorna pagina valida
            }
            return false; //ritorna pagina non valida
        }
        public Customer AddCustomer(PostCustomer customer)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            string postData = JsonConvert.SerializeObject(customer, settings);

            //serializza le extendedProperties in modo dinamico 
            //string extendedPropertiesData =  ExtendedPropertiesUtil.SerializeExtendedProperties(customer.extended,"extended",customer.GetType());
            //if (!string.IsNullOrEmpty(extendedPropertiesData))
            //{
            //    //ottiene un JObject in modo da poterlo modificare aggiungendoci le extended properties
            //    JObject o = JObject.Parse(postData);
            //    JObject extendedProperties = JObject.Parse(extendedPropertiesData);
            //    //crea il nodo da aggiungere
            //    JToken jValue = null;
            //    extendedProperties.TryGetValue("extended", out jValue);
            //    o.AddFirst(new JProperty("extended", jValue));

            //    //ottiene il json finale
            //    postData = o.ToString();
            //}

            string jsonResponse = DoPostWebRequest("/customers", postData);
            Customer returnCustomer = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Customer>(jsonResponse));
            return returnCustomer;
        }
        public Customer GetCustomer(string id)
        {
            Customer returnValue = null;
            string jsonResponse = DoGetWebRequest(String.Format("/customers/{1}?nodeId={0}", _node, id));

            returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<Customer>(jsonResponse) : null);
            return returnValue;
        }
        public void DeleteCustomer(string id)
        {
            string jsonResponse = DoDeleteWebRequest(String.Format("/customers/{1}?nodeId={0}", _node, id));
        }
        #endregion
    }
}
