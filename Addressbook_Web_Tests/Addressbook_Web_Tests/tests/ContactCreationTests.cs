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
            ContactData newContact = new ContactData("Denis", "Tazitdinov")
            {
                Middlename = "Dinislan",
                Nickname = "Timbildim",
                Title = "Title here",
                Company = "One Inc",
                Address = "Kurchatova 25A, 47 ",
                HomePhone = "111 11 11",
                MobilePhone = "+2 (222) 222-22-22",
                WorkPhone = "333-33-33",
                FaxPhone = "444 44 44",
                Email = "1111@aaa.ru",
                Email2 = "2222@bbb.ru",
                Email3 = "3333@ccc.com",
                Homepage = "www.qwerty.com",
                Birthday = new DateTime(year: 1988, month: 10, day: 18),
                Anniversary = new DateTime(year: 2014, month: 03, day: 30),
                SecondaryAddress = "Komunna 23, 29",
                SecondaryPhone = "555-55-55",
                Notes = "Notes here"
            };

            appManager.Navigator.GoToHomePage();
            List<ContactData> oldContacts = appManager.Contacts.GetContactsList();

            appManager.Contacts.Create(newContact);

            Assert.AreEqual(oldContacts.Count + 1, appManager.Contacts.GetContactsCount());

            List<ContactData> newContacts = appManager.Contacts.GetContactsList();

            oldContacts.Add(newContact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}

