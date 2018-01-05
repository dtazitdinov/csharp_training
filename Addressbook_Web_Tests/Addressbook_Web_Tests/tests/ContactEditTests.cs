using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests.tests
{
    [TestFixture]
    public class ContactEditTests : TestBase
    {
        [Test]
        public void ContactEditTest()
        {
            ContactData newContactData = new ContactData("Daria");
            newContactData.Lastname = "Poliakova";

            appManager.Contacts.Edit(newContactData);
        }

    }
}
