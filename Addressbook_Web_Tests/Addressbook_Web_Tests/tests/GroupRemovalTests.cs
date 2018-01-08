using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {      
            appManager.Groups.CheckGroupPresent();
            List<GroupData> oldGroups = appManager.Groups.GetGroupsList();

            appManager.Groups.Remove();

            List<GroupData> newGroups = appManager.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count, newGroups.Count + 1);
        }
    }
}
