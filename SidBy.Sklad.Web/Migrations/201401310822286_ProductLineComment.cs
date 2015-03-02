namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductLineComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductLine", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductLine", "Comment");
        }
    }
}
