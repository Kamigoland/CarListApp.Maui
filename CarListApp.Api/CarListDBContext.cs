﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarListApp.Api
{
    public class CarListDBContext : IdentityDbContext
    {
        public CarListDBContext(DbContextOptions<CarListDBContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars_Table { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "d1b5952a-2162-46c7-b29e-1a2a68922c14", //guid generator
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "42358d3e-3c22-45e1-be81-6caa7ba865ef",
                    Name = "User",
                    NormalizedName = "USER"
                }
                );

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "61a50fbb-041d-4133-bbd2-0be0e369a795",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    UserName = "admin@localhost.com",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true
                },
                new IdentityUser
                {
                    Id = "7197e409-c2d1-4dcf-b0b0-b32054bbfd8b",
                    Email = "user@localhost.com",
                    NormalizedEmail = "USER@LOCALHOST.COM",
                    NormalizedUserName = "USER@LOCALHOST.COM",
                    UserName = "user@localhost.com",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true
                }
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = "d1b5952a-2162-46c7-b29e-1a2a68922c14",
                        UserId = "61a50fbb-041d-4133-bbd2-0be0e369a795"
                    },
                    new IdentityUserRole<string>
                    {
                        RoleId = "42358d3e-3c22-45e1-be81-6caa7ba865ef",
                        UserId = "7197e409-c2d1-4dcf-b0b0-b32054bbfd8b"
                    }
                );
        }
    }
}