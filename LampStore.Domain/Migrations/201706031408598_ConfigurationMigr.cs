namespace LampStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigurationMigr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItems", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CartItems", "Count", c => c.Int(nullable: false));
            DropColumn("dbo.Carts", "PurchaseDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "PurchaseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CartItems", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.CartItems", "Price");
        }
    }
}
