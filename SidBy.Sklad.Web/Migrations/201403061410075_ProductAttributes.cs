namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductAttributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Remains", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Reserve", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Pending", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Available", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Available");
            DropColumn("dbo.Product", "Pending");
            DropColumn("dbo.Product", "Reserve");
            DropColumn("dbo.Product", "Remains");
        }
    }
}
