using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace bartnikwolski.Models
{
    public class BeekeeperDbContext : DbContext
    {
        public BeekeeperDbContext()
            : base("BeekeeperDbContext")
        {
            Database.SetInitializer<BeekeeperDbContext>(new DropCreateDatabaseIfModelChanges<BeekeeperDbContext>());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}