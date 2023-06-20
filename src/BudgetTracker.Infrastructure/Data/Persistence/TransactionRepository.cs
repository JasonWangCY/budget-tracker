using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private readonly BudgetTrackerDbContext _dbContext;

    public TransactionRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Transaction>> GetTransactionsWithinDateRangeAsync(DateTime startDate, DateTime endDate, string userId)
    {
        return await dbSet
            .Where(x => x.UserId == userId &&
            x.TransactionDate >= startDate &&
            x.TransactionDate <= endDate)
            .ToListAsync();
    }

    public async Task<List<Transaction>> GetTransactionsBeforeDateAsync(DateTime endDate, string userId)
    {
        return await dbSet
            .Where(x => x.UserId == userId &&
            x.TransactionDate <= endDate)
            .ToListAsync();
    }

    public async Task<List<Transaction>> GetTransactionsAfterDateAsync(DateTime startDate, string userId)
    {
        return await dbSet
            .Where(x => x.UserId == userId &&
            x.TransactionDate >= startDate)
            .ToListAsync();
    }

    // TODO: Implement CQRS.
    public async Task<TransactionType?> GetTransactionType(string typeId, string userId)
    {
        return await _dbContext.TransactionTypes
            .FirstOrDefaultAsync(x => x.UserId == userId &&
            x.TransactionTypeId== typeId);
    }

    public async Task<List<TransactionType>> GetTransactionTypes(IEnumerable<string> typeIds, string userId)
    {
        return await _dbContext.TransactionTypes
            .Where(x => x.UserId == userId &&
            typeIds.Contains(x.TransactionTypeId)).ToListAsync();
    }
}
