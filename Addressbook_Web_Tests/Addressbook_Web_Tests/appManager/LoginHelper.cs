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
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Login(AccountData account)
        {
            if (LoggedIn())
            {
                if (LoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            Type(By.Name("user"), account.Username);
            Type(By.Name("pass"), account.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            return;
        }

        public bool LoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        public bool LoggedIn(AccountData account)
        {
            return LoggedIn()
                && GetLoggetUserName() == account.Username;
        }

        private string GetLoggetUserName()
        {
            string text = driver.FindElement(By.XPath("//form[@name=\"logout\"]/b")).Text;
            return text.Substring(1, text.Length - 2);
        }

        public void Logout()
        {
            if (IsElementPresent(By.LinkText("Logout")))
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
        }
    }
}
