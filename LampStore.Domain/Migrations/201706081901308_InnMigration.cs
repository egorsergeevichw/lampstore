namespace LampStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InnMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Inn", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Inn", c => c.Int());
        }
    }
}
