using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class CHubNode
    {
        #region Events

        /// <summary>
        /// Crea un nuovo evento legato a un customer o anonimo
        /// <summary>
        public string AddEvent(PostEvent event_)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(event_, settings);
            string statusCode = null;
            string jsonResponse = DoPostWebRequest("/events", postData,ref statusCode);
            //la json response dovrebbe contenere in questo caso solo la status code perchè l'inserimento è asyncrono (coda)
            return statusCode;
        }

        /// <summary>
        /// Ottiene la prima pagina della lista events, da effettuarsi quando si chiamano gli events la prima volta
        /// <summary>
        public bool GetEvents(ref PagedEvent pagedEvent, int pageSize,
            string customerID,
            EventTypeEnum? type, EventContextEnum? context, EventModeEnum? mode,
            DateTime? dateFrom, DateTime? dateTo)
        {
            pagedEvent = null; //lo azzera prima della chiamata in modo da ottenere la pagina 0
            return GetEvents(ref pagedEvent, PageRefEnum.first, pageSize, 0, customerID, type, context, mode, dateFrom, dateTo);
        }
        /// <summary>
        /// Ottiene le altre pagine events, da effetuarsi quando si chiamano gli events successivamente alla prima pagina
        /// </summary>
        public bool GetEvents(ref PagedEvent pagedEvent, PageRefEnum page)
        {
            if (pagedEvent == null)
                return false;
            else
                // return GetCustomers(ref pagedCustomer, page, pagedCustomer.page.size, 0, null, null, null);
                return GetEvents(ref pagedEvent, page, pagedEvent.page.size, 0, null, null, null, null, null, null);
        }
        /// <summary>
        /// Ottiene le altre pagine customers specificando il n. di pagina esatto
        /// </summary>
        public bool GetEvents(ref PagedEvent pagedEvent, int pageNumber)
        {
            if (pagedEvent == null)
                return false;
            else
                return GetEvents(ref pagedEvent, PageRefEnum.none, pagedEvent.page.size, pageNumber, null, null, null, null, null, null);
        }

        private bool GetEvents(ref PagedEvent pagedEvent, PageRefEnum page,
            int pageSize,
            int pageNumber,
            string customerID,
            EventTypeEnum? type, EventContextEnum? context, EventModeEnum? mode,
            DateTime? dateFrom, DateTime? dateTo
            )
        {
            /* parameters:        
            - customerId: The unique identifier of workspace (REQUIRED) 
            - type: The name of type event
            - context: the name of context event
            - mode: the mode of event. ACTIVE if the customer made the event, PASSIVE if the customer recive the event
            - dateFrom: From datetime for search of event
            - dateTo: To datetime for search of event
            */

            if (pagedEvent == null && page == PageRefEnum.first)
            {
                string querySTR = String.Format("/events?nodeId={0}", _node);
                /* crea la stringa */
                if (!string.IsNullOrEmpty(customerID))
                {
                    querySTR += String.Format("&customerId={0}", WebUtility.UrlEncode(customerID));
                }
                if (type != null)
                {
                    string displayValue = ContactHubSdkLibrary.EnumHelper<EventTypeEnum>.GetDisplayValue((EventTypeEnum)type);
                    querySTR += String.Format("&type={0}", WebUtility.UrlEncode(displayValue));
                }
                if (context != null)
                {
                    string displayValue = ContactHubSdkLibrary.EnumHelper<EventContextEnum>.GetDisplayValue((EventContextEnum)context);
                    querySTR += String.Format("&type={0}", WebUtility.UrlEncode(displayValue));
                }
                if (mode != null)
                {
                    string displayValue = ContactHubSdkLibrary.EnumHelper<EventModeEnum>.GetDisplayValue((EventModeEnum)mode);
                    querySTR += String.Format("&mode={0}", WebUtility.UrlEncode(displayValue));
                }
                if (dateFrom != null)
                {
                    string dateStr = ((DateTime)dateFrom).ToString("o");//.ToString("yyyy-MM-dd");
                    querySTR += String.Format("&dateFrom={0}", WebUtility.UrlEncode(dateStr));
                }
                if (dateTo != null)
                {
                    string dateStr = ((DateTime)dateTo).ToString("o");//.ToString("yyyy-MM-dd");
                    querySTR += String.Format("&dateTo={0}", WebUtility.UrlEncode(dateStr));
                }
                querySTR += String.Format("&size={0}", pageSize);
                querySTR += String.Format("&page={0}", 0); //first page

                string jsonResponse = DoGetWebRequest(querySTR);
                if (jsonResponse != null)
                {
                    pagedEvent = JsonConvert.DeserializeObject<PagedEvent>(jsonResponse);
                }
                return true;
            }
            else if (page != PageRefEnum.none)  //pagine relative first|last|next|prev
            {
                //in questi casi il link contiene anche i filtri applicati precedentemente 
                string otherPageUrl = null;
                switch (page)
                {
                    case PageRefEnum.first:
                        otherPageUrl = pagedEvent._links.first.href;
                        break;
                    case PageRefEnum.last:
                        otherPageUrl = pagedEvent._links.last.href;
                        break;
                    case PageRefEnum.next:
                        if (pagedEvent._links.next != null)
                        {
                            otherPageUrl = pagedEvent._links.next.href;
                        }
                        else
                        {
                            return false; //ritorna pagina non valida
                        }
                        break;
                    case PageRefEnum.previous:
                        if (pagedEvent._links.prev != null)
                        {
                            otherPageUrl = pagedEvent._links.prev.href;
                        }
                        else
                        {
                            return false; //ritorna pagina non valida
                        }
                        break;
                    default:
                        otherPageUrl = pagedEvent._links.first.href;
                        break;
                }
                //chiama il link che rappresenta l'altra pagina, così come restituito precedentemente dal contactlab
                string jsonResponse = DoGetWebRequest(otherPageUrl, false);
                if (jsonResponse != null)
                {
                    pagedEvent = JsonConvert.DeserializeObject<PagedEvent>(jsonResponse);
                }
                return true;
            }
            else if (page == PageRefEnum.none)
            {
                //è un page number specifico. Può ottenere il link semplicemente sostituendo il page number da link 'self'
                string currentUrl = pagedEvent._links.self.href;

                //se il page number non è valido, restituisce la pagecustomer corrente e un errore
                if (pageNumber < 0 || pageNumber >= pagedEvent.page.totalPages)
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

        /// <summary>
        /// Get single event by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Event GetEvent(string id)
        {
            Event returnValue = null;
            string jsonResponse = DoGetWebRequest(String.Format("/events/{1}?nodeId={0}", _node, id));
            returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<Event>(jsonResponse) : null);
            return returnValue;
        }
        //public void DeleteEvent(string id)
        //{
        //    string jsonResponse = DoDeleteWebRequest(String.Format("/events/{1}?nodeId={0}", _node, id));
        //}
        #endregion
    }
}
