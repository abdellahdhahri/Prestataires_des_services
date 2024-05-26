using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServicesPlomberie.Models;
using System;

namespace ServicesPlomberie.Data
{
    public class ApplicationDbContext : IdentityDbContext<Utilisateur>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
          
    }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Utilisateur>().ToTable("users","security");
            builder.Entity<IdentityRole>().ToTable("sRoles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("userRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("userClaim", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("userLogin", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("userRoleClaim", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("userTokens", "security");

           


        }
        public DbSet<ServicesPlomberie.Models.Client>? Client { get; set; }
        public DbSet<ServicesPlomberie.Models.Service>? Service { get; set; }
        public DbSet<ServicesPlomberie.Models.Specialite>? Specialite { get; set; }
        public DbSet<ServicesPlomberie.Models.Post>? Post { get; set; }

    }
}