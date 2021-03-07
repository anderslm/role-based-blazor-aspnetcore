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

        private string _username = "account-owner@andersmarchsteiner.onmicrosoft.com";
        private readonly AccountOwnerDbContext _context;

        public DatabaseAuthorizationTests() => 
            _context = new AccountOwnerDbContext(_database, new AccountOwner(_username));
        
        [Fact]
        public async Task CanDepositMoneyToMyAccount()
        {
            var account = await _context.AccountStatements.SingleAsync();
            account.Amount = 100;

            var numberOfChanges = await _context.SaveChangesAsync();

            numberOfChanges.Should().Be(1);
        }

        [Fact]
        public async Task OthersCanNotWithdrawMoneyFromMyAccount()
        {
            var username = "Another username";
            var context = new AccountOwnerDbContext(_database, new AccountOwner(username));

            var account = await _context.AccountStatements.SingleAsync();
            account.Amount -= 10;

            await Assert.ThrowsAsync<Exception>(() => context.SaveChangesAsync());
        }
    }
}