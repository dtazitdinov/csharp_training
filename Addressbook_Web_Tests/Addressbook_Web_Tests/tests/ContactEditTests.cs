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
            newContactData.Lastname = "Poliakova";


            appManager.Contacts.CheckContactPresent();
            List<ContactData> oldContacts = appManager.Contacts.GetContactsList();

            appManager.Contacts.Edit(newContactData);

            List<ContactData> newContacts = appManager.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count, newContacts.Count);
        }

    }
}
