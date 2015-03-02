using SidBy.Sklad.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SidBy.Sklad.DataAccess.Mappings
{
    public class LegalEntityMap : EntityTypeConfiguration<LegalEntity>
    {
        public LegalEntityMap()
        {
            // Primary key
            this.HasKey(t => t.LegalEntityId);

            // Properties
            this.Property(t => t.LegalEntityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name)
                .HasMaxLength(400).IsRequired();
            
            this.Property(t => t.Code)
              .HasMaxLength(300);
            this.Property(t => t.Phone)
                .HasMaxLength(150);
            this.Property(t => t.Fax)
                .HasMaxLength(150);
            this.Property(t => t.Email)
                .HasMaxLength(320);
            this.Property(t => t.Director)
                .HasMaxLength(400);
            this.Property(t => t.ChiefAccountant)
                .HasMaxLength(400);
          
            // Table & Column Mappings
            this.ToTable("LegalEntity");
        }
    }
}
