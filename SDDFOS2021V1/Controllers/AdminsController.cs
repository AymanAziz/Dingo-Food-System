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
    public class AdminsController : Controller
    {
        private FOSV1dbEntities db = new FOSV1dbEntities();

        // GET: loginAdmin
        public ActionResult login()
        {
            return View("login", "loginadmin");
        }

        [HttpPost]
        public ActionResult login(Admin log)//usinf admin table as object
        {
            //check if input same with value in database
            var Admin = db.Admins.Where(x => x.AdminUsername == log.AdminUsername && x.AdminPassword == log.AdminPassword).Count();

            if (Admin > 0)//if success
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                ModelState.Clear();//clear form
                return View("login", "loginadmin");
            }
        }
        public ActionResult Dashboard()
        {
            return View("home", "dashboard");
        }








        // GET: Admins
        public ActionResult Index()
        {
            return View("Index", "dashboard", db.Admins.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View("Details", "dashboard", admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View("Create", "dashboard");

        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminID,AdminName,AdminContactNumber,AdminEmail,AdminUsername,AdminPassword")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create", "dashboard", admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View("Edit", "dashboard", admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminID,AdminName,AdminContactNumber,AdminEmail,AdminUsername,AdminPassword")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", "dashboard", admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View("Delete", "dashboard", admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index", "Admins");

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
