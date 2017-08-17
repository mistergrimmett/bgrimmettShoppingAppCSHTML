namespace bgrimmettShoppingAppCSHTML.Migrations
{
    using bgrimmettShoppingAppCSHTML.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
                var roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));
                if (!context.Roles.Any(r => r.Name == "Admin"))
                {
                    roleManager.Create(new IdentityRole { Name = "Admin" });
                }
                var userManager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));
                if (!context.Users.Any(u => u.Email == "mistergrimmett1127@gmail.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        UserName = "mistergrimmett1127@gmail.com",
                        Email = "mistergrimmett1127@gmail.com",
                        FirstName = "Brandon",
                        LastName = "Grimmett",
                    }, "Powerman@1");
                }
                var userId = userManager.FindByEmail("mistergrimmett1127@gmail.com").Id;
                userManager.AddToRole(userId, "Admin");
            }
        }
    }
    

