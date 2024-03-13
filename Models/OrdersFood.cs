using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class OrdersFood
    {
        [Key]
        public int id { get; set; }

        public int foodId { get; set; }
        public int orderId { get; set; }
        public Food food { get; set; }
        public Order order { get; set; }

        public OrdersFood() { }
        public OrdersFood(int foodId, int orderId)
        {
            this.foodId = foodId;
            this.orderId = orderId;
        }
    }
}