namespace BudgetTracker.Domain.PersistenceInterfaces;

// This interface has no purpose other than enforcing the root with which the repositories work with.
// IGenericRepository extends IAggregateRoot.
// E.g. The Budget Entity implements IAggregateRoot, hence IBudgetRepository can be created.
// This is not possible if Budget Entity does not implement IAggregateRoot.
public interface IAggregateRoot
{
}
