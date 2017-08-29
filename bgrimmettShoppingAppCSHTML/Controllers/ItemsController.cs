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
using System.IO;

namespace bgrimmettShoppingAppCSHTML.Controllers
{
    public class ItemsController : Universal
    {


        // GET: Items
        public ActionResult Index()
        {
            return View(db.Items.ToList());
        }

        //Get Items
        public ActionResult SearchResults(string searchitem)
        {
            return View(db.Items.Where(i => i.Name.Contains(searchitem) || i.Description.Contains(searchitem)).ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        [Authorize(Roles = "Admin")]     // authorizes only the admin
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // makes sure that all fields have valid information in them before adding the new item to the index
        // if all fields are NOT valid, the item will not be created.
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]   // authorizes only the admin
        public ActionResult Create([Bind(Include = "Id,CreationDate,UpdatedDate,Name,Price,MediaURL,Description")] Item item, HttpPostedFileBase image)
        {
            // checking to make sure the file size is greater than zero, and that the extension is one of the listed extensions
            if (image != null && image.ContentLength > 0)   //Validating the image
            {
                var ext = Path.GetExtension(image.FileName).ToLower();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp")
                    ModelState.AddModelError("image", "Invalid Format.");
            }
            if (ModelState.IsValid)
            {
                var filePath = "/Upload/";  //adding the file to the database
                var absPath = Server.MapPath("~" + filePath);
                item.MediaURL = filePath + image.FileName;   //specifies the path of the file
                image.SaveAs(Path.Combine(absPath, image.FileName)); //saves the file. necessary for the specified path to have something to point at
                item.CreationDate = System.DateTime.Now;
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        [Authorize(Roles = "Admin")]   // authorizes only the admin
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]   // authorizes only the admin
        public ActionResult Edit([Bind(Include = "Id,CreationDate,UpdatedDate,Name,Price,MediaURL,Description")] Item item, string mediaUrl, HttpPostedFileBase image)
        {
            // checking to make sure the file size is greater than zero, and that the extension is one of the listed extensions
            if (image != null && image.ContentLength > 0)   //Validating the image
            {
                var ext = Path.GetExtension(image.FileName).ToLower();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp")
                    ModelState.AddModelError("image", "Invalid Format.");
            }
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                var filePath = "/Upload/";  //adding the file to the database
                var absPath = Server.MapPath("~" + filePath);
                item.MediaURL = filePath + image.FileName;   //specifies the path of the file
                image.SaveAs(Path.Combine(absPath, image.FileName)); //saves the file. necessary for the specified path to have something to point at
                
                }
                else
                {
                    item.MediaURL = mediaUrl;
                }
                item.UpdatedDate = System.DateTime.Now;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        [Authorize(Roles = "Admin")]   // authorizes only the admin
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]   // authorizes only the admin
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            var absPath = Server.MapPath("~" + item.MediaURL);
            System.IO.File.Delete(absPath);
            db.Items.Remove(item);
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
