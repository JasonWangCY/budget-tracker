﻿using BudgetTracker.Application.Services.Interfaces;
using BudgetTracker.Application.Utils;
using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.Entities.BudgetAggregate;
using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;
using BudgetTracker.Domain.Services;
using BudgetTracker.Domain.Services.Interfaces;
using BudgetTracker.Infrastructure.Data;
using BudgetTracker.Infrastructure.Data.Persistence;
using BudgetTracker.Infrastructure.Identity;
using BudgetTracker.Infrastructure.Identity.Interfaces;
using BudgetTracker.WebApi.Configs.Models;
using BudgetTracker.WebApi.Services;
using BudgetTracker.WebApi.Services.Interfaces;
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
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddLogging(x => x.AddSerilog())
            .AddSingleton(Log.Logger)
            .AddScoped<ITransactionService, TransactionService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<IDtoConverter, DtoConverter>();

        return services;
    }

    public static IServiceCollection RegisterDatabase(this IServiceCollection services)
    {
        // Entity Framework
        var dbConnection = EnvironmentManager.GetDatabaseConnection();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(dbConnection).UseSnakeCaseNamingConvention()
        );
        services.AddDbContext<BudgetTrackerDbContext>(options =>
            {
                options.UseNpgsql(dbConnection).UseSnakeCaseNamingConvention();
                options.UseLazyLoadingProxies();
            }
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

        // Unit of work + repository pattern
        services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IDatabaseTransaction, BudgetTrackerDatabaseTransaction>()
            .AddScoped<IBudgetRepository, BudgetRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<ITransactionRepository, TransactionRepository>()
            .AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IGenericRepository<Budget>, GenericRepository<Budget>>()
            .AddScoped<IGenericRepository<Category>, GenericRepository<Category>>()
            .AddScoped<IGenericRepository<Transaction>, GenericRepository<Transaction>>()
            .AddScoped<IGenericRepository<User>, GenericRepository<User>>();

        return services;
    }

    public static IServiceCollection RegisterAuth(this IServiceCollection services, IConfiguration conf)
    {
        var jwtOptions = new JwtOptions();
        conf.GetSection(nameof(JwtOptions)).Bind(jwtOptions);

        services.AddScoped<ITokenClaimService, IdentityTokenClaimService>()
            .AddSingleton(jwtOptions);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            if (EnvironmentManager.GetAspDotNetEnvironment() == Environments.Development)
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
                ValidAudience = jwtOptions.ValidAudience,
                ValidIssuer = jwtOptions.ValidIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
            };
        });

        return services;
    }

    public static IServiceCollection ConfigApi(this IServiceCollection services)
    {
        var corsOrigins = EnvironmentManager.GetCorsOrigins();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(corsOrigins).AllowAnyMethod().AllowAnyHeader();
            });
        });
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
