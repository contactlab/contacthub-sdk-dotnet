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
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6", true)]
//        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f", "invalidCode", false)]
        public void OpenSpecificNodeInWorkSpace(string tpWorkspaceID, string tpToken, string nodeID, bool tpIsValid)
        {
            Workspace currentWorkspace = new Workspace(
                    tpWorkspaceID,
                    tpToken
                );
            Node currentNode = null;
           
            currentNode = currentWorkspace.GetNode(nodeID);
            
            Assert.AreEqual(currentNode != null, tpIsValid);
            Thread.Sleep(Const.TIMEEXIT); //wait

        }
    }
}
