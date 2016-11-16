/* selfgenerated from version 0.0.0.1 16/11/2016 12:30:53 */

using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
namespace ContactHubSdkLibrary.Events {
public class EventBaseProperty {}
/// <summary>
/// Event class 'openedTicket': opened ticket
/// </summary>
public class EventPropertyOpenedTicket: EventBaseProperty
{
    public string ticketId {get;set;}
    public List<String> category {get;set;}
    public string subject {get;set;}
    public string text {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'repliedTicket': replied ticket
/// </summary>
public class EventPropertyRepliedTicket: EventBaseProperty
{
    public string ticketId {get;set;}
    public List<String> category {get;set;}
    public string subject {get;set;}
    public string text {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'closedTicket': closed ticket
/// </summary>
public class EventPropertyClosedTicket: EventBaseProperty
{
    public string ticketId {get;set;}
    public List<String> category {get;set;}
    public string subject {get;set;}
    public string text {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'loggedIn': logged in
/// </summary>
public class EventPropertyLoggedIn: EventBaseProperty
{
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'loggedOut': logged out
/// </summary>
public class EventPropertyLoggedOut: EventBaseProperty
{
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'reviewedProduct': reviewed product
/// </summary>
public class EventPropertyReviewedProduct: EventBaseProperty
{
    public string id {get;set;}
    public string sku {get;set;}
    public string name {get;set;}
    public decimal price {get;set;}
    //format: uri
    public string imageUrl {get;set;}
    //format: uri
    public string linkUrl {get;set;}
    public string shortDescription {get;set;}
    public List<String> category {get;set;}
    public List<Classifications> classifications {get;set;}
    public string rating {get;set;}
    public dynamic extraProperties {get;set;}
}


public class Classifications
{
    public string key {get;set;}
    public string value {get;set;}
}


/// <summary>
/// Event class 'viewedProductCategory': viewed product category
/// </summary>
public class EventPropertyViewedProductCategory: EventBaseProperty
{
    public string category {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'viewedProduct': viewed product
/// </summary>
public class EventPropertyViewedProduct: EventBaseProperty
{
    public string id {get;set;}
    public string sku {get;set;}
    public string name {get;set;}
    public decimal price {get;set;}
    //format: uri
    public string imageUrl {get;set;}
    //format: uri
    public string linkUrl {get;set;}
    public string shortDescription {get;set;}
    public List<String> category {get;set;}
    public List<Classifications> classifications {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'addedProduct': added product
/// </summary>
public class EventPropertyAddedProduct: EventBaseProperty
{
    public string id {get;set;}
    public string sku {get;set;}
    public string name {get;set;}
    public decimal price {get;set;}
    public decimal quantity {get;set;}
    //format: uri
    public string imageUrl {get;set;}
    //format: uri
    public string linkUrl {get;set;}
    public string shortDescription {get;set;}
    public List<String> category {get;set;}
    public List<Classifications> classifications {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'removedProduct': removed product
/// </summary>
public class EventPropertyRemovedProduct: EventBaseProperty
{
    public string id {get;set;}
    public string sku {get;set;}
    public string name {get;set;}
    public decimal price {get;set;}
    public decimal quantity {get;set;}
    //format: uri
    public string imageUrl {get;set;}
    //format: uri
    public string linkUrl {get;set;}
    public string shortDescription {get;set;}
    public List<String> category {get;set;}
    public List<Classifications> classifications {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'addedWishlist': added wishlist
/// </summary>
public class EventPropertyAddedWishlist: EventBaseProperty
{
    public string id {get;set;}
    public string sku {get;set;}
    public string name {get;set;}
    public decimal price {get;set;}
    //format: uri
    public string imageUrl {get;set;}
    //format: uri
    public string linkUrl {get;set;}
    public string shortDescription {get;set;}
    public List<String> category {get;set;}
    public List<Classifications> classifications {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'removedWishlist': removed wishlist
/// </summary>
public class EventPropertyRemovedWishlist: EventBaseProperty
{
    public string id {get;set;}
    public string sku {get;set;}
    public string name {get;set;}
    public decimal price {get;set;}
    //format: uri
    public string imageUrl {get;set;}
    //format: uri
    public string linkUrl {get;set;}
    public string shortDescription {get;set;}
    public List<String> category {get;set;}
    public List<Classifications> classifications {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'addedCompare': added compare
/// </summary>
public class EventPropertyAddedCompare: EventBaseProperty
{
    public string id {get;set;}
    public string sku {get;set;}
    public string name {get;set;}
    public decimal price {get;set;}
    //format: uri
    public string imageUrl {get;set;}
    //format: uri
    public string linkUrl {get;set;}
    public string shortDescription {get;set;}
    public List<String> category {get;set;}
    public List<Classifications> classifications {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'removedCompare': removed compare
/// </summary>
public class EventPropertyRemovedCompare: EventBaseProperty
{
    public string id {get;set;}
    public string sku {get;set;}
    public string name {get;set;}
    public decimal price {get;set;}
    //format: uri
    public string imageUrl {get;set;}
    //format: uri
    public string linkUrl {get;set;}
    public string shortDescription {get;set;}
    public List<String> category {get;set;}
    public List<Classifications> classifications {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'completedOrder': completed order
/// </summary>
public class EventPropertyCompletedOrder: EventBaseProperty
{
    public string orderId {get;set;}
[JsonProperty("type")]public string _type {get;set;}
[JsonProperty("hidden_type")][JsonIgnore]
                    public EventPropertyCompletedOrderTypeEnum type 
            {
                get
                {
                        EventPropertyCompletedOrderTypeEnum enumValue =ContactHubSdkLibrary.EnumHelper<EventPropertyCompletedOrderTypeEnum>.GetValueFromDisplayName(_type);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<EventPropertyCompletedOrderTypeEnum>.GetDisplayValue(value);
                        _type = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public string storeCode {get;set;}
[JsonProperty("paymentMethod")]public string _paymentMethod {get;set;}
[JsonProperty("hidden_paymentMethod")][JsonIgnore]
                    public EventPropertyCompletedOrderPaymentMethodEnum paymentMethod 
            {
                get
                {
                        EventPropertyCompletedOrderPaymentMethodEnum enumValue =ContactHubSdkLibrary.EnumHelper<EventPropertyCompletedOrderPaymentMethodEnum>.GetValueFromDisplayName(_paymentMethod);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<EventPropertyCompletedOrderPaymentMethodEnum>.GetDisplayValue(value);
                        _paymentMethod = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public Amount amount {get;set;}
    public List<Products> products {get;set;}
    public dynamic extraProperties {get;set;}
}


public class Amount
{
    public decimal total {get;set;}
    public decimal revenue {get;set;}
    public decimal shipping {get;set;}
    public decimal tax {get;set;}
    public decimal discount {get;set;}
    public string currency {get;set;}
    public string localCurrency {get;set;}
    public decimal exchangeRate {get;set;}
}


public class Products
{
    public string id {get;set;}
[JsonProperty("type")]public string _type {get;set;}
[JsonProperty("hidden_type")][JsonIgnore]
                    public ProductsTypeEnum type 
            {
                get
                {
                        ProductsTypeEnum enumValue =ContactHubSdkLibrary.EnumHelper<ProductsTypeEnum>.GetValueFromDisplayName(_type);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<ProductsTypeEnum>.GetDisplayValue(value);
                        _type = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public string sku {get;set;}
    public string name {get;set;}
    public decimal price {get;set;}
    public decimal quantity {get;set;}
    public decimal discount {get;set;}
    public decimal tax {get;set;}
    public string coupon {get;set;}
    //format: uri
    public string imageUrl {get;set;}
    //format: uri
    public string linkUrl {get;set;}
    public string shortDescription {get;set;}
    public List<String> category {get;set;}
    public List<Classifications> classifications {get;set;}
}

public enum ProductsTypeEnum {
	NoValue,
	[Display(Name="sale")]
	sale,
	[Display(Name="return")]
	@return
}public enum EventPropertyCompletedOrderPaymentMethodEnum {
	NoValue,
	[Display(Name="cash")]
	cash,
	[Display(Name="creditcard")]
	creditcard,
	[Display(Name="debitcard")]
	debitcard,
	[Display(Name="paypal")]
	paypal,
	[Display(Name="other")]
	other
}public enum EventPropertyCompletedOrderTypeEnum {
	NoValue,
	[Display(Name="sale")]
	sale,
	[Display(Name="return")]
	@return,
	[Display(Name="sale-return")]
	saleMinusreturn
}
/// <summary>
/// Event class 'viewedPage': viewed page
/// </summary>
public class EventPropertyViewedPage: EventBaseProperty
{
    public string path {get;set;}
    public string referer {get;set;}
    public string search {get;set;}
    public string title {get;set;}
    //format: uri
    public string url {get;set;}
    public List<String> pageCategories {get;set;}
    public List<String> pageTags {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'clickedLink': clicked link
/// </summary>
public class EventPropertyClickedLink: EventBaseProperty
{
    public string path {get;set;}
    public string referer {get;set;}
    public string search {get;set;}
    public string title {get;set;}
    //format: uri
    public string url {get;set;}
    public List<String> linkTags {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'formCompiled': form compiled
/// </summary>
public class EventPropertyFormCompiled: EventBaseProperty
{
    public string formName {get;set;}
    public string formId {get;set;}
    public dynamic data {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'campaignSent': campaign sent
/// </summary>
public class EventPropertyCampaignSent: EventBaseProperty
{
    public string subscriberId {get;set;}
    public string campaignSubject {get;set;}
    public string campaignId {get;set;}
    public string campaignName {get;set;}
    public List<String> campaignTags {get;set;}
[JsonProperty("channel")]public string _channel {get;set;}
[JsonProperty("hidden_channel")][JsonIgnore]
                    public EventPropertyCampaignSentChannelEnum channel 
            {
                get
                {
                        EventPropertyCampaignSentChannelEnum enumValue =ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignSentChannelEnum>.GetValueFromDisplayName(_channel);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignSentChannelEnum>.GetDisplayValue(value);
                        _channel = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public dynamic extraProperties {get;set;}
}

public enum EventPropertyCampaignSentChannelEnum {
	NoValue,
	[Display(Name="SMS")]
	SMS,
	[Display(Name="EMAIL")]
	EMAIL,
	[Display(Name="PUSH")]
	PUSH,
	[Display(Name="FAX")]
	FAX
}
/// <summary>
/// Event class 'campaignOpened': campaign opened
/// </summary>
public class EventPropertyCampaignOpened: EventBaseProperty
{
    public string subscriberId {get;set;}
    public string campaignSubject {get;set;}
    public string campaignId {get;set;}
    public string campaignName {get;set;}
    public List<String> campaignTags {get;set;}
[JsonProperty("channel")]public string _channel {get;set;}
[JsonProperty("hidden_channel")][JsonIgnore]
                    public EventPropertyCampaignOpenedChannelEnum channel 
            {
                get
                {
                        EventPropertyCampaignOpenedChannelEnum enumValue =ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignOpenedChannelEnum>.GetValueFromDisplayName(_channel);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignOpenedChannelEnum>.GetDisplayValue(value);
                        _channel = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public dynamic extraProperties {get;set;}
}

public enum EventPropertyCampaignOpenedChannelEnum {
	NoValue,
	[Display(Name="SMS")]
	SMS,
	[Display(Name="EMAIL")]
	EMAIL,
	[Display(Name="PUSH")]
	PUSH,
	[Display(Name="FAX")]
	FAX
}
/// <summary>
/// Event class 'campaignLinkClicked': campaign link clicked
/// </summary>
public class EventPropertyCampaignLinkClicked: EventBaseProperty
{
    public string subscriberId {get;set;}
    public string campaignSubject {get;set;}
    public string campaignId {get;set;}
    public string campaignName {get;set;}
    public List<String> campaignTags {get;set;}
[JsonProperty("channel")]public string _channel {get;set;}
[JsonProperty("hidden_channel")][JsonIgnore]
                    public EventPropertyCampaignLinkClickedChannelEnum channel 
            {
                get
                {
                        EventPropertyCampaignLinkClickedChannelEnum enumValue =ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignLinkClickedChannelEnum>.GetValueFromDisplayName(_channel);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignLinkClickedChannelEnum>.GetDisplayValue(value);
                        _channel = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public string linkId {get;set;}
    //format: uri
    public string linkUrl {get;set;}
    public List<String> linkTags {get;set;}
    public dynamic extraProperties {get;set;}
}

public enum EventPropertyCampaignLinkClickedChannelEnum {
	NoValue,
	[Display(Name="SMS")]
	SMS,
	[Display(Name="EMAIL")]
	EMAIL,
	[Display(Name="PUSH")]
	PUSH,
	[Display(Name="FAX")]
	FAX
}
/// <summary>
/// Event class 'campaignMarkedSpam': campaign marked spam
/// </summary>
public class EventPropertyCampaignMarkedSpam: EventBaseProperty
{
    public string subscriberId {get;set;}
    public string campaignSubject {get;set;}
    public string campaignId {get;set;}
    public string campaignName {get;set;}
    public List<String> campaignTags {get;set;}
[JsonProperty("channel")]public string _channel {get;set;}
[JsonProperty("hidden_channel")][JsonIgnore]
                    public EventPropertyCampaignMarkedSpamChannelEnum channel 
            {
                get
                {
                        EventPropertyCampaignMarkedSpamChannelEnum enumValue =ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignMarkedSpamChannelEnum>.GetValueFromDisplayName(_channel);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignMarkedSpamChannelEnum>.GetDisplayValue(value);
                        _channel = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public dynamic extraProperties {get;set;}
}

public enum EventPropertyCampaignMarkedSpamChannelEnum {
	NoValue,
	[Display(Name="SMS")]
	SMS,
	[Display(Name="EMAIL")]
	EMAIL,
	[Display(Name="PUSH")]
	PUSH,
	[Display(Name="FAX")]
	FAX
}
/// <summary>
/// Event class 'campaignBounced': campaign bounced
/// </summary>
public class EventPropertyCampaignBounced: EventBaseProperty
{
    public string subscriberId {get;set;}
    public string campaignSubject {get;set;}
    public string campaignId {get;set;}
    public string campaignName {get;set;}
    public List<String> campaignTags {get;set;}
[JsonProperty("channel")]public string _channel {get;set;}
[JsonProperty("hidden_channel")][JsonIgnore]
                    public EventPropertyCampaignBouncedChannelEnum channel 
            {
                get
                {
                        EventPropertyCampaignBouncedChannelEnum enumValue =ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignBouncedChannelEnum>.GetValueFromDisplayName(_channel);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignBouncedChannelEnum>.GetDisplayValue(value);
                        _channel = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public dynamic extraProperties {get;set;}
}

public enum EventPropertyCampaignBouncedChannelEnum {
	NoValue,
	[Display(Name="SMS")]
	SMS,
	[Display(Name="EMAIL")]
	EMAIL,
	[Display(Name="PUSH")]
	PUSH,
	[Display(Name="FAX")]
	FAX
}
/// <summary>
/// Event class 'campaignUnsubscribed': campaign unsubscribed
/// </summary>
public class EventPropertyCampaignUnsubscribed: EventBaseProperty
{
    public string subscriberId {get;set;}
    public string campaignSubject {get;set;}
    public string campaignId {get;set;}
    public string campaignName {get;set;}
    public List<String> campaignTags {get;set;}
[JsonProperty("channel")]public string _channel {get;set;}
[JsonProperty("hidden_channel")][JsonIgnore]
                    public EventPropertyCampaignUnsubscribedChannelEnum channel 
            {
                get
                {
                        EventPropertyCampaignUnsubscribedChannelEnum enumValue =ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignUnsubscribedChannelEnum>.GetValueFromDisplayName(_channel);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignUnsubscribedChannelEnum>.GetDisplayValue(value);
                        _channel = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public string listId {get;set;}
    public string listName {get;set;}
    public dynamic extraProperties {get;set;}
}

public enum EventPropertyCampaignUnsubscribedChannelEnum {
	NoValue,
	[Display(Name="SMS")]
	SMS,
	[Display(Name="EMAIL")]
	EMAIL,
	[Display(Name="PUSH")]
	PUSH,
	[Display(Name="FAX")]
	FAX
}
/// <summary>
/// Event class 'campaignSubscribed': campaign subscribed
/// </summary>
public class EventPropertyCampaignSubscribed: EventBaseProperty
{
    public string subscriberId {get;set;}
    public string listId {get;set;}
    public string listName {get;set;}
[JsonProperty("channel")]public string _channel {get;set;}
[JsonProperty("hidden_channel")][JsonIgnore]
                    public EventPropertyCampaignSubscribedChannelEnum channel 
            {
                get
                {
                        EventPropertyCampaignSubscribedChannelEnum enumValue =ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignSubscribedChannelEnum>.GetValueFromDisplayName(_channel);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignSubscribedChannelEnum>.GetDisplayValue(value);
                        _channel = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public dynamic extraProperties {get;set;}
}

public enum EventPropertyCampaignSubscribedChannelEnum {
	NoValue,
	[Display(Name="SMS")]
	SMS,
	[Display(Name="EMAIL")]
	EMAIL,
	[Display(Name="PUSH")]
	PUSH,
	[Display(Name="FAX")]
	FAX
}
/// <summary>
/// Event class 'eventInvited': event invited
/// </summary>
public class EventPropertyEventInvited: EventBaseProperty
{
    public string eventId {get;set;}
    public string eventName {get;set;}
    //format: date-time
    public string eventDate {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'eventParticipated': event participated
/// </summary>
public class EventPropertyEventParticipated: EventBaseProperty
{
    public string eventId {get;set;}
    public string eventName {get;set;}
    //format: date-time
    public string eventDate {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'searched': searched
/// </summary>
public class EventPropertySearched: EventBaseProperty
{
    public string keyword {get;set;}
    public decimal resultCount {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'changedSetting': changed setting
/// </summary>
public class EventPropertyChangedSetting: EventBaseProperty
{
[JsonProperty("setting")]public string _setting {get;set;}
[JsonProperty("hidden_setting")][JsonIgnore]
                    public EventPropertyChangedSettingSettingEnum setting 
            {
                get
                {
                        EventPropertyChangedSettingSettingEnum enumValue =ContactHubSdkLibrary.EnumHelper<EventPropertyChangedSettingSettingEnum>.GetValueFromDisplayName(_setting);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<EventPropertyChangedSettingSettingEnum>.GetDisplayValue(value);
                        _setting = (displayValue=="NoValue"? null : displayValue);
                }
            }
                public string oldValue {get;set;}
    public string newValue {get;set;}
    public dynamic extraProperties {get;set;}
}

public enum EventPropertyChangedSettingSettingEnum {
	NoValue,
	[Display(Name="LANGUAGE")]
	LANGUAGE,
	[Display(Name="TIMEZONE")]
	TIMEZONE,
	[Display(Name="CURRENCY")]
	CURRENCY,
	[Display(Name="EMAIL")]
	EMAIL,
	[Display(Name="PASSWORD")]
	PASSWORD,
	[Display(Name="USERNAME")]
	USERNAME,
	[Display(Name="OTHER")]
	OTHER
}
/// <summary>
/// Event class 'genericActiveEvent': generic active event
/// </summary>
public class EventPropertyGenericActiveEvent: EventBaseProperty
{
    public string name {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'genericPassiveEvent': generic passive event
/// </summary>
public class EventPropertyGenericPassiveEvent: EventBaseProperty
{
    public string name {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'serviceUnsubscribed': service unsubscribed
/// </summary>
public class EventPropertyServiceUnsubscribed: EventBaseProperty
{
    public string subscriberId {get;set;}
    public string serviceId {get;set;}
    public string serviceName {get;set;}
    public string serviceType {get;set;}
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
            set
            {
                try
                {
                    _endDate = value.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                }
                catch { _endDate = null; }
            }
        }
                public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'serviceSubscribed': service subscribed
/// </summary>
public class EventPropertyServiceSubscribed: EventBaseProperty
{
    public string subscriberId {get;set;}
    public string serviceId {get;set;}
    public string serviceName {get;set;}
    public string serviceType {get;set;}
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
            set
            {
                try
                {
                    _endDate = value.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                }
                catch { _endDate = null; }
            }
        }
                public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'abandonedCart': abandoned cart
/// </summary>
public class EventPropertyAbandonedCart: EventBaseProperty
{
    public string orderId {get;set;}
    //format: uri
    public string abandonedCartUrl {get;set;}
    public string storeCode {get;set;}
    public Amount amount {get;set;}
    public List<Products> products {get;set;}
    public dynamic extraProperties {get;set;}
}


public enum EventTypeEnum {
	NoValue,
	[Display(Name="openedTicket")]
	openedTicket,
	[Display(Name="repliedTicket")]
	repliedTicket,
	[Display(Name="closedTicket")]
	closedTicket,
	[Display(Name="loggedIn")]
	loggedIn,
	[Display(Name="loggedOut")]
	loggedOut,
	[Display(Name="reviewedProduct")]
	reviewedProduct,
	[Display(Name="viewedProductCategory")]
	viewedProductCategory,
	[Display(Name="viewedProduct")]
	viewedProduct,
	[Display(Name="addedProduct")]
	addedProduct,
	[Display(Name="removedProduct")]
	removedProduct,
	[Display(Name="addedWishlist")]
	addedWishlist,
	[Display(Name="removedWishlist")]
	removedWishlist,
	[Display(Name="addedCompare")]
	addedCompare,
	[Display(Name="removedCompare")]
	removedCompare,
	[Display(Name="completedOrder")]
	completedOrder,
	[Display(Name="viewedPage")]
	viewedPage,
	[Display(Name="clickedLink")]
	clickedLink,
	[Display(Name="formCompiled")]
	formCompiled,
	[Display(Name="campaignSent")]
	campaignSent,
	[Display(Name="campaignOpened")]
	campaignOpened,
	[Display(Name="campaignLinkClicked")]
	campaignLinkClicked,
	[Display(Name="campaignMarkedSpam")]
	campaignMarkedSpam,
	[Display(Name="campaignBounced")]
	campaignBounced,
	[Display(Name="campaignUnsubscribed")]
	campaignUnsubscribed,
	[Display(Name="campaignSubscribed")]
	campaignSubscribed,
	[Display(Name="eventInvited")]
	eventInvited,
	[Display(Name="eventParticipated")]
	eventParticipated,
	[Display(Name="searched")]
	searched,
	[Display(Name="changedSetting")]
	changedSetting,
	[Display(Name="genericActiveEvent")]
	genericActiveEvent,
	[Display(Name="genericPassiveEvent")]
	genericPassiveEvent,
	[Display(Name="serviceUnsubscribed")]
	serviceUnsubscribed,
	[Display(Name="serviceSubscribed")]
	serviceSubscribed,
	[Display(Name="abandonedCart")]
	abandonedCart
}
                public static class EventPropertiesUtil
                {
                    /// <summary>
                    /// Return events properties with right cast, event type based
                    /// </summary>

                  public static object GetEventProperties(JObject jo, JsonSerializer serializer)
                    {
                        var typeName = jo["type"].ToString().ToLowerInvariant();
                        switch (typeName)
                        {
                     case "openedticket": return jo["properties"].ToObject<EventPropertyOpenedTicket>(serializer);break;

 case "repliedticket": return jo["properties"].ToObject<EventPropertyRepliedTicket>(serializer);break;

 case "closedticket": return jo["properties"].ToObject<EventPropertyClosedTicket>(serializer);break;

 case "loggedin": return jo["properties"].ToObject<EventPropertyLoggedIn>(serializer);break;

 case "loggedout": return jo["properties"].ToObject<EventPropertyLoggedOut>(serializer);break;

 case "reviewedproduct": return jo["properties"].ToObject<EventPropertyReviewedProduct>(serializer);break;

 case "viewedproductcategory": return jo["properties"].ToObject<EventPropertyViewedProductCategory>(serializer);break;

 case "viewedproduct": return jo["properties"].ToObject<EventPropertyViewedProduct>(serializer);break;

 case "addedproduct": return jo["properties"].ToObject<EventPropertyAddedProduct>(serializer);break;

 case "removedproduct": return jo["properties"].ToObject<EventPropertyRemovedProduct>(serializer);break;

 case "addedwishlist": return jo["properties"].ToObject<EventPropertyAddedWishlist>(serializer);break;

 case "removedwishlist": return jo["properties"].ToObject<EventPropertyRemovedWishlist>(serializer);break;

 case "addedcompare": return jo["properties"].ToObject<EventPropertyAddedCompare>(serializer);break;

 case "removedcompare": return jo["properties"].ToObject<EventPropertyRemovedCompare>(serializer);break;

 case "completedorder": return jo["properties"].ToObject<EventPropertyCompletedOrder>(serializer);break;

 case "viewedpage": return jo["properties"].ToObject<EventPropertyViewedPage>(serializer);break;

 case "clickedlink": return jo["properties"].ToObject<EventPropertyClickedLink>(serializer);break;

 case "formcompiled": return jo["properties"].ToObject<EventPropertyFormCompiled>(serializer);break;

 case "campaignsent": return jo["properties"].ToObject<EventPropertyCampaignSent>(serializer);break;

 case "campaignopened": return jo["properties"].ToObject<EventPropertyCampaignOpened>(serializer);break;

 case "campaignlinkclicked": return jo["properties"].ToObject<EventPropertyCampaignLinkClicked>(serializer);break;

 case "campaignmarkedspam": return jo["properties"].ToObject<EventPropertyCampaignMarkedSpam>(serializer);break;

 case "campaignbounced": return jo["properties"].ToObject<EventPropertyCampaignBounced>(serializer);break;

 case "campaignunsubscribed": return jo["properties"].ToObject<EventPropertyCampaignUnsubscribed>(serializer);break;

 case "campaignsubscribed": return jo["properties"].ToObject<EventPropertyCampaignSubscribed>(serializer);break;

 case "eventinvited": return jo["properties"].ToObject<EventPropertyEventInvited>(serializer);break;

 case "eventparticipated": return jo["properties"].ToObject<EventPropertyEventParticipated>(serializer);break;

 case "searched": return jo["properties"].ToObject<EventPropertySearched>(serializer);break;

 case "changedsetting": return jo["properties"].ToObject<EventPropertyChangedSetting>(serializer);break;

 case "genericactiveevent": return jo["properties"].ToObject<EventPropertyGenericActiveEvent>(serializer);break;

 case "genericpassiveevent": return jo["properties"].ToObject<EventPropertyGenericPassiveEvent>(serializer);break;

 case "serviceunsubscribed": return jo["properties"].ToObject<EventPropertyServiceUnsubscribed>(serializer);break;

 case "servicesubscribed": return jo["properties"].ToObject<EventPropertyServiceSubscribed>(serializer);break;

 case "abandonedcart": return jo["properties"].ToObject<EventPropertyAbandonedCart>(serializer);break;

}
 return null;
}

}
}
