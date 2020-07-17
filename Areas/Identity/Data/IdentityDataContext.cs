using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace com.ironhasura.Areas.Identity.Data
{
    public class IdentityDataContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("mcsp");
            this.RenameIdentityTables(builder, "mcsp_");
            this.SeedData(builder);
        }

        private void RenameIdentityTables(ModelBuilder builder, string prefix)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var table = entityType.GetTableName();
                if (table.StartsWith("AspNet"))
                {
                    // entityType.SetTableName(prefix + table.Substring(6));
                    entityType.SetTableName(table.Substring(6));
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        private void SeedData(ModelBuilder builder) 
        {
            var email = "admin@microscope.net";
            var role = "Admin";
            var roleId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<IdentityUser>();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() {
                    Id = roleId, 
                    Name = role, 
                    NormalizedName = role.ToUpper()
                }
            );

            builder.Entity<IdentityUser>().HasData(
                new IdentityUser() 
                {
                    Id = userId,
                    Email = email,
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "microscope"),
                    SecurityStamp = string.Empty
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() {
                    RoleId = roleId,
                    UserId = userId
                }
            );
        }
    }
}
