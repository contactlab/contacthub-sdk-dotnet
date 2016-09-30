using ContactHubSdkLibrary.SDKclasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHubSdkLibrary.Test
{
    [TestFixture]
    public class NodeTest
    {
        [Test]
        public void Node_Init()
        {
            Node node = new Node("c", "xx", "xx");
 
            Assert.That(node,! Is.Null);
        }
    }
}
