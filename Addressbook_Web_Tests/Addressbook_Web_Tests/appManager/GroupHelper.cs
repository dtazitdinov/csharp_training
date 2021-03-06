﻿using System;
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

        public GroupHelper Edit(int index, GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(index);
            InitEditGroup();
            FillGroupForm(group);
            SubmitEditedGroup();
            ReturnToGroupPage();
            return this;
        }
        
        public GroupHelper Edit(string Id, GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(Id);
            InitEditGroup();
            FillGroupForm(group);
            SubmitEditedGroup();
            ReturnToGroupPage();
            return this;
        }

        public GroupHelper Remove(int index)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(index);
            RemoveGroup();
            ReturnToGroupPage();
            return this;
        }

        public GroupHelper Remove(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(group.Id);
            RemoveGroup();
            ReturnToGroupPage();
            return this;
        }

        public int GetGroupsCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        public void CheckGroupPresent()
        {
            manager.Navigator.GoToGroupsPage();
            if (IsElementPresent(By.XPath("//span")))
            {
                return;
            }

            GroupData group = new GroupData("group #" + TestBase.GenerateRandomNumber(1000));
            group.Header = "header";
            group.Footer = "footer";

            Create(group);
        }

        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public GroupHelper SelectGroup(string Id)
        {
            driver.FindElement(By.XPath($"(//input[@name='selected[]' and @value='{Id}'])")).Click();
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
            groupCache = null;
            return this;
        }

        public GroupHelper SubmitEditedGroup()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper ReturnToGroupPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        private List<GroupData> groupCache = null;

        public List<GroupData> GetGroupsList()
        {
            if (groupCache == null)
            {
                groupCache = new List<GroupData>();
                manager.Navigator.GoToGroupsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));

                foreach (IWebElement element in elements)
                {
                    groupCache.Add(new GroupData(null)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }

                string allGroupNames = driver.FindElement(By.CssSelector("div#content form")).Text;
                string[] groupNames = allGroupNames.Split('\n');
                int shift = groupCache.Count - groupNames.Length;
                for (int i = 0; i < groupCache.Count; i++)
                {
                    if (i < shift)
                    {
                        groupCache[i].Name = "";
                    }
                    else
                    {
                        groupCache[i].Name = groupNames[i-shift].Trim();
                    }
                }
            }
            return new List<GroupData>(groupCache);
        }

        public GroupData GetGroupWithContacts(List<GroupData> groups)
        {
            GroupData group = null;
            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].GetContacts().Count > 0)
                {
                    group = groups[i];
                    break;
                }
            }

            if (group == null)
            {
                ContactData contact = ContactData.GetAllFromDb().First();
                manager.Contacts.AddToGroup(contact, groups[0]);
                group = groups[0];
            }

            return group;
        }

        public GroupData GetFreeGroup(List<GroupData> groups)
        {
            GroupData freeGroup = null;

            foreach (GroupData group in groups)
            {
                List<ContactData> contactList = group.GetContacts();
                if (ContactData.GetAllFromDb().Except(contactList).Count() > 0)
                {
                    freeGroup = group;
                    break;
                }
            }

            if (freeGroup == null)
            {
                manager.Contacts.Create(new ContactData("Han", "Solo"));
                freeGroup = GroupData.GetAllFromDb().First();
            }

            return freeGroup;
        }
    }
}

