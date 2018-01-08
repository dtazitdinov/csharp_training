using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData newContact = new ContactData("Denis");
            newContact.Lastname = "Tazitdinov";

            appManager.Navigator.GoToHomePage();
            List<ContactData> oldContacts = appManager.Contacts.GetContactsList();

            appManager.Contacts.Create(newContact);

            List<ContactData> newContacts = appManager.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);
        }
    }
}

