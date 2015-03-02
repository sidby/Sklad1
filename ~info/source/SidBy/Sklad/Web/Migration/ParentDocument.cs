namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class ParentDocument : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(ParentDocument));

        public override void Down()
        {
            base.DropForeignKey("dbo.Document", "ParentDocumentId", "dbo.Document", null);
            base.DropIndex("dbo.Document", new string[] { "ParentDocumentId" }, null);
            base.DropColumn("dbo.Document", "ParentDocumentId", null);
        }

        public override void Up()
        {
            base.AddColumn("dbo.Document", "ParentDocumentId", c => c.Int(null, false, null, null, null, null, null), null);
            base.CreateIndex("dbo.Document", "ParentDocumentId", false, null, false, null);
            base.AddForeignKey("dbo.Document", "ParentDocumentId", "dbo.Document", "DocumentId", false, null, null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201402111103194_ParentDocument";
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

