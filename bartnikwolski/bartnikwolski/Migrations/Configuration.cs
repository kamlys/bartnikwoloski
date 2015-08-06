namespace bartnikwolski.Migrations
{
    using bartnikwolski.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<bartnikwolski.Models.BeekeeperDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(bartnikwolski.Models.BeekeeperDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Users.AddOrUpdate(new User
            {
                Login = "Admin",
                Password = Crypto.HashPassword("admin"),
            });

            context.Pages.AddOrUpdate(
                new Page { Title = "Start" },
                new Page { Title = "O nas" },
                new Page { Title = "Nasze produkty" },
                new Page { Title = "Nasze specja³y" },
                new Page { Title = "Uroda z pasieki" },
                new Page { Title = "Ciekawostki" }
                );
        }
    }
}
