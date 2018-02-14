using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            appManager.Contacts.CheckContactPresent();
            appManager.Groups.CheckGroupPresent();

            List<GroupData> groups = GroupData.GetAllFromDb();
            GroupData group = appManager.Groups.GetFreeGroup(groups);

            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAllFromDb().Except(oldList).First();

            appManager.Contacts.AddToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
