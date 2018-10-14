namespace LampStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        CartItemItemId = c.Guid(nullable: false),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cart_CartId = c.Guid(),
                        Product_ProductId = c.Guid(),
                    })
                .PrimaryKey(t => t.CartItemItemId)
                .ForeignKey("dbo.Carts", t => t.Cart_CartId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Cart_CartId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Guid(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Users", t => t.CartId)
                .Index(t => t.CartId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Carts", "CartId", "dbo.Users");
            DropForeignKey("dbo.CartItems", "Cart_CartId", "dbo.Carts");
            DropIndex("dbo.Carts", new[] { "CartId" });
            DropIndex("dbo.CartItems", new[] { "Product_ProductId" });
            DropIndex("dbo.CartItems", new[] { "Cart_CartId" });
            DropTable("dbo.Carts");
            DropTable("dbo.CartItems");
        }
    }
}
