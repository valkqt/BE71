namespace Pizzeria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartFoodQuantities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cartId = c.Int(nullable: false),
                        foodId = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Carts", t => t.cartId, cascadeDelete: true)
                .ForeignKey("dbo.Foods", t => t.foodId, cascadeDelete: true)
                .Index(t => t.cartId)
                .Index(t => t.foodId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false),
                        password = c.String(nullable: false, maxLength: 32),
                        role = c.String(maxLength: 50),
                        isRemember = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 50),
                        image = c.String(nullable: false, maxLength: 255),
                        price = c.Double(nullable: false),
                        deliveryTime = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.CartFoods",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cartId = c.Int(nullable: false),
                        foodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Carts", t => t.cartId, cascadeDelete: true)
                .ForeignKey("dbo.Foods", t => t.foodId, cascadeDelete: true)
                .Index(t => t.cartId)
                .Index(t => t.foodId);
            
            CreateTable(
                "dbo.FoodsIngredients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        foodId = c.Int(nullable: false),
                        ingredientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Foods", t => t.foodId, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.ingredientId, cascadeDelete: true)
                .Index(t => t.foodId)
                .Index(t => t.ingredientId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.OrderFoodQuantities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        orderId = c.Int(nullable: false),
                        foodId = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Foods", t => t.foodId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.orderId, cascadeDelete: true)
                .Index(t => t.orderId)
                .Index(t => t.foodId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        deliveryAddress = c.String(nullable: false),
                        notes = c.String(maxLength: 2500),
                        isCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.OrdersFoods",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        foodId = c.Int(nullable: false),
                        orderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Foods", t => t.foodId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.orderId, cascadeDelete: true)
                .Index(t => t.foodId)
                .Index(t => t.orderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdersFoods", "orderId", "dbo.Orders");
            DropForeignKey("dbo.OrdersFoods", "foodId", "dbo.Foods");
            DropForeignKey("dbo.OrderFoodQuantities", "orderId", "dbo.Orders");
            DropForeignKey("dbo.OrderFoodQuantities", "foodId", "dbo.Foods");
            DropForeignKey("dbo.FoodsIngredients", "ingredientId", "dbo.Ingredients");
            DropForeignKey("dbo.FoodsIngredients", "foodId", "dbo.Foods");
            DropForeignKey("dbo.CartFoods", "foodId", "dbo.Foods");
            DropForeignKey("dbo.CartFoods", "cartId", "dbo.Carts");
            DropForeignKey("dbo.CartFoodQuantities", "foodId", "dbo.Foods");
            DropForeignKey("dbo.CartFoodQuantities", "cartId", "dbo.Carts");
            DropForeignKey("dbo.Carts", "userId", "dbo.Users");
            DropIndex("dbo.OrdersFoods", new[] { "orderId" });
            DropIndex("dbo.OrdersFoods", new[] { "foodId" });
            DropIndex("dbo.OrderFoodQuantities", new[] { "foodId" });
            DropIndex("dbo.OrderFoodQuantities", new[] { "orderId" });
            DropIndex("dbo.FoodsIngredients", new[] { "ingredientId" });
            DropIndex("dbo.FoodsIngredients", new[] { "foodId" });
            DropIndex("dbo.CartFoods", new[] { "foodId" });
            DropIndex("dbo.CartFoods", new[] { "cartId" });
            DropIndex("dbo.Carts", new[] { "userId" });
            DropIndex("dbo.CartFoodQuantities", new[] { "foodId" });
            DropIndex("dbo.CartFoodQuantities", new[] { "cartId" });
            DropTable("dbo.OrdersFoods");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderFoodQuantities");
            DropTable("dbo.Ingredients");
            DropTable("dbo.FoodsIngredients");
            DropTable("dbo.CartFoods");
            DropTable("dbo.Foods");
            DropTable("dbo.Users");
            DropTable("dbo.Carts");
            DropTable("dbo.CartFoodQuantities");
        }
    }
}
