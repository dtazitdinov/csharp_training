using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupEditTests : GroupTestBase
    {
        [Test]
        public void GroupEditTest()
        {
            GroupData newData = new GroupData("Changed Group #" + GenerateRandomNumber(1000));
            newData.Header = "Changed Header";
            newData.Footer = "Changed Footer";

            appManager.Groups.CheckGroupPresent();
            List<GroupData> oldGroups = GroupData.GetAllFromDb();

            GroupData toBeEdited = oldGroups[0];

            GroupData oldData = toBeEdited;
            appManager.Groups.Edit(toBeEdited.Id, newData);

            Assert.AreEqual(oldGroups.Count, appManager.Groups.GetGroupsCount());

            List<GroupData> newGroups = GroupData.GetAllFromDb();

            oldGroups[0].Name = newData.Name;
            oldGroups[0].Header = newData.Header;
            oldGroups[0].Footer = newData.Footer;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
                
            }
        }
    }
}
