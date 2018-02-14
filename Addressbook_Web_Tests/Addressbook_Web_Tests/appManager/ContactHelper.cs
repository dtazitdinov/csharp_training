using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            InitNewContactCreation();
            FillForm(contact);
            SubmitContactCreation();
            ReturnHomePage();
            return this;
        }

        public ContactHelper Edit(string id, ContactData contact)
        {
            InitEditContact(id);
            FillForm(contact);
            SubmitEditedContact();
            ReturnHomePage();
            return this;
        }

        public ContactHelper Remove(string id)
        {
            SelectContact(id);
            InitRemoveContact();
            driver.SwitchTo().Alert().Accept();
            ReturnHomePage();
            return this;
        }

        public ContactHelper RemoveByName(ContactData contact)
        {
            SelectContactByName(contact);
            InitRemoveContact();
            driver.SwitchTo().Alert().Accept();
            ReturnHomePage();
            return this;
        }

        public void AddToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Id);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectGroup(group.Id);
            SelectContact(contact.Id);
            InitRemoveContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        

        private void InitRemoveContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string id)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByValue(id);
        }

        public void SelectGroup(string id)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByValue(id);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public int GetContactsCount()
        {
            return driver.FindElements(By.XPath("//tr[@name='entry']")).Count;
        }

        public void SelectContactByName(ContactData contact)
        {
            driver.FindElement(By.XPath("//td[text()=\"" + contact.FirstName + "\"]/..//input")).Click();
        }

        public void SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
        }

        public void SelectContact(string id)
        {
            driver.FindElement(By.Id(id)).Click();
        }

        public void SubmitEditedContact()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
        }

        public void InitEditContact(string id)
        {
            driver.FindElement(By.XPath($"//a[@href='edit.php?id={id}']")).Click();
        }

        public void InitEditContact(int index)
        {
            driver.FindElements(By.XPath("//img[@title=\"Edit\"]"))[index].Click();
        }

        public void GoToContactDetail(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
        }

        public ContactHelper ReturnHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper FillForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);

            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);

            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("fax"), contact.FaxPhone);

            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.Homepage);

            string[] monthNames = DateTimeFormatInfo.InvariantInfo.MonthNames;

            if (contact.Birthday != DateTime.MinValue)
            {
                //driver.FindElement(By.Name("bday")).FindElements(By.TagName("option"))[contact.Birthday.Day+2].Click();
                driver.FindElement(By.XPath($"//select[@name = 'bday']//option[@value = {contact.Birthday.Day}]")).Click();
                driver.FindElement(By.XPath($"//select[@name = 'bmonth']//option[@value = \"{monthNames[contact.Birthday.Month - 1]}\"]")).Click();
                Type(By.Name("byear"), contact.Birthday.Year.ToString());
            }

            if (contact.Anniversary != DateTime.MinValue)
            {
                driver.FindElement(By.XPath($"//select[@name = 'aday']//option[@value = {contact.Anniversary.Day}]")).Click();
                //driver.FindElement(By.XPath($"//select[@name = 'amonth']/option[@value = \"{monthNames[contact.Anniversary.Month - 1].ToLower()}\"]")).Click();
                driver.FindElement(By.Name("amonth")).FindElements(By.TagName("option"))[contact.Anniversary.Month].Click();
                Type(By.Name("ayear"), contact.Anniversary.Year.ToString());
            }

            Type(By.Name("address2"), contact.SecondaryAddress);
            Type(By.Name("phone2"), contact.SecondaryPhone);
            Type(By.Name("notes"), contact.Notes);
            return this;
        }

        public void InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return;
        }

        public void CheckContactPresent()
        {
            manager.Navigator.GoToHomePage();
            if (IsElementPresent(By.Name("entry")))
            {
                return;
            }

            ContactData contact = new ContactData("Denis");
            contact.Lastname = "Tazitdinov";

            Create(contact);
        }

        public void InitRemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactsList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();

                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[@name='entry']"));

                foreach (IWebElement element in elements)
                {
                    ICollection<IWebElement> columns = element.FindElements(By.TagName("td"));

                    contactCache.Add(new ContactData(columns.ElementAt(2).Text, columns.ElementAt(1).Text)
                    {
                        Id = columns.ElementAt(0).FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(contactCache);
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastname = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastname)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        public ContactData GetContactInformationFromForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitEditContact(index);

            return new ContactData(driver.FindElement(By.Name("firstname")).GetAttribute("value").Trim())
            {
                Middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value"),
                Lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value").Trim(),
                Nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value").Trim(),

                Company = driver.FindElement(By.Name("company")).GetAttribute("value").Trim(),
                Title = driver.FindElement(By.Name("title")).GetAttribute("value").Trim(),

                Address = driver.FindElement(By.Name("address")).GetAttribute("value").Trim(),
                HomePhone = driver.FindElement(By.Name("home")).GetAttribute("value").TrimStart().TrimEnd(),
                MobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value").Trim(),
                WorkPhone = driver.FindElement(By.Name("work")).GetAttribute("value").Trim(),
                FaxPhone = driver.FindElement(By.Name("fax")).GetAttribute("value").Trim(),

                Email = driver.FindElement(By.Name("email")).GetAttribute("value").Trim(),
                Email2 = driver.FindElement(By.Name("email2")).GetAttribute("value").Trim(),
                Email3 = driver.FindElement(By.Name("email3")).GetAttribute("value").Trim(),
                Homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value").Trim(),

                BirthdayDay = driver.FindElement(By.XPath("//select[@name=\"bday\"]/option[@selected=\"selected\"]")).GetAttribute("value"),
                BirthdayMonth = driver.FindElement(By.XPath("//select[@name=\"bmonth\"]/option[@selected=\"selected\"]")).GetAttribute("value"),
                BirthdayYear = driver.FindElement(By.Name("byear")).GetAttribute("value").Trim(),

                AnniversaryDay = driver.FindElement(By.XPath("//select[@name=\"aday\"]/option[@selected=\"selected\"]")).GetAttribute("value"),
                AnniversaryMonth = driver.FindElement(By.XPath("//select[@name=\"amonth\"]/option[@selected=\"selected\"]")).GetAttribute("value"),
                AnniversaryYear = driver.FindElement(By.Name("ayear")).GetAttribute("value").Trim(),

                SecondaryAddress = driver.FindElement(By.Name("address2")).GetAttribute("value").Trim(),
                SecondaryPhone = driver.FindElement(By.Name("phone2")).GetAttribute("value").Trim(),
                Notes = driver.FindElement(By.Name("notes")).GetAttribute("value").Trim(),
            };
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string searchResults = driver.FindElement(By.Id("search_count")).Text;
            return Int32.Parse(searchResults);
        }

        public string GetContactInformationFromDetail(int index)
        {
            manager.Navigator.GoToHomePage();
            GoToContactDetail(index);

            String contactDetail = driver.FindElement(By.Id("content")).Text;
            contactDetail = Regex.Replace(contactDetail, @"\s+", " ").Trim();
            
            return contactDetail;
        }

        public string GetContactInformationToString(ContactData contact)
        {
            string details = 
                $"{contact.FirstName} {contact.Middlename} {contact.Lastname}\n" +
                $"{contact.Nickname}\n" +
                $"{contact.Title}\n" +
                $"{contact.Company}\n" +
                $"{contact.Address}\n"+
                $"H: {contact.HomePhone}\n" +
                $"M: {contact.MobilePhone}\n" +
                $"W: {contact.WorkPhone}\n" +
                $"F: {contact.FaxPhone}\n" +
                $"{contact.Email}\n" +
                $"{contact.Email2}\n" +
                $"{contact.Email3}\n" +
                $"Homepage: " +
                $"{contact.Homepage}\n" +
                $"Birthday " +
                $"{contact.BirthdayDay}. ".TrimStart('.') +
                $"{contact.BirthdayMonth} " +
                $"{contact.BirthdayYear} " +
                $"{contact.Age}\n" +
                $"Anniversary " +
                $"{contact.AnniversaryDay}. ".TrimStart('.') +
                $"{contact.AnniversaryMonth} " +
                $"{contact.AnniversaryYear} " +
                $"{contact.YearsOfMarriage}\n" +
                $"{contact.SecondaryAddress}\n" +
                $"P: {contact.SecondaryPhone}\n" +
                $"{contact.Notes}";

            string pattern = @"(([HMWFP]: )|(Homepage: )|(Birthday )|(Anniversary ))$";
            details = Regex.Replace(details, pattern, "", RegexOptions.Multiline);
            details = Regex.Replace(details, @"\s+", " ");

            return details;
        }
    }
}


