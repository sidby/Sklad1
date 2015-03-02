using SidBy.Sklad.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SidBy.Sklad.DataAccess.Mappings
{
    public class RegionMap : EntityTypeConfiguration<Region>
    {
        public RegionMap()
        {
            // Primary key
            this.HasKey(t => t.RegionId);

            // Properties
            this.Property(t => t.RegionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name)
                .HasMaxLength(400).IsRequired();

            // Table & Column Mappings
            this.ToTable("Region");
        }
    }
}
