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
            ContactData newContactData = new ContactData("Daria");
            newContactData.Lastname = "Polyakova";


            appManager.Contacts.CheckContactPresent();
            List<ContactData> oldContacts = appManager.Contacts.GetContactsList();

            appManager.Contacts.Edit(0, newContactData);

            List<ContactData> newContacts = appManager.Contacts.GetContactsList();
            oldContacts[0].Name = "Daria";
            oldContacts[0].Lastname = "Polyakova";
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
