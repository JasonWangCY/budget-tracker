using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.Entities.BudgetAggregate;
using BudgetTracker.Domain.Entities.TransactionAggregate;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Infrastructure.Data;

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
            .HasOne(x => x.Category);
        modelBuilder.Entity<Transaction>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<TransactionType>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<Budget>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<BudgetCategory>()
            .HasKey(t => new { t.BudgetId, t.CategoryId });
        modelBuilder.Entity<BudgetCategory>()
            .HasOne(x => x.Budget)
            .WithMany(x => x.BudgetCategories)
            .HasForeignKey(x => x.BudgetId);
        modelBuilder.Entity<BudgetCategory>()
            .HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId);

        modelBuilder.Entity<BudgetTransactionType>()
            .HasKey(t => new { t.BudgetId, t.TransactionTypeId });
        modelBuilder.Entity<BudgetTransactionType>()
            .HasOne(x => x.Budget)
            .WithMany(x => x.BudgetTransactionTypes)
            .HasForeignKey(x => x.BudgetId);
        modelBuilder.Entity<BudgetTransactionType>()
            .HasOne(x => x.TransactionType)
            .WithMany()
            .HasForeignKey(x => x.TransactionTypeId);

        modelBuilder.Entity<Category>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}
