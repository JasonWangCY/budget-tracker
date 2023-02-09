namespace BudgetTracker.WebApi.TransferModels;

public class RegisterRequest
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class RegisterResponse
{
    public string Status { get; set; } = null!;
    public string Message { get; set; } = null!;
}