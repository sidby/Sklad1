using SidBy.Sklad.DataAccess.Mappings;
using SidBy.Sklad.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.DataAccess
{
    public class SkladDataContext : DbContext
    {
        public SkladDataContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<LegalEntity> LegalEntities { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        //public DbSet<Region> Regions { get; set; }
        public DbSet<ContractorTag> ContractorTags { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<ContractorType> ContractorTypes { get; set; }
        //public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLine> ProductLines { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<DocumentCounter> DocumentCounters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LegalEntityMap());
            modelBuilder.Configurations.Add(new ContactTypeMap());
            //modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new UsersMap());
            modelBuilder.Configurations.Add(new WarehouseMap());
       
            //modelBuilder.Configurations.Add(new RegionMap());
            modelBuilder.Configurations.Add(new ContractorTagMap());
            modelBuilder.Configurations.Add(new ContractorMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductLineMap());
            modelBuilder.Configurations.Add(new DocumentMap());
            modelBuilder.Configurations.Add(new DocumentTypeMap());
            modelBuilder.Configurations.Add(new ContractMap());
            modelBuilder.Configurations.Add(new DocumentCounterMap());

            modelBuilder.Entity<Contractor>()
                .HasMany(t => t.Tags)
                .WithMany(t => t.ContractorList)
                .Map(m =>
                {
                    m.ToTable("ContractorTagContractor");
                    m.MapLeftKey("ContractorTagId");
                    m.MapRightKey("ContractorId");
                });

            modelBuilder.Entity<UserProfile>()
              .HasMany(t => t.Contractors)
              .WithMany(t => t.Users)
              .Map(m =>
              {
                  m.ToTable("UserProfileContractor");
                  m.MapLeftKey("UserId");
                  m.MapRightKey("ContractorId");
              });

            modelBuilder.Entity<Document>()
                .HasMany(x => x.Products)
                .WithRequired()
                .WillCascadeOnDelete(true);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
