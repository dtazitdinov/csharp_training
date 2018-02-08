using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {      
            appManager.Groups.CheckGroupPresent();
            List<GroupData> oldGroups = appManager.Groups.GetGroupsList();

            GroupData toBeRemoved = oldGroups[0];
            appManager.Groups.Remove(0);

            Assert.AreEqual(oldGroups.Count - 1, appManager.Groups.GetGroupsCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupsList();
            oldGroups.RemoveAt(0);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }

        [Test]
        public void GroupRemovalTestFromDB()
        {
            appManager.Groups.CheckGroupPresent();
            List<GroupData> oldGroups = GroupData.GetAllFromDb();

            GroupData toBeRemoved = oldGroups[0];
            appManager.Groups.Remove(toBeRemoved);

            Assert.AreEqual(oldGroups.Count - 1, appManager.Groups.GetGroupsCount());

            List<GroupData> newGroups = GroupData.GetAllFromDb();
            oldGroups.RemoveAt(0);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
