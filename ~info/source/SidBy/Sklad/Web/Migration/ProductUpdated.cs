namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.0.1-21010")]
    public sealed class ProductUpdated : DbMigration, IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(ProductUpdated));

        public override void Down()
        {
            base.AddColumn("dbo.ProductLine", "SellingPrice", c => c.Decimal(false, 0x12, 2, null, null, null, null, false, null), null);
            base.AddColumn("dbo.Document", "Warehouse_WarehouseId1", c => c.Int(null, false, null, null, null, null, null), null);
            base.AddColumn("dbo.Document", "Warehouse_WarehouseId", c => c.Int(null, false, null, null, null, null, null), null);
            base.DropForeignKey("dbo.Document", "ToWarehouseId", "dbo.Warehouse", null);
            base.DropForeignKey("dbo.Document", "FromWarehouseId", "dbo.Warehouse", null);
            base.DropForeignKey("dbo.Contract", "UserProfile_UserId", "dbo.UserProfile", null);
            base.DropForeignKey("dbo.ProductLine", "ProductId", "dbo.Product", null);
            base.DropIndex("dbo.Document", new string[] { "ToWarehouseId" }, null);
            base.DropIndex("dbo.Document", new string[] { "FromWarehouseId" }, null);
            base.DropIndex("dbo.Contract", new string[] { "UserProfile_UserId" }, null);
            base.DropIndex("dbo.ProductLine", new string[] { "ProductId" }, null);
            base.AlterColumn("dbo.ProductLine", "Code", delegate (ColumnBuilder c) {
                bool? nullable = null;
                nullable = null;
                return c.String(nullable, null, nullable, null, null, null, null, null, null);
            }, null);
            base.AlterColumn("dbo.DocumentType", "Name", delegate (ColumnBuilder c) {
                bool? nullable = null;
                nullable = null;
                return c.String(nullable, null, nullable, null, null, null, null, null, null);
            }, null);
            base.AlterColumn("dbo.Document", "Number", delegate (ColumnBuilder c) {
                bool? nullable = null;
                nullable = null;
                return c.String(nullable, null, nullable, null, null, null, null, null, null);
            }, null);
            base.AlterColumn("dbo.Contract", "Number", delegate (ColumnBuilder c) {
                bool? nullable = null;
                nullable = null;
                return c.String(nullable, null, nullable, null, null, null, null, null, null);
            }, null);
            base.DropColumn("dbo.ProductLine", "SalePrice", null);
            base.DropColumn("dbo.Contract", "UserProfile_UserId", null);
            base.RenameColumn("dbo.Document", "ToWarehouseId", "ToWarehouse_WarehouseId", null);
            base.RenameColumn("dbo.Document", "FromWarehouseId", "FromWarehouse_WarehouseId", null);
            base.RenameColumn("dbo.Document", "EmployeeId", "Employee_UserId", null);
            base.AddColumn("dbo.Document", "ToWarehouseId", c => c.Int(null, false, null, null, null, null, null), null);
            base.AddColumn("dbo.Document", "FromWarehouseId", c => c.Int(null, false, null, null, null, null, null), null);
            base.AddColumn("dbo.Document", "EmployeeId", c => c.Int(null, false, null, null, null, null, null), null);
            base.CreateIndex("dbo.Document", "Warehouse_WarehouseId1", false, null, false, null);
            base.CreateIndex("dbo.Document", "Warehouse_WarehouseId", false, null, false, null);
            base.AddForeignKey("dbo.Document", "Warehouse_WarehouseId1", "dbo.Warehouse", "WarehouseId", false, null, null);
            base.AddForeignKey("dbo.Document", "Warehouse_WarehouseId", "dbo.Warehouse", "WarehouseId", false, null, null);
        }

        public override void Up()
        {
            base.DropForeignKey("dbo.Document", "Warehouse_WarehouseId", "dbo.Warehouse", null);
            base.DropForeignKey("dbo.Document", "Warehouse_WarehouseId1", "dbo.Warehouse", null);
            base.DropIndex("dbo.Document", new string[] { "Warehouse_WarehouseId" }, null);
            base.DropIndex("dbo.Document", new string[] { "Warehouse_WarehouseId1" }, null);
            base.DropColumn("dbo.Document", "EmployeeId", null);
            base.DropColumn("dbo.Document", "FromWarehouseId", null);
            base.DropColumn("dbo.Document", "ToWarehouseId", null);
            base.RenameColumn("dbo.Document", "Employee_UserId", "EmployeeId", null);
            base.RenameColumn("dbo.Document", "FromWarehouse_WarehouseId", "FromWarehouseId", null);
            base.RenameColumn("dbo.Document", "ToWarehouse_WarehouseId", "ToWarehouseId", null);
            base.AddColumn("dbo.Contract", "UserProfile_UserId", c => c.Int(null, false, null, null, null, null, null), null);
            base.AddColumn("dbo.ProductLine", "SalePrice", c => c.Decimal(false, 0x12, 2, null, null, null, null, false, null), null);
            base.AlterColumn("dbo.Contract", "Number", c => c.String(false, 400, null, null, null, null, null, null, null), null);
            base.AlterColumn("dbo.Document", "Number", c => c.String(false, 400, null, null, null, null, null, null, null), null);
            base.AlterColumn("dbo.DocumentType", "Name", c => c.String(false, 400, null, null, null, null, null, null, null), null);
            base.AlterColumn("dbo.ProductLine", "Code", c => c.String(false, 400, null, null, null, null, null, null, null), null);
            base.CreateIndex("dbo.ProductLine", "ProductId", false, null, false, null);
            base.CreateIndex("dbo.Contract", "UserProfile_UserId", false, null, false, null);
            base.CreateIndex("dbo.Document", "FromWarehouseId", false, null, false, null);
            base.CreateIndex("dbo.Document", "ToWarehouseId", false, null, false, null);
            base.AddForeignKey("dbo.ProductLine", "ProductId", "dbo.Product", "ProductId", true, null, null);
            base.AddForeignKey("dbo.Contract", "UserProfile_UserId", "dbo.UserProfile", "UserId", false, null, null);
            base.AddForeignKey("dbo.Document", "FromWarehouseId", "dbo.Warehouse", "WarehouseId", false, null, null);
            base.AddForeignKey("dbo.Document", "ToWarehouseId", "dbo.Warehouse", "WarehouseId", false, null, null);
            base.DropColumn("dbo.Document", "Warehouse_WarehouseId", null);
            base.DropColumn("dbo.Document", "Warehouse_WarehouseId1", null);
            base.DropColumn("dbo.ProductLine", "SellingPrice", null);
        }

        string IMigrationMetadata.Id
        {
            get
            {
                return "201401281037426_ProductUpdated";
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

