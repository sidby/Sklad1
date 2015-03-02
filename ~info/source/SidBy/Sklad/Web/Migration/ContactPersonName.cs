namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class ContactPersonName : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(ContactPersonName));

        public override void Down()
        {
            base.DropColumn("dbo.Contractor", "ContactPersonName", null);
        }

        public override void Up()
        {
            base.AddColumn("dbo.Contractor", "ContactPersonName", delegate (ColumnBuilder c) {
                bool? nullable = null;
                nullable = null;
                return c.String(nullable, 100, nullable, null, null, null, null, null, null);
            }, null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201403041255386_ContactPersonName";
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

