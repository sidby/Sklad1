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
    public class LoggingDataContext : DbContext
    {
        public LoggingDataContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LogMap());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
