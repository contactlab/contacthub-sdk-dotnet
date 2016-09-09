
using ContactHubSdkLibrary;
using ContactHubSdkLibrary.Models;

using System;
using System.Collections.Generic;
using System.Configuration;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {

            string currentNodeID = ConfigurationManager.AppSettings["node"].ToString();

            /* Example: open contacthub node */

            CHubNode currentNode = new CHubNode(
                        ConfigurationManager.AppSettings["workspaceID"].ToString(),
                        ConfigurationManager.AppSettings["token"].ToString(),
                        currentNodeID
                        );

            /* Example: retrieve customers list from node (first page) */
            if (currentNode.isValid)
            {
                PagedCustomer customers = currentNode.GetCustomers();

                //Example: delete customer by name
                //foreach(Customer c in customers._embedded.customers)
                //{
                //    if (c.@base.firstName=="Diego")
                //    {
                //        currentNode.DeleteCustomer(c.id);
                //    }
                //}
            }


            ///* Example: create new customers in node */
            ////define new customer
            //PostCustomer newCustomer = new PostCustomer()
            //{
            //    nodeId = currentNodeID,
            //    externalId = Guid.NewGuid().ToString(),
            //    @base = new BaseProperties()
            //    {
            //        firstName = "Diego",
            //        lastName = "Feltrin",
            //        contacts = new Contacts()
            //        {
            //            email = "diego@dimension.it"
            //        },
            //        timezone = BasePropertiesTimezoneEnum.YekaterinburgTime
            //    }
            //};
            ////post new customer
            //string customerID = null;
            //if (currentNode.isValid)
            //{
            //    Customer createdCustomer = currentNode.AddCustomer(newCustomer);
            //    customerID = createdCustomer.id;
            //}


            /* Example: create new customers in node with extended properties*/
            //define new customer



            PostCustomer newCustomer = new PostCustomer()
            {
                nodeId = currentNodeID,
                externalId = Guid.NewGuid().ToString(),
                @base = new BaseProperties()
                {
                    firstName = "Diego",
                    lastName = "Feltrin",
                    contacts = new Contacts()
                    {
                        email = "diego@dimension.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                },
                extended = new List<ExtendedProperty>()
                {
                    new ExtendedPropertyNumber()
                    {
                        name="point",
                        value=100
                    },
                    new ExtendedPropertyString()
                    {
                        name="Length",
                        value="123"
                    },
                    //new ExtendedPropertyStringArray()
                    //{
                    //    name="myStringArray",
                    //    value=new List<String>() { "123", "456" }
                    //},
                    //new ExtendedPropertyNumberArray()
                    //{
                    //    name="myNumberArray",
                    //    value=new List<Double>() { 123.99, 456.99 }
                    //},
                    //new ExtendedPropertyBoolean()
                    //{
                    //    name="myBoolean",
                    //    value=true
                    //},
                    //new ExtendedPropertyObject()
                    //{
                    //    name="myObject",
                    //    value=new List<ExtendedProperty>()
                    //    {
                    //           new ExtendedPropertyNumber()
                    //                    {
                    //                        name="Height",
                    //                        value=1000
                    //                    }
                    //    }
                    //},
                    //new ExtendedPropertyObjectArray()
                    //{
                    //    name="myObjectArray",
                    //    value=new List<ExtendedProperty>()
                    //    {
                    //           new ExtendedPropertyNumber()
                    //                    {
                    //                        name="Height",
                    //                        value=1000
                    //                    },
                    //                    new ExtendedPropertyNumber()
                    //                    {
                    //                        name="Width",
                    //                        value=1000
                    //                    }
                    //    }
                    //},
                    //new ExtendedPropertyDateTime()
                    //{
                    //    name="myDateTime",
                    //    value=DateTime.Now
                    //},
                    //new ExtendedPropertyDateTimeArray()
                    //{
                    //    name="myDateArray",
                    //    value=new List<DateTime>()
                    //    {
                    //        DateTime.Now.Date,DateTime.Now.Date.AddDays(1),DateTime.Now.Date.AddDays(2)
                    //    }
                    //}
                }
            };
            //post new customer
            string customerID = null;
            if (currentNode.isValid)
            {
                Customer createdCustomer = currentNode.AddCustomer(newCustomer);
                if (createdCustomer != null)
                {
                    customerID = createdCustomer.id;
                }
                else
                {
                    //add customer error
                }
            }

            ///* Example: get specific customer */
            //if (currentNode.isValid)
            //{
            //    Customer customer = currentNode.GetCustomer(customerID);
            //    customerID = customer.id;
            //}

            ///* Example: delete customers in node */
            //if (currentNode.isValid)
            //{
            //    currentNode.DeleteCustomer(customerID);
            //    //verify if deleted customer exists
            //    Customer customer = currentNode.GetCustomer(customerID);
            //    if (customer == null)
            //    {
            //        //customer does not exists
            //    }
            //}
        }
    }
}
