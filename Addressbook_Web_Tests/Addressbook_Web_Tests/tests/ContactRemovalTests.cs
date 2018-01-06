using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            appManager.Contacts.Remove();
        }

        [Test]
        public void ContactRemovalByNameTest()
        {
            ContactData contactForRemove = new ContactData("Contact for remove");
            contactForRemove.Lastname = null;

            appManager.Contacts.Create(contactForRemove);

            appManager.Contacts.RemoveByName(contactForRemove);
        }
    }
}
