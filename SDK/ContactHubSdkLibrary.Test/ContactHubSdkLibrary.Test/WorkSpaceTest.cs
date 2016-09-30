﻿using ContactHubSdkLibrary.SDKclasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHubSdkLibrary.Test
{
    [TestFixture]
    public class WorkSpaceTest
    {
        [TestCase("e9062bbf-4c71-42a0-af4e-3a145b0beb35", "0027255e02344ac1a0426d896cd899386beaf7d41c224c229e77432923f9301f",true)]
        public void OpenWorkSpace(string tpWorkspaceID, string tpToken, bool tpIsValid)
        {
            Workspace currentWorkspace = new Workspace(
                    tpWorkspaceID,
                    tpToken                
                );

            Assert.AreEqual(currentWorkspace.isValid,tpIsValid);
        }
    }
}
