using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        public GroupData()
        {
        }

        public GroupData(string name)
        {
            Name = name;
        }

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        private string name;
        [Column(Name = "group_name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                if (name != null)
                {
                    name = name.Trim(' ');
                }

            }
        }

        [Column(Name = "group_header")]
        public string Header { get; set; }

        [Column(Name = "group_footer")]
        public string Footer { get; set; }

        public int CompareTo(GroupData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public bool Equals(GroupData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"name = {Name} \n" +
                   $"header = {Header} \n" +
                   $"footer = {Footer}";
        }

        public static List<GroupData> GetAllFromDb()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from g in db.Groups
                        orderby g.Id
                        select g).ToList();
            }
        }

        public List<ContactData> GetContacts()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from c in db.Contacts
                        from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id && c.Deprecated < DateTime.MinValue)
                        select c).Distinct().ToList();
            }

        }        
    }
}
