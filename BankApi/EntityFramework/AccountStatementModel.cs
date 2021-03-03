using System;

namespace BankApi
{
    public class AccountStatementModel
    {
        public AccountStatementModel(string owner, DateTimeOffset timestamp, int amount)
        {
            Owner = owner;
            Timestamp = timestamp;
            Amount = amount;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Owner { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public int Amount { get; set; }
    }
}