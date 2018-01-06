using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class TestBase
    {
        protected ApplicationManager appManager;

        [SetUp]
        public void SetupApplicationManager()
        {
            appManager = ApplicationManager.GetThreadInstance();
        }  
    }
}
