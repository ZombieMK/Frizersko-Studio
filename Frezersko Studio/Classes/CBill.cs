using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frezersko_Studio.Classes
{
    public class CBill
    {
        public string id { get; set; }
        public string note { set; get; }
        public float price { set; get; }
        public List<CPackage> packages { set; get; }
        public List<CService> services { set; get; }
        public DateTime date { set; get; }
        public CCustomer customer { set; get; }
        public bool inAll { set; get; }

        public CBill(string nID, string nNote, float nPrice, List<CPackage> nPackages, List<CService> nServices, DateTime nDate, CCustomer nCustomer) 
        {
            id = nID;
            note = nNote;
            price = nPrice;
            packages = nPackages;
            services = nServices;
            date = nDate;
            customer = nCustomer;
            inAll = false;
        }

        public override string ToString()
        {
            if (inAll)
                return date.ToShortDateString() + " (" + customer.ToString() + ")";
            else
                return date.ToString();
        }
    }
}
