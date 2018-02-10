using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "address_in_groups")]
    public class GroupContactRelation
    {
        public GroupContactRelation()
        {
        }

        [Column(Name = "group_id")]
        public string GroupId { get; set; }

        [Column(Name = "id")]
        public string ContactId { get; set; }

        [Column(Name = "deprecated")]
        public DateTime Deprecated { get; set; }

        public static List<GroupContactRelation> GetAllFromDb()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from gcr in db.GCR
                        where gcr.Deprecated < DateTime.MinValue
                        select gcr).ToList();
            }
        }
    }
}
