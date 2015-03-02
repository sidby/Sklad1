using SidBy.Sklad.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SidBy.Sklad.DataAccess.Mappings
{
    public class WarehouseMap : EntityTypeConfiguration<Warehouse>
    {
        public WarehouseMap()
        {
            // Primary key
            this.HasKey(t => t.WarehouseId);

            // Properties
            this.Property(t => t.WarehouseId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name)
                .HasMaxLength(400).IsRequired();

            this.Property(t => t.Code)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("Warehouse");

            // Relationships
            this.HasOptional(t => t.ParentWarehouse)
                .WithMany(t => t.ChildWarehouses)
                .HasForeignKey(d => d.ParentId);
        }
    }
}
