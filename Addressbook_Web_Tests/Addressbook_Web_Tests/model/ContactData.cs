using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Birthday_year { get; set; }
        public string Birthday_day { get; set; }
        public string BirthdayMonth { get; set; }
        public string Anniversary_year { get; set; }
        public string Anniversary_day { get; set; }
        public string Anniversary_month { get; set; }

        public string SecondaryAddress { get; set; }
        public string Secondary_phone { get; set; }
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
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        private string CleanUp(string phone)
        {
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
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
                    return (Email + "\r\n" + Email2 + "\r\n" + Email3).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
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
    }
}
