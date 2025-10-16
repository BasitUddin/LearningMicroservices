using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Identity
{
    public class UserDbContext :
        IdentityDbContext<Users, Roles, Guid,
        UserClaims<Guid>, UserRoles<Guid>,
        UserLogin<Guid>, RoleClaims<Guid>,
        UserToken<Guid>>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("AspNetIdentity");

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");
            });

            modelBuilder.Entity<UserRoles<Guid>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("Roles");
            });
        }
    }
}
