using ContactHubSdkLibrary.SDKclasses;
using NUnit.Framework;
using System.Threading;

namespace ContactHubSdkLibrary.Test
{
    [TestFixture]
    public class NodeTest
    {
        /// <summary>
        /// Get node from workspace with specific node ID
        /// </summary>
        [TestCase( true)]
        public void S_OpenSpecificNodeInWorkSpace(bool tpIsValid)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();
            string tpNodeID = Util.getTestNode();

            Workspace currentWorkspace = new Workspace(
                    tpWorkspaceID,
                    tpTokenID
                );
            Node currentNode = null;
           
            currentNode = currentWorkspace.GetNode(tpNodeID);
            
            Assert.AreEqual(currentNode != null, tpIsValid);
            Thread.Sleep(Util.GetExitTime()); //wait
        }
    }
}
