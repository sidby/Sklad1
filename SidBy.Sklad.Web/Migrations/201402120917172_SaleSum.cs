namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaleSum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "SaleSum", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ProductLine", "SaleSum", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductLine", "SaleSum");
            DropColumn("dbo.Document", "SaleSum");
        }
    }
}
