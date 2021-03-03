using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Bank.Tests
{
    public class AccountTests
    {
        private DateTimeOffset timestamp = DateTimeOffset.Now;
        private Account _account;

        public AccountTests() => _account = new Account(() => timestamp, new List<Statement>());
        
        [Fact]
        public void AccountStartsWithNoMoneyInIt()
            => _account.Sum.Should().Be(0);

        [Fact]
        public void DepositedAmountIsAddedToSum()
        {
            _account.Deposit(10);

            _account.Sum.Should().Be(10);
        }

        [Fact]
        public void WithdrawedAmountIsSubtractedFromSum()
        {
            _account.Deposit(20);
            _account.Withdraw(10);

            _account.Sum.Should().Be(10);
        }

        [Fact]
        public void StatementLineIsTimestamped()
        {
            _account.Deposit(10);

            _account.Statements[0].Timestamp.Should().Be(timestamp);
        }

        [Fact]
        public void StatementLinesIsSortedByTimestamp_NewestFirst()
        {
            timestamp = DateTimeOffset.Parse("1/1/2020");
            _account.Deposit(1);
            timestamp = DateTimeOffset.Parse("1/2/2020");
            _account.Withdraw(1);

            _account.Statements[0].Timestamp.Should().BeAfter(_account.Statements[1].Timestamp);
        }
    }
}