using ContactHubSdkLibrary.Models;
using ContactHubSdkLibrary.SDKclasses;
using KellermanSoftware.CompareNetObjects;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ContactHubSdkLibrary.Test
{
    [TestFixture]
    class CustomerTest
    {
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
                externalId =DateTime.Now.Ticks.ToString(),
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
                Customer newCustomer = node.AddCustomer(newPostCustomer, false);
                //wait for elastic update
                Thread.Sleep(1000);
                if (newCustomer != null && newCustomer.id != null)
                {
                    //customer is created!
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id);
                    Customer myTestCustomer2 = node.GetCustomerByExternalID(newCustomer.externalId);
                    //compare results
                    CompareLogic compareLogic = new CompareLogic();
                    //       bool test1Passed = Util.Compare<Customer>(myTestCustomer1, myTestCustomer2);
                    compareLogic.Config.MembersToIgnore.Add("_registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("_updatedAt");//TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("updatedAt");//TO BE DONE: remote IT

                    bool testPassed1 = compareLogic.Compare(myTestCustomer1, myTestCustomer2).AreEqual;
                    //compare results with posted Customer
                    PostCustomer myPostTestCustomer1 = (PostCustomer)myTestCustomer1;
//                    bool test2Passed = Util.Compare<PostCustomer>(newCustomer, myPostTestCustomer1);
                    bool testPassed2 = compareLogic.Compare(newCustomer, myPostTestCustomer1).AreEqual;
                    //delete added customer
                    bool testPassed3 = node.DeleteCustomer(newCustomer.id);
                    testPassed = testPassed1 & testPassed2 & testPassed3;
                }
            }
            Common.WriteLog("End CustomerAddCustomer test", "passed:" + testPassed + "\n\n");
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
                externalId = Guid.NewGuid().ToString(),
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
                Customer newCustomer = node.AddCustomer(newPostCustomer);

                Thread.Sleep(1000); //waiting for elastic update

                if (newCustomer != null && newCustomer.id != null)
                {
                    //create clone on PostCustomer subclass
                    PostCustomer customer = newCustomer.ToPostCustomer();

                    //customer is created, then update any fields
                    customer.extra = "update data " + DateTime.Now.ToShortTimeString();
                    Customer updatedCustomer = node.UpdateCustomer(customer, newCustomer.id, true);
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id);
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
                    bool test3Passed = node.DeleteCustomer(newCustomer.id);
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
                externalId = Guid.NewGuid().ToString(),
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
                Customer newCustomer = node.AddCustomer(newPostCustomer);

                Thread.Sleep(1000); //waiting for elastic update

                if (newCustomer != null && newCustomer.id != null)
                {
                    PostCustomer customer = new PostCustomer();

                    //customer is created, then update any fields
                    customer.extra = "test data";
                    Customer updatedCustomer = node.UpdateCustomer(customer, newCustomer.id, false);
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id);
                    //compare updated customer
                    bool test1Passed = myTestCustomer1.extra == "test data";
                    //compare updated customer
                    CompareLogic compareLogic = new CompareLogic();
                    compareLogic.Config.MembersToIgnore.Add("_registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("_updatedAt");//TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("registeredAt"); //TO BE DONE: remote IT
                    compareLogic.Config.MembersToIgnore.Add("updatedAt");//TO BE DONE: remote IT

                    bool test2Passed =! compareLogic.Compare(newCustomer, updatedCustomer).AreEqual;

                    compareLogic.Config.MembersToIgnore.Add("extra");

                    bool test4Passed = compareLogic.Compare(newCustomer, updatedCustomer).AreEqual;

                    //delete added customer
                    bool test3Passed = node.DeleteCustomer(newCustomer.id);
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
                Customer newCustomer = node.AddCustomer(newPostCustomer);
                Thread.Sleep(1000); //wait remote update
                //create clone on PostCustomer subclass
                PostCustomer customer = newCustomer.ToPostCustomer();

                if (newCustomer != null && newCustomer.id != null)
                {
                    //customer is created, then update id
                    customer.extra = "CAMPO AGGIORNATO IN PUT " + DateTime.Now.ToShortTimeString();
                    Customer updatedCustomer = node.AddCustomer(customer, true);
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id);
                    //compare results
                    CompareLogic compareLogic = new CompareLogic();
                 //   bool test1Passed = Util.Compare<Customer>(myTestCustomer1, updatedCustomer);
                    bool test1Passed = compareLogic.Compare(myTestCustomer1, updatedCustomer).AreEqual;
                    //compare source data
                    //                    bool test2Passed = Util.Compare<Customer>(myTestCustomer1, newCustomer, new List<String>() { "extra" });
                    compareLogic.Config.MembersToIgnore.Add("extra");
                    bool test2Passed = compareLogic.Compare(myTestCustomer1, newCustomer).AreEqual;
                    //delete added customer
                    bool test3Passed = node.DeleteCustomer(newCustomer.id);
                    testPassed = test1Passed && test2Passed;
                }
            }
            Common.WriteLog("End CustomerUpdateCustomerForced test", "passed:" + testPassed + "\n\n");

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
            if (node.GetCustomers(ref pagedCustomers, pageSize, null, null, null))
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
                    Customer newCustomer = node.AddCustomer(newPostCustomer, false);
                    //  Thread.Sleep(5000); //wait for remote update
                    if (newCustomer.id != null)
                    {
                        ids.Add(newCustomer.id);
                    }
                    else //error
                    {
                        testPassed4 = false;
                        Customer xx = node.GetCustomerByExternalID(extID);
                    }
                    totalItem++;
                }
                Thread.Sleep(1000); //wait for remote db update
                int expectedPages = totalItem / pageSize;
                if (totalItem % pageSize != 0) expectedPages++;

                //get all customer with paging 
                bool pageIsValid = node.GetCustomers(ref pagedCustomers, pageSize, null, null, null);
                int totPage = 1;

                bool testPassed3 = false;
                if (pageIsValid)
                {
                    testPassed3 = true;
                    for (int i = 1; i < pagedCustomers.page.totalPages; i++)
                    {
                        testPassed3 = testPassed3 || node.GetCustomers(ref pagedCustomers, PageRefEnum.next);
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
                        Customer c = node.GetCustomerByID(s);
                    }
                }
                //remove new customers
                foreach (string s in ids)
                {
                    testPassed2 = testPassed2 && node.DeleteCustomer(s);
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
                Customer newCustomer = node.AddCustomer(newPostCustomer);
                Thread.Sleep(1000); //wait remote update

                if (newCustomer != null && newCustomer.id != null)
                {
                    Customer myTestCustomer1 = node.GetCustomerByExternalID(extID);
                    //compare source data
                    //                   bool testPassed1 = Util.Compare<Customer>(myTestCustomer1, newCustomer, new List<String>() { "extra" });
                    CompareLogic compareLogic = new CompareLogic();
                    compareLogic.Config.MembersToIgnore.Add("extra");
                    bool testPassed1 = compareLogic.Compare(myTestCustomer1, newCustomer).AreEqual;

                    //delete data
                    bool testPassed2 = node.DeleteCustomer(myTestCustomer1.id);
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
                Customer newCustomer = node.AddCustomer(newPostCustomer);
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
                    node.GetCustomers(ref pagedCustomers, 10, null, querySTR, null);

                    if (pagedCustomers._embedded != null && pagedCustomers._embedded.customers != null)
                    {
                        Customer myTestCustomer1 = pagedCustomers._embedded.customers.First();
                        //compare source data
                        //  bool testPassed1 = Util.Compare<Customer>(myTestCustomer1, newCustomer, new List<String>() { "extra" });
                        CompareLogic compareLogic = new CompareLogic();
                        compareLogic.Config.MembersToIgnore.Add("extra");
                        bool testPassed1 = compareLogic.Compare(myTestCustomer1, newCustomer).AreEqual;

                        //delete data
                        bool testPassed2 = node.DeleteCustomer(myTestCustomer1.id);
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
                Customer newCustomer = node.AddCustomer(newPostCustomer);
                Thread.Sleep(1000); //wait remote update

                if (newCustomer != null && newCustomer.id != null)
                {
                    PagedCustomer pagedCustomers = null;

                    QueryBuilder qb = new QueryBuilder();
                    qb.AddQuery(new QueryBuilderItem() { attributeName = "base.firstName", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = "Donald" });
                    qb.AddQuery(new QueryBuilderItem() { attributeName = "base.lastName", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = "Duck" });
                    qb.AddQuery(new QueryBuilderItem() { attributeName = "id", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = newCustomer.id });
                    node.GetCustomers(ref pagedCustomers, 10, null, qb.GenerateQuery(QueryBuilderConjunctionEnum.AND), null);

                    if (pagedCustomers._embedded != null && pagedCustomers._embedded.customers != null)
                    {
                        Customer myTestCustomer1 = pagedCustomers._embedded.customers.First();
                        //compare source data
                        //                        bool testPassed1 = Util.Compare<Customer>(myTestCustomer1, newCustomer, new List<String>() { "extra" });
                        CompareLogic compareLogic = new CompareLogic();
                        compareLogic.Config.MembersToIgnore.Add("extra");
                        bool testPassed1 = compareLogic.Compare(myTestCustomer1, newCustomer).AreEqual;

                        //delete data
                        bool testPassed2 = node.DeleteCustomer(myTestCustomer1.id);
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
        /// Test customer liks
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerLikesLifeCycle(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Common.WriteLog("Start CustomerLikes TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

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
                Customer newCustomer = node.AddCustomer(newPostCustomer);
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
                    Likes addLike = node.AddCustomerLike(newCustomer.id, newLike);
                    CompareLogic compareLogic = new CompareLogic();
                    bool testPassed1 = compareLogic.Compare(newLike, addLike).AreEqual;
                    Thread.Sleep(1000); //wait remote update
                    Likes getLike = node.GetCustomerLike(newCustomer.id, likeID);
                    bool testPassed2 = compareLogic.Compare(newLike, getLike).AreEqual;
                    getLike.category = "music";
                    Likes updatedLike = node.UpdateCustomerLike(newCustomer.id, getLike);
                    bool testPassed3 = !compareLogic.Compare(newLike, updatedLike).AreEqual;
                    compareLogic.Config.MembersToIgnore.Add("category"); //ignore category
                    bool testPassed4 = compareLogic.Compare(newLike, updatedLike).AreEqual;
                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4;
                    Thread.Sleep(1000); //wait remote update
                }
                Common.WriteLog("End CustomerLikes test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Const.TIMEEXIT); //wait
            }
        }
    }
}

