using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests

{
    class ContactData
    {
        private string name = "";
        private string lastname = "";

        public ContactData(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        public string Lastname { get => lastname; set => lastname = value; }
    }
}
