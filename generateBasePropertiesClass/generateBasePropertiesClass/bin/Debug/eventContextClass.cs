/* autogenerated from version 0.0.0.1 */


using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
public enum EventContextEnum {
	NoValue,
	[Display(Name="WEB")]
	WEB,
	[Display(Name="MOBILE")]
	MOBILE,
	[Display(Name="ECOMMERCE")]
	ECOMMERCE,
	[Display(Name="RETAIL")]
	RETAIL,
	[Display(Name="SOCIAL")]
	SOCIAL,
	[Display(Name="DIGITAL_CAMPAIGN")]
	DIGITALCAMPAIGN,
	[Display(Name="CONTACT_CENTER")]
	CONTACTCENTER,
	[Display(Name="OTHER")]
	OTHER
}