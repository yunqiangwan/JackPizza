using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JackPizza.DAL;
using JackPizza.ViewModels;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections;
using JackPizza.Models;
using System.Web.Script.Serialization;

namespace JackPizza.Controllers
{
    public class AddressController : Controller
    {
       private JackPizzaContext db = new JackPizzaContext();

        // GET: Address
        public ActionResult Index()
        {
            return View(db.Addresses.ToList());
        }
        
        // GET: Address/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Address/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressID,SUnitNumber,SStreetNumber,SStreet,SSuburb,DUnitNumber,DStreetNumber,DStreet,DSuburb")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            ArrayList  list = new ArrayList();
            //var results = new Address
            //{


            //};
            WebClient c = new WebClient();
            StringBuilder sbFullURL = new StringBuilder();
            sbFullURL.Append("https://maps.googleapis.com/maps/api/directions/json?origin=+" + address.SStreetNumber);
            string[] streets = address.SStreet.Split(' ');
            foreach(string ele in streets)
            {
                sbFullURL.Append("+"+ele);
            }
            sbFullURL.Append(",+AKL&destination=");
            sbFullURL.Append("+" + address.DStreetNumber);
            string[] dstreets = address.DStreet.Split(' ');
            foreach (string ele in dstreets)
            {
                sbFullURL.Append("+" + ele);
            }
            sbFullURL.Append(",+AKL&key=AIzaSyBy31wHgP9tfD10HR1VaEQepWvu1Y_ACbA");
            string strUri = sbFullURL.ToString();
            var data = c.DownloadString(strUri);

            JObject o = JObject.Parse(data);
            var dat = o["routes"].ToString().Trim();
            JArray JA = JArray.Parse(dat);

            foreach (JObject items in JA)
            {
                JArray JAA = JArray.Parse(items["legs"].ToString().Trim());                
                var newO = JAA.First;
                var Jstep = JArray.Parse(newO["steps"].ToString().Trim());
                foreach (JObject ele in Jstep)
                {
                    TwoPoints twoPoints = new TwoPoints();
                    twoPoints.startlat = double.Parse(ele["start_location"]["lat"].ToString().Trim());
                    twoPoints.startlng = double.Parse(ele["start_location"]["lng"].ToString().Trim());
                    twoPoints.endtlat = double.Parse(ele["end_location"]["lat"].ToString().Trim());
                    twoPoints.endlng = double.Parse(ele["end_location"]["lng"].ToString().Trim());
                    list.Add(twoPoints);
                }
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(list);
            return Json(json);
        }

        // GET: Address/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressID,SUnitNumber,SStreetNumber,SStreet,SSuburb,DUnitNumber,DStreetNumber,DStreet,DSuburb")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(address);
        }

        // GET: Address/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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
