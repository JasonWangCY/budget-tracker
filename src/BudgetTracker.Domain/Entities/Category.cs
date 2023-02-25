namespace BudgetTracker.Domain.Entities;

public class Category
{
    public string CategoryId { get; set; } = null!; 
    public string CategoryName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsDefaultCategory { get; set;} = false;
    public User? User { get; set; }
}
