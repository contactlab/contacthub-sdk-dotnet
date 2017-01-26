/* selfgenerated from version 0.0.0.1 26/01/2017 12:28:24 */

using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using ContactHubSdkLibrary.Events;
using ContactHubSdkLibrary;
using Newtonsoft.Json.Linq;
namespace ContactHubSdkLibrary.Events {
//context class 'CONTACT_CENTER': CONTACT_CENTER
public class EventContextPropertyCONTACT_CENTER: EventBaseProperty
{
    public string userAgent {get;set;}
    //format: ipv4
    public string ip {get;set;}
}


//context class 'WEB': WEB
public class EventContextPropertyWEB: EventBaseProperty
{
    public string userAgent {get;set;}
    //format: ipv4
    public string ip {get;set;}
}


//context class 'MOBILE': MOBILE
public class EventContextPropertyMOBILE: EventBaseProperty
{
    public string userAgent {get;set;}
    //format: ipv4
    public string ip {get;set;}
}


//context class 'ECOMMERCE': ECOMMERCE
public class EventContextPropertyECOMMERCE: EventBaseProperty
{
    public string userAgent {get;set;}
    //format: ipv4
    public string ip {get;set;}
}


//context class 'RETAIL': RETAIL
public class EventContextPropertyRETAIL: EventBaseProperty
{
    public string userAgent {get;set;}
    //format: ipv4
    public string ip {get;set;}
}


//context class 'IOT': IOT
public class EventContextPropertyIOT: EventBaseProperty
{
    public string userAgent {get;set;}
    //format: ipv4
    public string ip {get;set;}
}


//context class 'SOCIAL': SOCIAL
public class EventContextPropertySOCIAL: EventBaseProperty
{
    public string userAgent {get;set;}
    //format: ipv4
    public string ip {get;set;}
}


//context class 'DIGITAL_CAMPAIGN': DIGITAL_CAMPAIGN
public class EventContextPropertyDIGITAL_CAMPAIGN: EventBaseProperty
{
    public string userAgent {get;set;}
    //format: ipv4
    public string ip {get;set;}
}


//context class 'OTHER': OTHER
public class EventContextPropertyOTHER: EventBaseProperty
{
    public string userAgent {get;set;}
    //format: ipv4
    public string ip {get;set;}
}



}
public enum EventContextEnum {
	NoValue,
	[Display(Name="CONTACT_CENTER")]
	CONTACTCENTER,
	[Display(Name="WEB")]
	WEB,
	[Display(Name="MOBILE")]
	MOBILE,
	[Display(Name="ECOMMERCE")]
	ECOMMERCE,
	[Display(Name="RETAIL")]
	RETAIL,
	[Display(Name="IOT")]
	IOT,
	[Display(Name="SOCIAL")]
	SOCIAL,
	[Display(Name="DIGITAL_CAMPAIGN")]
	DIGITALCAMPAIGN,
	[Display(Name="OTHER")]
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
                     case "contact_center": return jo["contextInfo"].ToObject<EventContextPropertyCONTACT_CENTER > (serializer);break;

 case "web": return jo["contextInfo"].ToObject<EventContextPropertyWEB > (serializer);break;

 case "mobile": return jo["contextInfo"].ToObject<EventContextPropertyMOBILE > (serializer);break;

 case "ecommerce": return jo["contextInfo"].ToObject<EventContextPropertyECOMMERCE > (serializer);break;

 case "retail": return jo["contextInfo"].ToObject<EventContextPropertyRETAIL > (serializer);break;

 case "iot": return jo["contextInfo"].ToObject<EventContextPropertyIOT > (serializer);break;

 case "social": return jo["contextInfo"].ToObject<EventContextPropertySOCIAL > (serializer);break;

 case "digital_campaign": return jo["contextInfo"].ToObject<EventContextPropertyDIGITAL_CAMPAIGN > (serializer);break;

 case "other": return jo["contextInfo"].ToObject<EventContextPropertyOTHER > (serializer);break;

}
 return null;
}

}
