﻿using ContactHubSdkLibrary.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ContactHubSdkLibrary.Models
{
    public enum EventModeEnum
    {
        ACTIVE,
        PASSIVE
    }

    public enum BringBackPropertyTypeEnum
    {
        [Display(Name = "SESSION_ID")]
        SESSION_ID,
        [Display(Name = "EXTERNAL_ID")]
        EXTERNAL_ID,
        [Display(Name = "NoValue")]
        NoValue
    }
    public class BringBackProperty
    {
        [JsonProperty("type")]
        public string _type { get; set; }
        [JsonProperty("_type")]
        [JsonIgnore]
        public BringBackPropertyTypeEnum type
        {
            get
            {
                BringBackPropertyTypeEnum enumValue = ContactHubSdkLibrary.EnumHelper<BringBackPropertyTypeEnum>.GetValueFromDisplayName(_type);
                return enumValue;
            }
            set
            {
                var displayValue = ContactHubSdkLibrary.EnumHelper<BringBackPropertyTypeEnum>.GetDisplayValue(value);
                _type = (displayValue == "NoValue" ? null : displayValue);
            }
        }
        public string value { get; set; }
        public string nodeId { get; set; }

    }
    public class PostEvent
    {
        public string customerId { get; set; }
        public BringBackProperty bringBackProperties { get; set; }
        [JsonProperty("type")]
        public string _type { get; set; }
        [JsonProperty("_type")]
        [JsonIgnore]
        public EventTypeEnum type
        {
            get
            {
                EventTypeEnum enumValue = ContactHubSdkLibrary.EnumHelper<EventTypeEnum>.GetValueFromDisplayName(_type);
                return enumValue;
            }
            set
            {
                var displayValue = ContactHubSdkLibrary.EnumHelper<EventTypeEnum>.GetDisplayValue(value);
                _type = (displayValue == "NoValue" ? null : displayValue);
            }
        }
        [JsonProperty("context")]
        public string _context { get; set; }
        [JsonProperty("_context")]
        [JsonIgnore]
        public EventContextEnum context
        {
            get
            {
                EventContextEnum enumValue = ContactHubSdkLibrary.EnumHelper<EventContextEnum>.GetValueFromDisplayName(_context);
                return enumValue;
            }
            set
            {
                var displayValue = ContactHubSdkLibrary.EnumHelper<EventContextEnum>.GetDisplayValue(value);
                _context = (displayValue == "NoValue" ? null : displayValue);
            }
        }

        public TrackingProperties tracking { get; set; }
        public EventBaseProperty properties { get; set; }
        public EventBaseProperty contextInfo { get; set; }
        [JsonProperty("date")]
        public string _date { get; set; }
        [JsonProperty("_date")]
        [JsonIgnore]
        public DateTime date
        {
            //format:  2016-09-15T08:41:20.224+0000
            //get
            //{
            //    string currentValue = _date;
            //    if (currentValue == null)
            //    {
            //        return DateTime.MinValue;
            //    }
            //    else
            //    {
            //        return Convert.ToDateTime(_date, new CultureInfo("en-US")).ToUniversalTime();
            //    }
            //}
            //set
            //{
            //    try
            //    {
            //        _date = value.ToString("yyyy-MM-dd");
            //    }
            //    catch { _date = null; }
            //}
            get
            {
                string currentValue = _date;
                if (currentValue != null)
                {
                    if (!_date.Contains("T"))
                    {
                        return
                             //DateTime.ParseExact(_date,
                             //              "MM/dd/yyyy HH:mm:ss",
                             //              CultureInfo.InvariantCulture,
                             //              DateTimeStyles.AssumeUniversal |
                             //              DateTimeStyles.AdjustToUniversal);
                             Convert.ToDateTime(_date, new CultureInfo("en-US")).ToUniversalTime();
                    }
                    else
                    {
                        return
                            DateTime.ParseExact(_date,
                                          "yyyy-MM-dd'T'HH:mm:ss'Z'",
                                          CultureInfo.InvariantCulture,
                                          DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal);
                    }
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                try
                {
                    _date = value.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                }
                catch { _date = null; }
            }
        }
    }
    public class PagedEvent
    {
        // public EmbeddedEvents _embedded { get; set; }
        public List<Event> elements;
        //    public PageLink _links { get; set; }
        public Page page { get; set; }
        [JsonIgnore]
        public PagedEventFilter filter { get; set; }  //query string for relative paging
    }

    public class PagedEventFilter
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public string customerID { get; set; }
        public EventTypeEnum? type { get; set; }
        public EventContextEnum? context { get; set; }
        public EventModeEnum? mode { get; set; }
        public DateTime? dateFrom { get; set; }
        public DateTime? dateTo { get; set; }
    }


}
