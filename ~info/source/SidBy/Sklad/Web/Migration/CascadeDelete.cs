namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class CascadeDelete : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(CascadeDelete));

        public override void Down()
        {
        }

        public override void Up()
        {
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201402101233223_CascadeDelete";
            }
        }

        string IMigrationMetadata.Source
        {
            get
            {
                return this.Resources.GetString("Source");
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

