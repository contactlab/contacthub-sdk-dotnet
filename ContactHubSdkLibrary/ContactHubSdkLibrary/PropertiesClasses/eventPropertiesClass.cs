/* selfgenerated from version 0.0.0.1 26/01/2017 16:11:03 */

using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
namespace ContactHubSdkLibrary.Events {
public class EventBaseProperty {}
/// <summary>
/// Event class 'abandonedCart': Customer added product to cart but not completed the order/transaction
/// </summary>
public class EventPropertyAbandonedCart: EventBaseProperty
{
	[Display(Name="Order/Transaction ID")]
    public string orderId {get;set;}
	[Display(Name="The URL to recover the abandoned cart")]
    //format: uri
    public string abandonedCartUrl {get;set;}
	[Display(Name="Store or affiliation from which this transaction occurred")]
    public string storeCode {get;set;}
    public Amount amount {get;set;}
	[Display(Name="Details of transaction")]
    public List<Products> products {get;set;}
    public dynamic extraProperties {get;set;}
}


public class Amount
{
	[Display(Name="Total is calculated adding revenue, shipping and tax amount and eliminating discounts amount")]
    public decimal? total {get;set;}
	[Display(Name="Revenue is calculated by multiplying the price at which products or services are sold by the number of units or amount sold")]
    public decimal? revenue {get;set;}
	[Display(Name="Shipping cost")]
    public decimal? shipping {get;set;}
	[Display(Name="Total tax amount")]
    public decimal? tax {get;set;}
	[Display(Name="Total discount amount")]
    public decimal? discount {get;set;}
    public Local local {get;set;}
}


public class Local
{
	[Display(Name="Currency of the transaction")]
    public string currency {get;set;}
	[Display(Name="Rate of exchange currency between default currency to local currency of the event")]
    public decimal? exchangeRate {get;set;}
}


public class Products
{
	[Display(Name="The discount amount of order line")]
    public decimal? discount {get;set;}
	[Display(Name="Quantity of a product")]
    public decimal? quantity {get;set;}
	[Display(Name="The total amount of order line")]
    public decimal? subtotal {get;set;}
	[Display(Name="Price of the product")]
    public decimal? price {get;set;}
	[Display(Name="Name of the product")]
    public string name {get;set;}
	[Display(Name="Sku of the product")]
    public string sku {get;set;}
	[Display(Name="The type of order line")]
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
            	[Display(Name="The list of classifications of the product")]
    public List<Classifications> classifications {get;set;}
	[Display(Name="Categories list of the product")]
    public List<String> category {get;set;}
	[Display(Name="Short description of the product")]
    public string shortDescription {get;set;}
	[Display(Name="The online catalogue of the product")]
    //format: uri
    public string linkUrl {get;set;}
	[Display(Name="The image of online catalogue of the product")]
    //format: uri
    public string imageUrl {get;set;}
	[Display(Name="Coupon code associated with a product")]
    public string coupon {get;set;}
	[Display(Name="The tax associated with the order line")]
    public decimal? tax {get;set;}
	[Display(Name="Database id of the product")]
    public string id {get;set;}
}


public class Classifications
{
	[Display(Name="The key of the classification")]
    public string key {get;set;}
	[Display(Name="The value of the classification")]
    public string value {get;set;}
}

public enum ProductsTypeEnum {
	NoValue,
	[Display(Name="sale")]
	sale,
	[Display(Name="return")]
	@return
}
/// <summary>
/// Event class 'addedCompare': Customer added a product to comparator
/// </summary>
public class EventPropertyAddedCompare: EventBaseProperty
{
	[Display(Name="Categories list of the product")]
    public List<String> category {get;set;}
	[Display(Name="Short description of the product")]
    public string shortDescription {get;set;}
    public dynamic extraProperties {get;set;}
	[Display(Name="The list of classifications of the product")]
    public List<Classifications> classifications {get;set;}
	[Display(Name="Database id of the product")]
    public string id {get;set;}
	[Display(Name="The sku of the product")]
    public string sku {get;set;}
	[Display(Name="Name of the product")]
    public string name {get;set;}
	[Display(Name="Price of the product")]
    public decimal? price {get;set;}
	[Display(Name="The image of online catalogue of the product")]
    //format: uri
    public string imageUrl {get;set;}
	[Display(Name="The online catalogue of the product")]
    //format: uri
    public string linkUrl {get;set;}
}


/// <summary>
/// Event class 'addedProduct': Customer added a product to their shopping cart
/// </summary>
public class EventPropertyAddedProduct: EventBaseProperty
{
	[Display(Name="Short description of the product")]
    public string shortDescription {get;set;}
	[Display(Name="The online catalogue of the product")]
    //format: uri
    public string linkUrl {get;set;}
	[Display(Name="The image of online catalogue of the product")]
    //format: uri
    public string imageUrl {get;set;}
    public dynamic extraProperties {get;set;}
	[Display(Name="The list of classifications of the product")]
    public List<Classifications> classifications {get;set;}
	[Display(Name="Categories list of the product")]
    public List<String> category {get;set;}
	[Display(Name="Database id of the product")]
    public string id {get;set;}
	[Display(Name="The sku of the product")]
    public string sku {get;set;}
	[Display(Name="Name of the product")]
    public string name {get;set;}
	[Display(Name="Price of the product")]
    public decimal? price {get;set;}
	[Display(Name="Quantity of a product")]
    public decimal? quantity {get;set;}
}


/// <summary>
/// Event class 'addedWishlist': Customer added a product to the wish list
/// </summary>
public class EventPropertyAddedWishlist: EventBaseProperty
{
	[Display(Name="Categories list of the product")]
    public List<String> category {get;set;}
	[Display(Name="Short description of the product")]
    public string shortDescription {get;set;}
    public dynamic extraProperties {get;set;}
	[Display(Name="The list of classifications of the product")]
    public List<Classifications> classifications {get;set;}
	[Display(Name="Database id of the product")]
    public string id {get;set;}
	[Display(Name="The sku of the product")]
    public string sku {get;set;}
	[Display(Name="Name of the product")]
    public string name {get;set;}
	[Display(Name="Price of the product")]
    public decimal? price {get;set;}
	[Display(Name="The image of online catalogue of the product")]
    //format: uri
    public string imageUrl {get;set;}
	[Display(Name="The online catalogue of the product")]
    //format: uri
    public string linkUrl {get;set;}
}


/// <summary>
/// Event class 'campaignBlacklisted': Customer went to blacklist
/// </summary>
public class EventPropertyCampaignBlacklisted: EventBaseProperty
{
	[Display(Name="Name used to identify a list")]
    public string listName {get;set;}
    public dynamic extraProperties {get;set;}
	[Display(Name="An ID used to identify the subscriber")]
    public string subscriberId {get;set;}
	[Display(Name="The campaign’s subject line")]
    public string campaignSubject {get;set;}
	[Display(Name="An id used to identify a campaign")]
    public string campaignId {get;set;}
	[Display(Name="A name used to identify a campaign")]
    public string campaignName {get;set;}
	[Display(Name="The list of campaign's tags")]
    public List<String> campaignTags {get;set;}
	[Display(Name="The campaign medium")]
[JsonProperty("channel")]public string _channel {get;set;}
[JsonProperty("hidden_channel")][JsonIgnore]
                    public EventPropertyCampaignBlacklistedChannelEnum channel 
            {
                get
                {
                        EventPropertyCampaignBlacklistedChannelEnum enumValue =ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignBlacklistedChannelEnum>.GetValueFromDisplayName(_channel);
                        return enumValue;
                }
                set
                {
                        var displayValue = ContactHubSdkLibrary.EnumHelper<EventPropertyCampaignBlacklistedChannelEnum>.GetDisplayValue(value);
                        _channel = (displayValue=="NoValue"? null : displayValue);
                }
            }
            	[Display(Name="Id used to identify a list")]
    public string listId {get;set;}
}

public enum EventPropertyCampaignBlacklistedChannelEnum {
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
/// Event class 'campaignBounced': Notice from an email server that a campaign is undeliverable
/// </summary>
public class EventPropertyCampaignBounced: EventBaseProperty
{
	[Display(Name="An ID used to identify the subscriber")]
    public string subscriberId {get;set;}
	[Display(Name="The campaign’s subject line")]
    public string campaignSubject {get;set;}
	[Display(Name="An id used to identify a campaign")]
    public string campaignId {get;set;}
	[Display(Name="A name used to identify a campaign")]
    public string campaignName {get;set;}
	[Display(Name="The list of campaign's tags")]
    public List<String> campaignTags {get;set;}
	[Display(Name="The campaign medium")]
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
/// Event class 'campaignLinkClicked': Customer opened a link in a campaign received
/// </summary>
public class EventPropertyCampaignLinkClicked: EventBaseProperty
{
	[Display(Name="The online catalogue of the product")]
    //format: uri
    public string linkUrl {get;set;}
	[Display(Name="The id to identify the link")]
    public string linkId {get;set;}
    public dynamic extraProperties {get;set;}
	[Display(Name="The list of link tags")]
    public List<String> linkTags {get;set;}
	[Display(Name="An ID used to identify the subscriber")]
    public string subscriberId {get;set;}
	[Display(Name="The campaign’s subject line")]
    public string campaignSubject {get;set;}
	[Display(Name="An id used to identify a campaign")]
    public string campaignId {get;set;}
	[Display(Name="A name used to identify a campaign")]
    public string campaignName {get;set;}
	[Display(Name="The list of campaign's tags")]
    public List<String> campaignTags {get;set;}
	[Display(Name="The campaign medium")]
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
/// Event class 'campaignMarkedSpam': Customer marked as spam a campaign received
/// </summary>
public class EventPropertyCampaignMarkedSpam: EventBaseProperty
{
	[Display(Name="An ID used to identify the subscriber")]
    public string subscriberId {get;set;}
	[Display(Name="The campaign’s subject line")]
    public string campaignSubject {get;set;}
	[Display(Name="An id used to identify a campaign")]
    public string campaignId {get;set;}
	[Display(Name="A name used to identify a campaign")]
    public string campaignName {get;set;}
	[Display(Name="The list of campaign's tags")]
    public List<String> campaignTags {get;set;}
	[Display(Name="The campaign medium")]
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
/// Event class 'campaignOpened': Customer opened a campaign received
/// </summary>
public class EventPropertyCampaignOpened: EventBaseProperty
{
	[Display(Name="An ID used to identify the subscriber")]
    public string subscriberId {get;set;}
	[Display(Name="The campaign’s subject line")]
    public string campaignSubject {get;set;}
	[Display(Name="An id used to identify a campaign")]
    public string campaignId {get;set;}
	[Display(Name="A name used to identify a campaign")]
    public string campaignName {get;set;}
	[Display(Name="The list of campaign's tags")]
    public List<String> campaignTags {get;set;}
	[Display(Name="The campaign medium")]
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
/// Event class 'campaignSent': Customer received a campaign
/// </summary>
public class EventPropertyCampaignSent: EventBaseProperty
{
	[Display(Name="An ID used to identify the subscriber")]
    public string subscriberId {get;set;}
	[Display(Name="The campaign’s subject line")]
    public string campaignSubject {get;set;}
	[Display(Name="An id used to identify a campaign")]
    public string campaignId {get;set;}
	[Display(Name="A name used to identify a campaign")]
    public string campaignName {get;set;}
	[Display(Name="The list of campaign's tags")]
    public List<String> campaignTags {get;set;}
	[Display(Name="The campaign medium")]
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
/// Event class 'campaignSubscribed': Customer subscribed a list
/// </summary>
public class EventPropertyCampaignSubscribed: EventBaseProperty
{
	[Display(Name="An ID used to identify the subscriber")]
    public string subscriberId {get;set;}
	[Display(Name="Id used to identify a list")]
    public string listId {get;set;}
	[Display(Name="Name used to identify a list")]
    public string listName {get;set;}
	[Display(Name="The campaign medium")]
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
/// Event class 'campaignUnsubscribed': Customer unsubscribed from a list
/// </summary>
public class EventPropertyCampaignUnsubscribed: EventBaseProperty
{
	[Display(Name="Name used to identify a list")]
    public string listName {get;set;}
    public dynamic extraProperties {get;set;}
	[Display(Name="An ID used to identify the subscriber")]
    public string subscriberId {get;set;}
	[Display(Name="The campaign’s subject line")]
    public string campaignSubject {get;set;}
	[Display(Name="An id used to identify a campaign")]
    public string campaignId {get;set;}
	[Display(Name="A name used to identify a campaign")]
    public string campaignName {get;set;}
	[Display(Name="The list of campaign's tags")]
    public List<String> campaignTags {get;set;}
	[Display(Name="The campaign medium")]
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
            	[Display(Name="Id used to identify a list")]
    public string listId {get;set;}
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
/// Event class 'changedSetting': Customer changed a setting on website/app
/// </summary>
public class EventPropertyChangedSetting: EventBaseProperty
{
	[Display(Name="The setting changed")]
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
            	[Display(Name="The old value of property")]
    public string oldValue {get;set;}
	[Display(Name="The new value of property")]
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
/// Event class 'clickedLink': Customer clicked a link
/// </summary>
public class EventPropertyClickedLink: EventBaseProperty
{
	[Display(Name="The path of URL of the link")]
    public string path {get;set;}
	[Display(Name="The referer of the page")]
    public string referer {get;set;}
	[Display(Name="The parameters searched")]
    public string search {get;set;}
	[Display(Name="The title of link")]
    public string title {get;set;}
	[Display(Name="The URL of link")]
    //format: uri
    public string url {get;set;}
	[Display(Name="The list of link tags")]
    public List<String> linkTags {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'closedTicket': The customer closed a ticket to a customer care
/// </summary>
public class EventPropertyClosedTicket: EventBaseProperty
{
	[Display(Name="The ID used to identify the ticket")]
    public string ticketId {get;set;}
	[Display(Name="Categories list of ticket")]
    public List<String> category {get;set;}
	[Display(Name="The subject of ticket")]
    public string subject {get;set;}
	[Display(Name="The text of ticket")]
    public string text {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'completedOrder': The customer completed successfully an order/transaction
/// </summary>
public class EventPropertyCompletedOrder: EventBaseProperty
{
	[Display(Name="Order/Transaction ID")]
    public string orderId {get;set;}
	[Display(Name="Type of order")]
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
            	[Display(Name="Store or affiliation from which this transaction occurred")]
    public string storeCode {get;set;}
	[Display(Name="The payment method chosen")]
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
	[Display(Name="Products in the order")]
    public List<Products> products {get;set;}
    public dynamic extraProperties {get;set;}
}

public enum EventPropertyCompletedOrderPaymentMethodEnum {
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
/// Event class 'eventInvited': Customer was invited at an event
/// </summary>
public class EventPropertyEventInvited: EventBaseProperty
{
	[Display(Name="The idenfier of the event")]
    public string eventId {get;set;}
	[Display(Name="The name of the event")]
    public string eventName {get;set;}
	[Display(Name="The date-time of the event")]
    //format: date-time
    public string eventDate {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'eventParticipated': Customer has participated to an event
/// </summary>
public class EventPropertyEventParticipated: EventBaseProperty
{
	[Display(Name="The idenfier of the event")]
    public string eventId {get;set;}
	[Display(Name="The name of the event")]
    public string eventName {get;set;}
	[Display(Name="The date-time of the event")]
    //format: date-time
    public string eventDate {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'formCompiled': Customer compiled a form on the website/app/convention
/// </summary>
public class EventPropertyFormCompiled: EventBaseProperty
{
	[Display(Name="The name to identify the form")]
    public string formName {get;set;}
	[Display(Name="The id to identify the form")]
    public string formId {get;set;}
    public dynamic data {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'genericActiveEvent': Customer made an active event uncovered
/// </summary>
public class EventPropertyGenericActiveEvent: EventBaseProperty
{
	[Display(Name="Name of the event")]
    public string name {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'genericPassiveEvent': Customer made a passive event uncovered
/// </summary>
public class EventPropertyGenericPassiveEvent: EventBaseProperty
{
	[Display(Name="Name of the event")]
    public string name {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'loggedIn': The customer logged in an app/software/site web
/// </summary>
public class EventPropertyLoggedIn: EventBaseProperty
{
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'loggedOut': The customer logged out from an app/software/site web
/// </summary>
public class EventPropertyLoggedOut: EventBaseProperty
{
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'openedTicket': The customer opened a ticket to a customer care
/// </summary>
public class EventPropertyOpenedTicket: EventBaseProperty
{
	[Display(Name="The ID used to identify the ticket")]
    public string ticketId {get;set;}
	[Display(Name="Categories list of ticket")]
    public List<String> category {get;set;}
	[Display(Name="The subject of ticket")]
    public string subject {get;set;}
	[Display(Name="The text of ticket")]
    public string text {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'orderShipped': The company shipped the products present in the order
/// </summary>
public class EventPropertyOrderShipped: EventBaseProperty
{
	[Display(Name="Order/Transaction ID")]
    public string orderId {get;set;}
	[Display(Name="The type of shipment")]
    public string type {get;set;}
	[Display(Name="The name of the shipping service")]
    public string carrier {get;set;}
	[Display(Name="The tracking code/number of shipment")]
    public string trackingCode {get;set;}
	[Display(Name="The url for track the of shipment")]
    //format: uri
    public string trackingUrl {get;set;}
	[Display(Name="The weight of packages")]
    public decimal? weight {get;set;}
	[Display(Name="The number of packages")]
    public int packages {get;set;}
}


/// <summary>
/// Event class 'removedCompare': Customer removed a product to comparator
/// </summary>
public class EventPropertyRemovedCompare: EventBaseProperty
{
	[Display(Name="Categories list of the product")]
    public List<String> category {get;set;}
	[Display(Name="Short description of the product")]
    public string shortDescription {get;set;}
    public dynamic extraProperties {get;set;}
	[Display(Name="The list of classifications of the product")]
    public List<Classifications> classifications {get;set;}
	[Display(Name="Database id of the product")]
    public string id {get;set;}
	[Display(Name="The sku of the product")]
    public string sku {get;set;}
	[Display(Name="Name of the product")]
    public string name {get;set;}
	[Display(Name="Price of the product")]
    public decimal? price {get;set;}
	[Display(Name="The image of online catalogue of the product")]
    //format: uri
    public string imageUrl {get;set;}
	[Display(Name="The online catalogue of the product")]
    //format: uri
    public string linkUrl {get;set;}
}


/// <summary>
/// Event class 'removedProduct': Customer removed a product from their shopping cart
/// </summary>
public class EventPropertyRemovedProduct: EventBaseProperty
{
	[Display(Name="Short description of the product")]
    public string shortDescription {get;set;}
	[Display(Name="The online catalogue of the product")]
    //format: uri
    public string linkUrl {get;set;}
	[Display(Name="The image of online catalogue of the product")]
    //format: uri
    public string imageUrl {get;set;}
    public dynamic extraProperties {get;set;}
	[Display(Name="The list of classifications of the product")]
    public List<Classifications> classifications {get;set;}
	[Display(Name="Categories list of the product")]
    public List<String> category {get;set;}
	[Display(Name="Database id of the product")]
    public string id {get;set;}
	[Display(Name="The sku of the product")]
    public string sku {get;set;}
	[Display(Name="Name of the product")]
    public string name {get;set;}
	[Display(Name="Price of the product")]
    public decimal? price {get;set;}
	[Display(Name="Quantity of a product")]
    public decimal? quantity {get;set;}
}


/// <summary>
/// Event class 'removedWishlist': Customer removed a product from the wish list
/// </summary>
public class EventPropertyRemovedWishlist: EventBaseProperty
{
	[Display(Name="Categories list of the product")]
    public List<String> category {get;set;}
	[Display(Name="Short description of the product")]
    public string shortDescription {get;set;}
    public dynamic extraProperties {get;set;}
	[Display(Name="The list of classifications of the product")]
    public List<Classifications> classifications {get;set;}
	[Display(Name="Database id of the product")]
    public string id {get;set;}
	[Display(Name="The sku of the product")]
    public string sku {get;set;}
	[Display(Name="Name of the product")]
    public string name {get;set;}
	[Display(Name="Price of the product")]
    public decimal? price {get;set;}
	[Display(Name="The image of online catalogue of the product")]
    //format: uri
    public string imageUrl {get;set;}
	[Display(Name="The online catalogue of the product")]
    //format: uri
    public string linkUrl {get;set;}
}


/// <summary>
/// Event class 'repliedTicket': The Agent of customer care replied to ticket from a customer
/// </summary>
public class EventPropertyRepliedTicket: EventBaseProperty
{
	[Display(Name="The ID used to identify the ticket")]
    public string ticketId {get;set;}
	[Display(Name="Categories list of ticket")]
    public List<String> category {get;set;}
	[Display(Name="The subject of ticket")]
    public string subject {get;set;}
	[Display(Name="The text of ticket")]
    public string text {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'reviewedProduct': Customer reviewed a product
/// </summary>
public class EventPropertyReviewedProduct: EventBaseProperty
{
	[Display(Name="Categories list of the product")]
    public List<String> category {get;set;}
	[Display(Name="Short description of the product")]
    public string shortDescription {get;set;}
	[Display(Name="The online catalogue of the product")]
    //format: uri
    public string linkUrl {get;set;}
    public dynamic extraProperties {get;set;}
	[Display(Name="Review rating")]
    public string rating {get;set;}
	[Display(Name="The list of classifications of the product")]
    public List<Classifications> classifications {get;set;}
	[Display(Name="Database id of the product")]
    public string id {get;set;}
	[Display(Name="The sku of the product")]
    public string sku {get;set;}
	[Display(Name="Name of the product")]
    public string name {get;set;}
	[Display(Name="Price of the product")]
    public decimal? price {get;set;}
	[Display(Name="The image of online catalogue of the product")]
    //format: uri
    public string imageUrl {get;set;}
}


/// <summary>
/// Event class 'searched': Customer searched on website/app
/// </summary>
public class EventPropertySearched: EventBaseProperty
{
	[Display(Name="Key of search")]
    public string keyword {get;set;}
	[Display(Name="Number of results")]
    public decimal? resultCount {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'serviceSubscribed': Customer subscribed to a service subscription
/// </summary>
public class EventPropertyServiceSubscribed: EventBaseProperty
{
	[Display(Name="An ID used to identify the subscriber")]
    public string subscriberId {get;set;}
	[Display(Name="The idenfier of the service")]
    public string serviceId {get;set;}
	[Display(Name="The name of the service")]
    public string serviceName {get;set;}
	[Display(Name="The type of service")]
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
                public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'serviceUnsubscribed': Customer unsubscribed from a service subscription
/// </summary>
public class EventPropertyServiceUnsubscribed: EventBaseProperty
{
	[Display(Name="An ID used to identify the subscriber")]
    public string subscriberId {get;set;}
	[Display(Name="The idenfier of service")]
    public string serviceId {get;set;}
	[Display(Name="The name of service")]
    public string serviceName {get;set;}
	[Display(Name="The type of service")]
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
                public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'viewedPage': Customer viewed a page on website/app
/// </summary>
public class EventPropertyViewedPage: EventBaseProperty
{
	[Display(Name="The path of URL of the page")]
    public string path {get;set;}
	[Display(Name="The referer of the page")]
    public string referer {get;set;}
	[Display(Name="The parameters searched")]
    public string search {get;set;}
	[Display(Name="Title of the page")]
    public string title {get;set;}
	[Display(Name="The URL of the page")]
    //format: uri
    public string url {get;set;}
	[Display(Name="The list of page categories")]
    public List<String> pageCategories {get;set;}
	[Display(Name="The list of page tags")]
    public List<String> pageTags {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'viewedProduct': Customer viewed a product details
/// </summary>
public class EventPropertyViewedProduct: EventBaseProperty
{
	[Display(Name="Categories list of the product")]
    public List<String> category {get;set;}
	[Display(Name="Short description of the product")]
    public string shortDescription {get;set;}
    public dynamic extraProperties {get;set;}
	[Display(Name="The list of classifications of the product")]
    public List<Classifications> classifications {get;set;}
	[Display(Name="Database id of the product")]
    public string id {get;set;}
	[Display(Name="The sku of the product")]
    public string sku {get;set;}
	[Display(Name="Name of the product")]
    public string name {get;set;}
	[Display(Name="Price of the product")]
    public decimal? price {get;set;}
	[Display(Name="The image of online catalogue of the product")]
    //format: uri
    public string imageUrl {get;set;}
	[Display(Name="The online catalogue of the product")]
    //format: uri
    public string linkUrl {get;set;}
}


/// <summary>
/// Event class 'viewedProductCategory': Customer viewed a product category list
/// </summary>
public class EventPropertyViewedProductCategory: EventBaseProperty
{
	[Display(Name="Product category being viewed")]
    public string category {get;set;}
    public dynamic extraProperties {get;set;}
}


public enum EventTypeEnum {
	NoValue,
	[Display(Name="abandonedCart")]
	abandonedCart,
	[Display(Name="addedCompare")]
	addedCompare,
	[Display(Name="addedProduct")]
	addedProduct,
	[Display(Name="addedWishlist")]
	addedWishlist,
	[Display(Name="campaignBlacklisted")]
	campaignBlacklisted,
	[Display(Name="campaignBounced")]
	campaignBounced,
	[Display(Name="campaignLinkClicked")]
	campaignLinkClicked,
	[Display(Name="campaignMarkedSpam")]
	campaignMarkedSpam,
	[Display(Name="campaignOpened")]
	campaignOpened,
	[Display(Name="campaignSent")]
	campaignSent,
	[Display(Name="campaignSubscribed")]
	campaignSubscribed,
	[Display(Name="campaignUnsubscribed")]
	campaignUnsubscribed,
	[Display(Name="changedSetting")]
	changedSetting,
	[Display(Name="clickedLink")]
	clickedLink,
	[Display(Name="closedTicket")]
	closedTicket,
	[Display(Name="completedOrder")]
	completedOrder,
	[Display(Name="eventInvited")]
	eventInvited,
	[Display(Name="eventParticipated")]
	eventParticipated,
	[Display(Name="formCompiled")]
	formCompiled,
	[Display(Name="genericActiveEvent")]
	genericActiveEvent,
	[Display(Name="genericPassiveEvent")]
	genericPassiveEvent,
	[Display(Name="loggedIn")]
	loggedIn,
	[Display(Name="loggedOut")]
	loggedOut,
	[Display(Name="openedTicket")]
	openedTicket,
	[Display(Name="orderShipped")]
	orderShipped,
	[Display(Name="removedCompare")]
	removedCompare,
	[Display(Name="removedProduct")]
	removedProduct,
	[Display(Name="removedWishlist")]
	removedWishlist,
	[Display(Name="repliedTicket")]
	repliedTicket,
	[Display(Name="reviewedProduct")]
	reviewedProduct,
	[Display(Name="searched")]
	searched,
	[Display(Name="serviceSubscribed")]
	serviceSubscribed,
	[Display(Name="serviceUnsubscribed")]
	serviceUnsubscribed,
	[Display(Name="viewedPage")]
	viewedPage,
	[Display(Name="viewedProduct")]
	viewedProduct,
	[Display(Name="viewedProductCategory")]
	viewedProductCategory
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
                     case "abandonedcart": return jo["properties"].ToObject<EventPropertyAbandonedCart>(serializer);break;

 case "addedcompare": return jo["properties"].ToObject<EventPropertyAddedCompare>(serializer);break;

 case "addedproduct": return jo["properties"].ToObject<EventPropertyAddedProduct>(serializer);break;

 case "addedwishlist": return jo["properties"].ToObject<EventPropertyAddedWishlist>(serializer);break;

 case "campaignblacklisted": return jo["properties"].ToObject<EventPropertyCampaignBlacklisted>(serializer);break;

 case "campaignbounced": return jo["properties"].ToObject<EventPropertyCampaignBounced>(serializer);break;

 case "campaignlinkclicked": return jo["properties"].ToObject<EventPropertyCampaignLinkClicked>(serializer);break;

 case "campaignmarkedspam": return jo["properties"].ToObject<EventPropertyCampaignMarkedSpam>(serializer);break;

 case "campaignopened": return jo["properties"].ToObject<EventPropertyCampaignOpened>(serializer);break;

 case "campaignsent": return jo["properties"].ToObject<EventPropertyCampaignSent>(serializer);break;

 case "campaignsubscribed": return jo["properties"].ToObject<EventPropertyCampaignSubscribed>(serializer);break;

 case "campaignunsubscribed": return jo["properties"].ToObject<EventPropertyCampaignUnsubscribed>(serializer);break;

 case "changedsetting": return jo["properties"].ToObject<EventPropertyChangedSetting>(serializer);break;

 case "clickedlink": return jo["properties"].ToObject<EventPropertyClickedLink>(serializer);break;

 case "closedticket": return jo["properties"].ToObject<EventPropertyClosedTicket>(serializer);break;

 case "completedorder": return jo["properties"].ToObject<EventPropertyCompletedOrder>(serializer);break;

 case "eventinvited": return jo["properties"].ToObject<EventPropertyEventInvited>(serializer);break;

 case "eventparticipated": return jo["properties"].ToObject<EventPropertyEventParticipated>(serializer);break;

 case "formcompiled": return jo["properties"].ToObject<EventPropertyFormCompiled>(serializer);break;

 case "genericactiveevent": return jo["properties"].ToObject<EventPropertyGenericActiveEvent>(serializer);break;

 case "genericpassiveevent": return jo["properties"].ToObject<EventPropertyGenericPassiveEvent>(serializer);break;

 case "loggedin": return jo["properties"].ToObject<EventPropertyLoggedIn>(serializer);break;

 case "loggedout": return jo["properties"].ToObject<EventPropertyLoggedOut>(serializer);break;

 case "openedticket": return jo["properties"].ToObject<EventPropertyOpenedTicket>(serializer);break;

 case "ordershipped": return jo["properties"].ToObject<EventPropertyOrderShipped>(serializer);break;

 case "removedcompare": return jo["properties"].ToObject<EventPropertyRemovedCompare>(serializer);break;

 case "removedproduct": return jo["properties"].ToObject<EventPropertyRemovedProduct>(serializer);break;

 case "removedwishlist": return jo["properties"].ToObject<EventPropertyRemovedWishlist>(serializer);break;

 case "repliedticket": return jo["properties"].ToObject<EventPropertyRepliedTicket>(serializer);break;

 case "reviewedproduct": return jo["properties"].ToObject<EventPropertyReviewedProduct>(serializer);break;

 case "searched": return jo["properties"].ToObject<EventPropertySearched>(serializer);break;

 case "servicesubscribed": return jo["properties"].ToObject<EventPropertyServiceSubscribed>(serializer);break;

 case "serviceunsubscribed": return jo["properties"].ToObject<EventPropertyServiceUnsubscribed>(serializer);break;

 case "viewedpage": return jo["properties"].ToObject<EventPropertyViewedPage>(serializer);break;

 case "viewedproduct": return jo["properties"].ToObject<EventPropertyViewedProduct>(serializer);break;

 case "viewedproductcategory": return jo["properties"].ToObject<EventPropertyViewedProductCategory>(serializer);break;

}
 return null;
}

}
}
