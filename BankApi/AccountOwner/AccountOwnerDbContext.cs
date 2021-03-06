using System;
using System.Threading.Tasks;
using BankApi.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BankApi.AccountOwner
{
    public class AccountOwnerDbContext
    {
        private readonly Database _database;
        private readonly Bank.AccountOwner _accountOwner;

        public AccountOwnerDbContext(Database database, Bank.AccountOwner accountOwner)
        {
            _database = database;
            _accountOwner = accountOwner;
        }
        
        public DbSet<AccountStatementModel> AccountStatements => _database.AccountStatements;

        public Task<int> SaveChangesAsync()
        {
            foreach (var entry in _database.ChangeTracker.Entries())
            {
                if (entry.Entity is AccountStatementModel accountStatement)
                    if (entry.State != EntityState.Unchanged)
                        if (accountStatement.Owner != _accountOwner.Username)
                            throw new Exception("You are not authorized to save changes");
            }

            return _database.SaveChangesAsync();
        }
    }
}