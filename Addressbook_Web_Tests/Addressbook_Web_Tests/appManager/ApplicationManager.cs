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
    public class ApplicationManager
    {
        public IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseURL;
       

        protected LoginHelper loginHelper;
        protected GroupHelper groupHelper;
        protected NavigateHelper navigateHelper;
        protected ContactHelper contactHelper;

        public ApplicationManager()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"c:\Program Files\Mozilla Firefox\firefox.exe";
            options.UseLegacyImplementation = true;
            driver = new FirefoxDriver(options);
            baseURL = "http://localhost/";
            verificationErrors = new StringBuilder();

            loginHelper = new LoginHelper(this);
            navigateHelper = new NavigateHelper(this, baseURL);
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }

        public IWebDriver Driver
        {
            get => driver;
        }

        public void Stop()
        {
            try
            {
                Driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public LoginHelper Auth
        {
            get => loginHelper;
        }

        public NavigateHelper Navigator
        {
            get => navigateHelper;
        }

        public GroupHelper Group
        {
            get => groupHelper;
        }

        public ContactHelper Contact
        {
            get => contactHelper;
        }
    }
}
