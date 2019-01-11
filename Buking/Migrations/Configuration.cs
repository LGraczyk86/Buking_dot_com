using Microsoft.AspNet.Identity.EntityFramework;

namespace Buking.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Buking.Context.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Buking.Context.DataContext context)
        {
            var roleAdmin = context.Roles.SingleOrDefault(p => p.Name == "Vendor");
            if (roleAdmin == null)
            {
                roleAdmin = new IdentityRole("Vendor");
                context.Roles.Add(roleAdmin);
            }
        }
    }
}
