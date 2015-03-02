namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactType",
                c => new
                    {
                        ContactTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.ContactTypeId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 56),
                        DisplayName = c.String(maxLength: 100),
                        UserEmail = c.String(maxLength: 320),
                        Surname = c.String(maxLength: 100),
                        Name = c.String(maxLength: 100),
                        MiddleName = c.String(maxLength: 100),
                        Phone1 = c.String(maxLength: 150),
                        Phone2 = c.String(maxLength: 150),
                        Skype = c.String(maxLength: 150),
                        Comment = c.String(),
                        ContactTypeId = c.Int(),
                        LegalEntityId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.LegalEntity", t => t.LegalEntityId)
                .ForeignKey("dbo.ContactType", t => t.ContactTypeId)
                .Index(t => t.LegalEntityId)
                .Index(t => t.ContactTypeId);
            
            CreateTable(
                "dbo.Contractor",
                c => new
                    {
                        ContractorId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 400),
                        Code = c.String(maxLength: 300),
                        Phone = c.String(maxLength: 150),
                        Fax = c.String(maxLength: 150),
                        Email = c.String(maxLength: 320),
                        ActualAddress = c.String(),
                        Comment = c.String(),
                        ResponsibleId = c.Int(),
                        IsArchived = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ContractorTypeId = c.Int(nullable: false),
                        Region = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ContractorId)
                .ForeignKey("dbo.ContractorType", t => t.ContractorTypeId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfile", t => t.ResponsibleId)
                .Index(t => t.ContractorTypeId)
                .Index(t => t.ResponsibleId);
            
            CreateTable(
                "dbo.ContractorType",
                c => new
                    {
                        ContractorTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ContractorTypeId);
            
            CreateTable(
                "dbo.ContractorTag",
                c => new
                    {
                        ContractorTagId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.ContractorTagId);
            
            CreateTable(
                "dbo.LegalEntity",
                c => new
                    {
                        LegalEntityId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 400),
                        Code = c.String(maxLength: 300),
                        Phone = c.String(maxLength: 150),
                        Fax = c.String(maxLength: 150),
                        Email = c.String(maxLength: 320),
                        IsVATPayer = c.Boolean(nullable: false),
                        ActualAddress = c.String(),
                        Comment = c.String(),
                        Director = c.String(maxLength: 400),
                        ChiefAccountant = c.String(maxLength: 400),
                    })
                .PrimaryKey(t => t.LegalEntityId);
            
            CreateTable(
                "dbo.Warehouse",
                c => new
                    {
                        WarehouseId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 400),
                        Address = c.String(),
                        Comment = c.String(),
                        Code = c.String(maxLength: 300),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.WarehouseId)
                .ForeignKey("dbo.Warehouse", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.ContractorTagContractor",
                c => new
                    {
                        ContractorTagId = c.Int(nullable: false),
                        ContractorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContractorTagId, t.ContractorId })
                .ForeignKey("dbo.Contractor", t => t.ContractorTagId, cascadeDelete: true)
                .ForeignKey("dbo.ContractorTag", t => t.ContractorId, cascadeDelete: true)
                .Index(t => t.ContractorTagId)
                .Index(t => t.ContractorId);
            
            CreateTable(
                "dbo.UserProfileContractor",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ContractorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ContractorId })
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Contractor", t => t.ContractorId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ContractorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Warehouse", "ParentId", "dbo.Warehouse");
            DropForeignKey("dbo.UserProfile", "ContactTypeId", "dbo.ContactType");
            DropForeignKey("dbo.UserProfile", "LegalEntityId", "dbo.LegalEntity");
            DropForeignKey("dbo.UserProfileContractor", "ContractorId", "dbo.Contractor");
            DropForeignKey("dbo.UserProfileContractor", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.ContractorTagContractor", "ContractorId", "dbo.ContractorTag");
            DropForeignKey("dbo.ContractorTagContractor", "ContractorTagId", "dbo.Contractor");
            DropForeignKey("dbo.Contractor", "ResponsibleId", "dbo.UserProfile");
            DropForeignKey("dbo.Contractor", "ContractorTypeId", "dbo.ContractorType");
            DropIndex("dbo.Warehouse", new[] { "ParentId" });
            DropIndex("dbo.UserProfile", new[] { "ContactTypeId" });
            DropIndex("dbo.UserProfile", new[] { "LegalEntityId" });
            DropIndex("dbo.UserProfileContractor", new[] { "ContractorId" });
            DropIndex("dbo.UserProfileContractor", new[] { "UserId" });
            DropIndex("dbo.ContractorTagContractor", new[] { "ContractorId" });
            DropIndex("dbo.ContractorTagContractor", new[] { "ContractorTagId" });
            DropIndex("dbo.Contractor", new[] { "ResponsibleId" });
            DropIndex("dbo.Contractor", new[] { "ContractorTypeId" });
            DropTable("dbo.UserProfileContractor");
            DropTable("dbo.ContractorTagContractor");
            DropTable("dbo.Warehouse");
            DropTable("dbo.LegalEntity");
            DropTable("dbo.ContractorTag");
            DropTable("dbo.ContractorType");
            DropTable("dbo.Contractor");
            DropTable("dbo.UserProfile");
            DropTable("dbo.ContactType");
        }
    }
}
