using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<Users> userManager, RoleManager<Roles> roleManager)
        {
            // ✅ Step 1: Ensure roles exist
            await RoleSeeder.SeedAsync(roleManager);

            // ✅ Step 2: Create default SuperAdmin user
            var superAdminEmail = "superadmin@test.com";
            var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);

            if (superAdmin == null)
            {
                superAdmin = new Users
                {
                    UserName = superAdminEmail,
                    Email = superAdminEmail,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    FullName = "System SuperAdmin",
                    NormalizedUserName = superAdminEmail.ToUpper(),
                    NormalizedEmail = superAdminEmail.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = "03001234567",
                    PhoneNumberConfirmed = true,
                    Active = true,
                    Deleted = false,
                    CreatedBy = Guid.Empty,
                    ModifiedBy = Guid.Empty,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var createResult = await userManager.CreateAsync(superAdmin, "Test@123");

                if (!createResult.Succeeded)
                {
                    throw new Exception("Failed to create SuperAdmin user: " +
                        string.Join(", ", createResult.Errors.Select(e => e.Description)));
                }
            }

            // ✅ Step 3: Assign user to roles
            var roles = new[] { "SuperAdmin", "Admin", "Manager", "User" };

            foreach (var role in roles)
            {
                if (await roleManager.RoleExistsAsync(role))
                {
                    if (!await userManager.IsInRoleAsync(superAdmin, role))
                    {
                        await userManager.AddToRoleAsync(superAdmin, role);
                    }
                }
            }
        }
    }
}
