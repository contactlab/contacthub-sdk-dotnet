/* selfgenerated from version 0.0.0.1 26/01/2017 16:11:06 */

using ContactHubSdkLibrary.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
namespace ContactHubSdkLibrary.Events
{
    //context class 'CONTACT_CENTER': CONTACT_CENTER
    public class EventContextPropertyCONTACT_CENTER : EventBaseProperty
    {
        public Client client { get; set; }
    }


    public class Client
    {
        public string userAgent { get; set; }
        //format: ipv4
        public string ip { get; set; }
    }


    //context class 'WEB': WEB
    public class EventContextPropertyWEB : EventBaseProperty
    {
        public Client client { get; set; }
    }


    //context class 'MOBILE': MOBILE
    public class EventContextPropertyMOBILE : EventBaseProperty
    {
        public Client client { get; set; }
        public Device device { get; set; }
    }


    public class Device
    {
        public string bundleIdentifier { get; set; }
        public string versionNumber { get; set; }
        public string buildNumber { get; set; }
        public string identifierForVendor { get; set; }
        public string systemVersion { get; set; }
        public string model { get; set; }
        public string deviceVendor { get; set; }
        public string locale { get; set; }
        public string language { get; set; }
    }


    //context class 'ECOMMERCE': ECOMMERCE
    public class EventContextPropertyECOMMERCE : EventBaseProperty
    {
        public Client client { get; set; }
        public Store store { get; set; }
    }


    public class Store
    {
        public string id { get; set; }
        public string name { get; set; }
        [JsonProperty("type")]
        public string _type { get; set; }
        [JsonProperty("hidden_type")]
        [JsonIgnore]
        public StoreTypeEnum type
        {
            get
            {
                StoreTypeEnum enumValue = ContactHubSdkLibrary.EnumHelper<StoreTypeEnum>.GetValueFromDisplayName(_type);
                return enumValue;
            }
            set
            {
                var displayValue = ContactHubSdkLibrary.EnumHelper<StoreTypeEnum>.GetDisplayValue(value);
                _type = (displayValue == "NoValue" ? null : displayValue);
            }
        }
        public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string zip { get; set; }
        public Geo geo { get; set; }
        public string website { get; set; }
    }

    public enum StoreTypeEnum
    {
        NoValue,
        [Display(Name = "AIRPORT")]
        AIRPORT,
        [Display(Name = "ECOMMERCE")]
        ECOMMERCE,
        [Display(Name = "FLAGSHIP")]
        FLAGSHIP,
        [Display(Name = "FREE-STANDING")]
        FREEMinusSTANDING,
        [Display(Name = "MALL")]
        MALL,
        [Display(Name = "OUTLET")]
        OUTLET,
        [Display(Name = "RESORT")]
        RESORT,
        [Display(Name = "SIS")]
        SIS,
        [Display(Name = "WAREHOUSE")]
        WAREHOUSE,
        [Display(Name = "NOT-DEFINED")]
        NOTMinusDEFINED
    }
    //context class 'RETAIL': RETAIL
    public class EventContextPropertyRETAIL : EventBaseProperty
    {
        public Client client { get; set; }
        public Store store { get; set; }
    }


    //context class 'IOT': IOT
    public class EventContextPropertyIOT : EventBaseProperty
    {
        public Client client { get; set; }
    }


    //context class 'SOCIAL': SOCIAL
    public class EventContextPropertySOCIAL : EventBaseProperty
    {
        public Client client { get; set; }
    }


    //context class 'DIGITAL_CAMPAIGN': DIGITAL_CAMPAIGN
    public class EventContextPropertyDIGITAL_CAMPAIGN : EventBaseProperty
    {
        public Client client { get; set; }
    }


    //context class 'OTHER': OTHER
    public class EventContextPropertyOTHER : EventBaseProperty
    {
        public Client client { get; set; }
    }



}
public enum EventContextEnum
{
    NoValue,
    [Display(Name = "CONTACT_CENTER")]
    CONTACTCENTER,
    [Display(Name = "WEB")]
    WEB,
    [Display(Name = "MOBILE")]
    MOBILE,
    [Display(Name = "ECOMMERCE")]
    ECOMMERCE,
    [Display(Name = "RETAIL")]
    RETAIL,
    [Display(Name = "IOT")]
    IOT,
    [Display(Name = "SOCIAL")]
    SOCIAL,
    [Display(Name = "DIGITAL_CAMPAIGN")]
    DIGITALCAMPAIGN,
    [Display(Name = "OTHER")]
    OTHER
}
public static class EventPropertiesContextUtil
{
    /// <summary>
    /// Return events context properties with right cast, event type based
    /// </summary>

    public static object GetEventContext(JObject jo, JsonSerializer serializer)
    {
        var typeName = jo["context"].ToString().ToLowerInvariant();
        switch (typeName)
        {
            case "contact_center": return jo["contextInfo"].ToObject<EventContextPropertyCONTACT_CENTER>(serializer); break;

            case "web": return jo["contextInfo"].ToObject<EventContextPropertyWEB>(serializer); break;

            case "mobile": return jo["contextInfo"].ToObject<EventContextPropertyMOBILE>(serializer); break;

            case "ecommerce": return jo["contextInfo"].ToObject<EventContextPropertyECOMMERCE>(serializer); break;

            case "retail": return jo["contextInfo"].ToObject<EventContextPropertyRETAIL>(serializer); break;

            case "iot": return jo["contextInfo"].ToObject<EventContextPropertyIOT>(serializer); break;

            case "social": return jo["contextInfo"].ToObject<EventContextPropertySOCIAL>(serializer); break;

            case "digital_campaign": return jo["contextInfo"].ToObject<EventContextPropertyDIGITAL_CAMPAIGN>(serializer); break;

            case "other": return jo["contextInfo"].ToObject<EventContextPropertyOTHER>(serializer); break;

        }
        return null;
    }

}
