using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankApiTest.Core.Entities;
using BankApiTest.Core.Interfaces;
using BankApiTest.Infrastructure.Data;

namespace BankApiTest.Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of the account repository for database operations on Account entities.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account?> GetByAccountNumberAsync(string accountNumber)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<Account> AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            return account;
        }

        public Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            return Task.CompletedTask;
        }
    }
}