namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class ProductAttributes : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(ProductAttributes));

        public override void Down()
        {
            base.DropColumn("dbo.Product", "Available", null);
            base.DropColumn("dbo.Product", "Pending", null);
            base.DropColumn("dbo.Product", "Reserve", null);
            base.DropColumn("dbo.Product", "Remains", null);
        }

        public override void Up()
        {
            base.AddColumn("dbo.Product", "Remains", c => c.Int(false, false, null, null, null, null, null), null);
            base.AddColumn("dbo.Product", "Reserve", c => c.Int(false, false, null, null, null, null, null), null);
            base.AddColumn("dbo.Product", "Pending", c => c.Int(false, false, null, null, null, null, null), null);
            base.AddColumn("dbo.Product", "Available", c => c.Int(false, false, null, null, null, null, null), null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201403061410075_ProductAttributes";
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

