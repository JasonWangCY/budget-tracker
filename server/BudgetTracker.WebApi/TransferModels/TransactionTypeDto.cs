﻿namespace BudgetTracker.WebApi.TransferModels;

public class TransactionTypeDto
{
    public string TransactionTypeId { get; set; } = null!;
    public string TransactionTypeName { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public bool IsDefaultType { get; set; } = false;
}
