using BankApiTest.Application.DTOs;
using BankApiTest.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankApiTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto dto)
        {
            var client = await _clientService.CreateClientAsync(dto);
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
    }
}
