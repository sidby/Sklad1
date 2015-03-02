namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactPersonName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractor", "ContactPersonName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contractor", "ContactPersonName");
        }
    }
}
