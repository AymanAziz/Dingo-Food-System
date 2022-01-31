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
    public class PaymentsController : Controller
    {
        private FOSV1dbEntities db = new FOSV1dbEntities();

        // GET: Payments
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Customer).Include(p => p.OrderDetail).Include(p => p.Refund);
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create(int? id)
        {
            var custData = from x in db.Customers select x;
            if (id != null)
            {
                custData = custData.Where(x => x.CustomerID == id);
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "OrderID");
            ViewBag.RefundID = new SelectList(db.Refunds, "RefundID", "RefundID");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentID,CustomerID,OrderID,RefundID,PaymentAmount,PaymentType,PaymentDate,OrderDetails")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", payment.CustomerID);
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "OrderID", payment.OrderID);
            ViewBag.RefundID = new SelectList(db.Refunds, "RefundID", "RefundID", payment.RefundID);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", payment.CustomerID);
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "OrderID", payment.OrderID);
            ViewBag.RefundID = new SelectList(db.Refunds, "RefundID", "RefundID", payment.RefundID);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentID,CustomerID,OrderID,RefundID,PaymentAmount,PaymentType,PaymentDate,OrderDetails")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", payment.CustomerID);
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "OrderID", payment.OrderID);
            ViewBag.RefundID = new SelectList(db.Refunds, "RefundID", "RefundID", payment.RefundID);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
