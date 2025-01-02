
using Ecommerce_Core.Interfaces;
using Ecommerce_Core.Models;
using Ecommerce_Infrastracture.Data;
using Ecommerce_Infrastracture.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Access the configuration
            var configuration = builder.Configuration;
            
            // Add services to the container.
            builder.Services.AddControllers();

            // Configure the DbContext with SQL Server connection string
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Configure ASP.NET Identity

            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;  // Optional: change based on your policy
                options.Password.RequiredLength = 6;  // Minimum length of the password
                //options.Password.RequiredUniqueChars = 1; // Unique characters in the password
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Register AuthRepository as the implementation of IAuthRepository
            builder.Services.AddScoped<IAuthRepo, AuthRepo>();
            //builder.Services.AddScoped<IItemsRepository, ItemsRepository>();
            //builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
            
            //builder.Services.AddAutoMapper(typeof(Mapping_Profile));
            //// تسجيل AutoMapper
            //// تسجيل TypeAdapterConfig بشكل صحيح ليكون موجودًا في DI container
            //var config = TypeAdapterConfig.GlobalSettings;
            //builder.Services.AddSingleton(config);



            //MappingConfig.RegisterMappings(); // قم بتطبيق الإعدادات على الـ GlobalSettings
            // في برنامج Startup.cs أو Program.cs
            // تهيئة Mapster مع التخصيصات
            //var config = new TypeAdapterConfig();
            //config.Apply(new Mapping_Profile()); // تحميل التخصيصات

            //builder.Services.AddSingleton(config);  // تسجيل التخصيص في الـ DI container


            // Configure JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
