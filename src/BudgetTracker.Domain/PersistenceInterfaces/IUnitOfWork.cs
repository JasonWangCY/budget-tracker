namespace BudgetTracker.Domain.PersistenceInterfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    IBudgetRepository BudgetRepository { get; }
    ITransactionRepository TransactionRepository { get; }
}
