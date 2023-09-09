using BudgetTracker.Domain.PersistenceInterfaces.Repositories;

namespace BudgetTracker.Domain.PersistenceInterfaces;

public interface IUnitOfWork : IDisposable
{
    IDatabaseTransaction BeginTransaction();
    Task<int> SaveChangesAsync();
    IUserRepository Users { get; }
    IBudgetRepository Budgets { get; }
    ITransactionRepository Transactions { get; }
    ICategoryRepository Categories { get; }
}
