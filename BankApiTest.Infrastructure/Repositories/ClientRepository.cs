using System.Threading.Tasks;
using BankApiTest.Core.Entities;
using BankApiTest.Core.Interfaces;
using BankApiTest.Infrastructure.Data;

namespace BankApiTest.Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of the client repository for database operations on Client entities.
    /// </summary>
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client> AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            return client;
        }
    }
}