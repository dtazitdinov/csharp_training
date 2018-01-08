﻿using System;
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
        [Test]
        public void GroupCreationTest()
        {
            GroupData newGroup = new GroupData("group #" + OtherMetods.RandomNumber(1000));
            newGroup.Header = "header";
            newGroup.Footer = "footer";

            appManager.Navigator.GoToGroupsPage();
            List<GroupData> oldGroups = appManager.Groups.GetGroupsList();

            appManager.Groups.Create(newGroup);

            List<GroupData> newGroups = appManager.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            appManager.Navigator.GoToGroupsPage();
            List<GroupData> oldGroups = appManager.Groups.GetGroupsList();

            appManager.Groups.Create(group);

            List<GroupData> newGroups = appManager.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        }
    }
}
