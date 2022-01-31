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
    public class RefundsController : Controller
    {
        private FOSV1dbEntities db = new FOSV1dbEntities();

        // GET: Refunds
        public ActionResult Index()
        {
            var refunds = db.Refunds.Include(r => r.Admin).Include(r => r.Payment);
            return View(refunds.ToList());
        }

        // GET: Refunds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Refund refund = db.Refunds.Find(id);
            if (refund == null)
            {
                return HttpNotFound();
            }
            return View(refund);
        }

        // GET: Refunds/Create
        public ActionResult Create()
        {
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "AdminName");
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentID");
            return View();
        }

        // POST: Refunds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RefundID,PaymentID,AdminID,RefundAmount,RefundDate")] Refund refund)
        {
            if (ModelState.IsValid)
            {
                db.Refunds.Add(refund);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "AdminName", refund.AdminID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentID", refund.PaymentID);
            return View(refund);
        }

        // GET: Refunds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Refund refund = db.Refunds.Find(id);
            if (refund == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "AdminName", refund.AdminID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentID", refund.PaymentID);
            return View(refund);
        }

        // POST: Refunds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RefundID,PaymentID,AdminID,RefundAmount,RefundDate")] Refund refund)
        {
            if (ModelState.IsValid)
            {
                db.Entry(refund).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "AdminName", refund.AdminID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentID", refund.PaymentID);
            return View(refund);
        }

        // GET: Refunds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Refund refund = db.Refunds.Find(id);
            if (refund == null)
            {
                return HttpNotFound();
            }
            return View(refund);
        }

        // POST: Refunds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Refund refund = db.Refunds.Find(id);
            db.Refunds.Remove(refund);
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
