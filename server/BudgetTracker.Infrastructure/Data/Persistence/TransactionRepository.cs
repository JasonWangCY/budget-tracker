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

    public async Task<List<TransactionType>> GetTransactionTypesIncludingDefaultAsync(string userId)
    {
        return await _dbContext.TransactionTypes.Where(x => x.UserId == userId || x.IsDefaultType).ToListAsync();
    }

    public async Task<List<Transaction>> GetTransactions(IEnumerable<string> transactionIds, string userId)
    {
        return await dbSet.Where(x => x.UserId == userId && transactionIds.Contains(x.TransactionId)).ToListAsync();
    }

    public async Task<List<TransactionType>> GetTransactionTypes(IEnumerable<string> transactionTypeIds, string userId)
    {
        return await _dbContext.TransactionTypes.Where(x => x.UserId == userId && transactionTypeIds.Contains(x.TransactionTypeId)).ToListAsync();
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

    // TODO: What if it is default?
    public async Task<TransactionType?> GetTransactionType(string typeId, string userId)
    {
        return await _dbContext.TransactionTypes
            .FirstOrDefaultAsync(x => x.UserId == userId &&
            x.TransactionTypeId== typeId);
    }

    public async Task<List<TransactionType>> GetTransactionTypesInclDefault(IEnumerable<string> typeIds, string userId)
    {
        var allTransactionTypes = await _dbContext.TransactionTypes
            .Where(x => typeIds.Contains(x.TransactionTypeId)).ToListAsync();
        var filteredTransactionTypes = allTransactionTypes.Where(x => x.IsDefaultType || x.UserId == userId);

        return filteredTransactionTypes.ToList();
    }

    public async Task<List<TransactionType>> GetDefaultTransactionTypes(IEnumerable<string> typeIds)
    {
        return await _dbContext.TransactionTypes.Where(x => x.IsDefaultType && typeIds.Contains(x.TransactionTypeId)).ToListAsync();
    }

    public async Task AddTransactionTypes(IEnumerable<TransactionType> transactionTypes)
    {
        await _dbContext.TransactionTypes.AddRangeAsync(transactionTypes);
    }

    public void DeleteTransactionTypes(IEnumerable<TransactionType> transactionTypes)
    {
        _dbContext.TransactionTypes.RemoveRange(transactionTypes);
    }
}
