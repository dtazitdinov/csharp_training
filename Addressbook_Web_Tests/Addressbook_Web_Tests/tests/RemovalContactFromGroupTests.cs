using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    class RemovalContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemovalContactFromGroupTest()
        {
            appManager.Contacts.CheckContactPresent();
            appManager.Groups.CheckGroupPresent();

            List<GroupData> groups = GroupData.GetAllFromDb();
            GroupData group = appManager.Groups.GetGroupWithContacts(groups);
            List<ContactData> oldList = group.GetContacts();

            ContactData contact = oldList.First();

            appManager.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
