namespace BudgetTracker.Domain.Entities.TransactionAggregate;

public class TransactionType : BaseEntity
{
    public string TransactionTypeId { get; private set; } = null!;
    public string TransactionTypeName { get; private set; } = null!;
    public string? Description { get; private set; } = null!;
    public TransactionTypeSign Sign { get; private set; }
    public bool IsDefaultType { get; private set; } = false;
    public string? UserId { get; set; }
    public virtual User? User { get; private set; }
    public virtual List<Transaction> Transactions { get; private set; } = new();

    public TransactionType()
    {
        // Used by EF Core migration.
    }

    public TransactionType(
        string transactionTypeName,
        string? description,
        TransactionTypeSign sign,
        bool isDefaultType,
        string? userId)
    {
        TransactionTypeId = Guid.NewGuid().ToString();
        TransactionTypeName = transactionTypeName;
        Description = description;
        Sign = sign;
        IsDefaultType = isDefaultType;
        UserId = userId;
    }
}

public enum TransactionTypeSign
{
    Undefined = 0,
    Plus = 1,
    Minus = -1
}
