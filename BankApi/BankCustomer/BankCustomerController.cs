using System;
using System.Threading.Tasks;
using BankApi.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.BankCustomer
{
    [Authorize(Roles = "BankCustomer")]
    [ApiController]
    [Route("[controller]")]
    public class BankCustomerController : ControllerBase
    {
        private readonly BankCustomerDbContext _database;

        public BankCustomerController(BankCustomerDbContext database)
        {
            _database = database;
        }

        [HttpPost("Deposit/{Amount}")]
        public async Task Deposit(int amount)
        {
            await _database
                .AccountStatements
                .AddAsync(new AccountStatementModel(User.Identity.Name, DateTimeOffset.Now, amount));

            await _database.SaveChangesAsync();
        }
    }
}