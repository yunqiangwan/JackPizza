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
    public class PizzaStoreController : Controller
    {
        private JackPizzaContext db = new JackPizzaContext();

        // GET: PizzaStore
        public ActionResult Index()
        {
            return View(db.PizzaStores.ToList());
        }

        // GET: PizzaStore/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PizzaStore pizzaStore = db.PizzaStores.Find(id);
            if (pizzaStore == null)
            {
                return HttpNotFound();
            }
            return View(pizzaStore);
        }

        // GET: PizzaStore/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PizzaStore/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PizzaStoreId,Name,Latitude,Longitude,Decription")] PizzaStore pizzaStore)
        {
            if (ModelState.IsValid)
            {
                db.PizzaStores.Add(pizzaStore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pizzaStore);
        }

        // GET: PizzaStore/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PizzaStore pizzaStore = db.PizzaStores.Find(id);
            if (pizzaStore == null)
            {
                return HttpNotFound();
            }
            return View(pizzaStore);
        }

        // POST: PizzaStore/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PizzaStoreId,Name,Latitude,Longitude,Decription")] PizzaStore pizzaStore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pizzaStore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizzaStore);
        }

        // GET: PizzaStore/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PizzaStore pizzaStore = db.PizzaStores.Find(id);
            if (pizzaStore == null)
            {
                return HttpNotFound();
            }
            return View(pizzaStore);
        }

        // POST: PizzaStore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PizzaStore pizzaStore = db.PizzaStores.Find(id);
            db.PizzaStores.Remove(pizzaStore);
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
