using ContactHubSdkLibrary.Models;
using ContactHubSdkLibrary.SDKclasses;
using NUnit.Framework;
using System;

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
                Customer newCustomer = AddTestCustomer(node, newPostCustomer);
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
                Customer newCustomer = AddTestCustomer(node, newPostCustomer);
                if (newCustomer != null && newCustomer.id != null)
                {
                    //customer is created, then update id

                    newCustomer.extra = "CAMPO AGGIORNATO IN PUT " + DateTime.Now.ToShortTimeString();
                    Customer updatedCustomer = node.UpdateCustomer((PostCustomer)newCustomer, newCustomer.id, true);

                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id);
                    //compare results
                    bool test1Passed = Util.Compare<Customer>(myTestCustomer1, updatedCustomer);

                    //delete added customer
                    bool test2Passed = node.DeleteCustomer(newCustomer.id);
                    testPassed = test1Passed && test2Passed;
                }
            }
            Assert.AreEqual(testPassed, tpResult);
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
                Customer newCustomer = AddTestCustomer(node, newPostCustomer);
                if (newCustomer != null && newCustomer.id != null)
                {
                    //customer is created, then update id

                    newCustomer.extra = "CAMPO AGGIORNATO IN PUT " + DateTime.Now.ToShortTimeString();
                    Customer updatedCustomer = node.AddCustomer((PostCustomer)newCustomer, true);

                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id);
                    //compare results
                    bool test1Passed = Util.Compare<Customer>(myTestCustomer1, updatedCustomer);

                    //delete added customer
                    bool test2Passed = node.DeleteCustomer(newCustomer.id);
                    testPassed = test1Passed && test2Passed;
                }
            }
            Assert.AreEqual(testPassed, tpResult);
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
        /// Add a new customer with random external ID
        /// </summary>
        private Customer AddTestCustomer(Node currentNode, PostCustomer newCustomer)
        {
            //post new customer
            string customerID = null;
            Customer createdCustomer = null;
            createdCustomer = currentNode.AddCustomer(newCustomer, false);
            customerID = createdCustomer.id;
            return createdCustomer;
        }
    }
}
