using System.Collections.Generic;
using System.Threading.Tasks;
using BankApiTest.Core.Entities;

namespace BankApiTest.Core.Interfaces
{
    /// <summary>
    /// Repository interface for managing Account entities.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Retrieves an account by its unique identifier.
        /// </summary>
        Task<Account?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves an account by its unique account number.
        /// </summary>
        Task<Account?> GetByAccountNumberAsync(string accountNumber);

        /// <summary>
        /// Adds a new account to the repository.
        /// </summary>
        Task<Account> AddAsync(Account account);

        /// <summary>
        /// Updates an existing account's information.
        /// </summary>
        Task UpdateAsync(Account account);
    }
}
