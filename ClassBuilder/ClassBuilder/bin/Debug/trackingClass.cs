/* selfgenerated from version 0.0.0.1 03/09/2018 12:35:43 */

using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace ContactHubSdkLibrary {

public class TrackingProperties
{
    public Ga ga {get;set;}
}


public class Ga
{
	[Display(Name="Parameter to identify the source of your traffic such as: search engine, newsletter, or other referral")]
    public string utm_source {get;set;}
	[Display(Name="Parameter to identify the medium the link was used upon such as: email, CPC, or other method of sharing")]
    public string utm_medium {get;set;}
	[Display(Name="Parameter suggested for paid search to identify keywords for your ad")]
    public string utm_term {get;set;}
	[Display(Name="Parameter for additional details for A/B testing and content-targeted ads")]
    public string utm_content {get;set;}
	[Display(Name="Parameter to identify a specific product promotion or strategic campaign such as a spring sale or other")]
    public string utm_campaign {get;set;}
}


}
