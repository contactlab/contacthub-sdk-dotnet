using ContactHubSdkLibrary.Models;
using Newtonsoft.Json;

namespace ContactHubSdkLibrary.SDKclasses
{

    public enum CustomerTagTypeEnum
    {
        Auto,
        Manual
    }
    public partial class Node
    {
        #region customers tag
        /// <summary>
        /// Get customers tags
        /// </summary>
        public Tags GetCustomerTags(string customerID)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            Customer c = GetCustomerByID(customerID);
            if (c != null)
            {
                return c.tags;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Add new tag to customer
        /// </summary>
        public Tags AddCustomerTag(string customerID, string tag, CustomerTagTypeEnum type)
        {
            string fixedTag = tag.Trim();
            Tags currentTags = GetCustomerTags(customerID);
            bool exists = false;
            //discrimina tra auto e manual
            switch (type)
            {
                case CustomerTagTypeEnum.Auto:
                    //verifica se già presente
                    exists = (currentTags != null && currentTags.auto!=null && currentTags.auto.Contains(fixedTag));
                    if (!exists)
                    {
                        if (currentTags == null) currentTags = new Tags();
                        if (currentTags.auto == null) currentTags.auto = new System.Collections.Generic.List<string>();
                        currentTags.auto.Add(fixedTag);
                    }
                    break;
                case CustomerTagTypeEnum.Manual:
                    //verifica se già presente
                    exists = (currentTags!=null && currentTags.manual != null && currentTags.manual.Contains(fixedTag));
                    if (!exists)
                    {
                        if (currentTags == null) currentTags = new Tags();
                        if (currentTags.manual == null) currentTags.manual = new System.Collections.Generic.List<string>();
                        currentTags.manual.Add(fixedTag);
                    }
                    break;
            }
            if (!exists)
            {
                //manda in patch la modifica al customer
                PostCustomer patchCustomer = new PostCustomer();
                patchCustomer.tags = currentTags;
                UpdateCustomer(patchCustomer, customerID, false);
            }
            return currentTags;
        }
        /// <summary>
        /// Remove tag from customer
        /// </summary>
        public Tags RemoveCustomerTag(string customerID, string tag, CustomerTagTypeEnum type)
        {
            string fixedTag = tag.Trim();
            Tags currentTags = GetCustomerTags(customerID);

            //verifica se è già presente
            bool exists = false;
            //discrimina tra auto e manual
            switch (type)
            {
                case CustomerTagTypeEnum.Auto:
                    exists = (currentTags != null && currentTags.auto != null && currentTags.auto.Contains(fixedTag));
                    if (exists)
                    {
                        currentTags.auto.Remove(fixedTag);
                    }
                    break;
                case CustomerTagTypeEnum.Manual:
                    exists = (currentTags != null && currentTags.manual != null && currentTags.manual.Contains(fixedTag));
                    if (exists)
                    {
                        currentTags.manual.Remove(fixedTag);
                    }
                    break;
            }
            //manda in patch la modifica al customer
            if (exists)
            {
                PostCustomer patchCustomer = new PostCustomer();
                patchCustomer.tags = currentTags;
                UpdateCustomer(patchCustomer, customerID, false);
            }
            return currentTags;
        }
        #endregion
    }
}
