using System.Threading.Tasks;
using BankApiTest.Core.Interfaces;
using BankApiTest.Infrastructure.Data;

namespace BankApiTest.Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of the Unit of Work pattern to ensure transactional integrity.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IClientRepository Clients { get; }
        public IAccountRepository Accounts { get; }
        public ITransactionRepository Transactions { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Clients = new ClientRepository(_context);
            Accounts = new AccountRepository(_context);
            Transactions = new TransactionRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}