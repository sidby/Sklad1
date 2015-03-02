namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarginAbs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductLine", "MarginAbs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductLine", "MarginAbs");
        }
    }
}
