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
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "username, password, isRemember")] User user)
        {
            if (ModelState.IsValid)
            {
                var loggedUser = db.Users.Where(u => u.username == user.username && u.password == user.password).FirstOrDefault();
                if (loggedUser == null)
                {
                    TempData["error"] = "Login unsuccessful, incorrect username or password.";
                    return View(user);
                }
                loggedUser.isRemember = user.isRemember;
                db.Entry(loggedUser).State = EntityState.Modified;
                db.SaveChanges();

                return Redirect(loggedUser);
            }
            else
            {
                return View(user);
            }
        }

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
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "username, password, isRemember")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();

                return Redirect(user);

            }
            TempData["error"] = "La registrazione non ha avuto successo";
            return View(user);
        }

    }
}
