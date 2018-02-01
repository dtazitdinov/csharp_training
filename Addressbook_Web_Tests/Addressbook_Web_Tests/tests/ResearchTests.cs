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
        public string Age { get; set; }

        public DateTime Anniversary;
        public string AnniversaryYear { get; set; }
        public string AnniversaryDay { get; set; }
        public string AnniversaryMonth { get; set; }
        public string YearsOfMarriage { get; set; }


        [Test]
        public void ResearchTest()
        {            
            BirthdayDay = "18";
            BirthdayMonth = "October";
            BirthdayYear = "1988";

            if (Birthday == DateTime.MinValue)
            {
                Birthday = GetDateTime(BirthdayDay, BirthdayMonth, BirthdayYear);
                Age = GetYearsFromTheDate(Birthday);                    
            }

            if (Anniversary == DateTime.MinValue)
            {
                Anniversary = GetDateTime(AnniversaryDay, AnniversaryMonth, AnniversaryYear);
                YearsOfMarriage = GetYearsFromTheDate(Birthday);
            }

            System.Console.Out.WriteLine(string.Format("Birthday {0}{1}{2}{3}", 
                BirthdayDay.Equals("0") ? "" : (BirthdayDay + ". "),
                BirthdayMonth.Equals("-") ? "" : (BirthdayMonth + ' '),
                BirthdayYear + " ", 
                Age).Trim());
            //"Birthday 18. October 1988 (29)"

            

        }

        public DateTime GetDateTime(string day, string month, string year)
        {
            DateTime date = new DateTime();
            string[] monthNames = DateTimeFormatInfo.InvariantInfo.MonthNames;

            if (day != null && day != "0")
            {
                date = date.AddDays(Int32.Parse(day) - 1);
            }

            if (month != null && month != "-")
            {
                date = date.AddMonths(Array.IndexOf(monthNames, month));
            }

            Regex checkNums = new Regex(@"^\d+$"); // любые цифры
            if (year != null && checkNums.IsMatch(year))
            {
                date = date.AddYears(Int32.Parse(year) - 1);                
            }
            return date;
        }

        public string GetYearsFromTheDate(DateTime date)
        {
            int age = DateTime.Today.Year - date.Year;
            if (date > DateTime.Today.AddYears(-age))
            {
                age--;
            }
            if (age > 149)
            {
                return "";
            }

            return $"({age.ToString()})";
        }

    }
}
