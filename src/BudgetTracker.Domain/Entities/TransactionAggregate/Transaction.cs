using BudgetTracker.Domain.Constants;

namespace BudgetTracker.Domain.Entities.TransactionAggregate;

public class Transaction : BaseEntity
{
    public DateTime TransactionDate { get; private set; }
    public Guid TransactionId { get; private set; }
    public decimal TransactionAmount { get; private set; }
    public string? Currency { get; private set; }
    public List<CategoryType> Category { get; private set; } = null!;
    public string? Description { get; private set; } = null!;
    public string TransactionType { get; private set; } = null!;

    public Transaction(
        DateTime transactionDate,
        Guid transactionId,
        decimal transactionAmount,
        string? currency,
        List<CategoryType> category,
        string? description,
        string transactionType)
    {
        TransactionDate = transactionDate;
        TransactionId = transactionId;
        Currency = currency;
        Category = category;
        Description = description;
        SetTransactionAmount(transactionAmount);
        SetTransactionType(transactionType);
    }
    
    public void SetTransactionAmount(decimal transactionAmount)
    {
        TransactionAmount = Math.Abs(transactionAmount);
    }

    public void SetTransactionType(string transactionType)
    {
        if (!TransactionConstants.TransactionTypes.Contains(transactionType))
        {
            throw new ApplicationException($"{transactionType} is not supported");
        }

        TransactionType = transactionType;
    }
}
