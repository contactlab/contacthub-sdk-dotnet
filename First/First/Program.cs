using ContactHubSdkLibrary;
using ContactHubSdkLibrary.Models;
using ContactHubSdkLibrary.SDKclasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace First
{
    class Program
    {
        static void Main(string[] args)
        {

            Workspace currentWorkspace = new Workspace(ConfigurationManager.AppSettings["workspaceID"].ToString(),
                    ConfigurationManager.AppSettings["token"].ToString());
            string currentNodeID = ConfigurationManager.AppSettings["nodeID"].ToString();
            Node currentNode = currentWorkspace.GetNode(currentNodeID);

            int pageSize = 5;
            PagedCustomer pagedCustomers = null;
            bool pageIsValid = currentNode.GetCustomers(ref pagedCustomers, pageSize, null, null, null);
            List<Customer> customers = pagedCustomers._embedded.customers;

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

        }
    }
}
