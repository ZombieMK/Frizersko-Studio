using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frezersko_Studio.Classes
{
    public class CEmployee
    {
        public string id { set; get; }
        public string name { set; get; }
        public string lastName { set; get; }
        public string address { set; get; }
        public string phone { set; get; }
        public DateTime birthday { set; get; }
        public DateTime dateOfEmploymend { set; get; }
        public bool fired;

        public CEmployee(string nId, string nName, string nLastName, string nAddress, string nPhone, DateTime nBirthday, DateTime nDateOfEmploymend, bool nFired) 
        {
            id = nId;
            name = nName;
            lastName = nLastName;
            address = nAddress;
            phone = nPhone;
            birthday = nBirthday;
            dateOfEmploymend = nDateOfEmploymend;
            fired = nFired;
        }

        public override string ToString()
        {
            return name + " " + lastName;
        }
    }
}
