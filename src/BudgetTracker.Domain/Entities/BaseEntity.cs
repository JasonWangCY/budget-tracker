namespace BudgetTracker.Domain.Entities;

public abstract class BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedTime { get; set; }
}
