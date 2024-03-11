using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizzeria.Controllers
{
    public class HomeController : Controller
    {
        PizzaDB dbContext = new PizzaDB();
        public ActionResult Index()
        {
            dbContext.Foods.ToList();
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
