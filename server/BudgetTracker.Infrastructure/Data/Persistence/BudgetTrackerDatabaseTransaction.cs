using BudgetTracker.Domain.PersistenceInterfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class BudgetTrackerDatabaseTransaction : IDatabaseTransaction
{
    private readonly IDbContextTransaction _transaction;

    public BudgetTrackerDatabaseTransaction(BudgetTrackerDbContext context)
    {
        _transaction = context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _transaction.Commit();
    }

    public void Rollback()
    {
        _transaction.Rollback();
    }

    public void Dispose()
    {
        _transaction.Dispose();
    }
}
