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
            List<ContactData> oldContacts = ContactData.GetAllFromDb();

            ContactData toBeRemoved = oldContacts[0];
            appManager.Contacts.Remove(toBeRemoved.Id);

            Assert.AreEqual(oldContacts.Count - 1, appManager.Contacts.GetContactsCount());

            List<ContactData> newContacts = ContactData.GetAllFromDb();
            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }

        [Test]
        public void ContactRemovalByNameTest()
        {
            ContactData contactForRemove = new ContactData("Contact for remove");
            contactForRemove.Lastname = null;

            appManager.Contacts.Create(contactForRemove);

            appManager.Contacts.CheckContactPresent();
            List<ContactData> oldContacts = ContactData.GetAllFromDb();

            appManager.Contacts.RemoveByName(contactForRemove);

            Assert.AreEqual(oldContacts.Count - 1, appManager.Contacts.GetContactsCount());

            List<ContactData> newContacts = ContactData.GetAllFromDb();

            foreach (ContactData element in oldContacts)
            {
                if (element.FirstName == "Contact for remove")
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
