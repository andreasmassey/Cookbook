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

            modelBuilder.Entity<RecipeEntity>(entity =>
            {
                entity.ToTable("Recipe");
                entity.HasKey(e => e.RecipeID);

                entity.Property(e => e.Title)
                    .HasColumnType("nvarchar(256)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("nvarchar(50)");
            });
        }
    }
}
