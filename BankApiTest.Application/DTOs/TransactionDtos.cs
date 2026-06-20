using System;

namespace BankApiTest.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a Transaction.
    /// </summary>
    public class TransactionDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the transaction (e.g., Deposit, Withdrawal).
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the amount of money involved in the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the transaction occurred.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the balance remaining after the transaction is applied.
        /// </summary>
        public decimal ResultingBalance { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the account associated with the transaction.
        /// </summary>
        public int AccountId { get; set; }
    }

    /// <summary>
    /// Data Transfer Object used to register a deposit or withdrawal.
    /// </summary>
    public class TransactionRequestDto
    {
        /// <summary>
        /// Gets or sets the account number where the transaction will be applied.
        /// </summary>
        public string AccountNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the amount of money to be deposited or withdrawn.
        /// </summary>
        public decimal Amount { get; set; }
    }
}