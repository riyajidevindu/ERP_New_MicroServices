using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using LoginManagement.Core.Entities;


namespace LoginManagement.DataService
{
    public static class DataInitializer
    {
        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Ensure roles
            string[] roleNames = { "Student", "Coordinator", "Work Admin", "Staff Admin" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Id = Guid.NewGuid(), Name = roleName, NormalizedName = roleName.ToUpper() });
                }
            }

            // Ensure default users
            await EnsureUserAsync(userManager, "coordinator@example.com", "John", "Doe", "Password123!", "Coordinator");
            await EnsureUserAsync(userManager, "student1@example.com", "S.A.Y.A Suraweera", "Smith", "Password1!", "Student");
            await EnsureUserAsync(userManager, "student2@example.com", "K.K.S Madushanka", "Johnson", "Password13!", "Student");
            await EnsureUserAsync(userManager, "workadmin@example.com", "Work", "Admin", "Password123!", "Work Admin");
            await EnsureUserAsync(userManager, "staffadmin@example.com", "Staff", "Admin", "Password123!", "Staff Admin");
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
