using BankApiTest.Application.DTOs;
using BankApiTest.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankApiTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public TransactionsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionRequestDto dto)
        {
            var transaction = await _accountService.DepositAsync(dto);
            return Ok(transaction);
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionRequestDto dto)
        {
            var transaction = await _accountService.WithdrawAsync(dto);
            return Ok(transaction);
        }

        [HttpGet("{accountNumber}/history")]
        public async Task<IActionResult> GetHistory(string accountNumber)
        {
            var history = await _accountService.GetTransactionHistoryAsync(accountNumber);
            return Ok(history);
        }
    }
}
