using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class TransactionRepository : Repository, ITransactionRepository
{
    private readonly BudgetTrackerDbContext _dbContext;

    public TransactionRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Transaction>> GetTransactionsWithinTimeRange(DateTime startDate, DateTime endDate, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionType> GetTypeByUserId(string typeName, string userId)
    {
        throw new NotImplementedException();
    }
}
