using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "La password deve essere compresa fra 4 e 32 caratteri")]
        public string password { get; set; }
        [StringLength(50)]
        public string role { get; set; } = "user";
        public bool isRemember { get; set; } = false;
        public ICollection<Cart> cart { get; set; }
    }
}