namespace BudgetTracker.Domain.Entities;

public abstract class BaseEntity
{
    public virtual int Id { get; protected set; }
    public DateTime CreatedAt { get; set; }
}
