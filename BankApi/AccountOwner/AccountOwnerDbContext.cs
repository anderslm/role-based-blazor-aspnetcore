using System.Threading.Tasks;
using BankApi.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BankApi.AccountOwner
{
    public class AccountOwnerDbContext
    {
        private readonly Database _database;

        public AccountOwnerDbContext(Database database, Bank.AccountOwner accountOwner)
        {
            _database = database;
        }
        
        public DbSet<AccountStatementModel> AccountStatements => _database.AccountStatements;
        
        public Task SaveChangesAsync() => _database.SaveChangesAsync();
    }
}