using FlavoursomeWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static Humanizer.On;

namespace FlavoursomeWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Step> Steps { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Store QuantityType enum as string in the DB.
            builder.Entity<Ingredient>()
            .Property(p => p.QuantityType)
            .HasConversion<string>();

            // Each user can only favorite a recipe once, but many users can have many favorite recipes.
            builder.Entity<Favorite>()
            .HasKey(f => new { f.UserId, f.RecipeID }); // Composite key

            //A User can have many Favorite recipes.
            builder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Remove user's favorites when user is deleted

            // A Recipe can be favorited by many Users.
            builder.Entity<Favorite>()
                .HasOne(f => f.Recipe)      // Favorite has 1 Recipe
                .WithMany(r => r.FavoritedByUsers) // A Recipe can be favorited by many Users
                .HasForeignKey(f => f.RecipeID)
                .OnDelete(DeleteBehavior.Cascade); // Remove favorites when recipe is deleted

            // User - Recipe Relationship (1-to-Many)
            builder.Entity<Recipe>()
                .HasOne(r => r.User) // A recipe belongs to one user
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Delete recipes when user is deleted

            // Recipe - Ingredient Relationship (1-to-Many)
            builder.Entity<Ingredient>()
                .HasOne(p => p.Recipe) // An ingredient belongs to one recipe
                .WithMany(r => r.Ingredients) // A recipe has many ingredients
                .HasForeignKey(p => p.RecipeID)
                .OnDelete(DeleteBehavior.Cascade); // If a recipe is deleted, its ingredients are also deleted

            // each step belongs to one recipe and one recipe has many steps
            builder.Entity<Step>()
                .HasOne(s => s.Recipe)
                .WithMany(r => r.Steps) // A recipe has many steps
                .HasForeignKey(s => s.RecipeID)
                .OnDelete(DeleteBehavior.Cascade); // Delete steps when a recipe is deleted

            builder.Entity<Step>()
                .Property(s => s.Description)
                .IsRequired();

            string adminUserId = "a820ccf9-54ac-4047-b4b5-48dab0dc962b";
            string adminRoleId = "a23a7ee8-beb5-4238-ad8a-88d54b3c3d29";

            // Seed Admin role.
            builder
                .Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = adminRoleId,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });

            // Create admin user.
            var appUser = new IdentityUser
            {
                Id = adminUserId,
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM"
            };

            // Set initial admin password.
            var ph = new PasswordHasher<IdentityUser>();

            // Don't forget to change admin password after initial account creation.
            appUser.PasswordHash = ph.HashPassword(appUser, "Abc123!");

            // Seed user.
            builder
                .Entity<IdentityUser>()
                .HasData(appUser);

            // Set user role to admin.
            builder
                .Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                });
        }
    }
}
