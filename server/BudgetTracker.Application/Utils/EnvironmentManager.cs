namespace BudgetTracker.Application.Utils;

public static class EnvironmentManager
{
    public static string GetAspDotNetEnvironment()
    {
        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
    }

    public static bool GetSeedDb()
    {
        return bool.TryParse(Environment.GetEnvironmentVariable("SEED_DB"), out var result) && result;
    }

    public static string GetDatabaseConnection()
    {
        return Environment.GetEnvironmentVariable("DB_CONN")!;
    }

    public static string[] GetCorsOrigins()
    {
        var corsOriginsStr = Environment.GetEnvironmentVariable("CORS_ORIGINS");
        return corsOriginsStr?.Split(",") ?? Array.Empty<string>();
    }
}
