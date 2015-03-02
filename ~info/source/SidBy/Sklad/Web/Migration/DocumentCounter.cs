namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class DocumentCounter : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(DocumentCounter));

        public override void Down()
        {
            base.DropForeignKey("dbo.DocumentCounter", "DocumentTypeId", "dbo.DocumentType", null);
            base.DropIndex("dbo.DocumentCounter", new string[] { "DocumentTypeId" }, null);
            base.DropTable("dbo.DocumentCounter", null);
        }

        public override void Up()
        {
            base.CreateTable("dbo.DocumentCounter", delegate (ColumnBuilder c) {
                int? nullable = null;
                nullable = null;
                nullable = null;
                return new { DocumentCounterId = c.Int(false, true, nullable, null, null, null, null), Counter = c.Int(false, false, nullable, null, null, null, null), Year = c.Int(false, false, nullable, null, null, null, null), DocumentTypeId = c.Int(false, false, null, null, null, null, null) };
            }, null).PrimaryKey(t => t.DocumentCounterId, null, true, null).ForeignKey("dbo.DocumentType", t => t.DocumentTypeId, true, null, null).Index(t => t.DocumentTypeId, null, false, false, null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201401310710411_DocumentCounter";
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

