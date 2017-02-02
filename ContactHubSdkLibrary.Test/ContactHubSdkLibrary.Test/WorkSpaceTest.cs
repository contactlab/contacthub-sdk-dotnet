using ContactHubSdkLibrary.SDKclasses;
using NUnit.Framework;
using System.Threading;

namespace ContactHubSdkLibrary.Test
{
    [TestFixture]
    public class WorkSpaceTest
    {
        [TestCase("{{workspaceID}}", "{{token}}", true)]
        public void S_OpenWorkSpace(string tpWorkspaceID, string tpToken, bool tpIsValid)
        {
            Workspace currentWorkspace = new Workspace(
                    tpWorkspaceID,
                    tpToken
                );

            Assert.AreEqual(currentWorkspace.isValid, tpIsValid);
            Thread.Sleep(Util.GetExitTime()); //wait

        }
    }
}
