using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidData()
        {
            appManager.Auth.Logout();

            AccountData account = new AccountData("admin", "secret");
            appManager.Auth.Login(account);

            Assert.IsTrue(appManager.Auth.LoggedIn(account));
        }

        [Test]
        public void LoginWithInvalidData()
        {
            appManager.Auth.Logout();

            AccountData account = new AccountData("admin", "12345");
            appManager.Auth.Login(account);

            Assert.IsFalse(appManager.Auth.LoggedIn(account));
        }
    }
}
