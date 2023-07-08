using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.Constants;

namespace BudgetTracker.Domain.Entities.TransactionAggregate;

public class Transaction : BaseEntity, IAggregateRoot
{
    public DateTime TransactionDate { get; private set; }
    public string TransactionId { get; private set; } = null!;
    public decimal TransactionAmount { get; private set; }
    public string Currency { get; private set; } = null!;
    public string? Description { get; private set; } = null!;
    public string TransactionTypeId { get; private set; } = null!;
    public string CategoryId { get; private set; } = null!;
    public virtual TransactionType TransactionType { get; private set; } = null!;
    public virtual Category Category { get; private set; } = null!;
    public string UserId { get; private set; } = null!;
    public string TransactionStatus { get; private set; } = null!;

    public Transaction()
    {
        // Used by EF Core migration.
    }

    public Transaction(
        DateTime transactionDate,
        string transactionId,
        decimal transactionAmount,
        string currency,
        string? description,
        TransactionType transactionType,
        Category category,
        string userId)
    {
        TransactionDate = transactionDate;
        TransactionId = transactionId;
        TransactionAmount = transactionAmount;
        Currency = currency;
        Description = description;
        TransactionType = transactionType;
        Category = category;
        UserId = userId;
        TransactionStatus = GetTransactionStatus(transactionAmount);
    }

    public void UpdateTransaction(
        DateTime transactionDate,
        decimal transactionAmount,
        string currency,
        string? description,
        string transactionTypeId,
        string categoryId)
    {
        TransactionDate = transactionDate;
        TransactionAmount = transactionAmount;
        Currency = currency;
        Description = description;
        TransactionTypeId = transactionTypeId;
        CategoryId = categoryId;
        TransactionStatus = GetTransactionStatus(transactionAmount);
    }

    private string GetTransactionStatus(decimal transactionAmount)
    {
        if (transactionAmount > 0) return TransactionConstants.TransactionStatus.Gain;
        if (transactionAmount < 0) return TransactionConstants.TransactionStatus.Loss;

        return TransactionConstants.TransactionStatus.NoChange;
    }
}
