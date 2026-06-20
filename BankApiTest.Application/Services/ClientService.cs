using System.Threading.Tasks;
using AutoMapper;
using BankApiTest.Application.DTOs;
using BankApiTest.Core.Entities;
using BankApiTest.Core.Interfaces;

namespace BankApiTest.Application.Services
{
    /// <summary>
    /// Application service interface for Client management.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Creates a new client and persists it to the database.
        /// </summary>
        Task<ClientDto> CreateClientAsync(CreateClientDto dto);

        /// <summary>
        /// Retrieves a client by its unique identifier.
        /// </summary>
        Task<ClientDto?> GetClientByIdAsync(int id);
    }

    /// <summary>
    /// Application service implementation for Client management.
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ClientDto> CreateClientAsync(CreateClientDto dto)
        {
            var client = _mapper.Map<Client>(dto);
            await _unitOfWork.Clients.AddAsync(client);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto?> GetClientByIdAsync(int id)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            return client == null ? null : _mapper.Map<ClientDto>(client);
        }
    }
}