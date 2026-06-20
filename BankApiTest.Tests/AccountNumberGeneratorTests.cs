using System;
using System.Text.RegularExpressions;
using BankApiTest.Application.Services;
using Xunit;

namespace BankApiTest.Tests
{
    public class AccountNumberGeneratorTests
    {
        private readonly IAccountNumberGenerator _generator;

        public AccountNumberGeneratorTests()
        {
            _generator = new AccountNumberGenerator();
        }

        [Fact]
        public void Generate_ReturnsCorrectFormatAndLength()
        {
            // Act
            var accountNumber = _generator.Generate();

            // Assert
            Assert.NotNull(accountNumber);
            Assert.Equal(17, accountNumber.Length); // ACC-(4) + YYYYMMDD(8) + -(1) + XXXX(4) = 17
            
            var today = DateTime.UtcNow.ToString("yyyyMMdd");
            Assert.StartsWith($"ACC-{today}-", accountNumber);

            // Match exact regex: ^ACC-\d{8}-\d{4}$
            Assert.Matches(@"^ACC-\d{8}-\d{4}$", accountNumber);
        }

        [Fact]
        public void Generate_ReturnsUniqueNumbers()
        {
            // Act
            var accountNumber1 = _generator.Generate();
            var accountNumber2 = _generator.Generate();

            // Assert
            Assert.NotEqual(accountNumber1, accountNumber2);
        }
    }
}
