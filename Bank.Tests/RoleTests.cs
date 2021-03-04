using FluentAssertions;
using Xunit;

namespace Bank.Tests
{
    public class RoleTests
    {
        [Fact]
        public void CanCreateBankCustomerRole()
        {
            Role.CreateFromString(BankCustomer.Name).Should().BeOfType<BankCustomer>();
        }
        
        [Fact]
        public void CanCreateAccountOwnerRole()
        {
            Role.CreateFromString(AccountOwner.Name).Should().BeOfType<AccountOwner>();
        }
    }
}