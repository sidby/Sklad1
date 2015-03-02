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
    public class ProductLineMap : EntityTypeConfiguration<ProductLine>
    {
        public ProductLineMap()
        {
            // Primary key
            this.HasKey(t => t.ProductLineId);

            // Properties
            this.Property(t => t.ProductLineId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            this.Property(t => t.ProductArticle)
                .HasMaxLength(400).IsRequired();
            this.Property(t => t.SupplierCode)
                .HasMaxLength(400).IsRequired();

            this.Property(t => t.Quantity)
               .IsRequired();
            this.Property(t => t.Reserve)
               .IsRequired();
            this.Property(t => t.Shipped)
               .IsRequired();
            this.Property(t => t.Available)
               .IsRequired();
            this.Property(t => t.PurchasePrice)
                .IsRequired();
            this.Property(t => t.SalePrice)
                .IsRequired();
            this.Property(t => t.MarginAbs)
                .IsRequired();
            this.Property(t => t.Discount)
                .IsRequired();
            this.Property(t => t.Sum)
                .IsRequired();
            this.Property(t => t.SaleSum)
                .IsRequired(); 
            this.Property(t => t.VAT)
                .IsRequired();
            this.Property(t => t.IsPriceIncludesVAT)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProductLine");

            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.ProductId);

            this.HasOptional(t => t.Supplier)
              .WithMany(t => t.ProductLines)
              .HasForeignKey(d => d.SupplierId);

            this.HasRequired(t => t.Document)
               .WithMany(t => t.Products)
               .HasForeignKey(d => d.DocumentId);
        }
    }
}
