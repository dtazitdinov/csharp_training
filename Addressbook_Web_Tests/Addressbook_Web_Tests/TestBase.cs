using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class TestBase
    {
        public IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseURL;
        protected static Random rndNumber = new Random((int)DateTime.Now.Ticks);

        protected HelperLogin helperLogin;
        protected HelperGroup helperGroup;
        protected HelperNavigate navigate;
        protected HelperContact helperContact;

        [SetUp]
        public void SetupTest()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"c:\Program Files\Mozilla Firefox\firefox.exe";
            options.UseLegacyImplementation = true;
            driver = new FirefoxDriver(options);
            baseURL = "http://localhost/";
            verificationErrors = new StringBuilder();

            helperLogin = new HelperLogin(driver);
            navigate = new HelperNavigate(driver, baseURL);
            helperGroup = new HelperGroup(driver);
            helperContact = new HelperContact(driver);
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }    
    }
}
