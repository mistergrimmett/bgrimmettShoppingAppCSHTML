using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bgrimmettShoppingAppCSHTML.Models;
using bgrimmettShoppingAppCSHTML.Models.CodeFirst;
using Microsoft.AspNet.Identity;

namespace bgrimmettShoppingAppCSHTML.Controllers
{
    public class OrdersController : Universal
    {
        

        // GET: Orders
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(user.Orders.ToList());
        }

        //// GET: Orders/Details/5
        //public ActionResult OrderConfirmed(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order Order = db.Orders.Find(id);
        //    if (Order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var user = db.Users.Find(User.Identity.GetUserId());
        //    Order order = db.Orders.Find(id);
        //    return View(order);
        //}

        //// POST: Orders/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult OrderConfirmed(int id)
        //{
        //    Order order = db.Orders.Find(id);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        // GET: Orders/Details/5
        public ActionResult Details(int? id, bool? justCompleted)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order Order = db.Orders.Find(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            if (justCompleted != null && justCompleted == true)
            {
                ViewBag.JustCompleted = true;
            }
            else
            {
                ViewBag.JustCompleted = false;
            }
            return View(Order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            if (user.CartItems.Count == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,City,State,ZipCode,Country,Phone,Total,OrderDate,CustomerId,OrderDetails")] Order order, decimal total)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                order.CustomerId = user.Id;
                order.OrderDate = System.DateTime.Now;
                order.Total = total;
                db.Orders.Add(order);
                db.SaveChanges();    //generates the Id of an order


                foreach(var item in user.CartItems.ToList())   //ToList puts items in a list and closes the connection
                {
                    OrderItem orderitem = new OrderItem();
                    orderitem.ItemId = item.ItemId;
                    orderitem.OrderId = order.Id;
                    orderitem.Quantity = item.Count;
                    orderitem.UnitPrice = item.Item.Price;
                    db.OrderItems.Add(orderitem);
                    db.CartItems.Remove(item);
                    db.SaveChanges();
                }
               
                return RedirectToAction("Details", new { id = order.Id, justCompleted = true });
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order Order = db.Orders.Find(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(Order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,City,State,ZipCode,Country,Phone,Total,OrderDate,CustomerId,OrderDetails")] Order Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order Order = db.Orders.Find(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(Order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order Order = db.Orders.Find(id);
            db.Orders.Remove(Order);
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
