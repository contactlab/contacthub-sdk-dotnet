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
            CHubNode node = new CHubNode();

            node.Init(
                ConfigurationManager.AppSettings["workspaceID"].ToString(),
                ConfigurationManager.AppSettings["token"].ToString(),
                ConfigurationManager.AppSettings["node"].ToString()
                );

            var customers= node.GetCustomers();

        }
    }
}
