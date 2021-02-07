using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SwGlobal.App_Start;
using SwGlobal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

[assembly: OwinStartupAttribute(typeof(SwGlobal.Startup))]
namespace SwGlobal
{
    public partial class Startup
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
           SwGlobal.App_Start.Startup.ConfigureAuth(app);

            //Seed();
        }
        private void Seed()
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            SwGlobal.Models.ApplicationDbContext context = new ApplicationDbContext();
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
    
    //public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    //{
    //    public bool Authorize(DashboardContext context)
    //    {
    //        bool response = false;
    //        // In case you need an OWIN context, use the next line, `OwinContext` class
    //        // is the part of the `Microsoft.Owin` package.
    //        var owinContext = new OwinContext(context.GetOwinEnvironment());

    //        // Allow all authenticated users to see the Dashboard (potentially dangerous).
    //        if (owinContext.Authentication.User.Identity.IsAuthenticated)
    //        {
    //            if (owinContext.Authentication.User.IsInRole("Super Admin"))
    //            {
    //                response = true;
    //            }
    //        }
    //        return response;
    //    }
    //}
}