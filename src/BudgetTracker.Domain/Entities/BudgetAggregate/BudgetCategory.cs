namespace BudgetTracker.Domain.Entities.BudgetAggregate;

public class BudgetCategory
{
    public string BudgetId { get; private set; } = null!;
    public string CategoryId { get; private set; } = null!;
    public Budget Budget { get; private set; } = null!;
    public Category Category { get; private set; } = null!;
}
