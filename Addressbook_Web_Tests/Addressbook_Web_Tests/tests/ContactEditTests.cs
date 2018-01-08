using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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
            appManager.Contacts.Edit(newContactData);
        }

    }
}
