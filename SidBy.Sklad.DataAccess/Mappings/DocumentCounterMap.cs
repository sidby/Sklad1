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
    public class DocumentCounterMap : EntityTypeConfiguration<DocumentCounter>
    {
        public DocumentCounterMap()
        {
            // Primary key
            this.HasKey(t => t.DocumentCounterId);

            // Properties
            this.Property(t => t.DocumentCounterId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Counter)
                .IsRequired();

            this.Property(t => t.Year)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("DocumentCounter");

            // Relationships
            this.HasRequired(t => t.DocumentType)
                .WithMany(t => t.DocumentCounters)
                .HasForeignKey(d => d.DocumentTypeId);
        }
    }
}
