using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private static ThreadLocal<ApplicationManager> appManager = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            FirefoxOptions options = new FirefoxOptions();
            //options.BrowserExecutableLocation = @"c:\Program Files\Mozilla Firefox\firefox.exe";
            options.BrowserExecutableLocation = @"c:\Program Files (x86)\Mozilla Firefox ESR\firefox.exe";
            options.UseLegacyImplementation = true;
            driver = new FirefoxDriver(options);
            baseURL = "http://localhost";
            verificationErrors = new StringBuilder();

            loginHelper = new LoginHelper(this);
            navigateHelper = new NavigateHelper(this, baseURL);
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }

        public static ApplicationManager GetThreadInstance()
        {
            if (! appManager.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigator.GoToHomePage();
                appManager.Value = newInstance;                
            }
            return appManager.Value;
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch(Exception)
            {
                //Ignor errors if unable to close the browser
            }
        }

        public IWebDriver Driver
        {
            get => driver;
        }

        public LoginHelper Auth
        {
            get => loginHelper;
        }

        public NavigateHelper Navigator
        {
            get => navigateHelper;
        }

        public GroupHelper Groups
        {
            get => groupHelper;
        }

        public ContactHelper Contacts
        {
            get => contactHelper;
        }
    }
}
