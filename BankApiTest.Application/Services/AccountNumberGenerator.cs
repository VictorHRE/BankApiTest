using System;

namespace BankApiTest.Application.Services
{
    /// <summary>
    /// Contract for generating unique account numbers.
    /// </summary>
    public interface IAccountNumberGenerator
    {
        /// <summary>
        /// Generates a correctly formatted account number.
        /// </summary>
        /// <returns>A string representing the account number.</returns>
        string Generate();
    }

    /// <summary>
    /// Implementation of the account number generator according to the business rules.
    /// </summary>
    public class AccountNumberGenerator : IAccountNumberGenerator
    {
        /// <summary>
        /// Generates a new account number.
        /// </summary>
        /// <returns>A string representing the new account number.</returns>
        public string Generate()
        {
            var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var random = new Random();
            var randomPart = random.Next(0, 10000).ToString("D4");
            return $"ACC-{datePart}-{randomPart}";
        }
    }
}