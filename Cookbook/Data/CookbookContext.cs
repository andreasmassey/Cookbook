using Cookbook.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cookbook.Data
{
    public class CookbookContext : DbContext
    {
        private readonly IConfiguration _config;

        public CookbookContext(IConfiguration config, DbContextOptions<CookbookContext> options)
            : base(options)
        {
            _config = config;
        }

        public DbSet<RecipeEntity> Recipes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _config["ConnectionStrings:CookbookDbConnection"];
            optionsBuilder.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(120));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Categories Table
            modelBuilder.Entity<CategoryEntity>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(e => e.CategoryID);

                entity.Property(e => e.CategoryName)
                    .HasColumnType("nvarchar(256)");
            });

            //Directions Table
            modelBuilder.Entity<DirectionEntity>(entity =>
            {
                entity.ToTable("Directions");
                entity.HasKey(e => e.DirectionID);

                entity.Property(e => e.StepNum)
                    .HasColumnType("int");

                entity.Property(e => e.DirectionDesc)
                    .HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.RecipeID)
                    .HasColumnType("bigint");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime");
            });

            //Groups Table
            modelBuilder.Entity<GroupEntity>(entity =>
            {
                entity.ToTable("Groups");
                entity.HasKey(e => e.GroupID);

                entity.Property(e => e.GroupName)
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.OwnerID)
                    .HasColumnType("bigint");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime");
            });

            //Ingredients Table
            modelBuilder.Entity<IngredientEntity>(entity =>
            {
                entity.ToTable("Ingredients");
                entity.HasKey(e => e.IngredientId);

                entity.Property(e => e.IngredientName)
                    .HasColumnType("nvarchar(256)");

                entity.Property(e => e.RecipeID)
                    .HasColumnType("bigint");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime");
            });

            //RecipeCategories Table
            modelBuilder.Entity<RecipeCategoryEntity>(entity =>
            {
                entity.ToTable("RecipeCategories");
                entity.HasKey(e => e.RecipeCategoryID);

                entity.Property(e => e.RecipeID)
                    .HasColumnType("bigint");

                entity.Property(e => e.CategoryID)
                    .HasColumnType("bigint");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime");
            });

            //RecipeGroups Table
            modelBuilder.Entity<RecipeGroupEntity>(entity =>
            {
                entity.ToTable("RecipeGroups");
                entity.HasKey(e => e.RecipeGroupID);

                entity.Property(e => e.RecipeID)
                    .HasColumnType("bigint");

                entity.Property(e => e.GroupID)
                    .HasColumnType("bigint");
            });

            //Recipes Table
            modelBuilder.Entity<RecipeEntity>(entity =>
            {
                entity.ToTable("Recipes");
                entity.HasKey(e => e.RecipeID);

                entity.Property(e => e.RecipeName)
                    .HasColumnType("nvarchar(256)");

                entity.Property(e => e.UserID)
                    .HasColumnType("bigint");

                entity.Property(e => e.Servings)
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.PrepTime)
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.CookTime)
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime");
            });

            //UserGroups Table
            modelBuilder.Entity<UserGroupEntity>(entity =>
            {
                entity.ToTable("UserGroups");
                entity.HasKey(e => e.UserGroupID);

                entity.Property(e => e.UserID)
                    .HasColumnType("bigint");

                entity.Property(e => e.GroupID)
                    .HasColumnType("bigint");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("datetime");
            });

            //Users Table
            modelBuilder.Entity<UserEntity>(entity => 
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.UserID);

                entity.Property(e => e.UserEmail)
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.UserPassword)
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.UserFirstName)
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.UserLastName)
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime");
            });
        }
    }
}
