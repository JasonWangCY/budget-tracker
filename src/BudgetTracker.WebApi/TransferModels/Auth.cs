namespace BudgetTracker.WebApi.TransferModels;

public class AuthRequest
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class AuthResponse
{
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
}