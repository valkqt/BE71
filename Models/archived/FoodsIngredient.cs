using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class FoodsIngredient
    {
        [Key]
        public int id { get; set; }
        public int foodId { get; set; }
        public int ingredientId { get; set; }
        public Food food { get; set; }
        public Ingredient ingredient { get; set; }
    }
}