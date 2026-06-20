using BankApiTest.Application.DTOs;
using BankApiTest.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankApiTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto dto)
        {
            var account = await _accountService.CreateAccountAsync(dto);
            return Created(string.Empty, account);
        }

        [HttpGet("{accountNumber}/balance")]
        public async Task<IActionResult> GetBalance(string accountNumber)
        {
            var balance = await _accountService.GetBalanceAsync(accountNumber);
            return Ok(new { AccountNumber = accountNumber, Balance = balance });
        }
    }
}
