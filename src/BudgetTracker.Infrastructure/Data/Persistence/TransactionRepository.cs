using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class TransactionRepository : Repository, ITransactionRepository
{
    private readonly BudgetTrackerDbContext _dbContext;

    public TransactionRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Transaction>> GetTransactionsWithinTimeRange(DateTime startDate, DateTime endDate, string userId)
    {
        return await _dbContext.Transactions
            .Where(x => x.UserId == userId &&
            x.TransactionDate >= startDate &&
            x.TransactionDate <= endDate)
            .ToListAsync();
    }

    public async Task<TransactionType?> GetTypeByUserId(string typeName, string userId)
    {
        return await _dbContext.TransactionTypes
            .FirstOrDefaultAsync(x => x.UserId == userId &&
            x.TransactionTypeName == typeName);
    }
}
