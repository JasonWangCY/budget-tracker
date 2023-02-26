namespace BudgetTracker.Domain.PersistenceInterfaces;

public interface IRepository<T> where T : class
{
    Task Add(T entity);
}
