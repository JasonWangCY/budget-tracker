using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.Entities.BudgetAggregate;
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
    public DbSet<TransactionType> TransactionTypes { get; set; } = null!;
    public DbSet<Budget> Budgets { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasKey(x => x.TransactionId);
        modelBuilder.Entity<TransactionType>()
            .HasKey(x => x.TransactionTypeId);
        modelBuilder.Entity<Budget>()
            .HasKey(x => x.BudgetId);
        modelBuilder.Entity<Category>()
            .HasKey(x => x.CategoryId);
        modelBuilder.Entity<User>()
            .HasKey(x => x.UserId);

        modelBuilder.Entity<Transaction>()
            .HasOne(x => x.TransactionType)
            .WithMany(y => y.Transactions);
        modelBuilder.Entity<Transaction>()
            .HasOne(x => x.Category)
            .WithMany();
    }
}
