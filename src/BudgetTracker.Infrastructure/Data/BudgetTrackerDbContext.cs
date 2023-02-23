using BudgetTracker.Domain.Entities.TransactionAggregate;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Infrastructure.Data;

// TODO: Combine this DbContext with ApplicationDbContext?
public class BudgetTrackerDbContext : DbContext
{
    public BudgetTrackerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasKey(x => x.TransactionId);
    }
}
