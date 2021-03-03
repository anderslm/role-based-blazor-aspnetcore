using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BankApi.EntityFramework
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options)
        {
            
        }
        
        public DbSet<AccountStatementModel> AccountStatements => Set<AccountStatementModel>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountStatementModel>().HasData(
                new List<AccountStatementModel>
                {
                    new("account-owner@andersmarchsteiner.onmicrosoft.com", DateTimeOffset.Now, 4200)
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}