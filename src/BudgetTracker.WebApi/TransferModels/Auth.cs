namespace BudgetTracker.WebApi.TransferModels;

public class AuthRequest
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
}

public class AuthResponse
{
    public string Token { get; init; } = null!;
    public DateTime Expiration { get; init; }
}