using SidBy.Sklad.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace SidBy.Sklad.Web.Migration
{
    /// <summary>
    /// Use for rebuild UserProfile table
    /// </summary>
    public class UsersContextMigrationConfiguration : DbMigrationsConfiguration<UsersContext>
    {
        public UsersContextMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UsersContext context)
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
    }
}