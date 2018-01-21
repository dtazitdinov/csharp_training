using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

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
            driver.FindElement(By.XPath("//td[text()=\"" + contact.Name + "\"]/..//input")).Click();
        }

        public void SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
        }

        public void SubmitEditedContact()
        {
            driver.FindElement(By.Name("update")).Click();
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
            return this;
        }

        public ContactHelper FillForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Name);
            Type(By.Name("lastname"), contact.Lastname);
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
            return this;
        }

        public List<ContactData> GetContactsList()
        {
            List<ContactData> contacts = new List<ContactData>();

            ICollection<IWebElement> names = driver.FindElements(By.XPath("//tr[@name=\"entry\"]/td[3]"));
            ICollection<IWebElement> lastnames = driver.FindElements(By.XPath("//tr[@name=\"entry\"]/td[2]"));

            for ( int i = 0; i < names.Count; i++ )
            {
                contacts.Add(new ContactData(names.ElementAt(i).Text, lastnames.ElementAt(i).Text));         
            }

            return contacts;
        }
    }
}
