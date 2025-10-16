using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Roles> roleManager)
        {
            // Define the list of default roles
            var defaultRoles = new[]
            {
                new Roles("SuperAdmin")
                {
                    NormalizedName = "SUPERADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreatedBy = Guid.Empty,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Empty,
                    ModifiedDate = DateTime.UtcNow,
                    isSuperAdminRole = true,
                    isAdminRole = false,
                    isManagerRole = false,
                    isUserRole = false,
                    isAnonymousRole = false,
                    isActive = true
                },
                new Roles("Admin")
                {
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreatedBy = Guid.Empty,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Empty,
                    ModifiedDate = DateTime.UtcNow,
                    isSuperAdminRole = false,
                    isAdminRole = true,
                    isManagerRole = false,
                    isUserRole = false,
                    isAnonymousRole = false,
                    isActive = true
                },
                new Roles("Manager")
                {
                    NormalizedName = "MANAGER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreatedBy = Guid.Empty,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Empty,
                    ModifiedDate = DateTime.UtcNow,
                    isSuperAdminRole = false,
                    isAdminRole = false,
                    isManagerRole = true,
                    isUserRole = false,
                    isAnonymousRole = false,
                    isActive = true
                },
                new Roles("User")
                {
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreatedBy = Guid.Empty,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Empty,
                    ModifiedDate = DateTime.UtcNow,
                    isSuperAdminRole = false,
                    isAdminRole = false,
                    isManagerRole = false,
                    isUserRole = true,
                    isAnonymousRole = false,
                    isActive = true
                },
                new Roles("Anonymous")
                {
                    NormalizedName = "ANONYMOUS",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreatedBy = Guid.Empty,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Empty,
                    ModifiedDate = DateTime.UtcNow,
                    isSuperAdminRole = false,
                    isAdminRole = false,
                    isManagerRole = false,
                    isUserRole = false,
                    isAnonymousRole = true,
                    isActive = true
                }
            };

            // Create each role if it doesn’t exist
            foreach (var role in defaultRoles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
