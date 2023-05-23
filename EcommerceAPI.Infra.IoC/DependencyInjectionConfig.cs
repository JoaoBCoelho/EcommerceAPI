using EcommerceAPI.Application.Interfaces;
using EcommerceAPI.Application.Mappings;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Interfaces;
using EcommerceAPI.Identity.Configuration;
using EcommerceAPI.Identity.Context;
using EcommerceAPI.Identity.Services;
using EcommerceAPI.Infra.Data.Context;
using EcommerceAPI.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace EcommerceAPI.Infra.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }
        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration["SqlServerConnectionString"])
            );
        }
        public static void RegisterIdentity(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<IdentityDataContext>(options =>
                options.UseSqlServer(configuration["IdentityConnectionString"])
            );

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }

        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtOptionsSecurityKey"]));

            //Configure the JtwOptions to use on token generation
            services.Configure<JwtOptions>(options =>
            {
                options.JwtOptionsIssuer = configuration[nameof(JwtOptions.JwtOptionsIssuer)];
                options.JwtOptionsAudience = configuration[nameof(JwtOptions.JwtOptionsAudience)];
                options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                options.JwtOptionsExpiration = int.Parse(configuration[nameof(JwtOptions.JwtOptionsExpiration)] ?? "0");
            });

            //Set Password Requirements
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            });

            //Set token validation parameters
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration[nameof(JwtOptions.JwtOptionsIssuer)],

                ValidateAudience = true,
                ValidAudience = configuration[nameof(JwtOptions.JwtOptionsAudience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            //Set the default authentication as jwt bearer token 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "E-Commerce.Api",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = @"JWT Authorization header using Bearer scheme.
                                Enter 'Bearer' [space] and then your token in the text input below.
                                Example: 'Bearer 123abc'",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
            });
        }
    }
}
