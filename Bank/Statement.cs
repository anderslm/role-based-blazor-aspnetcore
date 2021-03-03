using System;

namespace Bank
{
    public record Statement
    {
        public Statement(int amount, DateTimeOffset timestamp)
        {
            Amount = amount;
            Timestamp = timestamp;
        }

        public int Amount { get; }
        public DateTimeOffset Timestamp { get; }
    }
}