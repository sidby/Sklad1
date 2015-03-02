namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class ProductLine_ProductOptional : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(ProductLine_ProductOptional));

        public override void Down()
        {
            base.DropForeignKey("dbo.ProductLine", "ProductId", "dbo.Product", null);
            base.DropIndex("dbo.ProductLine", new string[] { "ProductId" }, null);
            base.AlterColumn("dbo.ProductLine", "ProductId", c => c.Int(false, false, null, null, null, null, null), null);
            base.CreateIndex("dbo.ProductLine", "ProductId", false, null, false, null);
            base.AddForeignKey("dbo.ProductLine", "ProductId", "dbo.Product", "ProductId", true, null, null);
        }

        public override void Up()
        {
            base.DropForeignKey("dbo.ProductLine", "ProductId", "dbo.Product", null);
            base.DropIndex("dbo.ProductLine", new string[] { "ProductId" }, null);
            base.AlterColumn("dbo.ProductLine", "ProductId", c => c.Int(null, false, null, null, null, null, null), null);
            base.CreateIndex("dbo.ProductLine", "ProductId", false, null, false, null);
            base.AddForeignKey("dbo.ProductLine", "ProductId", "dbo.Product", "ProductId", false, null, null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201402060956447_ProductLine_ProductOptional";
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

