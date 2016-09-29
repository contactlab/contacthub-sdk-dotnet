using ContactHubSdkLibrary;
using ContactHubSdkLibrary.Events;
using ContactHubSdkLibrary.Models;
using ContactHubSdkLibrary.SDKclasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

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
            List<Customer> allCustomers = new List<Customer>();
            #endregion

            #region Example: init contacthub node 
            CHubNode currentNode = new CHubNode(
                        ConfigurationManager.AppSettings["workspaceID"].ToString(),
                        ConfigurationManager.AppSettings["token"].ToString(),
                        currentNodeID
                        );

            #endregion

            #region CUSTOMERS
            #region Example: get all customers
            if (false)
            {
                if (currentNode.isValid)
                {
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
            }
            #endregion

            #region Example: alternative method to get all customers pages
            if (false)
            {
                if (currentNode.isValid)
                {
                    allCustomers = new List<Customer>();
                    int pageSize = 5;
                    bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null);
                    if (pageIsValid)
                    {
                        allCustomers.AddRange(pagedCustomers._embedded.customers);
                        Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
                        while (currentNode.GetCustomers(ref pagedCustomers, PageRefEnum.next))
                        {

                            allCustomers.AddRange(pagedCustomers._embedded.customers);
                            Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
                        }
                    }
                }
            }
            #endregion

            #region Example: get single page (pageCustomer value must not be null!!!)
            if (false)
            {
                if (currentNode.isValid)
                {
                    bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, 3);
                }
            }
            #endregion

            #region Example: invalid method to get single page (pageCustomer value must not be null!!!)
            if (false)
            {
                if (currentNode.isValid)
                {
                    //this sample return invalid response
                    pagedCustomers = null;
                    bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, 1);
                }
            }
            #endregion

            #region Example: get invalid page
            if (false)
            {
                if (currentNode.isValid)
                {
                    bool isValid = currentNode.GetCustomers(ref pagedCustomers, PageRefEnum.next);
                }
            }
            #endregion

            #region  Example: get customer by externalID (using GetCustomers() , returns single customers in array )
            if (false)
            {
                if (currentNode.isValid)
                {
                    bool isValid = currentNode.GetCustomers(ref pagedCustomers, 10, "2dc51963-4a15-4ffa-943d-16bcc28d19e0", null, null);
                }
            }
            #endregion

            #region  Example: get customer by externalID (using GetCustomer(), returns single customer)
            if (false)
            {
                if (currentNode.isValid)
                {
                    Customer customerByExtID = currentNode.GetCustomerByExternalID("2dc51963-4a15-4ffa-943d-16bcc28d19e0");
                }
            }
            #endregion

            #region Example: get customer with custom query
            if (false)
            {
                string querySTR = @"{
                                    ""name"": """",
                                    ""query"": {
                                                ""are"": {
                                                    ""condition"": {
                                                        ""attribute"": ""base.firstName"",
                                                        ""operator"": ""EQUALS"",
                                                        ""type"": ""atomic"",
                                                        ""value"": ""Diego""
                                                                    }
                                                         },
                                                ""name"": ""No name"",
                                                ""type"": ""simple""
                                                }
                                        }";
                currentNode.GetCustomers(ref pagedCustomers, 10, null, querySTR, null);
            }
            #endregion

            #region Example: get customer with query builder
            if (false)
            {
                QueryBuilder qb = new QueryBuilder();
                qb.AddQuery(new QueryBuilderItem() { attributeName = "base.firstName", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = "Diego" });
                qb.AddQuery(new QueryBuilderItem() { attributeName = "base.lastName", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = "Feltrin" });
                currentNode.GetCustomers(ref pagedCustomers, 10, null, qb.GenerateQuery(QueryBuilderConjunctionEnum.AND), null);
            }
            #endregion

            #region Example: get selected fields form customer  
            //return customer with only values in selected fields.
            if (false)
            {
                currentNode.GetCustomers(ref pagedCustomers, 10, null, null, "base.firstName,base.lastName");
            }
            #endregion

            #region Example: delete customer by name
            if (false)
            {
                foreach (Customer c in allCustomers)
                {
                    if (c.@base.firstName == "Diego")
                    {
                        currentNode.DeleteCustomer(c.id);
                    }
                }
            }
            #endregion

            #region Example: create new customers in node 
            if (false)
            {
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
                    Customer createdCustomer = currentNode.AddCustomer(newCustomer, false);
                    customerID = createdCustomer.id;
                }
            }
            #endregion

            #region Example: force update on existing customer on addCustomer
            if (false)
            {
                currentNode.GetCustomers(ref pagedCustomers, 110, "2dc51963-4a15-4ffa-943d-16bcc28d19e0", null, null);

                PostCustomer updateCustomer = pagedCustomers._embedded.customers.First();
                updateCustomer.extra = "CAMPO AGGIORNATO IN PUT " + DateTime.Now.ToShortTimeString();

                if (currentNode.isValid)
                {
                    Customer createdCustomer = currentNode.AddCustomer(updateCustomer, true);  //force update
                }
            }
            #endregion

            #region Example: update customer (full update)
            if (false)
            {
                currentNode.GetCustomers(ref pagedCustomers, 110, "2dc51963-4a15-4ffa-943d-16bcc28d19e0", null, null);

                Customer updateCustomer = pagedCustomers._embedded.customers.First();
                updateCustomer.extra = "CAMPO AGGIORNATO IN PUT " + DateTime.Now.ToShortTimeString();

                if (currentNode.isValid)
                {
                    Customer customer = currentNode.UpdateCustomer((PostCustomer)updateCustomer, updateCustomer.id, true);
                }
            }
            #endregion

            #region Example: update customer (partial update)
            if (false)
            {
                Customer c = currentNode.GetCustomerByExternalID("2dc51963-4a15-4ffa-943d-16bcc28d19e0");

                PostCustomer partialData = new PostCustomer();
                partialData.extra = "CAMPO AGGIORNATO IN PATCH " + DateTime.Now.ToShortTimeString();

                if (currentNode.isValid)
                {
                    string customerID = c.id;
                    Customer customer = currentNode.UpdateCustomer((PostCustomer)partialData, customerID, false);
                }
            }
            #endregion

            #region Example: create new customers in node with extended properties
            if (false)
            {
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
            }
            #endregion

            #region Example: get specific customer 
            if (false)
            {
                string customerID = "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f";
                if (currentNode.isValid)
                {
                    Customer customer = currentNode.GetCustomerByID(customerID);
                    customerID = customer.id;
                }
            }
            #endregion

            #region Example: delete customers in node 
            if (false)
            {
                string customerID = "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f";
                if (currentNode.isValid)
                {
                    currentNode.DeleteCustomer(customerID);
                    //verify if deleted customer exists
                    Customer customer = currentNode.GetCustomerByID(customerID);
                    if (customer == null)
                    {
                        //customer does not exists
                    }
                }
            }
            #endregion
            #endregion

            #region LIKE
            #region Example: add like to customer
            if (false)
            {
                Customer myCustomer = currentNode.GetCustomerByID("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
                Likes newLike = new Likes()
                {
                    category = "sport",
                    id = "eee8c9d6-e30a-4aa9-93f0-db949ba32841",
                    name = "tennis",
                    createdTime = DateTime.Now
                };
                Likes returnLike = currentNode.AddCustomerLike(myCustomer.id, newLike);
            }
            #endregion

            #region Example: get like detail
            if (false)
            {
                string customerID = "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f";
                string likeID = "eee8c9d6-e30a-4aa9-93f0-db949ba32840";
                Likes returnLike = currentNode.GetCustomerLike(customerID, likeID);
            }
            #endregion

            #region Example: update like detail
            if (false)
            {
                string customerID = "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f";
                string likeID = "eee8c9d6-e30a-4aa9-93f0-db949ba32840";
                Likes l = currentNode.GetCustomerLike(customerID, likeID);
                l.category = "music";
                Likes updatedLike = currentNode.UpdateCustomerLike(customerID, l);
            }
            #endregion

            #endregion

            #region EDUCATION
            #region Example: add education to customer
            if (false)
            {
                Customer myCustomer = currentNode.GetCustomerByID("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
                Educations newEdu = new Educations()
                {
                    id = "0eae64f3-12fb-49ad-abb9-82ee595037a2",
                    schoolConcentration = "123",
                    schoolName = "abc",
                    schoolType = EducationsSchoolTypeEnum.COLLEGE,

                };

                Educations returnEdu = currentNode.AddCustomerEducation(myCustomer.id, newEdu);
            }
            #endregion

            #region Example: get education detail
            if (false)
            {
                string customerID = "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f";
                string educationID = "0eae64f3-12fb-49ad-abb9-82ee595037a2";
                Educations returnEdu = currentNode.GetCustomerEducation(customerID, educationID);
            }
            #endregion

            #region Example: update education detail
            if (false)
            {
                string customerID = "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f";
                string educationID = "0eae64f3-12fb-49ad-abb9-82ee595037a2";
                Educations edu = currentNode.GetCustomerEducation(customerID, educationID);
                edu.startYear = 2010;
                edu.endYear = 2016;
                Educations updatedEducation = currentNode.UpdateCustomerEducation(customerID, edu);
            }
            #endregion
            #endregion

            #region SUBSCRIPTION
            #region Example: add subscription 
            if (false)
            {
                Customer myCustomer = currentNode.GetCustomerByID("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
                Subscriptions newSubscription = new Subscriptions()
                {
                    id = "b33c4b9e-4bbe-418f-a70b-6fb7384fc4ab",
                    name = "prova",
                    type = "miotipo",
                    kind = SubscriptionsKindEnum.DIGITALMESSAGE,
                    //enable = true,
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
            }
            #endregion

            #region Example: get subscription detail
            if (false)
            {
                string customerID = "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f";
                string subscriptionID = "b33c4b9e-4bbe-418f-a70b-6fb7384fc4ab";
                Subscriptions returnSub = currentNode.GetCustomerSubscription(customerID, subscriptionID);
            }
            #endregion

            #region Example: update subscription detail
            if (false)
            {
                string customerID = "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f";
                string subscriptionID = "b33c4b9e-4bbe-418f-a70b-6fb7384fc4ab";
                Subscriptions s = currentNode.GetCustomerSubscription(customerID, subscriptionID);
                s.dateStart = DateTime.Now;
                s.dateEnd = DateTime.Now.AddDays(10);
                Subscriptions updatedSubscription = currentNode.UpdateCustomerSubscription(customerID, s);
            }
            #endregion
            #endregion

            #region JOB
            #region Example: add job to customer
            if (false)
            {
                Customer myCustomer = currentNode.GetCustomerByID("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
                Jobs newJob = new Jobs()
                {
                    id = "9cb52d39-233b-4739-9830-bcf02186930e",
                    companyIndustry = "123",
                    companyName = "123",
                    jobTitle = "123",
                    startDate = DateTime.Now,
                    endDate = DateTime.Now.AddDays(1),
                    isCurrent = true
                };

                Jobs returnJob = currentNode.AddCustomerJob(myCustomer.id, newJob);
            }
            #endregion

            #region  Example: get job detail
            if (false)
            {
                string customerID = "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f";
                string jobID = "9cb52d39-233b-4739-9830-bcf02186930e";
                Jobs returnSub = currentNode.GetCustomerJob(customerID, jobID);
            }
            #endregion

            #region Example: update job detail
            if (false)
            {
                string customerID = "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f";
                string jobID = "9cb52d39-233b-4739-9830-bcf02186930e";
                Jobs j = currentNode.GetCustomerJob(customerID, jobID);
                j.startDate = DateTime.Now;
                j.endDate = DateTime.Now.AddDays(10);
                Jobs updatedJob = currentNode.UpdateCustomerJob(customerID, j);
            }
            #endregion
            #endregion

            #region SESSION
            #region Example: create a new session ad assign to customer
            if (false)
            {
                Customer myCustomer = currentNode.GetCustomerByID("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
                Session newSession = new Session();
                Session returnSession = currentNode.AddCustomerSession(myCustomer.id, newSession);
            }
            #endregion

            #region Example: create and reset Session
            if (false)
            {
                Session newSession = new Session();
                //[...] use the session, then reset it
                newSession.ResetID();
                var newID = newSession.id;
            }
            #endregion

            #endregion

            #region EVENTS
            #region Example: add event to customer
            if (false)
            {
                Customer myCustomer = currentNode.GetCustomerByID("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
                PostEvent newEvent = new PostEvent()
                {
                    customerId = myCustomer.id,
                    type = EventTypeEnum.clickedLink,
                    context = EventContextEnum.OTHER,
                    properties = new EventBaseProperty()
                };

                string result = currentNode.AddEvent(newEvent);
                if (result != "Accepted")
                {
                    //insert error
                }
            }
            #endregion

            #region Example: add customer event with properties and contextInfo (with external ID)
            if (true)
            {
                Customer myCustomer = currentNode.GetCustomerByID("5a0c7812-daa9-467a-b641-012d25b9cdd5"); //giorgio napolitano
                EventPropertyRepliedTicket typeProperties = new EventPropertyRepliedTicket()
                {
                    category = new List<String>() { "1" },
                    idTicket = "1",
                    subject = "web form question",
                    text = "lorem ipsum"
                };

                EventContextPropertyWEB contextProperties = new EventContextPropertyWEB()
                {
                    client = new Client()
                    {
                        ip = "192.168.1.1/16",
                        userAgent = "Mozilla"
                    }
                };

                PostEvent newEvent = new PostEvent()
                {
                    customerId = myCustomer.id,
                    type = EventTypeEnum.repliedTicket,
                    context = EventContextEnum.WEB,
                    properties = typeProperties,
                    contextInfo = contextProperties,
                    date = DateTime.Now
                };

                string result = currentNode.AddEvent(newEvent);
                if (result != "Accepted")
                {
                    //insert error
                }
            }
            #endregion

            #region Example: add anonymous event (with external ID)
            if (false)
            {
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
                    //insert error
                }
            }
            #endregion



            #region Example: add anonymous event (with sessionID) + customers reconciliation  (DA FINIRE DI TESTARE, AL MOMENTO HUB NON RICONCILIA VIA SESSION_ID)
            if (false)
            {
                //create new session
                Session currentSession = new Session();
                PostEvent newEvent = new PostEvent()
                {
                    bringBackProperties = new BringBackProperty()
                    {
                        nodeId = currentNode.id,
                        type = BringBackPropertyTypeEnum.SESSION_ID,
                        value = currentSession.value
                    },
                    type = EventTypeEnum.loggedIn,
                    context = EventContextEnum.WEB,
                    properties = new EventBaseProperty()
                };

                string result = currentNode.AddEvent(newEvent);
                if (result != "Accepted")
                {
                    //insert error
                }
                else
                {
                    //reconciles the events on the customer through the session ID
                    Customer myCustomer = currentNode.GetCustomerByID("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
                }
            }
            #endregion

            #region Example: get customers events (with paging)
            if (true)
            {
                List<Event> allEvents = new List<Event>();
                if (currentNode.isValid)
                {
                    int pageSize = 10;
                    //filter by customer id (required)
                    bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "5a0c7812-daa9-467a-b641-012d25b9cdd5", null, null, null, null, null);
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
            }
            #endregion

            #region Example: get customers events filtered by customer id (required) + eventtype
            if (false)
            {
                List<Event> allEvents = new List<Event>();
                if (currentNode.isValid)
                {
                    int pageSize = 3;
                    allEvents.Clear();
                    pagedEvents = null;
                    bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f", EventTypeEnum.clickedLink, null, null, null, null);
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
            }
            #endregion

            #region Example: get customers events filtered  by customer id (required) + eventtype + context
            if (false)
            {
                List<Event> allEvents = new List<Event>();
                if (currentNode.isValid)
                {
                    int pageSize = 3;
                    allEvents.Clear();
                    pagedEvents = null;
                    bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f", EventTypeEnum.clickedLink, EventContextEnum.OTHER, null, null, null);
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
            }
            #endregion

            #region Example: get customers events filtered  by customer id (required) + mode
            if (false)
            {
                if (currentNode.isValid)
                {
                    List<Event> allEvents = new List<Event>();
                    int pageSize = 3;
                    allEvents.Clear();
                    pagedEvents = null;
                    bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f", null, null, EventModeEnum.ACTIVE, null, null);
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
            }
            #endregion

            #region Example: get customers events filtered  by customer id (required) + date from|to  ( DA TESTARE, SEMBRA NON FUNZIONARE)
            if (false)
            {
                if (currentNode.isValid)
                {
                    List<Event> allEvents = new List<Event>();
                    int pageSize = 3;
                    allEvents.Clear();
                    pagedEvents = null;
                    bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f", null, null, null, Convert.ToDateTime("2016-01-01"), Convert.ToDateTime("2016-12-31"));
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
            }
            #endregion

            #region Example: get event by id
            if (false)
            {
                if (currentNode.isValid)
                {
                    Event ev = currentNode.GetEvent("a47c02d8-c8e0-4d8a-93c0-d35988eaa204");
                }
            }
            #endregion
            #endregion

            #region TAGS
            #region Example: get customer tags
            if (false)
            {
                if (currentNode.isValid)
                {
                    Tags customerTag = currentNode.GetCustomerTags("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f");
                }
            }

            #endregion

            #region Example: add customers tag
            if (false)
            {
                if (currentNode.isValid)
                {
                    Tags currentTags = currentNode.AddCustomerTag("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f", "sport", CustomerTagTypeEnum.Manual);
                    currentTags = currentNode.AddCustomerTag("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f", "music", CustomerTagTypeEnum.Manual);
                }
            }
            #endregion

            #region Example: remove customers tag (DA VERIFICA SEMBRA NON FUNZIONARE) CONFERMATO BUG VA SU il 3/10
            if (false)
            {
                if (currentNode.isValid)
                {
                    Tags currentTags = currentNode.RemoveCustomerTag("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f", "sport", CustomerTagTypeEnum.Manual);
                }
            }
            #endregion
            #endregion

        }
    }
}

