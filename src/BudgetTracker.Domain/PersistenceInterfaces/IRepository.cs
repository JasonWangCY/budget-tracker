namespace BudgetTracker.Domain.PersistenceInterfaces;

public interface IRepository
{
    void Add<TClass>(TClass entity);
}
