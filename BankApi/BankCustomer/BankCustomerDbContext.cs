using System.Threading.Tasks;
using BankApi.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BankApi.BankCustomer
{
    public class BankCustomerDbContext
    {
        private readonly Database _database;

        public BankCustomerDbContext(Database database, Bank.BankCustomer bankCustomer)
        {
            _database = database;
        }

        public DbSet<AccountStatementModel> AccountStatements => _database.AccountStatements;

        public Task SaveChangesAsync() => _database.SaveChangesAsync();
    }
}