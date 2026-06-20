using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BankApiTest.Application.DTOs;
using BankApiTest.Core.Entities;
using BankApiTest.Core.Exceptions;
using BankApiTest.Core.Interfaces;

namespace BankApiTest.Application.Services
{
    /// <summary>
    /// Application service interface for Account and Transaction operations.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Creates a new bank account associated with a client.
        /// </summary>
        Task<AccountDto> CreateAccountAsync(CreateAccountDto dto);

        /// <summary>
        /// Gets the current balance for an account using its account number.
        /// </summary>
        Task<decimal> GetBalanceAsync(string accountNumber);

        /// <summary>
        /// Processes a deposit transaction and updates the account balance.
        /// </summary>
        Task<TransactionDto> DepositAsync(TransactionRequestDto dto);

        /// <summary>
        /// Processes a withdrawal transaction with balance validation.
        /// </summary>
        Task<TransactionDto> WithdrawAsync(TransactionRequestDto dto);

        /// <summary>
        /// Gets the chronological transaction history for an account.
        /// </summary>
        Task<IEnumerable<TransactionDto>> GetTransactionHistoryAsync(string accountNumber);
    }

    /// <summary>
    /// Application service implementation for Account and Transaction operations.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountNumberGenerator _accountNumberGenerator;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IAccountNumberGenerator accountNumberGenerator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accountNumberGenerator = accountNumberGenerator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new bank account associated with a client.
        /// </summary>
        public async Task<AccountDto> CreateAccountAsync(CreateAccountDto dto)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(dto.ClientId);
            if (client == null)
                throw new NotFoundException($"Client with ID {dto.ClientId} not found.");

            var account = new Account
            {
                ClientId = dto.ClientId,
                Balance = dto.InitialBalance,
                AccountNumber = _accountNumberGenerator.Generate()
            };

            await _unitOfWork.Accounts.AddAsync(account);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AccountDto>(account);
        }

        /// <summary>
        /// Gets the current balance for an account using its account number.
        /// </summary>
        public async Task<decimal> GetBalanceAsync(string accountNumber)
        {
            var account = await _unitOfWork.Accounts.GetByAccountNumberAsync(accountNumber);
            if (account == null)
                throw new NotFoundException($"Account {accountNumber} not found.");

            return account.Balance;
        }

        /// <summary>
        /// Processes a deposit transaction and updates the account balance.
        /// </summary>
        public async Task<TransactionDto> DepositAsync(TransactionRequestDto dto)
        {
            var account = await _unitOfWork.Accounts.GetByAccountNumberAsync(dto.AccountNumber);
            if (account == null)
                throw new NotFoundException($"Account {dto.AccountNumber} not found.");

            if (dto.Amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            account.Balance += dto.Amount;

            var transaction = new Transaction
            {
                AccountId = account.Id,
                Amount = dto.Amount,
                Type = TransactionType.Deposit,
                Timestamp = DateTime.UtcNow,
                ResultingBalance = account.Balance
            };

            await _unitOfWork.Transactions.AddAsync(transaction);
            await _unitOfWork.Accounts.UpdateAsync(account);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<TransactionDto>(transaction);
        }

        /// <summary>
        /// Processes a withdrawal transaction with balance validation.
        /// </summary>
        public async Task<TransactionDto> WithdrawAsync(TransactionRequestDto dto)
        {
            var account = await _unitOfWork.Accounts.GetByAccountNumberAsync(dto.AccountNumber);
            if (account == null)
                throw new NotFoundException($"Account {dto.AccountNumber} not found.");

            if (dto.Amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");

            if (account.Balance < dto.Amount)
                throw new InsufficientFundsException($"Insufficient funds. Current balance is {account.Balance}.");

            account.Balance -= dto.Amount;

            var transaction = new Transaction
            {
                AccountId = account.Id,
                Amount = dto.Amount,
                Type = TransactionType.Withdrawal,
                Timestamp = DateTime.UtcNow,
                ResultingBalance = account.Balance
            };

            await _unitOfWork.Transactions.AddAsync(transaction);
            await _unitOfWork.Accounts.UpdateAsync(account);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<TransactionDto>(transaction);
        }

        /// <summary>
        /// Gets the chronological transaction history for an account.
        /// </summary>
        public async Task<IEnumerable<TransactionDto>> GetTransactionHistoryAsync(string accountNumber)
        {
            var account = await _unitOfWork.Accounts.GetByAccountNumberAsync(accountNumber);
            if (account == null)
                throw new NotFoundException($"Account {accountNumber} not found.");

            var transactions = await _unitOfWork.Transactions.GetByAccountIdAsync(account.Id);
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }
    }
}