using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Collections.Generic;
using NUnit.Framework;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ResearchTests
    {
        public DateTime Birthday;
        public string BirthdayYear { get; set; }
        public string BirthdayDay { get; set; }
        public string BirthdayMonth { get; set; }
        public string AnniversaryYear { get; set; }
        public string AnniversaryDay { get; set; }
        public string AnniversaryMonth { get; set; }
        public string Age { get; set; }

        [Test]
        public void ResearchTest()
        {
            BirthdayYear = "";
            BirthdayDay = "0";
            BirthdayMonth = "-";

            
            if (Birthday == null)
            {
                Birthday = GetDate(BirthdayDay, BirthdayMonth, BirthdayYear);
                Age = GetAge(Birthday);
            }

            System.Console.Out.WriteLine(string.Format("Birthday {0}{1}{2}", 
                BirthdayDay.Equals("0") ? "" : (BirthdayDay + ". "),
                BirthdayMonth.Equals("-") ? "" : (BirthdayMonth + ' '),
                BirthdayYear + " ", 
                Age).Trim());
            //"Birthday 18. October 1988 (29)"

            

        }

        public DateTime GetDate(string day, string month, string year)
        {
            DateTime date = new DateTime();
            string[] monthNames = DateTimeFormatInfo.InvariantInfo.MonthNames;

            if (day != null || Int32.Parse(day) != 0)
            {
                date.AddDays(Int32.Parse(day) - 1);
            }

            if (month != null || month != "")
            {
                date.AddMonths(Array.IndexOf(monthNames, month));
            }

            Regex checkNums = new Regex(@"^\d+$"); // любые цифры

            if (checkNums.IsMatch(year))
            {
                date.AddYears(Int32.Parse(year) - 1);
            }
            return date;
        }

        public string GetAge(DateTime date)
        {
            int age = DateTime.Today.Year - date.Year;
            if (Birthday > DateTime.Today.AddYears(-age))
            {
                age--;
            }
            if (age > 149)
            {
                return "";
            }

            return string.Format("({0})",age.ToString());
        }

    }
}
