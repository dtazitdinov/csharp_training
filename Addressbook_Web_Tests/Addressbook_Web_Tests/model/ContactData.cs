using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string name = "";
        private string lastname = "";
        private int id = 0;

        public ContactData(string name)
        {
            Name = name;
        }

        public ContactData(string name, string lastname)
        {
            Name = name;
            Lastname = lastname;
        }

        public string Name { get => name; set => name = value; }

        public string Lastname { get => lastname; set => lastname = value; }

        public int Id { get => id; set => id = value; }

        public int CompareTo(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (Name.CompareTo(other.Name) == 0)
            {
                return Lastname.CompareTo(other.Lastname);
            }

            return Name.CompareTo(other.Name);
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

            if (Name == other.Name)
            {
                return Lastname == other.Lastname;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return "name = " + Name + ", Lastname = " + Lastname;
        }
    }
}
