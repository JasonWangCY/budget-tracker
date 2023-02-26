using BudgetTracker.Domain.PersistenceInterfaces.Repositories;

namespace BudgetTracker.Domain.PersistenceInterfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    IUserRepository UserRepo { get; }
    IBudgetRepository BudgetRepo { get; }
    ITransactionRepository TransactionRepo { get; }
    ICategoryRepository CategoryRepo { get; }
}
