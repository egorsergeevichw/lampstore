namespace LampStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "Count", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItems", "Index");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "Index", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "Status", c => c.String());
            DropColumn("dbo.OrderItems", "Price");
        }
    }
}
