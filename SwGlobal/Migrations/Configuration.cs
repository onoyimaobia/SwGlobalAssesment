namespace SwGlobal.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SwGlobal.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SwGlobal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected  override void Seed(SwGlobal.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //create roles below
            context.Roles.AddOrUpdate(x => x.Name,
                new IdentityRole("Admin"),
                new IdentityRole("Customer")
                );
            //creates super-admin user and assign Admin role
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            string email = "swglobal@gmail.com";
            var user = new ApplicationUser { UserName = email, PhoneNumber = "08103449953", Email = email };
            IdentityResult result = manager.Create(user, "SwGlobal1$");
            if (result.Succeeded)
            {
                var superAdmin = manager.FindByEmail(email);
                if (superAdmin != null && !manager.IsInRole(superAdmin.Id, "Admin"))
                {
                    manager.AddToRole(superAdmin.Id, "Admin");
                }

            }
            else
            {
                var superAdmin = manager.FindByEmail(email);
                if (superAdmin != null && !manager.IsInRole(superAdmin.Id, "Super Admin"))
                {
                    manager.AddToRole(superAdmin.Id, "Super Admin");
                }
            }
        }
    }
}
