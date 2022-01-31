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
    public class ReportsController : Controller
    {
        private FOSV1dbEntities db = new FOSV1dbEntities();

        // GET: Reports
        public ActionResult Index()
        {
            var reports = db.Reports.Include(r => r.Admin).Include(r => r.Customer).Include(r => r.OrderDetail).Include(r => r.Payment);
            return View(reports.ToList());
        }

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "AdminName");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "OrderID");
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentType");
            return View("Create", "dashboard");
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportID,OrderID,CustomerID,AdminID,PaymentID,ReportDate")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "AdminName", report.AdminID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", report.CustomerID);
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "OrderID", report.OrderID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentType", report.PaymentID);
            return View("Create", "dashboard", report);
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "AdminName", report.AdminID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", report.CustomerID);
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "OrderID", report.OrderID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentType", report.PaymentID);
            return View("Edit", "dashboard", report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportID,OrderID,CustomerID,AdminID,PaymentID,ReportDate")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "AdminName", report.AdminID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", report.CustomerID);
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "OrderID", report.OrderID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentType", report.PaymentID);
            return View("Edit", "dashboard", report);
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View("Delete", "dashboard", report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
            db.SaveChanges();
            return RedirectToAction("Index", "Reports");
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
