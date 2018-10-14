namespace LampStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndexField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.OrderItems", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "AddingDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "Index", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Index");
            DropColumn("dbo.Products", "AddingDate");
            DropColumn("dbo.Products", "Index");
            DropColumn("dbo.OrderItems", "Index");
            DropColumn("dbo.Orders", "Index");
        }
    }
}
