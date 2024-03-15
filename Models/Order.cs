using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
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
        [Display(Name = "Delivery Address:")]
        public string deliveryAddress { get; set; }
        [Display(Name = "Additional Notes (allergies, extra ingredients, etc.):")]

        [StringLength(2500)]
        public string notes { get; set; }
        public bool isReady { get; set; } = false;
        public int userId { get; set; }
        public bool isCompleted { get; set; } = false;
        public double total { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime completedAt { get; set; }
        public Order()
        {

        }

        public Order(int userId)
        {
            this.userId = userId;
        }
        public Order(string deliveryAddress, string notes, int userId, double total)
        {
            this.deliveryAddress = deliveryAddress;
            this.notes = notes;
            this.userId = userId;
            this.total = total;
        }
    }
}