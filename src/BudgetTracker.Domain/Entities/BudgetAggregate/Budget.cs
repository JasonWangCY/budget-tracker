﻿using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.PersistenceInterfaces;

namespace BudgetTracker.Domain.Entities.BudgetAggregate;

public class Budget : BaseEntity, IAggregateRoot
{
    public string BudgetId { get; private set; } = null!;
    public string BudgetName { get; private set; } = null!;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal SpendingLimit { get; private set; }
    public string? Description { get; private set; }
    public List<Category> Categories { get; set; } = new();
    public List<TransactionType>? TransactionTypes { get; private set; }
    public User User { get; private set; } = null!;
}
