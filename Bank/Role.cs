using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Bank.Tests")]

namespace Bank
{
    public abstract class Role
    {
        protected Role(string username) => Username = username;

        public string Username { get; }

        public static Role CreateFromString(string roleString, string username)
        {
            return roleString switch
            {
                BankCustomer.Name => new BankCustomer(username),
                AccountOwner.Name => new AccountOwner(username),
                _ => throw new Exception("You are not authorized")
            };
        }
    }

    public class BankCustomer : Role
    {
        public const string Name = "BankCustomer";

        protected internal BankCustomer(string username) : base(username) {}
    }

    public class AccountOwner : Role
    {
        public const string Name = "AccountOwner";

        protected internal AccountOwner(string username) : base(username) {}
    }
}