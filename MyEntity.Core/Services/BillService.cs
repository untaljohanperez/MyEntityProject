using Data.UnitOfWork;
using MyEntity.Data.Context;
using MyEntity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEntity.Core
{
    public class BillService
    {
        protected IUnitOfWork UnitOfWork;

        public BillService()
        {
            UnitOfWork = new UnitOfWork(new MyEntityContext());
        }
        public IEnumerable<Bill> GetAll()
        {
            return UnitOfWork.Bills.GetAll();    
        }
        public void init()
        {
            //Seller seller1 = new Seller()
            //{
            //    Name = "Smith",
            //    Birthday = new DateTime(1997, 7, 8),
            //};
            //Seller seller2 = new Seller()
            //{
            //    Name = "Anderson",
            //    Birthday = new DateTime(2000, 10, 12),
            //};
            //Bill bill = new Bill() { Customer = "Johan Perez", Sellers = new List<Seller>() { seller1, seller2 } };
            //Bill bill2 = new Bill() { Customer = "Kevin Ortiz", Sellers = new List<Seller>() { seller2 } };

            //Detail detail11 = new Detail() { Product = "Aguacate", Qty = 5 };
            //Detail detail12 = new Detail() { Product = "Salsa", Qty = 3 };
            //Detail detail21 = new Detail() { Product = "Pajarilla", Qty = 1 };
            //Detail detail22 = new Detail() { Product = "Tamal frances", Qty = 6 };

            //bill.Details = new List<Detail>() { detail11, detail12 };

            //bill2.Details = new List<Detail>() { detail21, detail22 };

            //db.Bills.Add(bill);
            //db.Bills.Add(bill2);

            //db.Details.Add(detail11);
            //db.Details.Add(detail12);
            //db.Details.Add(detail21);
            //db.Details.Add(detail22);

            //db.Sellers.Add(seller1);
            //db.Sellers.Add(seller2);
            //db.SaveChanges();

            //var result2 = db.Bills
            //              .Include(b => b.Sellers)
            //              .Join(db.Details, b => b.ID, det => det.BillID,
            //                    (b, det) => new { Bill = b, Detail = det });


            //var result3 = result2
            //                .Select(s => s.Bill)
            //                .GroupBy(s => s.ID)
            //                .Select(s => s.FirstOrDefault());


            //var result = from b in db.Bills
            //             from s in b.Sellers
            //                 //from det in b.Details
            //             join det in db.Details on b.ID equals det.BillID
            //             select new { Bill = b, det };


            //var result2 = result.GroupBy(s => s.Bill.ID).Select(s => s.FirstOrDefault());

            //var resultJson =
            //    JsonConvert
            //        .SerializeObject(result2, Formatting.Indented,
            //                                new JsonSerializerSettings()
            //                                {
            //                                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //                                });
            //return resultJson;
        }
    }
}
