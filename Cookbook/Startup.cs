using Cookbook.Data;
using Cookbook.Data.Repository;
using Cookbook.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cookbook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // this defines a CORS policy called "default"
            services.AddCors(options => { options.AddPolicy("default", policy => { policy.WithOrigins().AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }); });
            services.AddCors();
            services.AddControllers();
            services.AddDbContext<CookbookContext>(ServiceLifetime.Scoped);
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDirectionsRepository, DirectionsRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IIngredientsRepository, IngredientsRepository>();
            services.AddTransient(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.WithOrigins().AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseCors("default");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI();
        }
    }
}
