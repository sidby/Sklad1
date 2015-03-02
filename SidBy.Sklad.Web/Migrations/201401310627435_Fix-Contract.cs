namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixContract : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contract", "UserProfile_UserId", "dbo.UserProfile");
            DropIndex("dbo.Contract", new[] { "UserProfile_UserId" });
            DropColumn("dbo.Contract", "UserProfile_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contract", "UserProfile_UserId", c => c.Int());
            CreateIndex("dbo.Contract", "UserProfile_UserId");
            AddForeignKey("dbo.Contract", "UserProfile_UserId", "dbo.UserProfile", "UserId");
        }
    }
}
