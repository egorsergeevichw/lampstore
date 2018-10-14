namespace LampStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedbackTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Guid(nullable: false),
                        Index = c.Int(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        Message = c.String(),
                        SendDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FeedbackId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Feedbacks");
        }
    }
}
