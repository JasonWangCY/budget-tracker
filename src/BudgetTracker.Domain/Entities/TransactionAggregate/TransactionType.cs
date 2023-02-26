namespace BudgetTracker.Domain.Entities.TransactionAggregate;

public class TransactionType : BaseEntity
{
    public string TransactionTypeId { get; private set; } = null!;
    public string TransactionTypeName { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public TransactionTypeSign Sign { get; private set; }
    public bool IsDefaultType { get; private set; } = false;
    public User? User { get; private set; }
    public List<Transaction> Transactions { get; private set; } = new();
}

public enum TransactionTypeSign
{
    Undefined,
    Plus,
    Minus
}
