using ContactHubSdkLibrary.SDKclasses;
using NUnit.Framework;
using System.Threading;

namespace ContactHubSdkLibrary.Test
{
    [TestFixture]
    public class WorkSpaceTest
    {
        [TestCase( true)]
        public void S_OpenWorkSpace(bool tpIsValid)
        {
            string tpWorkspaceID = Util.getTestWorkspace();
            string tpTokenID = Util.getTestToken();

            Workspace currentWorkspace = new Workspace(
                    tpWorkspaceID,
                    tpTokenID
                );

            Assert.AreEqual(currentWorkspace.isValid, tpIsValid);
            Thread.Sleep(Util.GetExitTime()); //wait

        }
    }
}
