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
            string detail = "Denis Dinislan Tazitdinov\n\n\nOne Inc\nKurchatova 25A, 47\n\nH: \nM: \nW: \nF: 444 44 44\n\n\n3333@ccc.com\nHomepage: \nBirthday 18. October 1988 (29)\nAnniversary 30. March 2014 (3)\n\nP: 555-55-55\nNotes her";


            string pattern = @"(([HMWFP]: )|(Homepage: )|(Birthday )|(Anniversary ))$";
            detail = Regex.Replace(detail, pattern, "", RegexOptions.Multiline);

            //detail = Regex.Replace(detail, @"\s+", " ");

            System.Console.Out.WriteLine(detail);


        }
    }
}
