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
    public class NavigateHelper : HelperBase
    {
        public string baseURL;

        public NavigateHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public NavigateHelper GoToHomePage()
        {
            driver.Navigate().GoToUrl(baseURL + "addressbook/");
            return this;
        }

        public NavigateHelper GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
            return this;
        }

    }
}
