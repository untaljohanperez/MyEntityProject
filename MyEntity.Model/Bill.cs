using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEntity.Model
{
    public class Bill
    {
        public int ID { get; set; }
        public virtual ICollection<Seller> Sellers { get; set; }
        public string Customer { get; set; }
        public virtual ICollection<Detail> Details {get; set;}
        public Bill() { }
    }
}