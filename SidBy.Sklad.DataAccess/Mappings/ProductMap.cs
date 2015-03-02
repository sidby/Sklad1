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
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary key
            this.HasKey(t => t.ProductId);

            // Properties
            this.Property(t => t.ProductId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Article)
                .HasMaxLength(400).IsRequired();

            this.Property(t => t.PurchasePrice)
                .IsRequired();
            this.Property(t => t.SalePrice)
                .IsRequired(); 
            this.Property(t => t.VAT)
                .IsRequired();
            this.Property(t => t.CreatedAt)
                .IsRequired();
            this.Ignore(t => t.Code);

            this.Property(t => t.Remains)
               .IsRequired();
            this.Property(t => t.Reserve)
               .IsRequired();
            this.Property(t => t.Pending)
               .IsRequired();
            this.Property(t => t.Available)
              .IsRequired();

            // Table & Column Mappings
            this.ToTable("Product");

            // Relationships
            this.HasRequired(t => t.Supplier)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.ContractorId);
        }
    }
}
