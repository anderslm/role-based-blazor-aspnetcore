using System;
using Microsoft.EntityFrameworkCore;

namespace BankApi
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options)
        {

        }

        public DbSet<Account> Accounts => Set<Account>();
    }

    public class Account
    {
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public int Amount { get; set; }
    }
}