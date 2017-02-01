/* selfgenerated from version 0.0.0.1 30/01/2017 12:37:46 */

using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace ContactHubSdkLibrary {

                                public class ValidatePatternAttribute : System.ComponentModel.DisplayNameAttribute
                                {
                                    public ValidatePatternAttribute(string data) : base(data) { }
                                }
            
public class BaseProperties
{
	[Display(Name="the picture url of customer")]
    //format: uri
    public string pictureUrl {get;set;}
	[Display(Name="the title")]
    public string title {get;set;}
	[Display(Name="the prefix")]
    public string prefix {get;set;}
	[Display(Name="the first name")]
    public string firstName {get;set;}
	[Display(Name="the last name")]
    public string lastName {get;set;}
	[Display(Name="the middle name")]
    public string middleName {get;set;}
	[Display(Name="gender")]
    public string gender {get;set;}
	[Display(Name="date of birth")]
    //format: date
    public string dob {get;set;}
	[ValidatePattern(@"^[a-z]{2}(_([a-zA-Z]{2}){1,2})?_[A-Z]{2}$")]
	[Display(Name="the locale")]
    public string locale {get;set;}
	[Display(Name="the time zone")]
[JsonProperty("timezone")]public string _timezone {get;set;}
[JsonProperty("hidden_timezone")][JsonIgnore]
                    public BasePropertiesTimezoneEnum timezone 
            {
                get
                {
                        BasePropertiesTimezoneEnum enumValue =ContactHubSdkLibrary.EnumHelper<BasePropertiesTimezoneEnum>.GetValueFromDisplayName(_timezone);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<BasePropertiesTimezoneEnum>.GetDisplayValue(value);
                        _timezone = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public Contacts contacts {get;set;}
    public Address address {get;set;}
    public Credential credential {get;set;}
	[Display(Name="educations")]
    public List<Educations> educations {get;set;}
	[Display(Name="likes")]
    public List<Likes> likes {get;set;}
    public SocialProfile socialProfile {get;set;}
	[Display(Name="jobs")]
    public List<Jobs> jobs {get;set;}
	[Display(Name="subscriptions")]
    public List<Subscriptions> subscriptions {get;set;}
}


public class Contacts
{
	[Display(Name="the e-mail")]
    //format: email
    public string email {get;set;}
	[Display(Name="the fax number")]
    public string fax {get;set;}
	[Display(Name="the mobile phone number")]
    public string mobilePhone {get;set;}
	[Display(Name="the phone")]
    public string phone {get;set;}
	[Display(Name="other contacts")]
    public List<OtherContacts> otherContacts {get;set;}
	[Display(Name="mobile device")]
    public List<MobileDevices> mobileDevices {get;set;}
}


public class OtherContacts
{
    public string name {get;set;}
[JsonProperty("type")]public string _type {get;set;}
[JsonProperty("hidden_type")][JsonIgnore]
                    public OtherContactsTypeEnum type 
            {
                get
                {
                        OtherContactsTypeEnum enumValue =ContactHubSdkLibrary.EnumHelper<OtherContactsTypeEnum>.GetValueFromDisplayName(_type);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<OtherContactsTypeEnum>.GetDisplayValue(value);
                        _type = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public string value {get;set;}
}

public enum OtherContactsTypeEnum {
	NoValue,
	[Display(Name="MOBILE")]
	MOBILE,
	[Display(Name="PHONE")]
	PHONE,
	[Display(Name="EMAIL")]
	EMAIL,
	[Display(Name="FAX")]
	FAX,
	[Display(Name="OTHER")]
	OTHER
}
public class MobileDevices
{
    public string identifier {get;set;}
    public string name {get;set;}
[JsonProperty("type")]public string _type {get;set;}
[JsonProperty("hidden_type")][JsonIgnore]
                    public MobileDevicesTypeEnum type 
            {
                get
                {
                        MobileDevicesTypeEnum enumValue =ContactHubSdkLibrary.EnumHelper<MobileDevicesTypeEnum>.GetValueFromDisplayName(_type);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<MobileDevicesTypeEnum>.GetDisplayValue(value);
                        _type = (displayValue=="NoValue"? null : displayValue);
                }
            }
            }

public enum MobileDevicesTypeEnum {
	NoValue,
	[Display(Name="IOS")]
	IOS,
	[Display(Name="GCM")]
	GCM,
	[Display(Name="WP")]
	WP
}
public class Address
{
	[Display(Name="the street")]
    public string street {get;set;}
	[Display(Name="the city")]
    public string city {get;set;}
	[Display(Name="the country")]
    public string country {get;set;}
	[Display(Name="the province")]
    public string province {get;set;}
	[Display(Name="the zip code")]
    public string zip {get;set;}
    public Geo geo {get;set;}
}


public class Geo
{
	[Display(Name="latitude")]
    public decimal? lat {get;set;}
	[Display(Name="longitude")]
    public decimal? lon {get;set;}
}


public class Credential
{
	[Display(Name="the password")]
    public string password {get;set;}
	[Display(Name="the user name")]
    public string username {get;set;}
}


public class Educations
{
    public string id {get;set;}
[JsonProperty("schoolType")]public string _schoolType {get;set;}
[JsonProperty("hidden_schoolType")][JsonIgnore]
                    public EducationsSchoolTypeEnum schoolType 
            {
                get
                {
                        EducationsSchoolTypeEnum enumValue =ContactHubSdkLibrary.EnumHelper<EducationsSchoolTypeEnum>.GetValueFromDisplayName(_schoolType);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<EducationsSchoolTypeEnum>.GetDisplayValue(value);
                        _schoolType = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public string schoolName {get;set;}
    public string schoolConcentration {get;set;}
    public int startYear {get;set;}
    public int endYear {get;set;}
    public Boolean isCurrent {get;set;}
}

public enum EducationsSchoolTypeEnum {
	NoValue,
	[Display(Name="PRIMARY_SCHOOL")]
	PRIMARYSCHOOL,
	[Display(Name="SECONDARY_SCHOOL")]
	SECONDARYSCHOOL,
	[Display(Name="HIGH_SCHOOL")]
	HIGHSCHOOL,
	[Display(Name="COLLEGE")]
	COLLEGE,
	[Display(Name="OTHER")]
	OTHER
}
public class Likes
{
    public string id {get;set;}
    public string category {get;set;}
    public string name {get;set;}
    [JsonProperty("createdTime")]
    public string _createdTime {get;set;}
    [JsonProperty("_createdTime")]
    [JsonIgnore]
 
                 public DateTime createdTime
        {
            get
            {
                if (_createdTime != null)
                {
                    if (_createdTime.Contains("+"))  //date format: 2017-01-25T17:14:01.000+0000
                        {
                           return Convert.ToDateTime(_createdTime).ToUniversalTime();
                        }
                    else  //date format yyyy-MM-dd'T'HH:mm:ssZ
                    {
                        if (_createdTime.Contains("T")) 
                        {
                         return
                         DateTime.ParseExact(_createdTime,
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
                    _createdTime = value.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                }
                catch { _createdTime = null; }
            }
        }
            }


public class SocialProfile
{
	[Display(Name="facebook")]
    public string facebook {get;set;}
	[Display(Name="google+")]
    public string google {get;set;}
	[Display(Name="instagram")]
    public string instagram {get;set;}
	[Display(Name="linkedin")]
    public string linkedin {get;set;}
	[Display(Name="qzone")]
    public string qzone {get;set;}
	[Display(Name="twitter")]
    public string twitter {get;set;}
}


public class Jobs
{
    public string id {get;set;}
    public string companyIndustry {get;set;}
    public string companyName {get;set;}
    public string jobTitle {get;set;}
    [JsonProperty("startDate")]
    public string _startDate {get;set;}
    [JsonProperty("_startDate")]
    [JsonIgnore]
 
                 public DateTime startDate
        {
            get
            {
                if (_startDate != null)
                {
                    return
                         DateTime.ParseExact(_startDate,
                                       "yyyy-MM-dd",
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
                    _startDate = value.ToString("yyyy-MM-dd");
                }
                catch { _startDate = null; }
            }
        }
                [JsonProperty("endDate")]
    public string _endDate {get;set;}
    [JsonProperty("_endDate")]
    [JsonIgnore]
 
                 public DateTime endDate
        {
            get
            {
                if (_endDate != null)
                {
                    return
                         DateTime.ParseExact(_endDate,
                                       "yyyy-MM-dd",
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
                    _endDate = value.ToString("yyyy-MM-dd");
                }
                catch { _endDate = null; }
            }
        }
                public Boolean isCurrent {get;set;}
}


public class Subscriptions
{
    public string id {get;set;}
    public string name {get;set;}
    public string type {get;set;}
[JsonProperty("kind")]public string _kind {get;set;}
[JsonProperty("hidden_kind")][JsonIgnore]
                    public SubscriptionsKindEnum kind 
            {
                get
                {
                        SubscriptionsKindEnum enumValue =ContactHubSdkLibrary.EnumHelper<SubscriptionsKindEnum>.GetValueFromDisplayName(_kind);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<SubscriptionsKindEnum>.GetDisplayValue(value);
                        _kind = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public Boolean subscribed {get;set;}
    [JsonProperty("startDate")]
    public string _startDate {get;set;}
    [JsonProperty("_startDate")]
    [JsonIgnore]
 
                 public DateTime startDate
        {
            get
            {
                if (_startDate != null)
                {
                    if (_startDate.Contains("+"))  //date format: 2017-01-25T17:14:01.000+0000
                        {
                           return Convert.ToDateTime(_startDate).ToUniversalTime();
                        }
                    else  //date format yyyy-MM-dd'T'HH:mm:ssZ
                    {
                        if (_startDate.Contains("T")) 
                        {
                         return
                         DateTime.ParseExact(_startDate,
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
                    _startDate = value.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                }
                catch { _startDate = null; }
            }
        }
                [JsonProperty("endDate")]
    public string _endDate {get;set;}
    [JsonProperty("_endDate")]
    [JsonIgnore]
 
                 public DateTime endDate
        {
            get
            {
                if (_endDate != null)
                {
                    if (_endDate.Contains("+"))  //date format: 2017-01-25T17:14:01.000+0000
                        {
                           return Convert.ToDateTime(_endDate).ToUniversalTime();
                        }
                    else  //date format yyyy-MM-dd'T'HH:mm:ssZ
                    {
                        if (_endDate.Contains("T")) 
                        {
                         return
                         DateTime.ParseExact(_endDate,
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
                    _endDate = value.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                }
                catch { _endDate = null; }
            }
        }
                public string subscriberId {get;set;}
    [JsonProperty("registeredAt")]
    public string _registeredAt {get;set;}
    [JsonProperty("_registeredAt")]
    [JsonIgnore]
 
                 public DateTime registeredAt
        {
            get
            {
                if (_registeredAt != null)
                {
                    if (_registeredAt.Contains("+"))  //date format: 2017-01-25T17:14:01.000+0000
                        {
                           return Convert.ToDateTime(_registeredAt).ToUniversalTime();
                        }
                    else  //date format yyyy-MM-dd'T'HH:mm:ssZ
                    {
                        if (_registeredAt.Contains("T")) 
                        {
                         return
                         DateTime.ParseExact(_registeredAt,
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
                    _registeredAt = value.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                }
                catch { _registeredAt = null; }
            }
        }
                [JsonProperty("updatedAt")]
    public string _updatedAt {get;set;}
    [JsonProperty("_updatedAt")]
    [JsonIgnore]
 
                 public DateTime updatedAt
        {
            get
            {
                if (_updatedAt != null)
                {
                    if (_updatedAt.Contains("+"))  //date format: 2017-01-25T17:14:01.000+0000
                        {
                           return Convert.ToDateTime(_updatedAt).ToUniversalTime();
                        }
                    else  //date format yyyy-MM-dd'T'HH:mm:ssZ
                    {
                        if (_updatedAt.Contains("T")) 
                        {
                         return
                         DateTime.ParseExact(_updatedAt,
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
                    _updatedAt = value.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                }
                catch { _updatedAt = null; }
            }
        }
                public List<Preferences> preferences {get;set;}
}


public class Preferences
{
    public string key {get;set;}
    public string value {get;set;}
}

public enum SubscriptionsKindEnum {
	NoValue,
	[Display(Name="DIGITAL_MESSAGE")]
	DIGITALMESSAGE,
	[Display(Name="SERVICE")]
	SERVICE,
	[Display(Name="OTHER")]
	OTHER
}public enum BasePropertiesTimezoneEnum {
	NoValue,
	[Display(Name="Africa/Abidjan")]
	AfricaAbidjan,
	[Display(Name="Africa/Accra")]
	AfricaAccra,
	[Display(Name="Africa/Algiers")]
	AfricaAlgiers,
	[Display(Name="Africa/Bissau")]
	AfricaBissau,
	[Display(Name="Africa/Cairo")]
	AfricaCairo,
	[Display(Name="Africa/Casablanca")]
	AfricaCasablanca,
	[Display(Name="Africa/Ceuta")]
	AfricaCeuta,
	[Display(Name="Africa/El_Aaiun")]
	AfricaElAaiun,
	[Display(Name="Africa/Johannesburg")]
	AfricaJohannesburg,
	[Display(Name="Africa/Khartoum")]
	AfricaKhartoum,
	[Display(Name="Africa/Lagos")]
	AfricaLagos,
	[Display(Name="Africa/Maputo")]
	AfricaMaputo,
	[Display(Name="Africa/Monrovia")]
	AfricaMonrovia,
	[Display(Name="Africa/Nairobi")]
	AfricaNairobi,
	[Display(Name="Africa/Ndjamena")]
	AfricaNdjamena,
	[Display(Name="Africa/Tripoli")]
	AfricaTripoli,
	[Display(Name="Africa/Tunis")]
	AfricaTunis,
	[Display(Name="Africa/Windhoek")]
	AfricaWindhoek,
	[Display(Name="America/Adak")]
	AmericaAdak,
	[Display(Name="America/Anchorage")]
	AmericaAnchorage,
	[Display(Name="America/Araguaina")]
	AmericaAraguaina,
	[Display(Name="America/Argentina/Buenos_Aires")]
	AmericaArgentinaBuenosAires,
	[Display(Name="America/Argentina/Catamarca")]
	AmericaArgentinaCatamarca,
	[Display(Name="America/Argentina/Cordoba")]
	AmericaArgentinaCordoba,
	[Display(Name="America/Argentina/Jujuy")]
	AmericaArgentinaJujuy,
	[Display(Name="America/Argentina/La_Rioja")]
	AmericaArgentinaLaRioja,
	[Display(Name="America/Argentina/Mendoza")]
	AmericaArgentinaMendoza,
	[Display(Name="America/Argentina/Rio_Gallegos")]
	AmericaArgentinaRioGallegos,
	[Display(Name="America/Argentina/Salta")]
	AmericaArgentinaSalta,
	[Display(Name="America/Argentina/San_Juan")]
	AmericaArgentinaSanJuan,
	[Display(Name="America/Argentina/San_Luis")]
	AmericaArgentinaSanLuis,
	[Display(Name="America/Argentina/Tucuman")]
	AmericaArgentinaTucuman,
	[Display(Name="America/Argentina/Ushuaia")]
	AmericaArgentinaUshuaia,
	[Display(Name="America/Asuncion")]
	AmericaAsuncion,
	[Display(Name="America/Atikokan")]
	AmericaAtikokan,
	[Display(Name="America/Bahia")]
	AmericaBahia,
	[Display(Name="America/Bahia_Banderas")]
	AmericaBahiaBanderas,
	[Display(Name="America/Barbados")]
	AmericaBarbados,
	[Display(Name="America/Belem")]
	AmericaBelem,
	[Display(Name="America/Belize")]
	AmericaBelize,
	[Display(Name="America/Blanc-Sablon")]
	AmericaBlancMinusSablon,
	[Display(Name="America/Boa_Vista")]
	AmericaBoaVista,
	[Display(Name="America/Bogota")]
	AmericaBogota,
	[Display(Name="America/Boise")]
	AmericaBoise,
	[Display(Name="America/Cambridge_Bay")]
	AmericaCambridgeBay,
	[Display(Name="America/Campo_Grande")]
	AmericaCampoGrande,
	[Display(Name="America/Cancun")]
	AmericaCancun,
	[Display(Name="America/Caracas")]
	AmericaCaracas,
	[Display(Name="America/Cayenne")]
	AmericaCayenne,
	[Display(Name="America/Chicago")]
	AmericaChicago,
	[Display(Name="America/Chihuahua")]
	AmericaChihuahua,
	[Display(Name="America/Costa_Rica")]
	AmericaCostaRica,
	[Display(Name="America/Creston")]
	AmericaCreston,
	[Display(Name="America/Cuiaba")]
	AmericaCuiaba,
	[Display(Name="America/Curacao")]
	AmericaCuracao,
	[Display(Name="America/Danmarkshavn")]
	AmericaDanmarkshavn,
	[Display(Name="America/Dawson")]
	AmericaDawson,
	[Display(Name="America/Dawson_Creek")]
	AmericaDawsonCreek,
	[Display(Name="America/Denver")]
	AmericaDenver,
	[Display(Name="America/Detroit")]
	AmericaDetroit,
	[Display(Name="America/Edmonton")]
	AmericaEdmonton,
	[Display(Name="America/Eirunepe")]
	AmericaEirunepe,
	[Display(Name="America/El_Salvador")]
	AmericaElSalvador,
	[Display(Name="America/Fortaleza")]
	AmericaFortaleza,
	[Display(Name="America/Fort_Nelson")]
	AmericaFortNelson,
	[Display(Name="America/Glace_Bay")]
	AmericaGlaceBay,
	[Display(Name="America/Godthab")]
	AmericaGodthab,
	[Display(Name="America/Goose_Bay")]
	AmericaGooseBay,
	[Display(Name="America/Grand_Turk")]
	AmericaGrandTurk,
	[Display(Name="America/Guatemala")]
	AmericaGuatemala,
	[Display(Name="America/Guayaquil")]
	AmericaGuayaquil,
	[Display(Name="America/Guyana")]
	AmericaGuyana,
	[Display(Name="America/Halifax")]
	AmericaHalifax,
	[Display(Name="America/Havana")]
	AmericaHavana,
	[Display(Name="America/Hermosillo")]
	AmericaHermosillo,
	[Display(Name="America/Indiana/Indianapolis")]
	AmericaIndianaIndianapolis,
	[Display(Name="America/Indiana/Knox")]
	AmericaIndianaKnox,
	[Display(Name="America/Indiana/Marengo")]
	AmericaIndianaMarengo,
	[Display(Name="America/Indiana/Petersburg")]
	AmericaIndianaPetersburg,
	[Display(Name="America/Indiana/Tell_City")]
	AmericaIndianaTellCity,
	[Display(Name="America/Indiana/Vevay")]
	AmericaIndianaVevay,
	[Display(Name="America/Indiana/Vincennes")]
	AmericaIndianaVincennes,
	[Display(Name="America/Indiana/Winamac")]
	AmericaIndianaWinamac,
	[Display(Name="America/Inuvik")]
	AmericaInuvik,
	[Display(Name="America/Iqaluit")]
	AmericaIqaluit,
	[Display(Name="America/Jamaica")]
	AmericaJamaica,
	[Display(Name="America/Juneau")]
	AmericaJuneau,
	[Display(Name="America/Kentucky/Louisville")]
	AmericaKentuckyLouisville,
	[Display(Name="America/Kentucky/Monticello")]
	AmericaKentuckyMonticello,
	[Display(Name="America/La_Paz")]
	AmericaLaPaz,
	[Display(Name="America/Lima")]
	AmericaLima,
	[Display(Name="America/Los_Angeles")]
	AmericaLosAngeles,
	[Display(Name="America/Maceio")]
	AmericaMaceio,
	[Display(Name="America/Managua")]
	AmericaManagua,
	[Display(Name="America/Manaus")]
	AmericaManaus,
	[Display(Name="America/Martinique")]
	AmericaMartinique,
	[Display(Name="America/Matamoros")]
	AmericaMatamoros,
	[Display(Name="America/Mazatlan")]
	AmericaMazatlan,
	[Display(Name="America/Menominee")]
	AmericaMenominee,
	[Display(Name="America/Merida")]
	AmericaMerida,
	[Display(Name="America/Metlakatla")]
	AmericaMetlakatla,
	[Display(Name="America/Mexico_City")]
	AmericaMexicoCity,
	[Display(Name="America/Miquelon")]
	AmericaMiquelon,
	[Display(Name="America/Moncton")]
	AmericaMoncton,
	[Display(Name="America/Monterrey")]
	AmericaMonterrey,
	[Display(Name="America/Montevideo")]
	AmericaMontevideo,
	[Display(Name="America/Nassau")]
	AmericaNassau,
	[Display(Name="America/New_York")]
	AmericaNewYork,
	[Display(Name="America/Nipigon")]
	AmericaNipigon,
	[Display(Name="America/Nome")]
	AmericaNome,
	[Display(Name="America/Noronha")]
	AmericaNoronha,
	[Display(Name="America/North_Dakota/Beulah")]
	AmericaNorthDakotaBeulah,
	[Display(Name="America/North_Dakota/Center")]
	AmericaNorthDakotaCenter,
	[Display(Name="America/North_Dakota/New_Salem")]
	AmericaNorthDakotaNewSalem,
	[Display(Name="America/Ojinaga")]
	AmericaOjinaga,
	[Display(Name="America/Panama")]
	AmericaPanama,
	[Display(Name="America/Pangnirtung")]
	AmericaPangnirtung,
	[Display(Name="America/Paramaribo")]
	AmericaParamaribo,
	[Display(Name="America/Phoenix")]
	AmericaPhoenix,
	[Display(Name="America/Port-au-Prince")]
	AmericaPortMinusauMinusPrince,
	[Display(Name="America/Port_of_Spain")]
	AmericaPortofSpain,
	[Display(Name="America/Porto_Velho")]
	AmericaPortoVelho,
	[Display(Name="America/Puerto_Rico")]
	AmericaPuertoRico,
	[Display(Name="America/Rainy_River")]
	AmericaRainyRiver,
	[Display(Name="America/Rankin_Inlet")]
	AmericaRankinInlet,
	[Display(Name="America/Recife")]
	AmericaRecife,
	[Display(Name="America/Regina")]
	AmericaRegina,
	[Display(Name="America/Resolute")]
	AmericaResolute,
	[Display(Name="America/Rio_Branco")]
	AmericaRioBranco,
	[Display(Name="America/Santarem")]
	AmericaSantarem,
	[Display(Name="America/Santiago")]
	AmericaSantiago,
	[Display(Name="America/Santo_Domingo")]
	AmericaSantoDomingo,
	[Display(Name="America/Sao_Paulo")]
	AmericaSaoPaulo,
	[Display(Name="America/Scoresbysund")]
	AmericaScoresbysund,
	[Display(Name="America/Sitka")]
	AmericaSitka,
	[Display(Name="America/St_Johns")]
	AmericaStJohns,
	[Display(Name="America/Swift_Current")]
	AmericaSwiftCurrent,
	[Display(Name="America/Tegucigalpa")]
	AmericaTegucigalpa,
	[Display(Name="America/Thule")]
	AmericaThule,
	[Display(Name="America/Thunder_Bay")]
	AmericaThunderBay,
	[Display(Name="America/Tijuana")]
	AmericaTijuana,
	[Display(Name="America/Toronto")]
	AmericaToronto,
	[Display(Name="America/Vancouver")]
	AmericaVancouver,
	[Display(Name="America/Whitehorse")]
	AmericaWhitehorse,
	[Display(Name="America/Winnipeg")]
	AmericaWinnipeg,
	[Display(Name="America/Yakutat")]
	AmericaYakutat,
	[Display(Name="America/Yellowknife")]
	AmericaYellowknife,
	[Display(Name="Antarctica/Casey")]
	AntarcticaCasey,
	[Display(Name="Antarctica/Davis")]
	AntarcticaDavis,
	[Display(Name="Antarctica/DumontDUrville")]
	AntarcticaDumontDUrville,
	[Display(Name="Antarctica/Macquarie")]
	AntarcticaMacquarie,
	[Display(Name="Antarctica/Mawson")]
	AntarcticaMawson,
	[Display(Name="Antarctica/Palmer")]
	AntarcticaPalmer,
	[Display(Name="Antarctica/Rothera")]
	AntarcticaRothera,
	[Display(Name="Antarctica/Syowa")]
	AntarcticaSyowa,
	[Display(Name="Antarctica/Troll")]
	AntarcticaTroll,
	[Display(Name="Antarctica/Vostok")]
	AntarcticaVostok,
	[Display(Name="Asia/Almaty")]
	AsiaAlmaty,
	[Display(Name="Asia/Amman")]
	AsiaAmman,
	[Display(Name="Asia/Anadyr")]
	AsiaAnadyr,
	[Display(Name="Asia/Aqtau")]
	AsiaAqtau,
	[Display(Name="Asia/Aqtobe")]
	AsiaAqtobe,
	[Display(Name="Asia/Ashgabat")]
	AsiaAshgabat,
	[Display(Name="Asia/Baghdad")]
	AsiaBaghdad,
	[Display(Name="Asia/Baku")]
	AsiaBaku,
	[Display(Name="Asia/Bangkok")]
	AsiaBangkok,
	[Display(Name="Asia/Barnaul")]
	AsiaBarnaul,
	[Display(Name="Asia/Beirut")]
	AsiaBeirut,
	[Display(Name="Asia/Bishkek")]
	AsiaBishkek,
	[Display(Name="Asia/Brunei")]
	AsiaBrunei,
	[Display(Name="Asia/Chita")]
	AsiaChita,
	[Display(Name="Asia/Choibalsan")]
	AsiaChoibalsan,
	[Display(Name="Asia/Colombo")]
	AsiaColombo,
	[Display(Name="Asia/Damascus")]
	AsiaDamascus,
	[Display(Name="Asia/Dhaka")]
	AsiaDhaka,
	[Display(Name="Asia/Dili")]
	AsiaDili,
	[Display(Name="Asia/Dubai")]
	AsiaDubai,
	[Display(Name="Asia/Dushanbe")]
	AsiaDushanbe,
	[Display(Name="Asia/Famagusta")]
	AsiaFamagusta,
	[Display(Name="Asia/Gaza")]
	AsiaGaza,
	[Display(Name="Asia/Hebron")]
	AsiaHebron,
	[Display(Name="Asia/Ho_Chi_Minh")]
	AsiaHoChiMinh,
	[Display(Name="Asia/Hong_Kong")]
	AsiaHongKong,
	[Display(Name="Asia/Hovd")]
	AsiaHovd,
	[Display(Name="Asia/Irkutsk")]
	AsiaIrkutsk,
	[Display(Name="Asia/Jakarta")]
	AsiaJakarta,
	[Display(Name="Asia/Jayapura")]
	AsiaJayapura,
	[Display(Name="Asia/Jerusalem")]
	AsiaJerusalem,
	[Display(Name="Asia/Kabul")]
	AsiaKabul,
	[Display(Name="Asia/Kamchatka")]
	AsiaKamchatka,
	[Display(Name="Asia/Karachi")]
	AsiaKarachi,
	[Display(Name="Asia/Kathmandu")]
	AsiaKathmandu,
	[Display(Name="Asia/Khandyga")]
	AsiaKhandyga,
	[Display(Name="Asia/Kolkata")]
	AsiaKolkata,
	[Display(Name="Asia/Krasnoyarsk")]
	AsiaKrasnoyarsk,
	[Display(Name="Asia/Kuala_Lumpur")]
	AsiaKualaLumpur,
	[Display(Name="Asia/Kuching")]
	AsiaKuching,
	[Display(Name="Asia/Macau")]
	AsiaMacau,
	[Display(Name="Asia/Magadan")]
	AsiaMagadan,
	[Display(Name="Asia/Makassar")]
	AsiaMakassar,
	[Display(Name="Asia/Manila")]
	AsiaManila,
	[Display(Name="Asia/Nicosia")]
	AsiaNicosia,
	[Display(Name="Asia/Novokuznetsk")]
	AsiaNovokuznetsk,
	[Display(Name="Asia/Novosibirsk")]
	AsiaNovosibirsk,
	[Display(Name="Asia/Omsk")]
	AsiaOmsk,
	[Display(Name="Asia/Oral")]
	AsiaOral,
	[Display(Name="Asia/Pontianak")]
	AsiaPontianak,
	[Display(Name="Asia/Pyongyang")]
	AsiaPyongyang,
	[Display(Name="Asia/Qatar")]
	AsiaQatar,
	[Display(Name="Asia/Qyzylorda")]
	AsiaQyzylorda,
	[Display(Name="Asia/Riyadh")]
	AsiaRiyadh,
	[Display(Name="Asia/Sakhalin")]
	AsiaSakhalin,
	[Display(Name="Asia/Samarkand")]
	AsiaSamarkand,
	[Display(Name="Asia/Seoul")]
	AsiaSeoul,
	[Display(Name="Asia/Shanghai")]
	AsiaShanghai,
	[Display(Name="Asia/Singapore")]
	AsiaSingapore,
	[Display(Name="Asia/Srednekolymsk")]
	AsiaSrednekolymsk,
	[Display(Name="Asia/Taipei")]
	AsiaTaipei,
	[Display(Name="Asia/Tashkent")]
	AsiaTashkent,
	[Display(Name="Asia/Tbilisi")]
	AsiaTbilisi,
	[Display(Name="Asia/Tehran")]
	AsiaTehran,
	[Display(Name="Asia/Thimphu")]
	AsiaThimphu,
	[Display(Name="Asia/Tokyo")]
	AsiaTokyo,
	[Display(Name="Asia/Tomsk")]
	AsiaTomsk,
	[Display(Name="Asia/Ulaanbaatar")]
	AsiaUlaanbaatar,
	[Display(Name="Asia/Urumqi")]
	AsiaUrumqi,
	[Display(Name="Asia/Ust-Nera")]
	AsiaUstMinusNera,
	[Display(Name="Asia/Vladivostok")]
	AsiaVladivostok,
	[Display(Name="Asia/Yakutsk")]
	AsiaYakutsk,
	[Display(Name="Asia/Yangon")]
	AsiaYangon,
	[Display(Name="Asia/Yekaterinburg")]
	AsiaYekaterinburg,
	[Display(Name="Asia/Yerevan")]
	AsiaYerevan,
	[Display(Name="Atlantic/Azores")]
	AtlanticAzores,
	[Display(Name="Atlantic/Bermuda")]
	AtlanticBermuda,
	[Display(Name="Atlantic/Canary")]
	AtlanticCanary,
	[Display(Name="Atlantic/Cape_Verde")]
	AtlanticCapeVerde,
	[Display(Name="Atlantic/Faroe")]
	AtlanticFaroe,
	[Display(Name="Atlantic/Madeira")]
	AtlanticMadeira,
	[Display(Name="Atlantic/Reykjavik")]
	AtlanticReykjavik,
	[Display(Name="Atlantic/South_Georgia")]
	AtlanticSouthGeorgia,
	[Display(Name="Atlantic/Stanley")]
	AtlanticStanley,
	[Display(Name="Australia/Adelaide")]
	AustraliaAdelaide,
	[Display(Name="Australia/Brisbane")]
	AustraliaBrisbane,
	[Display(Name="Australia/Broken_Hill")]
	AustraliaBrokenHill,
	[Display(Name="Australia/Currie")]
	AustraliaCurrie,
	[Display(Name="Australia/Darwin")]
	AustraliaDarwin,
	[Display(Name="Australia/Eucla")]
	AustraliaEucla,
	[Display(Name="Australia/Hobart")]
	AustraliaHobart,
	[Display(Name="Australia/Lindeman")]
	AustraliaLindeman,
	[Display(Name="Australia/Lord_Howe")]
	AustraliaLordHowe,
	[Display(Name="Australia/Melbourne")]
	AustraliaMelbourne,
	[Display(Name="Australia/Perth")]
	AustraliaPerth,
	[Display(Name="Australia/Sydney")]
	AustraliaSydney,
	[Display(Name="Europe/Amsterdam")]
	EuropeAmsterdam,
	[Display(Name="Europe/Andorra")]
	EuropeAndorra,
	[Display(Name="Europe/Astrakhan")]
	EuropeAstrakhan,
	[Display(Name="Europe/Athens")]
	EuropeAthens,
	[Display(Name="Europe/Belgrade")]
	EuropeBelgrade,
	[Display(Name="Europe/Berlin")]
	EuropeBerlin,
	[Display(Name="Europe/Brussels")]
	EuropeBrussels,
	[Display(Name="Europe/Bucharest")]
	EuropeBucharest,
	[Display(Name="Europe/Budapest")]
	EuropeBudapest,
	[Display(Name="Europe/Chisinau")]
	EuropeChisinau,
	[Display(Name="Europe/Copenhagen")]
	EuropeCopenhagen,
	[Display(Name="Europe/Dublin")]
	EuropeDublin,
	[Display(Name="Europe/Gibraltar")]
	EuropeGibraltar,
	[Display(Name="Europe/Helsinki")]
	EuropeHelsinki,
	[Display(Name="Europe/Istanbul")]
	EuropeIstanbul,
	[Display(Name="Europe/Kaliningrad")]
	EuropeKaliningrad,
	[Display(Name="Europe/Kiev")]
	EuropeKiev,
	[Display(Name="Europe/Kirov")]
	EuropeKirov,
	[Display(Name="Europe/Lisbon")]
	EuropeLisbon,
	[Display(Name="Europe/London")]
	EuropeLondon,
	[Display(Name="Europe/Luxembourg")]
	EuropeLuxembourg,
	[Display(Name="Europe/Madrid")]
	EuropeMadrid,
	[Display(Name="Europe/Malta")]
	EuropeMalta,
	[Display(Name="Europe/Minsk")]
	EuropeMinsk,
	[Display(Name="Europe/Monaco")]
	EuropeMonaco,
	[Display(Name="Europe/Moscow")]
	EuropeMoscow,
	[Display(Name="Europe/Oslo")]
	EuropeOslo,
	[Display(Name="Europe/Paris")]
	EuropeParis,
	[Display(Name="Europe/Prague")]
	EuropePrague,
	[Display(Name="Europe/Riga")]
	EuropeRiga,
	[Display(Name="Europe/Rome")]
	EuropeRome,
	[Display(Name="Europe/Samara")]
	EuropeSamara,
	[Display(Name="Europe/Simferopol")]
	EuropeSimferopol,
	[Display(Name="Europe/Sofia")]
	EuropeSofia,
	[Display(Name="Europe/Stockholm")]
	EuropeStockholm,
	[Display(Name="Europe/Tallinn")]
	EuropeTallinn,
	[Display(Name="Europe/Tirane")]
	EuropeTirane,
	[Display(Name="Europe/Ulyanovsk")]
	EuropeUlyanovsk,
	[Display(Name="Europe/Uzhgorod")]
	EuropeUzhgorod,
	[Display(Name="Europe/Vienna")]
	EuropeVienna,
	[Display(Name="Europe/Vilnius")]
	EuropeVilnius,
	[Display(Name="Europe/Volgograd")]
	EuropeVolgograd,
	[Display(Name="Europe/Warsaw")]
	EuropeWarsaw,
	[Display(Name="Europe/Zaporozhye")]
	EuropeZaporozhye,
	[Display(Name="Europe/Zurich")]
	EuropeZurich,
	[Display(Name="Indian/Chagos")]
	IndianChagos,
	[Display(Name="Indian/Christmas")]
	IndianChristmas,
	[Display(Name="Indian/Cocos")]
	IndianCocos,
	[Display(Name="Indian/Kerguelen")]
	IndianKerguelen,
	[Display(Name="Indian/Mahe")]
	IndianMahe,
	[Display(Name="Indian/Maldives")]
	IndianMaldives,
	[Display(Name="Indian/Mauritius")]
	IndianMauritius,
	[Display(Name="Indian/Reunion")]
	IndianReunion,
	[Display(Name="Pacific/Apia")]
	PacificApia,
	[Display(Name="Pacific/Auckland")]
	PacificAuckland,
	[Display(Name="Pacific/Bougainville")]
	PacificBougainville,
	[Display(Name="Pacific/Chatham")]
	PacificChatham,
	[Display(Name="Pacific/Chuuk")]
	PacificChuuk,
	[Display(Name="Pacific/Easter")]
	PacificEaster,
	[Display(Name="Pacific/Efate")]
	PacificEfate,
	[Display(Name="Pacific/Enderbury")]
	PacificEnderbury,
	[Display(Name="Pacific/Fakaofo")]
	PacificFakaofo,
	[Display(Name="Pacific/Fiji")]
	PacificFiji,
	[Display(Name="Pacific/Funafuti")]
	PacificFunafuti,
	[Display(Name="Pacific/Galapagos")]
	PacificGalapagos,
	[Display(Name="Pacific/Gambier")]
	PacificGambier,
	[Display(Name="Pacific/Guadalcanal")]
	PacificGuadalcanal,
	[Display(Name="Pacific/Guam")]
	PacificGuam,
	[Display(Name="Pacific/Honolulu")]
	PacificHonolulu,
	[Display(Name="Pacific/Kiritimati")]
	PacificKiritimati,
	[Display(Name="Pacific/Kosrae")]
	PacificKosrae,
	[Display(Name="Pacific/Kwajalein")]
	PacificKwajalein,
	[Display(Name="Pacific/Majuro")]
	PacificMajuro,
	[Display(Name="Pacific/Marquesas")]
	PacificMarquesas,
	[Display(Name="Pacific/Nauru")]
	PacificNauru,
	[Display(Name="Pacific/Niue")]
	PacificNiue,
	[Display(Name="Pacific/Norfolk")]
	PacificNorfolk,
	[Display(Name="Pacific/Noumea")]
	PacificNoumea,
	[Display(Name="Pacific/Pago_Pago")]
	PacificPagoPago,
	[Display(Name="Pacific/Palau")]
	PacificPalau,
	[Display(Name="Pacific/Pitcairn")]
	PacificPitcairn,
	[Display(Name="Pacific/Pohnpei")]
	PacificPohnpei,
	[Display(Name="Pacific/Port_Moresby")]
	PacificPortMoresby,
	[Display(Name="Pacific/Rarotonga")]
	PacificRarotonga,
	[Display(Name="Pacific/Tahiti")]
	PacificTahiti,
	[Display(Name="Pacific/Tarawa")]
	PacificTarawa,
	[Display(Name="Pacific/Tongatapu")]
	PacificTongatapu,
	[Display(Name="Pacific/Wake")]
	PacificWake,
	[Display(Name="Pacific/Wallis")]
	PacificWallis
}
}
