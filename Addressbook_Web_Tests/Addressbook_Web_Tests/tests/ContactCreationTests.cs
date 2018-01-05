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
            ContactData newContact = new ContactData("Denis");
            newContact.Lastname = "Tazitdinov";

            appManager.Contacts.Create(newContact);
        }
    }
}

