namespace BankingSystem;

public class Account
{
    private readonly string ownerName;
    private readonly IList<Transaction> transactionsHistory;
    private decimal balance;
        
    private static int accountsCount = 0;
    
    public Account(string ownerName, decimal balance)
    {
        this.AccountNumber = 100000000 + ++accountsCount;
        this.OwnerName = ownerName;
        this.Balance = balance;
        this.transactionsHistory = new List<Transaction>();
    }
    
    public int AccountNumber { get; }

    public string OwnerName
    {
        get => this.ownerName;
        private init
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "Owner name cannot be null");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Owner name cannot be empty", nameof(value));
            }

            this.ownerName = value;
        }
    }
    
    public decimal Balance
    {
        get => this.balance;
        private set
        {
            if (balance < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Balance is negative");
            }

            this.balance = value;
        }
    }

    public Transaction[] TransactionsHistory => this.transactionsHistory.ToArray();

    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount cannot be negative");
        }
        
        this.Balance += amount;
        this.transactionsHistory.Add(new Transaction(TransactionType.Deposit, amount));
    }

    public bool Withdraw(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdraw amount cannot be negative");
        }
        
        try
        {
            this.Balance -= amount;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return false;
        }

        this.transactionsHistory.Add(new Transaction(TransactionType.Withdrawal, amount));
        return true;
    }
}