
using ContactHubSdkLibrary;
using ContactHubSdkLibrary.Models;

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

            /* Example: open contacthub node */
            
            CHubNode currentNode = new CHubNode(
                        ConfigurationManager.AppSettings["workspaceID"].ToString(),
                        ConfigurationManager.AppSettings["token"].ToString(),
                        currentNodeID
                        );

            /* Example: retrieve customers list from node (first page) */


            if (currentNode.isValid)
            {
                PagedCustomer customers = currentNode.GetCustomers();

            }


            /* Example: create new customers in node */
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
            string customerID = null;
            if (currentNode.isValid)
            {
                Customer createdCustomer = currentNode.CreateCustomer(newCustomer);
                customerID = createdCustomer.id;
            }

            /* Example: get specific customer */
            if (currentNode.isValid)
            {
                Customer customer=currentNode.GetCustomer(customerID);
                customerID = customer.id;
            }

            /* Example: delete customers in node */
            if (currentNode.isValid)
            {
                currentNode.DeleteCustomer(customerID);
                //verify if deleted customer exists
                Customer customer = currentNode.GetCustomer(customerID);
                if (customer==null)
                {
                    //customer does not exists
                }
            }
        }
    }
}
