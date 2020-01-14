using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreToDo
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await EnsureRoleAsync(roleManager);

            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            await EnsureTestAdminAsync(userManager);
        }

        private static async Task EnsureRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);

            if (alreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
        }

        private static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager)
        {
            var testAdmin = await userManager.Users.SingleOrDefaultAsync(x => x.UserName == "admin@todo.local");

            if (testAdmin != null) return;

            testAdmin = new IdentityUser
            {
                UserName = "alvinmuala@yahoo.com",
                Email = "alvinmuala@yahoo.com"
            };

            await userManager.CreateAsync(testAdmin, "Zealous2804$");

            await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
        }
    }
}
