using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEntity.Model
{
    public class Detail
    {
        public int ID { get; set; }
        public int BillID { get; set; }
        public string Product { get; set; }
        public int Qty { get; set; }
    }
}