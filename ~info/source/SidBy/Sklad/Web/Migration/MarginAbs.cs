namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class MarginAbs : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(MarginAbs));

        public override void Down()
        {
            base.DropColumn("dbo.ProductLine", "MarginAbs", null);
        }

        public override void Up()
        {
            base.AddColumn("dbo.ProductLine", "MarginAbs", c => c.Decimal(false, 0x12, 2, null, null, null, null, false, null), null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201402260735234_MarginAbs";
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

