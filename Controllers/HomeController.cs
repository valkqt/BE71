using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pizzeria.Controllers
{
    public class HomeController : Controller
    {
        PizzaDB2 db = new PizzaDB2();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            TempData["user"] = User.Identity.Name;

            return View();
        }

        public ActionResult Redirect(User loggedUser)
        {
            FormsAuthentication.SetAuthCookie(loggedUser.id.ToString(), loggedUser.isRemember);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var loggedUser = db.Users.Where(u => u.username == user.username && u.password == user.password).FirstOrDefault();
            loggedUser.isRemember = user.isRemember;
            if (loggedUser == null)
            {
                TempData["error"] = "Error: Non siamo riusciti ad effettuare il login con successo";
                return RedirectToAction("Login");
            }

            return Redirect(loggedUser);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.Carts.Add(new Cart(user.id));
                db.SaveChanges();

                return Redirect(user);

            }
            TempData["error"] = "La registrazione non ha avuto successo";
            return View(user);
        }

    }
}
