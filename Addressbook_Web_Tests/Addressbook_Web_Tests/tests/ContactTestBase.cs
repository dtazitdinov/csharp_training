using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    class ContactTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareContactsUI_DB()
        {
            if (PROTECTED_LONG_UI_CHECKS)
            {
                List<ContactData> fromUI = appManager.Contacts.GetContactsList();
                List<ContactData> fromDB = ContactData.GetAllFromDb();
                fromUI = (from c in fromUI
                          orderby c.Id
                          select c).ToList();

                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}
