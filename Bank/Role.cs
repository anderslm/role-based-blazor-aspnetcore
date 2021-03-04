using System;

namespace Bank
{
    public abstract class Role
    {
        public static Role CreateFromString(string roleString)
        {
            return roleString switch
            {
                BankCustomer.Name => new BankCustomer(),
                AccountOwner.Name => new AccountOwner(),
                _ => throw new Exception("You are not authorized")
            };
        }
    }

    public class BankCustomer : Role
    {
        public const string Name = "BankCustomer";
    }

    public class AccountOwner : Role
    {
        public const string Name = "AccountOwner";
    }
}