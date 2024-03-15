using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Pizzeria.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        PizzaDB2 db = new PizzaDB2();

        public ActionResult Menu()
        {
            return View(db.Foods.ToList());
        }

        public ActionResult Cart()
        {
            int user = int.Parse(User.Identity.Name);

            var userOrder = db.Orders.Where(o => o.userId == user && o.isReady == false).FirstOrDefault();
            ViewBag.Nothing = "";

            if (userOrder == null)
            {
                userOrder = new Order(user);
                ViewBag.Nothing = "empty";
            }

            List<OrdersFood> elements = db.OrdersFoods.Where(f => f.orderId == userOrder.id).ToList();
            elements.ForEach(elem =>
            {
                var food = db.Foods.Where(f => f.id == elem.foodId).FirstOrDefault();
                food.quantity = elem.quantity;
                userOrder.foods.Add(food);
            });

            return View(userOrder);
        }

        public ActionResult RemoveFromCart(int product)
        {
            int user = int.Parse(User.Identity.Name);
            var food = db.Foods.Where(f => f.id == product).FirstOrDefault();
            var order = db.Orders.Where(o => o.userId == user && o.isReady == false).FirstOrDefault();
            var orderFood = db.OrdersFoods.Where(of => of.foodId == product && of.orderId == order.id).FirstOrDefault();
            if (orderFood.quantity > 1)
            {
                orderFood.quantity -= 1;
                db.Entry(orderFood).State = EntityState.Modified;
            }
            else
            {
                db.OrdersFoods.Remove(orderFood);
            }
            order.total -= food.price;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Cart");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cart([Bind(Include = "deliveryAddress, notes")] Order orderData)
        {
            if (ModelState.IsValid)
            {
                if (orderData.deliveryAddress == null)
                {
                    TempData["error"] = "Please insert valid address";
                    return View(orderData);
                }
                int user = int.Parse(User.Identity.Name);
                var userOrder = db.Orders.Where(o => o.userId == user && o.isReady == false).FirstOrDefault();
                userOrder.deliveryAddress = orderData.deliveryAddress;
                userOrder.notes = orderData.notes;
                userOrder.isReady = true;

                var orderContents = db.OrdersFoods.Where(of => of.orderId == userOrder.id).ToList();
                db.OrdersFoods.RemoveRange(orderContents);


                db.Entry(userOrder).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Cart");
            }
            TempData["error"] = "error";
            return RedirectToAction("Cart");
        }


        public ActionResult AddToCart(string product)
        {
            TempData["product"] = product;
            int user = int.Parse(User.Identity.Name);
            int productId = int.Parse(product);
            Order userOrder = db.Orders.Where(o => o.userId == user && o.isReady == false).FirstOrDefault();


            if (userOrder == null)
            {
                Order newOrder = new Order(user);
                db.Orders.Add(newOrder);
                db.SaveChanges();

                userOrder = newOrder;
            }
            var foodIsPresent = db.OrdersFoods.Where(of => of.foodId == productId && of.orderId == userOrder.id).FirstOrDefault();
            if (foodIsPresent == null)
            {
                db.OrdersFoods.Add(new OrdersFood(productId, userOrder.id));
            }
            else
            {
                foodIsPresent.quantity += 1;
                db.Entry(foodIsPresent).State = EntityState.Modified;
            }
            userOrder.total += db.Foods.Where(f => f.id == productId).FirstOrDefault().price;
            db.SaveChanges();

            return RedirectToAction("Menu");
        }
    }
}