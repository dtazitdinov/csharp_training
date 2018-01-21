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
            GroupData newData = new GroupData("Changed Group #" + OtherMetods.RandomNumber(1000));
            newData.Header = "Changed Header";
            newData.Footer = "Changed Footer";


            appManager.Groups.CheckGroupPresent();
            List<GroupData> oldGroups = appManager.Groups.GetGroupsList();

            appManager.Groups.Edit(0, newData);
            List<GroupData> newGroups = appManager.Groups.GetGroupsList();

            oldGroups[0].Name = newData.Name;
            oldGroups[0].Header = newData.Header;
            oldGroups[0].Footer = newData.Footer;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

        }
    }
}
