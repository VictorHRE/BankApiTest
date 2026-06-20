namespace BankApiTest.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing an Account's details.
    /// </summary>
    public class AccountDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public int ClientId { get; set; }
    }

    /// <summary>
    /// Data Transfer Object used to create a new Account.
    /// </summary>
    public class CreateAccountDto
    {
        public int ClientId { get; set; }
        public decimal InitialBalance { get; set; }
    }
}