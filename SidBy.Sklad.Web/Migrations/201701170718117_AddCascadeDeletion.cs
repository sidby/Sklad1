namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCascadeDeletion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Document", "ParentDocumentId", "dbo.Document");
            AddForeignKey("dbo.Document", "ParentDocumentId", "dbo.Document", "DocumentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Document", "ParentDocumentId", "dbo.Document");
            AddForeignKey("dbo.Document", "ParentDocumentId", "dbo.Document", "DocumentId");
        }
    }
}
