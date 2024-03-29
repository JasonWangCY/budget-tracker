﻿using BudgetTracker.Application.Constants;
using BudgetTracker.Infrastructure.Data;
using BudgetTracker.Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BudgetTracker.Application.Constants.Constants;

namespace BudgetTracker.Infrastructure.Identity;

public class ApplicationDbContextSeed
{
    public static async Task SeedAsync(
        ApplicationDbContext applicationDbContext,
        BudgetTrackerDbContext budgetTrackerDbContext,
        RoleManager<IdentityRole> roleManager,
        IUserService userService)
    {
        await MigrateDatabase(applicationDbContext);
        await MigrateDatabase(budgetTrackerDbContext);

        foreach (var userRole in UserRoles)
        {
            await roleManager.CreateAsync(new IdentityRole(userRole));
        }

        // Create default user
        var defaultUser = new ApplicationUser
        {
            UserName = AuthorizationConstants.DEFAULT_USER_NAME,
            FirstName = AuthorizationConstants.DEFAULT_USER_NAME,
            Email = AuthorizationConstants.DEFAULT_USER_EMAIL
        };
        var userRoles = new List<string> { UserRole.USER };
        await userService.AddUser(defaultUser, AuthorizationConstants.DEFAULT_USER_PASSWORD, userRoles);

        // Create default admin
        var adminUser = new ApplicationUser
        {
            UserName = AuthorizationConstants.DEFAULT_ADMIN_NAME,
            FirstName = AuthorizationConstants.DEFAULT_ADMIN_NAME,
            Email = AuthorizationConstants.DEFAULT_ADMIN_EMAIL
        };
        var adminUserRoles = new List<string> { UserRole.ADMIN, UserRole.USER };
        await userService.AddUser(adminUser, AuthorizationConstants.DEFAULT_ADMIN_PASSWORD, adminUserRoles);
    }

    private static async Task MigrateDatabase(DbContext dbContext)
    {
        if (dbContext.Database.IsNpgsql())
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}
