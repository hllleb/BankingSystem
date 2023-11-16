using BankingSystem;

bool isRunning = true;
var accounts = new List<Account>();
while (isRunning)
{
    Console.WriteLine(@"1. Create Account
2. Deposit Money
3. Withdraw Money
4. Print Statement
5. Exit" + Environment.NewLine);

    Console.Write("Enter your choice: ");
    int choice = int.Parse(Console.ReadLine());

    switch (choice)
    {
        case 1:
            accounts.Add(CreateAccount());
            break;
        case 2:
            Deposit();
            break;
        case 3:
            Withdraw();
            break;
        case 4:
            PrintStatement();
            break;
        case 5:
            isRunning = false;
            break;
        default:
            Console.WriteLine(@"
Invalid choice. Try again.
");
            break;
    }
}

Account CreateAccount()
{
    Console.Write("Enter your name: ");
    string name = Console.ReadLine();
    Console.Write("Enter initial deposit amount: ");
    decimal initialAmount = decimal.Parse(Console.ReadLine());
    Account account;
    Console.WriteLine();
    try
    {
        account = new Account(name, initialAmount);
        Console.WriteLine($"Account created successfully! Your account number is {account.AccountNumber}.");
    }
    catch (ArgumentException)
    {
        account = null;
        Console.WriteLine("Account creation failed!");
    }

    return account;
}

void PrintAccountNumberIsInvalid() => Console.WriteLine("Account number is invalid");

void PrintStatement()
{
    var account = GetAccount();
    if (account is null)
    {
        PrintAccountNumberIsInvalid();
    }
    else
    {
        Console.WriteLine($"Account Number: {account.AccountNumber}");
        Console.WriteLine($"Owner Name: {account.OwnerName}");
        Console.WriteLine($"Balance: {account.Balance}");
    }

    Console.WriteLine();
}

Account? GetAccount()
{
    Console.Write("Enter account number: ");
    int accountNumber = int.Parse(Console.ReadLine());
    Console.WriteLine();
    return accounts!.FirstOrDefault(x => x.AccountNumber == accountNumber);
}

void Withdraw()
{
    var account = GetAccount();
    if (account is null)
    {
        PrintAccountNumberIsInvalid();
        Console.WriteLine();
        return;
    }

    Console.Write("Enter withdrawal amount: ");
    decimal amount = decimal.Parse(Console.ReadLine());

    Console.WriteLine();
    if (account.Withdraw(amount))
    {
        Console.WriteLine("Withdrawal successful!");
    }
    else
    {
        Console.WriteLine("Withdrawal failed!");
    }

    Console.WriteLine();
}

void Deposit()
{
    var account = GetAccount();
    if (account is null)
    {
        PrintAccountNumberIsInvalid();
        Console.WriteLine();
        return;
    }

    Console.Write("Enter deposit amount: ");
    decimal amount = decimal.Parse(Console.ReadLine());
    Console.WriteLine();
    account.Deposit(amount);
    Console.WriteLine("Deposit successful!");

    Console.WriteLine();
}