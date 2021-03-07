# Bank
## Users
* account-owner@andersmarchsteiner.onmicrosoft.com
* bank-customer@andersmarchsteiner.onmicrosoft.com
## Use cases
### Account
```
As an account owner in the bank
I want to see all account statements 
So that I know when I've spend or earned money 
And how many money I've earned or spend
```
### Withdraw
```
As an account owner in the bank
I want to withdraw money from my account
So that I can use them to buy things
```
### Deposit
```
As a customer in the bank
I want to be able to deposit money in an account
So that I can pay salary to the account owner 
```

## Design

### Filesystem layout

#### Typical
This could be a typical layout of an AspNetCore Mvc application using Entity Framework:
```
BankApi
+-- Controllers
|   +-- BankController.cs
+-- EntityFramework
|   +-- EntityFrameworkContext.cs
+-- Startup.cs
+-- Program.cs
```
And an corresponding AspNetCore Blazor Website:
```
Website
+-- Pages
|   +-- Deposit.razor
|   +-- Withdraw.razor
|   +-- Index.razor
+-- Program.cs
```
The problem with this sort of architecture is that it doesn't really tell you what the application
can do. Words like 'Pages', 'Controller' and 'EntityFramework' doesn't help us to understand the purpose
of the application and it doesn't help us to secure the application either.
As the application grows there will probably pop up new controllers in the 'Controller' directory,
database models in the 'EntityFramework' directory and pages in the 'Pages' directory.
This makes it more and more difficult to separate code into the roles that is authorized to execute it.
Our architecture should help us to understand the problem domain and help us to organize code
into the roles of the application.

#### With actors (roles)
We should organize our code with the actors, roles, in mind.
The use cases of this application mentions two different actors; account owner and bank customer.
These actors are the roles in a role-base security setup. This should be obvious in our design:
```
BankApi
+-- AccountOwner
|   +-- AccountOwnerController.cs
|   +-- AccountOwnerDbContext.cs
+-- BankCustomer
|   +-- BankCustomerController.cs
|   +-- BankCustomerDbContext.cs
+-- Startup.cs
+-- Program.cs
```
```
Website
+-- AccountOwner
|   +-- Withdraw.razor
|   +-- Index.razor
+-- BankCustomer
|   +-- Deposit.razor
+-- Program.cs
```
This is already much better. At first glance we can see that this application has two actors, roles;
an account owner and a bank customer. We can even deduce that a bank customer can make some kind of
deposit and that the account owner can do something called withdraw.
Better yet we now know that everything in the directory called 'AccountOwner' should be protected
by authorization to the 'AccountOwner' role and same for 'BankCustomer'. Sometimes frameworks
helps you to do that. In Blazor we can use '_Imports.razor' files to do that.

### Roles as part of the business rules
Up until now the actual role authorization is done with the use of the 'Authorize' attribute both
in 'BankApi' and 'Website', eg:
```
[Authorize(Roles = "AccountOwner")]
```
That's fine. Whatever framework one uses it probably has some mechanism for role-based authorization
and that mechanism should be used.
But our design should also have a mechanism to ensure authorization. Frameworks changes fast and often and it's easy
to forget framework specific authorization during updates or when replacing an old framework with a newer framework.

But where to put authorization? We already have authorization in 'BankApi'.
But really; if a malicious person, lets call her Darlene, should find a way to exploit a weakness in 'BankApi'
and be able to call a method inside of it, what is it we want to protect?
The business rules? Not really. If Darlene is able to calculate the amount on an account after a deposit it doesn't change
anything. What we want to protect is if Darlene tries to calculate the amount on her own account after a deposit and save
it to the database. We want to protect the persistence of state. In our case the 'DbContext'.

