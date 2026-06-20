using System;
using System.Threading.Tasks;
using AutoMapper;
using BankApiTest.Application.DTOs;
using BankApiTest.Application.Mappings;
using BankApiTest.Application.Services;
using BankApiTest.Core.Entities;
using BankApiTest.Core.Exceptions;
using BankApiTest.Core.Interfaces;
using Moq;
using Xunit;

namespace BankApiTest.Tests
{
    public class AccountServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IAccountRepository> _mockAccountRepo;
        private readonly Mock<ITransactionRepository> _mockTransactionRepo;
        private readonly IMapper _mapper;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockAccountRepo = new Mock<IAccountRepository>();
            _mockTransactionRepo = new Mock<ITransactionRepository>();

            _mockUnitOfWork.Setup(u => u.Accounts).Returns(_mockAccountRepo.Object);
            _mockUnitOfWork.Setup(u => u.Transactions).Returns(_mockTransactionRepo.Object);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<TransactionDto>(It.IsAny<Transaction>()))
                      .Returns((Transaction t) => new TransactionDto { Amount = t.Amount });
            _mapper = mockMapper.Object;

            var mockGenerator = new Mock<IAccountNumberGenerator>();
            _accountService = new AccountService(_mockUnitOfWork.Object, mockGenerator.Object, _mapper);
        }

        [Fact]
        public async Task DepositAsync_ValidAmount_IncreasesBalance()
        {
            // Arrange
            var account = new Account { Id = 1, AccountNumber = "ACC-123", Balance = 100 };
            _mockAccountRepo.Setup(r => r.GetByAccountNumberAsync("ACC-123")).ReturnsAsync(account);
            
            var dto = new TransactionRequestDto { AccountNumber = "ACC-123", Amount = 50 };

            // Act
            var result = await _accountService.DepositAsync(dto);

            // Assert
            Assert.Equal(150, account.Balance);
            Assert.Equal(50, result.Amount);
            _mockTransactionRepo.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task WithdrawAsync_SufficientFunds_DecreasesBalance()
        {
            // Arrange
            var account = new Account { Id = 1, AccountNumber = "ACC-123", Balance = 100 };
            _mockAccountRepo.Setup(r => r.GetByAccountNumberAsync("ACC-123")).ReturnsAsync(account);
            
            var dto = new TransactionRequestDto { AccountNumber = "ACC-123", Amount = 50 };

            // Act
            var result = await _accountService.WithdrawAsync(dto);

            // Assert
            Assert.Equal(50, account.Balance);
            Assert.Equal(50, result.Amount);
            _mockTransactionRepo.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task WithdrawAsync_InsufficientFunds_ThrowsInsufficientFundsException()
        {
            // Arrange
            var account = new Account { Id = 1, AccountNumber = "ACC-123", Balance = 10 };
            _mockAccountRepo.Setup(r => r.GetByAccountNumberAsync("ACC-123")).ReturnsAsync(account);
            
            var dto = new TransactionRequestDto { AccountNumber = "ACC-123", Amount = 50 };

            // Act & Assert
            await Assert.ThrowsAsync<InsufficientFundsException>(() => _accountService.WithdrawAsync(dto));
        }
    }
}
