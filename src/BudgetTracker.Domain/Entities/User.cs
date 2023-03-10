namespace BudgetTracker.Domain.Entities;

public class User
{
    public string UserId { get; private set; } = null!;
    public string UserName { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string? LastName { get; set; }
    public bool UseDarkMode { get; private set; } = true;
    public decimal CurrentBalance { get; private set; }

    private User()
    {
        // Used by EF Core migration.
    }

    public User(
        string userId,
        string userName,
        string firstName,
        string? lastName)
    {
        UserId = userId;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
    }
}
