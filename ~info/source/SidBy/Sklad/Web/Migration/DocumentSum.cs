namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class DocumentSum : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(DocumentSum));

        public override void Down()
        {
            base.DropColumn("dbo.Document", "Sum", null);
        }

        public override void Up()
        {
            base.AddColumn("dbo.Document", "Sum", c => c.Decimal(false, 0x12, 2, null, null, null, null, false, null), null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201401311230228_DocumentSum";
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

