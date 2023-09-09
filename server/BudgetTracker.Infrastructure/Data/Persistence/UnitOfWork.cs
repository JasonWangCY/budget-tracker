using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly BudgetTrackerDbContext _dbContext;
    public IUserRepository Users { get; private set; }
    public IBudgetRepository Budgets { get; private set; }
    public ITransactionRepository Transactions { get; private set; }
    public ICategoryRepository Categories { get; private set; }

    public UnitOfWork(
        BudgetTrackerDbContext dbContext,
        IUserRepository users,
        IBudgetRepository budgets,
        ITransactionRepository transactions,
        ICategoryRepository categories)
    {
        _dbContext = dbContext;
        Users = users;
        Budgets = budgets;
        Transactions = transactions;
        Categories = categories;
    }

    public IDatabaseTransaction BeginTransaction()
    {
        return new BudgetTrackerDatabaseTransaction(_dbContext);
    }

    public async Task<int> SaveChangesAsync()
    {
        var numberOfStatesWritten = await _dbContext.SaveChangesAsync();
        return numberOfStatesWritten;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
