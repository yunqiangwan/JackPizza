using JackPizza.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JackPizza.Controllers
{
    public class DeliveryController : Controller
    {
        private JackPizzaContext db = new JackPizzaContext();
        // GET: Delivery
        public ActionResult Index()
        {
            return View(db.PizzaStores.ToList());
        }
    }
}