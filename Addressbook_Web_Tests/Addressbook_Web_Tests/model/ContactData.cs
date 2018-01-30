using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

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

        public DateTime birthday;
        public DateTime Birthday { get; set; }
        /*{
            get
            {
                if (birthday != null)
                {
                    return birthday;
                }
                else
                {
                    if (BirthdayYear != "")
                    {
                        int year = Int32.Parse(BirthdayYear);

                        if (BirthdayDay == "")
                        {
                            int day = 1;
                        }
                        else
                        {
                            int day = Int32.Parse(BirthdayDay);
                        }
                        string[] monthNames = DateTimeFormatInfo.InvariantInfo.MonthNames;
                        if (BirthdayMonth == "-")
                        {
                            int month = 1;
                        }
                        else
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                if (BirthdayMonth == monthNames[i])
                                {
                                    int month = i + 1;
                                }
                            }
                        }

                        string date = string.Format("{0}.{1}.{2}", BirthdayDay, BirthdayMonth, BirthdayYear);
                        birthday = new DateTime(1988, month, day);
                    }
                }
                return birthday;
            }
            set
            {
                birthday = value;
            }
        }*/
        public string BirthdayYear { get; set; }
        public string BirthdayDay { get; set; }
        public string BirthdayMonth { get; set; }
        public DateTime Anniversary { get; set; }
        public string AnniversaryYear { get; set; }
        public string AnniversaryDay { get; set; }
        public string AnniversaryMonth { get; set; }
        
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
                    
                    return ((Email + "\r\n" + Email2).Trim() + "\r\n" + Email3).Trim();
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
