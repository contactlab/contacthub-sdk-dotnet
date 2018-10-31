/* selfgenerated from version 0.0.0.1 27/09/2018 09:48:44 */

using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace ContactHubSdkLibrary {

public class ConsentsProperties
{
    public Disclaimer disclaimer {get;set;}
    public Marketing marketing {get;set;}
    public Profiling profiling {get;set;}
    public SoftSpam softSpam {get;set;}
    public ThirdPartyTransfer thirdPartyTransfer {get;set;}
}


public class Disclaimer
{
    [JsonProperty("date")]
    public string _date {get;set;}
    [JsonProperty("_date")]
    [JsonIgnore]
 
                 public DateTime date
        {
            get
            {
                if (_date != null)
                {
                    if (_date.Contains("+"))  //date format: 2017-01-25T17:14:01.000+0000
                        {
                           return Convert.ToDateTime(_date).ToUniversalTime();
                        }
                    else  //date format yyyy-MM-dd'T'HH:mm:ssZ
                    {
                        if (_date.Contains("T")) 
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
    public Boolean? status {get;set;}
    public Boolean? limitation {get;set;}
    public Boolean? objection {get;set;}
}


public class Papery
{
    public Boolean? status {get;set;}
    public Boolean? limitation {get;set;}
    public Boolean? objection {get;set;}
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
    public Boolean? status {get;set;}
    public Boolean? limitation {get;set;}
    public Boolean? objection {get;set;}
}


public class Email
{
    public Boolean? status {get;set;}
    public Boolean? limitation {get;set;}
    public Boolean? objection {get;set;}
}


public class Push
{
    public Boolean? status {get;set;}
    public Boolean? limitation {get;set;}
    public Boolean? objection {get;set;}
}


public class Im
{
    public Boolean? status {get;set;}
    public Boolean? limitation {get;set;}
    public Boolean? objection {get;set;}
}


public class Profiling
{
    public Classic classic {get;set;}
    public Online online {get;set;}
}


public class Classic
{
    public Boolean? status {get;set;}
    public Boolean? limitation {get;set;}
    public Boolean? objection {get;set;}
}


public class Online
{
    public Boolean? status {get;set;}
    public Boolean? limitation {get;set;}
    public Boolean? objection {get;set;}
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
