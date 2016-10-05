/* autogenerated from version 0.0.0.1 05/10/2016 15:40:23 */

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
	[ValidatePattern(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$")]
	[Display(Name="date of birth")]
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
    public decimal lat {get;set;}
	[Display(Name="longitude")]
    public decimal lng {get;set;}
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
    [JsonProperty("dateStart")]
    public string _dateStart {get;set;}
    [JsonProperty("_dateStart")]
    [JsonIgnore]
 
                 public DateTime dateStart
        {
            get
            {
                if (_dateStart != null)
                {
                    return
                         DateTime.ParseExact(_dateStart,
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
                    _dateStart = value.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                }
                catch { _dateStart = null; }
            }
        }
                [JsonProperty("dateEnd")]
    public string _dateEnd {get;set;}
    [JsonProperty("_dateEnd")]
    [JsonIgnore]
 
                 public DateTime dateEnd
        {
            get
            {
                if (_dateEnd != null)
                {
                    return
                         DateTime.ParseExact(_dateEnd,
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
                    _dateEnd = value.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                }
                catch { _dateEnd = null; }
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
	[Display(Name="Acre Time")]
	AcreTime,
	[Display(Name="Afghanistan Time")]
	AfghanistanTime,
	[Display(Name="Alaska Standard Time")]
	AlaskaStandardTime,
	[Display(Name="Alma-Ata Time")]
	AlmaMinusAtaTime,
	[Display(Name="Amazon Time")]
	AmazonTime,
	[Display(Name="Anadyr Time")]
	AnadyrTime,
	[Display(Name="Aqtau Time")]
	AqtauTime,
	[Display(Name="Aqtobe Time")]
	AqtobeTime,
	[Display(Name="Arabia Standard Time")]
	ArabiaStandardTime,
	[Display(Name="Argentine Time")]
	ArgentineTime,
	[Display(Name="Armenia Time")]
	ArmeniaTime,
	[Display(Name="Atlantic Standard Time")]
	AtlanticStandardTime,
	[Display(Name="Australian Central Standard Time (Northern Territory)")]
	AustralianCentralStandardTimeNorthernTerritory,
	[Display(Name="Australian Central Standard Time (South Australia)")]
	AustralianCentralStandardTimeSouthAustralia,
	[Display(Name="Australian Central Standard Time (South Australia/New South Wales)")]
	AustralianCentralStandardTimeSouthAustraliaNewSouthWales,
	[Display(Name="Australian Central Western Standard Time")]
	AustralianCentralWesternStandardTime,
	[Display(Name="Australian Eastern Standard Time (New South Wales)")]
	AustralianEasternStandardTimeNewSouthWales,
	[Display(Name="Australian Eastern Standard Time (Queensland)")]
	AustralianEasternStandardTimeQueensland,
	[Display(Name="Australian Eastern Standard Time (Tasmania)")]
	AustralianEasternStandardTimeTasmania,
	[Display(Name="Australian Eastern Standard Time (Victoria)")]
	AustralianEasternStandardTimeVictoria,
	[Display(Name="Australian Western Standard Time")]
	AustralianWesternStandardTime,
	[Display(Name="Azerbaijan Time")]
	AzerbaijanTime,
	[Display(Name="Azores Time")]
	AzoresTime,
	[Display(Name="Bangladesh Time")]
	BangladeshTime,
	[Display(Name="Bhutan Time")]
	BhutanTime,
	[Display(Name="Bolivia Time")]
	BoliviaTime,
	[Display(Name="Bougainville Standard Time")]
	BougainvilleStandardTime,
	[Display(Name="Brasilia Time")]
	BrasiliaTime,
	[Display(Name="Brunei Time")]
	BruneiTime,
	[Display(Name="Cape Verde Time")]
	CapeVerdeTime,
	[Display(Name="Central African Time")]
	CentralAfricanTime,
	[Display(Name="Central European Time")]
	CentralEuropeanTime,
	[Display(Name="Central Indonesia Time")]
	CentralIndonesiaTime,
	[Display(Name="Central Standard Time")]
	CentralStandardTime,
	[Display(Name="Chamorro Standard Time")]
	ChamorroStandardTime,
	[Display(Name="Chatham Standard Time")]
	ChathamStandardTime,
	[Display(Name="Chile Time")]
	ChileTime,
	[Display(Name="China Standard Time")]
	ChinaStandardTime,
	[Display(Name="Choibalsan Time")]
	ChoibalsanTime,
	[Display(Name="Christmas Island Time")]
	ChristmasIslandTime,
	[Display(Name="Chuuk Time")]
	ChuukTime,
	[Display(Name="Cocos Islands Time")]
	CocosIslandsTime,
	[Display(Name="Colombia Time")]
	ColombiaTime,
	[Display(Name="Cook Is. Time")]
	CookIsTime,
	[Display(Name="Coordinated Universal Time")]
	CoordinatedUniversalTime,
	[Display(Name="Cuba Standard Time")]
	CubaStandardTime,
	[Display(Name="Davis Time")]
	DavisTime,
	[Display(Name="Dumont-d'Urville Time")]
	DumontMinusdUrvilleTime,
	[Display(Name="East Indonesia Time")]
	EastIndonesiaTime,
	[Display(Name="Easter Is. Time")]
	EasterIsTime,
	[Display(Name="Eastern African Time")]
	EasternAfricanTime,
	[Display(Name="Eastern European Time")]
	EasternEuropeanTime,
	[Display(Name="Eastern Greenland Time")]
	EasternGreenlandTime,
	[Display(Name="Eastern Standard Time")]
	EasternStandardTime,
	[Display(Name="Ecuador Time")]
	EcuadorTime,
	[Display(Name="Falkland Is. Time")]
	FalklandIsTime,
	[Display(Name="Fernando de Noronha Time")]
	FernandodeNoronhaTime,
	[Display(Name="Fiji Time")]
	FijiTime,
	[Display(Name="French Guiana Time")]
	FrenchGuianaTime,
	[Display(Name="French Southern & Antarctic Lands Time")]
	FrenchSouthernAntarcticLandsTime,
	[Display(Name="GMT+01:00")]
	GMT0100,
	[Display(Name="GMT+02:00")]
	GMT0200,
	[Display(Name="GMT+03:00")]
	GMT0300,
	[Display(Name="GMT+04:00")]
	GMT0400,
	[Display(Name="GMT+05:00")]
	GMT0500,
	[Display(Name="GMT+06:00")]
	GMT0600,
	[Display(Name="GMT+07:00")]
	GMT0700,
	[Display(Name="GMT+08:00")]
	GMT0800,
	[Display(Name="GMT+09:00")]
	GMT0900,
	[Display(Name="GMT+10:00")]
	GMT1000,
	[Display(Name="GMT+11:00")]
	GMT1100,
	[Display(Name="GMT+12:00")]
	GMT1200,
	[Display(Name="GMT+13:00")]
	GMT1300,
	[Display(Name="GMT+14:00")]
	GMT1400,
	[Display(Name="GMT-01:00")]
	GMTMinus0100,
	[Display(Name="GMT-02:00")]
	GMTMinus0200,
	[Display(Name="GMT-03:00")]
	GMTMinus0300,
	[Display(Name="GMT-04:00")]
	GMTMinus0400,
	[Display(Name="GMT-05:00")]
	GMTMinus0500,
	[Display(Name="GMT-06:00")]
	GMTMinus0600,
	[Display(Name="GMT-07:00")]
	GMTMinus0700,
	[Display(Name="GMT-08:00")]
	GMTMinus0800,
	[Display(Name="GMT-09:00")]
	GMTMinus0900,
	[Display(Name="GMT-10:00")]
	GMTMinus1000,
	[Display(Name="GMT-11:00")]
	GMTMinus1100,
	[Display(Name="GMT-12:00")]
	GMTMinus1200,
	[Display(Name="Galapagos Time")]
	GalapagosTime,
	[Display(Name="Gambier Time")]
	GambierTime,
	[Display(Name="Georgia Time")]
	GeorgiaTime,
	[Display(Name="Ghana Mean Time")]
	GhanaMeanTime,
	[Display(Name="Gilbert Is. Time")]
	GilbertIsTime,
	[Display(Name="Greenwich Mean Time")]
	GreenwichMeanTime,
	[Display(Name="Gulf Standard Time")]
	GulfStandardTime,
	[Display(Name="Guyana Time")]
	GuyanaTime,
	[Display(Name="Hawaii Standard Time")]
	HawaiiStandardTime,
	[Display(Name="Hong Kong Time")]
	HongKongTime,
	[Display(Name="Hovd Time")]
	HovdTime,
	[Display(Name="India Standard Time")]
	IndiaStandardTime,
	[Display(Name="Indian Ocean Territory Time")]
	IndianOceanTerritoryTime,
	[Display(Name="Indochina Time")]
	IndochinaTime,
	[Display(Name="Iran Standard Time")]
	IranStandardTime,
	[Display(Name="Irkutsk Time")]
	IrkutskTime,
	[Display(Name="Israel Standard Time")]
	IsraelStandardTime,
	[Display(Name="Japan Standard Time")]
	JapanStandardTime,
	[Display(Name="Khandyga Time")]
	KhandygaTime,
	[Display(Name="Kirgizstan Time")]
	KirgizstanTime,
	[Display(Name="Korea Standard Time")]
	KoreaStandardTime,
	[Display(Name="Kosrae Time")]
	KosraeTime,
	[Display(Name="Krasnoyarsk Time")]
	KrasnoyarskTime,
	[Display(Name="Line Is. Time")]
	LineIsTime,
	[Display(Name="Lord Howe Standard Time")]
	LordHoweStandardTime,
	[Display(Name="Macquarie Island Standard Time")]
	MacquarieIslandStandardTime,
	[Display(Name="Magadan Time")]
	MagadanTime,
	[Display(Name="Malaysia Time")]
	MalaysiaTime,
	[Display(Name="Maldives Time")]
	MaldivesTime,
	[Display(Name="Marquesas Time")]
	MarquesasTime,
	[Display(Name="Marshall Islands Time")]
	MarshallIslandsTime,
	[Display(Name="Mauritius Time")]
	MauritiusTime,
	[Display(Name="Mawson Time")]
	MawsonTime,
	[Display(Name="Middle Europe Time")]
	MiddleEuropeTime,
	[Display(Name="Moscow Standard Time")]
	MoscowStandardTime,
	[Display(Name="Mountain Standard Time")]
	MountainStandardTime,
	[Display(Name="Myanmar Time")]
	MyanmarTime,
	[Display(Name="Nauru Time")]
	NauruTime,
	[Display(Name="Nepal Time")]
	NepalTime,
	[Display(Name="New Caledonia Time")]
	NewCaledoniaTime,
	[Display(Name="New Zealand Standard Time")]
	NewZealandStandardTime,
	[Display(Name="Newfoundland Standard Time")]
	NewfoundlandStandardTime,
	[Display(Name="Niue Time")]
	NiueTime,
	[Display(Name="Norfolk Time")]
	NorfolkTime,
	[Display(Name="Novosibirsk Time")]
	NovosibirskTime,
	[Display(Name="Omsk Time")]
	OmskTime,
	[Display(Name="Oral Time")]
	OralTime,
	[Display(Name="Pacific Standard Time")]
	PacificStandardTime,
	[Display(Name="Pakistan Time")]
	PakistanTime,
	[Display(Name="Palau Time")]
	PalauTime,
	[Display(Name="Papua New Guinea Time")]
	PapuaNewGuineaTime,
	[Display(Name="Paraguay Time")]
	ParaguayTime,
	[Display(Name="Peru Time")]
	PeruTime,
	[Display(Name="Petropavlovsk-Kamchatski Time")]
	PetropavlovskMinusKamchatskiTime,
	[Display(Name="Philippines Time")]
	PhilippinesTime,
	[Display(Name="Phoenix Is. Time")]
	PhoenixIsTime,
	[Display(Name="Pierre & Miquelon Standard Time")]
	PierreMiquelonStandardTime,
	[Display(Name="Pitcairn Standard Time")]
	PitcairnStandardTime,
	[Display(Name="Pohnpei Time")]
	PohnpeiTime,
	[Display(Name="Qyzylorda Time")]
	QyzylordaTime,
	[Display(Name="Reunion Time")]
	ReunionTime,
	[Display(Name="Rothera Time")]
	RotheraTime,
	[Display(Name="Sakhalin Time")]
	SakhalinTime,
	[Display(Name="Samara Time")]
	SamaraTime,
	[Display(Name="Samoa Standard Time")]
	SamoaStandardTime,
	[Display(Name="Seychelles Time")]
	SeychellesTime,
	[Display(Name="Singapore Time")]
	SingaporeTime,
	[Display(Name="Solomon Is. Time")]
	SolomonIsTime,
	[Display(Name="South Africa Standard Time")]
	SouthAfricaStandardTime,
	[Display(Name="South Georgia Standard Time")]
	SouthGeorgiaStandardTime,
	[Display(Name="Srednekolymsk Time")]
	SrednekolymskTime,
	[Display(Name="Suriname Time")]
	SurinameTime,
	[Display(Name="Syowa Time")]
	SyowaTime,
	[Display(Name="Tahiti Time")]
	TahitiTime,
	[Display(Name="Tajikistan Time")]
	TajikistanTime,
	[Display(Name="Timor-Leste Time")]
	TimorMinusLesteTime,
	[Display(Name="Tokelau Time")]
	TokelauTime,
	[Display(Name="Tonga Time")]
	TongaTime,
	[Display(Name="Turkmenistan Time")]
	TurkmenistanTime,
	[Display(Name="Tuvalu Time")]
	TuvaluTime,
	[Display(Name="Ulaanbaatar Time")]
	UlaanbaatarTime,
	[Display(Name="Uruguay Time")]
	UruguayTime,
	[Display(Name="Ust-Nera Time")]
	UstMinusNeraTime,
	[Display(Name="Uzbekistan Time")]
	UzbekistanTime,
	[Display(Name="Vanuatu Time")]
	VanuatuTime,
	[Display(Name="Venezuela Time")]
	VenezuelaTime,
	[Display(Name="Vladivostok Time")]
	VladivostokTime,
	[Display(Name="Vostok Time")]
	VostokTime,
	[Display(Name="Wake Time")]
	WakeTime,
	[Display(Name="Wallis & Futuna Time")]
	WallisFutunaTime,
	[Display(Name="West Indonesia Time")]
	WestIndonesiaTime,
	[Display(Name="West Samoa Standard Time")]
	WestSamoaStandardTime,
	[Display(Name="Western African Time")]
	WesternAfricanTime,
	[Display(Name="Western European Time")]
	WesternEuropeanTime,
	[Display(Name="Western Greenland Time")]
	WesternGreenlandTime,
	[Display(Name="Xinjiang Standard Time")]
	XinjiangStandardTime,
	[Display(Name="Yakutsk Time")]
	YakutskTime,
	[Display(Name="Yekaterinburg Time")]
	YekaterinburgTime
}
}
