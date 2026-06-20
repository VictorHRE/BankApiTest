# BankApiTest

Backend solution for the core services engine of a digital banking platform, implemented following **Clean Architecture** (N-Tier) and **SOLID** principles in .NET 10.

## Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 or your preferred IDE

## Running the Application

1. Clone this repository to your local machine.
2. Open the solution file (`BankApiTest.slnx`) using Visual Studio 2022 or your preferred IDE.
3. The project is configured to automatically create the local SQLite database (`BankApiTest.db`) upon startup, so there is no need to run EF Core migrations manually.
4. Set **`BankApiTest.API`** as the startup project.
5. Run the application (Press `F5` in Visual Studio or run `dotnet run` inside the `BankApiTest.API` folder).
6. The browser will automatically open at `https://localhost:<port>/scalar/v1` (or redirect you from the root), where you can interact with the visual Scalar UI to test all the system's endpoints.

## How to Test the Endpoints (Step-by-Step)

To verify the core business logic, you can use the interactive Scalar interface (or tools like Postman) following this exact workflow:

1. **Create a Client Profile**
   - **Endpoint:** `POST /api/Clients`
   - **Body:** Provide a JSON with `fullName`, `dateOfBirth` (format: `YYYY-MM-DDTHH:mm:ss`), `gender`, and `monthlyIncome`.
   - **Result:** You will receive a `201 Created` status with the generated Client `id` (e.g., `1`).

2. **Create a Bank Account**
   - **Endpoint:** `POST /api/Accounts`
   - **Body:** Provide the `"clientId": 1` (or your generated ID) and an `"initialBalance": 1000`.
   - **Result:** You will receive a `201 Created` status. The system will auto-generate an account number (e.g., `ACC-20260620-1234`). Copy this `accountNumber` for the next steps.

3. **Check Balance**
   - **Endpoint:** `GET /api/Accounts/{accountNumber}/balance`
   - **Result:** You should see the exact initial balance you provided (e.g., `1000`).

4. **Register a Deposit**
   - **Endpoint:** `POST /api/Transactions/deposit`
   - **Body:** Provide the `accountNumber` and an `amount` to deposit (e.g., `500`).
   - **Result:** A successful transaction response. Checking the balance again should now reflect `1500`.

5. **Register a Withdrawal & Test Insufficient Funds**
   - **Endpoint:** `POST /api/Transactions/withdraw`
   - **Successful Test:** Try withdrawing an amount lower than the balance (e.g., `200`). The balance will properly decrease.
   - **Failure Test (Global Exception Handler):** Try withdrawing an amount higher than the current balance (e.g., `5000`). The API will **not** crash. Instead, it will cleanly return a `400 Bad Request` JSON with an `Insufficient funds` message, proving the global exception middleware works perfectly.

6. **View Transaction History**
   - **Endpoint:** `GET /api/Transactions/{accountNumber}/history`
   - **Result:** Returns a chronologically ordered list of all your deposits and withdrawals, displaying the resulting historical balance after each transaction.

## Running Unit Tests
The project includes automated tests (xUnit and Moq) to validate the core business logic, such as the account number generator algorithm and the deposit/withdrawal rules.

To run them:
- **From Visual Studio:** Open the "Test Explorer" and click "Run All".
- **From Terminal:** Execute `dotnet test` at the root of the solution.
