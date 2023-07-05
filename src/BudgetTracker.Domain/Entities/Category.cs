using BudgetTracker.Domain.PersistenceInterfaces;

namespace BudgetTracker.Domain.Entities;

public class Category : IAggregateRoot
{
    public string CategoryId { get; private set; } = null!;
    public string CategoryName { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool IsDefaultCategory { get; private set; } = false;
    public string? UserId { get; private set; } = null!;
    public virtual User? User { get; private set; } = null!;

    public Category()
    {
        // Used by EF Core migration.
    }

    public Category(
        string categoryName,
        string? description,
        bool isDefaultCategory,
        string? userId
    )
    {
        CategoryId = Guid.NewGuid().ToString();
        CategoryName = categoryName;
        Description = description;
        IsDefaultCategory = isDefaultCategory;
        UserId = userId;
    }
}
