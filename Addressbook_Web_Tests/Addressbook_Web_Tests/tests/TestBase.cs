using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class TestBase
    {
        protected ApplicationManager appManager;

        [SetUp]
        public void SetupTest()
        {
            appManager = new ApplicationManager();
            appManager.Navigator.GoToHomePage();
            appManager.Auth.Login(new AccountData("admin", "secret"));
        }

        [TearDown]
        public void TeardownTest()
        {
            appManager.Auth.Logout();
            appManager.Stop();
        }    
    }
}
