using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData
    {
        private string name = "";
        private string lastname = "";
        private int id = 0;

        public ContactData(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Lastname
        {
            get => lastname;
            set => lastname = value;
        }

        public int ID
        {
            get => id;
            set => id = value;
        }
    }
}
