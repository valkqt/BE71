using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [NotMapped]
        public List<Food> foods { get; set; } = new List<Food>();
        [StringLength(255)]
        public string deliveryAddress { get; set; }
        [StringLength(2500)]
        public string notes { get; set; }
        public bool isReady { get; set; } = false;
        public int userId { get; set; }
        public bool isCompleted { get; set; } = false;
        public Order()
        {

        }

        public Order(int userId)
        {
            this.userId = userId;
        }
        public Order(string deliveryAddress, string notes, int userId)
        {
            this.deliveryAddress = deliveryAddress;
            this.notes = notes;
            this.userId = userId;
        }
    }
}