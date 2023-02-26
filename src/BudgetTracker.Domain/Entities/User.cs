namespace BudgetTracker.Domain.Entities;

public class User
{
    public string UserId { get; private set; } = null!;
    public string UserName { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public bool UseDarkMode { get; private set; } = true;
    public decimal CurrentBalance { get; private set; }
}
