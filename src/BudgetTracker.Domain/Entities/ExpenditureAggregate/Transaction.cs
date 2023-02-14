namespace BudgetTracker.Domain.Entities.ExpenditureAggregate;

public class Transaction
{
    public DateTime TransactionDate { get; set; }
    public Guid TransactionId { get; set; }
    public string UserId { get; set; } = null!;
    public decimal TransactionAmount { get; set; }
    public string Category { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string TransactionType { get; set; } = null!;
}
