namespace BudgetTracker.WebApi.TransferModels;

public class RegisterRequest
{
    public string UserName { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string? LastName { get; init; }
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}

public class RegisterResponse
{
    public string Status { get; init; } = null!;
    public string Message { get; init; } = null!;
}

public static class RegisterStatus
{
    public const string SUCCESS = "SUCCESS";
    public const string ERROR = "ERROR";
}