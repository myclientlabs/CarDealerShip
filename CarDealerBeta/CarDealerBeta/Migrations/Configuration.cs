namespace CarDealerBeta.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarDealerBeta.Models.ApplicationDbContext>
    {
        private const string ADMIN_ROLE = "Admin";
        private const string SALES_ROLE = "Sales";
        private const string DISABLED_ROLE = "Disabled";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarDealerBeta.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(r => r.Name,
                    new IdentityRole { Name = ADMIN_ROLE },
                    new IdentityRole { Name = SALES_ROLE },
                    new IdentityRole { Name = DISABLED_ROLE }
                );

            context.SaveChanges();

            //create super admin
            CreateNewUser(context, "superadmin@example.com", new List<string> { ADMIN_ROLE, SALES_ROLE });

            //Admin user
            CreateNewUser(context, "admin@example.com", new List<string> { ADMIN_ROLE });

            //Sale Role user
            CreateNewUser(context, "sales@example.com", new List<string> { SALES_ROLE });

        }

        private void CreateNewUser(ApplicationDbContext context, string email, List<string> roles)
        {
            if (!context.Users.Any(u => u.UserName == email))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    PhoneNumber = "08869879"
                };

                manager.Create(user, "Pass@word1");
                if (roles != null)
                {
                    foreach (var item in roles)
                    {
                        manager.AddToRole(user.Id, item);

                    }
                }
                context.SaveChanges();
            }
        }
    }
}
