using ContactHubSdkLibrary.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ContactHubSdkLibrary.Models
{
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

        public List<EventBaseProperty> properties { get; set; }
        // public object contextInfo { get; set; }   //DA COMPLETARE DOPO IL 19
        [JsonProperty("date")]
        public string _date { get; set; }
        [JsonProperty("_date")]
        [JsonIgnore]
        public DateTime date
        {
            get
            {
                if (_date != null)
                {
                    return
                         DateTime.ParseExact(_date,
                                       "yyyy-MM-dd'T'HH:mm:ss'Z'",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal);
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
}
