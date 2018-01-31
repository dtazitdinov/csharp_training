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
            ContactData newContactData = new ContactData("Daria", "Polyakova")
            {
                Birthday = new DateTime(year: 1983, month: 7, day: 3),
            };



            appManager.Contacts.CheckContactPresent();
            List<ContactData> oldContacts = appManager.Contacts.GetContactsList();

            ContactData oldData = oldContacts[0];
            appManager.Contacts.Edit(0, newContactData);

            Assert.AreEqual(oldContacts.Count, appManager.Contacts.GetContactsCount());

            List<ContactData> newContacts = appManager.Contacts.GetContactsList();
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
