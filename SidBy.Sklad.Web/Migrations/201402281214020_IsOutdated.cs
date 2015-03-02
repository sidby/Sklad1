namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsOutdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "IsReportOutdated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Document", "IsReportOutdated");
        }
    }
}
