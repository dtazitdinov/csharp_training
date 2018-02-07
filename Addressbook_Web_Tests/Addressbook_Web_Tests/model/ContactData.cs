using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public ContactData()
        {
        }

        public ContactData(string name)
        {
            FirstName = name;
        }

        public ContactData(string name, string lastname)
        {
            FirstName = name;
            Lastname = lastname;
        }


        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "firstName")]
        public string FirstName { get; set; }

        [Column(Name = "middlename")]
        public string Middlename { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "nickname")]
        public string Nickname { get; set; }

        [Column(Name = "company")]
        public string Company { get; set; }

        [Column(Name = "title")]
        public string Title { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "fax")]
        public string FaxPhone { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "homepage")]
        public string Homepage { get; set; }

        [Column(Name = "byear")]
        public string BirthdayYear { get; set; }

        private string birthdayMonth;
        [Column(Name = "bmonth")]
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
            set
            {
                if (value != null)
                {
                    birthdayMonth = value;
                    birthdayMonth = char.ToUpper(birthdayMonth[0]) + birthdayMonth.Substring(1);
                }
                else
                {
                    birthdayMonth = "-";
                }
            }
        }

        private string birthdayDay;
        [Column(Name = "bday")]
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

        private DateTime birthday = new DateTime();
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

        private string age;
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

        [Column(Name = "ayear")]
        public string AnniversaryYear { get; set; }

        private string anniversaryMonth;
        [Column(Name = "amonth")]
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
            set
            {
                anniversaryMonth = value;
                if (value != null)
                {
                    anniversaryMonth = char.ToUpper(anniversaryMonth[0]) + anniversaryMonth.Substring(1);
                }
            }
        }

        private string anniversaryDay;
        [Column(Name = "aday")]
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

        private DateTime anniversary = new DateTime();
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

        private string yearsOfMarriage;
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

        [Column(Name = "address2")]
        public string SecondaryAddress { get; set; }

        [Column(Name = "phone2")]
        public string SecondaryPhone { get; set; }

        [Column(Name = "notes")]
        public string Notes { get; set; }

        //string deprecated;
        //[Column(Name = "deprecated")]
        public string Deprecated { get; set; }
/*        {
            get
            {
                return deprecated;
            }
            set
            {
                deprecated = DateTime.Parse(value.ToString());
            }
        }*/

        private string allPhones;
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
                    return ((CleanUp(HomePhone) + "\r\n" + CleanUp(MobilePhone)).Trim() + "\r\n" + (CleanUp(WorkPhone) + "\r\n" + CleanUp(SecondaryPhone)).Trim()).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        private string allEmails;
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
            phone = Regex.Replace(phone, @"[-() ]", "");
            return phone;
            //return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
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
            if (age > 149 || date.Year > 2200)
            {
                return "";
            }

            return $"({age.ToString()})";
        }

        public static List<ContactData> GetAllFromDb()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from g in db.Contacts
                            
                        select g).ToList();
            }
        }//where g.Deprecated > DateTime.MinValue 
    }
}
