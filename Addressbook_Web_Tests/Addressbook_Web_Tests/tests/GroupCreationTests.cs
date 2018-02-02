using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
            });
            }
            return groups;
        }

        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void GroupCreationTestWithRandomData(GroupData newGroup)
        {
            appManager.Navigator.GoToGroupsPage();
            List<GroupData> oldGroups = appManager.Groups.GetGroupsList();

            appManager.Groups.Create(newGroup);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupsCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupsList();
            oldGroups.Add(newGroup);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void GroupCreationTest()
        {
            GroupData newGroup = new GroupData("Group #" + GenerateRandomNumber(1000))
            {
                Header = "header",
                Footer = "footer"
            };

            appManager.Navigator.GoToGroupsPage();
            List<GroupData> oldGroups = appManager.Groups.GetGroupsList();

            appManager.Groups.Create(newGroup);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupsCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupsList();
            oldGroups.Add(newGroup);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

    }
}
