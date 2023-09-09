namespace BudgetTracker.WebApi.Configs.Models;

public class JwtOptions
{
    public string ValidAudience { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
    public string Secret { get; set; } = null!;
}
