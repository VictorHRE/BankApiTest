using Microsoft.EntityFrameworkCore;
using BankApiTest.Core.Entities;

namespace BankApiTest.Infrastructure.Data
{
    /// <summary>
    /// Represents the database context for the banking application.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Account Entity
            // The AccountNumber must have a unique constraint to avoid duplications.
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.AccountNumber)
                .IsUnique();

            // Configure Relationships
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Accounts)
                .WithOne(a => a.Client)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure ResultingBalance is properly mapped or has precision if needed (SQLite doesn't strictly enforce decimal scale, but good practice)
            modelBuilder.Entity<Account>().Property(a => a.Balance).HasConversion<double>();
            modelBuilder.Entity<Transaction>().Property(t => t.Amount).HasConversion<double>();
            modelBuilder.Entity<Transaction>().Property(t => t.ResultingBalance).HasConversion<double>();
            modelBuilder.Entity<Client>().Property(c => c.MonthlyIncome).HasConversion<double>();
        }
    }
}