/* selfgenerated from version 0.0.0.1 12/10/2016 10:46:46 */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ContactHubSdkLibrary.Events
{
    public class EventBaseProperty { }
    /// <summary>
    /// Event class 'openedTicket': opened ticket
    /// </summary>
    public class EventPropertyOpenedTicket : EventBaseProperty
    {
        public string idTicket { get; set; }
        public List<String> category { get; set; }
        public string subject { get; set; }
        public string text { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'repliedTicket': replied ticket
    /// </summary>
    public class EventPropertyRepliedTicket : EventBaseProperty
    {
        public string idTicket { get; set; }
        public List<String> category { get; set; }
        public string subject { get; set; }
        public string text { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'closedTicket': closed ticket
    /// </summary>
    public class EventPropertyClosedTicket : EventBaseProperty
    {
        public string idTicket { get; set; }
        public List<String> category { get; set; }
        public string subject { get; set; }
        public string text { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'loggedIn': logged in
    /// </summary>
    public class EventPropertyLoggedIn : EventBaseProperty
    {
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'loggedOut': logged out
    /// </summary>
    public class EventPropertyLoggedOut : EventBaseProperty
    {
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'reviewedProduct': reviewed product
    /// </summary>
    public class EventPropertyReviewedProduct : EventBaseProperty
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string imageUrl { get; set; }
        public string linkUrl { get; set; }
        public string shortDescription { get; set; }
        public List<String> category { get; set; }
        public List<Classifications> classifications { get; set; }
        public string rating { get; set; }
        public dynamic extraProperties { get; set; }
    }


    public class Classifications
    {
        public string key { get; set; }
        public string value { get; set; }
    }


    /// <summary>
    /// Event class 'viewedProductCategory': viewed product category
    /// </summary>
    public class EventPropertyViewedProductCategory : EventBaseProperty
    {
        public string category { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'viewedProduct': viewed product
    /// </summary>
    public class EventPropertyViewedProduct : EventBaseProperty
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string imageUrl { get; set; }
        public string linkUrl { get; set; }
        public string shortDescription { get; set; }
        public List<String> category { get; set; }
        public List<Classifications> classifications { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'addedProduct': added product
    /// </summary>
    public class EventPropertyAddedProduct : EventBaseProperty
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal quantity { get; set; }
        public string imageUrl { get; set; }
        public string linkUrl { get; set; }
        public string shortDescription { get; set; }
        public List<String> category { get; set; }
        public List<Classifications> classifications { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'removedProduct': removed product
    /// </summary>
    public class EventPropertyRemovedProduct : EventBaseProperty
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal quantity { get; set; }
        public string imageUrl { get; set; }
        public string linkUrl { get; set; }
        public string shortDescription { get; set; }
        public List<String> category { get; set; }
        public List<Classifications> classifications { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'addedWishlist': added wishlist
    /// </summary>
    public class EventPropertyAddedWishlist : EventBaseProperty
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string imageUrl { get; set; }
        public string linkUrl { get; set; }
        public string shortDescription { get; set; }
        public List<String> category { get; set; }
        public List<Classifications> classifications { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'removedWishlist': removed wishlist
    /// </summary>
    public class EventPropertyRemovedWishlist : EventBaseProperty
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string imageUrl { get; set; }
        public string linkUrl { get; set; }
        public string shortDescription { get; set; }
        public List<String> category { get; set; }
        public List<Classifications> classifications { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'addedCompare': added compare
    /// </summary>
    public class EventPropertyAddedCompare : EventBaseProperty
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string imageUrl { get; set; }
        public string linkUrl { get; set; }
        public string shortDescription { get; set; }
        public List<String> category { get; set; }
        public List<Classifications> classifications { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'removedCompare': removed compare
    /// </summary>
    public class EventPropertyRemovedCompare : EventBaseProperty
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string imageUrl { get; set; }
        public string linkUrl { get; set; }
        public string shortDescription { get; set; }
        public List<String> category { get; set; }
        public List<Classifications> classifications { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'completedOrder': completed order
    /// </summary>
    public class EventPropertyCompletedOrder : EventBaseProperty
    {
        public string orderId { get; set; }
        public string type { get; set; }
        public string storeCode { get; set; }
        public string paymentMethod { get; set; }
        public Amount amount { get; set; }
        public List<Products> products { get; set; }
        public dynamic extraProperties { get; set; }
    }


    public class Amount
    {
        public decimal total { get; set; }
        public decimal revenue { get; set; }
        public decimal shipping { get; set; }
        public decimal tax { get; set; }
        public decimal discount { get; set; }
        public string currency { get; set; }
        public string currency_local { get; set; }
        public decimal exchangeRate { get; set; }
    }


    public class Products
    {
        public string id { get; set; }
        public string type { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal quantity { get; set; }
        public decimal discount { get; set; }
        public decimal tax { get; set; }
        public string coupon { get; set; }
        public string imageUrl { get; set; }
        public string linkUrl { get; set; }
        public string shortDescription { get; set; }
        public List<String> category { get; set; }
        public List<Classifications> classifications { get; set; }
    }


    /// <summary>
    /// Event class 'viewedPage': viewed page
    /// </summary>
    public class EventPropertyViewedPage : EventBaseProperty
    {
        public string path { get; set; }
        public string referrer { get; set; }
        public string search { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public List<String> pageTags { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'clickedLink': clicked link
    /// </summary>
    public class EventPropertyClickedLink : EventBaseProperty
    {
        public string path { get; set; }
        public string referrer { get; set; }
        public string search { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public List<String> linkTags { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'formCompiled': form compiled
    /// </summary>
    public class EventPropertyFormCompiled : EventBaseProperty
    {
        public string formName { get; set; }
        public string formId { get; set; }
        public dynamic data { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'campaignSent': campaign sent
    /// </summary>
    public class EventPropertyCampaignSent : EventBaseProperty
    {
        public string subscriberId { get; set; }
        public string campaignSubject { get; set; }
        public string campaignId { get; set; }
        public string campaignName { get; set; }
        public List<String> campaignTags { get; set; }
        public string channel { get; set; }
        public dynamic extraProperties { get; set; }
    }


    /// <summary>
    /// Event class 'campaignOpened': campaign opened
    /// </summary>
    public class EventPropertyCampaignOpened : EventBaseProperty
    {
        public string subscriberId { get; set; }
        public string campaignSubject { get; set; }
        public string campaignId { get; set; }
        public string campaignName { get; set; }
        public List<String> campaignTags { get; set; }
        public string channel { get; set; }
        public dynamic extraProperties { get; set; }
    }


    public enum EventTypeEnum
    {
        NoValue,
        [Display(Name = "openedTicket")]
        openedTicket,
        [Display(Name = "repliedTicket")]
        repliedTicket,
        [Display(Name = "closedTicket")]
        closedTicket,
        [Display(Name = "loggedIn")]
        loggedIn,
        [Display(Name = "loggedOut")]
        loggedOut,
        [Display(Name = "reviewedProduct")]
        reviewedProduct,
        [Display(Name = "viewedProductCategory")]
        viewedProductCategory,
        [Display(Name = "viewedProduct")]
        viewedProduct,
        [Display(Name = "addedProduct")]
        addedProduct,
        [Display(Name = "removedProduct")]
        removedProduct,
        [Display(Name = "addedWishlist")]
        addedWishlist,
        [Display(Name = "removedWishlist")]
        removedWishlist,
        [Display(Name = "addedCompare")]
        addedCompare,
        [Display(Name = "removedCompare")]
        removedCompare,
        [Display(Name = "completedOrder")]
        completedOrder,
        [Display(Name = "viewedPage")]
        viewedPage,
        [Display(Name = "clickedLink")]
        clickedLink,
        [Display(Name = "formCompiled")]
        formCompiled,
        [Display(Name = "campaignSent")]
        campaignSent,
        [Display(Name = "campaignOpened")]
        campaignOpened
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
                case "openedticket": return jo["properties"].ToObject<EventPropertyOpenedTicket>(serializer); break;

                case "repliedticket": return jo["properties"].ToObject<EventPropertyRepliedTicket>(serializer); break;

                case "closedticket": return jo["properties"].ToObject<EventPropertyClosedTicket>(serializer); break;

                case "loggedin": return jo["properties"].ToObject<EventPropertyLoggedIn>(serializer); break;

                case "loggedout": return jo["properties"].ToObject<EventPropertyLoggedOut>(serializer); break;

                case "reviewedproduct": return jo["properties"].ToObject<EventPropertyReviewedProduct>(serializer); break;

                case "viewedproductcategory": return jo["properties"].ToObject<EventPropertyViewedProductCategory>(serializer); break;

                case "viewedproduct": return jo["properties"].ToObject<EventPropertyViewedProduct>(serializer); break;

                case "addedproduct": return jo["properties"].ToObject<EventPropertyAddedProduct>(serializer); break;

                case "removedproduct": return jo["properties"].ToObject<EventPropertyRemovedProduct>(serializer); break;

                case "addedwishlist": return jo["properties"].ToObject<EventPropertyAddedWishlist>(serializer); break;

                case "removedwishlist": return jo["properties"].ToObject<EventPropertyRemovedWishlist>(serializer); break;

                case "addedcompare": return jo["properties"].ToObject<EventPropertyAddedCompare>(serializer); break;

                case "removedcompare": return jo["properties"].ToObject<EventPropertyRemovedCompare>(serializer); break;

                case "completedorder": return jo["properties"].ToObject<EventPropertyCompletedOrder>(serializer); break;

                case "viewedpage": return jo["properties"].ToObject<EventPropertyViewedPage>(serializer); break;

                case "clickedlink": return jo["properties"].ToObject<EventPropertyClickedLink>(serializer); break;

                case "formcompiled": return jo["properties"].ToObject<EventPropertyFormCompiled>(serializer); break;

                case "campaignsent": return jo["properties"].ToObject<EventPropertyCampaignSent>(serializer); break;

                case "campaignopened": return jo["properties"].ToObject<EventPropertyCampaignOpened>(serializer); break;

            }
            return null;
        }

    }
}
