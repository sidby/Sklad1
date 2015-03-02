namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductLine_ProductOptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductLine", "ProductId", "dbo.Product");
            DropIndex("dbo.ProductLine", new[] { "ProductId" });
            AlterColumn("dbo.ProductLine", "ProductId", c => c.Int());
            CreateIndex("dbo.ProductLine", "ProductId");
            AddForeignKey("dbo.ProductLine", "ProductId", "dbo.Product", "ProductId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductLine", "ProductId", "dbo.Product");
            DropIndex("dbo.ProductLine", new[] { "ProductId" });
            AlterColumn("dbo.ProductLine", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductLine", "ProductId");
            AddForeignKey("dbo.ProductLine", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
        }
    }
}
