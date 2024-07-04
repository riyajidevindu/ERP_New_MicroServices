using ERP.TrainingManagement.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.DataServices
{
    public static class DataInitializer
    {
        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Ensure roles
            string[] roleNames = { "Student", "Coordinator" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Id = Guid.NewGuid(), Name = roleName, NormalizedName = roleName.ToUpper() });
                }
            }

            // Ensure default users
            await EnsureUserAsync(userManager, "coordinator@example.com", "John", "Doe", "Password123!", "Coordinator");
            await EnsureUserAsync(userManager, "student1@example.com", "Jane", "Smith", "Password1!", "Student");
            await EnsureUserAsync(userManager, "student2@example.com", "Alice", "Johnson", "Password13!", "Student");
        }

        public static async Task EnsureUserAsync(UserManager<ApplicationUser> userManager, string email, string firstName, string lastName, string password, string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    EmailConfirmed = true
                };

                var createUser = await userManager.CreateAsync(user, password);
                if (createUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
