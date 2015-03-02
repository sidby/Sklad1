using SidBy.Sklad.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SidBy.Sklad.DataAccess.Mappings
{
    public class ContractorTagMap : EntityTypeConfiguration<ContractorTag>
    {
        public ContractorTagMap()
        {
            // Primary key
            this.HasKey(t => t.ContractorTagId);

            // Properties
            this.Property(t => t.ContractorTagId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name)
                .HasMaxLength(400).IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractorTag");
        }
    }
}
