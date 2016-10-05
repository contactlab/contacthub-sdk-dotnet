## Contact Hub C# .NET SDK for Windows

This SDK allows you to easily access to the REST API ContactHub, simplifying the authentication operations and data read/write on Contact Hub.
The project is based on the Visual Studio 2015 IDE.
The project can be compiled as a library (dll) and is accompanied by a sample project and unit test.

### Dependencies

The only dependency is NewtonsoftJson library , a very popular high-performance Json framework for .NET [(read licence)](https://raw.github.com/JamesNK/Newtonsoft.Json/master/LICENSE.md)

Newtonsoft Json is available as NuGet package and is already configured in the *packages.config* file.

The project also uses two NuGet packages for unit testing (NUnit). If you don't use the unit test, these packages are not required for the integration of the library into your project.

## Getting Started 

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### 1. Create your client application

Create a new Visual Studio solution with a new console application and add the project Contact Sdk Library in the references.
If you do not need at this moment of the unit test (ContactHubSdkLibrary.Test)  don't include it in the solution. You can add it later if you need to.

### 2. Download required packages

You can compile this sdk library only if you get the packages listed in packages.config. <return>
To get all required packages, open NuGet Package Manager Console and type:

```shell
PM> update-package -reinstall
```

Then clean and rebuild all solution.

### 3. Include sdk library

Add references to sdk library in your application.
```cs
using ContactHubSdkLibrary;
using ContactHubSdkLibrary.Events;
using ContactHubSdkLibrary.Models;
using ContactHubSdkLibrary.SDKclasses;
```

### 4. Configure credential

Edit your app.config (or web.config) file and add this settings:
```xml
<appSettings>
    <add key="workspaceID" value="123123123-123-1232-1232-23433333333"/>
    <add key="token" value="12341234121234123412341234"/>
    <add key="nodeID" value= "23423434-54544-4545-3434-34523453444"/>
</appSettings>
```

Replace the sample values with real credentials provided by Contact Lab.

You are not required to save them in this file, if you want you can save this data in any way and make them available in the code as they are needed to invoke the sdk functions calls.
The following examples use the AppSettings because they are a convenient way to configure the credentials in the project.

### 5. Istantiate the workspace and the node
```cs
Workspace currentWorkspace = new Workspace(
  ConfigurationManager.AppSettings["workspaceID"].ToString(),
  ConfigurationManager.AppSettings["token"].ToString()
);
Node currentNode = currentWorkspace.GetNode(ConfigurationManager.AppSettings["nodeID"].ToString());
```

These instructions do not actually make the call to the remote system. They are used only to initialize the node to enable it to operate properly.

### 6. Get all customers

The most simple call you can do is take every customer. Obviously, if your workspace is empty you will not get results, but I confirm that the credentials you entered are correct.
To get the customer must work with paging. For this you need a PagedCustomer object where to get the data.

```cs
int pageSize = 5;
PagedCustomer pagedCustomers = null;
bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null);
List<Customer> customers = pagedCustomers._embedded.customers;
```

For this first call is very important that the value returns in pageIsValid is *true* <return>

### 7. Add customer

To complete your first application, add a *customer* to contacthub node.
```cs
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
Customer createdCustomer = currentNode.AddCustomer(newCustomer, false);
```

If everything went well you should get back a  *customer* object with the fields that you posted with more the *id* attribute valorized.
This is the internal *id* you'll be using as an identifier for your customer.


## Usage

### Customer Class

#### Add a customer
To create a customer instantiate an object of type Customer and assigns the required attributes.
@base field contains the main customer data.
This code creates a customer with name, surname, email, timezone and an External ID.
The External ID is a field in which the client application can write its own primary key and use in searches to get the customer.
Sample:
```cs
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
```


#### Add customer with forced update

You can put a customer forcing the update if already exists in the node. The remote system verifies the presence of the customer according to the rules defined in the contacthub configuration.
To force the update if exists, use the *true* in forceUpdate parameter.
Sample:
```cs
PostCustomer updateCustomer = [...]
updateCustomer.extra = DateTime.Now.ToShortTimeString();
Customer createdCustomer = currentNode.AddCustomer(updateCustomer, true);  
```

#### Update customer (full update)

To update all customer fields, you create an PostCustomer object and call the updateCustomer  function with fullUpdate  parameter set to *true*.
In this way the customer object will be completely replaced with the new data, including all fields set to null.
Sample:
```cs
Customer customer = currentNode.UpdateCustomer((PostCustomer)updateCustomer, updateCustomer.id,true);
```
You have to pass the customer id to  update, because the PostCustomer object does not have the id attribute, exactly as in the APIs that these SDK go to call.
We recommend using partial update to avoid deleting fields already setted previously.

#### Update customer (partial update)
If you need to update only certain fields of the customer, you can make an partial update. In this case only the not null fields will be used in the update.
Sample:
```cs
Customer customer = currentNode.UpdateCustomer((PostCustomer)partialData, customerID, false);
```

#### Add or update customer with extended properties

The extended properties have a dynamic structure that is defined in the server-side workspace configuration.
You can not have on client an auto-builder that get a class already structured as extendend properties on server side. You must build your data structure exactly as it is structured on the server. Extended properties validator is not available in this sdk.
For each extended property you must use the correct datatype.
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
```cs
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
    Customer createdCustomer = currentNode.AddCustomer(newCustomer);
  if (createdCustomer != null)
  {
                    customerID = createdCustomer.id;
  }
  else
  {
                    //add customer error
  }

```

#### Get paged customers

To get a list of customer you have to go through a pager.
Each page is returned as PageCustomers object that is passed by ref to the function.
Customers array is in ._embedded.customers attribute

```cs
PagedEvent pagedEvents = null;
int pageSize = 5;
bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null);
```

After the first page you can easily cycle on next pages with
```cs
pageIsValid = currentNode.GetCustomers(ref pagedCustomers, PageRefEnum.next);
```
Sample:
```cs
bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null);  //first page
if (pageIsValid)
{
 Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
 for (int i = 1; i < pagedCustomers.page.totalPages; i++)
 {
   pageIsValid = currentNode.GetCustomers(ref pagedCustomers, PageRefEnum.next); //next page
   Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
  }
}
```

or in this way:
```cs
 bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null);
 if (pageIsValid)
 {
   Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
   while (currentNode.GetCustomers(ref pagedCustomers, PageRefEnum.next))
   {
    Debug.Print(String.Format("Current page {0}/{1}", pagedCustomers.page.number + 1, pagedCustomers.page.totalPages));
   }
 }
```

If you have already get the first page or one of the following, you can jump to a specific page. You can do if you pass a PagedCustomer object already valorized by a previous call. Make sure it is not null.

Sample: get third page
```cs
bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, 3);
```

#### Get single customer

You can get a customer through  internal id or by the ExternalID

Sample: get customer by ID (internal)
```cs
Customer customer = currentNode.GetCustomerByID(customerID);
```

Sample: get customer by external ID
```cs
Customer customerByExtID = currentNode.GetCustomerByExternalID(extID);
```

You can get the customer through external ID also using GetCustomers().
If the external id is unique in theory should always return a single result.
Sample: get customer by external ID
```cs
bool isValid = currentNode.GetCustomers(ref pagedCustomers, 10, extID, null, null);
```

#### Query on customers

You can create a query to refine GetCustomers()
You have two ways to specify a query. The simple mode allows you to easily build a query with AND or OR operator. If you want to build a complex quey can pass it directly in json format, according to the rest api specifications.

Simple mode:
this mode use a query builder to get query string.

Sample: query on firstName and lastName (AND condition)
```cs
QueryBuilder qb = new QueryBuilder();
qb.AddQuery(new QueryBuilderItem() { attributeName = "base.firstName", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = "Donald" });
qb.AddQuery(new QueryBuilderItem() { attributeName = "base.lastName", attributeOperator = QueryBuilderOperatorEnum.EQUALS, attributeValue = "Duck" });
currentNode.GetCustomers(ref pagedCustomers, 10, null, qb.GenerateQuery(QueryBuilderConjunctionEnum.AND), null);
```

Advanced mode:
 pass a query string in json format 
```cs
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
```

#### Select fields 

You can select the fields returned from the get customers
```cs
currentNode.GetCustomers(ref pagedCustomers, 10, null, null, "base.firstName,base.lastName");
```

#### Delete customer

You can delete a customer just by knowing its id
```cs
currentNode.DeleteCustomer(c.id);
```
