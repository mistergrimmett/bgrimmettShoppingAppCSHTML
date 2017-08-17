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

namespace bgrimmettShoppingAppCSHTML.Controllers
{
    public class OrderEntryTablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderEntryTables
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: OrderEntryTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderEntryTable orderEntryTable = db.Orders.Find(id);
            if (orderEntryTable == null)
            {
                return HttpNotFound();
            }
            return View(orderEntryTable);
        }

        // GET: OrderEntryTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderEntryTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,City,State,ZipCode,Country,Phone,Total,OrderDate,CustomerId,OrderDetails")] OrderEntryTable orderEntryTable)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orderEntryTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderEntryTable);
        }

        // GET: OrderEntryTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderEntryTable orderEntryTable = db.Orders.Find(id);
            if (orderEntryTable == null)
            {
                return HttpNotFound();
            }
            return View(orderEntryTable);
        }

        // POST: OrderEntryTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,City,State,ZipCode,Country,Phone,Total,OrderDate,CustomerId,OrderDetails")] OrderEntryTable orderEntryTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderEntryTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderEntryTable);
        }

        // GET: OrderEntryTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderEntryTable orderEntryTable = db.Orders.Find(id);
            if (orderEntryTable == null)
            {
                return HttpNotFound();
            }
            return View(orderEntryTable);
        }

        // POST: OrderEntryTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderEntryTable orderEntryTable = db.Orders.Find(id);
            db.Orders.Remove(orderEntryTable);
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
