namespace LampStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Guid(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        User_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.Guid(nullable: false),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Order_OrderId = c.Guid(),
                        Product_ProductId = c.Guid(),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Order_OrderId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Guid(nullable: false),
                        Picture = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        Count = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rating = c.Int(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        FullName = c.String(),
                        CompanyName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        Inn = c.Int(),
                        ConfirmEmail = c.Boolean(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.OrderItems", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "Product_ProductId" });
            DropIndex("dbo.OrderItems", new[] { "Order_OrderId" });
            DropIndex("dbo.Orders", new[] { "User_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
        }
    }
}
