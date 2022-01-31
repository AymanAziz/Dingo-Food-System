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
    public class OrderDetailsController : Controller
    {
        private FOSV1dbEntities db = new FOSV1dbEntities();

        // GET: OrderDetails
        public ActionResult Index(int? id)
        {
         
            var orderDetails = db.OrderDetails.Include(o => o.Customer).Include(o => o.Menu);
            if (id != null)
            {
                orderDetails = orderDetails.Where(o => o.CustomerID == id);

            }
            return View("Index", "User",orderDetails.ToList());
        }


        // GET: OrderDetails/Create
        public ActionResult Create(int? id)
        {
            var custData = from x in db.Customers select x;
            if (id != null)
            {
                custData = custData.Where(x => x.CustomerID == id);
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "FoodName");
           
            return View("Create","User");
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderDetail orderDetail)
        {

            int a = (int)orderDetail.OrderQuantity;
            orderDetail.OrderDate = DateTime.Now; //auto generate date 
         

            double mID = (double)orderDetail.MenuID;

            Menu menu = db.Menus.Find(mID);

            double cc = (double)menu.FoodPrice;

            double total = cc * Convert.ToDouble(a);

            orderDetail.OrderAmount = total;

            if (ModelState.IsValid)
            {
               
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", orderDetail.CustomerID);
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "FoodName", orderDetail.MenuID);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", orderDetail.CustomerID);
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "FoodName", orderDetail.MenuID);
            return View("Edit","User",orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( OrderDetail orderDetail)
        {
          

            if (ModelState.IsValid)
            {
                int a = (int)orderDetail.OrderQuantity;
                orderDetail.OrderDate = DateTime.Now; //auto generate date 


                double mID = (double)orderDetail.MenuID;

                Menu menu = db.Menus.Find(mID);

                double cc = (double)menu.FoodPrice;

                double total = cc * Convert.ToDouble(a);

                orderDetail.OrderAmount = total;

                db.Entry(orderDetail).State = EntityState.Modified;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", orderDetail.CustomerID);
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "FoodName", orderDetail.MenuID);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View("Delete","User",orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
