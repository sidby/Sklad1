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
    //public class ContactMap : EntityTypeConfiguration<Contact>
    //{
    //    public ContactMap()
    //    {
    //        // Primary key
    //        this.HasKey(t => t.ContactId);

    //        // Properties
    //        this.Property(t => t.ContactId)
    //            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
    //        this.Property(t => t.DisplayName)
    //            .HasMaxLength(400).IsRequired();

    //        this.Property(t => t.Name)
    //            .HasMaxLength(150);

    //        this.Property(t => t.MiddleName)
    //            .HasMaxLength(150);

    //        this.Property(t => t.Surname)
    //            .HasMaxLength(150);

    //        this.Property(t => t.Phone1)
    //            .HasMaxLength(150);
            
    //        this.Property(t => t.Phone2)
    //            .HasMaxLength(150);

    //        this.Property(t => t.Email)
    //          .HasMaxLength(320);

    //        this.Property(t => t.Skype)
    //          .HasMaxLength(320);

    //        // Table & Column Mappings
    //        this.ToTable("Contact");

    //        // Relationships
    //        this.HasRequired(t => t.Type)
    //            .WithMany(t => t.Contacts)
    //            .HasForeignKey(d => d.ContactTypeId);
    //    }
    //}
}
