namespace BudgetTracker.Domain.Entities;

public class User
{
    public string UserId { get; private set; } = null!;
    public string UserName { get; private set; } = null!;
    public string FirstName { get; set; } = null!;
    public bool UseDarkMode { get; set; } = true;
    public decimal CurrentBalance { get; private set; }
}
