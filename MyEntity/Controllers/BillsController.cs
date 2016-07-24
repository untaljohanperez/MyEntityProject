using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEntity.Models;
using Newtonsoft.Json;

namespace MyEntity.Controllers
{
    public class BillsController : Controller
    {
        private MyEntityContext db = new MyEntityContext();

        // GET: Bills
        public Object Index()
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

            var result = from b in db.Bills
                         from s in b.Sellers
                         //from det in b.Details
                         join det in db.Details on b.ID equals det.BillID
                         select new { Bill = b, det};

            
            var result2 = result.GroupBy(s => s.Bill.ID).Select(s => s.FirstOrDefault());

            var resultJson = 
                JsonConvert
                    .SerializeObject(result2, Formatting.Indented,
                                            new JsonSerializerSettings()
                                            {
                                                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                            });
            return resultJson;
        }

        // GET: Bills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // GET: Bills/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Seller,Customer")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bill);
        }

        // GET: Bills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Seller,Customer")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bill);
        }

        // GET: Bills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
