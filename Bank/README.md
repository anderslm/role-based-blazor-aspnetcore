# Bank
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
The problem with this sort of architecture is that it doesn't really tell you what the application
can do. Words like 'Controller' and 'EntityFramework' doesn't help us to understand the purpose
of the application and it doesn't help us to secure the application either.
As the application grows there will probably pop up new controllers in the 'Controller' directory
and database models in the 'EntityFramework' directory. This makes it more and more difficult to
separate code into the roles that is authorized to use it. 
Our architecture should help us to understand the problem domain and help us to organize
into the actors of the application.

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
This is already much better, but we can do better yet.