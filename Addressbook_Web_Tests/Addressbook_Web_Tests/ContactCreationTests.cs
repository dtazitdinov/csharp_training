using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            navigate.GoToHomePage();
            helperLogin.Login(new AccountData("admin", "secret"));
            helperContact.InitNewContactCreation();
            ContactData contact = new ContactData("Denis");
            contact.Lastname = "Tazitdinov";
            helperContact.FillForm(contact);
            helperContact.SubmitContactCreation();
            helperContact.ReturnHomePage();
            helperLogin.Logout();
        }
    }
}

