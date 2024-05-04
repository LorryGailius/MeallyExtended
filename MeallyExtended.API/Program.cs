
using MeallyExtended.Business.Data;
using MeallyExtended.DataModels.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

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
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
            builder.Services.AddAuthorizationBuilder();

            builder.Services.AddDbContext<MeallyDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("MeallyConnectionString"), b => b.MigrationsAssembly("MeallyExtended.API")));

            builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<MeallyDbContext>().AddApiEndpoints();


            var app = builder.Build();

            app.MapIdentityApi<User>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
