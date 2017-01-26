using ContactHubSdkLibrary.Events;
using ContactHubSdkLibrary.Models;
using ContactHubSdkLibrary.SDKclasses;
using KellermanSoftware.CompareNetObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ContactHubSdkLibrary.Test
{
    [TestFixture]
    class EventsTest
    {
        #region common var
        Error error = null;
        #endregion

        /// <summary>
        /// Test add event to customer
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void E_AddEvent(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerAddEvent TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = createTestPostCustomer(tpNodeID);
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error, false);
                //wait for elastic update
                Thread.Sleep(Util.GetWaitTime());
                bool testPassed1 = false;
                if (newCustomer != null && newCustomer.id != null)
                {
                    PostEvent newEvent = new PostEvent()
                    {
                        customerId = newCustomer.id,
                        type = EventTypeEnum.clickedLink,
                        context = EventContextEnum.OTHER,
                        properties = new EventPropertyClickedLink()
                        {
                            title = "hello",
                            path = "/tests/1"
                        },
                        contextInfo = new EventContextPropertyOTHER
                        {
                            client = new Client()
                            {
                                ip = "8.8.8.8",
                                userAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; nl-NL; rv:1.7.5) Gecko/20041202 Firefox/1.0"
                            }
                        },
                        date = DateTime.Now
                    };

                    string result = node.AddEvent(newEvent, ref error);
                    if (result == "202")
                    {
                        testPassed1 = true;
                    }

                    //verify if customer has added event
                    PagedEvent events = null;
                    bool tmp = false;
                    //polling on insert
                    if (testPassed1)
                    {
                        while (events == null || events.elements.Count == 0)
                        {
                            events = null;
                            tmp = node.GetEvents(ref events, 1, newCustomer.id, null, null, null, null, null, ref error);
                            Thread.Sleep(1000);
                        }
                    }
                    bool testPassed2 = false;
                    //if (events._embedded!=null && events._embedded.events.Count==1)
                    if (events.elements != null && events.elements.Count == 1)
                    {
                        Event firstEvent = events.elements.First();//events._embedded.events.First();
                        PostEvent firstPostEvent = firstEvent.ToPostEvent();
                        CompareLogic compareLogic = new CompareLogic();
                        ComparisonResult compare = compareLogic.Compare(firstPostEvent, newEvent);
                        testPassed2 = compare.AreEqual;
                    }
                    bool testPassed3 = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = testPassed1 && testPassed2 && testPassed3;
                }
            }
            Common.WriteLog("End CustomerAddEvent test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Util.GetExitTime()); //wait
        }

        /// <summary>
        /// Test add complex event to customer
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void E_AddEventWithComplexProperties(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerAddEventWithComplexProperties TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = createTestPostCustomer(tpNodeID);
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error, false);
                //wait for elastic update
                Thread.Sleep(Util.GetWaitTime());
                bool testPassed1 = false;
                if (newCustomer != null && newCustomer.id != null)
                {
                    EventPropertyRepliedTicket typeProperties = new EventPropertyRepliedTicket()
                    {
                        category = new List<String>() { "1" },
                        ticketId = "1",
                        subject = "web form question " + DateTime.Now.ToString(),
                        text = "lorem ipsum"
                    };

                    EventContextPropertyWEB contextProperties = new EventContextPropertyWEB()
                    {
                        client = new Client()
                        {
                            ip = "192.168.1.1",
                            userAgent = "Mozilla"
                        }
                    };

                    PostEvent newEvent = new PostEvent()
                    {
                        customerId = newCustomer.id,
                        type = EventTypeEnum.repliedTicket,
                        context = EventContextEnum.WEB,
                        properties = typeProperties,
                        contextInfo = contextProperties,
                        date = DateTime.Now
                    };

                    string result = node.AddEvent(newEvent, ref error);
                    if (result == "202")
                    {
                        testPassed1 = true;
                    }

                    //get events
                    PagedEvent pagedEvents = null;

                    //polling on insert
                    bool pageIsValid = false;
                    if (testPassed1)
                    {
                        while (pagedEvents == null || pagedEvents.elements.Count == 0)
                        {
                            pagedEvents = null;
                            pageIsValid = node.GetEvents(ref pagedEvents, 10, newCustomer.id, null, null, null, null, null, ref error);
                            Thread.Sleep(1000);
                        }
                    }
                    bool testPassed2 = (pagedEvents != null && pagedEvents.elements != null && pagedEvents.elements.Count == 1);
                    Event addedEvent = pagedEvents.elements.First();
                    PostEvent addedPostEvent = addedEvent.ToPostEvent();  //scale to subclass
                    CompareLogic compareLogic = new CompareLogic();
                    ComparisonResult compare = compareLogic.Compare(addedPostEvent, newEvent);
                    bool testPassed3 = compare.AreEqual;
                    bool testPassed4 = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4;
                }
            }
            Common.WriteLog("End CustomerAddEventWithComplexProperties test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Util.GetExitTime()); //wait
        }

        /// <summary>
        /// Test add event + externalID reconciliation
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void E_AddEventWithExtIdReconciliation(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerAddEventWithExtIdReconciliation TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            bool testPassed = false;
            if (node != null)
            {
                bool testPassed1 = false;
                string extID = DateTime.Now.Ticks.ToString();
                {
                    PostEvent newEvent = new PostEvent()
                    {
                        bringBackProperties = new BringBackProperty()
                        {
                            nodeId = node.id,
                            type = BringBackPropertyTypeEnum.EXTERNAL_ID,
                            value = extID
                        },
                        type = EventTypeEnum.loggedIn,
                        context = EventContextEnum.WEB,
                        properties = new EventBaseProperty()
                    };

                    string result = node.AddEvent(newEvent, ref error);
                    if (result == "202")
                    {
                        testPassed1 = true;
                    }
                    // creation of the event also triggers the user creation with only externalID
                    Customer extIdCustomer = null;
                    int maxCount = 600;
                    while (extIdCustomer == null && maxCount > 0)
                    {
                        try
                        {
                            extIdCustomer = node.GetCustomerByExternalID(extID, ref error).FirstOrDefault();
                        }
                        catch (Exception ex)
                        {
                        }

                        Thread.Sleep(1000);  //wait remote processing
                        maxCount--;
                    }

                    bool testPassed2 = extIdCustomer != null && !string.IsNullOrEmpty(extIdCustomer.id);

                    string customerID = extIdCustomer.id;
                    //update void customer created from event
                    PostCustomer postCustomer = createTestPostCustomer(tpNodeID);
                    postCustomer.nodeId = null; //don't update nodeId (it's readonly)
                    postCustomer.externalId = extID;
                    Customer updatedCustomer = node.UpdateCustomer(postCustomer, customerID, ref error, false);
                    bool testPassed3 = updatedCustomer != null && !string.IsNullOrEmpty(updatedCustomer.id);
                    //wait queue elaboration
                    Thread.Sleep(Util.GetWaitTime());
                    customerID = updatedCustomer.id;
                    //test reconciliation: get events
                    PagedEvent pagedEvents = null;
                    bool pageIsValid = node.GetEvents(ref pagedEvents, 10, customerID, null, null, null, null, null, ref error);
                    //bool testPassed4 = (pagedEvents != null && pagedEvents._embedded != null && pagedEvents._embedded.events != null && pagedEvents._embedded.events.Count == 1);
                    bool testPassed4 = (pagedEvents != null && pagedEvents.elements != null && pagedEvents.elements.Count == 1);
                    //delete customer
                    bool testPassed5 = node.DeleteCustomer(updatedCustomer.id, ref error);
                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5;
                }
            }
            Common.WriteLog("End CustomerAddEventWithExtIdReconciliation test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Util.GetExitTime()); //wait
        }

        /// <summary>
        /// Test add event + session ID reconciliation
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void E_AddEventWithSessionIdReconciliation(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerAddEventWithSessionIdReconciliation TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = createTestPostCustomer(tpNodeID);
            bool testPassed = false;
            if (node != null)
            {
                bool testPassed1 = false;
                string extID = DateTime.Now.Ticks.ToString();
                {
                    //create new session
                    Session currentSession = new Session();

                    PostEvent newEvent = new PostEvent()
                    {
                        bringBackProperties = new BringBackProperty()
                        {
                            nodeId = node.id,
                            type = BringBackPropertyTypeEnum.SESSION_ID,
                            value = currentSession.value
                        },
                        type = EventTypeEnum.loggedIn,
                        context = EventContextEnum.WEB,
                        properties = new EventBaseProperty()
                    };

                    string result = node.AddEvent(newEvent, ref error);
                    if (result == "202")
                    {
                        testPassed1 = true;
                    }
                    Thread.Sleep(Util.GetWaitTime());
                    Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                    bool testPassed2 = (newCustomer != null && !string.IsNullOrEmpty(newCustomer.id));
                    Thread.Sleep(Util.GetWaitTime());
                    Session returnSession = node.AddCustomerSession(newCustomer.id, currentSession, ref error);
                    bool testPassed3 = (returnSession != null);
                    Thread.Sleep(Util.GetWaitTime());
                    //test reconciliation: get events
                    PagedEvent pagedEvents = null;
                    bool pageIsValid = node.GetEvents(ref pagedEvents, 10, newCustomer.id,
                        null, null, null, null, null,
                        ref error);
                    bool testPassed4 = (pagedEvents != null && pagedEvents.elements != null && pagedEvents.elements.Count == 1);
                    //delete customer
                    bool testPassed5 = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5;
                }
            }
            Common.WriteLog("End CustomerAddEventWithSessionIdReconciliation test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Util.GetExitTime()); //wait
        }

        /// <summary>
        /// Test add event to customer
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 1, true)]
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 3, true)]
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 5, true)]
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 10, true)]
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 50, true)]
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 100, true)]
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 250, true)]
        public void E_EventPaging(string tpWorkspaceID, string tpTokenID, string tpNodeID, int maxEvents, bool tpResult)
        {
            Common.WriteLog("Start E_EventPaging TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = createTestPostCustomer(tpNodeID);
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error, false);
                //wait for elastic update
                Thread.Sleep(Util.GetWaitTime());
                bool testPassed1 = true;
                int pageSize = 5;
                if (newCustomer != null && newCustomer.id != null)
                {
                    int totalItem = 0;
                    for (int i = 0; i < maxEvents; i++)
                    {
                        PostEvent newEvent = new PostEvent()
                        {
                            customerId = newCustomer.id,
                            type = EventTypeEnum.clickedLink,
                            context = EventContextEnum.OTHER,
                            properties = new EventBaseProperty(),
                            date = DateTime.Now
                        };
                        string result = node.AddEvent(newEvent, ref error);
                        if (result == "202")
                        {
                            testPassed1 = testPassed1 && true;
                            totalItem++;
                        }
                    }
                    Thread.Sleep(Util.GetWaitTime());
                    //get pages
                    int expectedPages = totalItem / pageSize;
                    if (totalItem % pageSize != 0) expectedPages++;
                    //get all customer with paging 
                    PagedEvent pagedEvents = null;
                    bool pageIsValid = node.GetEvents(ref pagedEvents, pageSize, newCustomer.id, null, null, null, null, null, ref error);
                    bool testPassed2 = expectedPages == pagedEvents.page.totalPages && totalItem == pagedEvents.page.totalElements;
                    bool testPassed3 = false;
                    int totPage = 1;
                    if (pageIsValid)
                    {
                        testPassed3 = true;
                        for (int i = 1; i < pagedEvents.page.totalPages; i++)
                        {
                            testPassed3 = testPassed3 && node.GetEvents(ref pagedEvents, PageRefEnum.next, ref error);
                            totPage++;
                        }
                    }
                    bool testPassed4 = totPage == pagedEvents.page.totalPages;
                    //reverse paging
                    pageIsValid = node.GetEvents(ref pagedEvents, pageSize, newCustomer.id, null, null, null, null, null, ref error);
                    pageIsValid = node.GetEvents(ref pagedEvents, PageRefEnum.last, ref error); //get last page
                    bool testPassed9 = false;
                    if (pageIsValid)
                    {
                        totPage = pagedEvents.page.totalPages;
                        testPassed9 = true;
                        for (int i = pagedEvents.page.totalPages - 1; i > 0; i--)
                        {
                            testPassed9 = testPassed9 && node.GetEvents(ref pagedEvents, PageRefEnum.previous, ref error);
                            totPage--;
                        }
                    }
                    bool testPassed10 = totPage == 1;
                    //test filter
                    pagedEvents = null;
                    pageIsValid = node.GetEvents(ref pagedEvents, pageSize, newCustomer.id,
                        EventTypeEnum.clickedLink, EventContextEnum.OTHER,
                        EventModeEnum.ACTIVE, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1),
                        ref error);
                    bool testPassed6 = expectedPages == pagedEvents.page.totalPages && totalItem == pagedEvents.page.totalElements;
                    bool testPassed7 = false;
                    totPage = 1;
                    if (pageIsValid)
                    {
                        testPassed7 = true;
                        for (int i = 1; i < pagedEvents.page.totalPages; i++)
                        {
                            testPassed7 = testPassed7 && node.GetEvents(ref pagedEvents, PageRefEnum.next, ref error);
                            totPage++;
                        }
                    }
                    bool testPassed8 = totPage == pagedEvents.page.totalPages;
                    //delete customer
                    bool testPassed5 = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5 && testPassed6 && testPassed7 && testPassed8 && testPassed9 && testPassed10;
                }
            }
            Common.WriteLog("End E_EventPaging test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Util.GetExitTime()); //wait
        }

        /// <summary>
        /// Get test node
        /// </summary>
        private Node GetTestNode(string workspaceID, string tokenID, string nodeID)
        {
            Workspace currentWorkspace = new Workspace(workspaceID, tokenID);
            if (currentWorkspace == null || !currentWorkspace.isValid) return null;
            string currentNodeID = nodeID;
            Node currentNode = currentWorkspace.GetNode(currentNodeID);
            if (currentNode == null) return null;
            return currentNode;
        }

        private PostCustomer createTestPostCustomer(string nodeID)
        {
            return new PostCustomer()
            {
                nodeId = nodeID,
                externalId = DateTime.Now.Ticks.ToString(),
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
        }
    }


}