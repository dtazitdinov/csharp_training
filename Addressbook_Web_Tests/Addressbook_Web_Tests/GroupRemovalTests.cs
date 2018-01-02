using System;
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
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            navigate.GoToHomePage();
            helperLogin.Login(new AccountData("admin", "secret"));
            navigate.GoToGroupsPage();
            helperGroup.SelectGroup(1);
            helperGroup.RemoveGroup();
            helperGroup.ReturnToGroupPage();
            helperLogin.Logout();
        }
    }
}
