using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitNewGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupPage();
            return this;
        }

        public GroupHelper Edit(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            CheckGroupPresent();
            SelectGroup(1);
            InitEditGroup();
            FillGroupForm(group);
            SubmitEditedGroup();
            ReturnToGroupPage();
            return this;
        }

        public GroupHelper Remove()
        {
            manager.Navigator.GoToGroupsPage();
            CheckGroupPresent();
            SelectGroup(1);
            RemoveGroup();
            ReturnToGroupPage();
            return this;
        }

        public void CheckGroupPresent()
        {
            if (IsElementPresent(By.XPath("//span")))
            {
                return;
            }

            GroupData group = new GroupData("group #" + OtherMetods.RandomNumber(1000));
            group.Header = "header";
            group.Footer = "footer";

            Create(group);
        }

        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public GroupHelper SelectGroupByName(GroupData group)
        {
            driver.FindElement(By.XPath("//span[text()=\"" + group.Name + "\"]/input")).Click();
            return this;
        }

        public GroupHelper InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper InitEditGroup()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public GroupHelper SubmitEditedGroup()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public GroupHelper ReturnToGroupPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }
    }
}

