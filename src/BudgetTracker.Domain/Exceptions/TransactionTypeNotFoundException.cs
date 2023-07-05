namespace BudgetTracker.Domain.Exceptions;

public class TransactionTypeNotFoundException : Exception
{
    public TransactionTypeNotFoundException(string type) : base($"Transaction Type: '{type}' does not exist.")
    {
    }

    public TransactionTypeNotFoundException() : base($"Transaction Type does not exist.")
    {
    }
}
