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
    public class DocumentMap : EntityTypeConfiguration<Document>
    {
        public DocumentMap()
        {
            // Primary key
            this.HasKey(t => t.DocumentId);

            // Properties
            this.Property(t => t.DocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Number)
                .HasMaxLength(400).IsRequired();

            this.Property(t => t.CreatedOf)
               .IsRequired();
            this.Property(t => t.IsCommitted)
               .IsRequired();
            this.Property(t => t.CreatedAt)
               .IsRequired();
            this.Property(t => t.Sum)
              .IsRequired();
            this.Property(t => t.SaleSum)
                .IsRequired();
            this.Ignore(x => x.DocumentTypeName);
            this.Ignore(x => x.TotalQuantity);
            this.Property(t => t.IsReportOutdated)
                .IsRequired();

            this.Property(t => t.CommonFolderName)
                .HasMaxLength(36).IsRequired();
            this.Property(t => t.SecureFolderName)
                .HasMaxLength(36).IsRequired();

            // Table & Column Mappings
            this.ToTable("Document");

            // Relationships
            this.HasRequired(t => t.DocumentType)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.DocumentTypeId);

            this.HasOptional(t => t.FromWarehouse)
               .WithMany(t => t.DocumentsFrom)
               .HasForeignKey(d => d.FromWarehouseId);

            this.HasOptional(t => t.ToWarehouse)
              .WithMany(t => t.DocumentsTo)
              .HasForeignKey(d => d.ToWarehouseId);

            this.HasOptional(t => t.Contract)
             .WithMany(t => t.Documents)
             .HasForeignKey(d => d.ContractId);

            this.HasOptional(t => t.Employee)
            .WithMany(t => t.Documents)
            .HasForeignKey(d => d.EmployeeId);

            this.HasOptional(t => t.ParentDocument)
            .WithMany(t => t.ChildDocuments)
            .HasForeignKey(d => d.ParentDocumentId);
        }
    }
}
