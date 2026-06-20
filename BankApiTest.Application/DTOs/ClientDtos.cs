using System;

namespace BankApiTest.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a Client's details.
    /// </summary>
    public class ClientDto
    {
        /// <summary>
        /// The unique identifier of the client.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The full name of the client.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// The date of birth of the client.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The gender of the client.
        /// </summary>
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// The monthly income of the client.
        /// </summary>
        public decimal MonthlyIncome { get; set; }
    }

    /// <summary>
    /// Data Transfer Object used to create a new Client.
    /// </summary>
    public class CreateClientDto
    {
        /// <summary>
        /// The full name of the client.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// The date of birth of the client.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The gender of the client.
        /// </summary>
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// The monthly income of the client.
        /// </summary>
        public decimal MonthlyIncome { get; set; }
    }
}