using System.Linq.Expressions;

namespace BudgetTracker.Domain.PersistenceInterfaces;

public interface IGenericRepository<TClass> where TClass : class
{
    Task<TClass?> GetById(string id);
    Task Add(TClass entity);
}
