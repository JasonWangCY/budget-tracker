using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class TransactionRepository : ITransactionRepository
{
    public Task Add(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionType> GetTypeByUserId(string typeName, string userId)
    {
        throw new NotImplementedException();
    }
}
