using System;
using System.Collections.Generic;

namespace BankApiTest.Core.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public decimal MonthlyIncome { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
