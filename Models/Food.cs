using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class Food
    {
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public double price { get; set; }
        public int deliveryTime { get; set; }
        public int foodIngredients { get; set; }
    }
}