using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank;
using BankApi.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.AccountOwner
{
    [Authorize(Roles = "AccountOwner")]
    [ApiController]
    [Route("[controller]")]
    public class AccountOwnerController : ControllerBase
    {
        private readonly AccountOwnerDbContext _database;

        public AccountOwnerController(AccountOwnerDbContext database)
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
    }
}