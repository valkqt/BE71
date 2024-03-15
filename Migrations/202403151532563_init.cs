namespace Pizzeria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 50),
                        image = c.String(nullable: false, maxLength: 255),
                        price = c.Double(nullable: false),
                        deliveryTime = c.Int(nullable: false),
                        ingredients = c.String(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        deliveryAddress = c.String(maxLength: 255),
                        notes = c.String(maxLength: 2500),
                        isReady = c.Boolean(nullable: false),
                        userId = c.Int(nullable: false),
                        isCompleted = c.Boolean(nullable: false),
                        total = c.Double(nullable: false),
                        completedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.OrdersFoods",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        foodId = c.Int(nullable: false),
                        orderId = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Foods", t => t.foodId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.orderId, cascadeDelete: true)
                .Index(t => t.foodId)
                .Index(t => t.orderId);
            
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
                "dbo.Carts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "userId", "dbo.Users");
            DropForeignKey("dbo.OrdersFoods", "orderId", "dbo.Orders");
            DropForeignKey("dbo.OrdersFoods", "foodId", "dbo.Foods");
            DropIndex("dbo.Carts", new[] { "userId" });
            DropIndex("dbo.OrdersFoods", new[] { "orderId" });
            DropIndex("dbo.OrdersFoods", new[] { "foodId" });
            DropTable("dbo.Carts");
            DropTable("dbo.Users");
            DropTable("dbo.OrdersFoods");
            DropTable("dbo.Orders");
            DropTable("dbo.Foods");
        }
    }
}
