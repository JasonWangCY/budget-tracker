namespace BudgetTracker.Domain.PersistenceInterfaces;

public interface IDatabaseTransaction : IDisposable
{
    void Commit();
    void Rollback();
}
