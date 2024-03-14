using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizzeria.Models
{
    public class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        [Required]
        [StringLength(50)]
        public string title { get; set; }
        [Required]
        [StringLength(255)]
        public string image { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Il prezzo dev'essere maggiore di 0")]
        public double price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Il tempo di consegna non può essere negativo")]
        public int deliveryTime { get; set; }
        [Required]
        public string ingredients { get; set; }
        public int quantity { get; set; } = 1;

        public Food() { }
        public Food(int id, string title, string image, double price, int deliveryTime, int quantity, string ingredients)
        {
            this.id = id;
            this.title = title;
            this.image = image;
            this.price = price;
            this.deliveryTime = deliveryTime;
            this.quantity = quantity;
            this.ingredients = ingredients;
        }


    }
}