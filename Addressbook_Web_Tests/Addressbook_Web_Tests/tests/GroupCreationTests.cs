using System;
using System.Text;
using System.IO;
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

        public static IEnumerable<GroupData> GroupDataFromFile()
        {
            List<GroupData> groups = new List<GroupData>();

            string[] lines = File.ReadAllLines("GroupData.csv");
            foreach(string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        [Test, TestCaseSource("GroupDataFromFile")]
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
        public void GroupCreationTestRandomData()
        {
            GroupData newGroup = new GroupData(GenerateRandomString(30))
            {
                Header = GenerateRandomString(100),
                Footer = GenerateRandomString(100)
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
