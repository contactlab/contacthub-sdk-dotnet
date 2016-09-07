using ContactHubSdklibrary;
using ContactHubSdkLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {

            string currentNodeID = ConfigurationManager.AppSettings["node"].ToString();

            /* Example 1: open contacthub node */
            
            CHubNode currentNode = new CHubNode(
                        ConfigurationManager.AppSettings["workspaceID"].ToString(),
                        ConfigurationManager.AppSettings["token"].ToString(),
                        currentNodeID
                        );

            /* Example 2: retrieve customers list from node (first page) */


            if (currentNode.isValid)
            {
                PagedCustomer customers = currentNode.GetCustomers();

            }

            /* Example 3: create new customers in node */
            //define new customer
            PostCustomer newCustomer = new PostCustomer()
            {
                nodeId = currentNodeID,
                externalId = Guid.NewGuid().ToString(),
                @base = new BaseProperties()
                {
                    firstName = "Diego",
                    lastName = "Feltrin",
                    contacts = new Contacts()
                    {
                        email = "diego@dimension.it"
                    },
                    timezone = BasePropertiesTimezoneEnum.YekaterinburgTime
                }
            };
            //post new customer
            if (currentNode.isValid)
            {
               
                Customer createdCustomer = currentNode.CreateCustomer(newCustomer);
            }
        }
    }
}
