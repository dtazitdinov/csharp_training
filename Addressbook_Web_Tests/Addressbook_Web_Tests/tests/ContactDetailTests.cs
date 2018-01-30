﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactDetailsTests : AuthTestBase
    {
        [Test]
        public void TestContactDetail()
        {
            string fromDetail = appManager.Contacts.GetContactInformationFromDetail(0);
            ContactData fromForm = appManager.Contacts.GetContactInformationFromForm(0);
            string formInString = appManager.Contacts.GetContactInformationToString(fromForm);

            /*Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);*/
        }



    }
}