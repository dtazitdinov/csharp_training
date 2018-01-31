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

        public string GetContactInformationToString(ContactData contact)
        {
            string ContactInformation = 
                contact.FirstName + " " + contact.Middlename + " " + contact.Lastname + "\r\n" +
                contact.Nickname + "\r\n" +
                contact.Title + "\r\n" +
                contact.Company + "\r\n" +
                contact.Address + "\r\n" +
                "\r\n" +
                "H:" + contact.HomePhone + "\r\n" +
                "M:" + contact.MobilePhone + "\r\n" +
                "W:" + contact.WorkPhone + "\r\n" +
                "F:" + contact.FaxPhone + "\r\n" +
                "\r\n" +
                contact.Email + "\r\n" +
                contact.Email2 + "\r\n" +
                contact.Email3 + "\r\n" +
                "Homepage:\r\n" +
                contact.Homepage + "\r\n" +
                "\r\n" +
                "Birthday "+ contact.Birthday + "\r\n" +
                "Anniversary" + contact.Anniversary + "\r\n" +
                "\r\n" +
                contact.SecondaryAddress + "\r\n" +
                "\r\n" +
                "P:" + contact.SecondaryPhone + "\r\n" +
                "\r\n" +
                contact.Notes;


            throw new NotImplementedException();
        }

        public ContactHelper Edit(int index, ContactData contact)
        {
            InitEditContact(index);
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

        public void InitEditContact(int index)
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
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
            Type(By.Name("nickname"),contact.Nickname);

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

            //driver.FindElement(By.Name("bday")).Click();
            //string path = string.Format(@"option[value='{0}']", contact.Birthday.Day);
            //driver.FindElement(By.Name("bday")).FindElements(By.TagName("option"))[contact.Birthday.Day+2].Click();

            string[] monthNames = DateTimeFormatInfo.InvariantInfo.MonthNames;

            if (contact.Birthday != DateTime.MinValue)
            {
                driver.FindElement(By.XPath($"//select[@name = 'bday']//option[@value = {contact.Birthday.Day}]")).Click();
                driver.FindElement(By.XPath($"//select[@name = 'bmonth']//option[@value = \"{monthNames[contact.Birthday.Month - 1]}\"]")).Click();
                Type(By.Name("byear"), contact.Birthday.Year.ToString());
            }

            if (contact.Anniversary != DateTime.MinValue)
            {
                driver.FindElement(By.XPath($"//select[@name = 'aday']//option[@value = {contact.Anniversary.Day}]")).Click();
                //driver.FindElement(By.XPath($"//select[@name = 'amonth']/option[@value = \"{monthNames[contact.Anniversary.Month - 1].ToLower()}\"]")).Click();
                driver.FindElement(By.Name("amonth")).FindElements(By.TagName("option"))[contact.Anniversary.Month + 2].Click(); /*.ToLower()}*/
                Type(By.Name("ayear"), contact.Anniversary.Year.ToString());
            }

            Type(By.Name("address2"), contact.SecondaryAddress);
            Type(By.Name("phone2"), contact.SecondaryPhone);
            Type(By.Name("notes"), contact.Notes);
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

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value").Trim();
            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value").Trim();
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");

            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");

            string address = driver.FindElement(By.Name("address")).GetAttribute("value").Trim();
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value").Trim();
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value").Trim();
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value").Trim();
            string faxPhone = driver.FindElement(By.Name("fax")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value").Trim();
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value").Trim();
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value").Trim();
            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            string birthdayDay = driver.FindElement(By.XPath("//select[@name=\"bday\"]/option[@selected=\"selected\"]")).GetAttribute("value");
            string birthdayMonth = driver.FindElement(By.XPath("//select[@name=\"bmonth\"]/option[@selected=\"selected\"]")).GetAttribute("value");
            string birthdayYear = driver.FindElement(By.Name("byear")).GetAttribute("value").Trim();

            string AnniversaryDay = driver.FindElement(By.XPath("//select[@name=\"aday\"]/option[@selected=\"selected\"]")).GetAttribute("value");
            string AnniversaryMonth = driver.FindElement(By.XPath("//select[@name=\"amonth\"]/option[@selected=\"selected\"]")).GetAttribute("value");
            string AnniversaryYear = driver.FindElement(By.Name("ayear")).GetAttribute("value").Trim();

            string secondaryAddress = driver.FindElement(By.Name("address2")).GetAttribute("value").Trim();
            string secondaryPhone = driver.FindElement(By.Name("phone2")).GetAttribute("value").Trim();
            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value").Trim();

            return new ContactData(firstName, lastname)
            {
                Middlename = middlename,
                Nickname = nickname,

                Company = company,
                Title = title,

                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                FaxPhone = faxPhone,

                Email = email,
                Email2 = email2,
                Email3 = email3,
                Homepage = homepage,

                BirthdayDay = birthdayDay,
                BirthdayMonth = birthdayMonth,
                BirthdayYear = birthdayYear,

                AnniversaryDay = AnniversaryDay,
                AnniversaryMonth = AnniversaryMonth,
                AnniversaryYear = AnniversaryYear,

                SecondaryAddress = secondaryAddress,
                SecondaryPhone = secondaryPhone,
                Notes = notes
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
            return contactDetail;
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


