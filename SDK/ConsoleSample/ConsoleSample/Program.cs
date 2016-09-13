using ContactHubSdkLibrary;
using ContactHubSdkLibrary.Models;
using ContactHubSdkLibrary.SDKclasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;


namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {

            PagedCustomer pagedCustomers = null;

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
                //Example get all customers pages

                List<Customer> allCustomers = new List<Customer>();
                int pageSize = 5;
                bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null);
                if (pageIsValid)
                {
                    allCustomers.AddRange(pagedCustomers._embedded.customers);
                    Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
                    for (int i = 1; i < pagedCustomers.page.totalPages; i++)
                    {
                        pageIsValid = currentNode.GetCustomers(ref pagedCustomers, CustomersPage.next);
                        allCustomers.AddRange(pagedCustomers._embedded.customers);
                        Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
                    }
                }


                //Example: alternative method to get all customers pages
                /*
                allCustomers = new List<Customer>();
                pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null);
                if (pageIsValid)
                {
                    allCustomers.AddRange(pagedCustomers._embedded.customers);
                    Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
                    while (currentNode.GetCustomers(ref pagedCustomers, CustomersPage.next))
                    {

                        allCustomers.AddRange(pagedCustomers._embedded.customers);
                        Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
                    }
                }
                */

                //Example: get single page (pageCustomer value must not be null!!!)
                /*
                pageIsValid = currentNode.GetCustomers(ref pagedCustomers, 3);
                */

                //Example: invalid method to get single page (pageCustomer value must not be null!!!)
                // return invalid response
                /*
                pagedCustomers = null;
                pageIsValid= currentNode.GetCustomers(ref pagedCustomers, 1);
                */

                //Example: chiama una pagina next fuori dal set esistente
                /*
                 currentNode.GetCustomers(ref pagedCustomers, CustomersPage.next); //returns the same page if there are no other
                 */
                //Example: get customer by externalID
                /*
                bool isValid= currentNode.GetCustomers(ref pagedCustomers,10, "2dc51963-4a15-4ffa-943d-16bcc28d19e0", null, null);
                */

                //Example: get customer by query  (DA TESTARE)
                /*
                currentNode.GetCustomers(ref pagedCustomers, null, "externalId='2dc51963-4a15-4ffa-943d-16bcc28d19e0'", null);
                */

                //Example: get selected fields form customer  
                //return customer with only values in selected fields.
                /*
                 currentNode.GetCustomers(ref pagedCustomers, null, null, "base.firstName,base.lastName");
                */
                //Example: delete customer by name
                /*
                foreach (Customer c in allCustomers)
                {
                    if (c.@base.firstName == "Diego")
                    {
                        currentNode.DeleteCustomer(c.id);
                    }
                }
                */
            }


            ///* Example: create new customers in node */
            //define new customer
            /*
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
                    timezone = BasePropertiesTimezoneEnum.YekaterinburgTime
                }
            };
            //post new customer
            string customerID = null;
            if (currentNode.isValid)
            {
                Customer createdCustomer = currentNode.AddCustomer(newCustomer);
                customerID = createdCustomer.id;
            }
            */

            /* Example: create new customers in node with extended properties*/
            //define new customer

            /*

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
            new ExtendedPropertyStringArray()
            {
                name="myStringArray",
                value=new List<String>() { "123", "456" }
            },
            new ExtendedPropertyNumberArray()
            {
                name="myNumberArray",
                value=new List<Double>() { 123.99, 456.99 }
            },
            new ExtendedPropertyBoolean()
            {
                name="myBoolean",
                value=true
            },
            new ExtendedPropertyObject()
            {
                name="myObject",
                value=new List<ExtendedProperty>()
                {
                       new ExtendedPropertyNumber()
                                {
                                    name="Height",
                                    value=1000
                                }
                }
            },
            new ExtendedPropertyObjectArray()
            {
                name="myObjectArray",
                value=new List<ExtendedProperty>()
                {
                       new ExtendedPropertyNumber()
                                {
                                    name="Height",
                                    value=1000
                                },
                                new ExtendedPropertyNumber()
                                {
                                    name="Width",
                                    value=1000
                                }
                }
            },
            new ExtendedPropertyDateTime()
            {
                name="myDateTime",
                value=DateTime.Now
            },
            new ExtendedPropertyDateTimeArray()
            {
                name="myDateArray",
                value=new List<DateTime>()
                {
                    DateTime.Now.Date,DateTime.Now.Date.AddDays(1),DateTime.Now.Date.AddDays(2)
                }
            }
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
            */

            /* Example: get specific customer */
            /*
            if (currentNode.isValid)
            {
                Customer customer = currentNode.GetCustomer(customerID);
                customerID = customer.id;
            }
            */

            /* Example: delete customers in node */
            /*
            if (currentNode.isValid)
            {
                currentNode.DeleteCustomer(customerID);
                //verify if deleted customer exists
                Customer customer = currentNode.GetCustomer(customerID);
                if (customer == null)
                {
                    //customer does not exists
                }
            }
            */

            //Example: add like to customer
            /*
            Customer myCustomer = currentNode.GetCustomer("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
            
            Likes newLike = new Likes()
            {
                category = "sport",
                id = "eee8c9d6-e30a-4aa9-93f0-db949ba32841",
                name = "tennis",
                createdTime=DateTime.Now
            };

            Likes returnLike=currentNode.AddCustomerLike(myCustomer.id, newLike);
            */

            //Example: get like detail
            /*
            Likes returnLike = currentNode.GetCustomerLike(myCustomer.id, "eee8c9d6-e30a-4aa9-93f0-db949ba32840");
          */

            //Example: add education to customer

            Customer myCustomer = currentNode.GetCustomer("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");

            
            Educations newEdu = new Educations()
            {
                id = "0eae64f3-12fb-49ad-abb9-82ee595037a2",
                schoolConcentration = "123",
                schoolName = "abc",
                schoolType = EducationsSchoolTypeEnum.COLLEGE,

            };

            Educations returnEdu = currentNode.AddCustomerEducation(myCustomer.id, newEdu);
            

            //Example: get edu detail

            Educations returnLike = currentNode.GetCustomerEducation(myCustomer.id, "0eae64f3-12fb-49ad-abb9-82ee595037a2");




        }
    }
}
