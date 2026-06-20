using System;

namespace BankApiTest.Core.Entities
{
    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }

    public class Transaction
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public decimal ResultingBalance { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}
