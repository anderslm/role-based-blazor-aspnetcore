using System;
using System.Threading.Tasks;
using BankApi.AccountOwner;
using BankApi.EntityFramework;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bank.Tests
{
    public class DatabaseAuthorizationTests
    {
        private readonly Database _database = 
            new(new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        private string _accountOwnerName = "Some account owner";

        [Fact]
        public async Task AccountOwnerCanDepositMoneyToMyAccount()
        {
            var context = new AccountOwnerDbContext(_database, new AccountOwner(_accountOwnerName));

            await context.AccountStatements.AddAsync(new AccountStatementModel(_accountOwnerName, DateTimeOffset.Now, 10));

            await context.SaveChangesAsync();

            (await context.AccountStatements.LastAsync()).Amount.Should().Be(10);
        }

        [Fact]
        public async Task OthersCannotWithdrawMoneyFromMyAccount()
        {
            var context = new AccountOwnerDbContext(_database, new AccountOwner("Another username"));

            await context.AccountStatements.AddAsync(new AccountStatementModel(_accountOwnerName, DateTimeOffset.Now, -10));

            await Assert.ThrowsAsync<Exception>(() => context.SaveChangesAsync());
        }
        
        // Implement that bank customers cannot make withdrawals
        // Implement that bank customers can make deposits
    }
}