namespace BudgetTracker.Domain.PersistenceInterfaces;

public interface IGenericRepository<TClass> where TClass : class, IAggregateRoot
{
    Task<TClass?> GetByIdAsync(string id);
    Task AddAsync(TClass entity);
    Task AddRangeAsync(IEnumerable<TClass> entities);
    void RemoveRange(IEnumerable<TClass> entities);
}
