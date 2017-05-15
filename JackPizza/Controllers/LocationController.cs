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
using System.Text;
using Newtonsoft.Json.Linq;

namespace JackPizza.Controllers
{
    public class LocationController : Controller
    {
        private JackPizzaContext db = new JackPizzaContext();

        // GET: Location
        public ActionResult Index()
        {
            return View(db.Locations.ToList());
        }

        // GET: Location/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Location/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Location/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public ActionResult Create([Bind(Include = "LocationID,UnitNumber,StreetNumber,Street,Suburb,Latitude,Longitude")] Location location)
        //{
        //    WebClient c = new WebClient();
        //    StringBuilder sbFullURL = new StringBuilder();
        //    sbFullURL.Append("https://maps.googleapis.com/maps/api/geocode/json?address=" + location.StreetNumber);
        //    string[] streets = location.Street.Split(' ');
        //    foreach (string ele in streets)
        //    {
        //        sbFullURL.Append("+" + ele);
        //    }
        //    sbFullURL.Append(",");
        //    string[] suburbs = location.Suburb.Split(' ');
        //    foreach (string ele in suburbs)
        //    {
        //        sbFullURL.Append("+" + ele);
        //    }
        //    sbFullURL.Append(",+AKL&key=AIzaSyBy31wHgP9tfD10HR1VaEQepWvu1Y_ACbA");
        //    string strUri = sbFullURL.ToString();
        //    var data = c.DownloadString(strUri);

        //    JObject o = JObject.Parse(data);
        //    var dat = o["results"].ToString().Trim();
        //    JArray JA = JArray.Parse(dat);
        //    var newO = JA.First;
        //    JObject newOO = JObject.Parse(newO.ToString().Trim());
        //    var latitude = double.Parse(newOO["geometry"]["location"]["lat"].ToString().Trim());
        //    var longitude = double.Parse(newOO["geometry"]["location"]["lng"].ToString().Trim());


        //    if (ModelState.IsValid)
        //    {
        //        db.Locations.Add(location);
        //        db.SaveChanges();

        //    }

        //    var results = new Location
        //    {
        //        UnitNumber = location.UnitNumber,
        //        StreetNumber = location.StreetNumber,
        //        Street = location.Street,
        //        Suburb = location.Suburb,
        //        Latitude = latitude,
        //        Longitude = longitude
        //    };
        //    return Json("Longitude:"+ longitude);

        //}


        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "LocationID,UnitNumber,StreetNumber,Street,Suburb,Latitude,Longitude")] Location location)
        {
            WebClient c = new WebClient();
            StringBuilder sbFullURL = new StringBuilder();
            sbFullURL.Append("https://maps.googleapis.com/maps/api/geocode/json?address=" + location.StreetNumber);
            string[] streets = location.Street.Split(' ');
            foreach (string ele in streets)
            {
                sbFullURL.Append("+" + ele);
            }
            sbFullURL.Append(",");
            string[] suburbs = location.Suburb.Split(' ');
            foreach (string ele in suburbs)
            {
                sbFullURL.Append("+" + ele);
            }
            sbFullURL.Append(",+AKL&key=AIzaSyBy31wHgP9tfD10HR1VaEQepWvu1Y_ACbA");
            string strUri = sbFullURL.ToString();
            var data = c.DownloadString(strUri);

            JObject o = JObject.Parse(data);
            var dat = o["results"].ToString().Trim();
            JArray JA = JArray.Parse(dat);
            var newO = JA.First;
            JObject newOO = JObject.Parse(newO.ToString().Trim());
            var latitude = double.Parse(newOO["geometry"]["location"]["lat"].ToString().Trim());
            var longitude = double.Parse(newOO["geometry"]["location"]["lng"].ToString().Trim());


            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();

            }

            return Json("lat:" + latitude + ", lng:"+ longitude);
        }
        // GET: Location/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationID,UnitNumber,StreetNumber,Street,Suburb,Latitude,Longitude")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        // GET: Location/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
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
