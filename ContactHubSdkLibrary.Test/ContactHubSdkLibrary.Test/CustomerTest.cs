using ContactHubSdkLibrary.Models;
using ContactHubSdkLibrary.SDKclasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
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
                    bool test1Passed = Util.Compare<Customer>(myTestCustomer1, myTestCustomer2);

                    //compare results with posted Customer
                    PostCustomer myPostTestCustomer1 = (PostCustomer)myTestCustomer1;
                    bool test2Passed = Util.Compare<PostCustomer>(newCustomer, myPostTestCustomer1);

                    //delete added customer
                    bool test3Passed = node.DeleteCustomer(newCustomer.id);
                    testPassed = test1Passed & test2Passed & test3Passed;
                }
            }

            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
        }
        /// <summary>
        /// Test customer update (use .UpdateCustomer)
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerUpdateCustomer(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
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
                    PostCustomer customer = newCustomer.CreateObject<PostCustomer>();

                    //customer is created, then update any fields
                    customer.extra = "CAMPO AGGIORNATO IN PUT " + DateTime.Now.ToShortTimeString();
                    Customer updatedCustomer = node.UpdateCustomer(customer, newCustomer.id, true);
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id);
                    //compare updated customer
                    bool test1Passed = Util.Compare<Customer>(myTestCustomer1, newCustomer, new List<String>() { "extra" });
                    //compare updated customer
                    bool test2Passed = Util.Compare<Customer>(myTestCustomer1, updatedCustomer);
                    //delete added customer
                    bool test3Passed = node.DeleteCustomer(newCustomer.id);
                    testPassed = test1Passed && test2Passed && test3Passed;
                }
            }

            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
        }

        /// <summary>
        /// Test customer update (use .AddCustomer with FORCE)
        /// </summary>
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
        public void CustomerUpdateCustomerForced(string tpWorkspaceID, string tpTokenID, string tpNodeID, bool tpResult)
        {
            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            PostCustomer newPostCustomer = new PostCustomer()
            {
                nodeId = tpNodeID,
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
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer);
                Thread.Sleep(1000); //wait remote update
                //create clone on PostCustomer subclass
                PostCustomer customer = newCustomer.CreateObject<PostCustomer>();

                if (newCustomer != null && newCustomer.id != null)
                {
                    //customer is created, then update id
                    customer.extra = "CAMPO AGGIORNATO IN PUT " + DateTime.Now.ToShortTimeString();
                    Customer updatedCustomer = node.AddCustomer(customer, true);
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id);
                    //compare results
                    bool test1Passed = Util.Compare<Customer>(myTestCustomer1, updatedCustomer);
                    //compare source data
                    bool test2Passed = Util.Compare<Customer>(myTestCustomer1, newCustomer, new List<String>() { "extra" });
                    //delete added customer
                    bool test3Passed = node.DeleteCustomer(newCustomer.id);
                    testPassed = test1Passed && test2Passed;
                }
            }

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
                            firstName = "Diego" + Guid.NewGuid().ToString(),
                            lastName = "Feltrin" + Guid.NewGuid().ToString(),
                            contacts = new Contacts()
                            {
                                email = "diego@dimension.it" + Guid.NewGuid().ToString(),
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
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Const.TIMEEXIT); //wait
        }

    }
}
