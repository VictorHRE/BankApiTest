using BankApiTest.API.Middlewares;
using BankApiTest.Application.Mappings;
using BankApiTest.Application.Services;
using BankApiTest.Core.Interfaces;
using BankApiTest.Infrastructure.Data;
using BankApiTest.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register DbContext (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Register Application Services
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddSingleton<IAccountNumberGenerator, AccountNumberGenerator>();

// Register AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

// Register Global Exception Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler(); // This enables the IExceptionHandler pipeline

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
