using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

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

        public ContactHelper Edit(int index, ContactData contact)
        {
            SelectContact(index);
            InitEditContact();
            FillForm(contact);
            SubmitEditedContact();
            ReturnHomePage();
            return this;
        }

        public int GetContactsCount()
        {
            return driver.FindElements(By.XPath("//tr[@name='entry']")).Count;
        }

        public ContactHelper Remove(int index)
        {
            SelectContact(index);
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

        public void SelectContactByName(ContactData contact)
        {
            driver.FindElement(By.XPath("//td[text()=\"" + contact.FirstName + "\"]/..//input")).Click();
        }

        public void SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
        }

        public void SubmitEditedContact()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
        }

        public ContactHelper InitEditContact()
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
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
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            return this;
        }

        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
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

        public ContactHelper InitRemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
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

                    contactCache.Add(new ContactData(columns.ElementAt(2).Text, columns.ElementAt(1).Text) {
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
            int f = 1 + 2;

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
            SelectContact(index);
            InitEditContact();

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastname)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };

        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string searchResults = driver.FindElement(By.Id("search_count")).Text;
            return Int32.Parse(searchResults);
        }



        /*public ContactData GetContactInformationFromForm(int index)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(index);
            InitEditContact();

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string faxPhone = driver.FindElement(By.Name("fax")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");
            
            string birthday_day = driver.FindElement(By.XPath("//select[@name=\"bday\"]/option[@selected=\"selected\"]")).GetAttribute("value");
            string birthdayMonth = driver.FindElement(By.XPath("//select[@name=\"bmonth\"]/option[@selected=\"selected\"]")).GetAttribute("value");
            string birthday_year = driver.FindElement(By.Name("byear")).GetAttribute("value");
            string Anniversary_day = driver.FindElement(By.XPath("//select[@name=\"aday\"]/option[@selected=\"selected\"]")).GetAttribute("value");
            string Anniversary_month = driver.FindElement(By.XPath("//select[@name=\"amonth\"]/option[@selected=\"selected\"]")).GetAttribute("value");
            string Anniversary_year = driver.FindElement(By.Name("ayear")).GetAttribute("value");

            string secondaryAddress = driver.FindElement(By.Name("address2")).GetAttribute("value");
            string secondary_phone = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");

            return new ContactData(firstName, lastname)
            {
                Middlename = middlename,
                Nickname = nickname,
                Company = company,
                Title = title,
                Address = address,
                Home_phone = home_phone,
                Mobile_phone = mobile_phone,
                Work_phone = work_phone,
                Fax_phone = fax_phone,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                Homepage = homepage,
                Birthday_day = birthday_day,
                BirthdayMonth = birthdayMonth,
                Birthday_year = birthday_year,
                Anniversary_day = Anniversary_day,
                Anniversary_month = Anniversary_month,
                Anniversary_year = Anniversary_year,
                SecondaryAddress= secondaryAddress,
                Secondary_phone = secondary_phone,
                Notes = notes
            };
        }*/
    }
}


