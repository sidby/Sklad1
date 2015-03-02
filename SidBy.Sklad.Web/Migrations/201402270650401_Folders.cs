namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Folders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "CommonFolderName", c => c.String(nullable: false));
            AddColumn("dbo.Document", "SecureFolderName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Document", "SecureFolderName");
            DropColumn("dbo.Document", "CommonFolderName");
        }
    }
}
