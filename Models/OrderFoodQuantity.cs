using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class OrderFoodQuantity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int orderId { get; set; }
        public int foodId { get; set; }
        [Required]
        public int quantity { get; set; }
        public Order order { get; set; }
        public Food food { get; set; }
        public OrderFoodQuantity() { }
        public OrderFoodQuantity(int orderId, int foodId, int quantity)
        {
            this.orderId = orderId;
            this.foodId = foodId;
            this.quantity = quantity;

        }
    }
}