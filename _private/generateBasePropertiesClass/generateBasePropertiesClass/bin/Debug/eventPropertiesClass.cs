/* selfgenerated from version 0.0.0.1 20/10/2016 11:34:20 */

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
    public string imageUrl {get;set;}
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
    public string imageUrl {get;set;}
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
    public string imageUrl {get;set;}
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
    public string imageUrl {get;set;}
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
    public string imageUrl {get;set;}
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
    public string imageUrl {get;set;}
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
    public string imageUrl {get;set;}
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
    public string imageUrl {get;set;}
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
    public string type {get;set;}
    public string storeCode {get;set;}
    public string paymentMethod {get;set;}
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
    public string currency_local {get;set;}
    public decimal exchangeRate {get;set;}
}


public class Products
{
    public string id {get;set;}
    public string type {get;set;}
    public string sku {get;set;}
    public string name {get;set;}
    public decimal price {get;set;}
    public decimal quantity {get;set;}
    public decimal discount {get;set;}
    public decimal tax {get;set;}
    public string coupon {get;set;}
    public string imageUrl {get;set;}
    public string linkUrl {get;set;}
    public string shortDescription {get;set;}
    public List<String> category {get;set;}
    public List<Classifications> classifications {get;set;}
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
    public string channel {get;set;}
    public dynamic extraProperties {get;set;}
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
    public string channel {get;set;}
    public dynamic extraProperties {get;set;}
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
    public string channel {get;set;}
    public string linkId {get;set;}
    public string linkUrl {get;set;}
    public List<String> linkTags {get;set;}
    public dynamic extraProperties {get;set;}
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
    public string channel {get;set;}
    public dynamic extraProperties {get;set;}
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
    public string channel {get;set;}
    public dynamic extraProperties {get;set;}
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
    public string channel {get;set;}
    public string listId {get;set;}
    public string listName {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'campaignSubscribed': campaign subscribed
/// </summary>
public class EventPropertyCampaignSubscribed: EventBaseProperty
{
    public string subscriberId {get;set;}
    public string listId {get;set;}
    public string listName {get;set;}
    public string channel {get;set;}
    public dynamic extraProperties {get;set;}
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
    public string setting {get;set;}
    public string oldValue {get;set;}
    public string newValue {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'genericActiveEvent': generic active event
/// </summary>
public class EventPropertyGenericActiveEvent: EventBaseProperty
{
    public string name {get;set;}
    public dynamic data {get;set;}
    public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'genericPassiveEvent': generic passive event
/// </summary>
public class EventPropertyGenericPassiveEvent: EventBaseProperty
{
    public string name {get;set;}
    public dynamic data {get;set;}
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
                public dynamic extraProperties {get;set;}
}


/// <summary>
/// Event class 'abandonedCart': abandoned cart
/// </summary>
public class EventPropertyAbandonedCart: EventBaseProperty
{
    public string orderId {get;set;}
    public string abandonedCartUrl {get;set;}
    public string storeCode {get;set;}
    public Amount amount {get;set;}
    public Products products {get;set;}
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
