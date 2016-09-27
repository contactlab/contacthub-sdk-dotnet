using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using System;
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

    public partial class CHubNode
    {
        #region Customers
        /// <summary>
        /// Ottiene la prima pagina della lista customers, da effettuarsi quando si chiamano i customer la prima volta
        /// <summary>
        public bool GetCustomers(ref PagedCustomer pagedCustomer, int pageSize, string externalId, string query, string fields)
        {
            pagedCustomer = null; //lo azzera prima della chiamata in modo da ottenere la pagina 0
            return GetCustomers(ref pagedCustomer, PageRefEnum.first, pageSize, 0, externalId, query, fields);
        }
        /// <summary>
        /// Ottiene le altre pagine customers, da effetuarsi quando si chiamano i customer successivamente alla prima pagina
        /// </summary>
        public bool GetCustomers(ref PagedCustomer pagedCustomer, PageRefEnum page)
        {
            if (pagedCustomer == null)
                return false;
            else
                return GetCustomers(ref pagedCustomer, page, pagedCustomer.page.size, 0, null, null, null);
        }
        /// <summary>
        /// Ottiene le altre pagine customers specificando il n. di pagina esatto
        /// </summary>
        public bool GetCustomers(ref PagedCustomer pagedCustomer, int pageNumber)
        {
            if (pagedCustomer == null)
                return false;
            else
                return GetCustomers(ref pagedCustomer, PageRefEnum.none, pagedCustomer.page.size, pageNumber, null, null, null);
        }

        private bool GetCustomers(ref PagedCustomer pagedCustomer, PageRefEnum page, int pageSize, int pageNumber, string externalId, string query, string fields)
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
                if (pagedCustomer._embedded == null || pagedCustomer._embedded.customers == null) return false;
                return true;
            }
            else if (page != PageRefEnum.none)  //pagine relative first|last|next|prev
            {
                //in questi casi il link contiene anche i filtri applicati precedentemente 
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
                            return false; //ritorna pagina non valida
                        }
                        break;
                    case PageRefEnum.previous:
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
                if (pagedCustomer._embedded == null || pagedCustomer._embedded.customers == null) return false;

                return true;
            }
            else if (page == PageRefEnum.none)
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
                if (pagedCustomer._embedded == null || pagedCustomer._embedded.customers == null) return false;

                return true; //ritorna pagina valida
            }
            return false; //ritorna pagina non valida
        }

        public bool GetCustomers(ref PagedCustomer pagedCustomers, object next)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a new customer (force update if exists)
        /// </summary>
        public Customer AddCustomer(PostCustomer customer, bool forceUpdate = false)
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
            string statusCode = null;
            string jsonResponse = DoPostWebRequest("/customers", postData, ref statusCode);
            Customer returnCustomer = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Customer>(jsonResponse));

            //simula un inserimento fallito, per causa doppioni. Questa funzionalità andrà testata dopo il rilascio di hub di metà ottobre '16, utilizzando l'errore specifico
            //in teoria ritorna l'id dell'customer esistente, che va quindi usato per l'update
            bool isError = (returnCustomer.id == null);
            if (isError && forceUpdate)
            {
                string existingID = "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f";
                jsonResponse = DoPutWebRequest(String.Format("/customers/{0}", existingID), postData, ref statusCode);
            }

            return returnCustomer;
        }
        /// <summary>
        /// Return a customer by internal ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomerByID(string id)
        {
            Customer returnValue = null;
            string jsonResponse = DoGetWebRequest(String.Format("/customers/{1}?nodeId={0}", _node, id));

            returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<Customer>(jsonResponse) : null);
            return returnValue;
        }

        /// <summary>
        /// Return a customer by external ID
        /// </summary>
        /// <param name="externalID"></param>
        /// <returns></returns>
        public Customer GetCustomerByExternalID(string externalID)
        {
            Customer returnValue = null;
            PagedCustomer pagedCustomers = null;
            //richiede i customers filtrati per externalID
            GetCustomers(ref pagedCustomers, 1, externalID, null, null);

            if (pagedCustomers._embedded.customers != null && pagedCustomers._embedded.customers.Count > 0)
            {
                returnValue = pagedCustomers._embedded.customers.First();
            }
            else
            {
                returnValue = null;
            }
            return returnValue;
        }

        /// <summary>
        /// Add a new customer (force update if exists)
        /// </summary>
        public Customer UpdateCustomer(PostCustomer customer, string customerID, bool fullUpdate = false)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(customer, settings);
            string statusCode = null;
            string jsonResponse = null;
            if (fullUpdate)
            {
                //aggiorna tutto il customer intero
                jsonResponse = DoPutWebRequest(String.Format("/customers/{0}", customerID), postData, ref statusCode);
            }
            else
            {
                //aggiorna solo i campi valorizzati del customer
                jsonResponse = DoPatchWebRequest(String.Format("/customers/{0}", customerID), postData, ref statusCode);
            }
            Customer returnCustomer = (jsonResponse == null ? null : JsonConvert.DeserializeObject<Customer>(jsonResponse));
            return returnCustomer;
        }

        public void DeleteCustomer(string id)
        {
            string jsonResponse = DoDeleteWebRequest(String.Format("/customers/{1}?nodeId={0}", _node, id));
        }
        #endregion
    }
}
