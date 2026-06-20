using System.Threading.Tasks;

namespace BankApiTest.Core.Interfaces
{
    /// <summary>
    /// Unit of Work interface to manage database transactions and ensure data consistency.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the client repository.
        /// </summary>
        IClientRepository Clients { get; }

        /// <summary>
        /// Gets the account repository.
        /// </summary>
        IAccountRepository Accounts { get; }

        /// <summary>
        /// Gets the transaction repository.
        /// </summary>
        ITransactionRepository Transactions { get; }

        /// <summary>
        /// Commits all changes made within this unit of work to the underlying database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> CompleteAsync();
    }
}
