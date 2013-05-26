using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frezersko_Studio.Classes
{
    public class CService
    {
        public string id { set; get; }
        public string name { set; get; }
        public float price { set; get; }
        public CEmployee employee { set; get; }

        public CService(string nId, string nName, float nPrice) 
        {
            id = nId;
            name = nName;
            price = nPrice;
            employee = null;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
