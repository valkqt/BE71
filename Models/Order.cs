﻿using System;
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
        [Required]
        public string deliveryAddress { get; set; }
        [StringLength(2500)]
        public string notes { get; set; }
        public bool isCompleted { get; set; } = false;
    }
}