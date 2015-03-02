using SidBy.Sklad.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SidBy.Sklad.DataAccess.Mappings
{
    public class ContractorTypeMap : EntityTypeConfiguration<ContractorType>
    {
        public ContractorTypeMap()
        {
            // Primary key
            this.HasKey(t => t.ContractorTypeId);

            // Properties
            this.Property(t => t.ContractorTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name)
                .HasMaxLength(400).IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractorType");
        }
    }
}
