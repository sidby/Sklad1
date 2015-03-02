using SidBy.Sklad.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SidBy.Sklad.DataAccess.Mappings
{
    public class UsersMap : EntityTypeConfiguration<UserProfile>
    {
        public UsersMap()
        {
            // Primary key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.UserName)
                .HasMaxLength(56).IsRequired();

            this.Property(t => t.UserEmail)
                .HasMaxLength(320);

            this.Property(t => t.DisplayName)
                .HasMaxLength(100);

            this.Property(t => t.Surname)
                .HasMaxLength(100);

            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.MiddleName)
                .HasMaxLength(100);
            this.Property(t => t.Phone1)
                .HasMaxLength(150);

            this.Property(t => t.Phone2)
               .HasMaxLength(150);

            this.Property(t => t.Skype)
             .HasMaxLength(150);

            this.Ignore(x => x.NewPassword);

            // Table & Column Mappings
            this.ToTable("UserProfile");

            // Relationships
            this.HasOptional(t => t.Job)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.LegalEntityId);

            this.HasOptional(t => t.Type)
                .WithMany(t => t.Profiles)
                .HasForeignKey(d => d.ContactTypeId);
        }
    }
}
