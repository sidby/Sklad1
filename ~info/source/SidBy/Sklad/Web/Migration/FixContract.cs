namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class FixContract : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(FixContract));

        public override void Down()
        {
            base.AddColumn("dbo.Contract", "UserProfile_UserId", c => c.Int(null, false, null, null, null, null, null), null);
            base.CreateIndex("dbo.Contract", "UserProfile_UserId", false, null, false, null);
            base.AddForeignKey("dbo.Contract", "UserProfile_UserId", "dbo.UserProfile", "UserId", false, null, null);
        }

        public override void Up()
        {
            base.DropForeignKey("dbo.Contract", "UserProfile_UserId", "dbo.UserProfile", null);
            base.DropIndex("dbo.Contract", new string[] { "UserProfile_UserId" }, null);
            base.DropColumn("dbo.Contract", "UserProfile_UserId", null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201401310627435_Fix-Contract";
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

