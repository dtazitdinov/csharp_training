using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupEditTests : AuthTestBase
    {
        [Test]
        public void GroupEditTest()
        {
            GroupData newGroupData = new GroupData("Changed Group #" + OtherMetods.RandomNumber(1000));
            newGroupData.Header = "Changed Header";
            newGroupData.Footer = "Changed Footer";


            appManager.Groups.CheckGroupPresent();
            List<GroupData> oldGroups = appManager.Groups.GetGroupsList();

            appManager.Groups.Edit(newGroupData);

            List<GroupData> newGroups = appManager.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count, newGroups.Count);
        }
    }
}
