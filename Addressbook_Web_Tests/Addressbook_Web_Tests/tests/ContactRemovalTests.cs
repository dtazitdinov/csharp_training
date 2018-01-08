using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            appManager.Contacts.CheckContactPresent();
            List<ContactData> oldContacts = appManager.Contacts.GetContactsList();

            appManager.Contacts.Remove();

            List<ContactData> newContacts = appManager.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count, newContacts.Count + 1);
        }

        [Test]
        public void ContactRemovalByNameTest()
        {
            ContactData contactForRemove = new ContactData("Contact for remove");
            contactForRemove.Lastname = null;

            appManager.Contacts.Create(contactForRemove);

            appManager.Contacts.CheckContactPresent();
            List<ContactData> oldContacts = appManager.Contacts.GetContactsList();

            appManager.Contacts.RemoveByName(contactForRemove);

            List<ContactData> newContacts = appManager.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count, newContacts.Count + 1);
        }
    }
}
