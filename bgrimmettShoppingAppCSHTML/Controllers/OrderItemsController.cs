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
    public class OrderItemsController : Universal
    {


        // GET: OrderItems
        public ActionResult Index()
        {
            var orderItems = db.OrderItems.Include(o => o.Item).Include(o => o.Order);
            return View(orderItems.ToList());
        }

        // GET: OrderItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // GET: OrderItems/Create
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name");
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Address");
            return View();
        }

        // POST: OrderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id)
        {
            if (ModelState.IsValid)
            {
                OrderItem orderitem = new OrderItem();
                var user = db.Users.Find(User.Identity.GetUserId());
                var cartitem = db.CartItems.Find(User.Identity.GetUserId());
                orderitem.Quantity = 1;
                orderitem.ItemId = id.Value;
                db.OrderItems.Add(orderitem);
                db.CartItems.Remove(cartitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", orderItem.ItemId);
            //ViewBag.OrderId = new SelectList(db.Orders, "Id", "Address", orderItem.OrderId);



            return View();
        }

        // GET: OrderItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", orderItem.ItemId);
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Address", orderItem.OrderId);
            return View(orderItem);
        }

        // POST: OrderItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,ItemId,Quantity,UnitPrice")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", orderItem.ItemId);
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Address", orderItem.OrderId);
            return View(orderItem);
        }

        // GET: OrderItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderItem orderItem = db.OrderItems.Find(id);
            db.OrderItems.Remove(orderItem);
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
