namespace SidBy.Sklad.Web.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Document", "Warehouse_WarehouseId", "dbo.Warehouse");
            DropForeignKey("dbo.Document", "Warehouse_WarehouseId1", "dbo.Warehouse");
            DropIndex("dbo.Document", new[] { "Warehouse_WarehouseId" });
            DropIndex("dbo.Document", new[] { "Warehouse_WarehouseId1" });
            DropColumn("dbo.Document", "EmployeeId");
            DropColumn("dbo.Document", "FromWarehouseId");
            DropColumn("dbo.Document", "ToWarehouseId");
            RenameColumn(table: "dbo.Document", name: "Employee_UserId", newName: "EmployeeId");
            RenameColumn(table: "dbo.Document", name: "FromWarehouse_WarehouseId", newName: "FromWarehouseId");
            RenameColumn(table: "dbo.Document", name: "ToWarehouse_WarehouseId", newName: "ToWarehouseId");
            AddColumn("dbo.Contract", "UserProfile_UserId", c => c.Int());
            AddColumn("dbo.ProductLine", "SalePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Contract", "Number", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("dbo.Document", "Number", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("dbo.DocumentType", "Name", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("dbo.ProductLine", "Code", c => c.String(nullable: false, maxLength: 400));
            CreateIndex("dbo.ProductLine", "ProductId");
            CreateIndex("dbo.Contract", "UserProfile_UserId");
            CreateIndex("dbo.Document", "FromWarehouseId");
            CreateIndex("dbo.Document", "ToWarehouseId");
            AddForeignKey("dbo.ProductLine", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.Contract", "UserProfile_UserId", "dbo.UserProfile", "UserId");
            AddForeignKey("dbo.Document", "FromWarehouseId", "dbo.Warehouse", "WarehouseId");
            AddForeignKey("dbo.Document", "ToWarehouseId", "dbo.Warehouse", "WarehouseId");
            DropColumn("dbo.Document", "Warehouse_WarehouseId");
            DropColumn("dbo.Document", "Warehouse_WarehouseId1");
            DropColumn("dbo.ProductLine", "SellingPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductLine", "SellingPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Document", "Warehouse_WarehouseId1", c => c.Int());
            AddColumn("dbo.Document", "Warehouse_WarehouseId", c => c.Int());
            DropForeignKey("dbo.Document", "ToWarehouseId", "dbo.Warehouse");
            DropForeignKey("dbo.Document", "FromWarehouseId", "dbo.Warehouse");
            DropForeignKey("dbo.Contract", "UserProfile_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.ProductLine", "ProductId", "dbo.Product");
            DropIndex("dbo.Document", new[] { "ToWarehouseId" });
            DropIndex("dbo.Document", new[] { "FromWarehouseId" });
            DropIndex("dbo.Contract", new[] { "UserProfile_UserId" });
            DropIndex("dbo.ProductLine", new[] { "ProductId" });
            AlterColumn("dbo.ProductLine", "Code", c => c.String());
            AlterColumn("dbo.DocumentType", "Name", c => c.String());
            AlterColumn("dbo.Document", "Number", c => c.String());
            AlterColumn("dbo.Contract", "Number", c => c.String());
            DropColumn("dbo.ProductLine", "SalePrice");
            DropColumn("dbo.Contract", "UserProfile_UserId");
            RenameColumn(table: "dbo.Document", name: "ToWarehouseId", newName: "ToWarehouse_WarehouseId");
            RenameColumn(table: "dbo.Document", name: "FromWarehouseId", newName: "FromWarehouse_WarehouseId");
            RenameColumn(table: "dbo.Document", name: "EmployeeId", newName: "Employee_UserId");
            AddColumn("dbo.Document", "ToWarehouseId", c => c.Int());
            AddColumn("dbo.Document", "FromWarehouseId", c => c.Int());
            AddColumn("dbo.Document", "EmployeeId", c => c.Int());
            CreateIndex("dbo.Document", "Warehouse_WarehouseId1");
            CreateIndex("dbo.Document", "Warehouse_WarehouseId");
            AddForeignKey("dbo.Document", "Warehouse_WarehouseId1", "dbo.Warehouse", "WarehouseId");
            AddForeignKey("dbo.Document", "Warehouse_WarehouseId", "dbo.Warehouse", "WarehouseId");
        }
    }
}
