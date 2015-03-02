namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParentDocument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "ParentDocumentId", c => c.Int());
            CreateIndex("dbo.Document", "ParentDocumentId");
            AddForeignKey("dbo.Document", "ParentDocumentId", "dbo.Document", "DocumentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Document", "ParentDocumentId", "dbo.Document");
            DropIndex("dbo.Document", new[] { "ParentDocumentId" });
            DropColumn("dbo.Document", "ParentDocumentId");
        }
    }
}
