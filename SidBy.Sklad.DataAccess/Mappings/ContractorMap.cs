using SidBy.Sklad.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SidBy.Sklad.DataAccess.Mappings
{
    public class ContractorMap : EntityTypeConfiguration<Contractor>
    {
        public ContractorMap()
        {
            // Primary key
            this.HasKey(t => t.ContractorId);

            // Properties
            this.Property(t => t.ContractorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name)
                .HasMaxLength(400).IsRequired();

            this.Property(t => t.Code)
                .HasMaxLength(300);
            this.Property(t => t.ContactPersonName)
                .HasMaxLength(100);
            
            this.Property(t => t.Phone)
            .HasMaxLength(150);
            this.Property(t => t.Fax)
                .HasMaxLength(150);
            this.Property(t => t.Region)
               .HasMaxLength(150);
            this.Property(t => t.Email)
                .HasMaxLength(320);

            this.Property(t => t.IsArchived)
                .IsRequired();

            this.Property(t => t.MarginAbs)
                .IsRequired();

            this.Ignore(t => t.ResponsibleName);

            // Table & Column Mappings
            this.ToTable("Contractor");

            // Relationships
            this.HasOptional(t => t.Responsible)
                .WithMany(t => t.ResponsibleContractors)
                .HasForeignKey(d => d.ResponsibleId);

            //this.HasOptional(t => t.RegionItem)
            //   .WithMany(t => t.Contractors)
            //   .HasForeignKey(d => d.RegionId);

            this.HasRequired(t => t.ContractorType)
             .WithMany(t => t.Contractors)
             .HasForeignKey(d => d.ContractorTypeId);
        }
    }
}
