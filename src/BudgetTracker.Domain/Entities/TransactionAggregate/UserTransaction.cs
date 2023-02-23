using BudgetTracker.Domain.Interfaces;

namespace BudgetTracker.Domain.Entities.TransactionAggregate;

public class UserTransaction : BaseEntity, IAggregateRoot
{
    public string UserId { get; set; } = null!;
    public Guid TransactionId { get; set; }
}
