namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class ProductLineComment : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(ProductLineComment));

        public override void Down()
        {
            base.DropColumn("dbo.ProductLine", "Comment", null);
        }

        public override void Up()
        {
            base.AddColumn("dbo.ProductLine", "Comment", delegate (ColumnBuilder c) {
                bool? nullable = null;
                nullable = null;
                return c.String(nullable, null, nullable, null, null, null, null, null, null);
            }, null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201401310822286_ProductLineComment";
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

