﻿using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                int bYear = GenerateRandomNumber(9999);
                int bMonth = GenerateRandomNumber(11) + 1;
                int bDay = GenerateRandomNumber(DateTime.DaysInMonth(bYear, bMonth) - 1) + 1;
                int yYear = GenerateRandomNumber(9999);
                int yMonth = GenerateRandomNumber(11) + 1;
                int yDay = GenerateRandomNumber(DateTime.DaysInMonth(yYear, yMonth) - 1) + 1;

                contacts.Add(new ContactData(GenerateRandomString(35))
                {
                    Middlename = GenerateRandomString(30),
                    Lastname = GenerateRandomString(30),
                    Nickname = GenerateRandomString(30),
                    Title = GenerateRandomString(100),
                    Company = GenerateRandomString(30),
                    Address = GenerateRandomString(100),
                    HomePhone = GenerateRandomString(15),
                    MobilePhone = GenerateRandomString(15),
                    WorkPhone = GenerateRandomString(15),
                    FaxPhone = GenerateRandomString(15),
                    Email = GenerateRandomString(100).Replace(" ",""),
                    Email2 = GenerateRandomString(100).Replace(" ", ""),
                    Email3 = GenerateRandomString(100).Replace(" ", ""),
                    Homepage = GenerateRandomString(100),
                    Birthday = new DateTime(bYear, bMonth, bDay), 
                    Anniversary = new DateTime(yYear, yMonth, yDay),
                    SecondaryAddress = GenerateRandomString(100),
                    SecondaryPhone = GenerateRandomString(15),
                    Notes = GenerateRandomString(150)
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromFile()
        {
            List<ContactData> contacts = new List<ContactData>();

            string[] lines = File.ReadAllLines("ContactData.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactData(parts[0])
                {
                    Middlename = parts[1],
                    Lastname = parts[2],
                    Nickname = parts[3],
                    Title = parts[4],
                    Company = parts[5],
                    Address = parts[6],
                    HomePhone = parts[7],
                    MobilePhone = parts[8],
                    WorkPhone = parts[9],
                    FaxPhone = parts[10],
                    Email = parts[11],
                    Email2 = parts[12],
                    Email3 = parts[13],
                    Homepage = parts[14],
                    BirthdayDay = parts[15],
                    BirthdayMonth = parts[16],
                    BirthdayYear = parts[17],
                    AnniversaryDay = parts[18],
                    AnniversaryMonth = parts[19],
                    AnniversaryYear = parts[20],
                    SecondaryAddress = parts[21],
                    SecondaryPhone = parts[22],
                    Notes = parts[23]
                });
            }
            return contacts;
        }

        [Test, TestCaseSource("ContactDataFromFile")]
        public void ContactCreationTestWithRandomData(ContactData newContact)
        {
            appManager.Navigator.GoToHomePage();
            List<ContactData> oldContacts = appManager.Contacts.GetContactsList();

            appManager.Contacts.Create(newContact);

            Assert.AreEqual(oldContacts.Count + 1, appManager.Contacts.GetContactsCount());

            List<ContactData> newContacts = appManager.Contacts.GetContactsList();

            oldContacts.Add(newContact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void ContactCreationTestRandomData()
        {
            int bYear = GenerateRandomNumber(9999);
            int bMonth = GenerateRandomNumber(11) + 1;
            int bDay = GenerateRandomNumber(DateTime.DaysInMonth(bYear, bMonth) - 1) + 1;
            int yYear = GenerateRandomNumber(9999);
            int yMonth = GenerateRandomNumber(11) + 1;
            int yDay = GenerateRandomNumber(DateTime.DaysInMonth(yYear, yMonth) - 1) + 1;

            ContactData newContact = new ContactData(GenerateRandomString(35))
            {
                Middlename = GenerateRandomString(30),
                Lastname = GenerateRandomString(30),
                Nickname = GenerateRandomString(30),
                Title = GenerateRandomString(100),
                Company = GenerateRandomString(30),
                Address = GenerateRandomString(100),
                HomePhone = GenerateRandomString(15),
                MobilePhone = GenerateRandomString(15),
                WorkPhone = GenerateRandomString(15),
                FaxPhone = GenerateRandomString(15),
                Email = Regex.Replace(GenerateRandomString(100), @"\s+", ""),
                Email2 = Regex.Replace(GenerateRandomString(100), @"\s+", ""),
                Email3 = Regex.Replace(GenerateRandomString(100), @"\s+", ""),
                Homepage = GenerateRandomString(100),
                Birthday = new DateTime(bYear, bMonth, bDay),
                Anniversary = new DateTime(yYear, yMonth, yDay),
                SecondaryAddress = GenerateRandomString(100),
                SecondaryPhone = GenerateRandomString(15),
                Notes = GenerateRandomString(150)
            };

            appManager.Navigator.GoToHomePage();
            List<ContactData> oldContacts = appManager.Contacts.GetContactsList();

            appManager.Contacts.Create(newContact);

            Assert.AreEqual(oldContacts.Count + 1, appManager.Contacts.GetContactsCount());

            List<ContactData> newContacts = appManager.Contacts.GetContactsList();

            oldContacts.Add(newContact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void ContactCreationTest()
        {
            ContactData newContact = new ContactData("Denis", "Tazitdinov")
            {
                Middlename = "Dinislan",
                Nickname = "Timbildim",
                Title = "Title here",
                Company = "One Inc",
                Address = "Kurchatova 25A, 47 ",
                HomePhone = "111 11 11",
                MobilePhone = "+2 (222) 222-22-22",
                WorkPhone = "333-33-33",
                FaxPhone = "444 44 44",
                Email = "1111@aaa.ru",
                Email2 = "2222@bbb.ru",
                Email3 = "3333@ccc.com",
                Homepage = "www.qwerty.com",
                Birthday = new DateTime(year: 2017 + 183, month: 10, day: 18),
                Anniversary = new DateTime(year: 2017 + 183, month: 01, day: 30),
                SecondaryAddress = "Komunna 23, 29",
                SecondaryPhone = "555-55-55",
                Notes = "Notes here"
            };

            appManager.Navigator.GoToHomePage();
            List<ContactData> oldContacts = appManager.Contacts.GetContactsList();

            appManager.Contacts.Create(newContact);

            Assert.AreEqual(oldContacts.Count + 1, appManager.Contacts.GetContactsCount());

            List<ContactData> newContacts = appManager.Contacts.GetContactsList();

            oldContacts.Add(newContact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}

