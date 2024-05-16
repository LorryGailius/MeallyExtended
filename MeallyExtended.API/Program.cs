using System.Reflection;
using MeallyExtended.Business.Data;
using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Repository.Interfaces;
using MeallyExtended.Business.Repository;
using MeallyExtended.Business.Services;
using MeallyExtended.DataModels.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeallyExtended.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
                {
                    var filename = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                    var filepath = Path.Combine(AppContext.BaseDirectory, filename);
                    options.IncludeXmlComments(filepath);
                });

            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
            builder.Services.AddAuthorizationBuilder();

            builder.Services.AddDbContext<MeallyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MeallyConnectionString"), b => b.MigrationsAssembly("MeallyExtended.API")));

            builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<MeallyDbContext>().AddApiEndpoints();

            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IRecipeLikesRepository, RecipeLikesRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRecipeService, RecipeService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();

            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            var popularityConfig = builder.Configuration["PopularityConfig"];

            switch (popularityConfig)
            {
                case "ByClicks":
                    builder.Services.AddScoped<IPopularityService, ClickPopularityService>();
                    break;
                case "ByLikes":
                    builder.Services.AddScoped<IPopularityService, LikePopularityService>();
                    break;
                default:
                    builder.Services.AddScoped<IPopularityService, LikePopularityService>();
                    break;
            }

            var app = builder.Build();

            app.MapIdentityApi<User>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();

            // Use CORS middleware
            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}