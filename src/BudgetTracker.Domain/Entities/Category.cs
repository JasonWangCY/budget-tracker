namespace BudgetTracker.Domain.Entities;

public class Category
{
    public string CategoryId { get; private set; } = null!;
    public string CategoryName { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool IsDefaultCategory { get; private set;} = false;
    public User User { get; private set; } = new();
}
