using System;
using System.Collections.Generic;

namespace BankApiTest.Core.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
