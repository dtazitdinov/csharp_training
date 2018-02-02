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

        public static Random rndNumber = new Random((int)DateTime.Now.Ticks);

        public static int GenerateRandomNumber(int maxNum)
        {
            return rndNumber.Next(maxNum);
        }

        public static string GenerateRandomString(int max)
        {
            int lenght = rndNumber.Next(max);
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < lenght; i++)
            {
                builder.Append(Convert.ToChar(rndNumber.Next(223) + 32));
            }
            return builder.ToString().Replace("'"," ").Replace(@"\", " ").Replace(@"/"," ").Replace(@"<", " ");            
        }
    }
}
