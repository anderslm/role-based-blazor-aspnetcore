using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
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