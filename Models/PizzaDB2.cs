using System;
using System.Data.Entity;
using System.Linq;

namespace Pizzeria.Models
{
    public class PizzaDB2 : DbContext
    {
        public PizzaDB2()
            : base("name=Pizzeria")

        {
        }


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<OrdersFood> OrdersFoods { get; set; }
        public virtual DbSet<FoodsIngredient> FoodsIngredients { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartFood> CartFoods { get; set; }
        public virtual DbSet<CartFoodQuantity> CartFoodQuantities { get; set; }
        public virtual DbSet<OrderFoodQuantity> OrderFoodQuantities { get; set; }




    }
}

//public class MyEntity
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//}
