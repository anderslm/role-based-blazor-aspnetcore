using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Bank;
using BankApi.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [Authorize(Roles = "AccountOwner,BankCustomer")]
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {
        private readonly DatabaseContext _database;

        public BankController(DatabaseContext database)
        {
            _database = database;
        }

        [HttpGet]
        public List<Statement> Get()
        {
            return _database
                .AccountStatements
                .Select(m => new Statement(m.Amount, m.Timestamp))
                .ToList();
        }
        
        [HttpPost("Withdraw/{Amount}")]
        public async Task Withdraw(int amount)
        {
            await _database
                .AccountStatements
                .AddAsync(new AccountStatementModel(User.Identity.Name, DateTimeOffset.Now, amount * -1));

            await _database.SaveChangesAsync();
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