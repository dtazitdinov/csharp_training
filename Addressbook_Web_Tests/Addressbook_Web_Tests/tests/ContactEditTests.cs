using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactEditTests : AuthTestBase
    {
        [Test]
        public void ContactEditTest()
        {
            ContactData newContactData = new ContactData("Daria", "Polyakova");

            appManager.Contacts.CheckContactPresent();
            List<ContactData> oldContacts = ContactData.GetAllFromDb();

            ContactData toBeEdited = oldContacts[0];

            ContactData oldData = toBeEdited;
            appManager.Contacts.Edit(toBeEdited.Id, newContactData);

            Assert.AreEqual(oldContacts.Count, appManager.Contacts.GetContactsCount());

            List<ContactData> newContacts = ContactData.GetAllFromDb();
            oldContacts[0].FirstName = "Daria";
            oldContacts[0].Lastname = "Polyakova";
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData Contact in newContacts)
            {
                if (Contact.Id == oldData.Id)
                {
                    Assert.AreEqual(Contact.FirstName, newContactData.FirstName);
                    Assert.AreEqual(Contact.Lastname, newContactData.Lastname);
                }
            }
            }
    }
}
