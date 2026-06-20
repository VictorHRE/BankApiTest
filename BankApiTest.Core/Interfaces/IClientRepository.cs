using System.Collections.Generic;
using System.Threading.Tasks;
using BankApiTest.Core.Entities;

namespace BankApiTest.Core.Interfaces
{
    /// <summary>
    /// Repository interface for managing Client entities.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Retrieves a client by their unique identifier.
        /// </summary>
        Task<Client?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new client to the repository.
        /// </summary>
        Task<Client> AddAsync(Client client);
    }
}
