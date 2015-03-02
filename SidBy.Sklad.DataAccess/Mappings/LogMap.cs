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
    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            // Primary key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Date)
                .IsRequired();

            this.Property(t => t.Thread)
                .HasMaxLength(255).IsRequired();

            this.Property(t => t.Level)
              .HasMaxLength(50).IsRequired();

            this.Property(t => t.Logger)
              .HasMaxLength(255).IsRequired();

            this.Property(t => t.Message)
              .HasMaxLength(4000).IsRequired();

            this.Property(t => t.Exception)
              .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("Log");
        }
    }
}
