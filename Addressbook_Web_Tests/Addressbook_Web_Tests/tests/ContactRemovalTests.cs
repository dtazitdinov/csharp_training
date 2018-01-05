using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests.tests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            ContactData contactForRemove = new ContactData("Contact for remove");

            appManager.Contacts.Create(contactForRemove);

            appManager.Contacts.RemoveByName(contactForRemove);
        }
    }
}
