namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contract",
                c => new
                    {
                        ContractId = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Code = c.String(),
                        CreatedOf = c.DateTime(nullable: false),
                        ContractorId = c.Int(nullable: false),
                        LegalEntityId = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Paid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarriedOut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ContractId)
                .ForeignKey("dbo.Contractor", t => t.ContractorId, cascadeDelete: true)
                .ForeignKey("dbo.LegalEntity", t => t.LegalEntityId, cascadeDelete: true)
                .Index(t => t.ContractorId)
                .Index(t => t.LegalEntityId);
            
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        DocumentId = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        CreatedOf = c.DateTime(nullable: false),
                        PlannedDate = c.DateTime(),
                        FromWarehouseId = c.Int(),
                        ToWarehouseId = c.Int(),
                        ContractId = c.Int(),
                        EmployeeId = c.Int(),
                        IsCommitted = c.Boolean(nullable: false),
                        Comment = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                        DocumentTypeId = c.Int(nullable: false),
                        Employee_UserId = c.Int(),
                        Warehouse_WarehouseId = c.Int(),
                        Warehouse_WarehouseId1 = c.Int(),
                        FromWarehouse_WarehouseId = c.Int(),
                        ToWarehouse_WarehouseId = c.Int(),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.Contract", t => t.ContractId)
                .ForeignKey("dbo.DocumentType", t => t.DocumentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfile", t => t.Employee_UserId)
                .ForeignKey("dbo.Warehouse", t => t.Warehouse_WarehouseId)
                .ForeignKey("dbo.Warehouse", t => t.Warehouse_WarehouseId1)
                .ForeignKey("dbo.Warehouse", t => t.FromWarehouse_WarehouseId)
                .ForeignKey("dbo.Warehouse", t => t.ToWarehouse_WarehouseId)
                .Index(t => t.ContractId)
                .Index(t => t.DocumentTypeId)
                .Index(t => t.Employee_UserId)
                .Index(t => t.Warehouse_WarehouseId)
                .Index(t => t.Warehouse_WarehouseId1)
                .Index(t => t.FromWarehouse_WarehouseId)
                .Index(t => t.ToWarehouse_WarehouseId);
            
            CreateTable(
                "dbo.DocumentType",
                c => new
                    {
                        DocumentTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DocumentTypeId);
            
            CreateTable(
                "dbo.ProductLine",
                c => new
                    {
                        ProductLineId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Code = c.String(),
                        Quantity = c.Int(nullable: false),
                        Reserve = c.Int(nullable: false),
                        Shipped = c.Int(nullable: false),
                        Available = c.Int(nullable: false),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPriceIncludesVAT = c.Boolean(nullable: false),
                        DocumentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductLineId)
                .ForeignKey("dbo.Document", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.DocumentId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Article = c.String(nullable: false, maxLength: 400),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContractorId = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Contractor", t => t.ContractorId, cascadeDelete: true)
                .Index(t => t.ContractorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "ContractorId", "dbo.Contractor");
            DropForeignKey("dbo.Contract", "LegalEntityId", "dbo.LegalEntity");
            DropForeignKey("dbo.Document", "ToWarehouse_WarehouseId", "dbo.Warehouse");
            DropForeignKey("dbo.ProductLine", "DocumentId", "dbo.Document");
            DropForeignKey("dbo.Document", "FromWarehouse_WarehouseId", "dbo.Warehouse");
            DropForeignKey("dbo.Document", "Warehouse_WarehouseId1", "dbo.Warehouse");
            DropForeignKey("dbo.Document", "Warehouse_WarehouseId", "dbo.Warehouse");
            DropForeignKey("dbo.Document", "Employee_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Document", "DocumentTypeId", "dbo.DocumentType");
            DropForeignKey("dbo.Document", "ContractId", "dbo.Contract");
            DropForeignKey("dbo.Contract", "ContractorId", "dbo.Contractor");
            DropIndex("dbo.Product", new[] { "ContractorId" });
            DropIndex("dbo.Contract", new[] { "LegalEntityId" });
            DropIndex("dbo.Document", new[] { "ToWarehouse_WarehouseId" });
            DropIndex("dbo.ProductLine", new[] { "DocumentId" });
            DropIndex("dbo.Document", new[] { "FromWarehouse_WarehouseId" });
            DropIndex("dbo.Document", new[] { "Warehouse_WarehouseId1" });
            DropIndex("dbo.Document", new[] { "Warehouse_WarehouseId" });
            DropIndex("dbo.Document", new[] { "Employee_UserId" });
            DropIndex("dbo.Document", new[] { "DocumentTypeId" });
            DropIndex("dbo.Document", new[] { "ContractId" });
            DropIndex("dbo.Contract", new[] { "ContractorId" });
            DropTable("dbo.Product");
            DropTable("dbo.ProductLine");
            DropTable("dbo.DocumentType");
            DropTable("dbo.Document");
            DropTable("dbo.Contract");
        }
    }
}
