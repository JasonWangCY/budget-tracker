using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using BudgetTracker.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using BudgetTracker.Infrastructure.Data;
using BudgetTracker.Infrastructure.Identity.Interfaces;

namespace BudgetTracker.WebApi.Configs;

public static class SetupConfigs
{
    public static void SetUpLogger()
    {
        var outputTemplateStr = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug, outputTemplate: outputTemplateStr, theme: AnsiConsoleTheme.Code)
            .CreateLogger();
    }

    public static async Task SeedDatabase(WebApplication app)
    {
        Log.Information("Seeding Database...");
        using var scope = app.Services.CreateScope();
        var scopedProvider = scope.ServiceProvider;
        try
        {
            var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var identityContext = scopedProvider.GetRequiredService<ApplicationDbContext>();
            var budgetTrackerContext = scopedProvider.GetRequiredService<BudgetTrackerDbContext>();
            var userService = scopedProvider.GetRequiredService<IUserService>();
            await ApplicationDbContextSeed.SeedAsync(identityContext, budgetTrackerContext, userManager, roleManager, userService);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred seeding the DB.");
        }
    }
}
