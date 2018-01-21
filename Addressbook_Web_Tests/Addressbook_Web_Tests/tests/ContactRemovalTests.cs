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

            appManager.Contacts.Remove(0);

            List<ContactData> newContacts = appManager.Contacts.GetContactsList();
            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
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

            foreach (ContactData element in oldContacts)
            {
                if (element.Name == "Contact for remove")
                {
                    oldContacts.Remove(element);
                    break;
                }
            }
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
