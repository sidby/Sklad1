namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class Folders36 : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(Folders36));

        public override void Down()
        {
            base.AlterColumn("dbo.Document", "SecureFolderName", c => c.String(false, null, null, null, null, null, null, null, null), null);
            base.AlterColumn("dbo.Document", "CommonFolderName", c => c.String(false, null, null, null, null, null, null, null, null), null);
        }

        public override void Up()
        {
            base.AlterColumn("dbo.Document", "CommonFolderName", c => c.String(false, 0x24, null, null, null, null, null, null, null), null);
            base.AlterColumn("dbo.Document", "SecureFolderName", c => c.String(false, 0x24, null, null, null, null, null, null, null), null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201402270656434_Folders36";
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

