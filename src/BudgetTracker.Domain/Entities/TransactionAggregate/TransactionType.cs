namespace BudgetTracker.Domain.Entities.TransactionAggregate;

public class TransactionType : BaseEntity
{
    public string TransactionTypeId { get; private set; } = null!;
    public string TransactionTypeName { get; private set; } = null!;
    public string? Description { get; private set; } = null!;
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
        bool isDefaultType,
        string? userId)
    {
        TransactionTypeId = Guid.NewGuid().ToString();
        TransactionTypeName = transactionTypeName;
        Description = description;
        IsDefaultType = isDefaultType;
        UserId = userId;
    }

    public void UpdateTransactionType(
        string transactionTypeName,
        string? description)
    {
        TransactionTypeName = transactionTypeName;
        Description = description;
    }
}
