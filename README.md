Contact Hub C# .NET SDK for Windows
====================================

Table of contents
-----------------

-   [Introduction](#introduction)
-   [Dependencies](#Dependencies)
-   [Getting Started](#GettingStarted)
	-   [1. Create your client application](#1Createyourclientapplication)
	-   [2. Download required packages](#2Downloadrequiredpackages)
	-   [3. Include sdk library](#3Includesdklibrary)
	-   [4. Configure credential](#4Configurecredential)
	-   [5. Instantiate the workspace and the node](#5Instantiatetheworkspaceandthenode)
	-   [6. Getallcustomers](#6Getallcustomers)
	-   [7. Add customer](#7Addcustomer)
	-   [8. Error handling](#8ErrorHandling)
-   [Usage](#usage)
-   [Customer Class](#customerClass)
    -   [Add a customer](#addACustomer)
    -   [Add customer with forced update](#Addcustomerwithforcedupdate)
    -   [Update customer (full update)](#updateCustomerFullUpdate)
    -   [Update customer (partial update)](#updateCustomerPartialUpdate)
    -   [Add or update customer with extended properties](#addOrUpdateWithExtProperties)
    -   [Get paged customers](#Getpagedcustomers)
    -   [Get single customer](#GetSingleCustomer)
    -   [Query on customers](#Queryoncustomers)
    -   [Select fields](#Selectfields)
    -   [Delete customer](#Deletecustomer)
    -   [Customer data shortcuts](#Customershortcuts)
    -   [Jobs](#Jobs)
    -   [Education](#Education)
    -   [Subscription](#Subscription)
    -   [Like](#Like)
    -   [Tags](#tags)
-   [Event Class](#EventClass)
    -   [Customer Events](#CustomerEvents)
    -   [Anonymous Events](#AnonymousEvents)
    -   [Get paged events](#Getevents)
    -   [Get single event](#Gesingletevents)
    -   [Session](#Session)
-   [Others](#Others)
    -   [System Update Time](#SystemUpdateTime)
    -   [Logs](#Logs)

<a name="introduction"/>
## Introduction 

This SDK allows you to easily access Contacthub REST API, simplifying all
authentication and data read/write operation. The project is based on the Visual
Studio 2015 IDE. The project can be compiled as a library (dll) and is
accompanied by a sample project and unit test.

<a name="Dependencies"/>
## Dependencies

The only dependency is NewtonsoftJson library, a very popular high-performance
Json framework for .NET [(read
license)](https://raw.github.com/JamesNK/Newtonsoft.Json/master/LICENSE.md).
Newtonsoft Json is available as NuGet package and is already configured in the
*packages.config* file. The project also uses other NuGet packages for unit
testing (NUnit.CompareNETObject). If you are not using units test, these
packages are not required.

<a name="GettingStarted"/>
## Getting Started 

These instructions will help you a get copy of the project up and running on
your local machine for development and testing purposes.

<a name="1Createyourclientapplication"/>
### 1. Create your client application

Create a new Visual Studio solution with a new console application and add the
project Contacthub SDK Library in the references. If you do not need unit test,
ContactHubSdkLibrary.Test is not required.

<a name="2Downloadrequiredpackages"/>
### 2. Download required packages

You can compile this SDK library only if you get the packages listed in
packages.config. To get all required packages, open NuGet Package Manager
Console and type:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ shell
PM> update-package -reinstall
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Then clean and rebuild all solution.

<a name="3Includesdklibrary"/>
### 3. Include SDK library

Add references to SDK library in your application.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
using ContactHubSdkLibrary;
using ContactHubSdkLibrary.Events;
using ContactHubSdkLibrary.Models;
using ContactHubSdkLibrary.SDKclasses;
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="4Configurecredential"/>
### 4. Configure credential

Edit your app.config (or web.config) file and add the following settings:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ xml
<appSettings>
    <add key="workspaceID" value="123123123-123-1232-1232-23433333333"/>
    <add key="token" value="12341234121234123412341234"/>
    <add key="nodeID" value= "23423434-54544-4545-3434-34523453444"/>
</appSettings>
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Replace the sample values with real credentials provided by Contactlab.
AppSettings is the most convenient way to configure the credentials, but if you
wish, you can use your favorite techniques to save and retrive them in order for
the SDK to work.

<a name="5Instantiatetheworkspaceandthenode"/>
### 5. Instantiate the workspace and the node

To instantiate the workspace, use the following code:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
Workspace currentWorkspace = new Workspace(
  ConfigurationManager.AppSettings["workspaceID"].ToString(),
  ConfigurationManager.AppSettings["token"].ToString()
);
Node currentNode = currentWorkspace.GetNode(ConfigurationManager.AppSettings["nodeID"].ToString());
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

This code doesb’t actually make the call to the remote system. It’s only used
to initialize the node.

<a name="6Getallcustomers"/>
### 6. Get all customers

The most simple call is reading customers. If your workspace is empty you’ll get
any results, but it will confirm if your credentials are working.  To get
customers, use PagedCostumer object.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
int pageSize = 5;
PagedCustomer pagedCustomers = null;
Error error=null;
bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null,ref error);
List<Customer> customers = pagedCustomers._embedded.customers;
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

If the call is correct, pageIsCalid return *true*.

<a name="7Addcustomer"/>
### 7. Add customer

To complete your first application, add a *customer* to Contacthub node.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
PostCustomer newCustomer = new PostCustomer()
{
  externalId = Guid.NewGuid().ToString(),
  @base = new BaseProperties()
  {
   firstName = "Donald",
   lastName = "Duck",
   contacts = new Contacts()
   {
    email = "dduck@yourdomain.com"
   },
   timezone = BasePropertiesTimezoneEnum.GeorgiaTime
  }
};
Error error=null;
Customer createdCustomer = currentNode.AddCustomer(newCustomer,ref error, false);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

If the call is correct, you should get a *customer* object with all of its
fields and its *id* attribute not null. This is the internal *id* you'll be
using as an identifier for your customer.

<a name="8ErrorHandling"/>
### 8. Error handling

Each call requires an Error class parameter passed by ref, that allows you to
throw an exception if required.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 Error error=null;
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

The parameter must be void. All functions returns an Error object with error
description in the *message* attribute. If no error are present, null is
returned.

<a name="usage"/>
## Usage

<a name="customerClass"/>
### Customer Class

<a name="addACustomer"/>
#### Add a customer

To create a customer instantiate an object of type Customer and assigns the
required attributes. @base field contains the main customer data. This code
creates a customer with name, surname, email, timezone and an External ID. The
External ID is a field in which the client application can write its own primary
key and use in searches to get the customer.

Sample:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
PostCustomer newCustomer = new PostCustomer()
{
  externalId = Guid.NewGuid().ToString(),
  @base = new BaseProperties()
  {
                    firstName = "Donald",
                    lastName = "Duck",
                    contacts = new Contacts()
                    {
                        email = "dduck@yourdomain.com"
                    },
                    timezone = BasePropertiesTimezoneEnum.GeorgiaTime
  }
};
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Addcustomerwithforcedupdate"/>
#### Add customer with forced update

You can force insert a costumer if it already exists in the node. The remote
system verifies the presence of the customer according to the rules defined in
the Contacthub configuration. To force the update use the *true* in forceUpdate
parameter.

Sample:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
PostCustomer updateCustomer = [...]
updateCustomer.extra = DateTime.Now.ToShortTimeString();
Customer createdCustomer = currentNode.AddCustomer(updateCustomer,ref error, true);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="updateCustomerFullUpdate"/>
#### Update customer (full update)

To update all customer fields, you need to create a PostCustomer object and call
the updateCustomer function with fullUpdate parameter set to *true*. In this way
the customer object will be completely replaced with the new data, including all
fields set to null.

Sample:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
Customer customer = currentNode.UpdateCustomer((PostCustomer)updateCustomer, updateCustomer.id,ref error, true);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

You have to include the customer id to update, because the PostCustomer object
does not have the id attribute. We recommend using partial update to avoid
deleting fields already setted previously.

Important! If you need to update a customer remember to always use a
PostCustomer object. You can’t cast Customer to PostCustomer, you must use
.ToPostCustomer() method.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
                Customer newCustomer = [...] //set customer
                PostCustomer customer = newCustomer.ToPostCustomer();
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="updateCustomerPartialUpdate"/>
#### Update customer (partial update)

If you need to update only certain fields of the customer, you can make a
partial update. In this case only the not null fields will be used in the
update.

Sample:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
Customer customer = currentNode.UpdateCustomer((PostCustomer)partialData, customerID,ref error, false);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="addOrUpdateWithExtProperties"/>
#### Add or update customer with extended properties

The extended properties have a dynamic structure that is defined in the
server-side workspace configuration. You can not have on client an auto-builder
that get a class already structured as extended properties on server side. You
must build your data structure exactly as it is structured on the server.

Extended properties validator is not available in this sdk. For each extended
property you must use the correct datatype.

Available datatype are:

* ExtendedPropertyString: string

* ExtendedPropertyStringArray: array of string

* ExtendedPropertyNumber: number

* ExtendedPropertyNumberArray: array of number

* ExtendedPropertyBoolean: boolean

* ExtendedPropertyObject: object

* ExtendedPropertyObjectArray: array of object

* ExtendedPropertyDateTime: datetime

* ExtendedPropertyDateTimeArray: array of datetime

Sample:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
  PostCustomer newCustomer = new PostCustomer()
  {
   nodeId = currentNodeID,
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
  Customer createdCustomer = currentNode.AddCustomer(newCustomer, ref error);
  if (createdCustomer != null)
  {
     customerID = createdCustomer.id;
  }
  else
  {
     //add customer error
  }
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Getpagedcustomers"/>
#### Get paged customers

To get a list of customer you have to go through a pager. Each page is returned
as PageCustomers object that is passed by ref to the function. Customers array
is in ._embedded.customers attribute

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
PagedEvent pagedEvents = null;
int pageSize = 5;
bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

After the first page, you can easily cycle on next pages with

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
pageIsValid = currentNode.GetCustomers(ref pagedCustomers, PageRefEnum.next,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Sample:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null,ref error);  //first page
if (pageIsValid)
{
 Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
 for (int i = 1; i < pagedCustomers.page.totalPages; i++)
 {
   pageIsValid = currentNode.GetCustomers(ref pagedCustomers, PageRefEnum.next,ref error); //next page
   Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
  }
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

or in this way:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize,
                                             null, null, null,ref error);
 if (pageIsValid)
 {
   Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
   while (currentNode.GetCustomers(ref pagedCustomers, PageRefEnum.next, ref error))
   {
    Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
   }
 }
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

If you have already got the first page or one of the following, you can jump to
a specific page. You can do it if you pass a PagedCustomer object already
valorized by a previous call. Make sure it is not null. Sample: get third page

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, 3, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="GetSingleCustomer"/>
#### Get single customer

You can get a customer through internal id or by the ExternalID

Sample: get customer by ID (internal)

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
Customer customer = currentNode.GetCustomerByID(customerID,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Sample: get customer by external ID

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
Customer customerByExtID = currentNode.GetCustomerByExternalID(extID, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

You can get the customer through external ID also using GetCustomers(). If the
external id is unique, it should always return a single result.

Sample: get customer by external ID

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
bool isValid = currentNode.GetCustomers(ref pagedCustomers, 10, extID, null, null, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Queryoncustomers"/>
#### Query on customers

You can create a query to refine GetCustomers(). You have two ways to specify a
query, the simple mode and the advance mode.

Simple mode: this mode use a query builder to get query string. Sample: query on
firstName and lastName (AND condition).

The simple mode allows you to easily build a query with AND or OR operator. If
you want to build a complex query, you can pass it directly in json format,
according to the rest api specifications.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
QueryBuilder qb = new QueryBuilder();
qb.AddQuery(new QueryBuilderItem() {
            attributeName = "base.firstName",
            attributeOperator = QueryBuilderOperatorEnum.EQUALS,
            attributeValue = "Donald" });
qb.AddQuery(new QueryBuilderItem() {
            attributeName = "base.lastName",
            attributeOperator = QueryBuilderOperatorEnum.EQUALS,
            attributeValue = "Duck" });
currentNode.GetCustomers(ref pagedCustomers, 10, null,
                            qb.GenerateQuery(QueryBuilderConjunctionEnum.AND),
                            null,
                            ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Advanced mode: pass a query string in json format

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
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
currentNode.GetCustomers(ref pagedCustomers, 10, null, querySTR, null,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Selectfields"/>
#### Select fields

You can select the fields returned from the get customers

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
currentNode.GetCustomers(ref pagedCustomers, 10, null, null, "base.firstName,base.lastName",ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Deletecustomer"/>
#### Delete customer

You can delete a customer just by knowing its id

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
currentNode.DeleteCustomer(c.id,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Customershortcuts"/>
#### Customer data shortcuts

There are 5 entities over which you can access directly without updating the
customer, despite being Customer attributes. Subclasses Jobs, Education,
Subscription and Like from Customer Class can be managed directly with specific
add, update and delete method. In a similar way it is also possible to act on
the Tag subclass.

<a name="Jobs"/>
##### Jobs 

Add new job:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
  Customer myCustomer = currentNode.GetCustomerByID("16917ed3-6789-48e0-9f8e-e5e8d3c92310",ref error);
  Jobs newJob = new Jobs()
    {
        id = "d14ef5ad-675d-4bac-a8bb-c4feb4641050",
        companyIndustry = "Aerospace",
        companyName = "Acme inc",
        jobTitle = "Director",
        startDate = DateTime.Now,
        endDate = DateTime.Now.AddDays(1),
        isCurrent = true
    };
  Jobs returnJob = currentNode.AddCustomerJob(myCustomer.id, newJob,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Get and update a customer job:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
        Jobs j = currentNode.GetCustomerJob(customerID, jobID,ref error);
        j.startDate = DateTime.Now;
        j.endDate = DateTime.Now.AddDays(10);
        Jobs updatedJob = currentNode.UpdateCustomerJob(customerID, j,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Remove a customer job: 

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
	bool idDeleted = node.DeleteCustomerJob(customerID, jobID, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Education"/>
##### Education 

Add new education:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
  Customer myCustomer = currentNode.GetCustomerByID("d14ef5ad-675d-4bac-a8bb-c4feb4641050",ref error);
  Educations newEdu = new Educations()
        {
            id = "0eae64f3-12fb-49ad-abb9-82ee595037a2",
            schoolConcentration = "123",
            schoolName = "abc",
            schoolType = EducationsSchoolTypeEnum.COLLEGE,
        };
  Educations returnEdu = currentNode.AddCustomerEducation(myCustomer.id, newEdu,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Get and update a customer education:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
  Educations edu = currentNode.GetCustomerEducation(customerID, educationID,ref error);
  edu.startYear = 2010;
  edu.endYear = 2016;
  Educations updatedEducation = currentNode.UpdateCustomerEducation(customerID, edu,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Remove a customer education:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
	bool isDeleted = node.DeleteCustomerEducation(customerID, educationID, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Subscription"/>
##### Subscription 

Add new subscription:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
    Customer myCustomer = currentNode.GetCustomerByID("d14ef5ad-675d-4bac-a8bb-c4feb4641050",ref error);
    Subscriptions newSubscription = new Subscriptions()
    {
        id = "b33c4b9e-4bbe-418f-a70b-6fb7384fc4ab",
        name = "test subscription",
        type = "testType",
        kind = SubscriptionsKindEnum.DIGITALMESSAGE,
        dateStart = DateTime.Now,
        dateEnd = DateTime.Now,
        subscriberId = "e3ab0e11-4310-4329-b70b-a8b0d0250f67",
        registeredAt = DateTime.Now,
        updatedAt = DateTime.Now,
        preferences = new List<Preferences>()
                {
                    new Preferences()
                                {
                                    key="myKey", value="MyValue"
                                }
                    }
    };
    Subscriptions returnSub = currentNode.AddCustomerSubscription(myCustomer.id, newSubscription,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Get and update a customer subscription:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
    Subscriptions s = currentNode.GetCustomerSubscription(customerID, subscriptionID,ref error);
    s.dateStart = DateTime.Now;
    s.dateEnd = DateTime.Now.AddDays(10);
    Subscriptions updatedSubscription = currentNode.UpdateCustomerSubscription(customerID, s, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Remove a customer subscription: 

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
	bool isDeleted = node.DeleteCustomerSubscription(customerID, subscriptionID, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Like"/>
##### Like 

Add new like:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
    Customer myCustomer = currentNode.GetCustomerByID("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f", ref error);
    Likes newLike = new Likes()
    {
        category = "sport",
        id = "eee8c9d6-e30a-4aa9-93f0-db949ba32841",
        name = "tennis",
        createdTime = DateTime.Now
    };
    Likes returnLike = currentNode.AddCustomerLike(myCustomer.id, newLike,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Get and update a customer like:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
    Likes l = currentNode.GetCustomerLike(customerID, likeID, ref error);
    l.category = "music";
    Likes updatedLike = currentNode.UpdateCustomerLike(customerID, l, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Remove a customer subscription: 

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
	bool isDeleted = node.DeleteCustomerLike(customerID, likeID, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="tags"/>
##### Tags 

The tags consist of two arrays of strings called 'auto' and
'manual' (CustomerTagTypeEnum.Auto CustomerTagTypeEnum.Manual) Get customer
tags:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
    Tags customerTag = currentNode.GetCustomerTags("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

You can add and delete a single element using these shortcuts. Add customer
tags:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
   Tags currentTags = currentNode.AddCustomerTag(
                            "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",
                            "sport",
                            CustomerTagTypeEnum.Manual,
                            ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Remove customer tags:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
    Tags currentTags = currentNode.RemoveCustomerTag(
                            "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",
                            "sport",
                            CustomerTagTypeEnum.Manual,
                            ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="EventClass"/>
### Event Class

The events are based on the template, choose the value for the 'type' field will
then have to use therefore the right template for the field 'properties'.

The same thing also applies to the 'context' field, which defines the template
to be used for the field 'contextInfo' Type and Context are defined in
eventPropertiesClass.cs and eventContextClass.cs files.

The correspondence between the value of the enum and its class is very intuitive
because you can derive the name of each other. For example if you choose
type=EventTypeEnum.openedTicket, the properties will be allocated through
EventPropertyOpenedTicket class; if you choose context = EventContextEnum.WEB,
the contextInfo will be allocated through a EventContextPropertyWEB class.

<a name="CustomerEvents"/>
#### Customer Events

You can add an event directly to a customer if you know the id.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
  Customer myCustomer = currentNode.GetCustomerByID("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",ref error);
                PostEvent newEvent = new PostEvent()
                {
                    customerId = myCustomer.id,
                    type = EventTypeEnum.clickedLink,
                    context = EventContextEnum.OTHER,
                    properties = new EventBaseProperty()
                };
                string result = currentNode.AddEvent(newEvent,ref error);
                if (result != "202")
                {
                    //insert error
                }
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

in this example are used both properties and contextInfo data:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
    Customer myCustomer = currentNode.GetCustomerByID("d14ef5ad-675d-4bac-a8bb-c4feb4641050",ref error);
    EventPropertyRepliedTicket typeProperties = new EventPropertyRepliedTicket()
    {
        category = new List<String>() { "MyCategory" },
        idTicket = "MyTicketID",
        subject = "Question",
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
    string result = currentNode.AddEvent(newEvent,ref error);
    if (result != "202")
    {
        //insert error
    }
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="AnonymousEvents"/>
#### Anonymous Events

The event class allows you to add events to customer even if they do not have
immediate access to a customer. You can create an event and using
BringBackProperties in two different ways: through a sessionID or through a
ExternalID.

If you use a sessionID, you must first create a session and then use it in any
event. At the end when you create a customer, you will associate the customer
session and reconcile it to all added events so far.

If you create an event using in BringBackProperties with ExternalID, you will
create automatically an empty customer and all events will be associated with
him. Eventually you will be able to update that customer through the ExternalID.
In general the addition of events is not synchronous to which you can not obtain
the newly generated id, as it is put in a processing queue. Add an anonymous
event with a externalID and then reconciles to the customer

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
    string extID = Guid.NewGuid().ToString();
    PostEvent newEvent = new PostEvent()
    {
        bringBackProperties = new BringBackProperty()
        {
            nodeId = currentNode.id,
            type = BringBackPropertyTypeEnum.EXTERNAL_ID,
            value = extID
        },
        type = EventTypeEnum.loggedIn,
        context = EventContextEnum.WEB,
        properties = new EventBaseProperty()
    };
    string result = currentNode.AddEvent(newEvent,ref error);
    if (result != "202")
    {
         //insert error
    }
    else
    {
         Thread.Sleep(1000); //wait event and customer elaboration
         //update customer
         string customerID = null;
         //the customer was made by filling the event with the ExternalID. You must retrieve the customer from externaID and update it
         Customer extIdCustomer = currentNode.GetCustomerByExternalID(extID,ref error);
         customerID = extIdCustomer.id;
         PostCustomer postCustomer = new PostCustomer()
         {
            @base = new BaseProperties()
            {
                firstName = "Donald",
                lastName = "Duck",
                contacts = new Contacts()
                {
                    email = "dduck@yourdomain.it"
                },
                timezone = BasePropertiesTimezoneEnum.YekaterinburgTime
            }
         };
         Customer createdCustomer = currentNode.UpdateCustomer(postCustomer, customerID, true,ref error);
         customerID = createdCustomer.id;
         //wait queue elaboration
         Thread.Sleep(10000);
         pagedEvents = null;
         bool pageIsValid = currentNode.GetEvents(ref pagedEvents, 10, customerID,
                                                     null, null, null, null, null,
                                                     ref error);
        }
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Add an anonymous event with a Session and then reconciles to the customer.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
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
    string result = currentNode.AddEvent(newEvent,ref error);
    Thread.Sleep(1000);
    if (result != "202")
    {
        //insert error
    }
    else
    {
        PostCustomer newPostCustomer = new PostCustomer()
        {
            nodeId = currentNode.id,
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
        Customer newCustomer = currentNode.AddCustomer(newPostCustomer,ref error);
        Thread.Sleep(1000);
        Session returnSession = currentNode.AddCustomerSession(newCustomer.id, currentSession,ref error);
        Thread.Sleep(1000);
        pagedEvents = null;
        bool pageIsValid = currentNode.GetEvents(ref pagedEvents, 10, newCustomer.id,
                                                    null, null, null, null, null,
                                                    ref error);
    }
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Getevents"/>
#### Get paged events

This example allows you to get all the events filtered by customerID. Pagination
follows the same rules as described above for paging customer.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
    List<Event> allEvents = new List<Event>();
    int pageSize = 20;
    //filter by customer id (required)
    bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "5a0c7812-daa9-467a-b641-012d25b9cdd5",
                                        null, null, null, null, null,
                                        ref error);
    if (pageIsValid)
    {
        allEvents.AddRange(pagedEvents._embedded.events);
        Debug.Print(String.Format("Current page {0}/{1}", pagedEvents.page.number + 1, pagedEvents.page.totalPages));
        for (int i = 1; i < pagedEvents.page.totalPages; i++)
        {
            pageIsValid = currentNode.GetEvents(ref pagedEvents, PageRefEnum.next,ref error);
            allEvents.AddRange(pagedEvents._embedded.events);
            Debug.Print(String.Format("Current page {0}/{1}", pagedEvents.page.number + 1, pagedEvents.page.totalPages));
        }
    }
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

In addition to customer id you can filter by type:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
    bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",
                                                EventTypeEnum.clickedLink, null, null, null, null,
                                                ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Or filter by context:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",
                                                EventTypeEnum.clickedLink, EventContextEnum.OTHER, null, null, null,
                                                ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Or filter by active/passive event:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",
                                                null, null, EventModeEnum.ACTIVE, null, null,
                                                ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Or filter by dates:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",
                                                null, null, null, Convert.ToDateTime("2016-01-01"),
                                                Convert.ToDateTime("2016-12-31"),
                                                ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Gesingletevents"/>
#### Get single event

You can get a single event knowing its id.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 Tags customerTag = currentNode.GetCustomerTags("d14ef5ad-675d-4bac-a8bb-c4feb4641050",ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Session"/>
#### Session

The session object allows you to have a session to connect with each other
events and eventually reconcile them to a customer. The Session object is local
in the client SDK, it does not create any type of object on Contact Hub server.
The session ID is automatically generated in the attribute .value

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 Customer myCustomer = currentNode.GetCustomerByID("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",ref error);
 Session newSession = new Session();
 Session returnSession = currentNode.AddCustomerSession(myCustomer.id, newSession,ref error);
 //[...] use the session, then reset it
 newSession.ResetID();
 var newID = newSession.value;
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Others"/>
## Others

<a name="SystemUpdateTime"/>
### System Update Time

The writing of data on the remote platform has latency of approximately 30
seconds. For example, if you add or delete a customer, it will take about 30
seconds before its GetCustomers return a consistent data.

<a name="Logs"/>
### Logs 

You can enable a detailed log of all calls to the rest of Contacthub
remote system. To enable logging you simply add in your app.config \| web.config
the following keys

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ xml
    <add key="ContactHubSdkEnableLog" value= "true"/>
    <add key="ContactHubSdkPathLog" value= "c:\temp\trace.log"/>
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

This can be useful for intercept the server-side errors, not visible on SDK
client side.
