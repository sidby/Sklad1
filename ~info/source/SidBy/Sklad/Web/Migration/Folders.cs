namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class Folders : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(Folders));

        public override void Down()
        {
            base.DropColumn("dbo.Document", "SecureFolderName", null);
            base.DropColumn("dbo.Document", "CommonFolderName", null);
        }

        public override void Up()
        {
            base.AddColumn("dbo.Document", "CommonFolderName", c => c.String(false, null, null, null, null, null, null, null, null), null);
            base.AddColumn("dbo.Document", "SecureFolderName", c => c.String(false, null, null, null, null, null, null, null, null), null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201402270650401_Folders";
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

