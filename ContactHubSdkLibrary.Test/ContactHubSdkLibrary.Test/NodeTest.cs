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
        [TestCase("{{workspaceID}}", "{{token}}", "{{nodeID}}", true)]
//        [TestCase("{{workspaceID}}", "{{token}}", "invalidCode", false)]
        public void S_OpenSpecificNodeInWorkSpace(string tpWorkspaceID, string tpToken, string nodeID, bool tpIsValid)
        {
            Workspace currentWorkspace = new Workspace(
                    tpWorkspaceID,
                    tpToken
                );
            Node currentNode = null;
           
            currentNode = currentWorkspace.GetNode(nodeID);
            
            Assert.AreEqual(currentNode != null, tpIsValid);
            Thread.Sleep(Util.GetExitTime()); //wait

        }
    }
}
