namespace BudgetTracker.Domain.Entities.BudgetAggregate;

public class BudgetCategory
{
    public string BudgetId { get; private set; } = null!;
    public string CategoryId { get; private set; } = null!;
    public virtual Budget Budget { get; private set; } = null!;
    public virtual Category Category { get; private set; } = null!;
}
