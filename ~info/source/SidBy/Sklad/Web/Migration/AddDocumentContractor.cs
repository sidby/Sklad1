namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class AddDocumentContractor : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddDocumentContractor));

        public override void Down()
        {
            base.DropForeignKey("dbo.Document", "ContractorId", "dbo.Contractor", null);
            base.DropIndex("dbo.Document", new string[] { "ContractorId" }, null);
            base.DropColumn("dbo.Document", "ContractorId", null);
        }

        public override void Up()
        {
            base.AddColumn("dbo.Document", "ContractorId", c => c.Int(null, false, null, null, null, null, null), null);
            base.CreateIndex("dbo.Document", "ContractorId", false, null, false, null);
            base.AddForeignKey("dbo.Document", "ContractorId", "dbo.Contractor", "ContractorId", false, null, null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201402111337443_AddDocumentContractor";
            }
        }

        string IMigrationMetadata.Source
        {
            get
            {
                return null;
            }
        }

        string IMigrationMetadata.Target
        {
            get
            {
                return this.Resources.GetString("Target");
            }
        }
    }
}

