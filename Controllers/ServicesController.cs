using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Pizzeria.Controllers
{
    [Authorize(Roles = "user")]
    public class ServicesController : Controller
    {
        PizzaDB2 db = new PizzaDB2();

        public ActionResult Menu()
        {
            return View(db.Foods.ToList());
        }

        public ActionResult Cart()
        {
            Order order = new Order();
            int user = int.Parse(User.Identity.Name);
            var cart = db.Carts.Where(c => c.userId == user).FirstOrDefault();
            List<CartFood> elements = db.CartFoods.Where(c => c.cartId == cart.id).ToList();
            elements.ForEach(elem =>
            {
                var quantity = db.CartFoodQuantities.Where(q => q.foodId == elem.foodId && q.cartId == cart.id).FirstOrDefault().quantity;
                var food = db.Foods.Where(f => f.id == elem.foodId).FirstOrDefault();
                food.quantity = quantity;
                order.foods.Add(food);
            });

            return View(order);
        }


        [HttpPost]
        public ActionResult Cart(Order order)
        {
            if (ModelState.IsValid)
            {
                int user = int.Parse(User.Identity.Name);
                var cart = db.Carts.Where(c => c.userId == user).FirstOrDefault();
                List<CartFood> elements = db.CartFoods.Where(c => c.cartId == cart.id).ToList();
                elements.ForEach(elem =>
                {
                    var quantity = db.CartFoodQuantities.Where(q => q.foodId == elem.foodId && q.cartId == cart.id).FirstOrDefault().quantity;
                    var food = db.Foods.Where(f => f.id == elem.foodId).FirstOrDefault();
                    db.OrdersFoods.Add(new OrdersFood(food.id, order.id));
                    db.OrderFoodQuantities.Add(new OrderFoodQuantity(order.id, food.id, quantity));

                    food.quantity = quantity;
                    order.foods.Add(food);
                });

                db.Orders.Add(order);
                db.SaveChanges();

                return View(order);
            }
            TempData["error"] = "error";
            return View(order);
        }


        public ActionResult AddToCart(string product)
        {
            TempData["product"] = product;
            int user = int.Parse(User.Identity.Name);
            int productId = int.Parse(product);
            var cart = db.Carts.Where(c => c.userId == user).FirstOrDefault();
            var food = db.Foods.Where(f => f.id == productId).FirstOrDefault();
            var foodIsPresent = db.CartFoods.Where(cf => cf.foodId == productId && cf.cartId == cart.id).FirstOrDefault();
            if (foodIsPresent == null)
            {
                db.CartFoods.Add(new CartFood(cart.id, food.id));

            }
            var isPresent = db.CartFoodQuantities.Where(q => q.cartId == cart.id && q.foodId == productId).FirstOrDefault();

            if (isPresent != null)
            {
                isPresent.quantity += 1;
                db.Entry(isPresent).State = EntityState.Modified;
            }
            else
            {
                db.CartFoodQuantities.Add(new CartFoodQuantity(cart.id, food.id, 1));
            }
            db.SaveChanges();

            return RedirectToAction("Menu");
        }
    }
}