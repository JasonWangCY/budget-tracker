using BudgetTracker.Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BudgetTracker.Common.Constants;

namespace BudgetTracker.Infrastructure.Identity;

public class ApplicationDbContextSeed
{
    public static async Task SeedAsync(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (dbContext.Database.IsNpgsql())
        {
            await dbContext.Database.MigrateAsync();
        }

        foreach (var userRole in UserRoles)
        {
            await roleManager.CreateAsync(new IdentityRole(userRole));
        }

        // Create default user
        var defaultUser = new ApplicationUser
        {
            UserName = AuthorizationConstants.DEFAULT_USER_NAME,
            Email = AuthorizationConstants.DEFAULT_USER_EMAIL
        };
        await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_USER_PASSWORD);
        defaultUser = await userManager.FindByNameAsync(AuthorizationConstants.DEFAULT_ADMIN_NAME);
        await userManager.AddToRoleAsync(defaultUser, UserRole.USER);

        // Create default admin
        var adminUser = new ApplicationUser
        {
            UserName = AuthorizationConstants.DEFAULT_ADMIN_NAME,
            Email = AuthorizationConstants.DEFAULT_ADMIN_EMAIL
        };
        await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_ADMIN_PASSWORD);
        adminUser = await userManager.FindByNameAsync(AuthorizationConstants.DEFAULT_ADMIN_NAME);
        await userManager.AddToRoleAsync(adminUser, UserRole.ADMIN);
    }
}
