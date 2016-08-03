using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEntity.Model
{
    public class Seller
    {
        public int SellerID { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        
        public virtual ICollection<Bill> Bills { get; set; }

    }
}