using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frezersko_Studio.Classes
{
    public class CCustomer
    {
        public string id { set; get; }
        public string name { set; get; }
        public string lastName { set; get; }
        public string address { set; get; }
        public string phone { set; get; }
        public DateTime birthday { set; get; }

        public CCustomer(string nId, string nName, string nLastName, string nAddress, string nPhone, DateTime nBirthday) 
        {
            id = nId;
            name = nName;
            lastName = nLastName;
            address = nAddress;
            phone = nPhone;
            birthday = nBirthday;
        }

        public override string ToString()
        {
            return name + " " + lastName;
        }
    }
}
