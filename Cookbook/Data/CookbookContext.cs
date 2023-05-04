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
        public DbSet<DirectionEntity> Directions { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<IngredientEntity> Ingredients { get; set; }

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
                entity.HasKey(e => e.Category_ID);

                entity.Property(e => e.CategoryName)
                    .HasColumnName("category_name")
                    .HasColumnType("nvarchar(256)");
            });

            //Directions Table
            modelBuilder.Entity<DirectionEntity>(entity =>
            {
                entity.ToTable("Directions");
                entity.HasKey(e => e.Direction_ID);

                entity.Property(e => e.StepNumber)
                    .HasColumnName("step_number")
                    .HasColumnType("int");

                entity.Property(e => e.DirectionDescription)
                    .HasColumnName("direction_description")
                    .HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.RecipeID)
                    .HasColumnName("recipe_id")
                    .HasColumnType("bigint");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime");
            });

            //Groups Table
            modelBuilder.Entity<GroupEntity>(entity =>
            {
                entity.ToTable("Groups");
                entity.HasKey(e => e.Group_ID);

                entity.Property(e => e.GroupName)
                    .HasColumnName("group_name")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.OwnerID)
                    .HasColumnName("owner_id")
                    .HasColumnType("bigint");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime");
            });

            //Images Table
            modelBuilder.Entity<ImageEntity>(entity =>
            {
                entity.ToTable("Images");
                entity.HasKey(e => e.Image_ID);

                entity.Property(e => e.Image)
                    .HasColumnType("varbinary(max)");
            });

                //Ingredients Table
                modelBuilder.Entity<IngredientEntity>(entity =>
            {
                entity.ToTable("Ingredients");
                entity.HasKey(e => e.Ingredient_ID);

                entity.Property(e => e.IngredientName)
                    .HasColumnName("ingredient_name")
                    .HasColumnType("nvarchar(256)");

                entity.Property(e => e.RecipeID)
                    .HasColumnName("recipe_id")
                    .HasColumnType("bigint");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime");
            });

            //RecipeGroups Table
            modelBuilder.Entity<RecipeGroupEntity>(entity =>
            {
                entity.ToTable("RecipeGroups");
                entity.HasKey(e => e.Recipe_Group_ID);

                entity.Property(e => e.RecipeID)
                    .HasColumnName("recipe_id")
                    .HasColumnType("bigint");

                entity.Property(e => e.GroupID)
                    .HasColumnName("group_id")
                    .HasColumnType("bigint");
            });

            //Recipes Table
            modelBuilder.Entity<RecipeEntity>(entity =>
            {
                entity.ToTable("Recipes");
                entity.HasKey(e => e.Recipe_ID);

                entity.Property(e => e.RecipeName)
                    .HasColumnName("recipe_name")
                    .HasColumnType("nvarchar(256)");

                entity.Property(e => e.UserID)
                    .HasColumnName("user_id")
                    .HasColumnType("bigint");

                entity.Property(e => e.Servings)
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.PrepTime)
                    .HasColumnName("prep_time")
                    .HasColumnType("int");

                entity.Property(e => e.CookTime)
                    .HasColumnName("cook_time")
                    .HasColumnType("int");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CategoryID)
                    .HasColumnName("category_id")
                    .HasColumnType("bigint");

                entity.Property(e => e.ImageID)
                    .HasColumnName("image_id")
                    .HasColumnType("bigint");
            });

            //UserGroups Table
            modelBuilder.Entity<UserGroupEntity>(entity =>
            {
                entity.ToTable("UserGroups");
                entity.HasKey(e => e.User_Group_ID);

                entity.Property(e => e.UserID)
                    .HasColumnName("user_id")
                    .HasColumnType("bigint");

                entity.Property(e => e.GroupID)
                    .HasColumnName("group_id")
                    .HasColumnType("bigint");

                entity.Property(e => e.JoinDate)
                    .HasColumnName("join_date")
                    .HasColumnType("datetime");
            });

            //Users Table
            modelBuilder.Entity<UserEntity>(entity => 
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.User_ID);

                entity.Property(e => e.Email)
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Password)
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime");

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("password_hash")
                    .HasColumnType("nvarchar(50)");
            });
        }
    }
}
