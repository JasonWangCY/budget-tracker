using BudgetTracker.Domain.Entities.TransactionAggregate;

namespace BudgetTracker.Domain.Entities.BudgetAggregate;

public class BudgetTransactionType
{
    public string BudgetId { get; private set; } = null!;
    public string TransactionTypeId { get; private set; } = null!;
    public Budget Budget { get; private set; } = null!;
    public TransactionType TransactionType { get; private set; } = null!;
}
