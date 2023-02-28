namespace BudgetTracker.Domain.Entities.TransactionAggregate;

public class Transaction : BaseEntity
{
    public DateTime TransactionDate { get; private set; }
    public string TransactionId { get; private set; } = null!;
    public decimal TransactionAmount { get; private set; }
    public string? Currency { get; private set; }
    public string? Description { get; private set; } = null!;
    public string TransactionTypeId { get; private set; } = null!;
    public string CategoryId { get; private set; } = null!;
    public TransactionType TransactionType { get; private set; } = null!;
    public Category Category { get; private set; } = null!;
    public string UserId { get; private set; } = null!;

    public Transaction(
        DateTime transactionDate,
        string transactionId,
        decimal transactionAmount,
        string? currency,
        string? description,
        TransactionType transactionType,
        Category category,
        string userId)
    {
        TransactionDate = transactionDate;
        TransactionId = transactionId;
        Currency = currency;
        Description = description;
        TransactionType = transactionType;
        Category = category;
        UserId = userId;

        SetTransactionAmount(transactionAmount);
    }
    
    public void SetTransactionAmount(decimal transactionAmount)
    {
        TransactionAmount = Math.Abs(transactionAmount);
    }
}
