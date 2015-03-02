namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocumentContractor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "ContractorId", c => c.Int());
            CreateIndex("dbo.Document", "ContractorId");
            AddForeignKey("dbo.Document", "ContractorId", "dbo.Contractor", "ContractorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Document", "ContractorId", "dbo.Contractor");
            DropIndex("dbo.Document", new[] { "ContractorId" });
            DropColumn("dbo.Document", "ContractorId");
        }
    }
}
