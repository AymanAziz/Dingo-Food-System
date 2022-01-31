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
    public class CustomersController : Controller
    {
        private FOSV1dbEntities db = new FOSV1dbEntities();

        // GET: Admins/Main
        public ActionResult login()
        {
            return View("login", "loginuser");

        }

        [HttpPost]
        public ActionResult login(Customer log)//usinf admin table as object
        {
            //check if input same with value in database
            var Customer = db.Customers.Where(x => x.CustomerUsername == log.CustomerUsername && x.CustomerPassword == log.CustomerPassword).Count();

        

            if (Customer > 0)//if success
            {
               
                Session["CustomerID"] = Customer;

                // ViewBag.ID = id;
                return RedirectToAction("dashboard");
            }
            else
            {
                ModelState.Clear();//clear form
                return View("login", "loginuser");
            }

        }

        public ActionResult Dashboard()
        {

            if (Session["CustomerID"] != null)
            {

                return RedirectToAction("ViewMenuCust", "Menus");
            }
            else
            {
                return RedirectToAction("login");
            }
        }



        // GET: Customers
        public ActionResult Index()
        {
            return View("Index", "dashboard", db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View("Details", "dashboard", customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View("Create", "loginuser");
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CustomerName,CustomerContactNumber,CustomerAddress,CustomerUsername,CustomerPassword")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("login");
            }

            return View("Create", "loginuser",customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View("Edit", "dashboard", customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CustomerName,CustomerContactNumber,CustomerAddress,CustomerUsername,CustomerPassword")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", "dashboard", customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View("Delete", "dashboard", customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index", "Customers");
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
