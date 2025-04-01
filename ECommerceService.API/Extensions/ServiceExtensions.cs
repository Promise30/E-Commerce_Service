using ECommerceService.API.Application.Implementation;
using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data;
using ECommerceService.API.Database.Implementation;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.HealthChecks;
using Hangfire;
using Hangfire.SQLite;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace ECommerceService.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ECommerceService.API",
                    Description = "API for ECommerceService",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \",\"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

        public static void ConfigureIdentity(this IServiceCollection services) =>
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ECommerceDbContext>()
                .AddDefaultTokenProviders();

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration) =>
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:ValidIssuer"],
                    ValidAudience = configuration["JwtSettings:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<ECommerceDbContext>(options =>
            {
                //options.UseSqlite("Ecommerce.db");
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        public static void ConfigureRepository(this IServiceCollection services) =>
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>))
                    .AddScoped<ICategoryRepository, CategoryRepository>()
                    .AddScoped<IProductRepository, ProductRepository>()
                    .AddScoped<ICartRepository, CartRepository>()
                    .AddScoped<ICartItemRepository, CartItemRepository>()
                    .AddScoped<IOrderRepository, OrderRepository>()
                    .AddScoped<IOrderItemRepository, OrderItemRepository>();
                

        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration) =>
            services.AddHangfire(c => c
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
            
                //.UseSQLiteStorage(configuration.GetConnectionString("DefaultConnection")))
                //.AddHangfireServer();

                .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                {
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                }))
                .AddHangfireServer();

        // Add the processing server as IHostedService

        public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration) =>
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection")!,
                name: "SQL Server",
                healthQuery: "SELECT 1;",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "Feedback", "Database" })
            .AddCheck<CustomHealthCheck>("CustomHealthCheck", tags: new[] { "custom" });

        public static void ConfigureHostingServices(this IServiceCollection services, IConfiguration configuration) =>
            services.AddScoped<ICategoryService, CategoryService>()
                    .AddScoped<IProductService, ProductService>()
                    .AddScoped<IRoleManagementService, RoleManagementService>()
                    .AddScoped<IUserManagementService, UserManagementService>()
                    .AddScoped<ICartService, CartService>()
                    .AddScoped<IEmailService, EmailService>()
                    .AddScoped<INotificationService, NotificationService>()
                    .AddScoped<IOrderService, OrderService>();
    }

}
