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
    public class CHubNodeTest
    {
        [Test]
        public void CHubNode_Init()
        {
            CHubNode node = new CHubNode("c", "xx", "xx");
 
            Assert.That(node,! Is.Null);
        }
    }
}
