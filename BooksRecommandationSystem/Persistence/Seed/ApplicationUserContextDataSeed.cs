using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Seed
{
    public static class ApplicationUserContextDataSeed
    {
        /// <summary>
        ///     Seed users and roles in the Identity database.
        /// </summary>
        /// <param name="userManager">ASP.NET Core Identity User Manager</param>
        /// <param name="roleManager">ASP.NET Core Identity Role Manager</param>
        /// <returns></returns>
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Add roles supported
            await roleManager.CreateAsync(new IdentityRole(ApplicationIdentityConstants.Roles.Administrator));
            await roleManager.CreateAsync(new IdentityRole(ApplicationIdentityConstants.Roles.Member));

            // New admin user
            string adminUserName = "admin@test.com";
            var adminUser = new ApplicationUser
            {
                UserName = adminUserName,
                Email = adminUserName,
                ImageUri = new Uri("https://media.news9live.com/h-upload/2021/10/01/30834-newproject3.jpg"),
                IsEnabled = true,
                EmailConfirmed = true,
                FirstName = "Test",
                LastName = "Admin"
            };

            // Add new user and their role
            await userManager.CreateAsync(adminUser, ApplicationIdentityConstants.DefaultPassword);
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, ApplicationIdentityConstants.Roles.Administrator);
        }
    }
}
