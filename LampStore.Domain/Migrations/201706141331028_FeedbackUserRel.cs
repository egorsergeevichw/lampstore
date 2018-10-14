namespace LampStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedbackUserRel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Feedbacks", "FeedbackId");
            AddForeignKey("dbo.Feedbacks", "FeedbackId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "FeedbackId", "dbo.Users");
            DropIndex("dbo.Feedbacks", new[] { "FeedbackId" });
        }
    }
}
