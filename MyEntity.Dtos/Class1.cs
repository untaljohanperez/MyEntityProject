using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MyEntity.Dtos
{
    //[DataContract(Namespace = "http://www.gooogle.com")]
    public class Bill
    {
      //  [DataMember]
        public int ID { get; set; }
        //public virtual ICollection<Seller> Sellers { get; set; }
        public string Customer { get; set; }
        //public virtual ICollection<Detail> Details { get; set; }
        public Bill() { }
    }
}
