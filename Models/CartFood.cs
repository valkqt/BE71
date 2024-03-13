using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class CartFood
    {
        [Key]
        public int id { get; set; }
        public int cartId { get; set; }
        public int foodId { get; set; }
        public Cart cart { get; set; }
        public Food food { get; set; }
        public CartFood() { }
        public CartFood(int cartId, int foodId)
        {
            this.cartId = cartId;
            this.foodId = foodId;
        }
    }
}