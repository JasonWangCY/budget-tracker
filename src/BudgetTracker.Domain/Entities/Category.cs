namespace BudgetTracker.Domain.Entities;

public class Category
{
    public string CategoryId { get; private set; } = null!;
    public string CategoryName { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool IsDefaultCategory { get; private set;} = false;
    public string UserId { get; set; } = null!;
    public User User { get; private set; } = null!;
}
