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
            #region common variables
            PagedCustomer pagedCustomers = null;
            PagedEvent pagedEvents = null;
            string currentNodeID = ConfigurationManager.AppSettings["node"].ToString();
            #endregion

            #region Example: init contacthub node 
            CHubNode currentNode = new CHubNode(
                        ConfigurationManager.AppSettings["workspaceID"].ToString(),
                        ConfigurationManager.AppSettings["token"].ToString(),
                        currentNodeID
                        );

            #endregion

            #region Example: get all customers
            if (currentNode.isValid)
            {
                List<Customer> allCustomers = new List<Customer>();
                int pageSize = 5;
                bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null);
                if (pageIsValid)
                {
                    allCustomers.AddRange(pagedCustomers._embedded.customers);
                    Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
                    for (int i = 1; i < pagedCustomers.page.totalPages; i++)
                    {
                        pageIsValid = currentNode.GetCustomers(ref pagedCustomers, PageRefEnum.next);
                        allCustomers.AddRange(pagedCustomers._embedded.customers);
                        Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
                    }
                }
            }
            #endregion

            #region Example: alternative method to get all customers pages
            /*
            if (currentNode.isValid)
            {
                List<Customer> allCustomers = new List<Customer>();
                int pageSize = 5;
                bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null);
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
            }
            */
            #endregion

            #region Example: get single page (pageCustomer value must not be null!!!)
            /*
            if (currentNode.isValid)
            {
                bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, 3);
            }
            */
            #endregion

            #region Example: invalid method to get single page (pageCustomer value must not be null!!!)
            /*
            if (currentNode.isValid)
            {
                //this sample return invalid response
                pagedCustomers = null;
                bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, 1);
            }
            */
            #endregion

            #region Example: chiama una pagina next fuori dal set esistente
            /*
            if (currentNode.isValid)
            {
                bool isValid = currentNode.GetCustomers(ref pagedCustomers, CustomersPage.next);
            }
            */
            #endregion

            #region  Example: get customer by externalID
            /*
            if (currentNode.isValid)
            {
                bool isValid = currentNode.GetCustomers(ref pagedCustomers, 10, "2dc51963-4a15-4ffa-943d-16bcc28d19e0", null, null);
            }
            */
            #endregion

            #region Example: get customer by query (DA TESTARE)
            /*
            currentNode.GetCustomers(ref pagedCustomers, null, "externalId='2dc51963-4a15-4ffa-943d-16bcc28d19e0'", null);
            */
            #endregion

            #region Example: get selected fields form customer  
            //return customer with only values in selected fields.
            /*
             currentNode.GetCustomers(ref pagedCustomers, null, null, "base.firstName,base.lastName");
            */
            #endregion

            #region Example: delete customer by name
            /*
            foreach (Customer c in allCustomers)
            {
                if (c.@base.firstName == "Diego")
                {
                    currentNode.DeleteCustomer(c.id);
                }
            }
            */
            #endregion

            #region Example: create new customers in node 
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
            #endregion

            #region Example: create new customers in node with extended properties
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
            #endregion

            #region Example: get specific customer 
            /*
            if (currentNode.isValid)
            {
                Customer customer = currentNode.GetCustomer(customerID);
                customerID = customer.id;
            }
            */
            #endregion

            #region Example: delete customers in node 
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
            #endregion

            #region Example: add like to customer
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
            #endregion

            #region Example: get like detail
            /*
            Likes returnLike = currentNode.GetCustomerLike(myCustomer.id, "eee8c9d6-e30a-4aa9-93f0-db949ba32840");
            */
            #endregion

            #region Example: add education to customer
            /*
            Customer myCustomer = currentNode.GetCustomer("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
            Educations newEdu = new Educations()
            {
                id = "0eae64f3-12fb-49ad-abb9-82ee595037a2",
                schoolConcentration = "123",
                schoolName = "abc",
                schoolType = EducationsSchoolTypeEnum.COLLEGE,

            };

            Educations returnEdu = currentNode.AddCustomerEducation(myCustomer.id, newEdu);
            */
            #endregion

            #region Example: get education detail
            /*
            Educations returnEdu= currentNode.GetCustomerEducation(myCustomer.id, "0eae64f3-12fb-49ad-abb9-82ee595037a2");
            */
            #endregion

            #region Example: add subscription 
            /*
            Customer myCustomer = currentNode.GetCustomer("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
            Subscriptions newSubscription = new Subscriptions()
            {
                id = "b33c4b9e-4bbe-418f-a70b-6fb7384fc4ab",
                name = "prova",
                type = "miotipo",
                kind = SubscriptionsKindEnum.DIGITALMESSAGE,
                enable = true,
                dateStart = DateTime.Now,
                dateEnd = DateTime.Now,
                subscriberId = "e3ab0e11-4310-4329-b70b-a8b0d0250f67",
                registeredAt = DateTime.Now,
                updatedAt = DateTime.Now,
                preferences = new List<Preferences>()
                            {
                                new Preferences()
                                            {
                                                key="chiave1", value="valore123"
                                            }
                                }
            };

            Subscriptions returnSub = currentNode.AddCustomerSubscription(myCustomer.id, newSubscription);
            */
            #endregion

            #region Example: get subscription detail
            /*
            Subscriptions returnSub = currentNode.GetCustomerSubscription(myCustomer.id, "b33c4b9e-4bbe-418f-a70b-6fb7384fc4ab");
            */
            #endregion

            #region Example: add job to customer
            /*
            Customer myCustomer = currentNode.GetCustomer("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");

            Jobs newJob = new Jobs()
            {
                id = "9cb52d39-233b-4739-9830-bcf02186930e",
                companyIndustry = "123",
                companyName = "123",
                jobTitle = "123",
                start_date = DateTime.Now,
                end_date = DateTime.Now,
                isCurrent = true
            };

            Jobs returnJob = currentNode.AddCustomerJob(myCustomer.id, newJob);
            */
            #endregion

            #region  Example: get job detail
            /*
            Jobs returnSub = currentNode.GetCustomerJob(myCustomer.id, "9cb52d39-233b-4739-9830-bcf02186930e");
            */
            #endregion

            #region Example: add session to customer  (DA COMPLETARE!!!)

            //SESSION DA FINIRE

            //Customer myCustomer = currentNode.GetCustomer("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
            //Session newSession = new Session()
            //{
            //    _embedded=new Embedded()
            //    {
            //        sources=new List<Session>()
            //        {
            //            new Session()
            //            {
            //                id="741307a8-2831-46f1-ad09-943ec8fd8b5a",
            //                value="123",
            //                _links=
            //            }
            //        }
            //    }
            //};

            //Jobs returnJob = currentNode.AddCustomerJob(myCustomer.id, newJob);
            #endregion

            #region Example: get session detail (DA COMPLETARE)

            //Sessions returnSub = currentNode.GetCustomerSession(myCustomer.id, "9cb52d39-233b-4739-9830-bcf02186930e");

            #endregion

            #region Example: add event to customer
            /*
            
            Customer myCustomer = currentNode.GetCustomer("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
            PostEvent newEvent = new PostEvent()
            {
                customerId = myCustomer.id,
                type = EventTypeEnum.clickedLink,
                context = EventContextEnum.OTHER,
                properties =  new EventBaseProperty()
            };

            string result = currentNode.AddEvent(newEvent);
            if(result!="Accepted")
            {
                //errore inserimento
            }
            */
            #endregion

            #region Example: add anonymous event (with external ID)
            /*
            PostEvent newEvent = new PostEvent()
            {
                bringBackProperties = new BringBackProperty()
                {
                    nodeId = currentNode.id,
                    type = BringBackPropertyTypeEnum.EXTERNAL_ID,
                    value = Guid.NewGuid().ToString()
                },
                type = EventTypeEnum.loggedIn,
                context = EventContextEnum.WEB,
                properties = new EventBaseProperty()
            };

            string result = currentNode.AddEvent(newEvent);
            if (result != "Accepted")
            {
                //errore inserimento
            }
            */
            #endregion

            #region Example: get customers events (with paging)
            /*
            if (currentNode.isValid)
            {
                List<Event> allEvents = new List<Event>();
                int pageSize = 3;
                //filter by customer id (required)
                bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f", null, null, null, null, null);
                if (pageIsValid)
                {
                    allEvents.AddRange(pagedEvents._embedded.events);
                    Debug.Print(String.Format("Current page {0}/{1}", pagedEvents.page.number + 1, pagedEvents.page.totalPages));
                    for (int i = 1; i < pagedEvents.page.totalPages; i++)
                    {
                        pageIsValid = currentNode.GetEvents(ref pagedEvents, PageRefEnum.next);
                        allEvents.AddRange(pagedEvents._embedded.events);
                        Debug.Print(String.Format("Current page {0}/{1}", pagedEvents.page.number + 1, pagedEvents.page.totalPages));
                    }
                }
            }
            */
            #endregion

            #region Example: get customers events filtered by customer id (required) + eventtype
            /*
            if (currentNode.isValid)
            {
                List<Event> allEvents = new List<Event>();
                int pageSize = 3;
                allEvents.Clear();
                pagedEvents = null;
                bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f", EventTypeEnum.clickedLink, null, null, null, null);
            }
            */
            #endregion

            #region Example: get customers events filtered  by customer id (required) + eventtype + context
            /*
            if (currentNode.isValid)
            {
                List<Event> allEvents = new List<Event>();
                int pageSize = 3;
                allEvents.Clear();
                pagedEvents = null;
                bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f", EventTypeEnum.clickedLink, EventContextEnum.OTHER, null, null, null);
            }
            */
            #endregion

            #region Example: get customers events filtered  by customer id (required) + mode
            /*
            if (currentNode.isValid)
            {
                List<Event> allEvents = new List<Event>();
                int pageSize = 3;
                allEvents.Clear();
                pagedEvents = null;
                bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",null,null, EventModeEnum.ACTIVE, null, null);
            }
            */
            #endregion

            #region Example: get customers events filtered  by customer id (required) + date from|to  ( DA TESTARE, SEMBRA NON FUNZIONARE)

            if (currentNode.isValid)
            {
                List<Event> allEvents = new List<Event>();
                int pageSize = 3;
                allEvents.Clear();
                pagedEvents = null;
                bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f", null, null, null, Convert.ToDateTime("2016-01-01"), Convert.ToDateTime("2016-12-31"));
            }

            #endregion

            #region Example: get event by id
            /*
            if (currentNode.isValid)
            {
                Event ev = currentNode.GetEvent("a47c02d8-c8e0-4d8a-93c0-d35988eaa204");
            }
            */
            #endregion

        }
    }
}

