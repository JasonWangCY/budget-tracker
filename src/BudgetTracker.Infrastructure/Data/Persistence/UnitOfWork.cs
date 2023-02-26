using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepo => throw new NotImplementedException();

    public IBudgetRepository BudgetRepo => throw new NotImplementedException();

    public ITransactionRepository TransactionRepo => throw new NotImplementedException();

    public ICategoryRepository CategoryRepo => throw new NotImplementedException();

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
