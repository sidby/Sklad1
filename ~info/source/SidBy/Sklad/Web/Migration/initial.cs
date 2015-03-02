namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class initial : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(initial));

        public override void Down()
        {
            base.DropForeignKey("dbo.Warehouse", "ParentId", "dbo.Warehouse", null);
            base.DropForeignKey("dbo.UserProfile", "ContactTypeId", "dbo.ContactType", null);
            base.DropForeignKey("dbo.UserProfile", "LegalEntityId", "dbo.LegalEntity", null);
            base.DropForeignKey("dbo.UserProfileContractor", "ContractorId", "dbo.Contractor", null);
            base.DropForeignKey("dbo.UserProfileContractor", "UserId", "dbo.UserProfile", null);
            base.DropForeignKey("dbo.ContractorTagContractor", "ContractorId", "dbo.ContractorTag", null);
            base.DropForeignKey("dbo.ContractorTagContractor", "ContractorTagId", "dbo.Contractor", null);
            base.DropForeignKey("dbo.Contractor", "ResponsibleId", "dbo.UserProfile", null);
            base.DropForeignKey("dbo.Contractor", "ContractorTypeId", "dbo.ContractorType", null);
            base.DropIndex("dbo.Warehouse", new string[] { "ParentId" }, null);
            base.DropIndex("dbo.UserProfile", new string[] { "ContactTypeId" }, null);
            base.DropIndex("dbo.UserProfile", new string[] { "LegalEntityId" }, null);
            base.DropIndex("dbo.UserProfileContractor", new string[] { "ContractorId" }, null);
            base.DropIndex("dbo.UserProfileContractor", new string[] { "UserId" }, null);
            base.DropIndex("dbo.ContractorTagContractor", new string[] { "ContractorId" }, null);
            base.DropIndex("dbo.ContractorTagContractor", new string[] { "ContractorTagId" }, null);
            base.DropIndex("dbo.Contractor", new string[] { "ResponsibleId" }, null);
            base.DropIndex("dbo.Contractor", new string[] { "ContractorTypeId" }, null);
            base.DropTable("dbo.UserProfileContractor", null);
            base.DropTable("dbo.ContractorTagContractor", null);
            base.DropTable("dbo.Warehouse", null);
            base.DropTable("dbo.LegalEntity", null);
            base.DropTable("dbo.ContractorTag", null);
            base.DropTable("dbo.ContractorType", null);
            base.DropTable("dbo.Contractor", null);
            base.DropTable("dbo.UserProfile", null);
            base.DropTable("dbo.ContactType", null);
        }

        public override void Up()
        {
            base.CreateTable("dbo.ContactType", c => new { ContactTypeId = c.Int(false, true, null, null, null, null, null), Name = c.String(false, 400, null, null, null, null, null, null, null) }, null).PrimaryKey(t => t.ContactTypeId, null, true, null);
            base.CreateTable("dbo.UserProfile", delegate (ColumnBuilder c) {
                int? nullable = null;
                bool? nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable = null;
                return new { UserId = c.Int(false, true, nullable, null, null, null, null), UserName = c.String(false, 0x38, nullable2, nullable2, null, null, null, null, null), DisplayName = c.String(nullable2, 100, nullable2, nullable2, null, null, null, null, null), UserEmail = c.String(nullable2, 320, nullable2, nullable2, null, null, null, null, null), Surname = c.String(nullable2, 100, nullable2, nullable2, null, null, null, null, null), Name = c.String(nullable2, 100, nullable2, nullable2, null, null, null, null, null), MiddleName = c.String(nullable2, 100, nullable2, nullable2, null, null, null, null, null), Phone1 = c.String(nullable2, 150, nullable2, nullable2, null, null, null, null, null), Phone2 = c.String(nullable2, 150, nullable2, nullable2, null, null, null, null, null), Skype = c.String(nullable2, 150, nullable2, nullable2, null, null, null, null, null), Comment = c.String(nullable2, nullable, nullable2, nullable2, null, null, null, null, null), ContactTypeId = c.Int(nullable2, false, nullable, null, null, null, null), LegalEntityId = c.Int(null, false, null, null, null, null, null) };
            }, null).PrimaryKey(t => t.UserId, null, true, null).ForeignKey("dbo.LegalEntity", t => t.LegalEntityId, false, null, null).ForeignKey("dbo.ContactType", t => t.ContactTypeId, false, null, null).Index(t => t.LegalEntityId, null, false, false, null).Index(t => t.ContactTypeId, null, false, false, null);
            base.CreateTable("dbo.Contractor", delegate (ColumnBuilder c) {
                int? nullable = null;
                bool? nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                return new { ContractorId = c.Int(false, true, nullable, null, null, null, null), Name = c.String(false, 400, nullable2, nullable2, null, null, null, null, null), Code = c.String(nullable2, 300, nullable2, nullable2, null, null, null, null, null), Phone = c.String(nullable2, 150, nullable2, nullable2, null, null, null, null, null), Fax = c.String(nullable2, 150, nullable2, nullable2, null, null, null, null, null), Email = c.String(nullable2, 320, nullable2, nullable2, null, null, null, null, null), ActualAddress = c.String(nullable2, nullable, nullable2, nullable2, null, null, null, null, null), Comment = c.String(nullable2, nullable, nullable2, nullable2, null, null, null, null, null), ResponsibleId = c.Int(nullable2, false, nullable, null, null, null, null), IsArchived = c.Boolean(false, nullable2, null, null, null, null), CreatedAt = c.DateTime(false, null, null, null, null, null, null), ContractorTypeId = c.Int(false, false, null, null, null, null, null), Region = c.String(nullable2, 150, nullable2, null, null, null, null, null, null) };
            }, null).PrimaryKey(t => t.ContractorId, null, true, null).ForeignKey("dbo.ContractorType", t => t.ContractorTypeId, true, null, null).ForeignKey("dbo.UserProfile", t => t.ResponsibleId, false, null, null).Index(t => t.ContractorTypeId, null, false, false, null).Index(t => t.ResponsibleId, null, false, false, null);
            base.CreateTable("dbo.ContractorType", delegate (ColumnBuilder c) {
                int? nullable = null;
                bool? nullable2 = null;
                nullable2 = null;
                return new { ContractorTypeId = c.Int(false, true, nullable, null, null, null, null), Name = c.String(nullable2, null, nullable2, null, null, null, null, null, null) };
            }, null).PrimaryKey(t => t.ContractorTypeId, null, true, null);
            base.CreateTable("dbo.ContractorTag", c => new { ContractorTagId = c.Int(false, true, null, null, null, null, null), Name = c.String(false, 400, null, null, null, null, null, null, null) }, null).PrimaryKey(t => t.ContractorTagId, null, true, null);
            base.CreateTable("dbo.LegalEntity", delegate (ColumnBuilder c) {
                int? nullable = null;
                bool? nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                return new { LegalEntityId = c.Int(false, true, nullable, null, null, null, null), Name = c.String(false, 400, nullable2, nullable2, null, null, null, null, null), Code = c.String(nullable2, 300, nullable2, nullable2, null, null, null, null, null), Phone = c.String(nullable2, 150, nullable2, nullable2, null, null, null, null, null), Fax = c.String(nullable2, 150, nullable2, nullable2, null, null, null, null, null), Email = c.String(nullable2, 320, nullable2, nullable2, null, null, null, null, null), IsVATPayer = c.Boolean(false, nullable2, null, null, null, null), ActualAddress = c.String(nullable2, nullable, nullable2, nullable2, null, null, null, null, null), Comment = c.String(nullable2, null, nullable2, nullable2, null, null, null, null, null), Director = c.String(nullable2, 400, nullable2, nullable2, null, null, null, null, null), ChiefAccountant = c.String(nullable2, 400, nullable2, null, null, null, null, null, null) };
            }, null).PrimaryKey(t => t.LegalEntityId, null, true, null);
            base.CreateTable("dbo.Warehouse", delegate (ColumnBuilder c) {
                int? nullable = null;
                bool? nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                return new { WarehouseId = c.Int(false, true, nullable, null, null, null, null), Name = c.String(false, 400, nullable2, nullable2, null, null, null, null, null), Address = c.String(nullable2, nullable, nullable2, nullable2, null, null, null, null, null), Comment = c.String(nullable2, nullable, nullable2, nullable2, null, null, null, null, null), Code = c.String(nullable2, 300, nullable2, nullable2, null, null, null, null, null), ParentId = c.Int(null, false, null, null, null, null, null) };
            }, null).PrimaryKey(t => t.WarehouseId, null, true, null).ForeignKey("dbo.Warehouse", t => t.ParentId, false, null, null).Index(t => t.ParentId, null, false, false, null);
            base.CreateTable("dbo.ContractorTagContractor", c => new { ContractorTagId = c.Int(false, false, null, null, null, null, null), ContractorId = c.Int(false, false, null, null, null, null, null) }, null).PrimaryKey(t => new { ContractorTagId = t.ContractorTagId, ContractorId = t.ContractorId }, null, true, null).ForeignKey("dbo.Contractor", t => t.ContractorTagId, true, null, null).ForeignKey("dbo.ContractorTag", t => t.ContractorId, true, null, null).Index(t => t.ContractorTagId, null, false, false, null).Index(t => t.ContractorId, null, false, false, null);
            base.CreateTable("dbo.UserProfileContractor", c => new { UserId = c.Int(false, false, null, null, null, null, null), ContractorId = c.Int(false, false, null, null, null, null, null) }, null).PrimaryKey(t => new { UserId = t.UserId, ContractorId = t.ContractorId }, null, true, null).ForeignKey("dbo.UserProfile", t => t.UserId, true, null, null).ForeignKey("dbo.Contractor", t => t.ContractorId, true, null, null).Index(t => t.UserId, null, false, false, null).Index(t => t.ContractorId, null, false, false, null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201401061809049_initial";
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

