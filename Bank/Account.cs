using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank
{
    public record Account
    {
        private readonly Func<DateTimeOffset> _timestamper;
        private List<Statement> _statements;

        public Account(Func<DateTimeOffset> timestamper, List<Statement> statements)
        {
            _timestamper = timestamper;
            _statements = statements;
        }

        public List<Statement> Statements => 
            _statements.OrderByDescending(s => s.Timestamp).ToList();
        public int Sum => 
            Statements.Sum(s => s.Amount);

        public void Deposit(int amount)
            => _statements.Add(new Statement(amount, _timestamper()));

        public void Withdraw(int amount)
            => _statements.Add(new Statement(amount * -1, _timestamper()));
    }
}