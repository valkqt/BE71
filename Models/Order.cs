using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class Order
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public int articles { get; set; }
        public string deliveryAddress { get; set; }
        public string notes { get; set; }
        public bool isCompleted { get; set; }
    }
}