using BudgetTracker.Application.Services.Interfaces;
using BudgetTracker.Infrastructure.Data;
using BudgetTracker.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace BudgetTracker.WebApi.Configs;

public static class Dependencies
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration conf)
    {
        services.AddLogging(x => x.AddSerilog())
            .AddSingleton(Log.Logger);

        // Entity Framework
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseNpgsql(conf.GetConnectionString("DbConnection"))
        );
        services.AddDbContext<BudgetTrackerDbContext>(options =>
            options.UseNpgsql(conf.GetConnectionString("DbConnection"))
        );

        // Identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        });

        return services;
    }

    public static IServiceCollection RegisterAuth(this IServiceCollection services, IConfiguration conf)
    {
        services.AddScoped<ITokenClaimService, IdentityTokenClaimService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                options.RequireHttpsMetadata = false;
            }
            options.MapInboundClaims = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = conf["JWT:ValidAudience"],
                ValidIssuer = conf["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:Secret"])),
            };
        });

        return services;
    }

    public static IServiceCollection ConfigApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BudgetTrackerAPI",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n"
                              + "Enter 'Bearer <token>'\r\n\r\n"
                              + "Example: \"Bearer XXXXXXXXX\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }
}
