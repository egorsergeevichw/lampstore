namespace LampStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixMigr : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CartItems");
            AddColumn("dbo.CartItems", "CartItemId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.CartItems", "CartItemId");
            DropColumn("dbo.CartItems", "CartItemItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartItems", "CartItemItemId", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.CartItems");
            DropColumn("dbo.CartItems", "CartItemId");
            AddPrimaryKey("dbo.CartItems", "CartItemItemId");
        }
    }
}
