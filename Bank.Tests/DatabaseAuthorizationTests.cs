using System;
using System.Threading.Tasks;
using BankApi.AccountOwner;
using BankApi.EntityFramework;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bank.Tests
{
    public class DatabaseAuthorizationTests : IAsyncLifetime
    {
        private Database _database = 
            new(new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        private string _username;
        private AccountOwnerDbContext _context;

        public async Task InitializeAsync()
        {
            _username = "My username";
            _context = new AccountOwnerDbContext(_database, new AccountOwner(_username));
            await _context.AccountStatements.AddAsync(new AccountStatementModel(_username, DateTimeOffset.Now, 0));
            await _context.SaveChangesAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;

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