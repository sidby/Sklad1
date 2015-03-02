namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Folders36 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Document", "CommonFolderName", c => c.String(nullable: false, maxLength: 36));
            AlterColumn("dbo.Document", "SecureFolderName", c => c.String(nullable: false, maxLength: 36));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Document", "SecureFolderName", c => c.String(nullable: false));
            AlterColumn("dbo.Document", "CommonFolderName", c => c.String(nullable: false));
        }
    }
}
