using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class Cart
    {
        [Key]
        public int id { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        [NotMapped]
        public List<Food> foods { get; set; } = new List<Food>();

        public Cart() { }
        public Cart(int userId)
        {
            this.userId = userId;
        }
    }
}