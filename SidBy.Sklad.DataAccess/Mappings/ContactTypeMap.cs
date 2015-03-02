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
    public class ContactTypeMap : EntityTypeConfiguration<ContactType>
    {
        public ContactTypeMap()
        {
            // Primary key
            this.HasKey(t => t.ContactTypeId);

            // Properties
            this.Property(t => t.ContactTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name)
                .HasMaxLength(400).IsRequired();

            // Table & Column Mappings
            this.ToTable("ContactType");
        }
    }
}
