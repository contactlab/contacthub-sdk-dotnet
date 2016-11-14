using ContactHubSdkLibrary.Events;
using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace ContactHubSdkLibrary.SDKclasses
{
    public partial class Node
    {
        #region Events

        /// <summary>
        /// Create a new event related to a customer or anonymous
        /// <summary>
        public string AddEvent(PostEvent event_, ref Error error)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string postData = JsonConvert.SerializeObject(event_, settings);
            string statusCode = null;
            string url = "/events";
            string jsonResponse = DoPostWebRequest(url, postData, ref statusCode);
            Common.WriteLog("-> AddEvent() get data:", "querystring:" + url + " data:" + postData);
            Common.WriteLog("<- AddEvent() return data:", jsonResponse);
            error = Common.ResponseIsError(jsonResponse);
            //the json response should contain only the status code because the insertion is async (queue)
            return statusCode;
        }

        /// <summary>
        /// Get the first page of the event list, to be carried out when calling the events the first time
        /// <summary>
        public bool GetEvents(ref PagedEvent pagedEvent, int pageSize,
            string customerID,
            EventTypeEnum? type, EventContextEnum? context, EventModeEnum? mode,
            DateTime? dateFrom, DateTime? dateTo,
            ref Error error)
        {
            pagedEvent = null; //resets before the call in order to get the page 0
            return GetEvents(ref pagedEvent, PageRefEnum.first, pageSize, 0, customerID, type, context, mode, dateFrom, dateTo, ref error);
        }
        /// <summary>
        /// Get other events pages, to be carried out when calling the events subsequent to the first page
        /// </summary>
        public bool GetEvents(ref PagedEvent pagedEvent, PageRefEnum page, ref Error error)
        {
            if (pagedEvent == null)
                return false;
            else
                return GetEvents(ref pagedEvent, page, pagedEvent.page.size, 0, null, null, null, null, null, null, ref error);
        }
        /// <summary>
        /// Get other customers pages specifying the page number
        /// </summary>
        public bool GetEvents(ref PagedEvent pagedEvent, int pageNumber, ref Error error)
        {
            if (pagedEvent == null)
                return false;
            else
                return GetEvents(ref pagedEvent, PageRefEnum.none, pagedEvent.page.size, pageNumber, null, null, null, null, null, null, ref error);
        }

        private bool GetEvents(ref PagedEvent pagedEvent, PageRefEnum page,
            int pageSize,
            int pageNumber,
            string customerID,
            EventTypeEnum? type, EventContextEnum? context, EventModeEnum? mode,
            DateTime? dateFrom, DateTime? dateTo,
            ref Error error
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
                    querySTR += String.Format("&context={0}", WebUtility.UrlEncode(displayValue));
                }
                if (mode != null)
                {
                    string displayValue = ContactHubSdkLibrary.EnumHelper<EventModeEnum>.GetDisplayValue((EventModeEnum)mode);
                    querySTR += String.Format("&mode={0}", WebUtility.UrlEncode(displayValue));
                }
                if (dateFrom != null)
                {
                    string dateStr = Common.ConvertToIso8601Date(((DateTime)dateFrom));
                    querySTR += String.Format("&dateFrom={0}", WebUtility.UrlEncode(dateStr));
                }
                if (dateTo != null)
                {
                    string dateStr = Common.ConvertToIso8601Date(((DateTime)dateTo));
                    querySTR += String.Format("&dateTo={0}", WebUtility.UrlEncode(dateStr));
                }
                querySTR += String.Format("&size={0}", pageSize);
                querySTR += String.Format("&page={0}", 0); //first page

                string jsonResponse = DoGetWebRequest(querySTR);
                Common.WriteLog("-> GetEvents() get data:", "querystring:" + querySTR);
                Common.WriteLog("<- GetEvents() return data:", jsonResponse);

                if (jsonResponse != null)
                {
                    error = Common.ResponseIsError(jsonResponse);
                    if (error == null)
                    {
                        pagedEvent = JsonConvert.DeserializeObject<PagedEvent>(jsonResponse, new EventPropertiesJsonConverter());
                    }
                    else
                    {
                        pagedEvent = null;
                        return false;
                    }
                }
                if (pagedEvent._embedded == null || pagedEvent._embedded.events == null) return false;
                return true;
            }
            else if (page != PageRefEnum.none)  //relative page first|last|next|prev
            {
                //in these cases the link also contains the applied filters previously
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
                            return false; //return not valid page
                        }
                        break;
                    case PageRefEnum.previous:
                        if (pagedEvent._links.prev != null)
                        {
                            otherPageUrl = pagedEvent._links.prev.href;
                        }
                        else
                        {
                            return false; //return not valid page
                        }
                        break;
                    default:
                        otherPageUrl = pagedEvent._links.first.href;
                        break;
                }
                //calls the link that represents the other page, as previously returned by ContactLab
                string jsonResponse = DoGetWebRequest(otherPageUrl, false);
                Common.WriteLog("-> GetEvents() get data:", "querystring:" + otherPageUrl);
                Common.WriteLog("<- GetEvents() return data:", jsonResponse);

                if (jsonResponse != null)
                {
                    error = Common.ResponseIsError(jsonResponse);
                    if (error == null)
                    {
                        pagedEvent = JsonConvert.DeserializeObject<PagedEvent>(jsonResponse);
                    }
                    else
                    {
                        pagedEvent = null;
                        return false;
                    }
                }
                if (pagedEvent._embedded == null || pagedEvent._embedded.events == null) return false;
                return true;
            }
            else if (page == PageRefEnum.none)
            {
                //this is a specific page number. You can get specific page link replacing page number from 'self' link.
                string currentUrl = pagedEvent._links.self.href;

                //if page number is not valid, return current pagecustomer with error
                if (pageNumber < 0 || pageNumber >= pagedEvent.page.totalPages)
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

                return true; //return valid page
            }
            return false; //return invalid page
        }

        /// <summary>
        /// Custom deserializer for events. ReadJson() detect right class for 'properties' attribute.
        /// The GetEventProperties method is self-generated from generateBasePropertiesClass projects.
        /// Supported classes are self-generated from configuration json schema in the file eventPropertiesClass.cs 
        /// </summary>
        public class EventPropertiesJsonConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(Event));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JObject jo = JObject.Load(reader);
                //verify the type
                _Event castEvent = jo.ToObject<_Event>(serializer);
                //force de-serialization of the properties for specific model, based on the event type
                castEvent.properties = (EventBaseProperty)EventPropertiesUtil.GetEventProperties(jo, serializer);
                //force de-serialization of the context properties for specific model, based on the context type
                castEvent.contextInfo = (EventBaseProperty)EventPropertiesContextUtil.GetEventContext(jo, serializer);
                return castEvent;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// Get single event by id
        /// </summary>
        public Event GetEvent(string id, ref Error error)
        {
            Event returnValue = null;
            string url = String.Format("/events/{1}?nodeId={0}", _node, id);
            string jsonResponse = DoGetWebRequest(url);
            Common.WriteLog("-> GetEvents() get data:", "querystring:" + url);
            Common.WriteLog("<- GetEvents() return data:", jsonResponse);
            error = Common.ResponseIsError(jsonResponse);
            if (error == null)
            {
                returnValue = (jsonResponse != null ? JsonConvert.DeserializeObject<Event>(jsonResponse, new EventPropertiesJsonConverter()) : null);
            }
            else
            {
                returnValue = null;
            }
            return returnValue;
        }
        #endregion


        //public void DeleteEvent(string id)
        //{
        //    string jsonResponse = DoDeleteWebRequest(String.Format("/events/{1}?nodeId={0}", _node, id));
        //Common.WriteLog("-> AddEvent() get data:", "querystring:" + url + " data:" +postData);
        //    Common.WriteLog("<- AddEvent() return data:", jsonResponse);

        //}

    }
}
