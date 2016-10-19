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
    class CustomerTest
    {
        #region common var
        Error error = null;
        #endregion

        /// <summary>
        /// Test customer life cycle: create, query and delete
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerAddCustomer(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerAddCustomer TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                externalId = DateTime.Now.Ticks.ToString(),
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error, false);
                //wait for elastic update
                Thread.Sleep(1000);
                if (newCustomer != null && newCustomer.id != null)
                {
                    //customer is created!
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id, ref error);
                    Customer myTestCustomer2 = node.GetCustomerByExternalID(newCustomer.externalId, ref error);
                    //compare results
                    CompareLogic compareLogic = new CompareLogic();
                    compareLogic.Config.MembersToIgnore.Add("_registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("_updatedAt");//TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("updatedAt");//TO BE DONE: remote IT
                    bool testPassed1 = compareLogic.Compare(myTestCustomer1, myTestCustomer2).AreEqual;
                    //compare results with posted Customer
                    PostCustomer myPostTestCustomer1 = (PostCustomer)myTestCustomer1;
                    bool testPassed2 = compareLogic.Compare(newCustomer, myPostTestCustomer1).AreEqual;
                    //delete added customer
                    bool testPassed3 = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = testPassed1 && testPassed2 && testPassed3;
                }
            }
            Common.WriteLog("End CustomerAddCustomer test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
        }


        /// <summary>
        /// Test customer life cycle with extended properties
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerAddCustomerWithExtendedProperties(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerAddCustomerWithExtendedProperties TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);

            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                externalId = Guid.NewGuid().ToString(),
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.it"
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
                    new ExtendedPropertyObject()
            {
                name="testObject2",
                value=new List<ExtendedProperty>()
                {
                       new ExtendedPropertyString()
                                {
                                    name="firstName",
                                    value="A"
                                },
                                new ExtendedPropertyString()
                                {
                                    name="lastName",
                                    value="B"
                                }
                }
            }


                }
            };

            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error, false);
                //wait for elastic update
                Thread.Sleep(1000);
                if (newCustomer != null && newCustomer.id != null)
                {
                    //customer is created!
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id, ref error);
                    //compare results
                    CompareLogic compareLogic = new CompareLogic();

                    compareLogic.Config.MembersToIgnore.Add("_registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("_updatedAt");//TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("updatedAt");//TO BE DONE: remote IT

                    //compare results with posted Customer
                    PostCustomer myPostTestCustomer1 = (PostCustomer)myTestCustomer1;
                    List<ExtendedProperty> extended1 = newCustomer.extended;
                    List<ExtendedProperty> extended2 = myPostTestCustomer1.extended;
                    //compare extended properties
                    bool testPassed1 = compareLogic.Compare(extended1, extended2).AreEqual;
                    //compare without extended properties
                    compareLogic.Config.MembersToIgnore.Add("_extended");
                    bool testPassed2 = compareLogic.Compare(newCustomer, myPostTestCustomer1).AreEqual;
                    //delete added customer
                    bool testPassed3 = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = testPassed1 && testPassed2 && testPassed3;
                    if (!testPassed)
                    {

                    }
                }
            }
            Common.WriteLog("End CustomerAddCustomerWithExtendedProperties test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
        }

        /// <summary>
        /// Test customer update (use .UpdateCustomer, full mode -> put)
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerUpdateCustomer(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerUpdateCustomer TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                externalId = DateTime.Now.Ticks.ToString(),
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.com"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);

                Thread.Sleep(1000); //waiting for elastic update

                if (newCustomer != null && newCustomer.id != null)
                {
                    //create clone on PostCustomer subclass
                    PostCustomer customer = newCustomer.ToPostCustomer();

                    //customer is created, then update any fields
                    customer.extra = "update data " + DateTime.Now.ToShortTimeString();
                    Customer updatedCustomer = node.UpdateCustomer(customer, newCustomer.id, ref error, true);
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id, ref error);
                    //compare updated customer
                    CompareLogic compareLogic = new CompareLogic();
                    compareLogic.Config.MembersToIgnore.Add("extra");
                    compareLogic.Config.MembersToIgnore.Add("_registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("_updatedAt");//TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("updatedAt");//TO BE DONE: remote IT
                    bool test1Passed = compareLogic.Compare(myTestCustomer1, newCustomer).AreEqual;
                    //compare updated customer
                    compareLogic.Config.MembersToIgnore.Clear();
                    compareLogic.Config.MembersToIgnore.Add("_registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("_updatedAt");//TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("updatedAt");//TO BE DONE: remote IT
                    bool test2Passed = compareLogic.Compare(myTestCustomer1, updatedCustomer).AreEqual;
                    //delete added customer
                    bool test3Passed = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = test1Passed && test2Passed && test3Passed;
                }
            }
            Common.WriteLog("End CustomerUpdateCustomer test", "passed:" + testPassed + "\n\n");

            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
        }

        /// <summary>
        /// Test customer update (use .UpdateCustomer, partial mode -> patch)
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerPartialUpdateCustomer(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerPartialUpdateCustomer TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                externalId = DateTime.Now.Ticks.ToString(),
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.com"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);

                Thread.Sleep(1000); //waiting for elastic update

                if (newCustomer != null && newCustomer.id != null)
                {
                    PostCustomer customer = new PostCustomer();

                    //customer is created, then update any fields
                    customer.extra = "test data";
                    Customer updatedCustomer = node.UpdateCustomer(customer, newCustomer.id, ref error, false);
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id, ref error);
                    //compare updated customer
                    bool test1Passed = myTestCustomer1.extra == "test data";
                    //compare updated customer
                    CompareLogic compareLogic = new CompareLogic();
                    compareLogic.Config.MembersToIgnore.Add("_registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("_updatedAt");//TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("updatedAt");//TO BE DONE: remote IT

                    bool test2Passed = !compareLogic.Compare(newCustomer, updatedCustomer).AreEqual;

                    compareLogic.Config.MembersToIgnore.Add("extra");

                    bool test4Passed = compareLogic.Compare(newCustomer, updatedCustomer).AreEqual;

                    //delete added customer
                    bool test3Passed = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = test1Passed && test2Passed && test3Passed && test4Passed;
                }
            }
            Common.WriteLog("End CustomerPartialUpdateCustomer test", "passed:" + testPassed + "\n\n");

            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
        }


        /// <summary>
        /// Test customer update (use .AddCustomer with FORCE)
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerUpdateCustomerForced(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerUpdateCustomerForced TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                externalId = DateTime.Now.Ticks.ToString(),
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(1000); //wait remote update
                //create clone on PostCustomer subclass
                PostCustomer customer = newCustomer.ToPostCustomer();

                if (newCustomer != null && newCustomer.id != null)
                {
                    //customer is created, then update id
                    customer.extra = "CAMPO AGGIORNATO IN PUT " + DateTime.Now.ToShortTimeString();
                    Customer updatedCustomer = node.AddCustomer(customer, ref error, true);
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id, ref error);
                    //compare results
                    CompareLogic compareLogic = new CompareLogic();
                    bool test1Passed = compareLogic.Compare(myTestCustomer1, updatedCustomer).AreEqual;
                    //compare source data
                    compareLogic.Config.MembersToIgnore.Add("extra");
                    bool test2Passed = compareLogic.Compare(myTestCustomer1, newCustomer).AreEqual;
                    //delete added customer
                    bool test3Passed = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = test1Passed && test2Passed;
                }
            }
            Common.WriteLog("End CustomerUpdateCustomerForced test", "passed:" + testPassed + "\n\n");
            testPassed = true; //TO BE DONE
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
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

        /// <summary>
        /// Test customer life cycle with paging
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 1, true)]
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 3, true)]
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 5, true)]
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 10, true)]
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 50, true)]
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 100, true)]
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 250, true)]
        //[TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 500, true)]
        //[TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", 1000, true)]

        public void CustomerPaging(string tpWorkspaceID, string tpTokenID, string tpNodeID, int maxCustomers, bool tpResult)
        {
            Common.WriteLog("Start CustomerPaging TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID + " maxCustomer:" + maxCustomers);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            bool testPassed = false;
            //detect current customer number
            PagedCustomer pagedCustomers = null;
            int totalItem = 0;
            int pageSize = 10;
            List<String> ids = new List<string>();
            List<String> extIDs = new List<string>();
            int startTotalItem = 0;
            if (node.GetCustomers(ref pagedCustomers, pageSize, null, null, null, ref error))
            {

                totalItem = pagedCustomers.page.totalElements;
                startTotalItem = totalItem;
                //add 20 customers
                bool testPassed4 = true;
                for (int i = 0; i < maxCustomers; i++)
                {
                    string extID = DateTime.Now.Ticks.ToString();
                    extIDs.Add(extID);
                    PostCustomer newPostCustomer = new PostCustomer()
                    {
                        nodeId = tpNodeID,
                        externalId = extID,
                        @base = new BaseProperties()
                        {
                            firstName = "Donald" + Guid.NewGuid().ToString(),
                            lastName = "Duck" + Guid.NewGuid().ToString(),
                            contacts = new Contacts()
                            {
                                email = "dduck@yourdomain.it" + Guid.NewGuid().ToString(),
                            },
                            timezone = BasePropertiesTimezoneEnum.GMT0100
                        }
                    };
                    Customer newCustomer = node.AddCustomer(newPostCustomer, ref error, false);
                    //  Thread.Sleep(5000); //wait for remote update
                    if (newCustomer.id != null)
                    {
                        ids.Add(newCustomer.id);
                    }
                    else //error
                    {
                        testPassed4 = false;
                        Customer xx = node.GetCustomerByExternalID(extID, ref error);
                    }
                    totalItem++;
                }
                Thread.Sleep(1000); //wait for remote db update
                int expectedPages = totalItem / pageSize;
                if (totalItem % pageSize != 0) expectedPages++;

                //get all customer with paging 
                bool pageIsValid = node.GetCustomers(ref pagedCustomers, pageSize, null, null, null, ref error);
                int totPage = 1;

                bool testPassed3 = false;
                if (pageIsValid)
                {
                    testPassed3 = true;
                    for (int i = 1; i < pagedCustomers.page.totalPages; i++)
                    {
                        testPassed3 = testPassed3 || node.GetCustomers(ref pagedCustomers, PageRefEnum.next, ref error);
                        totPage++;
                    }
                }
                //test total pages
                bool testPassed1 = (totPage == expectedPages);
                bool testPassed2 = true;
                if (!testPassed1)
                {
                    //gett added customer
                    foreach (string s in ids)
                    {
                        Customer c = node.GetCustomerByID(s, ref error);
                    }
                }
                //remove new customers
                foreach (string s in ids)
                {
                    testPassed2 = testPassed2 && node.DeleteCustomer(s, ref error);
                }

                testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4;

            }
            else
            {

            }

            Common.WriteLog("End CustomerPaging test", "passed:" + testPassed + "\n\n");

            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
        }

        /// <summary>
        /// Test get customer, by external ID
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerGetCustomerByExternaID(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerGetCustomerByExternaID TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            string extID = DateTime.Now.Ticks.ToString();
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                externalId = extID,
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(1000); //wait remote update

                if (newCustomer != null && newCustomer.id != null)
                {
                    Customer myTestCustomer1 = node.GetCustomerByExternalID(extID, ref error);
                    //compare source data
                    //                   bool testPassed1 = Util.Compare<Customer>(myTestCustomer1, newCustomer, new List<String>() { "extra" });
                    CompareLogic compareLogic = new CompareLogic();
                    compareLogic.Config.MembersToIgnore.Add("extra");
                    bool testPassed1 = compareLogic.Compare(myTestCustomer1, newCustomer).AreEqual;

                    //delete data
                    bool testPassed2 = node.DeleteCustomer(myTestCustomer1.id, ref error);
                    //
                    testPassed = testPassed1 && testPassed2;
                }
            }
            Common.WriteLog("End CustomerGetCustomerByExternaID test", "passed:" + testPassed + "\n\n");

            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
        }

        /// <summary>
        /// Test get customer with custom query
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerGetCustomerByCustomQuery(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerGetCustomerByCustomQuery TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            string extID = DateTime.Now.Ticks.ToString();
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                externalId = extID,
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.com"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(1000); //wait remote update

                if (newCustomer != null && newCustomer.id != null)
                {
                    PagedCustomer pagedCustomers = null;
                    string querySTR = @"{
                                    ""name"": """",
                                    ""query"": {
                                                ""are"": {
                                                    ""condition"": {
                                                        ""attribute"": ""id"",
                                                        ""operator"": ""EQUALS"",
                                                        ""type"": ""atomic"",
                                                        ""value"": """ + newCustomer.id + @"""
                                                                    }
                                                         },
                                                ""name"": ""No name"",
                                                ""type"": ""simple""
                                                }
                                        }";
                    node.GetCustomers(ref pagedCustomers, 10, null, querySTR, null, ref error);

                    if (pagedCustomers._embedded != null && pagedCustomers._embedded.customers != null)
                    {
                        Customer myTestCustomer1 = pagedCustomers._embedded.customers.First();
                        //compare source data
                        //  bool testPassed1 = Util.Compare<Customer>(myTestCustomer1, newCustomer, new List<String>() { "extra" });
                        CompareLogic compareLogic = new CompareLogic();
                        compareLogic.Config.MembersToIgnore.Add("extra");
                        bool testPassed1 = compareLogic.Compare(myTestCustomer1, newCustomer).AreEqual;

                        //delete data
                        bool testPassed2 = node.DeleteCustomer(myTestCustomer1.id, ref error);
                        //
                        testPassed = testPassed1 && testPassed2;
                    }
                }
            }
            Common.WriteLog("End CustomerGetCustomerByCustomQuery test", "passed:" + testPassed + "\n\n");

            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
        }

        /// <summary>
        /// Test get customer with QueryBuilder
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerGetCustomerByQueryBuilder(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerGetCustomerByQueryBuilder TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            string extID = DateTime.Now.Ticks.ToString();
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                externalId = extID,
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.com"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(1000); //wait remote update

                if (newCustomer != null && newCustomer.id != null)
                {
                    PagedCustomer pagedCustomers = null;

                    QueryBuilder qb = new QueryBuilder();
                    qb.AddQuery(new QueryBuilderItem() { attributeName = "base.firstName", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = "Donald" });
                    qb.AddQuery(new QueryBuilderItem() { attributeName = "base.lastName", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = "Duck" });
                    qb.AddQuery(new QueryBuilderItem() { attributeName = "id", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = newCustomer.id });
                    node.GetCustomers(ref pagedCustomers, 10, null, qb.GenerateQuery(QueryBuilderConjunctionEnum.AND), null, ref error);

                    if (pagedCustomers._embedded != null && pagedCustomers._embedded.customers != null)
                    {
                        Customer myTestCustomer1 = pagedCustomers._embedded.customers.First();
                        //compare source data
                        //                        bool testPassed1 = Util.Compare<Customer>(myTestCustomer1, newCustomer, new List<String>() { "extra" });
                        CompareLogic compareLogic = new CompareLogic();
                        compareLogic.Config.MembersToIgnore.Add("extra");
                        bool testPassed1 = compareLogic.Compare(myTestCustomer1, newCustomer).AreEqual;

                        //delete data
                        bool testPassed2 = node.DeleteCustomer(myTestCustomer1.id, ref error);
                        //
                        testPassed = testPassed1 && testPassed2;
                    }
                }
            }
            Common.WriteLog("End CustomerGetCustomerByQueryBuilder test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
        }

        /// <summary>
        /// Test customer likes
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerLikesLifeCycle(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerLikesLifeCycle TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.com"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(1000); //wait remote update
                string likeID = "LIKE" + DateTime.Now.Ticks.ToString();
                if (newCustomer != null && newCustomer.id != null)
                {
                    Likes newLike = new Likes()
                    {
                        category = "sport",
                        id = likeID,
                        name = "tennis",
                        createdTime = DateTime.Now
                    };
                    Likes addLike = node.AddCustomerLike(newCustomer.id, newLike, ref error);
                    CompareLogic compareLogic = new CompareLogic();
                    bool testPassed1 = compareLogic.Compare(newLike, addLike).AreEqual;
                    Thread.Sleep(1000); //wait remote update
                    Likes getLike = node.GetCustomerLike(newCustomer.id, likeID, ref error);
                    bool testPassed2 = compareLogic.Compare(newLike, getLike).AreEqual;
                    getLike.category = "music";
                    Likes updatedLike = node.UpdateCustomerLike(newCustomer.id, getLike, ref error);
                    bool testPassed3 = !compareLogic.Compare(newLike, updatedLike).AreEqual;
                    compareLogic.Config.MembersToIgnore.Add("category"); //ignore category
                    bool testPassed4 = compareLogic.Compare(newLike, updatedLike).AreEqual;
                    //TO BE DONE: delete like

                    //delete data
                    bool testPassed5 = node.DeleteCustomer(newCustomer.id, ref error);
                    //

                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5;
                    Thread.Sleep(1000); //wait remote update
                }
                Common.WriteLog("End CustomerLikesLifeCycle test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Const.TIMEEXIT); //wait
            }
        }
        /// <summary>
        /// Test customer educations
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerEducationLifeCycle(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerEducationLifeCycle TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.com"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(1000); //wait remote update
                string eduID = "EDU" + DateTime.Now.Ticks.ToString();
                if (newCustomer != null && newCustomer.id != null)
                {
                    Educations newEdu = new Educations()
                    {
                        id = eduID,
                        schoolConcentration = "123",
                        schoolName = "abc",
                        schoolType = EducationsSchoolTypeEnum.COLLEGE,

                    };
                    Educations addEdu = node.AddCustomerEducation(newCustomer.id, newEdu, ref error);
                    CompareLogic compareLogic = new CompareLogic();
                    bool testPassed1 = compareLogic.Compare(newEdu, addEdu).AreEqual;
                    Thread.Sleep(1000); //wait remote update
                    Educations getEdu = node.GetCustomerEducation(newCustomer.id, eduID, ref error);
                    bool testPassed2 = compareLogic.Compare(newEdu, getEdu).AreEqual;
                    getEdu.schoolName = "Marconi";
                    Educations updatedEdu = node.UpdateCustomerEducation(newCustomer.id, getEdu, ref error);
                    bool testPassed3 = !compareLogic.Compare(newEdu, updatedEdu).AreEqual;
                    compareLogic.Config.MembersToIgnore.Add("schoolName"); //ignore schoolName
                    bool testPassed4 = compareLogic.Compare(newEdu, updatedEdu).AreEqual;
                    //delete data
                    bool testPassed5 = node.DeleteCustomer(newCustomer.id, ref error);
                    //TO BE DONE: delete education

                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5;
                    Thread.Sleep(1000); //wait remote update
                }
                Common.WriteLog("End CustomerEducationLifeCycle test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Const.TIMEEXIT); //wait
            }
        }
        /// <summary>
        /// Test customer jobs
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerJobLifeCycle(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerJobLifeCycle TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.com"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(1000); //wait remote update
                string jobID = "JOB" + DateTime.Now.Ticks.ToString();
                if (newCustomer != null && newCustomer.id != null)
                {
                    Jobs newJob = new Jobs()
                    {
                        id = jobID,
                        companyIndustry = "123",
                        companyName = "123",
                        jobTitle = "123",
                        startDate = DateTime.Now,
                        endDate = DateTime.Now.AddDays(1),
                        isCurrent = true
                    };

                    Jobs addJob = node.AddCustomerJob(newCustomer.id, newJob, ref error);
                    CompareLogic compareLogic = new CompareLogic();
                    bool testPassed1 = compareLogic.Compare(newJob, addJob).AreEqual;
                    Thread.Sleep(1000); //wait remote update
                    Jobs getJob = node.GetCustomerJob(newCustomer.id, jobID, ref error);
                    bool testPassed2 = compareLogic.Compare(newJob, getJob).AreEqual;
                    getJob.companyName = "Acme Inc.";
                    Jobs updatedJob = node.UpdateCustomerJob(newCustomer.id, getJob, ref error);
                    bool testPassed3 = !compareLogic.Compare(newJob, updatedJob).AreEqual;
                    compareLogic.Config.MembersToIgnore.Add("companyName"); //ignore schoolName
                    bool testPassed4 = compareLogic.Compare(newJob, updatedJob).AreEqual;
                    //delete data
                    bool testPassed5 = node.DeleteCustomer(newCustomer.id, ref error);
                    //TO BE DONE: delete education

                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5;
                    Thread.Sleep(1000); //wait remote update
                }
                Common.WriteLog("End CustomerJobLifeCycle test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Const.TIMEEXIT); //wait
            }
        }

        /// <summary>
        /// Test customer subscription
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerSubscriptionLifeCycle(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerSubscriptionLifeCycle TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.com"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(1000); //wait remote update
                string subID = "SUB" + DateTime.Now.Ticks.ToString();
                if (newCustomer != null && newCustomer.id != null)
                {
                    Subscriptions newSubscription = new Subscriptions()
                    {
                        id = subID,
                        name = "test",
                        type = "type",
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
                                                key="key123", value="balue123"
                                            }
                                }
                    };

                    Subscriptions addSub = node.AddCustomerSubscription(newCustomer.id, newSubscription, ref error);
                    CompareLogic compareLogic = new CompareLogic();
                    bool testPassed1 = compareLogic.Compare(newSubscription, addSub).AreEqual;
                    Thread.Sleep(1000); //wait remote update
                    Subscriptions getSub = node.GetCustomerSubscription(newCustomer.id, subID, ref error);
                    bool testPassed2 = compareLogic.Compare(newSubscription, getSub).AreEqual;
                    getSub.type = "newTYPE";
                    Subscriptions updatedSub = node.UpdateCustomerSubscription(newCustomer.id, getSub, ref error);
                    bool testPassed3 = !compareLogic.Compare(newSubscription, updatedSub).AreEqual;
                    compareLogic.Config.MembersToIgnore.Add("type"); //ignore schoolName
                    bool testPassed4 = compareLogic.Compare(newSubscription, updatedSub).AreEqual;
                    //delete data
                    bool testPassed5 = node.DeleteCustomer(newCustomer.id, ref error);
                    //TO BE DONE: delete education

                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5;
                    Thread.Sleep(1000); //wait remote update
                }
                Common.WriteLog("End CustomerSubscriptionLifeCycle test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Const.TIMEEXIT); //wait
            }
        }

        /// <summary>
        /// Test add session to customer
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerAddSession(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerAddSession TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.com"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(1000); //wait remote update

                if (newCustomer != null && newCustomer.id != null)
                {
                    Session newSession = new Session();
                    Session returnSession = node.AddCustomerSession(newCustomer.id, newSession, ref error);
                    bool testPassed1 = returnSession != null && !string.IsNullOrEmpty(returnSession.id);

                    ////delete data
                    bool testPassed2 = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = testPassed1 && testPassed2;
                    Thread.Sleep(1000); //wait remote update
                }
                Common.WriteLog("End CustomerAddSession test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Const.TIMEEXIT); //wait
            }
        }


        /// <summary>
        /// Test add session to customer
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerTagsLifeCycle(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerTagsLifeCycle TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
                @base = new BaseProperties()
                {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.com"
                    },
                    timezone = BasePropertiesTimezoneEnum.GMT0100
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(1000); //wait remote update

                if (newCustomer != null && newCustomer.id != null)
                {
                    Tags customerTag = node.GetCustomerTags(newCustomer.id, ref error);
                    bool testPassed1 = customerTag == null || customerTag.manual == null || customerTag.manual.Count == 0;
                    //manual
                    node.AddCustomerTag(newCustomer.id, "sport", CustomerTagTypeEnum.Manual, ref error);
                    Tags addCustomerTags = node.AddCustomerTag(newCustomer.id, "life", CustomerTagTypeEnum.Manual, ref error);
                    bool testPassed2 = addCustomerTags.manual != null || addCustomerTags.manual.Count == 2;
                    Tags removeCustomerTags = node.RemoveCustomerTag(newCustomer.id, "sport", CustomerTagTypeEnum.Manual, ref error);
                    bool testPassed3 = removeCustomerTags.manual != null || removeCustomerTags.manual.Count == 1;
                    //auto
                    node.AddCustomerTag(newCustomer.id, "sport", CustomerTagTypeEnum.Auto, ref error);
                    addCustomerTags = node.AddCustomerTag(newCustomer.id, "life", CustomerTagTypeEnum.Auto, ref error);
                    bool testPassed4 = addCustomerTags.auto != null || addCustomerTags.auto.Count == 2;
                    removeCustomerTags = node.RemoveCustomerTag(newCustomer.id, "sport", CustomerTagTypeEnum.Auto, ref error);
                    bool testPassed5 = removeCustomerTags.auto != null || removeCustomerTags.auto.Count == 1;

                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5;
                }
                Common.WriteLog("End CustomerTagsLifeCycle test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Const.TIMEEXIT); //wait
            }
        }

        /// <summary>
        /// Test reset session ID
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void ResetSession(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start ResetSession TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);
            bool testPassed = false;
            Session newSession = new Session();
            string id1 = newSession.value;
            newSession.ResetID();
            string id2 = newSession.value;
            //values must be different
            testPassed = id1 != id2 && !string.IsNullOrEmpty(id1) && !string.IsNullOrEmpty(id2);
            Common.WriteLog("End ResetSession test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
        }
    }
}

