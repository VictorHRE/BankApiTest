using System.Collections.Generic;
using System.Threading.Tasks;
using BankApiTest.Core.Entities;

namespace BankApiTest.Core.Interfaces
{
    /// <summary>
    /// Repository interface for managing Transaction entities.
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Adds a new transaction to the repository.
        /// </summary>
        Task<Transaction> AddAsync(Transaction transaction);

        /// <summary>
        /// Retrieves all transactions associated with a specific account identifier.
        /// </summary>
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId);
    }
}
