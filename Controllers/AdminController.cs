using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizzeria.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        PizzaDB2 db = new PizzaDB2();

        public ActionResult Orders()
        {
            // visualizza ordini con un'opzione per "completarli"

            var orders = db.Orders.Where(o => o.isReady == true).ToList();
            orders.ForEach(o =>
            {
                var query = (from OF in db.OrdersFoods
                             join FD in db.Foods on OF.foodId equals FD.id
                             where (OF.foodId == FD.id)
                             select new { id = FD.id, image = FD.image, deliveryTime = FD.deliveryTime, price = FD.price, quantity = FD.quantity, title = FD.title, ingredients = FD.ingredients });
                query.ToList().ForEach(elem => o.foods.Add(new Food(elem.id, elem.title, elem.image, elem.price, elem.deliveryTime, elem.quantity, elem.ingredients)));
            });

            TempData["error"] = "Can't retrieve orders";

            return View(orders);
        }

        public ActionResult Services()
        {
            // visualizza tabella cibi con add, edit, delete
            return View();
        }

        public ActionResult Dispatch(string product)
        {
            int productId = int.Parse(product);
            var order = db.Orders.Where(o => o.id == productId).FirstOrDefault();
            order.isCompleted = true;
            db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Orders");
        }

        public JsonResult Stats()
        {
            // Json controller per le chiamate asincrone
            return Json("pepe");
        }
    }
}