using BudgetTracker.Domain.Entities.TransactionAggregate;

namespace BudgetTracker.Domain.Entities.BudgetAggregate;

public class BudgetTransactionType
{
    public string BudgetId { get; private set; } = null!;
    public string TransactionTypeId { get; private set; } = null!;
    public virtual Budget Budget { get; private set; } = null!;
    public virtual TransactionType TransactionType { get; private set; } = null!;
}
