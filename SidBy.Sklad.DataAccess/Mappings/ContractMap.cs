using SidBy.Sklad.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.DataAccess.Mappings
{
    public class ContractMap : EntityTypeConfiguration<Contract>
    {
        public ContractMap()
        {
            // Primary key
            this.HasKey(t => t.ContractId);

            // Properties
            this.Property(t => t.ContractId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Number)
                .HasMaxLength(400).IsRequired();
           
            this.Property(t => t.CreatedOf)
               .IsRequired();
            this.Property(t => t.Sum)
               .IsRequired();
            this.Property(t => t.Paid)
                .IsRequired();
            this.Property(t => t.CarriedOut)
                .IsRequired();
            this.Property(t => t.CreatedAt)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Contract");
          
            // Relationships
            this.HasRequired(t => t.Contractor)
                .WithMany(t => t.Contracts)
                .HasForeignKey(d => d.ContractorId);

            this.HasRequired(t => t.LegalEntity)
               .WithMany(t => t.Contracts)
               .HasForeignKey(d => d.LegalEntityId);
        }
    }
}
