/* selfgenerated from version 0.0.0.1 19/03/2018 11:04:02 */

using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace ContactHubSdkLibrary {

public class ConsensProperties
{
    public Disclaimer disclaimer {get;set;}
    public Marketing marketing {get;set;}
    public Profiling profiling {get;set;}
    public SoftSpam softSpam {get;set;}
    public ThirdPartyTransfer thirdPartyTransfer {get;set;}
}


public class Disclaimer
{
	[Display(Name="Version of disclaimer")]
    //format: date-time
    public string date {get;set;}
	[Display(Name="The date of acceptance of the disclaimer")]
    public string version {get;set;}
}


public class Marketing
{
    public Traditional traditional {get;set;}
    public Automatic automatic {get;set;}
}


public class Traditional
{
    public Telephonic telephonic {get;set;}
    public Papery papery {get;set;}
}


public class Telephonic
{
	[Display(Name="Status of consent")]
    public Boolean status {get;set;}
	[Display(Name="Status of consent limitation")]
    public Boolean limitation {get;set;}
	[Display(Name="Status of consent objection")]
    public Boolean objection {get;set;}
}


public class Papery
{
	[Display(Name="Status of consent")]
    public Boolean status {get;set;}
	[Display(Name="Status of consent limitation")]
    public Boolean limitation {get;set;}
	[Display(Name="Status of consent objection")]
    public Boolean objection {get;set;}
}


public class Automatic
{
    public Sms sms {get;set;}
    public Email email {get;set;}
    public Push push {get;set;}
    public Im im {get;set;}
    public Telephonic telephonic {get;set;}
}


public class Sms
{
	[Display(Name="Status of consent")]
    public Boolean status {get;set;}
	[Display(Name="Status of consent limitation")]
    public Boolean limitation {get;set;}
	[Display(Name="Status of consent objection")]
    public Boolean objection {get;set;}
}


public class Email
{
	[Display(Name="Status of consent")]
    public Boolean status {get;set;}
	[Display(Name="Status of consent limitation")]
    public Boolean limitation {get;set;}
	[Display(Name="Status of consent objection")]
    public Boolean objection {get;set;}
}


public class Push
{
	[Display(Name="Status of consent")]
    public Boolean status {get;set;}
	[Display(Name="Status of consent limitation")]
    public Boolean limitation {get;set;}
	[Display(Name="Status of consent objection")]
    public Boolean objection {get;set;}
}


public class Im
{
	[Display(Name="Status of consent")]
    public Boolean status {get;set;}
	[Display(Name="Status of consent limitation")]
    public Boolean limitation {get;set;}
	[Display(Name="Status of consent objection")]
    public Boolean objection {get;set;}
}


public class Profiling
{
    public Classic classic {get;set;}
    public Online online {get;set;}
}


public class Classic
{
	[Display(Name="Status of consent")]
    public Boolean status {get;set;}
	[Display(Name="Status of consent limitation")]
    public Boolean limitation {get;set;}
	[Display(Name="Status of consent objection")]
    public Boolean objection {get;set;}
}


public class Online
{
	[Display(Name="Status of consent")]
    public Boolean status {get;set;}
	[Display(Name="Status of consent limitation")]
    public Boolean limitation {get;set;}
	[Display(Name="Status of consent objection")]
    public Boolean objection {get;set;}
}


public class SoftSpam
{
    public Email email {get;set;}
    public Papery papery {get;set;}
}


public class ThirdPartyTransfer
{
    public Profiling profiling {get;set;}
    public Marketing marketing {get;set;}
}


}
