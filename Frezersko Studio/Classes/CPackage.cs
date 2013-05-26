using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frezersko_Studio.Classes
{
    public class CPackage
    {
        public string id { set; get; }
        public string name { set; get; }
        public float price { set; get; }
        public List<CService> services { set; get; }

        public CPackage(string nId, string nName, float nPrice, List<CService> nServices) 
        {
            id = nId;
            name = nName;
            price = nPrice;
            services = nServices;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
