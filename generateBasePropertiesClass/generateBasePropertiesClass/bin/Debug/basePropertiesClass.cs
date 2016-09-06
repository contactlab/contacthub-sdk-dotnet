using System;
using System.Collections.Generic;
/* version 0.0.0.1 */

namespace ContactHubSdklibrary {

[AttributeUsage(AttributeTargets.Field)]
public class EnumDisplayNameAttribute : System.ComponentModel.DisplayNameAttribute
{
	public EnumDisplayNameAttribute(string data) : base(data) { }
}
			
public class FieldDisplayNameAttribute : System.ComponentModel.DisplayNameAttribute
{
	public FieldDisplayNameAttribute(string data) : base(data) { }
}
			
public class ValidatePatternAttribute : System.ComponentModel.DisplayNameAttribute
{
	public ValidatePatternAttribute(string data) : base(data) { }
}
			
public class BaseProperties
{
	[FieldDisplayName("the picture url of customer")]
	public string pictureUrl {get;set;}
	[FieldDisplayName("the title")]
	public string title {get;set;}
	[FieldDisplayName("the prefix")]
	public string prefix {get;set;}
	[FieldDisplayName("the first name")]
	public string firstName {get;set;}
	[FieldDisplayName("the last name")]
	public string lastName {get;set;}
	[FieldDisplayName("the middle name")]
	public string middleName {get;set;}
	[FieldDisplayName("gender")]
	public string gender {get;set;}
	[ValidatePattern(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$")]
	[FieldDisplayName("dob")]
	public string dob {get;set;}
	[ValidatePattern(@"^[a-z]{2}(_([a-zA-Z]{2}){1,2})?_[A-Z]{2}$")]
	[FieldDisplayName("the locale")]
	public string locale {get;set;}
	[FieldDisplayName("the time zone")]
	public BasePropertiesTimezoneEnum timezone {get;set;}
	public Contacts contacts {get;set;}
	public Address address {get;set;}
	public Credential credential {get;set;}
	[FieldDisplayName("educations")]
	public List<Educations> educations {get;set;}
	[FieldDisplayName("likes")]
	public List<Likes> likes {get;set;}
	public SocialProfile socialProfile {get;set;}
	[FieldDisplayName("jobs")]
	public List<Jobs> jobs {get;set;}
	[FieldDisplayName("subscriptions")]
	public List<Subscriptions> subscriptions {get;set;}
}


public class Contacts
{
	[FieldDisplayName("the e-mail")]
	public string email {get;set;}
	[FieldDisplayName("the fax number")]
	public string fax {get;set;}
	[FieldDisplayName("the mobile phone number")]
	public string mobilePhone {get;set;}
	[FieldDisplayName("the phone")]
	public string phone {get;set;}
	[FieldDisplayName("other contacts")]
	public List<OtherContacts> otherContacts {get;set;}
	[FieldDisplayName("mobile device")]
	public List<MobileDevice> mobileDevice {get;set;}
}


public class OtherContacts
{
	public string name {get;set;}
	public OtherContactsTypeEnum type {get;set;}
	public string value {get;set;}
}

public enum OtherContactsTypeEnum {
	[EnumDisplayName("MOBILE")]
	MOBILE,
	[EnumDisplayName("PHONE")]
	PHONE,
	[EnumDisplayName("EMAIL")]
	EMAIL,
	[EnumDisplayName("FAX")]
	FAX,
	[EnumDisplayName("OTHER")]
	OTHER
}
public class MobileDevice
{
	public string identifier {get;set;}
	public string name {get;set;}
	public MobileDeviceTypeEnum type {get;set;}
}

public enum MobileDeviceTypeEnum {
	[EnumDisplayName("IOS")]
	IOS,
	[EnumDisplayName("GCM")]
	GCM,
	[EnumDisplayName("WP")]
	WP
}
public class Address
{
	[FieldDisplayName("the street")]
	public string street {get;set;}
	[FieldDisplayName("the city")]
	public string city {get;set;}
	[FieldDisplayName("the country")]
	public string country {get;set;}
	[FieldDisplayName("the province")]
	public string province {get;set;}
	[FieldDisplayName("the zip code")]
	public string zip {get;set;}
	public Geo geo {get;set;}
}


public class Geo
{
	[FieldDisplayName("latitude")]
	public decimal lat {get;set;}
	[FieldDisplayName("longitude")]
	public decimal lng {get;set;}
}


public class Credential
{
	[FieldDisplayName("the password")]
	public string password {get;set;}
	[FieldDisplayName("the user name")]
	public string username {get;set;}
}


public class Educations
{
	public string id {get;set;}
	public EducationsSchoolTypeEnum schoolType {get;set;}
	public string schoolName {get;set;}
	public string schoolConcentration {get;set;}
}

public enum EducationsSchoolTypeEnum {
	[EnumDisplayName("PRIMARY_SCHOOL")]
	PRIMARYSCHOOL,
	[EnumDisplayName("SECONDARY_SCHOOL")]
	SECONDARYSCHOOL,
	[EnumDisplayName("HIGH_SCHOOL")]
	HIGHSCHOOL,
	[EnumDisplayName("COLLEGE")]
	COLLEGE,
	[EnumDisplayName("OTHER")]
	OTHER
}
public class Likes
{
	public string id {get;set;}
	public string category {get;set;}
	public string name {get;set;}
	public string createdTime {get;set;}
}


public class SocialProfile
{
	[FieldDisplayName("facebook")]
	public string facebook {get;set;}
	[FieldDisplayName("google+")]
	public string @google {get;set;}
	[FieldDisplayName("instagram")]
	public string instagram {get;set;}
	[FieldDisplayName("linkedin")]
	public string linkedin {get;set;}
	[FieldDisplayName("qzone")]
	public string qzone {get;set;}
	[FieldDisplayName("twitter")]
	public string twitter {get;set;}
}


public class Jobs
{
	public string id {get;set;}
	public string companyIndustry {get;set;}
	public string companyName {get;set;}
	public string jobTitle {get;set;}
	[ValidatePattern(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$")]
	public string start_date {get;set;}
	[ValidatePattern(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$")]
	public string end_date {get;set;}
}


public class Subscriptions
{
	public string id {get;set;}
	public string name {get;set;}
	public string type {get;set;}
	public SubscriptionsKindEnum kind {get;set;}
	public string dateStart {get;set;}
	public string dateEnd {get;set;}
	public string subscriberId {get;set;}
	public string registeredAt {get;set;}
	public string updatedAt {get;set;}
	public List<Preferences> preferences {get;set;}
}


public class Preferences
{
	public string key {get;set;}
	public string value {get;set;}
}

public enum SubscriptionsKindEnum {
	[EnumDisplayName("DIGITAL_MESSAGE")]
	DIGITALMESSAGE,
	[EnumDisplayName("SERVICE")]
	SERVICE,
	[EnumDisplayName("OTHER")]
	OTHER
}public enum BasePropertiesTimezoneEnum {
	[EnumDisplayName("Acre Time")]
	AcreTime,
	[EnumDisplayName("Afghanistan Time")]
	AfghanistanTime,
	[EnumDisplayName("Alaska Standard Time")]
	AlaskaStandardTime,
	[EnumDisplayName("Alma-Ata Time")]
	AlmaMinusAtaTime,
	[EnumDisplayName("Amazon Time")]
	AmazonTime,
	[EnumDisplayName("Anadyr Time")]
	AnadyrTime,
	[EnumDisplayName("Aqtau Time")]
	AqtauTime,
	[EnumDisplayName("Aqtobe Time")]
	AqtobeTime,
	[EnumDisplayName("Arabia Standard Time")]
	ArabiaStandardTime,
	[EnumDisplayName("Argentine Time")]
	ArgentineTime,
	[EnumDisplayName("Armenia Time")]
	ArmeniaTime,
	[EnumDisplayName("Atlantic Standard Time")]
	AtlanticStandardTime,
	[EnumDisplayName("Australian Central Standard Time (Northern Territory)")]
	AustralianCentralStandardTimeNorthernTerritory,
	[EnumDisplayName("Australian Central Standard Time (South Australia)")]
	AustralianCentralStandardTimeSouthAustralia,
	[EnumDisplayName("Australian Central Standard Time (South Australia/New South Wales)")]
	AustralianCentralStandardTimeSouthAustraliaNewSouthWales,
	[EnumDisplayName("Australian Central Western Standard Time")]
	AustralianCentralWesternStandardTime,
	[EnumDisplayName("Australian Eastern Standard Time (New South Wales)")]
	AustralianEasternStandardTimeNewSouthWales,
	[EnumDisplayName("Australian Eastern Standard Time (Queensland)")]
	AustralianEasternStandardTimeQueensland,
	[EnumDisplayName("Australian Eastern Standard Time (Tasmania)")]
	AustralianEasternStandardTimeTasmania,
	[EnumDisplayName("Australian Eastern Standard Time (Victoria)")]
	AustralianEasternStandardTimeVictoria,
	[EnumDisplayName("Australian Western Standard Time")]
	AustralianWesternStandardTime,
	[EnumDisplayName("Azerbaijan Time")]
	AzerbaijanTime,
	[EnumDisplayName("Azores Time")]
	AzoresTime,
	[EnumDisplayName("Bangladesh Time")]
	BangladeshTime,
	[EnumDisplayName("Bhutan Time")]
	BhutanTime,
	[EnumDisplayName("Bolivia Time")]
	BoliviaTime,
	[EnumDisplayName("Bougainville Standard Time")]
	BougainvilleStandardTime,
	[EnumDisplayName("Brasilia Time")]
	BrasiliaTime,
	[EnumDisplayName("Brunei Time")]
	BruneiTime,
	[EnumDisplayName("Cape Verde Time")]
	CapeVerdeTime,
	[EnumDisplayName("Central African Time")]
	CentralAfricanTime,
	[EnumDisplayName("Central European Time")]
	CentralEuropeanTime,
	[EnumDisplayName("Central Indonesia Time")]
	CentralIndonesiaTime,
	[EnumDisplayName("Central Standard Time")]
	CentralStandardTime,
	[EnumDisplayName("Chamorro Standard Time")]
	ChamorroStandardTime,
	[EnumDisplayName("Chatham Standard Time")]
	ChathamStandardTime,
	[EnumDisplayName("Chile Time")]
	ChileTime,
	[EnumDisplayName("China Standard Time")]
	ChinaStandardTime,
	[EnumDisplayName("Choibalsan Time")]
	ChoibalsanTime,
	[EnumDisplayName("Christmas Island Time")]
	ChristmasIslandTime,
	[EnumDisplayName("Chuuk Time")]
	ChuukTime,
	[EnumDisplayName("Cocos Islands Time")]
	CocosIslandsTime,
	[EnumDisplayName("Colombia Time")]
	ColombiaTime,
	[EnumDisplayName("Cook Is. Time")]
	CookIsTime,
	[EnumDisplayName("Coordinated Universal Time")]
	CoordinatedUniversalTime,
	[EnumDisplayName("Cuba Standard Time")]
	CubaStandardTime,
	[EnumDisplayName("Davis Time")]
	DavisTime,
	[EnumDisplayName("Dumont-d'Urville Time")]
	DumontMinusdUrvilleTime,
	[EnumDisplayName("East Indonesia Time")]
	EastIndonesiaTime,
	[EnumDisplayName("Easter Is. Time")]
	EasterIsTime,
	[EnumDisplayName("Eastern African Time")]
	EasternAfricanTime,
	[EnumDisplayName("Eastern European Time")]
	EasternEuropeanTime,
	[EnumDisplayName("Eastern Greenland Time")]
	EasternGreenlandTime,
	[EnumDisplayName("Eastern Standard Time")]
	EasternStandardTime,
	[EnumDisplayName("Ecuador Time")]
	EcuadorTime,
	[EnumDisplayName("Falkland Is. Time")]
	FalklandIsTime,
	[EnumDisplayName("Fernando de Noronha Time")]
	FernandodeNoronhaTime,
	[EnumDisplayName("Fiji Time")]
	FijiTime,
	[EnumDisplayName("French Guiana Time")]
	FrenchGuianaTime,
	[EnumDisplayName("French Southern & Antarctic Lands Time")]
	FrenchSouthernAntarcticLandsTime,
	[EnumDisplayName("GMT+01:00")]
	GMT0100,
	[EnumDisplayName("GMT+02:00")]
	GMT0200,
	[EnumDisplayName("GMT+03:00")]
	GMT0300,
	[EnumDisplayName("GMT+04:00")]
	GMT0400,
	[EnumDisplayName("GMT+05:00")]
	GMT0500,
	[EnumDisplayName("GMT+06:00")]
	GMT0600,
	[EnumDisplayName("GMT+07:00")]
	GMT0700,
	[EnumDisplayName("GMT+08:00")]
	GMT0800,
	[EnumDisplayName("GMT+09:00")]
	GMT0900,
	[EnumDisplayName("GMT+10:00")]
	GMT1000,
	[EnumDisplayName("GMT+11:00")]
	GMT1100,
	[EnumDisplayName("GMT+12:00")]
	GMT1200,
	[EnumDisplayName("GMT+13:00")]
	GMT1300,
	[EnumDisplayName("GMT+14:00")]
	GMT1400,
	[EnumDisplayName("GMT-01:00")]
	GMTMinus0100,
	[EnumDisplayName("GMT-02:00")]
	GMTMinus0200,
	[EnumDisplayName("GMT-03:00")]
	GMTMinus0300,
	[EnumDisplayName("GMT-04:00")]
	GMTMinus0400,
	[EnumDisplayName("GMT-05:00")]
	GMTMinus0500,
	[EnumDisplayName("GMT-06:00")]
	GMTMinus0600,
	[EnumDisplayName("GMT-07:00")]
	GMTMinus0700,
	[EnumDisplayName("GMT-08:00")]
	GMTMinus0800,
	[EnumDisplayName("GMT-09:00")]
	GMTMinus0900,
	[EnumDisplayName("GMT-10:00")]
	GMTMinus1000,
	[EnumDisplayName("GMT-11:00")]
	GMTMinus1100,
	[EnumDisplayName("GMT-12:00")]
	GMTMinus1200,
	[EnumDisplayName("Galapagos Time")]
	GalapagosTime,
	[EnumDisplayName("Gambier Time")]
	GambierTime,
	[EnumDisplayName("Georgia Time")]
	GeorgiaTime,
	[EnumDisplayName("Ghana Mean Time")]
	GhanaMeanTime,
	[EnumDisplayName("Gilbert Is. Time")]
	GilbertIsTime,
	[EnumDisplayName("Greenwich Mean Time")]
	GreenwichMeanTime,
	[EnumDisplayName("Gulf Standard Time")]
	GulfStandardTime,
	[EnumDisplayName("Guyana Time")]
	GuyanaTime,
	[EnumDisplayName("Hawaii Standard Time")]
	HawaiiStandardTime,
	[EnumDisplayName("Hong Kong Time")]
	HongKongTime,
	[EnumDisplayName("Hovd Time")]
	HovdTime,
	[EnumDisplayName("India Standard Time")]
	IndiaStandardTime,
	[EnumDisplayName("Indian Ocean Territory Time")]
	IndianOceanTerritoryTime,
	[EnumDisplayName("Indochina Time")]
	IndochinaTime,
	[EnumDisplayName("Iran Standard Time")]
	IranStandardTime,
	[EnumDisplayName("Irkutsk Time")]
	IrkutskTime,
	[EnumDisplayName("Israel Standard Time")]
	IsraelStandardTime,
	[EnumDisplayName("Japan Standard Time")]
	JapanStandardTime,
	[EnumDisplayName("Khandyga Time")]
	KhandygaTime,
	[EnumDisplayName("Kirgizstan Time")]
	KirgizstanTime,
	[EnumDisplayName("Korea Standard Time")]
	KoreaStandardTime,
	[EnumDisplayName("Kosrae Time")]
	KosraeTime,
	[EnumDisplayName("Krasnoyarsk Time")]
	KrasnoyarskTime,
	[EnumDisplayName("Line Is. Time")]
	LineIsTime,
	[EnumDisplayName("Lord Howe Standard Time")]
	LordHoweStandardTime,
	[EnumDisplayName("Macquarie Island Standard Time")]
	MacquarieIslandStandardTime,
	[EnumDisplayName("Magadan Time")]
	MagadanTime,
	[EnumDisplayName("Malaysia Time")]
	MalaysiaTime,
	[EnumDisplayName("Maldives Time")]
	MaldivesTime,
	[EnumDisplayName("Marquesas Time")]
	MarquesasTime,
	[EnumDisplayName("Marshall Islands Time")]
	MarshallIslandsTime,
	[EnumDisplayName("Mauritius Time")]
	MauritiusTime,
	[EnumDisplayName("Mawson Time")]
	MawsonTime,
	[EnumDisplayName("Middle Europe Time")]
	MiddleEuropeTime,
	[EnumDisplayName("Moscow Standard Time")]
	MoscowStandardTime,
	[EnumDisplayName("Mountain Standard Time")]
	MountainStandardTime,
	[EnumDisplayName("Myanmar Time")]
	MyanmarTime,
	[EnumDisplayName("Nauru Time")]
	NauruTime,
	[EnumDisplayName("Nepal Time")]
	NepalTime,
	[EnumDisplayName("New Caledonia Time")]
	NewCaledoniaTime,
	[EnumDisplayName("New Zealand Standard Time")]
	NewZealandStandardTime,
	[EnumDisplayName("Newfoundland Standard Time")]
	NewfoundlandStandardTime,
	[EnumDisplayName("Niue Time")]
	NiueTime,
	[EnumDisplayName("Norfolk Time")]
	NorfolkTime,
	[EnumDisplayName("Novosibirsk Time")]
	NovosibirskTime,
	[EnumDisplayName("Omsk Time")]
	OmskTime,
	[EnumDisplayName("Oral Time")]
	OralTime,
	[EnumDisplayName("Pacific Standard Time")]
	PacificStandardTime,
	[EnumDisplayName("Pakistan Time")]
	PakistanTime,
	[EnumDisplayName("Palau Time")]
	PalauTime,
	[EnumDisplayName("Papua New Guinea Time")]
	PapuaNewGuineaTime,
	[EnumDisplayName("Paraguay Time")]
	ParaguayTime,
	[EnumDisplayName("Peru Time")]
	PeruTime,
	[EnumDisplayName("Petropavlovsk-Kamchatski Time")]
	PetropavlovskMinusKamchatskiTime,
	[EnumDisplayName("Philippines Time")]
	PhilippinesTime,
	[EnumDisplayName("Phoenix Is. Time")]
	PhoenixIsTime,
	[EnumDisplayName("Pierre & Miquelon Standard Time")]
	PierreMiquelonStandardTime,
	[EnumDisplayName("Pitcairn Standard Time")]
	PitcairnStandardTime,
	[EnumDisplayName("Pohnpei Time")]
	PohnpeiTime,
	[EnumDisplayName("Qyzylorda Time")]
	QyzylordaTime,
	[EnumDisplayName("Reunion Time")]
	ReunionTime,
	[EnumDisplayName("Rothera Time")]
	RotheraTime,
	[EnumDisplayName("Sakhalin Time")]
	SakhalinTime,
	[EnumDisplayName("Samara Time")]
	SamaraTime,
	[EnumDisplayName("Samoa Standard Time")]
	SamoaStandardTime,
	[EnumDisplayName("Seychelles Time")]
	SeychellesTime,
	[EnumDisplayName("Singapore Time")]
	SingaporeTime,
	[EnumDisplayName("Solomon Is. Time")]
	SolomonIsTime,
	[EnumDisplayName("South Africa Standard Time")]
	SouthAfricaStandardTime,
	[EnumDisplayName("South Georgia Standard Time")]
	SouthGeorgiaStandardTime,
	[EnumDisplayName("Srednekolymsk Time")]
	SrednekolymskTime,
	[EnumDisplayName("Suriname Time")]
	SurinameTime,
	[EnumDisplayName("Syowa Time")]
	SyowaTime,
	[EnumDisplayName("Tahiti Time")]
	TahitiTime,
	[EnumDisplayName("Tajikistan Time")]
	TajikistanTime,
	[EnumDisplayName("Timor-Leste Time")]
	TimorMinusLesteTime,
	[EnumDisplayName("Tokelau Time")]
	TokelauTime,
	[EnumDisplayName("Tonga Time")]
	TongaTime,
	[EnumDisplayName("Turkmenistan Time")]
	TurkmenistanTime,
	[EnumDisplayName("Tuvalu Time")]
	TuvaluTime,
	[EnumDisplayName("Ulaanbaatar Time")]
	UlaanbaatarTime,
	[EnumDisplayName("Uruguay Time")]
	UruguayTime,
	[EnumDisplayName("Ust-Nera Time")]
	UstMinusNeraTime,
	[EnumDisplayName("Uzbekistan Time")]
	UzbekistanTime,
	[EnumDisplayName("Vanuatu Time")]
	VanuatuTime,
	[EnumDisplayName("Venezuela Time")]
	VenezuelaTime,
	[EnumDisplayName("Vladivostok Time")]
	VladivostokTime,
	[EnumDisplayName("Vostok Time")]
	VostokTime,
	[EnumDisplayName("Wake Time")]
	WakeTime,
	[EnumDisplayName("Wallis & Futuna Time")]
	WallisFutunaTime,
	[EnumDisplayName("West Indonesia Time")]
	WestIndonesiaTime,
	[EnumDisplayName("West Samoa Standard Time")]
	WestSamoaStandardTime,
	[EnumDisplayName("Western African Time")]
	WesternAfricanTime,
	[EnumDisplayName("Western European Time")]
	WesternEuropeanTime,
	[EnumDisplayName("Western Greenland Time")]
	WesternGreenlandTime,
	[EnumDisplayName("Xinjiang Standard Time")]
	XinjiangStandardTime,
	[EnumDisplayName("Yakutsk Time")]
	YakutskTime,
	[EnumDisplayName("Yekaterinburg Time")]
	YekaterinburgTime
}
}
