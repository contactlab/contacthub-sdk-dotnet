![Version 1.0.0 rc](https://img.shields.io/badge/version-1.0.0%20release%20candidate-0072bc.svg)

# Contacthub C# .NET SDK for Windows

## Table of contents

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
-   [Further information](#Others)
    -   [System Update Time](#SystemUpdateTime)
    -   [Logs](#Logs)
-   [Class builder](#classBuilder)

<a name="introduction"/>
## Introduction 

This SDK enables you to easily access the Contacthub REST API, and simplifies all
authentication and data read/write operations. The project is based on the Visual
Studio 2015 IDE and can be compiled as a DLL library. It is accompanied by a 
sample project and a unit test.

<a name="Dependencies"/>
## Dependencies

The only dependency is the Newtonsoft Json library, a very popular high-performance
JSON framework for .NET [(read license)](https://raw.github.com/JamesNK/Newtonsoft.Json/master/LICENSE.md).
 Newtonsoft Json is available as a NuGet package and is already configured in the
`packages.config` file. The project also uses other NuGet packages for unit
testing (NUnit.CompareNETObject). If you are not using unit tests, these
packages are not required.

<a name="GettingStarted"/>
## Getting started 

These instructions will help you to get a copy of the project up and running on
your local machine, for development and testing purposes.

<a name="1Createyourclientapplication"/>
### 1. Create your client application

Create a new Visual Studio solution with a new console application, and add the
Contacthub SDK Library belonging to the project, to the references. If you do not need unit test,
`ContactHubSdkLibrary.Test` is not required.

<a name="2Downloadrequiredpackages"/>
### 2. Download the required packages

You can only compile this SDK library if you have the packages listed in
`packages.config`. To get all required packages, open the NuGet Package Manager
Console and type:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ shell
PM> update-package -reinstall
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Then clean and rebuild the solution.

<a name="3Includesdklibrary"/>
### 3. Include the SDK library

Add references to the SDK library in your application:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
using ContactHubSdkLibrary;
using ContactHubSdkLibrary.Events;
using ContactHubSdkLibrary.Models;
using ContactHubSdkLibrary.SDKclasses;
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="4Configurecredential"/>
### 4. Configure the credentials

Edit your `app.config` (or `web.config`) file and add the following settings:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ xml
<appSettings>
    <add key="workspaceID" value="123123123-123-1232-1232-23433333333"/>
    <add key="token" value="12341234121234123412341234"/>
    <add key="nodeID" value= "23423434-54544-4545-3434-34523453444"/>
</appSettings>
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Replace the sample values above with the real credentials, which are provided by Contactlab.
`appSettings` is the most convenient way to configure the credentials, but if you
wish, you can use your favorite technique to save and retrieve them so
the SDK will work.

<a name="5Instantiatetheworkspaceandthenode"/>
### 5. Instantiate the workspace and the node

To instantiate the workspace, use the following:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
Workspace currentWorkspace = new Workspace(
  ConfigurationManager.AppSettings["workspaceID"].ToString(),
  ConfigurationManager.AppSettings["token"].ToString()
);
Node currentNode = currentWorkspace.GetNode(ConfigurationManager.AppSettings["nodeID"].ToString());
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

This code doesn’t actually make the call to the remote system. It is only used
to initialize the node.

<a name="6Getallcustomers"/>
### 6. Get all customers

The most simple call is to read customers. If your workspace is empty you will not get
any results, but it will confirm whether your credentials are working. To get
customers, use the `PagedCustomer` object:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
int pageSize = 5;
PagedCustomer pagedCustomers = null;
Error error=null;
bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null,ref error);
List<Customer> customers = pagedCustomers._embedded.customers;
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

If the call is correct, `pageIsValid` returns `true`.

<a name="7Addcustomer"/>
### 7. Add customer

To complete your first application, add a *customer* to the Contacthub node:

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

If the call is correct, you should get a `customer` object with all of its
fields, while its `id` attribute is not shown as null. This is the internal ID that you will 
use as an identifier for your customer.

<a name="8ErrorHandling"/>
### 8. Error handling

Each call requires an `Error` class parameter passed by a reference, which allows you to
throw an exception if required:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 Error error=null;
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

The parameter must be void. All functions return an `Error` object with an error
description in the `message` attribute. If no errors are present, null is
returned.

<a name="usage"/>
## Use

<a name="customerClass"/>
### Customer Class

<a name="addACustomer"/>
#### Add a customer

To create a customer, instantiate a `Customer` type object and assign the
required attributes. The `@base` field contains the main customer data. This code
creates a customer with a name, last name, email, time zone and an External ID. The
`externalId` is a field in which the client application can write its own primary
key and use it in searches to get the customer.

**Example:**

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
#### Add a customer with a forced update

You can force a customer to be inserted, if they already exist in the node. The remote
system verifies the presence of the customer, according to the rules defined in
the Contacthub configuration. To force the update use `true` in the `forceUpdate`
parameter.

**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
PostCustomer updateCustomer = [...]
updateCustomer.extra = DateTime.Now.ToShortTimeString();
Customer createdCustomer = currentNode.AddCustomer(updateCustomer,ref error, true);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="updateCustomerFullUpdate"/>
#### Update a customer (full update)

To update all customer fields, you need to create a `PostCustomer` object and call
the `updateCustomer` function, with the `fullUpdate` parameter set to `true`. In this way, 
the customer object will be completely replaced with the new data, including all
fields that are set to null.

**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
Customer customer = currentNode.UpdateCustomer((PostCustomer)updateCustomer, updateCustomer.id,ref error, true);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

To be able to update, you have to include the customer ID, because the `PostCustomer` object
does not have the ID attribute. We recommend using *partial update* (see below) to avoid
deleting fields that have already been previously set.

**Important:**

If you need to update a customer, always remember to use a`PostCustomer` object. 
You cannot cast `Customer` to `PostCustomer`, you must use the `.ToPostCustomer()` method.

**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
                Customer newCustomer = [...] //set customer
                PostCustomer customer = newCustomer.ToPostCustomer();
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="updateCustomerPartialUpdate"/>
#### Update a customer (partial update)

If you only need to update certain fields belonging to a customer, you can make a
partial update. In this case, only the `not null` fields will be used for the
update.

**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
Customer customer = currentNode.UpdateCustomer((PostCustomer)partialData, customerID,ref error, false);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="addOrUpdateWithExtProperties"/>
#### Add or update a customer with extended properties

Extended properties have a dynamic structure that is defined in the
server-side workspace configuration. You cannot use an auto-builder on the client, which 
gets a class that is already structured, as the extended properties are on the server side.
 You must build your data structure exactly as it defined on the server.

An extended properties validator is not part of this SDK. 
You must use the correct data type For each extended property.

Available data types are:

* `ExtendedPropertyString`

  String

* `ExtendedPropertyStringArray`

  Array of strings

* `ExtendedPropertyNumber`

  Number

* `ExtendedPropertyNumberArray`

  Array of numbers

* `ExtendedPropertyBoolean`

  Boolean

* `ExtendedPropertyObject`

  Object

* `ExtendedPropertyObjectArray`

  Array of objects

* `ExtendedPropertyDateTime`

  Datetime

* `ExtendedPropertyDateTimeArray`

  Array of datetimes

**Example:**

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

To get a list of customers, you must go through a pager. Each page is returned
as a `PageCustomers` object, which is passed by a reference to the function. The customers array
is in the `._embedded.customers` attribute. The maximum value for the page size is 50.

**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
PagedEvent pagedEvents = null;
int pageSize = 5;
bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

After the first page, you can easily cycle through the next pages with:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
pageIsValid = currentNode.GetCustomers(ref pagedCustomers, PageRefEnum.next,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

**Example:**

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

or:

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

If you already have the first or one of the following pages, you can jump to
a specific page. You can do this if you pass a `PagedCustomer` object that has already been 
identified by a previous call. Make sure it is not null. 

**Example:**

Get the third page:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, 3, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="GetSingleCustomer"/>
#### Get a single customer

You can get a customer through the internal `ID` or the `ExternalID`

**Example:**

Get a customer by the `ID` (internal):

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
Customer customer = currentNode.GetCustomerByID(customerID,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

**Example:**

Get a customer by the `ExternalID`:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
Customer customerByExtID = currentNode.GetCustomerByExternalID(extID, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

You can also get the customer through the external ID by using `GetCustomers()`. If the
external ID is unique, it should always return a single result.

**Example:**

Get a customer by the external ID:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
bool isValid = currentNode.GetCustomers(ref pagedCustomers, 10, extID, null, null, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Queryoncustomers"/>
#### Query customers

You can create a query to refine `GetCustomers()`. You have two ways to specify a
query, the simple mode and the advanced mode.

* Simple mode

  Uses a query builder to get the query string. For example, Query 
`firstName` and `lastName` (AND condition).

The simple mode allows you to easily build a query with an AND or an OR operator. If
you want to build a complex query, you can pass it directly in JSON (see below)
according to the REST API specifications.

**Example:**

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

* Advanced mode

  Pass a query string using JSON

**Example:**

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

You can select the fields returned by `GetCustomers`

**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
currentNode.GetCustomers(ref pagedCustomers, 10, null, null, "base.firstName,base.lastName",ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Deletecustomer"/>
#### Delete a customer

You can delete a customer by just using their ID

**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
currentNode.DeleteCustomer(c.id,ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Customershortcuts"/>
#### Customer data shortcuts

Although they are Customer attributes, there are five entities that you can 
access directly, without carrying out a full or partial customer update. The following 
subclasses of the `Customer Class` can be managed directly with specific 
`add`, `update` and `delete` methods:

- `Jobs`
- `Education`
- `Subscription`
- `Like` 

The `Tag` subclass can also be managed in a similar way.

<a name="Jobs"/>
##### Jobs 

**Examples:**

Add a new job:

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

**Examples:**

Add a new education entry:

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

**Examples:**

Add a new subscription:

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

**Examples:**

Add a new like:

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

Remove a customer like: 

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
	bool isDeleted = node.DeleteCustomerLike(customerID, likeID, ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="tags"/>
##### Tags 

Tags consist of two arrays of strings known as 'auto' and
'manual' (CustomerTagTypeEnum.Auto CustomerTagTypeEnum.Manual) 

**Example:**

Get customer tags:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
    Tags customerTag = currentNode.GetCustomerTags("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

You can add and delete a single element using the following shortcuts:

**Examples:**

Add customer tags:

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

Events are based on the appropriate template. By entering the value for the 
`type` field, the right template is defined for the `properties` field.

The same thing also applies to the `context` field, which defines the template
that is used for the `contextInfo` field. `ype` and `context` are defined in the
`eventPropertiesClass.cs` and `eventContextClass.cs` files.

`tracking` field contains tracking information (for example: google analitycs).

The correlation between the value of the enumeration and its class is very intuitive,
because you can derive the name of one from the other. For example, if you choose
`type=EventTypeEnum.openedTicket`, the properties will be allocated through the
`EventPropertyOpenedTicket` class. If you choose `context = EventContextEnum.WEB`,
the `contextInfo` will be allocated through an `EventContextPropertyWEB` class.

<a name="CustomerEvents"/>
#### Customer Events

You can directly add an event to a customer if you know their ID.

**Examples:**

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

in the following, both the properties and the `contextInfo` data are used:

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

The `event` class allows you to add customer events, even if you do not have
immediate access to the appropriate customer. You can create an event using
`BringBackProperties` in two different ways, through:

* A `sessionID` 

  or:

* An `ExternalID`

If you use a `sessionID`, you must first create a session, then use it for any 
events. When the customer is created, you can associate and reconcile them 
with all events that have been added so far.

If you create an event using `BringBackProperties` with an `ExternalID,` you can
automatically create an empty customer, and all relevant events will be associated with 
them. Later, you will can update the associated customer through the `ExternalID`.

In general, adding an event in this way is asynchronous and, as a result, the API does not 
reply with the newly generated identifier. 

**Examples:**

Add an anonymous event with a `externalID` and then reconcile it with a customer:

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

Add an anonymous event with a `Session` and then reconcile it with a customer:

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
#### Get paged Events

The following enables you to get all events that are filtered using a `customerID`. 
The pagination rules are the same as those described in the **Get paged customers** 
section above.

**Example:**

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

In addition to the `customerID`, you can filter by event type.

**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
    bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",
                                                EventTypeEnum.clickedLink, null, null, null, null,
                                                ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Or filter by context.
`
**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",
                                                EventTypeEnum.clickedLink, EventContextEnum.OTHER, null, null, null,
                                                ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Or filter by active/passive event.

**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",
                                                null, null, EventModeEnum.ACTIVE, null, null,
                                                ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Or filter by dates.

**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 bool pageIsValid = currentNode.GetEvents(ref pagedEvents, pageSize, "9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",
                                                null, null, null, Convert.ToDateTime("2016-01-01"),
                                                Convert.ToDateTime("2016-12-31"),
                                                ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Gesingletevents"/>
#### Get single event

You can get a single event, if you know its `ID`.

**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 Tags customerTag = currentNode.GetCustomerTags("d14ef5ad-675d-4bac-a8bb-c4feb4641050",ref error);
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Session"/>
#### Session

The `Session` object enables you to use a session to locate each event and 
eventually reconcile them with a customer. The `Session` object is local to the 
client SDK. It does not create any type of object on the Contacthub server. 
The `session ID` is automatically generated in the `.value` attribute.

**Example:**

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
 Customer myCustomer = currentNode.GetCustomerByID("9bdca5a7-5ecf-4da4-86f0-78dbf1fa950f",ref error);
 Session newSession = new Session();
 Session returnSession = currentNode.AddCustomerSession(myCustomer.id, newSession,ref error);
 //[...] use the session, then reset it
 newSession.ResetID();
 var newID = newSession.value;
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

<a name="Others"/>
## Further information

<a name="SystemUpdateTime"/>
### System update time

Writing data on the remote platform has a latency of approximately 30 seconds. 
For example, if you add or delete a customer, it will take about 30 seconds 
before `GetCustomers` returns consistent data.

<a name="Logs"/>
### Logs 

You can enable a detailed log of all calls to the Contacthub remote system. 
To enable logging, you simply add the following keys to your `app.config \| web.config`:


~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ xml
    <add key="ContactHubSdkEnableLog" value= "true"/>
    <add key="ContactHubSdkPathLog" value= "c:\temp\trace.log"/>
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

This can be useful to intercept any server-side errors that are not visible on 
the SDK client side.

<a name="classBuilder"/>
## Class builder

Some properties that you may want to change in the future, are not static 
in Contacthub. For example, if you want to add new event types, or new 
enumeration values as a prefix to some properties. 
These definitions are available in a JSON schema, which is readily 
accessible with the REST API. 

To make developer life easier, the SDK provides the required classes and 
enumerators for the schema, which are related to the customer base properties 
(`basePropertiesClass.cs`), events properties (`eventPropertiesClass.cs`) 
and events context properties (`eventContextClass.cs`).
Events have additional properties for tracking data (google analitycs, etc). In trackingClass.cs there are related tracking classes.

You can automatically create the classes as required, by using the project 
that is available in the `classBuilder` folder. 

To update the classes: 

- Open the project that is available in the `classBuilder` folder 
- Copy all generated files from `bin/debug` to `/PropertiesClasses` in the SDK project, overwriting the existing files 
- Rebuild the SDK project 

All new files include a comment with the date of generation in the first line, as follows: 

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ cs
/* selfgenerated from version 0.0.0.1 30/01/2017 12:37:51 */
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 

**Note:**

The real version number is not available at present (fixed value 0.0.0.1)

