using ContactHubSdkLibrary.Models;
using ContactHubSdkLibrary.SDKclasses;
using KellermanSoftware.CompareNetObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
        [TestCase(true)]
        public void C_AddCustomer( bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() + "@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error, false);
                //wait for elastic update
                DateTime start = DateTime.Now;
                Thread.Sleep(Util.GetWaitTime());
                if (newCustomer != null && newCustomer.id != null)
                {
                    double sec = 0;
                    //customer is created!
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id, ref error);

                    while (node.GetCustomerByExternalID(newCustomer.externalId, ref error)==null)
                    {
                        DateTime end2 = DateTime.Now;
                        sec = (end2 - start).TotalSeconds;
                        Debug.Print(sec.ToString());
                        Thread.Sleep(10000);
                    }
                    DateTime end = DateTime.Now;
                    sec = (end - start).TotalSeconds;

                    Customer myTestCustomer2 = node.GetCustomerByExternalID(newCustomer.externalId, ref error).FirstOrDefault();
                    //compare results
                    CompareLogic compareLogic = new CompareLogic();
                    bool testPassed1 = compareLogic.Compare(myTestCustomer1, myTestCustomer2).AreEqual;
                    //compare results with posted Customer
                    //PostCustomer myPostTestCustomer1 = myTestCustomer1.ToPostCustomer();
                    bool testPassed2 = compareLogic.Compare(newCustomer, myTestCustomer1).AreEqual;
                    //delete added customer
                    bool testPassed3 = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = testPassed1 && testPassed2 && testPassed3;
                }
            }
            Common.WriteLog("End CustomerAddCustomer test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Util.GetExitTime()); //wait
        }


        /// <summary>
        /// Test customer life cycle with extended properties
        /// </summary>
        [TestCase(true)]
        public void C_AddCustomerWithExtendedProperties( bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();
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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it",
                        otherContacts = new List<OtherContacts>(),
                        mobileDevices = new List<MobileDevices>()
                    },
                    educations = new List<Educations>(),
                    jobs = new List<Jobs>(),
                    likes = new List<Likes>(),
                    subscriptions = new List<Subscriptions>(),
                    timezone = BasePropertiesTimezoneEnum.AfricaAlgiers
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
                Thread.Sleep(Util.GetWaitTime());
                if (newCustomer != null && newCustomer.id != null)
                {
                    //customer is created!
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id, ref error);
                    //compare results
                    CompareLogic compareLogic = new CompareLogic();

                    //compare results with posted Customer
                    PostCustomer myPostTestCustomer1 = myTestCustomer1.ToPostCustomer();
                    List<ExtendedProperty> extended1 = newCustomer.extended;
                    List<ExtendedProperty> extended2 = myPostTestCustomer1.extended;
                    //compare extended properties
                    bool testPassed1 = compareLogic.Compare(extended1, extended2).AreEqual;
                    //compare without extended properties
                    compareLogic.Config.MembersToIgnore.Add("_extended");
                    bool testPassed2 = compareLogic.Compare(newCustomer, myTestCustomer1).AreEqual;


                    //compare results with original data
                    compareLogic.Config.MembersToIgnore.Clear();
                    compareLogic.Config.MembersToIgnore.Add("_extended");
                    compareLogic.Config.MembersToIgnore.Add("enabled");
                    bool testPassed4 = compareLogic.Compare(newPostCustomer, myPostTestCustomer1).AreEqual;

                    //delete added customer
                    bool testPassed3 = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4;
                }
            }
            Common.WriteLog("End CustomerAddCustomerWithExtendedProperties test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Util.GetExitTime()); //wait
        }


        /// <summary>
        /// Test customer life cycle with extended properties. This test use all property data types.
        /// </summary>
        [TestCase( true)]
        public void C_AddCustomerWithExtendedPropertiesFull(bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

            Common.WriteLog("Start C_AddCustomerWithExtendedPropertiesFull TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it",
                        otherContacts = new List<OtherContacts>(),
                        mobileDevices = new List<MobileDevices>()
                    },
                    educations = new List<Educations>(),
                    jobs = new List<Jobs>(),
                    likes = new List<Likes>(),
                    subscriptions = new List<Subscriptions>(),
                    timezone = BasePropertiesTimezoneEnum.AfricaAlgiers
                },
                extended = new List<ExtendedProperty>()
                {
                    new ExtendedPropertyObject()
                                {
                                    name="AllTestsProperties",
                                    value=new List<ExtendedProperty>()
                                    {
                                           new ExtendedPropertyBoolean() //!
                                            {
                                                name="BoolProperty",
                                                value=true
                                            },
                                           new ExtendedPropertyString() //2
                                            {
                                                name="StringProperty",
                                                value="lorem ipsum"
                                            },
                                           new ExtendedPropertyStringArray() //3
                                            {
                                                name="StringsArrayProperty",
                                                value=new List<String>() { "lorem","ipsum" }
                                            },
                                           new ExtendedPropertyNumber() //4
                                            {
                                                name="NumberProperty",
                                                value=1234.5
                                            },
                                           new ExtendedPropertyNumberArray() //5
                                            {
                                                name="NumbersArrayProperty",
                                                value=new List<Double>() { 1.1, 2.2, 3.3, 4, -5, 0 }
                                            },
                                           new ExtendedPropertyDateTime() //6
                                            {
                                               name="DateTimeProperty",
                                               value=DateTime.Now
                                            },
                                           new ExtendedPropertyDateTimeArray() //7
                                            {
                                               name="DateArrayProperty",
                                               value=new List<DateTime>() {  DateTime.Now, DateTime.Now.AddHours(1), DateTime.Now.AddYears(-1) }
                                            },
                                            new ExtendedPropertyEmail() //8
                                            {
                                                name="EmailProperty",
                                                value="name@mydomain.com"
                                            },
                                            new ExtendedPropertyEmailArray() //9
                                            {
                                                name="EmailsArrayProperty",
                                                value=new List<String>() { "name@mydomain.com","name@yourdomain.com" }
                                            },
                                            new ExtendedPropertyHostname() //10
                                            {
                                                name="HostnameProperty",
                                                value="www.mydomain.com"
                                            },
                                            new ExtendedPropertyHostnameArray() //11
                                            {
                                                name="HostnamesArrayProperty",
                                                value=new List<String>() { "www.mydomain.com","www.yourdomain.com" }
                                            },
                                             new ExtendedPropertyURI() //12
                                            {
                                                name="URIProperty",
                                                value="http://www.mydomain.com"
                                            },
                                            new ExtendedPropertyURIArray() //13
                                            {
                                                name="URIsArrayProperty",
                                                value=new List<String>() { "http://www.mydomain.com","http://www.yourdomain.com" }
                                            },
                                            new ExtendedPropertyIPv4() //14
                                            {
                                                name="IPv4Property",
                                                value="192.168.0.1/16"
                                            },
                                            new ExtendedPropertyIPv4Array() //15
                                            {
                                                name="IPv4sArrayProperty",
                                                value=new List<String>() { "192.168.0.1/16", "192.168.0.2/16" }
                                            },
                                            new ExtendedPropertyIPv6() //16
                                            {
                                                name="IPv6Property",
                                                value="ffff:ffff:ffff:ffff:0208:dbff:feef:3433"
                                            },
                                            new ExtendedPropertyIPv6Array() //17
                                            {
                                                name="IPv6sArrayProperty",
                                                value=new List<String>() { "ffff:ffff:ffff:ffff:0208:dbff:feef:3433", "ffff:ffff:ffff:ffff:0208:dbff:feef:3434" }
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
                Thread.Sleep(Util.GetWaitTime());
                if (newCustomer != null && newCustomer.id != null)
                {
                    //customer is created!
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id, ref error);
                    //compare results
                    CompareLogic compareLogic = new CompareLogic();

                    //compare results with posted Customer
                    PostCustomer myPostTestCustomer1 = myTestCustomer1.ToPostCustomer();
                    List<ExtendedProperty> extended1 = newCustomer.extended;
                    List<ExtendedProperty> extended2 = myPostTestCustomer1.extended;
                    //compare extended properties
                    bool testPassed1 = compareLogic.Compare(extended1, extended2).AreEqual;
                    //compare without extended properties
                    compareLogic.Config.MembersToIgnore.Add("_extended");
                    bool testPassed2 = compareLogic.Compare(newCustomer, myTestCustomer1).AreEqual;


                    //compare results with original data
                    compareLogic.Config.MembersToIgnore.Clear();
                    compareLogic.Config.MembersToIgnore.Add("_extended");
                    compareLogic.Config.MembersToIgnore.Add("enabled");
                    bool testPassed4 = compareLogic.Compare(newPostCustomer, myPostTestCustomer1).AreEqual;

                    //delete added customer
                    bool testPassed3 = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4;

                }
            }
            Common.WriteLog("End C_AddCustomerWithExtendedPropertiesFull test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Util.GetExitTime()); //wait
        }

        /// <summary>
        /// Test customer life cycle with extended properties
        /// </summary>
        [TestCase(true)]
        public void C_AddCustomerWithComplexContacts(bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

            Common.WriteLog("Start CustomerAddCustomerWithComplexContacts TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID);

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it",
                        mobileDevices = new List<MobileDevices>()
                        {
                            new MobileDevices()
                            {
                                name="mio",
                                type=MobileDevicesTypeEnum.IOS,
                                identifier=Guid.NewGuid().ToString()
                            }
                        },
                        otherContacts = new List<OtherContacts>(),

                    },
                    educations = new List<Educations>(),
                    jobs = new List<Jobs>(),
                    likes = new List<Likes>(),
                    subscriptions = new List<Subscriptions>(),
                    timezone = BasePropertiesTimezoneEnum.AfricaAlgiers
                }
            };

            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error, false);
                //wait for elastic update
                Thread.Sleep(Util.GetWaitTime());
                if (newCustomer != null && newCustomer.id != null)
                {
                    //customer is created!
                    //get customer by ID
                    Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id, ref error);
                    //compare results
                    CompareLogic compareLogic = new CompareLogic();

                    //compare results with posted Customer
                    bool testPassed2 = compareLogic.Compare(newCustomer, myTestCustomer1).AreEqual;

                    //compare results with original data
                    compareLogic.Config.MembersToIgnore.Add("enabled");
                    PostCustomer myPostTestCustomer1 = myTestCustomer1.ToPostCustomer();
                    bool testPassed1 = compareLogic.Compare(newPostCustomer, myPostTestCustomer1).AreEqual;

                    //delete added customer
                    bool testPassed3 = node.DeleteCustomer(newCustomer.id, ref error);


                    testPassed = testPassed1 && testPassed2 && testPassed3;
                }
            }
            Common.WriteLog("End CustomerAddCustomerWithComplexContacts test", "passed:" + testPassed + "\n\n");
            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Util.GetExitTime()); //wait
        }

        /// <summary>
        /// Test customer update (use .UpdateCustomer, full mode -> put)
        /// </summary>
        [TestCase( true)]
        public void C_UpdateCustomer(bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);

                Thread.Sleep(Util.GetWaitTime()); //waiting for elastic update

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
                    compareLogic.Config.MembersToIgnore.Add("_updatedAt");
                    compareLogic.Config.MembersToIgnore.Add("updatedAt");
                    bool test1Passed = compareLogic.Compare(myTestCustomer1, newCustomer).AreEqual;
                    //compare updated customer
                    compareLogic.Config.MembersToIgnore.Clear();
                    bool test2Passed = compareLogic.Compare(myTestCustomer1, updatedCustomer).AreEqual;
                    //delete added customer
                    bool test3Passed = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = test1Passed && test2Passed && test3Passed;
                }
            }
            Common.WriteLog("End CustomerUpdateCustomer test", "passed:" + testPassed + "\n\n");

            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Util.GetExitTime()); //wait
        }

        /// <summary>
        /// Test customer update (use .UpdateCustomer, partial mode -> patch)
        /// </summary>
        [TestCase( true)]
        public void C_PartialUpdateCustomer( bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAlgiers
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);

                Thread.Sleep(Util.GetWaitTime()); //waiting for elastic update

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
                    compareLogic.Config.MembersToIgnore.Add("_updatedAt");
                    compareLogic.Config.MembersToIgnore.Add("updatedAt");

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
            Thread.Sleep(Util.GetExitTime()); //wait
        }


        /// <summary>
        /// Test customer update (use .AddCustomer with FORCE)
        /// </summary>
        [TestCase( true)]
        public void C_UpdateCustomerForced(bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(Util.GetWaitTime()); //wait remote update
                //create clone on PostCustomer subclass
                if (error == null)
                {
                    PostCustomer customer = newCustomer.ToPostCustomer();

                    if (newCustomer != null && newCustomer.id != null)
                    {
                        string testField = "CAMPO AGGIORNATO IN PUT " + DateTime.Now.ToShortTimeString();

                        ////customer is created, then update id
                        customer.extra = testField;
                        ////change external id, try to test duplicate check on email
                        Customer updatedCustomer = node.AddCustomer(customer, ref error, true);
                        ////get customer by ID
                        Customer myTestCustomer1 = node.GetCustomerByID(newCustomer.id, ref error);
                        ////compare results
                        CompareLogic compareLogic = new CompareLogic();
                        bool test1Passed = compareLogic.Compare(myTestCustomer1, updatedCustomer).AreEqual;
                        ////compare source data
                        compareLogic.Config.MembersToIgnore.Add("extra");
                        compareLogic.Config.MembersToIgnore.Add("_updatedAt");
                        compareLogic.Config.MembersToIgnore.Add("updatedAt");

                        bool test2Passed = compareLogic.Compare(myTestCustomer1, newCustomer).AreEqual;
                        compareLogic.Config.MembersToIgnore.Clear();
                        newCustomer.extra = testField;
                        compareLogic.Config.MembersToIgnore.Add("_updatedAt");
                        compareLogic.Config.MembersToIgnore.Add("updatedAt");
                        bool test3Passed = compareLogic.Compare(myTestCustomer1, newCustomer).AreEqual;
                        //delete added customer
                        bool test4passed = node.DeleteCustomer(newCustomer.id, ref error);
                        testPassed = test1Passed && test2Passed && test3Passed && test4passed;
                    }
                }
            }
            Common.WriteLog("End CustomerUpdateCustomerForced test", "passed:" + testPassed + "\n\n");
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

        /// <summary>
        /// Test customer life cycle with paging
        /// </summary>
        [TestCase( 1, true)]
        [TestCase( 3, true)]
        [TestCase( 5, true)]
        [TestCase( 10, true)]
        [TestCase( 50, true)]
        [TestCase( 100, true)]
        [TestCase( 250, true)]

        public void C_Paging( int maxCustomers, bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

            Common.WriteLog("Start C_Paging TEST", "workspace:" + tpWorkspaceID + " token:" + tpTokenID + " node:" + tpNodeID + " maxCustomer:" + maxCustomers);

            Node node = GetTestNode(tpWorkspaceID, tpTokenID, tpNodeID);
            bool testPassed = false;
            //detect current customer number
            PagedCustomer pagedCustomers = null;
            int totalItem = 0;
            int pageSize = 50;
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
                                email = DateTime.Now.Ticks.ToString() +
                                       "dduck@yourdomain.it"
                            },
                            timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
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
                        Customer xx = node.GetCustomerByExternalID(extID, ref error).FirstOrDefault();
                    }
                    totalItem++;
                }
                Thread.Sleep(Util.GetWaitTime()); //wait for remote db update
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
                        testPassed3 = testPassed3 && node.GetCustomers(ref pagedCustomers, PageRefEnum.next, ref error);
                        totPage++;
                    }
                }
                //test total pages
                bool testPassed1 = (totPage == expectedPages);


                //reverse paging
                bool testPassed5 = false;
                pageIsValid = node.GetCustomers(ref pagedCustomers, pageSize, null, null, null, ref error);
                pageIsValid = node.GetCustomers(ref pagedCustomers, PageRefEnum.last, ref error);
                if (pageIsValid)
                {
                    totPage = pagedCustomers.page.totalPages;
                    testPassed5 = true;
                    for (int i = pagedCustomers.page.totalPages - 1; i > 0; i--)
                    {
                        testPassed5 = testPassed5 && node.GetCustomers(ref pagedCustomers, PageRefEnum.previous, ref error);
                        totPage--;
                    }
                }
                bool testPassed6 = (totPage == 1);
                Common.WriteLog("pageSize|totalItem|totPage|expectedPageg:", pageSize + "|" + totalItem + "|" + totPage + "|" + expectedPages + "\n\n");
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

                testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5 & testPassed6;
                Common.WriteLog("result:", testPassed1 + "," + testPassed2 + "," + testPassed3 + "," + testPassed4 + "," + testPassed5 + "," + testPassed5 + "\n\n");
            }
            else
            {

            }

            Common.WriteLog("End C_Paging test", "passed:" + testPassed + "\n\n");

            Assert.AreEqual(testPassed, tpResult);
            Thread.Sleep(Util.GetExitTime()); //wait
        }

        /// <summary>
        /// Test get customer, by external ID
        /// </summary>
        [TestCase( true)]
        public void C_GetCustomerByExternaID(bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(Util.GetWaitTime()); //wait remote update

                if (newCustomer != null && newCustomer.id != null)
                {
                    Customer myTestCustomer1 = node.GetCustomerByExternalID(extID, ref error).FirstOrDefault();
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
            Thread.Sleep(Util.GetExitTime()); //wait
        }

        /// <summary>
        /// Test get customer with custom query
        /// </summary>
        [TestCase( true)]
        public void C_GetCustomerByCustomQuery(bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(Util.GetWaitTime()); //wait remote update

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

                    //if (pagedCustomers._embedded != null && pagedCustomers._embedded.customers != null)
                    if (pagedCustomers.elements != null)
                    {
                        Customer myTestCustomer1 = pagedCustomers.elements.First();
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
            Thread.Sleep(Util.GetExitTime()); //wait
        }

        /// <summary>
        /// Test get customer with QueryBuilder
        /// </summary>
        [TestCase( true)]
        public void C_GetCustomerByQueryBuilder(bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(Util.GetWaitTime()); //wait remote update

                if (newCustomer != null && newCustomer.id != null)
                {
                    PagedCustomer pagedCustomers = null;

                    QueryBuilder qb = new QueryBuilder();
                    qb.AddQuery(new QueryBuilderItem() { attributeName = "base.firstName", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = "\"Donald\"" }); //use \" to delimit string value
                    qb.AddQuery(new QueryBuilderItem() { attributeName = "base.lastName", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = "\"Duck\"" });
                    qb.AddQuery(new QueryBuilderItem() { attributeName = "id", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = String.Format("\"{0}\"",newCustomer.id) });
                    node.GetCustomers(ref pagedCustomers, 10, null, qb.GenerateQuery(QueryBuilderConjunctionEnum.AND), null, ref error);

                    if (pagedCustomers.elements != null && pagedCustomers.elements != null && pagedCustomers.elements.Count > 0)
                    {
                        Customer myTestCustomer1 = pagedCustomers.elements.First();
                        //compare source data
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
            Thread.Sleep(Util.GetExitTime()); //wait
        }

        /// <summary>
        /// Test customer likes
        /// </summary>
        [TestCase(true)]
        public void C_LikesLifeCycle(bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(Util.GetWaitTime()); //wait remote update
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
                    compareLogic.Config.MembersToIgnore.Add("_createdTime"); //ignore createdTime
                    bool testPassed1 = compareLogic.Compare(newLike, addLike).AreEqual;
                    Thread.Sleep(Util.GetWaitTime()); //wait remote update
                    Likes getLike = node.GetCustomerLike(newCustomer.id, likeID, ref error);
                    compareLogic.Config.MembersToIgnore.Add("_createdTime"); //ignore createdTime
                    bool testPassed2 = compareLogic.Compare(newLike, getLike).AreEqual;
                    getLike.category = "music";
                    Likes updatedLike = node.UpdateCustomerLike(newCustomer.id, getLike, ref error);
                    bool testPassed3 = !compareLogic.Compare(newLike, updatedLike).AreEqual;
                    compareLogic.Config.MembersToIgnore.Add("category"); //ignore category
                    bool testPassed4 = compareLogic.Compare(newLike, updatedLike).AreEqual;

                    //delete data
                    bool testPassed5 = node.DeleteCustomerLike(newCustomer.id, updatedLike.id, ref error);
                    bool testPassed6 = node.DeleteCustomer(newCustomer.id, ref error);

                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5 && testPassed6;
                }
                Common.WriteLog("End CustomerLikesLifeCycle test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Util.GetExitTime()); //wait
            }
        }
        /// <summary>
        /// Test customer educations
        /// </summary>
        [TestCase(true)]
        public void C_EducationLifeCycle( bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(Util.GetWaitTime()); //wait remote update
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
                    Thread.Sleep(Util.GetWaitTime()); //wait remote update
                    Educations getEdu = node.GetCustomerEducation(newCustomer.id, eduID, ref error);
                    bool testPassed2 = compareLogic.Compare(newEdu, getEdu).AreEqual;
                    getEdu.schoolName = "Marconi";
                    Educations updatedEdu = node.UpdateCustomerEducation(newCustomer.id, getEdu, ref error);
                    bool testPassed3 = !compareLogic.Compare(newEdu, updatedEdu).AreEqual;
                    compareLogic.Config.MembersToIgnore.Add("schoolName"); //ignore schoolName
                    bool testPassed4 = compareLogic.Compare(newEdu, updatedEdu).AreEqual;
                    //delete data
                    bool testPassed5 = node.DeleteCustomerEducation(newCustomer.id, updatedEdu.id, ref error);
                    bool testPassed6 = node.DeleteCustomer(newCustomer.id, ref error);

                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5 & testPassed6;
                    Thread.Sleep(Util.GetWaitTime()); //wait remote update
                }
                Common.WriteLog("End CustomerEducationLifeCycle test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Util.GetExitTime()); //wait
            }
        }

        /// <summary>
        /// Test customer jobs
        /// </summary>
        [TestCase( true)]
        public void C_JobLifeCycle(bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(Util.GetWaitTime()); //wait remote update
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
                    if (error == null)
                    {
                        CompareLogic compareLogic = new CompareLogic();
                        bool testPassed1 = compareLogic.Compare(newJob, addJob).AreEqual;
                        Thread.Sleep(Util.GetWaitTime()); //wait remote update
                        Jobs getJob = node.GetCustomerJob(newCustomer.id, jobID, ref error);
                        bool testPassed2 = compareLogic.Compare(newJob, getJob).AreEqual;
                        getJob.companyName = "Acme Inc.";
                        Jobs updatedJob = node.UpdateCustomerJob(newCustomer.id, getJob, ref error);
                        bool testPassed3 = !compareLogic.Compare(newJob, updatedJob).AreEqual;
                        compareLogic.Config.MembersToIgnore.Add("companyName"); //ignore schoolName
                        bool testPassed4 = compareLogic.Compare(newJob, updatedJob).AreEqual;
                        //delete data
                        bool testPassed5 = node.DeleteCustomerJob(newCustomer.id, updatedJob.id, ref error);
                        bool testPassed6 = node.DeleteCustomer(newCustomer.id, ref error);

                        testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5 && testPassed6;
                    }
                    Thread.Sleep(Util.GetWaitTime()); //wait remote update
                }
                Common.WriteLog("End CustomerJobLifeCycle test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Util.GetExitTime()); //wait
            }
        }

        /// <summary>
        /// Test customer subscription
        /// </summary>
        [TestCase( true)]
        public void C_SubscriptionLifeCycle( bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(Util.GetWaitTime()); //wait remote update
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
                        startDate = DateTime.Now,
                        endDate = DateTime.Now,
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
                    compareLogic.Config.MembersToIgnore.Add("_createdTime"); //ignore createdTime
                    compareLogic.Config.MembersToIgnore.Add("_startDate");
                    compareLogic.Config.MembersToIgnore.Add("_endDate");
                    compareLogic.Config.MembersToIgnore.Add("_registeredAt");
                    compareLogic.Config.MembersToIgnore.Add("_updatedAt");

                    bool testPassed1 = compareLogic.Compare(newSubscription, addSub).AreEqual;
                    Thread.Sleep(Util.GetWaitTime()); //wait remote update
                    Subscriptions getSub = node.GetCustomerSubscription(newCustomer.id, subID, ref error);
                    compareLogic.Config.MembersToIgnore.Add("_createdTime"); //ignore createdTime
                    compareLogic.Config.MembersToIgnore.Add("_startDate");
                    compareLogic.Config.MembersToIgnore.Add("_endDate");
                    compareLogic.Config.MembersToIgnore.Add("_registeredAt");
                    compareLogic.Config.MembersToIgnore.Add("_updatedAt");
                    bool testPassed2 = compareLogic.Compare(newSubscription, getSub).AreEqual;
                    getSub.type = "newTYPE";
                    Subscriptions updatedSub = node.UpdateCustomerSubscription(newCustomer.id, getSub, ref error);
                    bool testPassed3 = !compareLogic.Compare(newSubscription, updatedSub).AreEqual;
                    compareLogic.Config.MembersToIgnore.Add("type"); //ignore schoolName
                    bool testPassed4 = compareLogic.Compare(newSubscription, updatedSub).AreEqual;
                    //delete data
                    bool testPassed5 = node.DeleteCustomerSubscription(newCustomer.id, updatedSub.id, ref error);
                    bool testPassed6 = node.DeleteCustomer(newCustomer.id, ref error);

                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5 && testPassed6;
                }
                Common.WriteLog("End CustomerSubscriptionLifeCycle test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Util.GetExitTime()); //wait
            }
        }

        /// <summary>
        /// Test add session to customer
        /// </summary>
        [TestCase(true)]
        public void C_AddSession(bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(Util.GetWaitTime()); //wait remote update

                if (newCustomer != null && newCustomer.id != null)
                {
                    Session newSession = new Session();
                    Session returnSession = node.AddCustomerSession(newCustomer.id, newSession, ref error);
                    bool testPassed1 = returnSession != null && !string.IsNullOrEmpty(returnSession.id);

                    ////delete data
                    bool testPassed2 = node.DeleteCustomer(newCustomer.id, ref error);
                    testPassed = testPassed1 && testPassed2;
                }
                Common.WriteLog("End CustomerAddSession test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Util.GetExitTime()); //wait
            }
        }


        /// <summary>
        /// Test add session to customer
        /// </summary>
        [TestCase( true)]
        public void C_TagsLifeCycle( bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
                        email = DateTime.Now.Ticks.ToString() +
                        "dduck@yourdomain.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.AfricaAbidjan
                }
            };
            bool testPassed = false;
            if (node != null)
            {
                Customer newCustomer = node.AddCustomer(newPostCustomer, ref error);
                Thread.Sleep(Util.GetWaitTime()); //wait remote update

                if (newCustomer != null && newCustomer.id != null)
                {
                    Tags customerTag = node.GetCustomerTags(newCustomer.id, ref error);
                    bool testPassed1 = customerTag == null || customerTag.manual == null || customerTag.manual.Count == 0;
                    //manual
                    node.AddCustomerTag(newCustomer.id, "sport", CustomerTagTypeEnum.Manual, ref error);
                    Tags addCustomerTags = node.AddCustomerTag(newCustomer.id, "life", CustomerTagTypeEnum.Manual, ref error);
                    bool testPassed2 = addCustomerTags.manual != null || addCustomerTags.manual.Count == 2;
                    Tags removeCustomerTags = node.DeleteCustomerTag(newCustomer.id, "sport", CustomerTagTypeEnum.Manual, ref error);
                    bool testPassed3 = removeCustomerTags.manual != null || removeCustomerTags.manual.Count == 1;
                    //auto
                    node.AddCustomerTag(newCustomer.id, "sport", CustomerTagTypeEnum.Auto, ref error);
                    addCustomerTags = node.AddCustomerTag(newCustomer.id, "life", CustomerTagTypeEnum.Auto, ref error);
                    bool testPassed4 = addCustomerTags.auto != null || addCustomerTags.auto.Count == 2;
                    removeCustomerTags = node.DeleteCustomerTag(newCustomer.id, "sport", CustomerTagTypeEnum.Auto, ref error);
                    bool testPassed5 = removeCustomerTags.auto != null || removeCustomerTags.auto.Count == 1;

                    testPassed = testPassed1 && testPassed2 && testPassed3 && testPassed4 && testPassed5;
                }
                Common.WriteLog("End CustomerTagsLifeCycle test", "passed:" + testPassed + "\n\n");
                Assert.AreEqual(testPassed, tpResult);
                Thread.Sleep(Util.GetExitTime()); //wait
            }
        }

        /// <summary>
        /// Test reset session ID
        /// </summary>
        [TestCase(true)]
        public void S_ResetSession( bool tpResult)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

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
            Thread.Sleep(Util.GetExitTime()); //wait
        }

     
    }
}

