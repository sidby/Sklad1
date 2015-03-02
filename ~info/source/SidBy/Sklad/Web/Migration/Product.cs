namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class Product : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(Product));

        public override void Down()
        {
            base.DropForeignKey("dbo.Product", "ContractorId", "dbo.Contractor", null);
            base.DropForeignKey("dbo.Contract", "LegalEntityId", "dbo.LegalEntity", null);
            base.DropForeignKey("dbo.Document", "ToWarehouse_WarehouseId", "dbo.Warehouse", null);
            base.DropForeignKey("dbo.ProductLine", "DocumentId", "dbo.Document", null);
            base.DropForeignKey("dbo.Document", "FromWarehouse_WarehouseId", "dbo.Warehouse", null);
            base.DropForeignKey("dbo.Document", "Warehouse_WarehouseId1", "dbo.Warehouse", null);
            base.DropForeignKey("dbo.Document", "Warehouse_WarehouseId", "dbo.Warehouse", null);
            base.DropForeignKey("dbo.Document", "Employee_UserId", "dbo.UserProfile", null);
            base.DropForeignKey("dbo.Document", "DocumentTypeId", "dbo.DocumentType", null);
            base.DropForeignKey("dbo.Document", "ContractId", "dbo.Contract", null);
            base.DropForeignKey("dbo.Contract", "ContractorId", "dbo.Contractor", null);
            base.DropIndex("dbo.Product", new string[] { "ContractorId" }, null);
            base.DropIndex("dbo.Contract", new string[] { "LegalEntityId" }, null);
            base.DropIndex("dbo.Document", new string[] { "ToWarehouse_WarehouseId" }, null);
            base.DropIndex("dbo.ProductLine", new string[] { "DocumentId" }, null);
            base.DropIndex("dbo.Document", new string[] { "FromWarehouse_WarehouseId" }, null);
            base.DropIndex("dbo.Document", new string[] { "Warehouse_WarehouseId1" }, null);
            base.DropIndex("dbo.Document", new string[] { "Warehouse_WarehouseId" }, null);
            base.DropIndex("dbo.Document", new string[] { "Employee_UserId" }, null);
            base.DropIndex("dbo.Document", new string[] { "DocumentTypeId" }, null);
            base.DropIndex("dbo.Document", new string[] { "ContractId" }, null);
            base.DropIndex("dbo.Contract", new string[] { "ContractorId" }, null);
            base.DropTable("dbo.Product", null);
            base.DropTable("dbo.ProductLine", null);
            base.DropTable("dbo.DocumentType", null);
            base.DropTable("dbo.Document", null);
            base.DropTable("dbo.Contract", null);
        }

        public override void Up()
        {
            base.CreateTable("dbo.Contract", delegate (ColumnBuilder c) {
                int? nullable = null;
                bool? nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                byte? nullable3 = null;
                DateTime? nullable4 = null;
                nullable = null;
                nullable = null;
                decimal? nullable5 = null;
                nullable5 = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                nullable3 = null;
                nullable4 = null;
                return new { ContractId = c.Int(false, true, nullable, null, null, null, null), Number = c.String(nullable2, nullable, nullable2, nullable2, null, null, null, null, null), Code = c.String(nullable2, nullable, nullable2, nullable2, null, null, null, null, null), CreatedOf = c.DateTime(false, nullable3, nullable4, null, null, null, null), ContractorId = c.Int(false, false, nullable, null, null, null, null), LegalEntityId = c.Int(false, false, nullable, null, null, null, null), Sum = c.Decimal(false, 0x12, 2, nullable5, null, null, null, false, null), Paid = c.Decimal(false, 0x12, 2, nullable5, null, null, null, false, null), CarriedOut = c.Decimal(false, 0x12, 2, null, null, null, null, false, null), Comment = c.String(nullable2, null, nullable2, nullable2, null, null, null, null, null), CreatedAt = c.DateTime(false, nullable3, nullable4, null, null, null, null), ModifiedAt = c.DateTime(null, null, null, null, null, null, null) };
            }, null).PrimaryKey(t => t.ContractId, null, true, null).ForeignKey("dbo.Contractor", t => t.ContractorId, true, null, null).ForeignKey("dbo.LegalEntity", t => t.LegalEntityId, true, null, null).Index(t => t.ContractorId, null, false, false, null).Index(t => t.LegalEntityId, null, false, false, null);
            base.CreateTable("dbo.Document", delegate (ColumnBuilder c) {
                int? nullable = null;
                bool? nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                byte? nullable3 = null;
                DateTime? nullable4 = null;
                nullable2 = null;
                nullable3 = null;
                nullable4 = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                nullable3 = null;
                nullable4 = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable = null;
                return new { 
                    DocumentId = c.Int(false, true, nullable, null, null, null, null), Number = c.String(nullable2, nullable, nullable2, nullable2, null, null, null, null, null), CreatedOf = c.DateTime(false, nullable3, nullable4, null, null, null, null), PlannedDate = c.DateTime(nullable2, nullable3, nullable4, null, null, null, null), FromWarehouseId = c.Int(nullable2, false, nullable, null, null, null, null), ToWarehouseId = c.Int(nullable2, false, nullable, null, null, null, null), ContractId = c.Int(nullable2, false, nullable, null, null, null, null), EmployeeId = c.Int(nullable2, false, nullable, null, null, null, null), IsCommitted = c.Boolean(false, nullable2, null, null, null, null), Comment = c.String(nullable2, nullable, nullable2, nullable2, null, null, null, null, null), CreatedAt = c.DateTime(false, nullable3, nullable4, null, null, null, null), ModifiedAt = c.DateTime(nullable2, null, null, null, null, null, null), DocumentTypeId = c.Int(false, false, nullable, null, null, null, null), Employee_UserId = c.Int(nullable2, false, nullable, null, null, null, null), Warehouse_WarehouseId = c.Int(nullable2, false, nullable, null, null, null, null), Warehouse_WarehouseId1 = c.Int(nullable2, false, nullable, null, null, null, null), 
                    FromWarehouse_WarehouseId = c.Int(nullable2, false, nullable, null, null, null, null), ToWarehouse_WarehouseId = c.Int(null, false, null, null, null, null, null)
                 };
            }, null).PrimaryKey(t => t.DocumentId, null, true, null).ForeignKey("dbo.Contract", t => t.ContractId, false, null, null).ForeignKey("dbo.DocumentType", t => t.DocumentTypeId, true, null, null).ForeignKey("dbo.UserProfile", t => t.Employee_UserId, false, null, null).ForeignKey("dbo.Warehouse", t => t.Warehouse_WarehouseId, false, null, null).ForeignKey("dbo.Warehouse", t => t.Warehouse_WarehouseId1, false, null, null).ForeignKey("dbo.Warehouse", t => t.FromWarehouse_WarehouseId, false, null, null).ForeignKey("dbo.Warehouse", t => t.ToWarehouse_WarehouseId, false, null, null).Index(t => t.ContractId, null, false, false, null).Index(t => t.DocumentTypeId, null, false, false, null).Index(t => t.Employee_UserId, null, false, false, null).Index(t => t.Warehouse_WarehouseId, null, false, false, null).Index(t => t.Warehouse_WarehouseId1, null, false, false, null).Index(t => t.FromWarehouse_WarehouseId, null, false, false, null).Index(t => t.ToWarehouse_WarehouseId, null, false, false, null);
            base.CreateTable("dbo.DocumentType", delegate (ColumnBuilder c) {
                int? nullable = null;
                bool? nullable2 = null;
                nullable2 = null;
                return new { DocumentTypeId = c.Int(false, true, nullable, null, null, null, null), Name = c.String(nullable2, null, nullable2, null, null, null, null, null, null) };
            }, null).PrimaryKey(t => t.DocumentTypeId, null, true, null);
            base.CreateTable("dbo.ProductLine", delegate (ColumnBuilder c) {
                int? nullable = null;
                nullable = null;
                bool? nullable2 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                nullable = null;
                nullable = null;
                nullable = null;
                nullable = null;
                decimal? nullable3 = null;
                nullable3 = null;
                return new { ProductLineId = c.Int(false, true, nullable, null, null, null, null), ProductId = c.Int(false, false, nullable, null, null, null, null), Code = c.String(nullable2, nullable, nullable2, nullable2, null, null, null, null, null), Quantity = c.Int(false, false, nullable, null, null, null, null), Reserve = c.Int(false, false, nullable, null, null, null, null), Shipped = c.Int(false, false, nullable, null, null, null, null), Available = c.Int(false, false, nullable, null, null, null, null), PurchasePrice = c.Decimal(false, 0x12, 2, nullable3, null, null, null, false, null), SellingPrice = c.Decimal(false, 0x12, 2, nullable3, null, null, null, false, null), VAT = c.Decimal(false, 0x12, 2, null, null, null, null, false, null), IsPriceIncludesVAT = c.Boolean(false, null, null, null, null, null), DocumentId = c.Int(false, false, null, null, null, null, null) };
            }, null).PrimaryKey(t => t.ProductLineId, null, true, null).ForeignKey("dbo.Document", t => t.DocumentId, true, null, null).Index(t => t.DocumentId, null, false, false, null);
            base.CreateTable("dbo.Product", delegate (ColumnBuilder c) {
                int? nullable = null;
                bool? nullable2 = null;
                nullable2 = null;
                decimal? nullable3 = null;
                nullable3 = null;
                nullable = null;
                nullable2 = null;
                nullable2 = null;
                nullable2 = null;
                byte? nullable4 = null;
                DateTime? nullable5 = null;
                return new { ProductId = c.Int(false, true, nullable, null, null, null, null), Article = c.String(false, 400, nullable2, nullable2, null, null, null, null, null), PurchasePrice = c.Decimal(false, 0x12, 2, nullable3, null, null, null, false, null), SalePrice = c.Decimal(false, 0x12, 2, nullable3, null, null, null, false, null), VAT = c.Decimal(false, 0x12, 2, null, null, null, null, false, null), ContractorId = c.Int(false, false, nullable, null, null, null, null), Description = c.String(nullable2, null, nullable2, nullable2, null, null, null, null, null), CreatedAt = c.DateTime(false, nullable4, nullable5, null, null, null, null), ModifiedAt = c.DateTime(null, null, null, null, null, null, null) };
            }, null).PrimaryKey(t => t.ProductId, null, true, null).ForeignKey("dbo.Contractor", t => t.ContractorId, true, null, null).Index(t => t.ContractorId, null, false, false, null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201401280631215_Product";
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

