using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JackPizza.DAL;
using JackPizza.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace JackPizza.Controllers
{

    [Authorize]
    public class CheckoutController : Controller
    {

        private JackPizzaContext db = new JackPizzaContext();
        const String PromoCode = "FREE";
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(CustomerOrder customerOrder)
        {
            var order = new CustomerOrder();

            TryUpdateModel(order);

            try
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(customerOrder.Email));  // replace with valid value 
                message.From = new MailAddress("wyqiang328@gmail.com");  // replace with valid value
                message.Subject = "Welcome to Jack's Pizza";
                message.Body = string.Format(body, customerOrder.CustomerUserName, customerOrder.Email, "Thank you for coming");
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {

                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    
                    NetworkCredential NetworkCred = new NetworkCredential("wyqiang328@gmail.com", "Talent-1982");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;

                    await smtp.SendMailAsync(message);

                    db.CustomerOrders.Add(order);
                    db.SaveChanges();

                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    db.SaveChanges();//we have received the total amount lets update it

                    return RedirectToAction("Complete", new { id = order.Id });
                }
            }
            catch(Exception ex)
            {
                ex.InnerException.ToString();
                return View(order);
            }
        }

        public ActionResult Complete(int id)
        {
            bool isValid = db.CustomerOrders.Any(
                o => o.Id == id &&
                     o.CustomerUserName == User.Identity.Name
                );

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}