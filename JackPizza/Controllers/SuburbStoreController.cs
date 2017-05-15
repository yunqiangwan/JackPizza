using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JackPizza.DAL;
using JackPizza.Models;

namespace JackPizza.Controllers
{
    public class SuburbStoreController : Controller
    {
        private JackPizzaContext db = new JackPizzaContext();

        // GET: SuburbStore
        public ActionResult Index()
        {
            return View(db.SuburbStores.ToList());
        }

        // GET: SuburbStore/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuburbStore suburbStore = db.SuburbStores.Find(id);
            if (suburbStore == null)
            {
                return HttpNotFound();
            }
            return View(suburbStore);
        }

        // GET: SuburbStore/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuburbStore/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "suburbstoreID,suburb,store,postcode")] SuburbStore suburbStore)
        {
            if (ModelState.IsValid)
            {
                db.SuburbStores.Add(suburbStore);
                db.SaveChanges();
               // return RedirectToAction("Index");
            }
             
            return Json("lat:"+ -36.908+", lng:" +174.682);
        }

        // GET: SuburbStore/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuburbStore suburbStore = db.SuburbStores.Find(id);
            if (suburbStore == null)
            {
                return HttpNotFound();
            }
            return View(suburbStore);
        }

        // POST: SuburbStore/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "suburbstoreID,suburb,store,postcode")] SuburbStore suburbStore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suburbStore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suburbStore);
        }

        // GET: SuburbStore/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuburbStore suburbStore = db.SuburbStores.Find(id);
            if (suburbStore == null)
            {
                return HttpNotFound();
            }
            return View(suburbStore);
        }

        // POST: SuburbStore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuburbStore suburbStore = db.SuburbStores.Find(id);
            db.SuburbStores.Remove(suburbStore);
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
