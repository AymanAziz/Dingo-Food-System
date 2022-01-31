using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SDDFOS2021V1.Models;

namespace SDDFOS2021V1.Controllers
{
    public class OrderTrackingsController : Controller
    {
        private FOSV1dbEntities db = new FOSV1dbEntities();


        // GET: OrderTrackings
        public ActionResult Index(int? id)
        {
            var orderTrackings = db.OrderTrackings.Include(o => o.Customer).Include(o => o.OrderDetail);


            if (id != null)
            {
                orderTrackings = orderTrackings.Where(m => m.OrderTrackingID == id);
            }

            return View(orderTrackings.ToList());
        }

        // GET: OrderTrackings/Details/5
        public ActionResult Customer(int? id)
        {
            var custData = from x in db.OrderTrackings select x;
            if (id != null)
            {
                custData = custData.Where(x => x.CustomerID == id);
            }

            return View("Customer", "User", custData.ToList());
        }



        // GET: OrderTrackings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTracking orderTracking = db.OrderTrackings.Find(id);
            if (orderTracking == null)
            {
                return HttpNotFound();
            }
            return View(orderTracking);
        }

        // GET: OrderTrackings/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "OrderID");
            return View();
        }

        // POST: OrderTrackings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderTrackingID,OrderID,CustomerID,OrderLocation,OrderDeliveryTime,Status")] OrderTracking orderTracking)
        {
            if (ModelState.IsValid)
            {
                db.OrderTrackings.Add(orderTracking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", orderTracking.CustomerID);
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "OrderID", orderTracking.OrderID);
            return View(orderTracking);
        }

        // GET: OrderTrackings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTracking orderTracking = db.OrderTrackings.Find(id);
            if (orderTracking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", orderTracking.CustomerID);
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "OrderID", orderTracking.OrderID);
            return View(orderTracking);
        }

        // POST: OrderTrackings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderTrackingID,OrderID,CustomerID,OrderLocation,OrderDeliveryTime,Status")] OrderTracking orderTracking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderTracking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", orderTracking.CustomerID);
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "OrderID", orderTracking.OrderID);
            return View(orderTracking);
        }

        // GET: OrderTrackings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTracking orderTracking = db.OrderTrackings.Find(id);
            if (orderTracking == null)
            {
                return HttpNotFound();
            }
            return View(orderTracking);
        }

        // POST: OrderTrackings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderTracking orderTracking = db.OrderTrackings.Find(id);
            db.OrderTrackings.Remove(orderTracking);
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
