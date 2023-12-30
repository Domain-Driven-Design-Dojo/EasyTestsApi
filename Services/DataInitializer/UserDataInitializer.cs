using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Services.Extensions;
using Entities.UserModels;

namespace Services.DataInitializer
{
    public class UserDataInitializer : IDataInitializer
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<AccRole> roleManager;

        public UserDataInitializer(UserManager<ApplicationUser> userManager, RoleManager<AccRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void InitializeData()
        {
            if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new AccRole { Name = "Admin", Description = "Admin role" }).GetAwaiter().GetResult();
            }
            if (!userManager.Users.AsNoTracking().Any(p => p.UserName == "Admin"))
            {
                var user = new ApplicationUser
                {
                    //Age = 37,
                    //FullName = "وحید محمدیان",
                    //Gender = GenderType.Male,
                    UserName = "admin",
                    Email = "admin@site.com"
                };
                userManager.CreateAsync(user, "12345678").GetAwaiter().GetResult();
                userManager.AddToRoleDbAndCache(user, "Admin").GetAwaiter().GetResult();
            }
        }
    }
}