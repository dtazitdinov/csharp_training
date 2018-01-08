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

        public ContactHelper Edit(ContactData contact)
        {
            SelectContact(1);
            InitEditContact();
            FillForm(contact);
            SubmitEditedContact();
            ReturnHomePage();
            return this;
        }

        public ContactHelper Remove()
        {
            SelectContact(1);
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
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
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
    }
}
