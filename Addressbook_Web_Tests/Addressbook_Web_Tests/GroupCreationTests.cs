﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            navigate.GoToHomePage();
            helperLogin.Login(new AccountData("admin", "secret"));
            navigate.GoToGroupsPage();
            helperGroup.InitNewGroupCreation();
            GroupData group = new GroupData("group_" + rndNumber.Next(1000).ToString());
            group.Header = "header";
            group.Footer = "footer";
            helperGroup.FillGroupForm(group);
            helperGroup.SubmitGroupCreation();
            helperGroup.ReturnToGroupPage();
            helperLogin.Logout();
        }
    }
}
