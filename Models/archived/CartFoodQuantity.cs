using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class CartFoodQuantity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int cartId { get; set; }
        public int foodId { get; set; }
        [Required]
        public int quantity { get; set; }
        public Cart cart { get; set; }
        public Food food { get; set; }
        public CartFoodQuantity() { }
        public CartFoodQuantity(int cartId, int foodId, int quantity)
        {
            this.cartId = cartId;
            this.foodId = foodId;
            this.quantity = quantity;

        }
    }
}