namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class IsOutdated : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(IsOutdated));

        public override void Down()
        {
            base.DropColumn("dbo.Document", "IsReportOutdated", null);
        }

        public override void Up()
        {
            base.AddColumn("dbo.Document", "IsReportOutdated", c => c.Boolean(false, null, null, null, null, null), null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201402281214020_IsOutdated";
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

