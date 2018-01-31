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

        public void GoToHomePage()
        {
            if (driver.Url == baseURL + ":8080/addressbook/")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + ":8080/addressbook/");
        }

        public void GoToGroupsPage()
        {
            if (driver.Url == ":8080/addressbook/group.php" && IsElementPresent(By.Name("new")))
            {
                return;
            }
            driver.FindElement(By.LinkText("groups")).Click();
        }

    }
}
