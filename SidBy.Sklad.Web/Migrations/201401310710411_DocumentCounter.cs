namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentCounter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocumentCounter",
                c => new
                    {
                        DocumentCounterId = c.Int(nullable: false, identity: true),
                        Counter = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        DocumentTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentCounterId)
                .ForeignKey("dbo.DocumentType", t => t.DocumentTypeId, cascadeDelete: true)
                .Index(t => t.DocumentTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocumentCounter", "DocumentTypeId", "dbo.DocumentType");
            DropIndex("dbo.DocumentCounter", new[] { "DocumentTypeId" });
            DropTable("dbo.DocumentCounter");
        }
    }
}
