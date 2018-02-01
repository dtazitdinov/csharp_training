using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public ContactData(string name)
        {
            FirstName = name;
        }

        public ContactData(string name, string lastname)
        {
            FirstName = name;
            Lastname = lastname;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }

        public string Company { get; set; }
        public string Title { get; set; }

        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string FaxPhone { get; set; }

        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Homepage { get; set; }

        public string BirthdayYear { get; set; }
        public string birthdayDay;
        public string BirthdayDay
        {
            get
            {
                if (birthdayDay == "0")
                {
                    return birthdayDay = "";
                }
                return birthdayDay;
            }
            set { birthdayDay = value; }
        }
        public string birthdayMonth;
        public string BirthdayMonth
        {
            get
            {
                if (birthdayMonth == "-")
                {
                    return birthdayMonth = "";
                }
                return birthdayMonth;
            }
            set { birthdayMonth = value; }
        }
        public DateTime birthday;
        public DateTime Birthday
        {
            get
            {
                if (birthday == DateTime.MinValue)
                {
                    birthday = GetDateTime(BirthdayDay, BirthdayMonth, BirthdayYear);
                }
                return birthday;
            }
            set
            {
                birthday = value;
            }
        }
        public string age;
        public string Age
        {

            get
            {
                return age = GetYearsFromTheDate(Birthday);
            }
            set
            {
                age = value;
            }

        }

        public string AnniversaryYear { get; set; }
        public string anniversaryDay;
        public string AnniversaryDay
        {
            get
            {
                if (anniversaryDay == "0")
                {
                    return anniversaryDay = "";
                }
                return anniversaryDay;
            }
            set { anniversaryDay = value; }
        }
        public string anniversaryMonth;
        public string AnniversaryMonth
        {
            get
            {
                if (anniversaryMonth == "-")
                {
                    return anniversaryMonth = "";
                }
                return anniversaryMonth;
            }
            set { anniversaryMonth = value; }
        }
        public DateTime anniversary;
        public DateTime Anniversary
        {

            get
            {
                if (anniversary == DateTime.MinValue)
                {
                    anniversary = GetDateTime(AnniversaryDay, AnniversaryMonth, AnniversaryYear);
                }
                return anniversary;
            }
            set
            {
                anniversary = value;
            }

        }
        public string yearsOfMarriage;
        public string YearsOfMarriage
        {

            get
            {
                return yearsOfMarriage = GetYearsFromTheDate(Anniversary);
            }
            set
            {
                yearsOfMarriage = value;
            }

        }

        public string SecondaryAddress { get; set; }
        public string SecondaryPhone { get; set; }
        public string Notes { get; set; }

        public string allPhones;
        public string AllPhones
        {
            get
            {
                if (allPhones != null || allPhones == "")
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone) + CleanUp(SecondaryPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string allEmails;
        public string AllEmails
        {
            get
            {
                if (allEmails != null || allEmails == "")
                {
                    return allEmails;
                }
                else
                {

                    return ((Email + "\r\n" + Email2).Trim() + "\r\n" + Email3).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        private string CleanUp(string phone)
        {
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
        }

        public int CompareTo(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (FirstName.CompareTo(other.FirstName) == 0)
            {
                return Lastname.CompareTo(other.Lastname);
            }

            return FirstName.CompareTo(other.FirstName);
        }

        public bool Equals(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (FirstName == other.FirstName)
            {
                return Lastname == other.Lastname;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode() + Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return "name = " + FirstName + ", Lastname = " + Lastname;
        }

        public DateTime GetDateTime(string day, string month, string year)
        {
            DateTime date = new DateTime();
            string[] monthNames = DateTimeFormatInfo.InvariantInfo.MonthNames;

            if (day != null && day != "")
            {
                date = date.AddDays(Int32.Parse(day) - 1);
            }

            if (month != null && month != "")
            {
                date = date.AddMonths(Array.IndexOf(monthNames, month));
            }

            Regex checkNums = new Regex(@"^\d+$"); // любые цифры

            if (checkNums.IsMatch(year))
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
